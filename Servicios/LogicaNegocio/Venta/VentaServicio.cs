using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using MigraDoc.DocumentObjectModel.Internals;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.OpcionesPagos;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.Extras;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.Helpers.Venta.Oferta;
using Servicios.Helpers.VentaEnum;
using Servicios.Infraestructura;
using Servicios.LogicaNegocio.Caja;
using Servicios.LogicaNegocio.CuentaCorriente;
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

        public string GenerarPdf(AccesoDatos.Entidades.Venta venta)
        {
            return venta.Estado switch
            {
                (int)EstadoVenta.CancelacionVenta => _pdf.GenerarCancelacionVenta(venta),
                _ => _pdf.GenerarVenta(venta)
            };
        }
        public AccesoDatos.Entidades.Venta CrearVentaInterna( GestorContextDB context, VentaDTO ventaDto, TipoMovimientoDetalle movimientoDetalle,long? ventaIdOriginalParaCancelar = null)
        {
            try
            {
                var montos = CalcularMontosVenta(ventaDto);

                var cajaId = ObtenerCajaAbierta(context);

                GenerarNumeroVenta(context, ventaDto);

                var venta = CrearEntidadVenta(ventaDto, montos.MontoCaja, montos.MontoCtaCte);

                GuardarCabeceraVenta(venta, context);

                RegistrarMovimientoVenta( venta, movimientoDetalle,context);

                RegistrarMovimientoCaja(venta, cajaId, context);

                RegistrarMovimientoCuentaCorriente(venta,ventaDto,cajaId,context);

                var resultado = ProcesarItemsVenta( venta, ventaDto, context, ventaIdOriginalParaCancelar);
                if (!resultado.Exitoso)
                {
                    throw new Exception(resultado.Mensaje);
                }

                RegistrarPagos(venta, ventaDto,context);

                context.SaveChanges();

                return venta;
            }
            catch
            {
                throw;
            }
        }
        private (decimal MontoCaja, decimal MontoCtaCte) CalcularMontosVenta(VentaDTO ventaDto)
        {
            decimal montoCaja = 0;
            decimal montoCtaCte = 0;

            var pagoCtaCte = ventaDto.TiposDePagoSeleccionado?
                .FirstOrDefault(x =>
                    Convert.ToInt32(x.TipoDePago.Value) == (int)TipoDePago.CtaCte);

            if (pagoCtaCte != null)
            {
                montoCtaCte = Math.Abs(pagoCtaCte.Monto);
                montoCaja = Math.Abs(ventaDto.Total) - montoCtaCte;
            }
            else
            {
                montoCaja = Math.Abs(ventaDto.Total);
            }

            return (montoCaja, montoCtaCte);
        }
        private long ObtenerCajaAbierta(GestorContextDB context)
        {
            var cajaServicio = new Caja.CajaServicio();

            var cajaId = cajaServicio.ObtenerIdDeEña(context);

            if (!cajaId.HasValue)
                throw new Exception(
                    "No hay una caja abierta. No se puede registrar la venta.");

            return cajaId.Value;
        }
        private void GenerarNumeroVenta(GestorContextDB context, VentaDTO ventaDto)
        {
            var fecha = DateTime.Today;

            var prefijo =
                ventaDto.Estado == (int)EstadoVenta.CancelacionVenta
                    ? "CAN"
                    : "VEN";

            var cantidadHoy = context.Ventas.Count(v =>
                v.NumeroVenta.StartsWith($"{prefijo}-{fecha:yyyyMMdd}"));

            ventaDto.NumeroVenta = GeneradorNumeroComprobante.Generar(prefijo,fecha,cantidadHoy);
        }
        private AccesoDatos.Entidades.Venta CrearEntidadVenta(VentaDTO ventaDto, decimal montoCaja, decimal montoCtaCte)
        {
            return new AccesoDatos.Entidades.Venta
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
                MontoPagado = montoCaja,
                MontoAdeudado = montoCtaCte
            };
        }
        private void GuardarCabeceraVenta(AccesoDatos.Entidades.Venta venta,GestorContextDB context)
        {
            context.Ventas.Add(venta);
            context.SaveChanges();
        }
        private void RegistrarMovimientoVenta(AccesoDatos.Entidades.Venta venta,TipoMovimientoDetalle movimientoDetalle,GestorContextDB context)
        {
            var movimientoServicio = new Movimiento.MovimientoServicio();

            movimientoServicio.CrearMovimientoVenta(venta.VentaId,venta.Total,venta.Estado,movimientoDetalle,TipoEntidadMovimiento.Venta,context);
        }
        private void RegistrarMovimientoCaja(AccesoDatos.Entidades.Venta venta,long cajaId, GestorContextDB context)
        {
            if (venta.MontoPagado == 0)
                return;

            var cajaServicio = new Caja.CajaServicio();

            cajaServicio.RegistrarTransaccion(context,venta.MontoPagado,venta.MontoPagado >= 0 ? TipoMovimiento.Ingreso : TipoMovimiento.Egreso, cajaId);
        }
        private void RegistrarMovimientoCuentaCorriente(AccesoDatos.Entidades.Venta venta,VentaDTO ventaDto,long cajaId,GestorContextDB context)
        {
            if (venta.MontoAdeudado == 0)
                return;

            if (!venta.IdCliente.HasValue)
                throw new Exception(
                    "No se puede registrar un movimiento de Cuenta Corriente sin un Cliente asignado.");

            var servicio = new CuentaCorrienteServicio();

            var cuenta =
                servicio.ObtenerCuentaCorrientePorClienteId(venta.IdCliente.Value);

            if (cuenta == null)
                throw new Exception(
                    "El cliente seleccionado no posee una Cuenta Corriente activa.");

            if (ventaDto.Estado == (int)EstadoVenta.Confirmada)
            {
                var resultado = servicio.RegistrarCompra(cuenta.CuentaCorrienteId,venta.MontoAdeudado,cajaId,$"Cargo por Venta Interna N° {venta.NumeroVenta}");

                if (!resultado.Exitoso)
                    throw new Exception(resultado.Mensaje);
            }
            else
            {
                var resultado = servicio.RegistrarDevolucionOAnulacion(cuenta.CuentaCorrienteId,Math.Abs(venta.MontoAdeudado), cajaId, $"Crédito por Anulación de Venta N° {venta.NumeroVenta}");

                if (!resultado.Exitoso)
                    throw new Exception(resultado.Mensaje);
            }
        }
        private EstadoOperacion ProcesarItemsVenta(AccesoDatos.Entidades.Venta venta,VentaDTO ventaDto,GestorContextDB context,long? ventaOriginal)
        {
            if (ventaDto.Items == null || !ventaDto.Items.Any())
                return new EstadoOperacion { Exitoso = true };

            var itemsStock = ObtenerItemsParaStock(ventaDto.Items, context);

            bool esCancelacion = ventaDto.Estado == (int)EstadoVenta.CancelacionVenta;

            if (!esCancelacion)
            {
                var resultado = ValidarLimitesOfertas(ventaDto.Items,context);

                if (!resultado.Exitoso)
                    return resultado;
            }
            ActualizarStock(ventaDto, venta, itemsStock, context, ventaOriginal);
            ActualizarEstadisticasOfertas(ventaDto,context);
            if (!esCancelacion)
            {
            DesactivarOfertasSinStockDisponible(ventaDto.Items.Where(x => x.EsOferta), context);
            }
            CrearDetallesVenta(venta, ventaDto.Items, context);
            return new EstadoOperacion
            {
                Exitoso = true
            };
        }
        private void DesactivarOfertasSinStockDisponible(IEnumerable<ItemVentaDTO> items, GestorContextDB context)
        {
            foreach (var item in items)
            {
                var oferta = context.OfertasDescuentos
                    .Include(x => x.Productos)
                    .Include(x => x.Estadisticas)
                    .First(x => x.OfertaDescuentoId == item.ItemId);

                if (OfertaAlcanzoLimite(oferta))
                    oferta.EstaActiva = false;
            }
        }
        private bool OfertaAlcanzoLimite(OfertaDescuento oferta)
        {
            foreach (var producto in oferta.Productos)
            {
                if (!producto.LimiteVentaProducto.HasValue)
                    continue;

                var estadistica = oferta.Estadisticas.FirstOrDefault(x =>
                    x.ProductoId == producto.ProductoId);

                decimal vendido = estadistica?.CantidadVendida ?? 0;

                if (vendido < producto.LimiteVentaProducto.Value)
                    return false;
            }

            return oferta.Productos.Any(x => x.LimiteVentaProducto.HasValue);
        }
        private EstadoOperacion ValidarLimitesOfertas(List<ItemVentaDTO> items, GestorContextDB context)
        {
            foreach (var item in items.Where(x => x.EsOferta))
            {
                var resultado = ValidarLimiteOferta(item,context);

                if (!resultado.Exitoso)
                    return resultado;
            }

            return new EstadoOperacion
            {
                Exitoso = true
            };
        }
        private EstadoOperacion ValidarLimiteOferta(ItemVentaDTO item,GestorContextDB context)
        {
            var productosOferta = context.ProductosEnOfertasDescuentos
                .Where(x => x.OfertaDescuentoId == item.ItemId)
                .ToList();

            foreach (var producto in productosOferta)
            {
                var resultado = ValidarLimiteProductoOferta(producto,item,context);

                if (!resultado.Exitoso)
                    return resultado;
            }

            return new EstadoOperacion
            {
                Exitoso = true
            };
        }
        private EstadoOperacion ValidarLimiteProductoOferta(ProductosEnOfertaDescuentos productoOferta,ItemVentaDTO itemVenta,GestorContextDB context)
        {
            if (!productoOferta.LimiteVentaProducto.HasValue)
                return new EstadoOperacion {
                Exitoso = true 
                };

            var estadistica = context.OfertaProductoEstadisticas
                .FirstOrDefault(x =>
                    x.OfertaDescuentoId == productoOferta.OfertaDescuentoId &&
                    x.ProductoId == productoOferta.ProductoId);
            decimal vendido = estadistica?.CantidadVendida ?? 0m;

            decimal cantidadLuegoVenta = vendido + (productoOferta.CantidadRequerida * itemVenta.Cantidad);

            if (cantidadLuegoVenta > productoOferta.LimiteVentaProducto.Value)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = $"La oferta '{itemVenta.Descripcion}' alcanzó el límite de ventas."
                };
            }

            return new EstadoOperacion
            {
                Exitoso = true
            };
        }
        private void ActualizarEstadisticasOfertas(VentaDTO ventaDto,GestorContextDB context)
        {
            bool esCancelacion =
                ventaDto.Estado == (int)EstadoVenta.CancelacionVenta;

            foreach (var item in ventaDto.Items.Where(x => x.EsOferta))
            {
                ActualizarEstadisticaOferta(item,context,esCancelacion);
            }
        }
        private void ActualizarEstadisticaOferta(ItemVentaDTO item,GestorContextDB context,bool esCancelacion)
        {
            var productosOferta = context.ProductosEnOfertasDescuentos
                .Include(x => x.OfertaDescuento)
                .Where(x => x.OfertaDescuentoId == item.ItemId)
                .ToList();

            foreach (var producto in productosOferta)
            {
                ActualizarEstadisticaProductoOferta(producto,item,context,esCancelacion);
            }
        }
        private void ActualizarEstadisticaProductoOferta(ProductosEnOfertaDescuentos productoOferta,ItemVentaDTO itemVenta,GestorContextDB context,bool esCancelacion)
        {
            var estadistica = context.OfertaProductoEstadisticas
                .FirstOrDefault(x =>
                    x.OfertaDescuentoId == productoOferta.OfertaDescuentoId &&
                    x.ProductoId == productoOferta.ProductoId);

            if (estadistica == null)
            {
                estadistica = new OfertaProductoEstadistica
                {
                    OfertaDescuentoId = productoOferta.OfertaDescuentoId,
                    ProductoId = productoOferta.ProductoId
                };

                context.OfertaProductoEstadisticas.Add(estadistica);
            }

            decimal cantidadVendida =
                productoOferta.CantidadRequerida * itemVenta.Cantidad;

            if (esCancelacion)
                cantidadVendida *= -1;

            estadistica.CantidadVendida += cantidadVendida;

            if (estadistica.CantidadVendida < 0)
                estadistica.CantidadVendida = 0;

            decimal precioOferta =
                productoOferta.PrecioOfertaBase ??
                productoOferta.PrecioVentaBase;

            estadistica.TotalCostoAcumulado +=
                productoOferta.PrecioCostoBase * cantidadVendida;

            estadistica.TotalVentaAcumulado +=
                productoOferta.PrecioVentaBase * cantidadVendida;

            estadistica.TotalOfertaAcumulado +=
                precioOferta * cantidadVendida;

            if (estadistica.TotalCostoAcumulado < 0)
                estadistica.TotalCostoAcumulado = 0;

            if (estadistica.TotalVentaAcumulado < 0)
                estadistica.TotalVentaAcumulado = 0;

            if (estadistica.TotalOfertaAcumulado < 0)
                estadistica.TotalOfertaAcumulado = 0;

            estadistica.FechaUltimaVenta = DateTime.Now;

            ValidarLimiteVentaProducto(productoOferta,estadistica,esCancelacion);
        }
        private void ValidarLimiteVentaProducto(ProductosEnOfertaDescuentos productoOferta,OfertaProductoEstadistica estadistica,bool esCancelacion)
        {
            if (!productoOferta.LimiteVentaProducto.HasValue)
                return;

            if (estadistica.CantidadVendida >=
                productoOferta.LimiteVentaProducto.Value)
            {
                productoOferta.OfertaDescuento.EstaActiva = false;
            }
        }
        private List<ItemVentaDTO> ObtenerItemsParaStock(List<ItemVentaDTO> items, GestorContextDB context)
        {
            var itemsStock = new List<ItemVentaDTO>();

            foreach (var item in items)
            {
                // Producto normal
                if (!item.EsOferta)
                {
                    itemsStock.Add(new ItemVentaDTO
                    {
                        ItemId = item.ItemId,
                        Cantidad = item.Cantidad,
                        EsOferta = item.EsOferta,
                    });

                    continue;
                }

                // Producto con descuento por grupo
                if (item.TipoOferta == (int)TipoOferta.Grupo)
                {
                    var existeProducto = context.Productos.Any(p =>
                        p.ProductoId == item.ItemId);

                    if (!existeProducto)
                        throw new Exception(
                            $"Producto con descuento inválido. Id: {item.ItemId}");

                    itemsStock.Add(new ItemVentaDTO
                    {
                        ItemId = item.ItemId,
                        Cantidad = item.Cantidad,
                        EsOferta = item.EsOferta,
                    });

                    continue;
                }

                // Oferta (Combo / 2x1)
                var oferta = context.OfertasDescuentos
                    .FirstOrDefault(o => o.OfertaDescuentoId == item.ItemId);

                if (oferta == null)
                    throw new Exception(
                        $"Oferta combo inválida. Id: {item.ItemId}");

                var productosOferta = context.ProductosEnOfertasDescuentos
                    .Where(x => x.OfertaDescuentoId == oferta.OfertaDescuentoId)
                    .ToList();

                if (!productosOferta.Any())
                    throw new Exception(
                        $"La oferta {oferta.Descripcion} no tiene productos asociados.");

                foreach (var po in productosOferta)
                {
                    itemsStock.Add(new ItemVentaDTO
                    {
                        ItemId = po.ProductoId,
                        Cantidad = po.CantidadRequerida * item.Cantidad,
                        EsOferta = item.EsOferta,
                    });
                }
            }

            return itemsStock;
        }
        private void ActualizarStock( VentaDTO ventaDto, AccesoDatos.Entidades.Venta venta, List<ItemVentaDTO> itemsStock, GestorContextDB context, long? ventaOriginal)
        {
            var detallesLotesUsado = new List<DetalleVentaLoteDTO>();

            bool esCancelacion =
                ventaDto.Estado == (int)EstadoVenta.CancelacionVenta;

            if (esCancelacion)
            {
                _productoServicio.RestaurarStockProductos(itemsStock,context,ventaOriginal ?? throw new Exception("No se recibió el Id de la venta original para restaurar stock."));
            }
            else
            {
                detallesLotesUsado = _productoServicio.DescontarStockProductos(itemsStock, context);
            }

            if (!detallesLotesUsado.Any())
                return;

            var detallesLotes = detallesLotesUsado
                .Select(d => new DetalleVentaLote
                {
                    IdVenta = venta.VentaId,
                    IdProducto = d.IdProducto,
                    IdLote = d.IdLote,
                    Cantidad = d.Cantidad
                })
                .ToList();

            context.DetalleVentaLotes.AddRange(detallesLotes);
        }
        private void CrearDetallesVenta(AccesoDatos.Entidades.Venta venta,List<ItemVentaDTO> items,GestorContextDB context)
        {
            var detalles = new List<DetallesVenta>();

            foreach (var item in items)
            {
                bool esOfertaCompuesta = EsOfertaCompuesta(item);

                decimal precioFinal = item.EsOferta
                    ? item.PrecioOferta
                    : item.PrecioVenta;

                detalles.Add(new DetallesVenta
                {
                    IdVenta = venta.VentaId,

                    IdProducto = esOfertaCompuesta
                        ? null
                        : item.ItemId,

                    IdOfertaDescuento = esOfertaCompuesta
                        ? item.ItemId
                        : null,

                    Cantidad = item.Cantidad,

                    PrecioUnitarioOriginal = item.PrecioVenta,
                    PrecioUnitarioFinal = precioFinal,

                    Subtotal = precioFinal * item.Cantidad,

                    EsOferta = item.EsOferta,

                    EsOfertaPorGrupo =
                        item.TipoOferta == (int)TipoOferta.Grupo,

                    Descripcion = item.Descripcion ?? string.Empty
                });
            }

            context.DetallesVentas.AddRange(detalles);
        }
        private static bool EsOfertaCompuesta(ItemVentaDTO item)
        {
            return item.EsOferta &&
                   (item.TipoOferta == (int)TipoOferta.Combo ||
                    item.TipoOferta == (int)TipoOferta.DosPorUno);
        }
        private void RegistrarPagos(AccesoDatos.Entidades.Venta venta, VentaDTO ventaDto, GestorContextDB context)
        {
            if (ventaDto.TiposDePagoSeleccionado == null ||
                !ventaDto.TiposDePagoSeleccionado.Any())
                return;

            var servicioTP = new TipoPagoServicio();

            var pagos =
                ventaDto.TiposDePagoSeleccionado
                .Select(p => new VentaPagoDetalle
                {
                    IdVenta = venta.VentaId,
                    IdTipoPago =
                        servicioTP.ObtenerTipoPagoPorNumero(
                            context,
                            Convert.ToInt32(p.TipoDePago.Value))
                        .TipoPagoId,

                    Monto = p.Monto,

                    ExtraDescripcionPago =
                        p.DatosExtra ?? "Sin especificar"
                });

            context.VentaPagosDetalles.AddRange(pagos);
        }
        //public AccesoDatos.Entidades.Venta CrearVentaInterna(GestorContextDB context, VentaDTO ventaDto, TipoMovimientoDetalle movimientoDetalle, long? ventdaIdOriginalParaCancerlar = null)
        //{
        //    //Debug.WriteLine("1 - Inicio CrearVentaInterna");

        //    try
        //    {
        //        //Debug.WriteLine("1.5 - Montos para ctacte o caja");

        //        decimal montoParaCtaCte = 0;
        //        decimal montoParaCaja = 0;

        //        // Buscamos si en los tipos de pago seleccionados se usó Cuenta Corriente (Valor 3)
        //        var pagoCtaCte = ventaDto.TiposDePagoSeleccionado?.FirstOrDefault(p => Convert.ToInt32(p.TipoDePago.Value) == (int)TipoDePago.CtaCte);

        //        if (pagoCtaCte != null)
        //        {
        //            // Usamos Math.Abs para asegurarnos de trabajar con el valor absoluto primero
        //            montoParaCtaCte = Math.Abs(pagoCtaCte.Monto);
        //            montoParaCaja = Math.Abs(ventaDto.Total) - montoParaCtaCte;
        //        }
        //        else
        //        {
        //            montoParaCaja = Math.Abs(ventaDto.Total);
        //        }

        //        //Debug.WriteLine("2 - Obtener caja");

        //        var cajaServicio = new Caja.CajaServicio();
        //        var cajaId = cajaServicio.ObtenerIdDeEña(context); // Mantenemos tu método original

        //        if (!cajaId.HasValue)
        //            throw new Exception("No hay una caja abierta. No se puede registrar la venta.");

        //        //Debug.WriteLine("3 - Generar número venta");

        //        var fecha = DateTime.Today;
        //        var prefijo = ventaDto.Estado == (int)EstadoVenta.CancelacionVenta ? "CAN" : "VEN";

        //        var cantidadHoy = context.Ventas.Count(v =>
        //            v.NumeroVenta.StartsWith($"{prefijo}-{fecha:yyyyMMdd}")
        //        );

        //        ventaDto.NumeroVenta = GeneradorNumeroComprobante.Generar(
        //            prefijo,
        //            fecha,
        //            cantidadHoy
        //        );

        //        //Debug.WriteLine("4 - Crear entidad venta");

        //        // 🌟 CAMBIO: Mapeamos los montos reales del DTO en lugar de hardcodearlos en 0 y Total
        //        var venta = new AccesoDatos.Entidades.Venta
        //        {
        //            NumeroVenta = ventaDto.NumeroVenta,
        //            IdEmpleado = ventaDto.IdEmpleado,
        //            IdVendedor = ventaDto.IdVendedor,
        //            IdCliente = ventaDto.IdCliente,
        //            FechaVenta = ventaDto.FechaVenta,
        //            Total = ventaDto.Total,
        //            TotalSinDescuento = ventaDto.TotalSinDescuento,
        //            Descuento = ventaDto.Descuento,
        //            Estado = ventaDto.Estado,
        //            Detalle = ventaDto.Detalle,
        //            MontoAdeudado = montoParaCtaCte, // Asigna lo que va a la cuenta corriente
        //            MontoPagado = montoParaCaja    // Asigna lo que se pagó en efectivo/tarjeta
        //        };

        //        //Debug.WriteLine("5 - Add venta");
        //        context.Ventas.Add(venta);

        //        //Debug.WriteLine("6 - SaveChanges venta");
        //        context.SaveChanges();

        //        //Debug.WriteLine("7 - Venta guardada ID: " + venta.VentaId);

        //        //Debug.WriteLine("8 - Crear movimiento");
        //        var movimientoServicio = new Movimiento.MovimientoServicio();
        //        movimientoServicio.CrearMovimientoVenta(
        //           venta.VentaId,
        //           venta.Total,
        //           venta.Estado,
        //           movimientoDetalle,
        //           TipoEntidadMovimiento.Venta,
        //           context
        //        );

        //        //Debug.WriteLine("9 - Movimiento creado");

        //        //Debug.WriteLine("10 - Actualizar caja");
        //        // 🌟 CAMBIO: La caja física solo debe enterarse de transacciones con dinero real (MontoPagado)
        //        if (venta.MontoPagado != 0)
        //        {
        //            cajaServicio.RegistrarTransaccion(
        //                context,
        //                venta.MontoPagado,
        //                venta.MontoPagado >= 0 ? TipoMovimiento.Ingreso : TipoMovimiento.Egreso,
        //                cajaId.Value
        //            );
        //        }
        //        //Debug.WriteLine("11 - Caja actualizada");

        //        // =========================================================================
        //        // 🌟 PASO 11b - NUEVO: IMPACTAR CUENTA CORRIENTE (VENTA O CANCELACIÓN)
        //        // =========================================================================
        //        //Debug.WriteLine("11b - Procesar Cuenta Corriente");
        //        if (venta.MontoAdeudado != 0)
        //        {
        //            if (!venta.IdCliente.HasValue)
        //                throw new Exception("No se puede registrar un movimiento de Cuenta Corriente sin un Cliente asignado.");

        //            var ctaCteServicio = new CuentaCorrienteServicio();
        //            var ctaCteDto = ctaCteServicio.ObtenerCuentaCorrientePorClienteId(venta.IdCliente.Value);

        //            if (ctaCteDto == null)
        //                throw new Exception("El cliente seleccionado no posee una Cuenta Corriente activa.");

        //            if (ventaDto.Estado == (int)EstadoVenta.Confirmada)
        //            {
        //                // FLUJO VENTA: Genera deuda (Resta saldo)
        //                var resCtaCte = ctaCteServicio.RegistrarCompra(
        //                    ctaCteDto.CuentaCorrienteId,
        //                    venta.MontoAdeudado,
        //                    cajaId.Value,
        //                    $"Cargo por Venta Interna N° {venta.NumeroVenta}"
        //                );

        //                if (!resCtaCte.Exitoso)
        //                    throw new Exception(resCtaCte.Mensaje);
        //            }
        //            else
        //            {
        //                // FLUJO CANCELACIÓN: Revierte deuda (Suma saldo usando valor absoluto)
        //                decimal montoDevolucion = Math.Abs(venta.MontoAdeudado);
        //                var resCtaCte = ctaCteServicio.RegistrarDevolucionOAnulacion(
        //                    ctaCteDto.CuentaCorrienteId,
        //                    montoDevolucion,
        //                    cajaId.Value,
        //                    $"Crédito por Anulación de Venta N° {venta.NumeroVenta}"
        //                );

        //                if (!resCtaCte.Exitoso)
        //                    throw new Exception(resCtaCte.Mensaje);
        //            }
        //        }
        //        //Debug.WriteLine("11c - Cuenta Corriente procesada");

        //        //Debug.WriteLine("12 - Procesar items");
        //        if (ventaDto.Items != null && ventaDto.Items.Any())
        //        {
        //            //Debug.WriteLine("13 - Items detectados");
        //            var itemsStock = new List<ItemVentaDTO>();

        //            foreach (var item in ventaDto.Items)
        //            {
        //                //Debug.WriteLine($"14 - Item: {item.ItemId} | EsOferta:{item.EsOferta} | Grupo:{item.EsOfertaPorGrupo}");

        //                // 🔹 1. PRODUCTO NORMAL
        //                if (!item.EsOferta)
        //                {
        //                    //Debug.WriteLine("15 - Producto normal");
        //                    itemsStock.Add(new ItemVentaDTO
        //                    {
        //                        ItemId = item.ItemId,
        //                        Cantidad = item.Cantidad
        //                    });
        //                    continue;
        //                }

        //                // 🔹 2. PRODUCTO CON DESCUENTO (POR GRUPO)
        //                if (item.EsOfertaPorGrupo)
        //                {
        //                    //Debug.WriteLine("16 - Producto con descuento por grupo");
        //                    var existeProducto = context.Productos.Any(p => p.ProductoId == item.ItemId);

        //                    if (!existeProducto)
        //                        throw new Exception($"Producto con descuento inválido. Id: {item.ItemId}");

        //                    itemsStock.Add(new ItemVentaDTO
        //                    {
        //                        ItemId = item.ItemId,
        //                        Cantidad = item.Cantidad
        //                    });
        //                    continue;
        //                }

        //                // 🔹 3. OFERTA COMBO
        //                //Debug.WriteLine("17 - Buscar oferta combo");
        //                var oferta = context.OfertasDescuentos.FirstOrDefault(o => o.OfertaDescuentoId == item.ItemId);

        //                if (oferta == null)
        //                    throw new Exception($"Oferta combo inválida. Id: {item.ItemId}");

        //                //Debug.WriteLine("18 - Oferta encontrada");
        //                var productosOferta = context.ProductosEnOfertasDescuentos
        //                    .Where(x => x.OfertaId == oferta.OfertaDescuentoId)
        //                    .ToList();

        //                if (!productosOferta.Any())
        //                    throw new Exception($"La oferta {oferta.Descripcion} no tiene productos asociados.");

        //                //Debug.WriteLine("19 - Productos de oferta cargados");
        //                foreach (var po in productosOferta)
        //                {
        //                    itemsStock.Add(new ItemVentaDTO
        //                    {
        //                        ItemId = po.ProductoId,
        //                        Cantidad = po.Cantidad * item.Cantidad
        //                    });
        //                }
        //            }

        //            //Debug.WriteLine("20 - Actualizar stock"); con control de cancealcion de venta por estado no por < 0
        //            var detallesLotesUsado = new List<DetalleVentaLoteDTO>();
        //            bool esCancelacion = ventaDto.Estado == (int)EstadoVenta.CancelacionVenta;

        //            if (esCancelacion)
        //            {
        //                _productoServicio.RestaurarStockProductos(
        //                    itemsStock,
        //                    context,
        //                    ventdaIdOriginalParaCancerlar
        //                        ?? throw new Exception("No se recibió el Id de la venta original para restaurar stock.")
        //                );
        //            }
        //            else
        //            {
        //                detallesLotesUsado = _productoServicio.DescontarStockProductos(
        //                    itemsStock,
        //                    context
        //                );
        //            }

        //            // CREAR DETALLE VENTA LOTE
        //            if (detallesLotesUsado.Any())
        //            {
        //                var detallesLotes = detallesLotesUsado.Select(d => new DetalleVentaLote
        //                {
        //                    IdVenta = venta.VentaId,
        //                    IdProducto = d.IdProducto,
        //                    IdLote = d.IdLote,
        //                    Cantidad = d.Cantidad
        //                }).ToList();

        //                context.DetalleVentaLotes.AddRange(detallesLotes);
        //            }

        //            //Debug.WriteLine("21 - Stock actualizado");

        //            var detalles = new List<DetallesVenta>();
        //            foreach (var i in ventaDto.Items)
        //            {
        //                //Debug.WriteLine($"22 - Crear detalle item | Id: {i.ItemId} | EsOferta: {i.EsOferta}");

        //                var precioOriginal = i.PrecioVenta;
        //                var precioFinal = i.EsOferta ? i.PrecioOferta : i.PrecioVenta;

        //                var detalle = new DetallesVenta
        //                {
        //                    IdVenta = venta.VentaId,
        //                    IdProducto = i.EsOferta && !i.EsOfertaPorGrupo ? null : i.ItemId,
        //                    IdOfertaDescuento = i.EsOferta && !i.EsOfertaPorGrupo ? i.ItemId : null,
        //                    Cantidad = i.Cantidad,
        //                    PrecioUnitarioOriginal = precioOriginal,
        //                    PrecioUnitarioFinal = precioFinal,
        //                    Subtotal = precioFinal * i.Cantidad,
        //                    EsOferta = i.EsOferta,
        //                    EsOfertaPorGrupo = i.EsOfertaPorGrupo,
        //                    Descripcion = i.Descripcion ?? string.Empty
        //                };

        //                detalles.Add(detalle);
        //            }

        //            //Debug.WriteLine("23 - Agregar detalles");
        //            context.DetallesVentas.AddRange(detalles);
        //        }

        //        //Debug.WriteLine("24 - Procesar pagos");
        //        if (ventaDto.TiposDePagoSeleccionado != null && ventaDto.TiposDePagoSeleccionado.Any())
        //        {
        //            var servicioTP = new TipoPagoServicio();

        //            var pagos = ventaDto.TiposDePagoSeleccionado.Select(p => new VentaPagoDetalle
        //            {
        //                IdVenta = venta.VentaId,
        //                IdTipoPago = servicioTP.ObtenerTipoPagoPorNumero(context, Convert.ToInt32(p.TipoDePago.Value)).TipoPagoId,
        //                Monto = p.Monto,
        //                ExtraDescripcionPago = p.DatosExtra ?? "Sin especificar"
        //            }).ToList();


        //            context.VentaPagosDetalles.AddRange(pagos);
        //        }

        //        var cambios = context.ChangeTracker.Entries()
        //           .Select(e => new
        //           {
        //               Entidad = e.Entity.GetType().Name,
        //               Estado = e.State
        //           }).ToList();

        //        foreach (var c in cambios)
        //        {
        //            Debug.WriteLine($"{c.Entidad} - {c.Estado}");
        //        }

        //        context.SaveChanges();
        //        //Debug.WriteLine("26 - SaveChanges final OK");

        //        return venta;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Debug.WriteLine("=================================");
        //        //Debug.WriteLine("ERROR EN PASO");
        //        //Debug.WriteLine(ex.ToString());
        //        //Debug.WriteLine("=================================");
        //        throw;
        //    }
        //}

        public EstadoOperacion NuevaVenta(VentaDTO ventaDto)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var venta = CrearVentaInterna(
                    context,
                    ventaDto,
                    TipoMovimientoDetalle.Venta);

                transaction.Commit();

                _productoServicio.ModificarEstadoStockProductos(context);

                try
                {
                    GeneracionComprobanteVenta(context, venta);
                }
                catch
                {
                    // No se cancela la venta por un error al generar el comprobante.
                }

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

            GenerarPdf(ventaCompleta); // 🔥 cambia acá
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

        public ResultadoPaginacion<VentaDTO> ObtenerVentas(FiltroConsulta filtros)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.Ventas
                .AsNoTracking()
                .Include(v => v.Cliente)
                    .ThenInclude(c => c.Persona)
                .AsQueryable();

            // =========================================================
            // 🔴 ESTADO
            // =========================================================

            if (filtros.Bool2)
            {
                // 👉 HISTORICO
                // trae todos los estados
            }
            else if (filtros.Bool1)
            {
                // 👉 SOLO canceladas
                query = query.Where(v =>
                    v.Estado == (int)EstadoVenta.Cancelada);
            }
            else
            {
                // 👉 NORMAL → no canceladas
                query = query.Where(v =>
                    v.Estado != (int)EstadoVenta.Cancelada);
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
                            v.NumeroVenta.Contains(texto));

                        break;

                    case "ClienteNombreCompleto":

                        query = query.Where(v =>
                            v.Cliente != null &&
                            (
                                (v.Cliente.Persona.Nombre + " " +
                                 v.Cliente.Persona.Apellido)
                                .Contains(texto)
                            ));

                        break;

                    default:

                        query = query.Where(v =>
                            v.NumeroVenta.Contains(texto)

                            ||

                            (
                                v.Cliente != null &&
                                (
                                    (v.Cliente.Persona.Nombre + " " +
                                     v.Cliente.Persona.Apellido)
                                    .Contains(texto)
                                )
                            ));

                        break;
                }
            }

            // =========================================================
            // 📅 FECHAS
            // =========================================================

            var filtroFecha = filtros.Filtro3?.ToString();

            bool hayFiltroFechaManual =
                filtroFecha == "FV" &&
                (filtros.FechaDesde.HasValue || filtros.FechaHasta.HasValue);

            if (hayFiltroFechaManual)
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
            else
            {
                // 🔹 NORMAL = 2 meses
                // 🔹 HISTORICO = 6 meses

                var fechaLimite = filtros.Bool2
                    ? DateTime.Now.AddMonths(-6)
                    : DateTime.Now.AddMonths(-2);

                query = query.Where(v =>
                    v.FechaVenta >= fechaLimite);
            }

            // =========================================================
            // 📌 ESTADO (cbx2)
            // =========================================================

            if (!string.IsNullOrWhiteSpace(filtros.Filtro2?.ToString()) &&
                int.TryParse(filtros.Filtro2.ToString(), out int estado))
            {
                query = query.Where(v =>
                    v.Estado == estado);
            }

            // =========================================================
            // 📊 TOTAL
            // =========================================================

            var total = query.Count();

            // =========================================================
            // 📄 PAGINACION
            // =========================================================

            var totalPaginas =
                (int)Math.Ceiling((double)total / filtros.PageSize);

            if (totalPaginas <= 0)
                totalPaginas = 1;

            if (filtros.Page > totalPaginas)
                filtros.Page = totalPaginas;

            if (filtros.Page < 1)
                filtros.Page = 1;

            // =========================================================
            // 📌 ORDEN
            // =========================================================

            query = query
                .OrderByDescending(v => v.FechaVenta);

            // =========================================================
            // 📦 DATA
            // =========================================================

            var data = query
                .Skip((filtros.Page - 1) * filtros.PageSize)
                .Take(filtros.PageSize)
                .Select(v => new VentaDTO
                {
                    VentaId = v.VentaId,

                    NumeroVenta = v.NumeroVenta,

                    FechaVenta = v.FechaVenta,

                    Total = v.Total,

                    Estado = v.Estado,

                    Detalle = v.Detalle,

                    ClienteNombreCompleto = v.Cliente != null
                        ? v.Cliente.Persona.Nombre + " " +
                          v.Cliente.Persona.Apellido
                        : string.Empty
                })
                .ToList();

            return new ResultadoPaginacion<VentaDTO>
            {
                Items = data,
                TotalRegistros = total,
                Page = filtros.Page,
                PageSize = filtros.PageSize
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

        private void GeneracionComprobanteCancelacion(GestorContextDB context, AccesoDatos.Entidades.Venta ventaCancelacion)
        {
            var ventaCompleta = context.Ventas
                .Include(v => v.DetallesVentas)
                    .ThenInclude(d => d.Producto)
                .Include(v => v.VentaPagoDetalles)
                    .ThenInclude(p => p.TipoPago)
                .Include(v => v.Cliente.Persona)
                .Include(v => v.Empleado.Persona)
                .Include(v => v.Vendedor.Persona)
                .First(v => v.VentaId == ventaCancelacion.VentaId);

            _pdf.GenerarCancelacionVenta(ventaCompleta);
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

                ventaOriginal.Estado = (int)EstadoVenta.Cancelada;

                // Traigo una sola vez los tipos de oferta necesarios
                var ofertasIds = ventaOriginal.DetallesVentas
                    .Where(d => d.IdOfertaDescuento.HasValue)
                    .Select(d => d.IdOfertaDescuento!.Value)
                    .Distinct()
                    .ToList();

                var tiposOferta = context.OfertasDescuentos
                    .Where(o => ofertasIds.Contains(o.OfertaDescuentoId))
                    .ToDictionary(
                        o => o.OfertaDescuentoId,
                        o => o.TipoOferta);

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
                        int tipoOferta = 0;

                        if (d.IdOfertaDescuento.HasValue)
                        {
                            itemId = d.IdOfertaDescuento.Value;

                            tiposOferta.TryGetValue(itemId, out tipoOferta);
                        }
                        else
                        {
                            if (!d.IdProducto.HasValue)
                                throw new Exception(
                                    $"Detalle inconsistente: falta IdProducto en DetalleVentaId {d.DetalleVentaId}");

                            itemId = d.IdProducto.Value;
                        }

                        return new ItemVentaDTO
                        {
                            ItemId = itemId,

                            Cantidad = d.Cantidad,

                            PrecioVenta = d.PrecioUnitarioOriginal,
                            PrecioOferta = d.PrecioUnitarioFinal,

                            EsOferta = d.EsOferta,
                            TipoOferta = tipoOferta,

                            Descripcion = d.Descripcion ?? string.Empty,

                            CodigoOferta = string.Empty,
                            Medida = string.Empty,
                            UnidadMedida = string.Empty,
                            Stock = 0
                        };
                    }).ToList(),


                    TiposDePagoSeleccionado = ventaOriginal.VentaPagoDetalles.Select(p => new FormaPago
                    {
                        TipoDePago = (TipoDePago)p.IdTipoPago,
                        Monto = -p.Monto
                    }).ToList()
                }; //AGREGAR DETALLEVENTALOTE EN EL CASO QUE EXISTA if(ventaOriginal.DetallesVentasLotes.any()), cargar en el dto 

                var ventaCancelacion = CrearVentaInterna(context, ventaCancelacionDto, TipoMovimientoDetalle.Cancelacion, ventaId);

                _productoServicio.ModificarEstadoStockProductos(context);
                context.SaveChanges();


                transaction.Commit();


                try
                {
                    GeneracionComprobanteCancelacion(context, ventaCancelacion);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error generando PDF cancelación: " + ex.Message);
                }
                ;

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
