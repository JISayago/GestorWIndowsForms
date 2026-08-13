using Servicios.LogicaNegocio.Cliente;
using Servicios.LogicaNegocio.CuentaCorriente;
using Servicios.LogicaNegocio.CuentaCorriente.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.CuentaCorriente
{
    public partial class FCuentaCorrienteValidacion : FBase.FBase
    {
        private readonly ICuentaCorrienteServicio _ctacteServicio;
        private readonly CuentaCorrienteDTO ctaCte;
        private readonly long clienteId;
        private readonly decimal monto;
        private readonly HashSet<string> dniAutorizadosNormalizados;

        public FCuentaCorrienteValidacion(long clienteCargado, decimal montoCtaCte)
        {
            InitializeComponent();

            clienteId = clienteCargado;
            monto = montoCtaCte;

            _ctacteServicio = new CuentaCorrienteServicio();

            ctaCte = _ctacteServicio.ObtenerCuentaCorrientePorClienteId(clienteId);
            dniAutorizadosNormalizados = CargarDnisAutorizados(ctaCte.CuentaCorrienteId, clienteId);

            lblCtaCte.Text = ctaCte.NombreCuentaCorriente;
            lblSaldoDisponible.Text = $"Saldo Disponible: {ctaCte.Saldo}";
            lblLimite.Text = $"Limite Deuda: {ctaCte.LimiteDeuda}";
        }

        private HashSet<string> CargarDnisAutorizados(long cuentaId, long clientePersonaId)
        {
            var dnis = _ctacteServicio.ObtenerDnisAutorizados(cuentaId) ?? new List<string>();
            var set = new HashSet<string>(
                dnis.Select(NormalizarDni).Where(d => !string.IsNullOrEmpty(d)),
                StringComparer.Ordinal);

            // El DNI del titular siempre debe poder operar, aunque falte en la lista autorizada.
            try
            {
                var cliente = new ClienteServicio().ObtenerClientePorId(clientePersonaId);
                var dniTitular = NormalizarDni(cliente?.Dni);
                if (!string.IsNullOrEmpty(dniTitular))
                    set.Add(dniTitular);
            }
            catch
            {
                // Si no se puede leer el cliente, se valida solo con la lista de autorizados.
            }

            return set;
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            var response = _ctacteServicio.PuedeComprar(ctaCte.CuentaCorrienteId, monto);
            if (!response.Exitoso)
            {
                MessageBox.Show(response.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var dniIngresado = NormalizarDni(txtDni.Text);
            if (string.IsNullOrEmpty(dniIngresado))
            {
                MessageBox.Show("Por favor, ingrese un DNI válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDni.Focus();
                return;
            }

            // Antes: el MessageBox estaba en el else del foreach → avisaba en el primer
            // DNI que no coincidía y después igual podía cerrar OK si otro sí coincidía.
            if (!dniAutorizadosNormalizados.Contains(dniIngresado))
            {
                MessageBox.Show("DNI no autorizado para cuenta corriente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDni.Focus();
                txtDni.SelectAll();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private static string NormalizarDni(string? dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return string.Empty;

            // Acepta "30.111.222", "30111222 ", etc.
            return new string(dni.Where(char.IsDigit).ToArray());
        }
    }
}
