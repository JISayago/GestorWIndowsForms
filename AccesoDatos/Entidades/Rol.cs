using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Rol
    {
        [Key]
        public long RolId { get; set; }
        public string Nombre { get; set; }
        public string DetalleRol { get; set; }
        public string CodigoRol { get; set; }

        public bool EstaEliminado { get; set; }
        public ICollection<EmpleadoRol> EmpleadosRoles { get; set; }

    }
}
