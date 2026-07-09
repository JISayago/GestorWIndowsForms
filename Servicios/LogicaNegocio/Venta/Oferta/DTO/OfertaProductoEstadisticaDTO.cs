using Servicios.LogicaNegocio.Producto.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta.Oferta.DTO
{
    public class OfertaProductoEstadisticaDTO
    {
        public long OfertaDescuentoId { get; set; }

        public long ProductoId { get; set; }

        public decimal CantidadVendida { get; set; }

        public decimal TotalCostoAcumulado { get; set; }

        public decimal TotalVentaAcumulado { get; set; }

        public decimal TotalOfertaAcumulado { get; set; }

        public DateTime? FechaUltimaVenta { get; set; }

        public ProductoDTO? Producto { get; set; }

        public OfertaDTO? Oferta { get; set; }
    }
}
