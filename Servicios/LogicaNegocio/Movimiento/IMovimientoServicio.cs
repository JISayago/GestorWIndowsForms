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

        /// <summary>Gasto pagado: Egreso. Con origen Caja afecta la caja (siempre en efectivo); con otro origen no.</summary>
        AccesoDatos.Entidades.Movimiento? RegistrarPagoGasto(GestorContextDB context, long gastoId, bool afectaCaja, long? cajaId, TipoDePago tipoPago, decimal monto);

        /// <summary>
        /// Contramovimiento de uno existente: tipo contrario, mismo monto, mismo tipo de pago y mismo AfectaCaja,
        /// con MovimientoOrigenId apuntando al original. <c>cajaId</c> es la caja abierta actual (solo si el original afectaba la caja).
        /// </summary>
        AccesoDatos.Entidades.Movimiento? RegistrarReversa(GestorContextDB context, AccesoDatos.Entidades.Movimiento original, TipoEntidadMovimiento entidad, long entidadId, long? cajaId, string prefijo);

        /// <summary>
        /// Cancelación: revierte uno a uno los cobros (que afectaron la caja) de la entidad original, apuntando cada reversa a su original.
        /// Devuelve false si la entidad original no tiene cobros en el libro nuevo (ventas anteriores al refactor): el llamador usa entonces <see cref="RegistrarCobros"/>.
        /// Lanza una excepción si hay dinero que devolver y no hay caja abierta, o si el efectivo a devolver supera el efectivo disponible de la caja.
        /// </summary>
        bool RegistrarReversasDeCobros(GestorContextDB context, TipoEntidadMovimiento entidad, long entidadOriginalId, long entidadNuevaId, long? cajaId, string prefijo);

        /// <summary>Efectivo disponible en una caja: saldo inicial + cobros en efectivo - pagos en efectivo (según el libro).</summary>
        decimal ObtenerEfectivoDisponible(GestorContextDB context, long cajaId);

        ResultadoPaginacion<MovimientoDTO> ObtenerMovimientos(FiltroConsulta filtros);
        MovimientoHelperDTO ObtenerDatosParaMovimientoConsulta(long movimientoId);
    }
}
