using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Formularios
{
    public partial class FProcesando : Form
    {
        private Label lblEstado;
        private ProgressBar progressBar;

        public FProcesando()
        {
            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            Text = "Procesando";
            StartPosition = FormStartPosition.CenterParent;

            FormBorderStyle = FormBorderStyle.FixedDialog;
            ControlBox = false;

            MaximizeBox = false;
            MinimizeBox = false;

            Width = 400;
            Height = 140;

            lblEstado = new Label
            {
                Text = "Procesando...",
                AutoSize = false,
                Width = 340,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 15)
            };

            progressBar = new ProgressBar
            {
                Width = 340,
                Height = 25,
                Location = new Point(20, 55),
                Style = ProgressBarStyle.Marquee
            };

            Controls.Add(lblEstado);
            Controls.Add(progressBar);
        }

        public void ActualizarEstado(string texto)
        {
            lblEstado.Text = texto;
            lblEstado.Refresh();
            Application.DoEvents();
        }
    }
}