using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Sistema
{
    public class InicializadorDatosObligatoriosServicio
    {
        public GestorContextDB ContextParaInicializar()
        {
            return new GestorContextDBFactory().CreateDbContext(null);
        }
    }
}
