using Servicios.Helpers.Movimiento;
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

        // =========================================
        // AUTOCALCULADOS
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

        public string TipoMovimientoDetalleDescripcion
        {
            get
            {
                if (!Enum.IsDefined(typeof(TipoMovimientoDetalle), TipoMovimientoDetalle))
                    return "Desconocido";

                return ((TipoMovimientoDetalle)TipoMovimientoDetalle).ToString();
            }
        }
    }
}
