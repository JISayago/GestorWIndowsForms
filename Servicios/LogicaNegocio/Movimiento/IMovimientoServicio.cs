using AccesoDatos;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.OpcionesPagos;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Movimiento.DTO;
using System;
using System.Collections.Generic;

namespace Servicios.LogicaNegocio.Movimiento
{
    /// <summary>
    /// Todas las altas reciben el contexto: el servicio no abre transacciones ni guarda;
    /// quien lo llama es dueño de la transacción y de SaveChanges.
    /// </summary>
    public interface IMovimientoServicio
    {
        /// <summary>Punto único de alta con todas las reglas. Devuelve null si el monto es 0.</summary>
        AccesoDatos.Entidades.Movimiento? Registrar(GestorContextDB context, NuevoMovimiento datos);

        /// <summary>
        /// Venta / venta libre: un movimiento por tipo de pago (sin cuenta corriente), que afecta la caja.
        /// Si es cancelación son Egresos con Operación Reversa. <c>montoCaja</c> es lo cobrado fuera de cuenta corriente.
        /// </summary>
        void RegistrarCobros(GestorContextDB context, TipoEntidadMovimiento entidad, long entidadId, bool esCancelacion, long cajaId, decimal montoCaja, IEnumerable<FormaPago>? pagos);

        /// <summary>Compra fiada: Egreso con tipo de pago Cuenta corriente, no afecta la caja.</summary>
        void RegistrarCargoCuentaCorriente(GestorContextDB context, long cuentaCorrienteId, decimal monto);

        /// <summary>Devolución por cancelación: Ingreso con tipo de pago Cuenta corriente, no afecta la caja.</summary>
        void RegistrarCreditoCuentaCorriente(GestorContextDB context, long cuentaCorrienteId, decimal monto);

        /// <summary>Carga de saldo: afecta la caja, en efectivo. Una diferencia negativa es un Egreso.</summary>
        void RegistrarCargaSaldo(GestorContextDB context, long cuentaCorrienteId, long cajaId, decimal diferencia);

        /// <summary>Se reemplaza en la Etapa 3b (gastos).</summary>
        void CrearMovimientoGasto(long gastoId, decimal monto, GestorContextDB context);

        ResultadoPaginacion<MovimientoDTO> ObtenerMovimientos(FiltroConsulta filtros);
        MovimientoHelperDTO ObtenerDatosParaMovimientoConsulta(long movimientoId);
    }
}
