using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using MigraDoc.DocumentObjectModel.Internals;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.OpcionesPagos;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.Extras;
using Servicios.Helpers.VentaEnum;
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


public AccesoDatos.Entidades.Venta CrearVentaInterna(GestorContextDB context, VentaDTO ventaDto, TipoMovimientoDetalle movimientoDetalle, long? ventdaIdOriginalParaCancerlar = null)
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
            var prefijo = ventaDto.Estado == (int)EstadoVenta.CancelacionVenta ? "CAN" : "VEN";

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
           venta.Estado,
           movimientoDetalle,
           TipoEntidadMovimiento.Venta,
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
                        Debug.WriteLine($"14 - Item: {item.ItemId} | EsOferta:{item.EsOferta} | Grupo:{item.EsOfertaPorGrupo}");

                        // 🔹 1. PRODUCTO NORMAL
                        if (!item.EsOferta )
                        {
                            Debug.WriteLine("15 - Producto normal");

                            itemsStock.Add(new ItemVentaDTO
                            {
                                ItemId = item.ItemId,
                                Cantidad = item.Cantidad
                            });

                            continue;
                        }

                        // 🔹 2. PRODUCTO CON DESCUENTO (POR GRUPO)
                        if (item.EsOfertaPorGrupo)
                        {
                            Debug.WriteLine("16 - Producto con descuento por grupo");

                            var existeProducto = context.Productos
                                .Any(p => p.ProductoId == item.ItemId);

                            if (!existeProducto)
                                throw new Exception($"Producto con descuento inválido. Id: {item.ItemId}");

                            itemsStock.Add(new ItemVentaDTO
                            {
                                ItemId = item.ItemId,
                                Cantidad = item.Cantidad
                            });

                            continue;
                        }

                        // 🔹 3. OFERTA COMBO
                        Debug.WriteLine("17 - Buscar oferta combo");

                        var oferta = context.OfertasDescuentos
                            .FirstOrDefault(o => o.OfertaDescuentoId == item.ItemId);

                        if (oferta == null)
                            throw new Exception($"Oferta combo inválida. Id: {item.ItemId}");

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

                    var detallesLotesUsado = new List<DetalleVentaLoteDTO>();
                    //EVALUAR EL FORMATO DE ITEMVENTADTO PUEDE SER OFERTA Y TENER QUE DESCONTAR EN MAS DE UN PRODUCTO.
                    if (ventaDto.Total < 0) //Cancelacion de venta, restaurar stock //ESTA FUNCION SOLO SE USA CUANDO SE CANCELA???
                    _productoServicio.RestaurarStockProductos(itemsStock, context, (long)ventdaIdOriginalParaCancerlar);
                    else
                    detallesLotesUsado = _productoServicio.DescontarStockProductos(itemsStock, context);
                    //Si el descuento de stock se hizo por lotes, guardamos el detalle venta lote para luego poder restaurar el stock correctamente en caso de cancelacion de venta.


                    //CREAR DETALLE VENTA LOTE
                    if (detallesLotesUsado.Any())
                    {
                        var detallesLotes = detallesLotesUsado.Select(d => new DetalleVentaLote
                        {
                            IdVenta = venta.VentaId,
                            IdProducto = d.IdProducto,
                            IdLote = d.IdLote,
                            Cantidad = d.Cantidad
                        }).ToList();
                        
                        context.DetalleVentaLotes.AddRange(detallesLotes);
                    }

                    Debug.WriteLine("21 - Stock actualizado");

                    var detalles = new List<DetallesVenta>();

                    foreach (var i in ventaDto.Items)
                    {
                        Debug.WriteLine($"22 - Crear detalle item | Id: {i.ItemId} | EsOferta: {i.EsOferta}");

                        var precioOriginal = i.PrecioVenta;
                        var precioFinal = i.EsOferta ? i.PrecioOferta : i.PrecioVenta;

                        var detalle = new DetallesVenta
                        {
                            IdVenta = venta.VentaId,

                            IdProducto = i.EsOferta && !i.EsOfertaPorGrupo ? null : i.ItemId,
                            IdOfertaDescuento = i.EsOferta && !i.EsOfertaPorGrupo ? i.ItemId : null,

                            Cantidad = i.Cantidad,

                            PrecioUnitarioOriginal = precioOriginal,
                            PrecioUnitarioFinal = precioFinal,

                            Subtotal = precioFinal * i.Cantidad,

                            EsOferta = i.EsOferta,
                            EsOfertaPorGrupo = i.EsOfertaPorGrupo,

                            Descripcion = i.Descripcion ?? string.Empty
                        };

                        detalles.Add(detalle);
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
                    v.Estado != (int)EstadoVenta.CancelacionVenta && v.Estado != (int)EstadoVenta.Cancelada &&
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

                if (ventaOriginal.Estado == (int)EstadoVenta.Cancelada)
                    return new EstadoOperacion { Exitoso = false, Mensaje = "La venta ya está cancelada." };

                ventaOriginal.Estado = 10;

                var ventaCancelacionDto = new VentaDTO
                {
                    IdEmpleado = ventaOriginal.IdEmpleado,
                    IdVendedor = ventaOriginal.IdVendedor,
                    IdCliente = ventaOriginal.IdCliente,
                    FechaVenta = DateTime.Now,
                    // se cambia el negativo de la oferta cancelada, solo va a filtrar en caso de estado 99 (cancelado.) pero los montos son iguales a lo de la venta en positivo.
                    Total = ventaOriginal.Total,
                    TotalSinDescuento = ventaOriginal.TotalSinDescuento,
                    Descuento = ventaOriginal.Descuento,

                    Estado = (int)EstadoVenta.CancelacionVenta,
                    Detalle = $"Cancelación de venta N° {ventaOriginal.NumeroVenta}",

                    Items = ventaOriginal.DetallesVentas.Select(d =>
                    {
                        long itemId;

                        if (d.EsOferta && !d.EsOfertaPorGrupo)
                        {
                            if (!d.IdOfertaDescuento.HasValue)
                                throw new Exception($"Detalle inconsistente: falta IdOfertaDescuento en DetalleVentaId {d.DetalleVentaId}");

                            itemId = d.IdOfertaDescuento.Value;
                        }
                        else
                        {
                            if (!d.IdProducto.HasValue)
                                throw new Exception($"Detalle inconsistente: falta IdProducto en DetalleVentaId {d.DetalleVentaId}");

                            itemId = d.IdProducto.Value;
                        }

                        return new ItemVentaDTO
                        {
                            ItemId = itemId,

                            Cantidad = d.Cantidad,

                            // 🔥 precios correctos
                            PrecioVenta = d.PrecioUnitarioOriginal,
                            PrecioOferta = d.PrecioUnitarioFinal,
                            PrecioOriginalOferta = d.PrecioUnitarioOriginal,

                            // 🔥 flags correctos
                            EsOferta = d.EsOferta,
                            EsOfertaPorGrupo = d.EsOfertaPorGrupo,

                            Descripcion = d.Descripcion ?? string.Empty
                        };
                    }).ToList(),

                    
                    TiposDePagoSeleccionado = ventaOriginal.VentaPagoDetalles.Select(p => new FormaPago
                    {
                        TipoDePago = (TipoDePago)p.IdTipoPago,
                        Monto = -p.Monto
                    }).ToList()
                }; //AGREGAR DETALLEVENTALOTE EN EL CASO QUE EXISTA if(ventaOriginal.DetallesVentasLotes.any()), cargar en el dto 

                CrearVentaInterna(context, ventaCancelacionDto, TipoMovimientoDetalle.Cancelacion, ventaId);

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
