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
        public string NumeroMovimiento { get; set; }
        public int TipoMovimiento { get; set; }
        public int TipoMovimientoDetalle { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public bool EstaEliminado { get; set; }
        public long? EntidadId { get; set; }
        public int? TipoEntidad { get; set; }
    }
}
