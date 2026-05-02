using Servicios.LogicaNegocio.CuentaCorriente.DTO;
using Servicios.LogicaNegocio.Gasto.DTO;
using Servicios.LogicaNegocio.Venta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Movimiento.DTO
{
    public class MovimientoHelperDTO //Se usa para la consulta de los movimientos
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

        public VentaDTO? Venta { get; set; }
        public GastoDTO? Gasto { get; set; }
        public CuentaCorrienteDTO? CuentaCorriente { get; set; }

    }
}
