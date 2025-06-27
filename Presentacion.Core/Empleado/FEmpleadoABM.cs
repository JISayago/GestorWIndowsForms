using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.Empleado.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Presentacion.Core.Empleado
{
    public partial class FEmpleadoABM : FBaseABM
    {
        private readonly IEmpleadoServicio _empleadoServicio;

        public override void FBaseABM_Load(object sender, EventArgs e)
        {
            base.FBaseABM_Load(sender, e);
            Inicializador(EntidadID);
        }
        public FEmpleadoABM(TipoOperacion tipoOperacion, long? entidadID = null) :base(tipoOperacion,entidadID)
        {
            InitializeComponent();
            _empleadoServicio = new EmpleadoServicio();

            dtpFIngreso.MaxDate = DateTime.Now.AddDays(1);
            dtpFNacimiento.MaxDate = DateTime.Now.AddDays(1);

            if (tipoOperacion == TipoOperacion.Eliminar || tipoOperacion == TipoOperacion.Modificar)
            {
                CargarDatos(entidadID);
            }

            if (tipoOperacion == TipoOperacion.Eliminar)
            {
                DesactivarControles(this);
            }

            AgregarControlesObligatorios(txtApellido, "Apellido");
            AgregarControlesObligatorios(txtNombre, "Nombre");
            AgregarControlesObligatorios(txtLegajo, "Legajo");
            AgregarControlesObligatorios(txtDni, "Dni");
            AgregarControlesObligatorios(txtTelefono, "Telefono");
            AgregarControlesObligatorios(txtCuil, "Cuil");
            AgregarControlesObligatorios(txtEmail, "Email");
            AgregarControlesObligatorios(txtDireccion, "Direccion");
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

            var empleado = _empleadoServicio.ObtenerEmpleadoPorId(entidadId.Value);

            MessageBox.Show($"{empleado.Nombre}");


            /*
            // Datos Personales
            nudLegajo.Minimum = 1;
            nudLegajo.Maximum = 9999999999;
            nudLegajo.Value = empleado.Legajo;

            txtApellido.Text = empleado.Apellido;
            txtNombre.Text = empleado.Nombre;
            txtDni.Text = empleado.Dni;
            txtTelefono.Text = empleado.Telefono;
            txtCelular.Text = empleado.Celular;
            txtEmail.Text = empleado.Email;
            txtCuil.Text = empleado.Cuil;
            dtpFechaNacimiento.Value = empleado.FechaNacimiento;
            imgFotoEmpleado.Image = Convertir_Bytes_Imagen(empleado.Foto);

            // Datos Direccion
            txtCalle.Text = empleado.Calle;
            txtNumero.Text = empleado.Numero.ToString();
            txtPiso.Text = empleado.Piso;
            txtDepartamento.Text = empleado.Dpto;
            txtCasa.Text = empleado.Casa;
            txtLote.Text = empleado.Lote;
            txtManzana.Text = empleado.Mza;
            txtBarrio.Text = empleado.Barrio;

            CargarComboBox(cmbProvincia, _provinciaServicio.ObtenerProvincia(string.Empty), "Descripcion", "Id");

            cmbProvincia.SelectedItem = empleado.ProvinciaId;

            if (cmbProvincia.Items.Count > 0)
            {
                CargarComboBox(cmbLocalidad, _localidadServicio.ObtenerLocalidadPorProvincia(empleado.ProvinciaId, string.Empty), "Descripcion", "Id");
            }
            */
        }


        public override bool EjecutarComandoNuevo()
        {

            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

           var nuevoEmpleado = new EmpleadoDTO
            {
                Apellido = txtApellido.Text,
                Nombre = txtNombre.Text,
                Legajo = txtLegajo.Text,
                Direccion = txtDireccion.Text,
                Telefono = txtCelular.Text,
                Cuil = txtCuil.Text,
                Dni = txtDni.Text,
                Email = txtEmail.Text,
                FechaNacimiento = dtpFNacimiento.Value,
                Telefono2 = txtCelular.Text,
                EstaEliminado = false,
                FechaIngreso = dtpFNacimiento.Value
            };

            var response = _empleadoServicio.Insertar(nuevoEmpleado);
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
    }
}
