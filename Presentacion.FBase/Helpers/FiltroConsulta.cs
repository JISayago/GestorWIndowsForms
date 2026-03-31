using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.FBase.Helpers
{
    public class FiltroConsulta
    {
        public string TextoBuscar { get; set; }
        public bool VerEliminados { get; set; }

        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }

        public object Extra { get; set; } // cualquier cosa extra
        public object Extra2 { get; set; } // cualquier cosa extra
    }
}
