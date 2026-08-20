using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Seguridad
{
    public static class PruebaConexion
    {
        public static bool ProbarConexion(out string mensajeError)
        {
            try
            {
                using var context =
                    new GestorContextDBFactory().CreateDbContext(null);

                var conectado = context.Database.CanConnect();

                mensajeError = conectado
                    ? string.Empty
                    : "SQL Server no permitió la conexión.";

                return conectado;
            }
            catch (Exception ex)
            {
                mensajeError = ex.ToString();
                return false;
            }
        }
    }
}
