using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Empleado.Rol.DTO
{
    public class RolDTO
    {
        public long RolId { get; set; }
        public string Nombre { get; set; }
        public long DetalleRol { get; set; }
        public long CodigoRol { get; set; }
    }
}
