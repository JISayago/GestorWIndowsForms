using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Producto.Rubro.DTO
{
    public class RubroDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public bool EstaEliminado { get; set; }
    }
}
