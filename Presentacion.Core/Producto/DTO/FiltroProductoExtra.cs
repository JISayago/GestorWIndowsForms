using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Core.Producto.DTO
{
    public class FiltroProductoExtra
    {
        public long? MarcaId { get; set; }
        public long? RubroId { get; set; }
        public decimal? PrecioDesde { get; set; }
        public decimal? PrecioHasta { get; set; }
        public bool SoloConStock { get; set; }
    }
}
