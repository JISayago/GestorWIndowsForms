using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Core.Administracion
{
    public class MesFiltro
    {
        public int Numero { get; set; }    // El valor numérico real para las consultas en Base de Datos (1 al 12)
        public string Nombre { get; set; } // El texto legible por el usuario en la interfaz ("Enero", etc.)
    }
}
