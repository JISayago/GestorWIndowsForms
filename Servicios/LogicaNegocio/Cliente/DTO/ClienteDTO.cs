using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Cliente.DTO
{
    public class ClienteDTO
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

        //Datos CLIENTE
        public string NumeroCliente { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; } // Puede ser null si sigue siendo cliente
        public int Estado { get; set; }
        public string EstadoDescripcion { get; set; }

    }
}
