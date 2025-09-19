using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta.Oferta.DTO
{
    public class ProductosEnOfertaDescuentosDTO
    {
        public long ProductoOfertaId { get; set; }
        public decimal Cantidad { get; set; }
        public string Codigo { get; set; }
        public string CodigoBarra { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioCosto { get; set; }
        public string Descripcion { get; set; }
    }
}
