using Presentacion.FBase.Helpers;
using Servicios.LogicaNegocio.Caja;
using Servicios.LogicaNegocio.Movimiento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Caja
{
    public partial class FCajaAbrir : Form
    {
        //private readonly ICajaServicio _cajaServicio;
        public CajaServicio cajaServicio;

        public FCajaAbrir()
        {
            cajaServicio = new CajaServicio();

            InitializeComponent();
        }

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            decimal montoApertura = txtMontoApertura.Text.Trim() == string.Empty ? 0 : Convert.ToDecimal(txtMontoApertura.Text);

            // verficar si hay caja abierta, si hay avisar al usuario

            var estadoCaja = cajaServicio.ObtenerEstadoCaja();

            if (estadoCaja)
            {
                MessageBox.Show("Hay una caja abierta!");
            }
            else
            {
                cajaServicio.AbrirCaja(montoApertura, DatosSistema.UsuarioId);

                MessageBox.Show("Caja Abierta");
                
                DatosSistema.EstaCajaAbierta = true;
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }
    }
}
