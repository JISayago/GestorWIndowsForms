using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class VentaPagoDetalle
    {
        [Key]
        public long VentaPagoDetalleId { get; set; }

        public long IdVenta { get; set; }
        public Venta Venta { get; set; }

        public long IdTipoPago { get; set; }
        public TipoPago TipoPago { get; set; }

        public decimal Monto { get; set; }

    }
}
