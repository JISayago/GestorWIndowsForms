using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers;
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

                if (venta.Estado == 3) // Anulado
                {
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "La venta ya se encuentra anulada."
                    };
                }

                venta.Estado = 3;

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
                    -venta.Total, // 🔥 invertir signo
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
                var prefijo = dto.Total < 0 ? "CAN" : "VLIB";

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
                    venta.Total, // 👈 negativo si es cancelación
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

        public IEnumerable<VentaLibreDTO> ObtenerVentasLibres(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.VentasLibres
                .Include(v => v.Empleado).ThenInclude(e => e.Persona)
                .Include(v => v.Vendedor).ThenInclude(e => e.Persona)
                .Include(v => v.Cliente).ThenInclude(c => c.Persona)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(cadenaBuscar))
            {
                cadenaBuscar = cadenaBuscar.ToLower();

                switch (cadenaBuscar)//ojo con la columna no es cadena es columna
                {
                    case "Numero":
                        query = query.Where(v => v.NumeroVenta.ToLower().Contains(cadenaBuscar));
                        break;

                    case "Cliente":
                        query = query.Where(v =>
                            v.Cliente != null &&
                            (v.Cliente.Persona.Nombre + " " + v.Cliente.Persona.Apellido)
                                .ToLower()
                                .Contains(cadenaBuscar));
                        break;

                    case "Vendedor":
                        query = query.Where(v =>
                            (v.Vendedor.Persona.Nombre + " " + v.Vendedor.Persona.Apellido)
                                .ToLower()
                                .Contains(cadenaBuscar));
                        break;

                    case "Empleado":
                        query = query.Where(v =>
                            (v.Empleado.Persona.Nombre + " " + v.Empleado.Persona.Apellido)
                                .ToLower()
                                .Contains(cadenaBuscar));
                        break;

                    default:
                        query = query.Where(v =>
                            v.NumeroVenta.ToLower().Contains(cadenaBuscar) ||
                            (v.Cliente != null &&
                             (v.Cliente.Persona.Nombre + " " + v.Cliente.Persona.Apellido)
                                .ToLower()
                                .Contains(cadenaBuscar))
                        );
                        break;
                }
            }

            var lista = query
                .OrderByDescending(v => v.FechaVenta)
                .Select(v => new VentaLibreDTO
                {
                    VentaLibreId = v.VentaLibreId,

                    NumeroVenta = v.NumeroVenta,
                    FechaVenta = v.FechaVenta,

                    IdEmpleado = v.IdEmpleado,
                    EmpleadoNombreCompleto = v.Empleado.Persona.Nombre + " " + v.Empleado.Persona.Apellido,

                    IdVendedor = v.IdVendedor,
                    VendedorNombreCompleto = v.Vendedor.Persona.Nombre + " " + v.Vendedor.Persona.Apellido,

                    IdCliente = v.IdCliente,
                    ClienteNombreCompleto = v.Cliente != null
                        ? v.Cliente.Persona.Nombre + " " + v.Cliente.Persona.Apellido
                        : "Consumidor Final",

                    Total = v.Total,
                    Estado = v.Estado,
                    Detalle = v.Detalle,

                    MontoPagado = v.MontoPagado,
                    MontoAdeudado = v.MontoAdeudado
                })
                .ToList();

            return lista;
        }
        public List<VentaLibreDTO> ObtenerVentasLibresFiltrados(
    string textoBuscar = null,
    int? estado = null,
    DateTime? fechaDesde = null,
    DateTime? fechaHasta = null)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.VentasLibres
                .Include(v => v.Empleado).ThenInclude(e => e.Persona)
                .Include(v => v.Vendedor).ThenInclude(e => e.Persona)
                .Include(v => v.Cliente).ThenInclude(c => c.Persona)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(textoBuscar))
            {
                query = query.Where(v =>
                    v.Detalle.Contains(textoBuscar) ||
                    v.NumeroVenta.Contains(textoBuscar) ||
                    (v.Cliente != null &&
                        (v.Cliente.Persona.Nombre + " " + v.Cliente.Persona.Apellido)
                            .Contains(textoBuscar)) ||
                    (v.Vendedor.Persona.Nombre + " " + v.Vendedor.Persona.Apellido)
                            .Contains(textoBuscar) ||
                    (v.Empleado.Persona.Nombre + " " + v.Empleado.Persona.Apellido)
                            .Contains(textoBuscar)
                );
            }

            if (estado.HasValue)
            {
                query = query.Where(v => v.Estado == estado.Value);
            }
            else
            {
                query = query.Where(v => v.Estado > 0);
            }

            if (fechaDesde.HasValue)
            {
                query = query.Where(v => v.FechaVenta >= fechaDesde.Value);
            }

            if (fechaHasta.HasValue)
            {
                var hastaReal = fechaHasta.Value.AddDays(1);
                query = query.Where(v => v.FechaVenta < hastaReal);
            }

            var lista = query
                .OrderByDescending(v => v.FechaVenta)
                .Select(v => new VentaLibreDTO
                {
                    VentaLibreId = v.VentaLibreId,

                    NumeroVenta = v.NumeroVenta,
                    FechaVenta = v.FechaVenta,

                    IdEmpleado = v.IdEmpleado,
                    EmpleadoNombreCompleto = v.Empleado.Persona.Nombre + " " + v.Empleado.Persona.Apellido,

                    IdVendedor = v.IdVendedor,
                    VendedorNombreCompleto = v.Vendedor.Persona.Nombre + " " + v.Vendedor.Persona.Apellido,

                    IdCliente = v.IdCliente,
                    ClienteNombreCompleto = v.Cliente != null
                        ? v.Cliente.Persona.Nombre + " " + v.Cliente.Persona.Apellido
                        : "Consumidor Final",

                    Total = v.Total,
                    Estado = v.Estado,
                    Detalle = v.Detalle,

                    MontoPagado = v.MontoPagado,
                    MontoAdeudado = v.MontoAdeudado
                })
                .ToList();

            return lista;
        }
    }
}
