using Servicios.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Empleado
{
    public  interface IUsuarioServicio
    {
        bool ExisteUsuario(string nombreUsuario);
        EstadoOperacion CrearUsuario(string nombre,string apellido, long empleadoId);
    }
}
