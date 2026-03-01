using Presentacion.FBase;
using Presentacion.FBase.Helpers;
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

            if (grilla.Columns.Contains("MovimientoId"))
            {
                grilla.Columns["MovimientoId"].Visible = false;
                grilla.Columns["MovimientoId"].Name = "Id";
            }

            if (grilla.Columns.Contains("NumeroMovimiento"))
            {
                grilla.Columns["NumeroMovimiento"].Visible = true;
                grilla.Columns["NumeroMovimiento"].HeaderText = "Número";
                grilla.Columns["NumeroMovimiento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (grilla.Columns.Contains("FechaMovimiento"))
            {
                grilla.Columns["FechaMovimiento"].Visible = true;
                grilla.Columns["FechaMovimiento"].HeaderText = "Fecha";
                grilla.Columns["FechaMovimiento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        #endregion


        #region 🔥 ACTUALIZAR DATOS (NUEVO SISTEMA)

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            if (filtros.VerEliminados)
            {
                dgv.DataSource = _movimientoServicio.ObtenerMovimientoEliminado(filtros.TextoBuscar);
                BarraLateralBotones.Enabled = false;
            }
            else
            {
                dgv.DataSource = _movimientoServicio.ObtenerMovimiento(filtros.TextoBuscar);
                BarraLateralBotones.Enabled = false;
            }
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

    }
}
