using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.Venta.Oferta
{
    public class FiltroBusquedaComboGrupo
    {
        // para juntar marca rubro y categorias en un mismo combo
        public long? IdMarca { get; set; }
        public long? IdRubro { get; set; }
        public List<long> IdCategorias { get; set; } = new List<long>();
    }
}
