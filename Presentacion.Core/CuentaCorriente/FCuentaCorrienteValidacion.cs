using Presentacion.Core.Venta.TipoPago;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Cliente.DTO;
using Servicios.LogicaNegocio.CuentaCorriente;
using Servicios.LogicaNegocio.CuentaCorriente.DTO;
using Servicios.LogicaNegocio.Venta;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.CuentaCorriente
{
    public partial class FCuentaCorrienteValidacion : Form
    {
        private readonly ICuentaCorrienteServicio _ctacteServicio;
        private CuentaCorrienteDTO ctaCte;
        private long clienteId;
        private decimal monto;
        private List<long> dniAutorizados;

        public FCuentaCorrienteValidacion(long clienteCargado, decimal montoCtaCte)
        {

            //cambiar clienteDTO por clienteId
            InitializeComponent();

            clienteId = clienteCargado;
            monto = montoCtaCte;

            _ctacteServicio = new CuentaCorrienteServicio();            

            ctaCte = _ctacteServicio.ObtenerCuentaCorrientePorClienteId(clienteId);
            dniAutorizados = _ctacteServicio.ObtenerDnisAutorizados(ctaCte.CuentaCorrienteId);

            lblCtaCte.Text = ctaCte.NombreCuentaCorriente;
            lblSaldoDisponible.Text = $"{"Saldo Disponible:"} {ctaCte.Saldo.ToString()}";
            lblLimite.Text = $"{"Limite Deuda:"} {ctaCte.LimiteDeuda.ToString()}";

            //arreglar uso de de servicios al pedo

            //validar saldo de la ctacte respecto al la compra

        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            if(!_ctacteServicio.PuedeComprar(ctaCte.CuentaCorrienteId, monto))
            {
                MessageBox.Show("La cuenta corriente no tiene saldo suficiente para realizar la compra.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDni.Text))
            {
                MessageBox.Show("Por favor, ingrese un DNI válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (var dni in dniAutorizados)
            {
                if (txtDni.Text.Trim() == dni.ToString())
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
                else
                {
                    MessageBox.Show("DNI no autorizado para cuenta corriente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
