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
        public long ProductoId { get; set; }
        public long OfertaDescuentoId { get; set; }

        public decimal CantidadRequerida { get; set; }
        public decimal PrecioVentaBase { get; set; }

        public decimal PrecioCostoBase { get; set; }

        public decimal? PrecioOfertaBase { get; set; }
        public decimal? LimiteVentaProducto { get; set; }

        public Producto Producto { get; set; }
        public OfertaDescuento OfertaDescuento { get; set; }

    }
}
