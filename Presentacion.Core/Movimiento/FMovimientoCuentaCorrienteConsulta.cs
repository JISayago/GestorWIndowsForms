using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase.Helpers;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Movimiento;
using Servicios.LogicaNegocio.Movimiento.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Presentacion.Core.Movimiento
{
    /// <summary>Cargos, créditos y cargas de saldo de las cuentas corrientes, con el saldo de la cuenta elegida.</summary>
    public class FMovimientoCuentaCorrienteConsulta : FMovimientoConsultaBase
    {
        public FMovimientoCuentaCorrienteConsulta() : this(null)
        {
        }

        /// <param name="cuentaCorrienteId">Cuenta a mostrar al abrir; sin valor se muestran todas.</param>
        public FMovimientoCuentaCorrienteConsulta(long? cuentaCorrienteId)
            : this(new MovimientoConsultaServicio(), cuentaCorrienteId)
        {
        }

        public FMovimientoCuentaCorrienteConsulta(IMovimientoConsultaServicio consultaServicio, long? cuentaCorrienteId)
            : base(consultaServicio)
        {
            if (cuentaCorrienteId.HasValue)
                BusquedaInicial = new BusquedaInicialConsulta { Filtro1 = cuentaCorrienteId.Value.ToString() };
        }

        protected override string TextoTitular => "Movimientos de cuentas corrientes";
        protected override string TextoLblCbx1 => "Cuenta corriente";
        protected override string TextoLblCbx2 => "Operación";

        protected override void ConfigurarFiltrosEspecificos()
        {
            var cuentas = new List<OpcionFiltro> { OpcionTodos("Todas las cuentas") };
            cuentas.AddRange(_consultaServicio.ObtenerCuentasCorrientesParaFiltro()
                .Select(c => new OpcionFiltro { Texto = c.Texto, Valor = c.Id.ToString() }));

            ActivarCombo(cbx1, lblcbx1, cuentas, "Texto", "Valor", "Cuenta corriente");

            var operaciones = new List<OpcionFiltro>
            {
                OpcionTodos(),
                OpcionOperacion(Operacion.CargoCuentaCorriente),
                OpcionOperacion(Operacion.CreditoCuentaCorriente),
                OpcionOperacion(Operacion.CargaSaldo)
            };

            ActivarCombo(cbx2, lblcbx2, operaciones, "Texto", "Valor", "Operación");

            cbx1.SelectedIndex = 0;
            cbx2.SelectedIndex = 0;
        }

        protected override void AplicarFiltrosEspecificos(FiltroConsulta origen, FiltroMovimientos destino)
        {
            destino.EntidadId = ComoLong(origen.Filtro1);
            AplicarTipoUOperacion(origen.Filtro2, destino);
        }

        protected override ResultadoMovimientos Consultar(FiltroMovimientos filtro) =>
            _consultaServicio.ConsultarCuentaCorriente(filtro);
    }
}
