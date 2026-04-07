using AccesoDatos.Entidades;
using Servicios.Helpers.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAccesoSistema.AccesoSistema
{
    public interface IAccesoSistema
    {
        EstadoOperacion LogeoAlSistema(string username, string pass);
        EstadoOperacion CerrarSesion(long usuarioId);
        bool VerificarSiExisteUsuario(string username);
        bool VerificarSiUsuarioEstaBloqueado(string nombreUsuario);
        EstadoOperacion PrimerIngreso(string nombreUsuario, string pass);
    }
}
