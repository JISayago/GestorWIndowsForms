using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class PantallaCargaEspera : Form
    {
        private ProgressBar progressBarCarga;
        private Label lblMensaje;
        private Label lblPorcentaje;

        public PantallaCargaEspera(string mensaje = "Cargando...")
        {
            InitializeControls();
            lblMensaje.Text = mensaje;
            SetProgress(0);
        }

        private void InitializeControls()
        {
            progressBarCarga = new ProgressBar();
            lblMensaje = new Label();
            lblPorcentaje = new Label();

            // Form
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.ControlBox = false;
            this.Width = 500;
            this.Height = 220;
            this.BackColor = Color.White;

            // Contenedor simple
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24),
                BackColor = Color.White
            };

            // Mensaje
            lblMensaje.AutoSize = false;
            lblMensaje.Dock = DockStyle.Top;
            lblMensaje.Height = 60;
            lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
            lblMensaje.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblMensaje.ForeColor = Color.Black;

            // Porcentaje
            lblPorcentaje.AutoSize = false;
            lblPorcentaje.Dock = DockStyle.Bottom;
            lblPorcentaje.Height = 30;
            lblPorcentaje.TextAlign = ContentAlignment.MiddleCenter;
            lblPorcentaje.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            lblPorcentaje.ForeColor = Color.DimGray;

            // Barra
            progressBarCarga.Dock = DockStyle.Bottom;
            progressBarCarga.Height = 25;
            progressBarCarga.Minimum = 0;
            progressBarCarga.Maximum = 100;
            progressBarCarga.Value = 0;
            progressBarCarga.Style = ProgressBarStyle.Continuous;

            panel.Controls.Add(progressBarCarga);
            panel.Controls.Add(lblPorcentaje);
            panel.Controls.Add(lblMensaje);

            this.Controls.Add(panel);
        }

        public void SetMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
        }

        public void SetProgress(int value)
        {
            if (value < 0) value = 0;
            if (value > 100) value = 100;

            progressBarCarga.Value = value;
            lblPorcentaje.Text = $"{value}%";
            Application.DoEvents();
        }

        // Evitar cerrar con Alt+F4
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.F4))
                return true;

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}