using Presentacion.FBase.Helpers;
using Servicios.LogicaNegocio.Caja;
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
    public partial class FCerrarCaja : Form
    {
        public CajaServicio cajaServicio;

        public FCerrarCaja()
        {
            cajaServicio = new CajaServicio();


            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var estadoCaja = cajaServicio.ObtenerEstadoCaja();

            if (estadoCaja)
            {
                cajaServicio.CerrarCaja();

                MessageBox.Show("Caja Cerrada");

                DatosSistema.EstaCajaAbierta = false;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("No hay caja abierta!");
            }
        }
    }
}
