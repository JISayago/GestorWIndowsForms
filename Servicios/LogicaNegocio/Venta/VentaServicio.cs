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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta
{
    public class CajaServicio : IVentaServicio
    {
        private readonly IPdfGenerator _pdf;

        public CajaServicio() : this(new PdfGenerator())
        {
        }
        public CajaServicio(IPdfGenerator pdf)
        {
            _pdf = pdf;
        }

        public string GenerarPdfDeVenta(/*long ventaId*/ AccesoDatos.Entidades.Venta venta)
        {
           // var venta = _repositorioVentas.Obtener(ventaId);
            return _pdf.GenerarComprobante(venta);
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
                    MontoAdeudado =0.0m,
                    MontoPagado = ventaDto.Total // replantear el ofertadto
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
                GenerarPdfDeVenta(venta);

                return new EstadoOperacion { Exitoso = true, Mensaje = "Venta creada correctamente." };
            }
            catch (Exception ex)
            {
                try { transaction.Rollback(); } catch { /* log */ }
                return new EstadoOperacion { Exitoso = false, Mensaje = "Error al crear la venta: " + ex.Message };
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
                .Include(v => v.DetallesVentas)
                .Include(v => v.VentaPagoDetalles)
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
                    Cantidad = d.Cantidad,
                    PrecioVenta = d.Subtotal / d.Cantidad
                    //nombreProducto = //llamado a la db para traer los nombres con el idProducto
                }).ToList()
            };

            return ventaConDetalles;

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
    }
}
