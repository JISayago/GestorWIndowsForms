using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class CategoriaProducto
    {
        [Key]
        public long CategoriaProductoId { get; set; }
        public Producto Producto { get; set; }
        public long IdProducto { get; set; }
        public Categoria Categoria { get; set; }
        public long IdCategoria { get; set; }
    }
}
