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

            // 🔹 Estados (solo los válidos)
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

            // 🔥 Default
            cmbEstado.SelectedValue = (int)EstadoGasto.Pagado;
            dtpDiaGasto.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnRegistrarGasto_Click(object sender, EventArgs e)
        {
            await RegistrarGasto();
        }

        private async Task RegistrarGasto()
        {
            if (cmbCategoriaGasto.SelectedValue == null)
            {
                MessageBox.Show("Seleccione una categoría.");
                return;
            }

            if (cmbEstado.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un estado.");
                return;
            }

            if (!decimal.TryParse(txtMontoPago.Text, out var monto))
            {
                MessageBox.Show("Monto inválido.");
                return;
            }

            var estado = (int)cmbEstado.SelectedValue;

            DateTime? fechaGasto = null;

            if (estado == (int)EstadoGasto.Pagado)
            {
                fechaGasto = dtpDiaGasto.Value;
            }

            var gasto = new GastoDTO
            {
                IdEmpleado = _logeadoId,
                CategoriaGasto = (int)cmbCategoriaGasto.SelectedValue,
                Detalle = txtDetalle.Text,
                MontoTotal = monto,
                FechaGasto = fechaGasto,
                EstadoGasto = estado
            };

            using var frmProcesando = new FProcesando();

            try
            {
                btnRegistrarGasto.Enabled = false;

                frmProcesando.Show();
                frmProcesando.ActualizarEstado("Registrando gasto...");

                var resultado = await Task.Run(() =>
                    _gastoServicio.NuevoGasto(gasto));

                frmProcesando.Close();

                if (resultado.Exitoso)
                {
                    RealizoAlgunaOperacion = true;

                    MessageBox.Show(
                        resultado.Mensaje,
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    Close();
                }
                else
                {
                    MessageBox.Show(
                        resultado.Mensaje,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            finally
            {
                btnRegistrarGasto.Enabled = true;
            }
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstado.SelectedValue == null) return;

            var estadoObj = cmbEstado.SelectedItem;

            if (estadoObj == null)
                return;

            var estado = (int)estadoObj.GetType().GetProperty("Valor").GetValue(estadoObj);

            bool esPagado = estado == (int)EstadoGasto.Pagado;

            dtpDiaGasto.Enabled = esPagado;

            if (!esPagado)
            {
                // opcional: limpiar visualmente
                dtpDiaGasto.Value = DateTime.Now;
            }
        }
    }
}