using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers;
using Servicios.Infraestructura;
using Servicios.LogicaNegocio.Caja;
using Servicios.LogicaNegocio.Empleado.DTO;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Venta.DTO;
using Servicios.LogicaNegocio.Venta.TipoPago;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servicios.LogicaNegocio.Venta
{
    public class VentaServicio : IVentaServicio
    {
        private readonly IPdfGenerator _pdf;
        private readonly IProductoServicio _productoServicio;

        public VentaServicio() : this(new PdfGenerator())
        {
            _productoServicio = new ProductoServicio();
        }

        public VentaServicio(IPdfGenerator pdf)
        {
            _pdf = pdf;
        }

        public string GenerarPdfDeVenta(AccesoDatos.Entidades.Venta venta)
        {
            return _pdf.GenerarComprobante(venta);
        }

        public AccesoDatos.Entidades.Venta CrearVentaInterna(GestorContextDB context, VentaDTO ventaDto, TipoMovimientoDetalle movimientoDetalle)
        {
            var cajaServicio = new Caja.CajaServicio();
            var cajaId = cajaServicio.ObtenerIdCajaAbierta(context);

            if (cajaId.HasValue)
            {

                var fecha = DateTime.Today;
                var prefijo = ventaDto.Total < 0 ? "CAN" : "VEN";

                var cantidadHoy = context.Ventas.Count(v =>
                    v.NumeroVenta.StartsWith($"{prefijo}-{fecha:yyyyMMdd}")
                );

                ventaDto.NumeroVenta = GeneradorNumeroComprobante.Generar(
                    prefijo,
                    fecha,
                    cantidadHoy
                );

                var venta = new AccesoDatos.Entidades.Venta
                {
                    NumeroVenta = ventaDto.NumeroVenta,
                    IdEmpleado = ventaDto.IdEmpleado,
                    IdVendedor = ventaDto.IdVendedor,
                    IdCliente = ventaDto.IdCliente,
                    FechaVenta = ventaDto.FechaVenta,
                    Total = ventaDto.Total,
                    TotalSinDescuento = ventaDto.TotalSinDescuento,
                    Descuento = ventaDto.Descuento,
                    Estado = ventaDto.Estado,
                    Detalle = ventaDto.Detalle,
                    MontoAdeudado = 0,
                    MontoPagado = ventaDto.Total
                };

                context.Ventas.Add(venta);
                context.SaveChanges(); // necesito VentaId

                // 3. Movimiento
                var movimientoServicio = new Movimiento.MovimientoServicio();
                movimientoServicio.CrearMovimientoVenta(
                    new VentaDTO
                    {
                        VentaId = venta.VentaId,
                        NumeroVenta = venta.NumeroVenta,
                        Total = venta.Total
                    },
                    cajaId.Value,
                    movimientoDetalle,
                    context
                );

                // 4. Caja
                cajaServicio.RegistrarTransaccion(
                    context,
                    venta.Total,
                    venta.Total >= 0
                        ? TipoMovimiento.Ingreso.ToString()
                        : TipoMovimiento.Egreso.ToString()
                );

                // 5. Detalles
                if (ventaDto.Items != null && ventaDto.Items.Any())
                {
                    if (ventaDto.Total < 0)
                    {
                        _productoServicio.RestaurarStockProductos(ventaDto.Items, context);
                    }
                    else
                    {
                        _productoServicio.DescontarStockProductos(ventaDto.Items, context);
                    }

                    var detalles = ventaDto.Items.Select(i => new DetallesVenta
                    {
                        IdVenta = venta.VentaId,
                        IdProducto = i.ItemId,
                        Cantidad = i.Cantidad,
                        Subtotal = i.PrecioVenta * i.Cantidad
                    }).ToList();

                    context.DetallesVentas.AddRange(detalles);
                }

                // 6. Pagos
                if (ventaDto.TiposDePagoSeleccionado != null &&
                    ventaDto.TiposDePagoSeleccionado.Any())
                {
                    var servicioTP = new TipoPagoServicio();

                    var pagos = ventaDto.TiposDePagoSeleccionado.Select(p => new VentaPagoDetalle
                    {
                        IdVenta = venta.VentaId,
                        IdTipoPago = p.TipoDePago.HasValue
                            ? servicioTP
                                .ObtenerTipoPagoPorNumero(
                                    Convert.ToInt32(p.TipoDePago.Value)
                                  ).TipoPagoId
                            : 0,
                        Monto = p.Monto
                    }).ToList();

                    context.VentaPagosDetalles.AddRange(pagos);
                }

                context.SaveChanges();

                // 7. Recargar venta completa
                var ventaCompleta = context.Ventas
                    .Include(v => v.DetallesVentas)
                        .ThenInclude(d => d.Producto)
                    .Include(v => v.VentaPagoDetalles)
                        .ThenInclude(p => p.TipoPago)
                    .Include(v => v.Cliente.Persona)
                    .Include(v => v.Empleado.Persona)
                    .Include(v => v.Vendedor.Persona)
                    .First(v => v.VentaId == venta.VentaId);

                // 8. PDF
                GenerarPdfDeVenta(ventaCompleta);

                return venta;
            }
            else
            {
                throw new Exception("No hay una caja abierta. No se puede registrar la venta.");
            }

        }

        public EstadoOperacion NuevaVenta(VentaDTO ventaDto)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            using var transaction = context.Database.BeginTransaction();

            try
            {
                CrearVentaInterna(context, ventaDto, TipoMovimientoDetalle.Venta);
                transaction.Commit();

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Venta registrada correctamente."
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        public VentaDTO ObtenerVentaPorId(long ventaId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var venta = context.Ventas.FirstOrDefault(v => v.VentaId == ventaId);

            if (venta == null)
                throw new Exception("No se encontró la venta.");

            return new VentaDTO
            {
                VentaId = venta.VentaId,
                NumeroVenta = venta.NumeroVenta,
                IdEmpleado = venta.IdEmpleado,
                IdVendedor = venta.IdVendedor,
                IdCliente = venta.IdCliente,
                FechaVenta = venta.FechaVenta,
                Total = venta.Total,
                TotalSinDescuento = venta.TotalSinDescuento,
                Descuento = venta.Descuento,
                Estado = venta.Estado,
                Detalle = venta.Detalle
            };
        }

        public VentaDTO ObtenerVentaDetalle(long ventaId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var venta = context.Ventas
                .Include(v => v.VentaPagoDetalles)
                .Include(v => v.DetallesVentas)
                    .ThenInclude(d => d.Producto)
                .First(v => v.VentaId == ventaId);

            return new VentaDTO
            {
                VentaId = venta.VentaId,
                NumeroVenta = venta.NumeroVenta,
                IdEmpleado = venta.IdEmpleado,
                IdVendedor = venta.IdVendedor,
                IdCliente = venta.IdCliente,
                FechaVenta = venta.FechaVenta,
                Total = venta.Total,
                TotalSinDescuento = venta.TotalSinDescuento,
                Descuento = venta.Descuento,
                Estado = venta.Estado,
                Detalle = venta.Detalle,
                TiposDePagoSeleccionado = venta.VentaPagoDetalles.Select(vp => new FormaPago
                {
                    TipoDePago = (TipoDePago?)vp.IdTipoPago,
                    Monto = vp.Monto
                }).ToList(),
                Items = venta.DetallesVentas.Select(d => new ItemVentaDTO
                {
                    ItemId = d.IdProducto,
                    Descripcion = d.Producto.Descripcion,
                    Cantidad = d.Cantidad,
                    PrecioVenta = d.Subtotal / d.Cantidad
                }).ToList()
            };
        }

        public List<VentaDTO> ObtenerTodasLasVentas()
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.Ventas.Select(v => new VentaDTO
            {
                VentaId = v.VentaId,
                NumeroVenta = v.NumeroVenta,
                IdEmpleado = v.IdEmpleado,
                IdVendedor = v.IdVendedor,
                FechaVenta = v.FechaVenta,
                Total = v.Total,
                TotalSinDescuento = v.TotalSinDescuento,
                Descuento = v.Descuento,
                Estado = v.Estado,
                Detalle = v.Detalle
            }).ToList();
        }

        public List<VentaDTO> ObtenerVentasPorMesYAño(int mes, int año)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.Ventas
                .Where(v => v.FechaVenta.Year == año);

            if (mes > 0)//Si queremos traer todo el año no filtramos por mes
            {
                query = query.Where(v => v.FechaVenta.Month == mes);
            }

            return query
                .Select(v => new VentaDTO
                {
                    VentaId = v.VentaId,
                    NumeroVenta = v.NumeroVenta,
                    IdEmpleado = v.IdEmpleado,
                    IdVendedor = v.IdVendedor,
                    FechaVenta = v.FechaVenta,
                    Total = v.Total,
                    TotalSinDescuento = v.TotalSinDescuento,
                    Descuento = v.Descuento,
                    Estado = v.Estado,
                    Detalle = v.Detalle
                })
                .ToList();
        }

        public List<long> ObtenerVentasParaCancelacion(DateTime fecha, string filtroNumero = null)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.Ventas
                .Where(v =>
                    v.Estado != 99 &&
                    v.Total > 0 &&
                    v.FechaVenta.Date == fecha.Date &&
                    v.NumeroVenta.StartsWith($"VEN-{fecha:yyyyMMdd}")
                );

            if (!string.IsNullOrEmpty(filtroNumero))
            {
                query = query.Where(v =>
                    v.NumeroVenta.Contains(filtroNumero)
                );
            }

            return query
                .OrderBy(v => v.NumeroVenta)
                .Select(v => v.VentaId)
                .ToList();
        }

        public List<VentaDTO> ObtenerVentasPorIds(List<long> ventaIds)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.Ventas
                .Where(v => ventaIds.Contains(v.VentaId))
                .OrderBy(v => v.FechaVenta)
                .ThenBy(v => v.NumeroVenta)
                .Select(v => new VentaDTO
                {
                    VentaId = v.VentaId,
                    NumeroVenta = v.NumeroVenta,
                    FechaVenta = v.FechaVenta,
                    Total = v.Total,
                    Estado = v.Estado,
                    IdEmpleado = v.IdEmpleado,
                    IdVendedor = v.IdVendedor,
                    IdCliente = v.IdCliente,
                    Detalle = v.Detalle
                })
                .ToList();
        }
        public EstadoOperacion CancelacionVentaPorId(long ventaId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var ventaOriginal = context.Ventas
                    .Include(v => v.DetallesVentas)
                    .Include(v => v.VentaPagoDetalles)
                    .FirstOrDefault(v => v.VentaId == ventaId);

                if (ventaOriginal == null)
                    return new EstadoOperacion { Exitoso = false, Mensaje = "La venta no existe." };

                if (ventaOriginal.Estado == 10)
                    return new EstadoOperacion { Exitoso = false, Mensaje = "La venta ya está cancelada." };

                ventaOriginal.Estado = 10;

                var ventaCancelacionDto = new VentaDTO
                {
                    IdEmpleado = ventaOriginal.IdEmpleado,
                    IdVendedor = ventaOriginal.IdVendedor,
                    IdCliente = ventaOriginal.IdCliente,
                    FechaVenta = DateTime.Now,

                    Total = -ventaOriginal.Total,
                    TotalSinDescuento = -ventaOriginal.TotalSinDescuento,
                    Descuento = -ventaOriginal.Descuento,

                    Estado = 99,
                    Detalle = $"Cancelación de venta N° {ventaOriginal.NumeroVenta}",

                    Items = ventaOriginal.DetallesVentas.Select(d => new ItemVentaDTO
                    {
                        ItemId = d.IdProducto,
                        Cantidad = d.Cantidad,
                        PrecioVenta = d.Subtotal / d.Cantidad
                    }).ToList(),

                    TiposDePagoSeleccionado = ventaOriginal.VentaPagoDetalles.Select(p => new FormaPago
                    {
                        TipoDePago = (TipoDePago)p.IdTipoPago,
                        Monto = -p.Monto
                    }).ToList()
                };

                CrearVentaInterna(context, ventaCancelacionDto, TipoMovimientoDetalle.Cancelacion);

                context.SaveChanges();
                transaction.Commit();

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Venta cancelada y contraventa generada correctamente."
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Error al cancelar la venta: " + ex.Message
                };
            }
        }
    }
}
