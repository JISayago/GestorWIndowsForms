using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers;
using Servicios.Infraestructura;
using Servicios.LogicaNegocio.Empleado.DTO;
using Servicios.LogicaNegocio.Venta.DTO;
using Servicios.LogicaNegocio.Venta.TipoPago;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta
{
    public class VentaServicio : IVentaServicio
    {
        private readonly IPdfGenerator _pdf;

        public VentaServicio() : this(new PdfGenerator())
        {
        }
        public VentaServicio(IPdfGenerator pdf)
        {
            _pdf = pdf;
        }

        public string GenerarPdfDeVenta(/*long ventaId*/ AccesoDatos.Entidades.Venta venta)
        {
            // var venta = _repositorioVentas.Obtener(ventaId);
            return _pdf.GenerarComprobante(venta);
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

        public string GenerateNextNumeroVenta(GestorContextDB context)
        {
            // Asumimos que los NumeroVenta en BD están normalizados (15 dígitos).
            // Pedimos el max directamente a la DB.
            var maxStr = context.Ventas
                                .Where(v => v.NumeroVenta != null)
                                .Max(v => (string)v.NumeroVenta);

            long max = 0;
            if (!string.IsNullOrEmpty(maxStr))
            {
                // maxStr debería ser algo como "000000000000012"
                var digits = new string(maxStr.Where(char.IsDigit).ToArray());
                if (long.TryParse(digits, out var n))
                    max = n;
            }

            return (max + 1).ToString().PadLeft(15, '0');
        }


        public AccesoDatos.Entidades.Venta CrearVentaInterna(
    GestorContextDB context,
    VentaDTO ventaDto
)
        {
            // 1. Número definitivo
            string numeroFinal = GenerateNextNumeroVenta(context);

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
                MontoAdeudado = 0,
                MontoPagado = ventaDto.Total
            };

            context.Ventas.Add(venta);
            context.SaveChanges(); // necesito VentaId

            // 2. Caja abierta
            var cajaServicio = new Caja.CajaServicio();
            var cajaId = cajaServicio.ObtenerIdCajaAbierta(context);


            //cajaId = cajaId ?? throw new Exception("No hay una caja abierta. No se puede registrar la venta.");

            // 3. Movimiento (usa MISMO context)
            var movimientoServicio = new Movimiento.MovimientoServicio();
            movimientoServicio.CrearMovimientoVenta(
                new VentaDTO
                {
                    VentaId = venta.VentaId,
                    NumeroVenta = venta.NumeroVenta,
                    Total = venta.Total
                },
                cajaId,
                context
            );
           
            // 4. Actualizar saldo caja (MISMO context)
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
                var detalles = ventaDto.Items.Select(i => new AccesoDatos.Entidades.DetallesVenta
                {
                    IdVenta = venta.VentaId,
                    IdProducto = i.ItemId,
                    Cantidad = i.Cantidad,
                    Subtotal = i.PrecioVenta * i.Cantidad
                }).ToList();
                context.DetallesVentas.AddRange(detalles);
            }

            // 6. Pagos
            if (ventaDto.TiposDePagoSeleccionado != null && ventaDto.TiposDePagoSeleccionado.Any())
            {
                var servicioTP = new TipoPagoServicio();
                var pagos = ventaDto.TiposDePagoSeleccionado.Select(p => new AccesoDatos.Entidades.VentaPagoDetalle
                {
                    IdVenta = venta.VentaId,
                    IdTipoPago = p.TipoDePago.HasValue ? servicioTP.ObtenerTipoPagoPorNumero(Convert.ToInt32(p.TipoDePago.Value)).TipoPagoId
                        : 0,
                    Monto = p.Monto
                }).ToList();

                context.VentaPagosDetalles.AddRange(pagos);
            }

            // 7. Guardar TODO junto
            context.SaveChanges();

            return venta;
        }
        public EstadoOperacion NuevaVenta(VentaDTO ventaDto)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            using var transaction = context.Database.BeginTransaction();

            try
            {
                CrearVentaInterna(context, ventaDto);

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
                Detalle = venta.Detalle
            };
        }

        public VentaDTO ObtenerVentaDetalle(long ventaId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            var venta = context.Ventas
                .Include(v => v.VentaPagoDetalles)
                .Include(v => v.DetallesVentas)
                    .ThenInclude(dv => dv.Producto)
                .Where(v => v.VentaId == ventaId)
                .FirstOrDefault(v => v.VentaId == ventaId);

            //revisar que trae DetallesVentas
            //venta.DetallesVentas.ToList();

            VentaDTO ventaConDetalles = new VentaDTO
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
                TiposDePagoSeleccionado = venta.VentaPagoDetalles.Select(vp => new FormaPago
                {
                    TipoDePago = (TipoDePago?)vp.IdTipoPago,
                    Monto = vp.Monto
                }).ToList(),
                Items = venta.DetallesVentas.Select(d => new Servicios.LogicaNegocio.Venta.DTO.ItemVentaDTO
                {
                    ItemId = d.IdProducto,
                    Descripcion = d.Producto.Descripcion,
                    Cantidad = d.Cantidad,
                    PrecioVenta = d.Subtotal / d.Cantidad
                    
                }).ToList()
            };

            return ventaConDetalles;

        }

        public List<long> ObtenerComprobantesParaCancelacionPorNroComprobante(string nroComprobante)
        {
            if (string.IsNullOrWhiteSpace(nroComprobante))
                return new List<long>();

            var numeroBuscado = nroComprobante.Trim();

            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var ids = context.Ventas
                .Where(v => v.NumeroVenta != null &&
                            v.NumeroVenta.Trim() == numeroBuscado)
                .Select(v => v.VentaId)
                .ToList();

            return ids;
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
                    FechaVenta = DateTime.Now,

                    Total = -ventaOriginal.Total,
                    TotalSinDescuento = -ventaOriginal.TotalSinDescuento,
                    Descuento = -ventaOriginal.Descuento,

                    Estado = 99,
                    Detalle = $"Cancelación de venta N° {ventaOriginal.NumeroVenta}",

                    Items = ventaOriginal.DetallesVentas.Select(d => new ItemVentaDTO
                    {
                        ItemId = d.IdProducto,
                        Cantidad = -d.Cantidad,
                        PrecioVenta = d.Subtotal / d.Cantidad
                    }).ToList(),

                    TiposDePagoSeleccionado = ventaOriginal.VentaPagoDetalles.Select(p => new FormaPago
                    {
                        TipoDePago = (TipoDePago)p.IdTipoPago,
                        Monto = -p.Monto
                    }).ToList()
                };

                CrearVentaInterna(context, ventaCancelacionDto);

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

        public List<VentaDTO> ObtenerTodasLasVentas() //no me gusta mucho traer TODAS las ventas, deberia meter un parametro para limitar por fecha o cantidad //take look paginacion
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.Ventas
                .Select(venta => new VentaDTO
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
                    Detalle = venta.Detalle
                })
                .ToList();
        }
    }
}


        //ahi tengo la venta con su detalle, despues tengo que buscar usando el 
        //service de producto los datos de cada item para completar el ItemVentaDTO y mostarlo en movimineto detallado?????
/*
 *  ItemVentaDTO
    public long ItemId { get; set; }
    public decimal Cantidad { get; set; }
    public decimal PrecioVenta { get; set; }
    public decimal PrecioOferta { get; set; }
    public string Descripcion { get; set; }
    public string Medida { get; set; }
    public string UnidadMedida { get; set; }
    public bool EsOferta { get; set; }

    public class DetallesVenta
    {
        [Key]
        public long DetalleVentaId { get; set; }

        public long IdVenta { get; set; }
        public long IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Subtotal { get; set; }

        // Relaciones
        public Venta Venta { get; set; }
        public Producto Producto { get; set; }
    }
 */
