using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Lote
    {
        public long LoteId { get; set; }
        public long IdProducto { get; set; }
        public decimal StockIncial { get; set; }
        public decimal StockActual { get; set; }
        public string NumeroLote { get; set; } //autogenerado por el sistema y el usuario puede cambiarlo si lo desea, o dejarlo como esta. El formato del numero de lote puede ser: "LOTE-0001", "LOTE-0002"
        public string Descripcion { get; set; }
        public DateTime FechaAlta { get; set; } //cambiar por datetime , asi se puede filtrar el mas viejo con la hora incluida, no mostrar desp la hora 
        public DateTime? FechaVencimiento { get; set; }
        public bool EstaVencido { get; set; }
        public bool EstaActivo { get; set; }

        public Producto Producto { get; set; }
        public ICollection<DetalleVentaLote> DetalleVentaLote { get; set; }
    }
}
