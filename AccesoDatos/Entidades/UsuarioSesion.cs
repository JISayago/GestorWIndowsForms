using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class UsuarioSesion
    {
        public long UsuarioSesionId { get; set; }

        public long UsuarioId { get; set; }

        public DateTime FechaLogin { get; set; }

        public DateTime? FechaLogout { get; set; }

        public bool Activa { get; set; }
        public Empleado Usuario { get; set; }
    }
}
