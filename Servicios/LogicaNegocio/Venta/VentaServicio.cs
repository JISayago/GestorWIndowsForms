using AccesoDatos;
using AccesoDatos.Entidades;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Empleado.DTO;
using Servicios.LogicaNegocio.Venta.DTO;
using Servicios.LogicaNegocio.Venta.TipoPago;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta
{
    public class VentaServicio : IVentaServicio
    {
        public VentaDTO ObtenerVentaPorId(long ventaId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            var venta = context.Ventas
                .FirstOrDefault(v => v.VentaId == ventaId);

            if (venta == null)
                throw new Exception("No se encontró la marca.");

            return new VentaDTO
            {
                VentaId = venta.VentaId,
                NumeroVenta = venta.NumeroVenta,
                IdEmpleado = venta.IdEmpleado,
                IdVendedor = venta.IdVendedor,
                FechaVenta = venta.FechaVenta,
                Total = venta.Total,
                TotalSinDescuento = venta.TotalSinDescuento,
                Descuento = venta.Descuento,
                Estado = venta.Estado,
                Detalle = venta.Detalle,
                MontoAdeudado = venta.MontoAdeudado,
                MontoPagado = venta.MontoPagado
            };
        }

        public string GenerateNextNumeroVenta()
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var valores = context.Ventas
                .Where(v => v.NumeroVenta != null)
                .Select(v => v.NumeroVenta)
                .AsEnumerable()  
                .Select(s =>
                {
                    var digits = new string(s.Where(char.IsDigit).ToArray());
                    if (long.TryParse(digits, out var n))
                        return n;
                    return 0L;
                });

            var max = valores.DefaultIfEmpty(0L).Max();
            var siguiente = max + 1;

            
            return siguiente.ToString().PadLeft(15, '0');
        }

        private string NormalizeNumeroVenta(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return null;

            var digits = new string(raw.Where(char.IsDigit).ToArray());
            if (string.IsNullOrEmpty(digits))
                return null;

        
            if (digits.Length > 15)
                digits = digits.Substring(digits.Length - 15);

            return digits.PadLeft(15, '0');
        }

        public EstadoOperacion NuevaVenta(VentaDTO ventaDto)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            using var transaction = context.Database.BeginTransaction();
            try
            {
               
                string numeroFinal = NormalizeNumeroVenta(ventaDto.NumeroVenta);
                if (numeroFinal == null)
                {
                    numeroFinal = GenerateNextNumeroVenta();
                }
                else
                {
                   
                    if (context.Ventas.Any(v => v.NumeroVenta == numeroFinal))
                    {
                        return new EstadoOperacion
                        {
                            Exitoso = false,
                            Mensaje = "Ya existe una venta con el mismo Número."
                        };
                    }
                }

                var venta = new AccesoDatos.Entidades.Venta
                {
                    NumeroVenta = numeroFinal,
                    IdEmpleado = ventaDto.IdEmpleado,
                    IdVendedor = ventaDto.IdVendedor,
                    FechaVenta = ventaDto.FechaVenta,
                    Total = ventaDto.Total,
                    TotalSinDescuento = ventaDto.TotalSinDescuento,
                    Descuento = ventaDto.Descuento,
                    Estado = ventaDto.Estado,
                    Detalle = ventaDto.Detalle,
                    MontoAdeudado = ventaDto.MontoAdeudado,
                    MontoPagado = ventaDto.MontoPagado
                };

                context.Ventas.Add(venta);
                context.SaveChanges();

                var movimientoServicio = new Movimiento.MovimientoServicio();
                movimientoServicio.CrearMovimientoVenta(new VentaDTO
                {
                    NumeroVenta = venta.NumeroVenta,
                    VentaId = venta.VentaId,
                    Total = venta.Total
                },context);


                // si se trata de oferta hay que hacer una iteracion de cada item de esa oferta para independizar los id de los productos afectados con la nueva propiedad de es oferta para identificarlos
                if (ventaDto.Items != null && ventaDto.Items.Any())
                {
                    var detallesV = ventaDto.Items.Select(d => new AccesoDatos.Entidades.DetallesVenta
                    {
                        IdVenta = venta.VentaId,
                        IdProducto = d.ItemId,
                        Cantidad = d.Cantidad,
                        Subtotal = d.PrecioVenta * d.Cantidad,
                    }).ToList();

                    context.DetallesVentas.AddRange(detallesV);
                }

                if (ventaDto.TiposDePagoSeleccionado != null && ventaDto.TiposDePagoSeleccionado.Any())
                {
                    var servicioTP = new TipoPagoServicio();
                    var pagos = ventaDto.TiposDePagoSeleccionado.Select(p => new AccesoDatos.Entidades.VentaPagoDetalle
                    {
                        IdVenta = venta.VentaId,
                        IdTipoPago = p.TipoDePago.HasValue? servicioTP.ObtenerTipoPagoPorNumero(Convert.ToInt32(p.TipoDePago.Value)).TipoPagoId
                            : 0,
                        Monto = p.Monto
                    }).ToList();

                    context.VentaPagosDetalles.AddRange(pagos);
                }

                context.SaveChanges();
                transaction.Commit();

                return new EstadoOperacion { Exitoso = true, Mensaje = "Venta creada correctamente." };
            }
            catch (Exception ex)
            {
                try { transaction.Rollback(); } catch { /* log */ }
                return new EstadoOperacion { Exitoso = false, Mensaje = "Error al crear la venta: " + ex.Message };
            }
        }
    }
}
