using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Caja;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Caja
{
    public partial class FCajaAbrir : FBase.FBase
    {
        public CajaServicio cajaServicio;
        public decimal SaldoApertura { get; private set; }
        public decimal SaldoFinal { get; private set; }

        private readonly TipoOperacion _tipo;
        private bool _esCierre;

        public FCajaAbrir(TipoOperacion tipo)
        {
            cajaServicio = new CajaServicio();
            InitializeComponent();

            _tipo = tipo;
            _esCierre = tipo == TipoOperacion.Cerrar;

            ConfigurarPantalla();
            AcomodarBotones();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AcomodarBotones();
        }

        private void AcomodarBotones()
        {
            if (pnlBotones == null || btnAbrirCaja == null || btnCancelar == null)
                return;

            const int gap = 12;
            int y = Math.Max(8, (pnlBotones.ClientSize.Height - btnAbrirCaja.Height) / 2);
            int right = pnlBotones.ClientSize.Width - pnlBotones.Padding.Right;

            btnCancelar.Location = new Point(right - btnCancelar.Width, y);
            btnAbrirCaja.Location = new Point(btnCancelar.Left - gap - btnAbrirCaja.Width, y);

            btnAbrirCaja.Visible = true;
            btnCancelar.Visible = true;
            btnAbrirCaja.BringToFront();
            btnCancelar.BringToFront();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AcomodarBotones();
        }

        protected override void AplicarTema(Control parent)
        {
            base.AplicarTema(parent);

            if (!ReferenceEquals(parent, this))
                return;

            BackColor = TemaSistema.Fondo;
            ForeColor = TemaSistema.Texto;

            EstilarTitulo(lblTitulo);
            EstilarLabel(lblUsuario);
            EstilarLabel(lblConfirmacion);
            EstilarLabel(lblMontoApertura);
            EstilarLabel(lblSaldoActualTitulo);

            if (lblUsuarioLogeado != null)
            {
                lblUsuarioLogeado.ForeColor = Color.FromArgb(46, 125, 50);
                lblUsuarioLogeado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            }

            if (lblSaldoActual != null)
            {
                lblSaldoActual.ForeColor = TemaSistema.Primario;
                lblSaldoActual.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            }

            if (txtMontoApertura != null)
            {
                txtMontoApertura.BackColor = TemaSistema.FondoControl;
                txtMontoApertura.ForeColor = TemaSistema.Texto;
                txtMontoApertura.BorderStyle = BorderStyle.FixedSingle;
            }

            EstilarBotonPrimario(btnAbrirCaja);
            EstilarBotonSecundario(btnCancelar);
        }

        private void ConfigurarPantalla()
        {
            lblUsuarioLogeado.Text = string.IsNullOrWhiteSpace(DatosSistema.NombreUsuario)
                ? "-"
                : DatosSistema.NombreUsuario;

            if (_esCierre)
            {
                Text = "Cerrar caja";
                lblTitulo.Text = "Cerrar caja";
                btnAbrirCaja.Text = "Cerrar caja";
                lblConfirmacion.Text = "Se registrará el cierre con el saldo actual de la caja.";

                lblMontoApertura.Visible = false;
                txtMontoApertura.Visible = false;

                lblSaldoActualTitulo.Visible = true;
                lblSaldoActual.Visible = true;

                decimal saldo = 0;
                try
                {
                    saldo = cajaServicio.ObtenerSaldoCaja();
                }
                catch
                {
                    // Si no hay caja abierta, el click mostrará el error.
                }

                lblSaldoActual.Text = saldo.ToString("C0");
            }
            else
            {
                Text = "Abrir caja";
                lblTitulo.Text = "Abrir caja";
                btnAbrirCaja.Text = "Abrir caja";
                lblConfirmacion.Text = "Ingresá el monto inicial con el que arranca la caja.";

                lblMontoApertura.Visible = true;
                txtMontoApertura.Visible = true;
                lblSaldoActualTitulo.Visible = false;
                lblSaldoActual.Visible = false;

                txtMontoApertura.Text = "$ 0";
                txtMontoApertura.Select();
            }
        }

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            try
            {
                if (_esCierre)
                {
                    if (!cajaServicio.ObtenerEstadoCaja())
                    {
                        MessageBox.Show("No hay una caja abierta para cerrar.", "Atención",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var confirmar = MessageBox.Show(
                        $"¿Confirmás el cierre de caja?\nSaldo actual: {lblSaldoActual.Text}",
                        "Cerrar caja",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (confirmar != DialogResult.Yes)
                        return;

                    cajaServicio.CerrarCaja(DatosSistema.UsuarioId);
                    DatosSistema.EstaCajaAbierta = false;
                    DatosSistema.CajaId = null;
                    SaldoFinal = cajaServicio.saldoFinalAlCierre;
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                if (cajaServicio.ObtenerEstadoCaja())
                {
                    MessageBox.Show("Ya hay una caja abierta. Cerrala antes de abrir otra.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal montoApertura = ParsearMonto(txtMontoApertura.Text);
                if (montoApertura < 0)
                {
                    MessageBox.Show("El monto de apertura no puede ser negativo.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMontoApertura.Focus();
                    return;
                }

                cajaServicio.AbrirCaja(montoApertura, DatosSistema.UsuarioId);
                DatosSistema.EstaCajaAbierta = true;
                var cajaId = cajaServicio.ObtenerIdCajaAbierta();
                if (cajaId.HasValue)
                    DatosSistema.CajaId = cajaId.Value;

                SaldoApertura = montoApertura;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private static decimal ParsearMonto(string texto)
        {
            string soloDigitos = new string((texto ?? string.Empty).Where(char.IsDigit).ToArray());
            return string.IsNullOrEmpty(soloDigitos) ? 0 : Convert.ToDecimal(soloDigitos);
        }

        private void txtMontoApertura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void txtMontoApertura_TextChanged(object sender, EventArgs e)
        {
            if (sender is not TextBox txt)
                return;

            txt.TextChanged -= txtMontoApertura_TextChanged;

            string soloNumeros = new string(txt.Text.Where(char.IsDigit).ToArray());

            if (long.TryParse(soloNumeros, out long valor))
                txt.Text = valor.ToString("C0");
            else
                txt.Text = "$ 0";

            txt.SelectionStart = txt.Text.Length;
            txt.TextChanged += txtMontoApertura_TextChanged;
        }

        private static void EstilarTitulo(Label lbl)
        {
            if (lbl == null) return;
            lbl.ForeColor = TemaSistema.Primario;
            lbl.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        }

        private static void EstilarLabel(Label lbl)
        {
            if (lbl == null) return;
            lbl.ForeColor = TemaSistema.Texto;
            lbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        private static void EstilarBotonPrimario(Button btn)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = TemaSistema.Primario;
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }

        private static void EstilarBotonSecundario(Button btn)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = TemaSistema.Borde;
            btn.BackColor = TemaSistema.Seleccion;
            btn.ForeColor = TemaSistema.Texto;
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }
    }
}
