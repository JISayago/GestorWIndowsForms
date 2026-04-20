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
        public List<NotificacionPP> checkearProductosVencidos(DateTime? fecha = null);
        public List<NotificacionPP> checkearOfertasVencidas(DateTime? fecha = null);
    }
}
