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
        public decimal PrecioFinal { get; set; }
        public decimal PrecioOriginal { get; set; }
        public decimal? DescuentoTotalFinal { get; set; } // Pesos
        public decimal? PorcentajeDescuento { get; set; } // Porcentaje
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? CantidadProductosDentroOferta { get; set; }
        public bool EstaActiva { get; set; }
        public bool EsUnSoloProducto { get; set; }
        public string Detalle { get; set; }
        public string Codigo { get; set; }
        public bool esOfertaPorGrupo { get; set; }
        public bool TieneLimiteDeStock { get; set; }              
        public decimal? CantidadLimiteDeStock { get; set; }       
        public long? IdMarca { get; set; }
        public long? IdRubro { get; set; }
        public long? IdCategoria { get; set; }
        public string? GrupoNombre { get; set; }
        public MarcaDTO? Marca { get; set; }
        public RubroDTO? Rubro { get; set; }
        public CategoriaDTO? Categoria { get; set; }

        public ICollection<ProductoDTO> Productos { get; set; } = new List<ProductoDTO>();
    }
}
