using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta.DTO
{
    public class DetalleVentaLoteDTO
    {

        public long Id { get; set; }
        public long IdProducto { get; set; }
        public long? IdVenta { get; set; } = null;
        public long IdLote { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
