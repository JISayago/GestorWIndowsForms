using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta.DTO
{
    internal class VentaPagoDetalleDTO
    {
        public long VentaPagoDetalleId { get; set; }
        public long IdVenta { get; set; }
        public long IdTipoPago { get; set; }
        public decimal Monto { get; set; }
    }
}
