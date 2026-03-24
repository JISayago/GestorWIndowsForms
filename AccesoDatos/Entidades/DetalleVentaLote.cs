using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class DetalleVentaLote
    {
        [Key]
        public long DetalleVentaLoteId { get; set; }

        public long IdProducto { get; set; }
        public long IdVenta { get; set; }
        public long IdLote { get; set; }
        public decimal Cantidad { get; set; }

        // Relaciones
        public Venta Venta { get; set; }
        public Producto Producto { get; set; }
        public Lote Lote { get; set; }
    }
}
