using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Producto
    {
        [Key]
        public long ProductoId { get; set; }
        public long? IdMarca { get; set; }
        public int Stock { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public string? Descripcion { get; set; }
        public bool EstaEliminado { get; set; }
        public int Estado { get; set; }
        public string Medida { get; set; }
        public string UnidadMedida { get; set; }

        // Navegación
        public Marca Marca { get; set; }
        public ICollection<CategoriaProducto> CategoriasProductos { get; set; }
        public ICollection<DetallesVenta> DetallesVentas { get; set; }
    }
}
