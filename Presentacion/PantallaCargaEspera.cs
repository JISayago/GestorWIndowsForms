using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class PantallaCargaEspera : Form
    {
        private PictureBox pictureBoxCarga;
        private Label lblMensaje;
        public PantallaCargaEspera(Image gifCarga, string mensaje = "Cargando...")
        {
            InitializeControls();

            if (gifCarga != null)
                pictureBoxCarga.Image = gifCarga;

            lblMensaje.Text = mensaje;
        }
        private void InitializeControls()
        {
            this.pictureBoxCarga = new PictureBox();
            this.lblMensaje = new Label();

            // Form
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.ControlBox = false;
            this.Width = 500;
            this.Height = 300;
            this.BackColor = Color.White;

            // PictureBox
            this.pictureBoxCarga.Size = new Size(200, 200);
            this.pictureBoxCarga.Location = new Point((this.ClientSize.Width - this.pictureBoxCarga.Width) / 2, 16);
            this.pictureBoxCarga.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBoxCarga.Anchor = AnchorStyles.Top;
            this.pictureBoxCarga.Image = null; // se asigna en el ctor

            // Label
            this.lblMensaje.AutoSize = false;
            this.lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
            this.lblMensaje.Dock = DockStyle.Bottom;
            this.lblMensaje.Height = 40;
            this.lblMensaje.Font = new Font("Segoe UI", 14F, FontStyle.Bold);

            // Agregar controles
            this.Controls.Add(this.pictureBoxCarga);
            this.Controls.Add(this.lblMensaje);

            // Centrar pictureBox (necesario después de agregar al form)
            this.pictureBoxCarga.Left = (this.ClientSize.Width - this.pictureBoxCarga.Width) / 2;
        }

        // Evitar que el usuario cierre el form con Alt+F4
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.F4))
                return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
