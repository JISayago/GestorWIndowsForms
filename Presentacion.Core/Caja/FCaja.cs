using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Caja
{
    public partial class FCaja : Form
    {
        public FCaja()
        {
            InitializeComponent();
        }

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            var FCajaAbrir = new FCajaAbrir();
            FCajaAbrir.ShowDialog();
        }
    }
}
