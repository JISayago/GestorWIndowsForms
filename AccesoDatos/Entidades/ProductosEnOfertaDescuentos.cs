using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class ProductosEnOfertaDescuentos
    {
        [Key]
        public long ProductosEnOfertaDescuentosId { get; set; }

        public long OfertaId { get; set; }
        public OfertaDescuento Oferta { get; set; }

        public long ProductoId { get; set; }
        public Producto Producto { get; set; }

        public decimal Cantidad { get; set; }
        public decimal? PrecioUnitarioOferta { get; set; } // opcional

        public decimal? DescuentoPorcentaje { get; set; } // opcional
    }
}
