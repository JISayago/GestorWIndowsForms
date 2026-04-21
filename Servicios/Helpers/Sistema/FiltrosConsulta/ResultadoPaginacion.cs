using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.Sistema.FiltrosConsulta
{
    public class ResultadoPaginacion<T>
    {
        public List<T> Items { get; set; }
        public int TotalRegistros { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int TotalPaginas =>
            (int)Math.Ceiling((double)TotalRegistros / PageSize);
    }
}
