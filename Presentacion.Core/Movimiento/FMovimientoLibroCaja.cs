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
    /// <summary>Libro de caja: todo lo que entró y salió de una caja, con el saldo después de cada movimiento.</summary>
    public class FMovimientoLibroCaja : FMovimientoConsultaBase
    {
        public FMovimientoLibroCaja() : this(null)
        {
        }

        /// <param name="cajaId">Caja a mostrar al abrir; sin valor se muestra la más reciente.</param>
        public FMovimientoLibroCaja(long? cajaId) : this(new MovimientoConsultaServicio(), cajaId)
        {
        }

        public FMovimientoLibroCaja(IMovimientoConsultaServicio consultaServicio, long? cajaId)
            : base(consultaServicio)
        {
            if (cajaId.HasValue)
                BusquedaInicial = new BusquedaInicialConsulta { Filtro1 = cajaId.Value.ToString() };
        }

        protected override string TextoTitular => "Libro de caja";
        protected override string TextoLblCbx1 => "Caja";
        protected override string TextoLblCbx2 => "Tipo / Operación";
        protected override string TextoLblCbx3 => "Medio de pago";
        protected override bool MostrarColumnaCaja => true;
        protected override string TextoCheckHistorial => "Ver todo el historial (todas las cajas)";

        protected override void ConfigurarFiltrosEspecificos()
        {
            var cajas = new List<OpcionFiltro> { OpcionTodos("Todas las cajas") };
            cajas.AddRange(_consultaServicio.ObtenerCajasParaFiltro()
                .Select(c => new OpcionFiltro { Texto = c.Texto, Valor = c.Id.ToString() }));

            ActivarCombo(cbx1, lblcbx1, cajas, "Texto", "Valor", "Caja");

            var tipos = new List<OpcionFiltro>
            {
                OpcionTodos(),
                new OpcionFiltro { Texto = "Ingresos", Valor = "TM_" + (int)TipoMovimiento.Ingreso },
                new OpcionFiltro { Texto = "Egresos", Valor = "TM_" + (int)TipoMovimiento.Egreso },
                OpcionOperacion(Operacion.Cobro),
                OpcionOperacion(Operacion.Reversa),
                OpcionOperacion(Operacion.CargaSaldo),
                OpcionOperacion(Operacion.PagoGasto)
            };

            ActivarCombo(cbx2, lblcbx2, tipos, "Texto", "Valor", "Tipo / Operación");
            ActivarCombo(cbx3, lblcbx3, OpcionesMedioDePago(incluirCuentaCorriente: false), "Texto", "Valor", "Medio de pago");

            // Por defecto, la caja más reciente (normalmente la abierta).
            cbx1.SelectedIndex = cajas.Count > 1 ? 1 : 0;
            cbx2.SelectedIndex = 0;
            cbx3.SelectedIndex = 0;
        }

        protected override void AplicarFiltrosEspecificos(FiltroConsulta origen, FiltroMovimientos destino)
        {
            destino.CajaId = ComoLong(origen.Filtro1);
            AplicarTipoUOperacion(origen.Filtro2, destino);
            destino.TipoPago = ComoInt(origen.Filtro3);
        }

        protected override ResultadoMovimientos Consultar(FiltroMovimientos filtro) =>
            _consultaServicio.ConsultarLibroCaja(filtro);
    }
}
