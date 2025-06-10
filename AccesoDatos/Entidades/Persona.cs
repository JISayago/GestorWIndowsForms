using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Persona
    {
        [Key]
        public long PersonaId { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Dni { get; set; }

        public string Cuil { get; set; }

        public string Telefono { get; set; }

        public string? Telefono2 { get; set; }

        public string? Email { get; set; }

        public string? Direccion { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public bool EstaEliminado { get; set; }

        public Empleado? Empleado { get; set; } // Relación 1:1 (opcional)
    }
}
