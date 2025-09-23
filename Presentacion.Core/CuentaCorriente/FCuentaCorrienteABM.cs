using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
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

        public override void FBaseABM_Load(object sender, EventArgs e)
        {
            base.FBaseABM_Load(sender, e);
            Inicializador(EntidadID);
        }
        public FCuentaCorrienteABM(TipoOperacion tipoOperacion, long? entidadID = null) : base(tipoOperacion, entidadID)
        {
            InitializeComponent();
            _cuentacorrienteServicio = new CuentaCorrienteServicio();

            if (tipoOperacion == TipoOperacion.Eliminar || tipoOperacion == TipoOperacion.Modificar)
            {
                CargarDatos(entidadID);
            }

            if (tipoOperacion == TipoOperacion.Eliminar)
            {
                DesactivarControles(this);
            }

            dtpFechaVencimiento.MinDate = DateTime.Now;

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

            // Datos Personales

            txtNombreCC.Text = cuentacorriente.NombreCuentaCorriente;
            txtSaldo.Text = cuentacorriente.Saldo.ToString(); // FIX: Assign the value as string
            dtpFechaVencimiento.Value = (DateTime)cuentacorriente.FechaVencimiento;
            chkbLimiteDeuda.Checked = cuentacorriente.LimiteDeudaActivo;
            txtLimiteDeuda.Text = cuentacorriente.LimiteDeuda.ToString();

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
                LimiteDeudaActivo = chkbLimiteDeuda.Checked,
                LimiteDeuda = Convert.ToDecimal(txtLimiteDeuda.Text),
                DniAutorizados = dgvDni.Rows.Cast<DataGridViewRow>()
                                  .Select(r => Convert.ToInt64(r.Cells["DNI"].Value))
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
                    LimiteDeudaActivo = chkbLimiteDeuda.Checked,
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
    }
}
