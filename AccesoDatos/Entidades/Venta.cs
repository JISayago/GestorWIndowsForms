using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Venta
    {
        [Key]
        public long VentaId { get; set; }
        public long IdEmpleado { get; set; }

        public Empleado Empleado { get; set; }
        public long IdVendedor { get; set; }
        public Empleado Vendedor { get; set; }
        public string NumeroVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
        public decimal TotalSinDescuento { get; set; }
        public decimal Descuento { get; set; }
        public int Estado { get; set; }
        public string Detalle { get; set; }
        public decimal MontoAdeudado { get; set; }
        public decimal MontoPagado { get; set; }

        public ICollection<DetallesVenta> DetallesVentas { get; set; }

        public ICollection<VentaPagoDetalle> VentaPagoDetalles { get; set; }
    }
}
