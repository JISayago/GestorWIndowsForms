using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.FBase.DTO
{
    public class AccionGrid
    {
        public string Nombre { get; set; }
        public Image Icono { get; set; }
        public Action<long?> Ejecutar { get; set; }
        public bool RequiereSeleccion { get; set; } = true;
        public bool SoloSiNoEliminado { get; set; } = false;
    }
}
