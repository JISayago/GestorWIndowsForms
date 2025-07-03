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
        public string DetalleRol { get; set; }
        public string CodigoRol { get; set; }
    }
}
