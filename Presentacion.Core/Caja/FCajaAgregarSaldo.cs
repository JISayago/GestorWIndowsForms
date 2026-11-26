using Presentacion.FBase.Helpers;
using Servicios.LogicaNegocio.Caja;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Caja
{
    /// <summary>
    /// Ventana para sumar efectivo a la caja abierta: pide el monto y el motivo.
    /// Al aceptar registra el movimiento (Ingreso en efectivo que afecta la caja) con el motivo como detalle.
    /// </summary>
    public partial class FCajaAgregarSaldo : FBase.FBase
    {
        private readonly CajaServicio _cajaServicio;
        private readonly decimal _saldoActual;

        private Label lblTitulo;
        private Label lblSaldoActualTitulo;
        private Label lblSaldoActual;
        private Label lblMontoTitulo;
        private TextBox txtMonto;
        private Label lblSaldoResultanteTitulo;
        private Label lblSaldoResultante;
        private Label lblMotivoTitulo;
        private TextBox txtMotivo;
        private Label lblContadorMotivo;
        private Button btnAgregar;
        private Button btnCancelar;

        /// <summary>Monto que se agregó a la caja (solo válido si el diálogo terminó en OK).</summary>
        public decimal MontoAgregado { get; private set; }

        /// <summary>Motivo con el que quedó registrado el movimiento (solo válido si el diálogo terminó en OK).</summary>
        public string Motivo { get; private set; } = string.Empty;

        public FCajaAgregarSaldo()
        {
            InitializeComponent();

            _cajaServicio = new CajaServicio();

            try
            {
                _saldoActual = _cajaServicio.ObtenerSaldoCaja();
            }
            catch
            {
                // Si no hay caja abierta, el click de "Agregar saldo" mostrará el error.
                _saldoActual = 0;
            }

            ConstruirControles();
            ActualizarSaldoResultante();
        }

        private void ConstruirControles()
        {
            Text = "Agregar saldo a la caja";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;

            ClientSize = new Size(470, 360);

            const int xEtiqueta = 24;
            const int xCampo = 170;
            const int anchoCampo = 270;

            lblTitulo = new Label
            {
                Text = "Agregar saldo a la caja",
                AutoSize = true,
                Location = new Point(xEtiqueta, 18)
            };

            lblSaldoActualTitulo = new Label
            {
                Text = "Saldo actual:",
                AutoSize = true,
                Location = new Point(xEtiqueta, 64)
            };

            lblSaldoActual = new Label
            {
                AutoSize = true,
                Text = _saldoActual.ToString("C2"),
                Location = new Point(xCampo, 64)
            };

            lblMontoTitulo = new Label
            {
                Text = "Monto a agregar:",
                AutoSize = true,
                Location = new Point(xEtiqueta, 104)
            };

            txtMonto = new TextBox
            {
                Width = anchoCampo,
                Location = new Point(xCampo, 100),
                Text = "$ 0",
                MaxLength = 20,
                TabIndex = 0
            };
            txtMonto.KeyPress += txtMonto_KeyPress;
            txtMonto.TextChanged += txtMonto_TextChanged;

            lblSaldoResultanteTitulo = new Label
            {
                Text = "Saldo resultante:",
                AutoSize = true,
                Location = new Point(xEtiqueta, 144)
            };

            lblSaldoResultante = new Label
            {
                AutoSize = true,
                Location = new Point(xCampo, 144)
            };

            lblMotivoTitulo = new Label
            {
                Text = "Motivo:",
                AutoSize = true,
                Location = new Point(xEtiqueta, 184)
            };

            txtMotivo = new TextBox
            {
                Multiline = true,
                Width = anchoCampo,
                Height = 80,
                Location = new Point(xCampo, 180),
                MaxLength = CajaServicio.LargoMaximoMotivoAgregadoSaldo,
                ScrollBars = ScrollBars.Vertical,
                TabIndex = 1
            };
            txtMotivo.TextChanged += txtMotivo_TextChanged;

            lblContadorMotivo = new Label
            {
                AutoSize = true,
                Location = new Point(xCampo, 264)
            };
            ActualizarContadorMotivo();

            btnAgregar = new Button
            {
                Text = "Agregar saldo",
                Size = new Size(130, 36),
                Location = new Point(ClientSize.Width - 24 - 110 - 12 - 130, 306),
                TabIndex = 2
            };
            btnAgregar.Click += btnAgregar_Click;

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Size = new Size(110, 36),
                Location = new Point(ClientSize.Width - 24 - 110, 306),
                TabIndex = 3
            };
            btnCancelar.Click += btnCancelar_Click;

            Controls.Add(lblTitulo);
            Controls.Add(lblSaldoActualTitulo);
            Controls.Add(lblSaldoActual);
            Controls.Add(lblMontoTitulo);
            Controls.Add(txtMonto);
            Controls.Add(lblSaldoResultanteTitulo);
            Controls.Add(lblSaldoResultante);
            Controls.Add(lblMotivoTitulo);
            Controls.Add(txtMotivo);
            Controls.Add(lblContadorMotivo);
            Controls.Add(btnAgregar);
            Controls.Add(btnCancelar);

            AcceptButton = btnAgregar;
            CancelButton = btnCancelar;

            ActiveControl = txtMonto;
        }

        // La base de formularios se traga el Enter: del monto se pasa al motivo.
        protected override void EjecutarEnter()
        {
            if (ActiveControl == txtMonto)
                txtMotivo.Focus();
        }

        protected override void AplicarTema(Control parent)
        {
            base.AplicarTema(parent);

            if (!ReferenceEquals(parent, this))
                return;

            BackColor = TemaSistema.Fondo;
            ForeColor = TemaSistema.Texto;

            if (lblTitulo != null)
            {
                lblTitulo.ForeColor = TemaSistema.Primario;
                lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            }

            foreach (var lbl in new[] { lblSaldoActualTitulo, lblMontoTitulo, lblSaldoResultanteTitulo, lblMotivoTitulo })
            {
                if (lbl == null) continue;
                lbl.ForeColor = TemaSistema.Texto;
                lbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            }

            if (lblSaldoActual != null)
            {
                lblSaldoActual.ForeColor = TemaSistema.Primario;
                lblSaldoActual.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            }

            if (lblSaldoResultante != null)
                lblSaldoResultante.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

            if (lblContadorMotivo != null)
            {
                lblContadorMotivo.ForeColor = TemaSistema.Borde;
                lblContadorMotivo.Font = new Font("Segoe UI", 8.5F);
            }

            foreach (var txt in new[] { txtMonto, txtMotivo })
            {
                if (txt == null) continue;
                txt.BackColor = TemaSistema.FondoControl;
                txt.ForeColor = TemaSistema.Texto;
                txt.BorderStyle = BorderStyle.FixedSingle;
                txt.Font = new Font("Segoe UI", 10F);
            }

            if (btnAgregar != null)
            {
                btnAgregar.FlatStyle = FlatStyle.Flat;
                btnAgregar.FlatAppearance.BorderSize = 0;
                btnAgregar.BackColor = TemaSistema.Primario;
                btnAgregar.ForeColor = Color.White;
                btnAgregar.Cursor = Cursors.Hand;
                btnAgregar.UseVisualStyleBackColor = false;
            }

            if (btnCancelar != null)
            {
                btnCancelar.FlatStyle = FlatStyle.Flat;
                btnCancelar.FlatAppearance.BorderSize = 1;
                btnCancelar.FlatAppearance.BorderColor = TemaSistema.Borde;
                btnCancelar.BackColor = TemaSistema.Seleccion;
                btnCancelar.ForeColor = TemaSistema.Texto;
                btnCancelar.Cursor = Cursors.Hand;
                btnCancelar.UseVisualStyleBackColor = false;
            }
        }

        // ----------------------------------------------------------------
        // Monto (pesos enteros, igual que el monto de apertura de la caja)
        // ----------------------------------------------------------------

        private static decimal ParsearMonto(string texto)
        {
            string soloDigitos = new string((texto ?? string.Empty).Where(char.IsDigit).ToArray());

            return decimal.TryParse(soloDigitos, out var monto) ? monto : 0;
        }

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void txtMonto_TextChanged(object sender, EventArgs e)
        {
            if (sender is not TextBox txt)
                return;

            txt.TextChanged -= txtMonto_TextChanged;

            string soloNumeros = new string(txt.Text.Where(char.IsDigit).ToArray());

            if (long.TryParse(soloNumeros, out long valor))
                txt.Text = valor.ToString("C0");
            else
                txt.Text = "$ 0";

            txt.SelectionStart = txt.Text.Length;
            txt.TextChanged += txtMonto_TextChanged;

            ActualizarSaldoResultante();
        }

        private void ActualizarSaldoResultante()
        {
            if (lblSaldoResultante == null || txtMonto == null)
                return;

            decimal resultado = _saldoActual + ParsearMonto(txtMonto.Text);

            lblSaldoResultante.Text = resultado.ToString("C2");
            lblSaldoResultante.ForeColor = resultado < 0 ? Color.Firebrick : Color.FromArgb(46, 125, 50);
        }

        // ----------------------------------------------------------------
        // Motivo
        // ----------------------------------------------------------------

        private void txtMotivo_TextChanged(object sender, EventArgs e)
        {
            ActualizarContadorMotivo();
        }

        private void ActualizarContadorMotivo()
        {
            if (lblContadorMotivo == null || txtMotivo == null)
                return;

            lblContadorMotivo.Text = $"{txtMotivo.Text.Length}/{CajaServicio.LargoMaximoMotivoAgregadoSaldo}";
        }

        // ----------------------------------------------------------------
        // Botones
        // ----------------------------------------------------------------

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            decimal monto = ParsearMonto(txtMonto.Text);

            if (monto <= 0)
            {
                Informar("Ingresá un monto mayor que cero.", "Atención");
                txtMonto.Focus();
                txtMonto.SelectAll();
                return;
            }

            // Una sola línea: sin saltos de línea ni espacios de más.
            string motivo = string.Join(" ",
                txtMotivo.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                .Trim();

            if (motivo.Length == 0)
            {
                Informar("Indicá el motivo del agregado de saldo.", "Atención");
                txtMotivo.Focus();
                return;
            }

            var resultado = _cajaServicio.AgregarSaldo(monto, motivo);

            if (!resultado.Exitoso)
            {
                MostrarError(resultado.Mensaje);
                return;
            }

            MontoAgregado = monto;
            Motivo = motivo;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
