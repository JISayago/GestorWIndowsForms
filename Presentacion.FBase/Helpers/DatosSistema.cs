using Microsoft.VisualBasic.Logging;
using Servicios.LogicaNegocio.Caja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.FBase.Helpers
{
    public class DatosSistema
    {
        public static long UsuarioId = 0;
        public static string NombreUsuario = "";
        public static long CajaId = 0;
        public static bool EstaCajaAbierta;

        //DONE Agregar un constructor para inicializar las variables, como cajaId y estaCajaAbierta, asi limpio un poco el program
        //Usar el cajaId al cargar el movimiento, no me acuerdo porque puse esto, sera para diferencia los moviminetos segun la caja?

        public DatosSistema()
        {
            // Inicializar CajaId y EstaCajaAbierta
            var cajaServicio = new CajaServicio();
            var estadoCaja = cajaServicio.ObtenerEstadoCaja();
            var cajaId = cajaServicio.ObtenerIdCajaAbierta();

            EstaCajaAbierta = estadoCaja;
            CajaId = cajaId;

            //Fixear si no hay caja abierta , que cajaId quede en 0 o null, tener en cuenta que va a tirar error la venta al crear el movimiento usando cajaID
            //por eso deje un 0 en vez de null para poder ir avanzando

            //En el form de venta tengo que validar si hay caja abierta o no antes de crear la venta

        }

        public DatosSistema(long usuarioId, string nombre, string apellido) : this()
        {
            string nombreUsuario = nombre + " " + apellido;

            UsuarioId = usuarioId;
            NombreUsuario = nombreUsuario;            
        }
    }
}
