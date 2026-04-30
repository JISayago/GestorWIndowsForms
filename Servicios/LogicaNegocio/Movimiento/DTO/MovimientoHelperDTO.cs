using Servicios.LogicaNegocio.Venta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Movimiento.DTO
{
    public class MovimientoHelperDTO
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

        // Información adicional para el movimiento, dependiendo del tipo de movimiento y entidad relacionada

        public VentaDTO Venta { get; set; }

    }
}
