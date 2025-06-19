using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAccesoSistema.AccesoSistema
{
    public interface IAccesoSistema
    {
        string LogeoAlSistema(string username, string pass);
        bool VerificarSiExisteUsuario(string username);
        bool VerificarSiUsuarioEstaBloqueado(string nombreUsuario);
        long ObtenerId(string nombreUsuario);
    }
}
