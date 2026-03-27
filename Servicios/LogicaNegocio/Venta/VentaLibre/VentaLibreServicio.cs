using AccesoDatos;
using AccesoDatos.Entidades;
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
        public EstadoOperacion NuevaVentaLibre(VentaLibreDTO dto)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            using var transaction = context.Database.BeginTransaction();

            try
            {
                var venta = CrearVentaLibreInterna(
                    context,
                    dto,
                    TipoMovimientoDetalle.Venta
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
                    venta.Total,
                    movimientoDetalle,
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
    }
}
