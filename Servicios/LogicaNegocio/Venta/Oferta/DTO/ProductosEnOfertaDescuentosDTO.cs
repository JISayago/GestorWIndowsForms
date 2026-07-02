using Servicios.LogicaNegocio.Producto.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta.Oferta.DTO
{
    public class ProductosEnOfertaDescuentosDTO
    {
        public long ProductoId { get; set; }
        public long OfertaId { get; set; }

        public decimal CantidadRequerida { get; set; }

        public decimal PrecioVentaBase { get; set; }

        public decimal PrecioCostoBase { get; set; }

        public decimal? PrecioOfertaBase { get; set; }

        public decimal? LimiteVentaProducto { get; set; }

        public ProductoDTO? Producto { get; set; }
    }

}
