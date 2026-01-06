using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.FBase.Helpers
{
    public static class DatosSistema
    {
        public static long UsuarioId = 0;
        public static string NombreUsuario = "";
        public static long CajaId = 0;
        public static bool EstaCajaAbierta;

        //Agregar un constructor para inicializar las variables, como cajaId y estaCajaAbierta, asi limpio un poco el program
        //Usar el cajaId al cargar el movimiento
    }
}
