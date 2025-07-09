using Servicios.Helpers;
using Servicios.LogicaNegocio.Empleado.DTO;
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
        EstadoOperacion CrearUsuario(UsuarioDTO usuario);
        EstadoOperacion ActualziarPassPrimerIngreso(long usuarioId, string pass);
        UsuarioDTO ObtenerUsuarioPorId(long usuarioId);
        UsuarioDTO GeneracionNombreUsuario(string nombre, string apellido, long empleadoId);
    }
}
