using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta.Oferta.DTO
{
    public class OfertaBajoStockDTO
    {
        public long OfertaDescuentoId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        public long ProductoId { get; set; }
        public string NombreProducto { get; set; }

        public decimal StockActual { get; set; }
        public decimal CantidadRequerida { get; set; }
    }
}
