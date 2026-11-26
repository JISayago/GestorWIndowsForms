using Servicios.Helpers.Sistema.FiltrosConsulta;

namespace Servicios.LogicaNegocio.Movimiento.DTO
{
    /// <summary>Filtros tipados de las consultas de movimientos. Los null significan "sin filtrar".</summary>
    public class FiltroMovimientos
    {
        public string? TextoBuscar { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }

        /// <summary>Sin fechas y con esto en true no se limita a los últimos 2 meses.</summary>
        public bool SinLimiteFecha { get; set; }

        /// <summary>TipoMovimiento: Ingreso (1) o Egreso (2).</summary>
        public int? Tipo { get; set; }

        /// <summary>Valor del enum Operacion.</summary>
        public int? Operacion { get; set; }

        /// <summary>NumeroReferencia del tipo de pago (valor del enum TipoDePago).</summary>
        public int? TipoPago { get; set; }

        /// <summary>Libro de caja: caja a consultar (con esto se calcula el saldo).</summary>
        public long? CajaId { get; set; }

        /// <summary>Cuenta corriente o gasto puntual (con la cuenta se calcula el saldo).</summary>
        public long? EntidadId { get; set; }

        /// <summary>Gastos: true = pagado desde la caja, false = pagado con otro origen.</summary>
        public bool? AfectaCaja { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 16;
    }

    /// <summary>Página de movimientos más los totales de TODO lo filtrado (no solo de la página).</summary>
    public class ResultadoMovimientos : ResultadoPaginacion<MovimientoListadoDTO>
    {
        public decimal TotalIngresos { get; set; }
        public decimal TotalEgresos { get; set; }
        public decimal Neto => TotalIngresos - TotalEgresos;
        public bool TieneSaldo { get; set; }
    }

    public class OpcionConsulta
    {
        public long Id { get; set; }
        public string Texto { get; set; } = string.Empty;
    }
}
