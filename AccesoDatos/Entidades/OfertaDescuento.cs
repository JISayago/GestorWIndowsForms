using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class OfertaDescuento
    {
        [Key]
        public long OfertaDescuentoId { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioFinal{ get; set; }
        public decimal PrecioOriginal { get; set; }
        public decimal? DescuentoTotalFinal { get; set; }
        public decimal? PorcentajeDescuento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? CantidadProductosDentroOferta { get; set; }
        public bool EstaActiva { get; set; } 
        public bool EsUnSoloProducto { get; set; }
        public ICollection<ProductosEnOfertaDescuentos> Productos { get; set; } = new List<ProductosEnOfertaDescuentos>();
    }
}
