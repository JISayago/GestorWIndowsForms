using AccesoDatos.Entidades;
using Servicios.Helpers.Sistema;
using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAccesoSistema.AccesoSistema
{
    public interface IAccesoSistema
    {
        // sesionPrincipalId != null => es un login de sesión secundaria. Devuelve en DatoExtra el id de la sesión creada.
        EstadoOperacion LogeoAlSistema(string username, string pass, long? sesionPrincipalId = null);
        long? ObtenerSesionActivaId(long usuarioId);
        void CerrarSesionesSecundarias(long sesionPrincipalId);

        // La sesión principal + las secundarias colgadas de ella (activas primero). Para mostrar quién está en qué sesión.
        List<SesionInfoDTO> ObtenerSesionesDelTerminal(long sesionPrincipalId);
        EstadoOperacion CerrarSesion(long usuarioId);
        bool VerificarSiExisteUsuario(string username);
        bool VerificarSiUsuarioEstaBloqueado(string nombreUsuario);
        EstadoOperacion PrimerIngreso(string nombreUsuario, string pass);
        EstadoOperacion ValidarEstadoUsuario(string username);

    }
}