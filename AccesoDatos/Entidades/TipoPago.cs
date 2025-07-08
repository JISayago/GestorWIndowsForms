using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class TipoPago
    {
        [Key]
        public long TipoPagoId { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        public string Codigo { get; set; }

        public bool EstaEliminado { get; set; }

        // Si lo usás en Ventas más adelante:
        // public ICollection<Venta> Ventas { get; set; }
    }
}
