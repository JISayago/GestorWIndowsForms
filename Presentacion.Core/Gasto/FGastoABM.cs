using Servicios.Helpers;
using Servicios.LogicaNegocio.Gasto;
using Servicios.LogicaNegocio.Gasto.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Gasto
{
    public partial class FGastoABM : Form
    {
        private readonly IGastoServicio _gastoServicio;
        private bool _esPagoPendiente;
        private readonly long _logeadoId;
        public FGastoABM(long logeadoId)
        {
            InitializeComponent();
            _gastoServicio = new GastoServicio();
            _logeadoId = logeadoId;
        }

        private void btnRegistrarGasto_Click(object sender, EventArgs e)
        {
            _esPagoPendiente = false;
            RegistrarGasto(_esPagoPendiente);
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void RegistrarGasto(bool esPagoPendiente)
        {
            var gasto = new GastoDTO
            {
                IdEmpleado = _logeadoId,
                CategoriaGasto = 1,
                Detalle = txtDetalle.Text,
                MontoTotal = decimal.Parse(txtMontoPago.Text),
                FechaGasto = dtpDiaGasto.Value,
                
            };
            if (!_esPagoPendiente)
            {
                gasto.EstadoGasto = (int)EstadoGasto.Pagado;
            }
            else
            {
                gasto.EstadoGasto = (int)EstadoGasto.Pendiente;
            }
             var resultado = _gastoServicio.NuevoGasto(gasto);
            if (resultado.Exitoso)
            {
                MessageBox.Show($"{resultado.Mensaje}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                               MessageBox.Show($"{resultado.Mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnPagoPendiente_Click(object sender, EventArgs e)
        {
            _esPagoPendiente = true;
            RegistrarGasto(_esPagoPendiente);
        }
    }
}
