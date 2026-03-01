using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Producto.DTO
{
    public class LoteDTO
    {
        public int Id { get; set; }
        public long IdProducto { get; set; }
        public int Cantidad { get; set; }
        public string NumeroLote { get; set; }
        public string NombreLote { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
    }
}
