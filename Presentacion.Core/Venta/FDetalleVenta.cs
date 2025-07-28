using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Venta
{
    public partial class FDetalleVenta : FBase.FBase
    {
        public bool confirmarDetalle = false;

        public string descripcionDetalle = null;
        public FDetalleVenta()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirmarDetalle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDetalleVenta.Text))
            {
               descripcionDetalle = txtDetalleVenta.Text.Trim();
            }
            confirmarDetalle = true;
            this.DialogResult = DialogResult.OK;
        }
    }
}
