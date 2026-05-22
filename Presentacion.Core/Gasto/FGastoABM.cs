using AccesoDatos.Entidades;
using Presentacion.FBase;
using Servicios.Helpers.Gasto;
using Servicios.LogicaNegocio.Gasto;
using Servicios.LogicaNegocio.Gasto.DTO;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Gasto
{
    public partial class FGastoABM : FBaseABM
    {
        private readonly IGastoServicio _gastoServicio;
        private readonly long _logeadoId;

        public bool RealizoAlgunaOperacion = false;

        public FGastoABM(long logeadoId)
        {
            InitializeComponent();
            _gastoServicio = new GastoServicio();
            _logeadoId = logeadoId;

            AgregarControlesObligatorios(txtDetalle, "Detalle");
            AgregarControlesObligatorios(txtMontoPago, "Monto Pago");
        }

        private void FGastoABM_Load(object sender, EventArgs e)
        {
            // 🔹 Categorías
            var listaCategoria = Enum.GetValues(typeof(CategoriaGasto))
                .Cast<CategoriaGasto>()
                .Select(x => new
                {
                    Texto = x.ToString().Replace("_", " "),
                    Valor = (int)x
                })
                .ToList();

            cmbCategoriaGasto.DataSource = listaCategoria;
            cmbCategoriaGasto.DisplayMember = "Texto";
            cmbCategoriaGasto.ValueMember = "Valor";

            // 🔹 Estado (solo Pagado / Pendiente)
            var listaEstado = Enum.GetValues(typeof(EstadoGasto))
                .Cast<EstadoGasto>()
                .Where(x => x == EstadoGasto.Pagado || x == EstadoGasto.Pendiente)
                .Select(x => new
                {
                    Texto = x.ToString(),
                    Valor = (int)x
                })
                .ToList();

            cmbEstado.DataSource = listaEstado;
            cmbEstado.DisplayMember = "Texto";
            cmbEstado.ValueMember = "Valor";

            // 🔥 Estado inicial
            chkPagado.Checked = true;
            chkPendiente.Checked = false;

            dtpDiaGasto.Enabled = true;
            cmbEstado.SelectedValue = (int)EstadoGasto.Pagado;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegistrarGasto_Click(object sender, EventArgs e)
        {
            RegistrarGasto();
        }

        private void RegistrarGasto()
        {
            if (cmbCategoriaGasto.SelectedValue == null)
            {
                MessageBox.Show("Seleccione una categoría.");
                return;
            }

            if (!decimal.TryParse(txtMontoPago.Text, out var monto))
            {
                MessageBox.Show("Monto inválido.");
                return;
            }

            int estado;

            if (chkPagado.Checked)
                estado = (int)EstadoGasto.Pagado;
            else if (chkPendiente.Checked)
                estado = (int)EstadoGasto.Pendiente;
            else
            {
                MessageBox.Show("Seleccione un estado.");
                return;
            }

            // 🔥 Fecha nullable
            DateTime? fechaGasto = null;

            if (estado == (int)EstadoGasto.Pagado)
            {
                if (!dtpDiaGasto.Enabled)
                {
                    MessageBox.Show("Un gasto pagado debe tener fecha.");
                    return;
                }

                fechaGasto = dtpDiaGasto.Value;
            }

            var gasto = new GastoDTO
            {
                IdEmpleado = _logeadoId,
                CategoriaGasto = (int)cmbCategoriaGasto.SelectedValue,
                Detalle = txtDetalle.Text,
                MontoTotal = monto,
                FechaGasto = fechaGasto, // 🔥 NULL SI ES PENDIENTE
                EstadoGasto = estado
            };

            var resultado = _gastoServicio.NuevoGasto(gasto);

            if (resultado.Exitoso)
            {
                RealizoAlgunaOperacion = true;
                MessageBox.Show(resultado.Mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show(resultado.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // 🔘 CHECKS (CONTROLAN TODO)
        // =========================================================

        private void chkPagado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPagado.Checked)
            {
                chkPendiente.Checked = false;

                dtpDiaGasto.Enabled = true;
                cmbEstado.SelectedValue = (int)EstadoGasto.Pagado;
            }
        }

        private void chkPendiente_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPendiente.Checked)
            {
                chkPagado.Checked = false;

                dtpDiaGasto.Enabled = false;
                cmbEstado.SelectedValue = (int)EstadoGasto.Pendiente;
            }
        }
    }
}