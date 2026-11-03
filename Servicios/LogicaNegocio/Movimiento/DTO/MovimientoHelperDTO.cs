using Servicios.Helpers.Movimiento;
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
        public int Operacion { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public long? EntidadId { get; set; }
        public int? TipoEntidad { get; set; }

        // =========================================
        // AUTOCALCULADOS (mismo criterio que MovimientoDTO)
        // =========================================
        public string TipoMovimientoDescripcion
        {
            get
            {
                if (!Enum.IsDefined(typeof(TipoMovimiento), TipoMovimiento))
                    return "Desconocido";

                return ((TipoMovimiento)TipoMovimiento).ToString();
            }
        }

        public string OperacionDescripcion
        {
            get
            {
                return Servicios.Helpers.Movimiento.OperacionDescripcion.De(Operacion);
            }
        }

        // Información adicional para el movimiento, dependiendo del tipo de movimiento y entidad relacionada

        public VentaDTO? Venta { get; set; }
        public GastoDTO? Gasto { get; set; }
        public CuentaCorrienteDTO? CuentaCorriente { get; set; }

    }
}
