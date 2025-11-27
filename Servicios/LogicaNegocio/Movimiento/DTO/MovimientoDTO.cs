using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Movimiento.DTO
{
    public class MovimientoDTO
    {
        public long MovimientoId { get; set; }
        public string NumeroMovimiento { get; set; } = string.Empty;
        public long? IdVenta { get; set; }     // Nullable si no siempre hay venta asociada
        // long? IdGasto { get; set; }     // Nullable si no siempre hay gasto asociado
        public int TipoMovimiento { get; set; }
        //public int Movimiento { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public bool EstaEliminado { get; set; }
    }
}
