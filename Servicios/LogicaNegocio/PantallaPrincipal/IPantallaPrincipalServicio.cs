using AccesoDatos.Entidades;
using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.PantallaPrincipal
{
    public interface IPantallaPrincipalServicio
    {
        List<NotificacionDTO> ObtenerNotificacionesProdutosVencidos();
        List<NotificacionDTO> ObtenerNotificacionesCtaCteVencidas();
        List<NotificacionDTO> ObtenerNotificacionesOfertasVencidas();
        void NotifiacionesProductosVencidos();
        void NotificacionesOfertasVencidas();
        void NotificacionesCtaCteVencidas();
        DatosTurnoDTO ObtenerDatosTurno(long? cajaId, long usuarioId);
        DatosTurnoDTO ObtenerActualizarDatosCaja(long? cajaId, DatosTurnoDTO datosTurno);
        void GuardarNotasRapidas(string textoLimpio, string nombreUsuario);
        string? ObtenerNotasRapidas();
    }
}
