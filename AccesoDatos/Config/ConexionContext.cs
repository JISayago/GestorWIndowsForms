using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Config
{
    public class ConexionContext
    {
        public GestorContextDBFactory AbrirConsulta()
        {
            var Gestor = new GestorContextDBFactory();
            return Gestor;
        }
    }
}
