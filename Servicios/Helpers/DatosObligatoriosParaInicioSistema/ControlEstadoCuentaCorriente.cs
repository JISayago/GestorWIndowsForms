using AccesoDatos;
using Servicios.Helpers.Cliente.CtaCte;
using Servicios.LogicaNegocio.CuentaCorriente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.DatosObligatoriosParaInicioSistema
{
    public static class ControlEstadoCuentaCorriente
    {

        public static void Inicializar(GestorContextDB context)
        {
            var cuentas = context.CuentaCorriente
                .Where(x => !x.EstaEliminado)
                .ToList();

            var hoy = DateTime.Now;

            foreach (var cuenta in cuentas)
            {
                CuentaCorrienteServicio.VerificarYActualizarEstadoInstancia(cuenta);
            }

            context.SaveChanges();
        }
    }
}
