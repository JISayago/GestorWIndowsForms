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
    public partial class FCaja : Form
    {
        public CajaServicio cajaServicio;



        public FCaja()
        {
            cajaServicio = new CajaServicio();

            InitializeComponent();
        }

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            var FCajaAbrir = new FCajaAbrir();

            var result = FCajaAbrir.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnCerrarCaja_Click(object sender, EventArgs e)
        {
            var FCerrarCaja = new FCerrarCaja();

            var result = FCerrarCaja.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void FCaja_Load(object sender, EventArgs e)
        {
            var estadoCaja = cajaServicio.ObtenerEstadoCaja();

            lblEstadoCaja.Text = estadoCaja ? "CAJA ABIERTA" : "CAJA CERRADA";
        }

        private void btnConsultarCajas_Click(object sender, EventArgs e)
        {
            var formConsultaCajas = new FCajaConsulta();
            formConsultaCajas.ShowDialog();
        }
    }
}
