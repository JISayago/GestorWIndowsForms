using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Movimiento
    {
        [Key]
        public long MovimientoId { get; set; }
        public long? IdVenta { get; set; }     // Nullable si no siempre hay venta asociada
        public int TipoMovimiento { get; set; }

        public bool EstaEliminado { get; set; }

        // Navegación
        public Venta Venta { get; set; }
    }
}
