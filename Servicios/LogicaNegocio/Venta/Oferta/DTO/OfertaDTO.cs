using AccesoDatos.Entidades;
using Servicios.LogicaNegocio.Articulo.Categoria.DTO;
using Servicios.LogicaNegocio.Articulo.Marca.DTO;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Producto.Rubro.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta.Oferta.DTO
{
    public class OfertaDTO
    {
        public long OfertaDescuentoId { get; set; }

        public string Descripcion { get; set; }

        public string Codigo { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public bool EstaActiva { get; set; }

        public int TipoOferta { get; set; }

        public decimal? PorcentajeDescuento { get; set; }

        public decimal? PrecioFinal { get; set; }

        public ICollection<OfertaProductoDTO> Productos { get; set; }
            = new List<OfertaProductoDTO>();
    }
}
