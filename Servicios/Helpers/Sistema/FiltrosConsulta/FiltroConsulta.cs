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
        public bool VerEliminados { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public object Extra { get; set; } // Columna propiedad
        public object Extra2 { get; set; } // estado enum
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string OrdenarPor { get; set; }
        public bool Ascendente { get; set; } = true;
        public int TotalRegistros { get; set; } //Limitar la cantidad de registros a mostrar
    }
}
