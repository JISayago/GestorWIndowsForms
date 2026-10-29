using Servicios.Helpers.Movimiento;

namespace Servicios.LogicaNegocio.Movimiento.DTO
{
    /// <summary>
    /// Datos de entrada para crear un movimiento. Todo alta pasa por MovimientoServicio.Registrar,
    /// que aplica las reglas en un solo lugar (monto positivo, número único, caja, reversa).
    /// </summary>
    public class NuevoMovimiento
    {
        /// <summary>Prefijo del número (ej. "MOV-VENTA", "CAN-VENTA", "MOV-GASTO"). El servicio agrega fecha y secuencia.</summary>
        public string Prefijo { get; set; } = string.Empty;

        public TipoMovimiento Tipo { get; set; }
        public TipoMovimientoDetalle Detalle { get; set; }

        public TipoEntidadMovimiento Entidad { get; set; }
        public long EntidadId { get; set; }

        /// <summary>Se guarda siempre en positivo; el signo lo da Tipo (Ingreso/Egreso).</summary>
        public decimal Monto { get; set; }

        /// <summary>true = entra en el Libro de caja. Exige CajaId; si es false, CajaId debe ser null.</summary>
        public bool AfectaCaja { get; set; }
        public long? CajaId { get; set; }

        public long? TipoPagoId { get; set; }

        /// <summary>Solo para reversas: el movimiento original que se revierte.</summary>
        public long? MovimientoOrigenId { get; set; }
    }
}
