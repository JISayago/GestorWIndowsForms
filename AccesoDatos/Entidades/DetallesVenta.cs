using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class DetallesVenta
    {
        [Key]
        public long DetalleVentaId { get; set; }

        public long IdVenta { get; set; }
        public long IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Subtotal { get; set; }

        //AGREGAR DETALLEVENTALOTEID para guardar los lotes en casos de cancelaciones o devoluciones, y para poder hacer un seguimiento de los lotes vendidos

        // Relaciones
        public Venta Venta { get; set; }
        public Producto Producto { get; set; }
    }
}
