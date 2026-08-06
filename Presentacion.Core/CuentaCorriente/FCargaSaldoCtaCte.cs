using Presentacion.FBase;
using Servicios.Helpers.Cliente.CtaCte;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.CuentaCorriente
{
    public partial class FCargaSaldoCtaCte : FBase.FBase
    {
        public decimal MontoIngresado { get; private set; }

        private Label lblTitulo;
        private Label lblSaldoActualTitulo;
        private Label lblSaldoActual;
        private Label lblMontoTitulo;
        private TextBox txtMonto;
        private Label lblSaldoResultanteTitulo;
        private Label lblSaldoResultante;

        private Button btnAceptar;
        private Button btnCancelar;

        private readonly decimal _importeActual;
        private readonly HelperFormularioCargaSaldoCtaCte _tipoCarga;

        public FCargaSaldoCtaCte(decimal importeActual, HelperFormularioCargaSaldoCtaCte tipoCarga)
        {
            InitializeComponent();

            _importeActual = importeActual;
            _tipoCarga = tipoCarga;

            InicializarControles();
            CargarDatos();
        }

        private void InicializarControles()
        {
            bool esSaldo = _tipoCarga == HelperFormularioCargaSaldoCtaCte.Saldo;

            Text = esSaldo ? "Carga de saldo" : "Límite de deuda";

            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            Width = 470;
            Height = 300;

            lblTitulo = new Label
            {
                Text = esSaldo ? "Carga de saldo" : "Límite de deuda",
                Font = new Font(Font.FontFamily, 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            lblSaldoActualTitulo = new Label
            {
                Text = esSaldo ? "Saldo actual:" : "Límite actual:",
                AutoSize = true,
                Location = new Point(20, 65)
            };

            lblSaldoActual = new Label
            {
                AutoSize = true,
                Font = new Font(Font.FontFamily, 10, FontStyle.Bold),
                Location = new Point(150, 65)
            };

            lblMontoTitulo = new Label
            {
                Text = esSaldo ? "Importe a cargar:" : "Nuevo límite:",
                AutoSize = true,
                Location = new Point(20, 110)
            };

            txtMonto = new TextBox
            {
                Width = 200,
                Location = new Point(150, 106),
                Text = "0"
            };

            txtMonto.TextChanged += TxtMonto_TextChanged;
            txtMonto.KeyPress += TxtMonto_KeyPress;

            lblSaldoResultanteTitulo = new Label
            {
                Text = esSaldo ? "Saldo resultante:" : "Límite resultante:",
                AutoSize = true,
                Location = new Point(20, 155)
            };

            lblSaldoResultante = new Label
            {
                AutoSize = true,
                Font = new Font(Font.FontFamily, 10, FontStyle.Bold),
                Location = new Point(150, 155)
            };

            btnAceptar = new Button
            {
                Text = "Aceptar",
                Width = 100,
                Height = 35,
                Location = new Point((Width - 250) / 2, 205)
            };

            btnAceptar.Click += BtnAceptar_Click;

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Width = 100,
                Height = 35,
                Location = new Point((Width + 50) / 2, 205)
            };

            btnCancelar.Click += BtnCancelar_Click;

            Controls.Add(lblTitulo);
            Controls.Add(lblSaldoActualTitulo);
            Controls.Add(lblSaldoActual);
            Controls.Add(lblMontoTitulo);
            Controls.Add(txtMonto);
            Controls.Add(lblSaldoResultanteTitulo);
            Controls.Add(lblSaldoResultante);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);

            AcceptButton = btnAceptar;
            CancelButton = btnCancelar;
        }

        private void CargarDatos()
        {
            lblSaldoActual.Text = _importeActual.ToString("C");
            lblSaldoActual.ForeColor = _importeActual < 0 ? Color.Firebrick : Color.DarkGreen;

            ActualizarImporteResultante();

            txtMonto.Focus();
            txtMonto.SelectAll();
        }

        private void ActualizarImporteResultante()
        {
            decimal.TryParse(txtMonto.Text, out decimal monto);

            decimal resultado;

            if (_tipoCarga == HelperFormularioCargaSaldoCtaCte.Saldo)
            {
                resultado = _importeActual + monto;
            }
            else
            {
                resultado = monto;
            }

            lblSaldoResultante.Text = resultado.ToString("C");
            lblSaldoResultante.ForeColor = resultado < 0 ? Color.Firebrick : Color.DarkGreen;
        }

        private void TxtMonto_TextChanged(object sender, EventArgs e)
        {
            ActualizarImporteResultante();
        }

        private void TxtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == ',' || e.KeyChar == '.')
            {
                if (txtMonto.Text.Contains(",") || txtMonto.Text.Contains("."))
                    e.Handled = true;

                return;
            }

            e.Handled = true;
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtMonto.Text, out decimal monto))
            {
                MessageBox.Show(
                    "Ingrese un importe válido.",
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtMonto.Focus();
                txtMonto.SelectAll();
                return;
            }

            if (monto <= 0)
            {
                MessageBox.Show(
                    "El importe debe ser mayor que cero.",
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtMonto.Focus();
                txtMonto.SelectAll();
                return;
            }

            if (_tipoCarga == HelperFormularioCargaSaldoCtaCte.Saldo)
                MontoIngresado = _importeActual + monto;
            else
                MontoIngresado = monto;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}