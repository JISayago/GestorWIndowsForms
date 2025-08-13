using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Producto.DTO
{
    public class ProductoDTO
    {
        public long ProductoId { get; set; }
        public long? IdMarca { get; set; }

        public int Stock { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public string Descripcion { get; set; }
        public bool EstaEliminado { get; set; }
        public int Estado { get; set; }
        public string Medida { get; set; }
        public string UnidadMedida { get; set; }

        // Relación simplificada
        public string MarcaNombre { get; set; }

        // Relación a múltiples categorías
        public List<long> CategoriaIds { get; set; }
    }
}
