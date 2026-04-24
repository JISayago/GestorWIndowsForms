using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.PantallaPrincipal.DTO
{
    public class NotificacionDTO
    {
        public long NotificacionId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaNotificacion { get; set; }
        public bool Leida { get; set; }
        public int NivelUrgencia { get; set; }
    }
}
