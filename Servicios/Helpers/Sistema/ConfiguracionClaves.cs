namespace Servicios.Helpers.Sistema
{
    /// <summary>Una clave por cada "documento" de configuración guardado en ParametroSistema.</summary>
    public enum ClaveConfiguracion
    {
        DatosNegocio,
        LogoNegocio,
        NomenclaturaVenta,
        NomenclaturaMovimiento,
        NomenclaturaGasto
    }

    public enum TipoCodigo
    {
        Venta,
        Movimiento,
        Gasto
    }

    public static class ConfiguracionClaves
    {
        public static ClaveConfiguracion Para(TipoCodigo tipo)
        {
            switch (tipo)
            {
                case TipoCodigo.Venta: return ClaveConfiguracion.NomenclaturaVenta;
                case TipoCodigo.Movimiento: return ClaveConfiguracion.NomenclaturaMovimiento;
                default: return ClaveConfiguracion.NomenclaturaGasto;
            }
        }
    }
}