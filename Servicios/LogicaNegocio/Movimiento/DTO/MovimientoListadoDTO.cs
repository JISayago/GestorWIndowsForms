
namespace Servicios.LogicaNegocio.Movimiento.DTO
{
    /// <summary>Fila de las consultas de movimientos (libro de caja, cuenta corriente, gastos).</summary>
    public class MovimientoListadoDTO
    {
        public long MovimientoId { get; set; }
        public string NumeroMovimiento { get; set; } = string.Empty;
        public DateTime FechaMovimiento { get; set; }

        public int TipoMovimiento { get; set; }
        public int Operacion { get; set; }

        /// <summary>Siempre positivo; el signo lo da TipoMovimiento.</summary>
        public decimal Monto { get; set; }

        public string TipoPago { get; set; } = string.Empty;
        public bool AfectaCaja { get; set; }
        public long? CajaId { get; set; }

        public int? TipoEntidad { get; set; }
        public long? EntidadId { get; set; }

        /// <summary>Texto legible de la entidad (N° de venta, N° de gasto, cuenta corriente...).</summary>
        public string Referencia { get; set; } = string.Empty;

        public long? MovimientoOrigenId { get; set; }

        /// <summary>Saldo después de este movimiento. Solo si la consulta se filtró por una caja o una cuenta.</summary>
        public decimal? Saldo { get; set; }

        public string TipoMovimientoDescripcion =>
            TipoMovimiento == (int)Servicios.Helpers.Movimiento.TipoMovimiento.Ingreso ? "Ingreso"
            : TipoMovimiento == (int)Servicios.Helpers.Movimiento.TipoMovimiento.Egreso ? "Egreso"
            : "Desconocido";

        public string OperacionDescripcion => Servicios.Helpers.Movimiento.OperacionDescripcion.De(Operacion);

        /// <summary>Monto con signo: Ingreso positivo, Egreso negativo.</summary>
        public decimal MontoConSigno =>
            TipoMovimiento == (int)Servicios.Helpers.Movimiento.TipoMovimiento.Egreso ? -Monto : Monto;
    }
}
