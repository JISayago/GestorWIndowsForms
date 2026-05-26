using Presentacion.Core.Movimiento;
using Servicios.Helpers.Sistema.Rol;
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
            if (!AuthHelper.Tiene("Caja.Abrir"))
            {
                MessageBox.Show("No Cuenta con las credenciales necesarias para abrir la caja");
                return;
            }
            var FCajaAbrir = new FCajaAbrir();

            var result = FCajaAbrir.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnCerrarCaja_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.Tiene("Caja.Cerrar"))
            {
                MessageBox.Show("No Cuenta con las credenciales necesarias para cerrar la caja");
                return;
            }
            var FCerrarCaja = new FCerrarCaja();

            var result = FCerrarCaja.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void FCaja_Load(object sender, EventArgs e)
        {
            //var estadoCaja = cajaServicio.ObtenerEstadoCaja();
            var cajaInicial = cajaServicio.EstadoInicioCaja();
            if (!cajaInicial.EstaCerrada)
            {
                btnAbrirCaja.Enabled = false;
                lblSaldoCaja.Text = cajaInicial.SaldoActual.ToString("C");
                if (cajaInicial.SaldoActual > 0)
                {
                    lblSaldoCaja.ForeColor = Color.Green;
                }
                else
                {
                    lblSaldoCaja.ForeColor = Color.Red;
                }
            }
            else
            {
                var x = 0.00m;
                btnCerrarCaja.Enabled = false;
                lblSaldoCaja.Text = x.ToString("C");
            }
            lblEstadoCaja.Text = !cajaInicial.EstaCerrada ? "CAJA ABIERTA" : "CAJA CERRADA";
        }

        private void btnConsultarCajas_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.Tiene("Caja.VerMovimientos"))
            {
                MessageBox.Show("No Cuenta con las credenciales necesarias para ver los movimientos de la caja");
                return;
            }
            var formConsultaCajas = new FCajaConsulta();
            formConsultaCajas.ShowDialog();
        }

        private void btnConsultarMovimientos_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.Tiene("Caja.VerMovimientos"))
            {
                MessageBox.Show("No Cuenta con las credenciales necesarias para ver los movimientos de la caja");
                return;
            }
            var fmov = new FMovimientoConsulta();
            fmov.ShowDialog();
            // debería abrir un formulario específico para consultar movimientos de caja, pero por ahora reutilizo el de movimientos generales
        }
    }
}
