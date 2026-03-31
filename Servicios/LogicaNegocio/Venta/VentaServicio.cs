using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using MigraDoc.DocumentObjectModel.Internals;
using Servicios.Helpers;
using Servicios.Infraestructura;
using Servicios.LogicaNegocio.Caja;
using Servicios.LogicaNegocio.Empleado.DTO;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Venta.DTO;
using Servicios.LogicaNegocio.Venta.TipoPago;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Debug.WriteLine("1 - Inicio CrearVentaInterna");

        try
        {
            Debug.WriteLine("2 - Obtener caja");

            var cajaServicio = new Caja.CajaServicio();
            var cajaId = cajaServicio.ObtenerIdDeEña(context);

            if (!cajaId.HasValue)
                throw new Exception("No hay una caja abierta. No se puede registrar la venta.");

            Debug.WriteLine("3 - Generar número venta");

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

            Debug.WriteLine("4 - Crear entidad venta");

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

            Debug.WriteLine("5 - Add venta");

            context.Ventas.Add(venta);

            Debug.WriteLine("6 - SaveChanges venta");

            context.SaveChanges();

            Debug.WriteLine("7 - Venta guardada ID: " + venta.VentaId);

            Debug.WriteLine("8 - Crear movimiento");

            var movimientoServicio = new Movimiento.MovimientoServicio();
            movimientoServicio.CrearMovimientoVenta(
                venta.VentaId,
                venta.Total,
                movimientoDetalle,
                context
            );

            Debug.WriteLine("9 - Movimiento creado");

            Debug.WriteLine("10 - Actualizar caja");

            cajaServicio.RegistrarTransaccion(
                context,
                venta.Total,
                venta.Total >= 0 ? TipoMovimiento.Ingreso : TipoMovimiento.Egreso,
                cajaId.Value
            );

            Debug.WriteLine("11 - Caja actualizada");

            Debug.WriteLine("12 - Procesar items");

            if (ventaDto.Items != null && ventaDto.Items.Any())
            {
                Debug.WriteLine("13 - Items detectados");

                var itemsStock = new List<ItemVentaDTO>();

                foreach (var item in ventaDto.Items)
                {
                    Debug.WriteLine("14 - Item: " + item.ItemId);

                    if (!item.EsOferta)
                    {
                        itemsStock.Add(new ItemVentaDTO
                        {
                            ItemId = item.ItemId,
                            Cantidad = item.Cantidad
                        });
                        continue;
                    }

                    Debug.WriteLine("15 - Buscar producto");

                    var producto = context.Productos
                        .FirstOrDefault(p => p.ProductoId == item.ItemId);

                    if (producto != null && producto.Estado == 2)
                    {
                        Debug.WriteLine("16 - Producto con descuento");

                        itemsStock.Add(new ItemVentaDTO
                        {
                            ItemId = producto.ProductoId,
                            Cantidad = item.Cantidad
                        });

                        continue;
                    }

                    Debug.WriteLine("17 - Buscar oferta");

                    var oferta = context.OfertasDescuentos
                        .FirstOrDefault(o => o.OfertaDescuentoId == item.ItemId);

                    if (oferta == null)
                        throw new Exception($"Oferta no encontrada. Id: {item.ItemId}");

                    Debug.WriteLine("18 - Oferta encontrada");

                    var productosOferta = context.ProductosEnOfertasDescuentos
                        .Where(x => x.OfertaId == oferta.OfertaDescuentoId)
                        .ToList();

                    if (!productosOferta.Any())
                        throw new Exception($"La oferta {oferta.Descripcion} no tiene productos asociados.");

                    Debug.WriteLine("19 - Productos de oferta cargados");

                    foreach (var po in productosOferta)
                    {
                        itemsStock.Add(new ItemVentaDTO
                        {
                            ItemId = po.ProductoId,
                            Cantidad = po.Cantidad * item.Cantidad
                        });
                    }
                }

                Debug.WriteLine("20 - Actualizar stock");

                if (ventaDto.Total < 0)
                    _productoServicio.RestaurarStockProductos(itemsStock, context);
                else
                    _productoServicio.DescontarStockProductos(itemsStock, context);

                Debug.WriteLine("21 - Stock actualizado");

                var detalles = new List<DetallesVenta>();

                foreach (var i in ventaDto.Items)
                {
                    Debug.WriteLine("22 - Crear detalle item");

                    long? idProducto = null;
                    long? idOferta = null;

                    if (!i.EsOferta)
                    {
                        idProducto = i.ItemId;
                    }
                    else
                    {
                        var producto = context.Productos
                            .FirstOrDefault(p => p.ProductoId == i.ItemId);

                        if (producto != null && producto.Estado == 2)
                            idProducto = producto.ProductoId;
                        else
                            idOferta = i.ItemId;
                    }

                    detalles.Add(new DetallesVenta
                    {
                        IdVenta = venta.VentaId,
                        IdProducto = idProducto,
                        IdOfertaDescuento = idOferta,
                        Cantidad = i.Cantidad,
                        Subtotal = i.PrecioVenta * i.Cantidad
                    });
                }

                Debug.WriteLine("23 - Agregar detalles");

                context.DetallesVentas.AddRange(detalles);
            }

            Debug.WriteLine("24 - Procesar pagos");

            if (ventaDto.TiposDePagoSeleccionado != null &&
                ventaDto.TiposDePagoSeleccionado.Any())
            {
                var servicioTP = new TipoPagoServicio();

                var pagos = ventaDto.TiposDePagoSeleccionado.Select(p => new VentaPagoDetalle
                {
                    IdVenta = venta.VentaId,
                    IdTipoPago = servicioTP
                        .ObtenerTipoPagoPorNumero(context, Convert.ToInt32(p.TipoDePago.Value))
                        .TipoPagoId,
                    Monto = p.Monto
                }).ToList();

                context.VentaPagosDetalles.AddRange(pagos);
            }

                var cambios = context.ChangeTracker.Entries()
               .Select(e => new
               {
                   Entidad = e.Entity.GetType().Name,
                   Estado = e.State
               })
               .ToList();

                foreach (var c in cambios)
                {
                    Debug.WriteLine($"{c.Entidad} - {c.Estado}");
                }
                Debug.WriteLine(context.Model.ToDebugString());
                context.SaveChanges();

            Debug.WriteLine("26 - SaveChanges final OK");

            return venta;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("=================================");
            Debug.WriteLine("ERROR EN PASO");
            Debug.WriteLine(ex.ToString());
            Debug.WriteLine("=================================");

            throw;
        }
    }

        public EstadoOperacion NuevaVenta(VentaDTO ventaDto)
        {
            Debug.WriteLine("A - Inicio NuevaVenta");

            using var context = new GestorContextDBFactory().CreateDbContext(null);
            Debug.WriteLine("B - Context creado");

            using var transaction = context.Database.BeginTransaction();
            Debug.WriteLine("C - Transacción iniciada");

            try
            {
                Debug.WriteLine("D - Antes CrearVentaInterna");

                var venta = CrearVentaInterna(context, ventaDto, TipoMovimientoDetalle.Venta);

                Debug.WriteLine("E - Antes Commit");

                transaction.Commit();

                Debug.WriteLine("F - Commit realizado");
                GeneracionComprobanteVenta(context, venta);


                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Venta registrada correctamente."
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine("=================================");
                Debug.WriteLine("ERROR EN NUEVA VENTA");
                Debug.WriteLine(ex.ToString());
                Debug.WriteLine("=================================");

                transaction.Rollback();

                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = ex.ToString()
                };
            }
        }

        private void GeneracionComprobanteVenta(GestorContextDB context, AccesoDatos.Entidades.Venta venta)
        {
            var ventaCompleta = context.Ventas
              .Include(v => v.DetallesVentas)
                  .ThenInclude(d => d.Producto)
              .Include(v => v.VentaPagoDetalles)
                  .ThenInclude(p => p.TipoPago)
              .Include(v => v.Cliente.Persona)
              .Include(v => v.Empleado.Persona)
              .Include(v => v.Vendedor.Persona)
              .First(v => v.VentaId == venta.VentaId);

            GenerarPdfDeVenta(ventaCompleta);
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
                .Include(v => v.DetallesVentas)
                    .ThenInclude(d => d.OfertaDescuento)
                .First(v => v.VentaId == ventaId);
            var ventaDto = new VentaDTO
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

                Items = venta.DetallesVentas.Select(d =>
                {
                    var esOferta = d.IdProducto == null;

                    return new ItemVentaDTO
                    {
                        ItemId = esOferta
                            ? d.IdOfertaDescuento.Value
                            : d.IdProducto.Value,

                        EsOferta = esOferta,

                        Descripcion = esOferta
                            ? d.OfertaDescuento.Descripcion
                            : d.Producto.Descripcion,

                        Cantidad = d.Cantidad,

                        PrecioVenta = d.Cantidad != 0
                            ? d.Subtotal / d.Cantidad
                            : 0
                    };
                }).ToList()
            };

            if (ventaDto != null)
            {
                return ventaDto; 

            }
            else {              
                throw new Exception("No se encontró la venta.");
            }
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
                    v.Estado != 99 && v.Estado != 10 &&
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
                        ItemId = (long)d.IdProducto,
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
