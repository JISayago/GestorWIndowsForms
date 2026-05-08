using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.Extras;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.Helpers.VentaEnum;
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
        public EstadoOperacion AnularVentaLibre(long ventaLibreId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            using var transaction = context.Database.BeginTransaction();

            try
            {
                var venta = context.VentasLibres
                    .FirstOrDefault(v => v.VentaLibreId == ventaLibreId);

                if (venta == null)
                {
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "La venta no existe."
                    };
                }

                if (venta.Estado == (int)EstadoVenta.Cancelada) // Anulado
                {
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "La venta ya se encuentra anulada."
                    };
                }

                venta.Estado = (int)EstadoVenta.Cancelada;

                var cajaServicio = new Caja.CajaServicio();
                var cajaId = cajaServicio.ObtenerIdDeEña(context);

                if (!cajaId.HasValue)
                    throw new Exception("No hay una caja abierta para revertir la operación.");

                // 🏦 Caja (correcto)
                cajaServicio.RegistrarTransaccion(
                    context,
                    venta.Total,
                    venta.Total >= 0 ? TipoMovimiento.Egreso : TipoMovimiento.Ingreso,
                    cajaId.Value
                );

                // 🔁 Movimiento inverso (corregido)
                var movimientoServicio = new Movimiento.MovimientoServicio();

                movimientoServicio.CrearMovimientoVenta(
                    venta.VentaLibreId,
                    venta.Total,
                    venta.Estado,
                    TipoMovimientoDetalle.Cancelacion,
                    TipoEntidadMovimiento.VentaLibre, // 🔥 nuevo
                    context
                );

                context.SaveChanges();

                transaction.Commit();

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Venta anulada correctamente.",
                    EntidadId = venta.VentaLibreId
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

                transaction.Commit();

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

                // 🔢 Número comprobante
                var fecha = DateTime.Today;
                var prefijo = dto.Estado == (int)EstadoVenta.Cancelada ? "CAN" : "VLIB";

                var cantidadHoy = context.VentasLibres.Count(v =>
                    v.NumeroVenta.StartsWith($"{prefijo}-{fecha:yyyyMMdd}")
                );

                dto.NumeroVenta = GeneradorNumeroComprobante.Generar(
                    prefijo,
                    fecha,
                    cantidadHoy
                );

                // 🧾 Crear entidad
                var venta = new AccesoDatos.Entidades.VentaLibre
                {
                    NumeroVenta = dto.NumeroVenta,
                    IdEmpleado = dto.IdEmpleado,
                    IdVendedor = dto.IdVendedor,
                    IdCliente = dto.IdCliente,
                    FechaVenta = dto.FechaVenta,
                    Total = dto.Total,
                    Estado = dto.Estado,
                    Detalle = dto.Detalle,
                    MontoPagado = dto.MontoPagado,
                    MontoAdeudado = dto.MontoAdeudado
                };

                context.VentasLibres.Add(venta);
                context.SaveChanges();

                // 💰 Movimiento
                var movimientoServicio = new Movimiento.MovimientoServicio();

                movimientoServicio.CrearMovimientoVenta(
                    venta.VentaLibreId,
                    venta.Total,
                    venta.Estado,
                    movimientoDetalle,
                    TipoEntidadMovimiento.VentaLibre, // 👈 🔥 CLAVE
                    context
                );

                // 🏦 Caja
                cajaServicio.RegistrarTransaccion(
                    context,
                    venta.Total,
                    venta.Total >= 0 ? TipoMovimiento.Ingreso : TipoMovimiento.Egreso,
                    cajaId.Value
                );

                // 💳 Pagos
                if (dto.TiposDePagoSeleccionado != null && dto.TiposDePagoSeleccionado.Any())
                {
                    var servicioTP = new TipoPagoServicio();

                    var pagos = dto.TiposDePagoSeleccionado.Select(p => new VentaPagoDetalle
                    {
                        IdVentaLibre = venta.VentaLibreId,
                        IdTipoPago = servicioTP
                            .ObtenerTipoPagoPorNumero(context, Convert.ToInt32(p.TipoDePago.Value))
                            .TipoPagoId,
                        Monto = p.Monto
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

            var query = context.VentasLibres
                .AsNoTracking()
                .Include(v => v.Empleado)
                    .ThenInclude(e => e.Persona)
                .Include(v => v.Vendedor)
                    .ThenInclude(e => e.Persona)
                .Include(v => v.Cliente)
                    .ThenInclude(c => c.Persona)
                .AsQueryable();

            // 🔍 BUSQUEDA
            if (!string.IsNullOrWhiteSpace(filtros.TextoBuscar))
            {
                var texto = filtros.TextoBuscar.Trim().ToLower();

                switch (filtros.Filtro1?.ToString())
                {
                    case "NumeroVenta":
                        query = query.Where(v =>
                            v.NumeroVenta.ToLower().Contains(texto));
                        break;

                    case "ClienteNombreCompleto":
                        query = query.Where(v =>
                            v.Cliente != null &&
                            (
                                (v.Cliente.Persona.Nombre + " " + v.Cliente.Persona.Apellido)
                                .ToLower()
                                .Contains(texto)
                            ));
                        break;

                    default:
                        query = query.Where(v =>
                            v.NumeroVenta.ToLower().Contains(texto)
                            ||
                            (
                                v.Cliente != null &&
                                (
                                    (v.Cliente.Persona.Nombre + " " + v.Cliente.Persona.Apellido)
                                    .ToLower()
                                    .Contains(texto)
                                )
                            ));
                        break;
                }
            }

            // 📅 FILTRO FECHA
            if (filtros.FechaDesde.HasValue)
            {
                query = query.Where(v =>
                    v.FechaVenta.Date >= filtros.FechaDesde.Value.Date);
            }

            if (filtros.FechaHasta.HasValue)
            {
                var fechaHasta = filtros.FechaHasta.Value.Date.AddDays(1);

                query = query.Where(v =>
                    v.FechaVenta < fechaHasta);
            }

            // 🔴 FILTRO ESTADO
            if (filtros.Filtro2 != null &&
                int.TryParse(filtros.Filtro2.ToString(), out var estado))
            {
                if (Enum.IsDefined(typeof(EstadoVenta), estado))
                {
                    query = query.Where(v => v.Estado == estado);
                }
            }

            // 📊 TOTAL
            var total = query.Count();

            // 🔴 CONTROL PAGINACION
            var totalPaginas = (int)Math.Ceiling((double)total / filtros.PageSize);

            if (totalPaginas <= 0)
                totalPaginas = 1;

            if (filtros.Page > totalPaginas)
                filtros.Page = totalPaginas;

            if (filtros.Page < 1)
                filtros.Page = 1;

            // 📦 DATA
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
