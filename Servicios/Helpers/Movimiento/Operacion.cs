namespace Servicios.Helpers.Movimiento
{
    /// <summary>
    /// Qué pasó en el movimiento (el "de dónde viene" lo dan TipoEntidad + EntidadId).
    /// Reemplaza al antiguo TipoMovimientoDetalle. Hasta la Etapa 5 se guarda en la columna
    /// tipo_movimiento_detalle (propiedad TipoMovimientoDetalle).
    /// </summary>
    public enum Operacion
    {
        Cobro = 1,                  // venta / venta libre: dinero cobrado por un tipo de pago
        Reversa = 2,                // contramovimiento de una cancelación o anulación
        CargaSaldo = 3,             // el cliente carga saldo o paga su deuda
        CargoCuentaCorriente = 4,   // compra fiada: aumenta la deuda
        CreditoCuentaCorriente = 5, // devolución por cancelación: reduce la deuda
        PagoGasto = 6               // gasto pagado
    }

    public static class OperacionDescripcion
    {
        public static string De(int valor)
        {
            if (!Enum.IsDefined(typeof(Operacion), valor))
                return "Desconocido";

            return ((Operacion)valor) switch
            {
                Operacion.Cobro => "Cobro",
                Operacion.Reversa => "Reversa",
                Operacion.CargaSaldo => "Carga de saldo",
                Operacion.CargoCuentaCorriente => "Cargo cuenta corriente",
                Operacion.CreditoCuentaCorriente => "Crédito cuenta corriente",
                Operacion.PagoGasto => "Pago de gasto",
                _ => "Desconocido"
            };
        }
    }
}
