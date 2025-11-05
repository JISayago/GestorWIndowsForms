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
            public long ProductoOfertaId { get; set; }
            public decimal Cantidad { get; set; }
            public decimal PrecioEnOferta { get; set; }

            public OfertaDTO Oferta { get; set; }
        public ProductoDTO Producto { get; set; }

        public string DescripcionProducto => Producto?.Descripcion ?? string.Empty;
        public decimal CantidadProducto => Producto != null ? Producto.Stock : 0m; // la otra "cantidad"
        public string CodigoProducto => Producto?.Codigo ?? string.Empty;
        public decimal PrecioVenta => Producto?.PrecioVenta ?? 0m;
        public decimal PrecioCosto => Producto?.PrecioCosto ?? 0m;


    }

}
