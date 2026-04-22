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
        List<NotificacionPP> notifiacionesProductosVencidos(int cantidadDiasABuscar);
        List<NotificacionPP> notifiacionesOfertasVencidas(int cantidadDiasABuscar);
        List<NotificacionPP> notifiacionesCtaCteVencidas(int cantidadDiasABuscar);
    }
}
