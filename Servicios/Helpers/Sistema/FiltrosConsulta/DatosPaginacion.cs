using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.Sistema.FiltrosConsulta
{
    public class DatosPaginacion
    {
        public int PaginaActual { get; set; }
        public int PageSize { get; set; }
        public int CantidadRegistros { get; set; }

        public int TotalPaginas =>
            (int)Math.Ceiling((double)CantidadRegistros / PageSize);
    }
}
