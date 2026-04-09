using Servicios.Helpers.Sistema;
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
        EstadoOperacion ResetearContra(long adminId, long usuarioDesbloquerID);
        ICollection<long> ObtenerEmpleadosSinPassIDs();
        EstadoOperacion DeshabilitarUsuarioYRecuperarContra(string usuario, string nro);
        bool ExisteUsuario(string nombreUsuario);
        EstadoOperacion CrearUsuario(UsuarioDTO usuario);
        EstadoOperacion ActualziarPassPrimerIngreso(long usuarioId, string pass);
        UsuarioDTO ObtenerUsuarioPorId(long usuarioId);
        UsuarioDTO GeneracionNombreUsuario(string nombre, string apellido, long empleadoId);
    }
}
