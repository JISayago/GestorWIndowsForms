using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Movimiento;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.Movimiento
{
    public partial class FMovimientoConsulta : FBaseConsulta
    {
        private readonly IMovimientoServicio _movimientoServicio;

        public FMovimientoConsulta() : this(new MovimientoServicio())
        {
            InitializeComponent();
        }

        public FMovimientoConsulta(IMovimientoServicio movimientoServicio)
        {
            _movimientoServicio = movimientoServicio;
        }

        #region INIT

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            Text = "Consulta de Movimientos";
        }

        #endregion

        #region 🔥 ACCIONES EXTRA (opcional futuro)

        protected override void ConfigurarAccionesPersonalizadas()
        {
            // abrir detalle desde botón dinámico (opcional)
            AgregarAccion(
                "Ver Detalle",
                SystemIcons.Information.ToBitmap(),
                (id) =>
                {
                    if (!id.HasValue) return;

                    var f = new FMovimientoDetallado(id);
                    f.ShowDialog();
                },
                true
            );
        }

        #endregion

        #region 🧱 GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (!grilla.Columns.Contains("MovimientoId")) return;

                grilla.Columns["MovimientoId"].Visible = false;
                grilla.Columns["MovimientoId"].Name = "Id";

                grilla.Columns["NumeroMovimiento"].Visible = true;
                grilla.Columns["NumeroMovimiento"].HeaderText = "Número";
                grilla.Columns["NumeroMovimiento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                grilla.Columns["FechaMovimiento"].Visible = true;
                grilla.Columns["FechaMovimiento"].HeaderText = "Fecha";
                grilla.Columns["FechaMovimiento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        #endregion
        #region 🔥 ACTUALIZAR DATOS (NUEVO SISTEMA)

        #endregion


        #region 🔥 ACTUALIZAR DATOS (NUEVO SISTEMA)


        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            var resultado = _movimientoServicio.ObtenerMovimientos(filtros);

            dgv.DataSource = resultado.Items;

            // 🔴 CLAVE: volver a aplicar formato
            ResetearGrilla(dgv);

            var paginacion = new DatosPaginacion
            {
                PaginaActual = resultado.Page,
                PageSize = resultado.PageSize,
                CantidadRegistros = resultado.TotalRegistros,
            };

            ActualizarPaginacionUI(paginacion);

            BarraLateralBotones.Enabled = !filtros.VerEliminados;
        }


        #endregion

        #region BOTON ABRIR DETALLE (label viejo)

        private void lblAbrir_Click(object sender, EventArgs e)
        {
            if (!entidadID.HasValue) return;

            var f = new FMovimientoDetallado(entidadID);
            f.ShowDialog();
        }

        #endregion
        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

            // 🔹 Combo opcional → Tipo de movimiento (opcional)
            var opciones = new List<OpcionFiltro>
    {
        new OpcionFiltro { Texto = "Todos", Valor = "0" },
        new OpcionFiltro { Texto = "Ingreso", Valor = "1" },
        new OpcionFiltro { Texto = "Egreso", Valor = "2" }
    };

            ActivarFiltroCombo(opciones, "Texto", "Valor");

            cbxFiltroOpcional.SelectedValue = "0";

            // 📅 Fechas (clave para movimientos)
            ActivarFiltroFechas("Filtrar por fecha de movimiento");
        }
    }
}
