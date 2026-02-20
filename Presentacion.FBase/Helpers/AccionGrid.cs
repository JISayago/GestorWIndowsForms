using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.FBase.Helpers
{
    public class AccionGrid
    {
        public string Nombre { get; set; }
        public Image Icono { get; set; }
        public Action<long?> Ejecutar { get; set; }

        // requiere que haya fila seleccionada
        public bool RequiereSeleccion { get; set; } = true;

        // solo habilitado si NO está en eliminados
        public bool SoloSiNoEliminado { get; set; } = false;

        // visible solo cuando está en eliminados
        public bool SoloEliminados { get; set; } = false;
    }
}
