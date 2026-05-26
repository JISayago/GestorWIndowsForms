using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.OpcionesPagos;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.Extras;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.Helpers.VentaEnum;
using Servicios.Infraestructura;
using Servicios.LogicaNegocio.Venta.TipoPago;
using Servicios.LogicaNegocio.Venta.VentaLibre.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta.VentaLibre
{
    public class VentaLibreServicio : IVentaLibreServicio
    {
        private readonly IPdfGenerator _pdf;

        public VentaLibreServicio()
        {
            _pdf = new PdfGenerator();
        }
        private void GeneracionComprobanteVentaLibre(GestorContextDB context, AccesoDatos.Entidades.VentaLibre venta)
        {
            if (venta == null)
            {
                Debug.WriteLine("VentaLibre es null al generar comprobante.");
                return;
            }

            var ventaCompleta = context.VentasLibres
                .Include(v => v.VentaPagoDetalles)
                    .ThenInclude(p => p.TipoPago)
                .Include(v => v.Empleado)
                    .ThenInclude(e => e.Persona)
                .Include(v => v.Vendedor)
                    .ThenInclude(v => v.Persona)
                .Include(v => v.Cliente)
                    .ThenInclude(c => c.Persona)
                .FirstOrDefault(v => v.VentaLibreId == venta.VentaLibreId);

            // 🔥 3. validar resultado
            if (ventaCompleta == null)
            {
                Debug.WriteLine($"No se encontró VentaLibreId: {venta.VentaLibreId}");
                return;
            }

            _pdf.GenerarVentaLibre(ventaCompleta);
        }
        public EstadoOperacion AnularVentaLibre(long ventaLibreId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var venta = context.VentasLibres
                    .Include(v => v.VentaPagoDetalles)
                    .FirstOrDefault(v => v.VentaLibreId == ventaLibreId);

                if (venta == null)
                {
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "La venta no existe."
                    };
                }

                if (venta.Estado == (int)EstadoVenta.Cancelada)
                {
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "La venta ya está anulada."
                    };
                }

                venta.Estado = (int)EstadoVenta.Cancelada;

                var dto = new VentaLibreDTO
                {
                    IdEmpleado = venta.IdEmpleado,
                    IdVendedor = venta.IdVendedor,
                    IdCliente = venta.IdCliente,
                    FechaVenta = DateTime.Now,

                    Total = venta.Total,
                    Estado = (int)EstadoVenta.CancelacionVenta,
                    Detalle = $"Cancelación de venta libre N° {venta.NumeroVenta}",

                    MontoPagado = venta.MontoPagado,
                    MontoAdeudado = venta.MontoAdeudado,

                    TiposDePagoSeleccionado = venta.VentaPagoDetalles.Select(p => new FormaPago
                    {
                        TipoDePago = (TipoDePago)p.IdTipoPago,
                        Monto = p.Monto
                    }).ToList()
                };

                // 🔥 CAPTURAR CONTRAVENTA
                var ventaCancelacion = CrearVentaLibreInterna(
                    context,
                    dto,
                    TipoMovimientoDetalle.Cancelacion
                );

                context.SaveChanges();
                transaction.Commit();

                // 🔥 PDF DE CANCELACIÓN
                try
                {
                    GeneracionComprobanteVentaLibre(context, ventaCancelacion);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error PDF cancelación venta libre: " + ex.Message);
                }

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Venta anulada y contraventa generada correctamente."
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = ex.ToString()
                };
            }
        }
        public EstadoOperacion NuevaVentaLibre(VentaLibreDTO dto)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var venta = CrearVentaLibreInterna(
                    context,
                    dto,
                    TipoMovimientoDetalle.VentaLibre
                );

                context.SaveChanges();
                transaction.Commit();

                // 🔥 PDF
                try
                {
                    GeneracionComprobanteVentaLibre(context, venta);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error PDF venta libre: " + ex.Message);
                }

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Venta libre registrada correctamente."
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = ex.ToString()
                };
            }
        }

        public AccesoDatos.Entidades.VentaLibre CrearVentaLibreInterna(
       GestorContextDB context,
       VentaLibreDTO dto,
       TipoMovimientoDetalle movimientoDetalle)
        {
            try
            {
                var cajaServicio = new Caja.CajaServicio();
                var cajaId = cajaServicio.ObtenerIdDeEña(context);

                if (!cajaId.HasValue)
                    throw new Exception("No hay una caja abierta.");

                bool esCancelacion = dto.Estado == (int)EstadoVenta.CancelacionVenta;

                // 🔢 Número comprobante
                var fecha = DateTime.Today;
                var prefijo = esCancelacion ? "CANVL" : "VLIB";

                var cantidadHoy = context.VentasLibres.Count(v =>
                    v.NumeroVenta.StartsWith($"{prefijo}-{fecha:yyyyMMdd}")
                );

                dto.NumeroVenta = GeneradorNumeroComprobante.Generar(
                    prefijo,
                    fecha,
                    cantidadHoy
                );

                // 🧾 Crear entidad (TODO POSITIVO)
                var venta = new AccesoDatos.Entidades.VentaLibre
                {
                    NumeroVenta = dto.NumeroVenta,
                    IdEmpleado = dto.IdEmpleado,
                    IdVendedor = dto.IdVendedor,
                    IdCliente = dto.IdCliente,
                    FechaVenta = dto.FechaVenta,
                    Total = Math.Abs(dto.Total),
                    Estado = dto.Estado,
                    Detalle = dto.Detalle,
                    MontoPagado = Math.Abs(dto.MontoPagado),
                    MontoAdeudado = Math.Abs(dto.MontoAdeudado),
                };

                context.VentasLibres.Add(venta);
                context.SaveChanges();

                // 🔁 Movimiento
                var movimientoServicio = new Movimiento.MovimientoServicio();

                movimientoServicio.CrearMovimientoVenta(
                    venta.VentaLibreId,
                    venta.Total,
                    venta.Estado,
                    esCancelacion ? TipoMovimientoDetalle.Cancelacion : movimientoDetalle,
                    TipoEntidadMovimiento.VentaLibre,
                    context
                );

                // 🏦 Caja (SIN SIGNO, SOLO TIPO)
                var tipoMovimientoCaja = esCancelacion
                    ? TipoMovimiento.Egreso
                    : TipoMovimiento.Ingreso;

                cajaServicio.RegistrarTransaccion(
                    context,
                    venta.MontoPagado,
                    tipoMovimientoCaja,
                    cajaId.Value
                );

                // 💳 Pagos (SIEMPRE POSITIVOS)
                if (dto.TiposDePagoSeleccionado != null && dto.TiposDePagoSeleccionado.Any())
                {
                    var servicioTP = new TipoPagoServicio();

                    var pagos = dto.TiposDePagoSeleccionado.Select(p => new VentaPagoDetalle
                    {
                        IdVentaLibre = venta.VentaLibreId,
                        IdTipoPago = servicioTP
                            .ObtenerTipoPagoPorNumero(context, Convert.ToInt32(p.TipoDePago.Value))
                            .TipoPagoId,
                        Monto = Math.Abs(p.Monto)
                    }).ToList();

                    context.VentaPagosDetalles.AddRange(pagos);
                }

                context.SaveChanges();

                return venta;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR EN VENTA LIBRE");
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        public ResultadoPaginacion<VentaLibreDTO> ObtenerVentasLibres(FiltroConsulta filtros)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            string collation = "Latin1_General_CI_AI";
            var query = context.VentasLibres
                .AsNoTracking()
                .Include(v => v.Empleado).ThenInclude(e => e.Persona)
                .Include(v => v.Vendedor).ThenInclude(e => e.Persona)
                .Include(v => v.Cliente).ThenInclude(c => c.Persona)
                .AsQueryable();

            // =========================================================
            // 🧠 CORE: ELIMINADOS + HISTORICO
            // =========================================================

            if (filtros.Bool2)
            {
                // 👉 HISTÓRICO → trae TODO (no filtra nada)
            }
            else if (filtros.Bool1)
            {
                // 👉 SOLO ELIMINADAS
                query = query.Where(v => v.Estado == (int)EstadoVenta.Cancelada);
            }
            else
            {
                // 👉 DEFAULT → NO eliminadas + último mes
                var desde = DateTime.Now.AddMonths(-1);

                query = query.Where(v =>
                    v.Estado != (int)EstadoVenta.Cancelada &&
                    v.FechaVenta >= desde);
            }

            // =========================================================
            // 🔍 BUSQUEDA
            // =========================================================

            if (!string.IsNullOrWhiteSpace(filtros.TextoBuscar))
            {
                var texto = filtros.TextoBuscar.Trim();

                switch (filtros.Filtro1?.ToString())
                {
                    case "NumeroVenta":

                        query = query.Where(v =>
                            EF.Functions.Collate(v.NumeroVenta, collation)
                                .Contains(texto));

                        break;

                    case "ClienteNombreCompleto":

                        query = query.Where(v =>
                            v.Cliente != null &&
                            EF.Functions.Collate(
                                (v.Cliente.Persona.Nombre + " " + v.Cliente.Persona.Apellido),
                                collation
                            ).Contains(texto));

                        break;

                    default:

                        query = query.Where(v =>
                            EF.Functions.Collate(v.NumeroVenta, collation)
                                .Contains(texto)
                            ||
                            (
                                v.Cliente != null &&
                                EF.Functions.Collate(
                                    (v.Cliente.Persona.Nombre + " " + v.Cliente.Persona.Apellido),
                                    collation
                                ).Contains(texto)
                            ));

                        break;
                }
            }

            // =========================================================
            // 🔴 FILTRO ESTADO (cbx2)
            // =========================================================

            if (filtros.Filtro2 != null &&
                int.TryParse(filtros.Filtro2.ToString(), out var estado))
            {
                query = query.Where(v => v.Estado == estado);
            }

            // =========================================================
            // 📅 FILTRO FECHA (cbx3 + picker)
            // =========================================================

            bool usaFechas = filtros.FechaDesde.HasValue || filtros.FechaHasta.HasValue;

            if (usaFechas && filtros.Filtro3?.ToString() == "FVL")
            {
                if (filtros.FechaDesde.HasValue)
                {
                    query = query.Where(v =>
                        v.FechaVenta >= filtros.FechaDesde.Value);
                }

                if (filtros.FechaHasta.HasValue)
                {
                    var hasta = filtros.FechaHasta.Value.AddDays(1);

                    query = query.Where(v =>
                        v.FechaVenta < hasta);
                }
            }

            // =========================================================
            // 📊 TOTAL
            // =========================================================

            var total = query.Count();

            // =========================================================
            // 🔴 PAGINACION
            // =========================================================

            var totalPaginas = (int)Math.Ceiling((double)total / filtros.PageSize);

            if (totalPaginas <= 0)
                totalPaginas = 1;

            if (filtros.Page > totalPaginas)
                filtros.Page = totalPaginas;

            if (filtros.Page < 1)
                filtros.Page = 1;

            // =========================================================
            // 📦 DATA
            // =========================================================

            var data = query
                .OrderByDescending(v => v.FechaVenta)
                .Skip((filtros.Page - 1) * filtros.PageSize)
                .Take(filtros.PageSize)
                .Select(v => new VentaLibreDTO
                {
                    VentaLibreId = v.VentaLibreId,

                    NumeroVenta = v.NumeroVenta,

                    FechaVenta = v.FechaVenta,

                    IdEmpleado = v.IdEmpleado,
                    EmpleadoNombreCompleto =
                        v.Empleado.Persona.Nombre + " " +
                        v.Empleado.Persona.Apellido,

                    IdVendedor = v.IdVendedor,
                    VendedorNombreCompleto =
                        v.Vendedor.Persona.Nombre + " " +
                        v.Vendedor.Persona.Apellido,

                    IdCliente = v.IdCliente,
                    ClienteNombreCompleto = v.Cliente != null
                        ? v.Cliente.Persona.Nombre + " " +
                          v.Cliente.Persona.Apellido
                        : "Consumidor Final",

                    Total = v.Total,

                    Estado = v.Estado,

                    Detalle = v.Detalle,

                    MontoPagado = v.MontoPagado,
                    MontoAdeudado = v.MontoAdeudado
                })
                .ToList();

            // =========================================================
            // 📄 RESULTADO
            // =========================================================

            return new ResultadoPaginacion<VentaLibreDTO>
            {
                Items = data,
                TotalRegistros = total,
                Page = filtros.Page,
                PageSize = filtros.PageSize
            };
        }
    }
}
