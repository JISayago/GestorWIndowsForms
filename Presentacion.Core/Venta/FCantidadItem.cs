using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Venta
{
    public partial class FCantidadItem : Form
    {
        public decimal cantidad;
        public FCantidadItem()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string texto = txtCantidad.Text.Trim();

            texto = texto.Replace(".", ",");

            if (decimal.TryParse(
                    texto,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out decimal cantidadParseada))
            {
                cantidad = cantidadParseada;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ingrese un número válido.");
                txtCantidad.Focus();
            }
        }
    }
}
