using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Seguridad
{
    public class PruebaConexion
    {
        public static bool ProbarConexion()
        {
            try
            {
                using var context = new GestorContextDBFactory().CreateDbContext(null);
                return context.Database.CanConnect();
            }
            catch
            {
                return false;
            }
        }
    }
}
