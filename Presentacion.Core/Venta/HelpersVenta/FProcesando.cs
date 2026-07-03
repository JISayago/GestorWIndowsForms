using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Presentacion.Core
{
    public partial class FProcesando : Form
    {
        private Label lblEstado;

        private Panel barraFondo;
        private Panel barraProgreso;

        private Timer timer;

        private int posicion;

        public FProcesando()
        {
            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            Text = "Procesando";
            StartPosition = FormStartPosition.CenterScreen;

            FormBorderStyle = FormBorderStyle.FixedDialog;
            ControlBox = false;

            MaximizeBox = false;
            MinimizeBox = false;

            ShowInTaskbar = false;
            TopMost = true;

            Width = 400;
            Height = 140;

            BackColor = Color.White;

            lblEstado = new Label
            {
                Text = "Procesando...",
                AutoSize = false,
                Width = 340,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 15),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.Black
            };

            barraFondo = new Panel
            {
                Width = 340,
                Height = 20,
                Location = new Point(20, 55),
                BackColor = Color.Gainsboro
            };

            barraProgreso = new Panel
            {
                Width = 90,
                Height = barraFondo.Height,
                Left = -90,
                Top = 0,
                BackColor = ColorTranslator.FromHtml("#291a3e")
            };

            barraFondo.Controls.Add(barraProgreso);

            Controls.Add(lblEstado);
            Controls.Add(barraFondo);

            posicion = -barraProgreso.Width;

            timer = new Timer();
            timer.Interval = 15;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            posicion += 4;

            if (posicion > barraFondo.Width)
                posicion = -barraProgreso.Width;

            barraProgreso.Left = posicion;
        }

        public void ActualizarEstado(string texto)
        {
            lblEstado.Text = texto;
            lblEstado.Refresh();
            Application.DoEvents();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            timer?.Stop();
            timer?.Dispose();

            base.OnFormClosed(e);
        }
    }
}