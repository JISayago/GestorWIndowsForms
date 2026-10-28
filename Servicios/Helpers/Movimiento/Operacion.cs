namespace Servicios.Helpers.Movimiento
{
    /// <summary>
    /// Qué pasó en el movimiento. Reemplazará a TipoMovimientoDetalle (el "de dónde viene" lo siguen
    /// dando TipoEntidad + EntidadId). Etapa 1: solo se define; todavía nadie lo usa.
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
}
