using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Empleado
    {
        [Key]
        public long PersonaId { get; set; }  // Mismo ID que la persona

        public string Legajo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaEgreso { get; set; } // Puede ser null si sigue trabajando

        public int Estado { get; set; }
        public string? Username { get; set; }
        public string? Pass { get; set; }
        public bool UsuarioEstaHabilitado { get; set; } = true;

        // Relaciones
        public Persona Persona { get; set; }        // Uno a uno con Persona
        public ICollection<Venta> Ventas { get; set; }
        public ICollection<EmpleadoRol> EmpleadoRoles { get; set; }
    }
}
