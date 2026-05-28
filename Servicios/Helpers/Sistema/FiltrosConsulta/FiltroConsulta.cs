using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.Sistema.FiltrosConsulta
{
    public class FiltroConsulta
    {
        public string TextoBuscar { get; set; }

        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }

        // Combos
        public object Filtro1 { get; set; }
        public object Filtro2 { get; set; }
        public object Filtro3 { get; set; }

        // Checks
        public bool Bool1 { get; set; }
        public bool Bool2 { get; set; }

        // Paginado
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 12;

        // Ordenamiento
        public string OrdenarPor { get; set; }
        public bool Ascendente { get; set; } = true;

        public int TotalRegistros { get; set; }
    }
}
