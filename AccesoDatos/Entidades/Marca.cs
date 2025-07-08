using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Marca
    {
        [Key]
        public long CategoriaId { get; set; }
        public string Nombre { get; set; }

        public bool EstaEliminado { get; set; }
        // Relación inversa (1 a muchos con Productos)
        public ICollection<Producto> Productos { get; set; }
    }
}
