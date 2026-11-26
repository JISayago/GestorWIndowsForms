using Presentacion.Core.Presentacion.Core.Helpers;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Movimiento;
using Servicios.LogicaNegocio.Movimiento.DTO;
using System.Collections.Generic;

namespace Presentacion.Core.Movimiento
{
    /// <summary>Pagos de gastos y sus reversas (anulaciones), con el origen del pago.</summary>
    public class FMovimientoGastoConsulta : FMovimientoConsultaBase
    {
        public FMovimientoGastoConsulta() : this(new MovimientoConsultaServicio())
        {
        }

        public FMovimientoGastoConsulta(IMovimientoConsultaServicio consultaServicio) : base(consultaServicio)
        {
        }

        protected override string TextoTitular => "Movimientos de gastos";
        protected override string TextoLblCbx1 => "Origen del pago";
        protected override string TextoLblCbx2 => "Operación";
        protected override string TextoLblCbx3 => "Medio de pago";

        protected override void ConfigurarFiltrosEspecificos()
        {
            var origenes = new List<OpcionFiltro>
            {
                OpcionTodos(),
                new OpcionFiltro { Texto = "Caja", Valor = "CAJA" },
                new OpcionFiltro { Texto = "Otro", Valor = "OTRO" }
            };

            ActivarCombo(cbx1, lblcbx1, origenes, "Texto", "Valor", "Origen del pago");

            var operaciones = new List<OpcionFiltro>
            {
                OpcionTodos(),
                OpcionOperacion(Operacion.PagoGasto),
                OpcionOperacion(Operacion.Reversa)
            };

            ActivarCombo(cbx2, lblcbx2, operaciones, "Texto", "Valor", "Operación");
            ActivarCombo(cbx3, lblcbx3, OpcionesMedioDePago(incluirCuentaCorriente: false), "Texto", "Valor", "Medio de pago");

            cbx1.SelectedIndex = 0;
            cbx2.SelectedIndex = 0;
            cbx3.SelectedIndex = 0;
        }

        protected override void AplicarFiltrosEspecificos(FiltroConsulta origen, FiltroMovimientos destino)
        {
            var o = origen.Filtro1?.ToString();
            destino.AfectaCaja = o == "CAJA" ? true : o == "OTRO" ? false : null;

            AplicarTipoUOperacion(origen.Filtro2, destino);
            destino.TipoPago = ComoInt(origen.Filtro3);
        }

        protected override ResultadoMovimientos Consultar(FiltroMovimientos filtro) =>
            _consultaServicio.ConsultarGastos(filtro);
    }
}
