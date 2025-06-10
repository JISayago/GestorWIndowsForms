using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class EmpleadoRol
    {
        [Key]
        public long EmpleadoRolId { get; set; }

        public long IdEmpleado { get; set; }
        public long IdRol { get; set; }

        public DateTime FechaAsignacion { get; set; }

        public Empleado Empleado { get; set; }
        public Rol Rol { get; set; }
    }
}
