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
        List<NotificacionDTO> ObtenerNotificacionesOfertasBajoStock();
        void NotifiacionesProductosVencidos();
        void NotificacionesOfertasVencidas();
        void NotificacionesOfertasBajoStock();
        void NotificacionesCtaCteVencidas();
        void MarcarNotificacionComoLeida(long notificacionId);
        List<VencimientoCalendarioDTO> ObtenerVencimientosCalendario(DateTime desde, DateTime hasta);
        string ObtenerDniClientePorNombreCuentaCorriente(string nombreCuentaCorriente);
        string ObtenerCodigoOfertaDeAvisoBajoStock(string tituloAviso);
        DatosTurnoDTO ObtenerDatosTurno(long? cajaId, long usuarioId);
        DatosTurnoDTO ObtenerActualizarDatosCaja(long? cajaId, DatosTurnoDTO datosTurno);
        void GuardarNotasRapidas(string textoLimpio, string nombreUsuario);
        string? ObtenerNotasRapidas();
    }
}
