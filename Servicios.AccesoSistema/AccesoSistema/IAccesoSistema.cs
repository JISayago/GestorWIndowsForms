using AccesoDatos.Entidades;
using Servicios.Helpers;
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
        bool VerificarSiExisteUsuario(string username);
        bool VerificarSiUsuarioEstaBloqueado(string nombreUsuario);
        EstadoOperacion PrimerIngreso(string nombreUsuario, string pass);
    }
}
