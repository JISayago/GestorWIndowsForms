using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers
{
    public class InformacionExistenciaOfertaDescuentoProducto
    {
        public long ProductoId { get; set; }
        public long OfertaId { get; set; }
        public string OfertaCodigo { get; set; } = string.Empty;
        public bool OfertaActiva { get; set; }

        public decimal cantidadProductoEnOferta { get; set; }

        public override string ToString()
        {
            var estado = OfertaActiva ? "ACTIVA" : "INACTIVA";
            return $"Producto {ProductoId} -> Oferta {OfertaCodigo} (Id:{OfertaId}) [{estado}]";
        }
    }
}
