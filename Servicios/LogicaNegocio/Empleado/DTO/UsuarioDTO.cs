using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Empleado.DTO
{
    public class UsuarioDTO
    {
        public long PersonaId { get; set; }
        public int Estado { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
    }
}
