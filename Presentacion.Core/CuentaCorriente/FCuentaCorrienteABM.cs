using AccesoDatos.Entidades;
using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Cliente;
using Servicios.LogicaNegocio.CuentaCorriente;
using Servicios.LogicaNegocio.CuentaCorriente.DTO;
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
    public partial class FCuentaCorrienteABM : FBaseABM
    {
        public FCuentaCorrienteABM()
        {
            InitializeComponent();
        }
        private readonly ICuentaCorrienteServicio _cuentacorrienteServicio;
        private readonly IClienteServicio _clienteServicio;

        public override void FBaseABM_Load(object sender, EventArgs e)
        {
            base.FBaseABM_Load(sender, e);
            Inicializador(EntidadID);
        }
        public FCuentaCorrienteABM(TipoOperacion tipoOperacion, long? entidadID = null) : base(tipoOperacion, entidadID)
        {
            InitializeComponent();
            _cuentacorrienteServicio = new CuentaCorrienteServicio();
            _clienteServicio = new ClienteServicio();

            if (tipoOperacion == TipoOperacion.Eliminar || tipoOperacion == TipoOperacion.Modificar)
            {
                CargarDatos(entidadID);
            }

            if (tipoOperacion == TipoOperacion.Eliminar)
            {
                DesactivarControles(this);
            }

            dtpFechaVencimiento.MinDate = DateTime.Now;

            var clientes = _clienteServicio.ObtenerClientes("").ToList();

            cmbClientes.DisplayMember = "NombreCompleto"; // lo que se muestra
            cmbClientes.ValueMember = "PersonaId";
            cmbClientes.DataSource = clientes;

            cmbClientes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbClientes.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbClientes.DropDownStyle = ComboBoxStyle.DropDown;


            AgregarControlesObligatorios(txtNombreCC, "Nombre Cuenta Corriente");
            AgregarControlesObligatorios(txtSaldo, "Saldo");

        }
        public override void Inicializador(long? entidadId)
        {

        }

        public override void CargarDatos(long? entidadId)
        {
            if (!entidadId.HasValue)
            {
                MessageBox.Show(@"Ocurrio un Error Grave", @"Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                this.Close();
            }

            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                btnLimpiar.Enabled = false;
            }

            var cuentacorriente = _cuentacorrienteServicio.ObtenerCuentaCorrientePorId(entidadId.Value);
            var clienteDeCuentaCorriente = _clienteServicio.ObtenerClientes(cuentacorriente.ClienteId.ToString()).ToList();
            // Datos Personales

            txtNombreCC.Text = cuentacorriente.NombreCuentaCorriente;
            txtSaldo.Text = cuentacorriente.Saldo.ToString(); // FIX: Assign the value as string
            dtpFechaVencimiento.Value = (DateTime)cuentacorriente.FechaVencimiento;
            chkLimiteDeuda.Checked = cuentacorriente.LimiteDeudaActivo;
            txtLimiteDeuda.Text = cuentacorriente.LimiteDeuda.ToString();
            txtLimiteDeuda.Enabled = cuentacorriente.LimiteDeudaActivo;
            cmbClientes.DisplayMember = "NombreCompleto"; // lo que se muestra
            cmbClientes.ValueMember = "PersonaId";
            cmbClientes.DataSource = clienteDeCuentaCorriente;
            cmbClientes.Enabled = false; // No se puede cambiar el cliente asociado en la modificación

            dgvDni.DataSource = cuentacorriente.DniAutorizados.Select(x => new { DNI = x }).ToList();

        }


        public override bool EjecutarComandoNuevo()
        {

            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            var nuevoCuentaCorriente = new CuentaCorrienteDTO
            {
                NombreCuentaCorriente = txtNombreCC.Text,
                Saldo = Convert.ToDecimal(txtSaldo.Text),
                FechaVencimiento = dtpFechaVencimiento.Value,
                LimiteDeudaActivo = chkLimiteDeuda.Checked,
                LimiteDeuda = Convert.ToDecimal(txtLimiteDeuda.Text),
                ClienteId = (long)cmbClientes.SelectedValue,
                DniAutorizados = dgvDni.Rows
                                  .Cast<DataGridViewRow>()
                                  .Where(r => r.Cells["Dni"].Value != null)
                                  .Select(r => Convert.ToInt64(r.Cells["Dni"].Value))
                                  .ToList(),
                EstaEliminado = false,
            };

            var response = _cuentacorrienteServicio.Insertar(nuevoCuentaCorriente);

            if (response.Exitoso)
            {
                MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                return true;
            }
            else
            {
                MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return false;
            }
        }

        public override bool EjecutarComandoEliminar()
        {
            if (!EntidadID.HasValue)
            {
                MessageBox.Show(@"´Por favor seleccione un cuentacorriente válido.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;

            }
            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                var response = _cuentacorrienteServicio.Eliminar((long)EntidadID);
                if (response.Exitoso)
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    return false;
                }

            }
            return false;
        }
        public override bool EjecutarComandoModificar()
        {
            if (!EntidadID.HasValue)
            {
                MessageBox.Show(@"´Por favor seleccione un cuentacorriente válido.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;

            }
            if (TipoOperacion == TipoOperacion.Modificar)
            {

                var cuentacorrienteEditar = new CuentaCorrienteDTO
                {
                    NombreCuentaCorriente = txtNombreCC.Text,
                    Saldo = Convert.ToDecimal(txtSaldo.Text),
                    FechaVencimiento = dtpFechaVencimiento.Value,
                    LimiteDeudaActivo = chkLimiteDeuda.Checked,
                    LimiteDeuda = Convert.ToDecimal(txtLimiteDeuda.Text),
                    DniAutorizados = dgvDni.Rows.Cast<DataGridViewRow>()
                                      .Select(r => Convert.ToInt64(r.Cells["DNI"].Value))
                                      .ToList(),
                    EstaEliminado = false
                };

                var response = _cuentacorrienteServicio.Modificar(cuentacorrienteEditar, EntidadID);

                if (response.Exitoso)
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    return false;
                }

            }
            return false;
        }

        private void chkbLimiteDeuda_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLimiteDeuda.Checked)
            {
                // Habilitar el textbox y darle foco
                txtLimiteDeuda.Enabled = true;

                // Si está vacío, poner valor base 0
                if (string.IsNullOrWhiteSpace(txtLimiteDeuda.Text))
                    txtLimiteDeuda.Text = "0";
            }
            else
            {
                // Deshabilitar el textbox y resetear a 0
                txtLimiteDeuda.Enabled = false;
                txtLimiteDeuda.Text = "0";
            }
        }

        private void FCuentaCorrienteABM_Load(object sender, EventArgs e)
        {
          
        }
    }
}
