namespace Servicios.Helpers.Gasto
{
    /// <summary>De dónde salió la plata de un gasto pagado (Gasto.OrigenPago). Etapa 1: solo se define.</summary>
    public enum OrigenPagoGasto
    {
        Caja = 1, // siempre efectivo, descuenta de la caja abierta
        Otro = 2  // cualquier otro medio (excepto cuenta corriente), no afecta la caja
    }
}
