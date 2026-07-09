using Presentacion.FBase.Helpers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class PantallaCargaEspera : Form
    {
        private Panel barraFondo;
        private Panel barraProgreso;
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
            lblMensaje = new Label();
            lblPorcentaje = new Label();
            barraFondo = new Panel();
            barraProgreso = new Panel();

            // Form
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.ControlBox = false;
            this.Width = 500;
            this.Height = 220;
            this.BackColor = Color.White;

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

            // Fondo de barra
            barraFondo.Dock = DockStyle.Bottom;
            barraFondo.Height = 25;
            barraFondo.BackColor = Color.Gainsboro;
            barraFondo.Padding = new Padding(0);
            barraFondo.Margin = new Padding(0);

            // Progreso
            barraProgreso.Dock = DockStyle.Left;
            barraProgreso.Width = 0;
            barraProgreso.BackColor = ColorTranslator.FromHtml("#291a3e"); 

            barraFondo.Controls.Add(barraProgreso);

            panel.Controls.Add(barraFondo);
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

            int anchoTotal = barraFondo.ClientSize.Width;
            barraProgreso.Width = (anchoTotal * value) / 100;

            lblPorcentaje.Text = $"{value}%";
            Application.DoEvents();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.F4))
                return true;

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            var txt = lblPorcentaje?.Text ?? "0%";
            if (!int.TryParse(txt.Replace("%",""), out var p)) p = 0;
            barraProgreso.Width = (barraFondo.ClientSize.Width * p) / 100;
        }
    }
}