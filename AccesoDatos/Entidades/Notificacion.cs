using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Notificacion
    {
        public long NotificacionId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Mensaje { get; set; }
        public long EmpleadoId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaConfirmacion { get; set; }
        public bool EstaLeida { get; set; }

        public Empleado Empleado { get; set; }
    }
}
