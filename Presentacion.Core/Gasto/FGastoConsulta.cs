using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Gasto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Gasto
{
    public partial class FGastoConsulta : FBaseConsulta
    {
        private readonly IGastoServicio _gastoServicio;
        private readonly long _logeadoId;

        public long? gastoSeleccionado = null;
        private bool vieneDeSeleccionGasto = false;

        public FGastoConsulta(long logeadoId)
            : this(new GastoServicio(), logeadoId)
        {
            InitializeComponent();
        }

        public FGastoConsulta(IGastoServicio gastoServicio, long logeadoId)
        {
            _gastoServicio = gastoServicio;
            _logeadoId = logeadoId;
        }

        public FGastoConsulta(bool _vieneDeSeleccionGasto, long logeadoId)
            : this(new GastoServicio(), logeadoId)
        {
            vieneDeSeleccionGasto = _vieneDeSeleccionGasto;
            InitializeComponent();
        }

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            grilla.Columns["GastoId"].Visible = false;
            grilla.Columns["GastoId"].Name = "Id";

            grilla.Columns["NumeroGasto"].Visible = true;
            grilla.Columns["NumeroGasto"].HeaderText = "N° Gasto";
            grilla.Columns["NumeroGasto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grilla.Columns["FechaGasto"].Visible = true;
            grilla.Columns["FechaGasto"].HeaderText = "Fecha";
            grilla.Columns["FechaGasto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grilla.Columns["CategoriaGasto"].Visible = true;
            grilla.Columns["CategoriaGasto"].HeaderText = "Categoría";
            grilla.Columns["CategoriaGasto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grilla.Columns["MontoTotal"].Visible = true;
            grilla.Columns["MontoTotal"].HeaderText = "Monto Total";
            grilla.Columns["MontoTotal"].DefaultCellStyle.Format = "C2";

            grilla.Columns["MontoPagado"].Visible = true;
            grilla.Columns["MontoPagado"].HeaderText = "Monto Pagado";
            grilla.Columns["MontoPagado"].DefaultCellStyle.Format = "C2";

            grilla.Columns["EstadoGasto"].Visible = true;
            grilla.Columns["EstadoGasto"].HeaderText = "Estado";
            grilla.Columns["EstadoGasto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grilla.Columns["Detalle"].Visible = true;
            grilla.Columns["Detalle"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public override void ActualizarDatos(
            DataGridView grilla,
            string cadenaBuscar,
            CheckBox check,
            ToolStrip toolStrip)
        {
            base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

            grilla.DataSource = _gastoServicio.ObtenerGastos(1);
            toolStrip.Enabled = true;
        }

        public override void EjecutarBtnNuevo()
        {
            var fGastoAbm = new FGastoABM(_logeadoId);
            fGastoAbm.ShowDialog();
        }

        private void ActualizarSegunOperacion(bool realizoOperacion)
        {
            if (realizoOperacion)
            {
                ActualizarDatos(dgvGrilla, string.Empty, cbxEstaEliminado, BarraLateralBotones);
            }
        }

        private void btnSeleccionarGasto_Click(object sender, EventArgs e)
        {
            if (!puedeEjecutarComando) return;

            gastoSeleccionado = entidadID;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnAnularGasto_Click(object sender, EventArgs e)
        {
            if (entidadID == null)
            {
                MessageBox.Show("Por favor seleccione un gasto.");
                return;
            }
            var resultado = _gastoServicio.AnularGasto(entidadID.Value);
            if (resultado.Exitoso)
            {
                MessageBox.Show($"{resultado.Mensaje}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarDatos(dgvGrilla, string.Empty, cbxEstaEliminado, BarraLateralBotones);
            }
            else
            {
                MessageBox.Show($"{resultado.Mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
