using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class CodigoRecuperacionPass
    {
        [Key]
        public long CodigoRecuperacionId { get; set; }

        public long UsuarioAsignadoId { get; set; }

        public string Codigo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaExpiracion { get; set; }

        public bool EstaUsado { get; set; }

        public DateTime? FechaUso { get; set; }

        public Empleado UsuarioAsignado { get; set; }
    }
}
