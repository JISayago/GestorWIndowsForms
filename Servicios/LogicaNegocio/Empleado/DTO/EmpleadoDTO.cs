using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Empleado.DTO
{
    public class EmpleadoDTO
    {
        //Datos PERSONA
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

        //Datos EMPLEADO
        public string Legajo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaEgreso { get; set; } // Puede ser null si sigue trabajando
        public int Estado { get; set; } 
        public string EstadoDescripcion { get; set; } 
        public string Username { get; set; }
        public string Pass { get; set; }
        public bool UsuarioEstaHabilitado { get; set; } = true;

    }
}
