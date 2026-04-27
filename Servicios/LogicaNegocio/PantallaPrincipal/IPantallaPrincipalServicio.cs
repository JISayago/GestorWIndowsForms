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
        List<NotificacionDTO> NotifiacionesProductosVencidos(int cantidadDiasABuscar);
        List<NotificacionDTO> NotifiacionesOfertasVencidas(int cantidadDiasABuscar);
        List<NotificacionDTO> NotifiacionesCtaCteVencidas(int cantidadDiasABuscar);
        DatosTurnoDTO ObtenerDatosTurno(long? cajaId, long usuarioId);
        DatosTurnoDTO ObtenerActualizarDatosCaja(long? cajaId, DatosTurnoDTO datosTurno);
        void GuardarNotasRapidas(string textoLimpio, string nombreUsuario);
        string? ObtenerNotasRapidas();
    }
}
