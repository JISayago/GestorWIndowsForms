using Servicios.LogicaNegocio.Movimiento.DTO;

namespace Servicios.LogicaNegocio.Movimiento
{
    /// <summary>Consultas de solo lectura sobre el libro de movimientos.</summary>
    public interface IMovimientoConsultaServicio
    {
        /// <summary>Movimientos que afectan la caja. Si se indica CajaId, cada fila trae el saldo de esa caja.</summary>
        ResultadoMovimientos ConsultarLibroCaja(FiltroMovimientos filtro);

        /// <summary>Cargos, créditos y cargas de saldo de cuentas corrientes. Si se indica EntidadId (cuenta), cada fila trae el saldo.</summary>
        ResultadoMovimientos ConsultarCuentaCorriente(FiltroMovimientos filtro);

        /// <summary>Pagos de gastos y sus reversas. EntidadId = un gasto puntual; AfectaCaja = origen Caja / Otro.</summary>
        ResultadoMovimientos ConsultarGastos(FiltroMovimientos filtro);

        List<OpcionConsulta> ObtenerCajasParaFiltro();
        List<OpcionConsulta> ObtenerCuentasCorrientesParaFiltro();
    }
}
