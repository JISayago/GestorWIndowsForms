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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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

            // Datos Personales
            txtApellido.Text = empleado.Apellido;
            txtNombre.Text = empleado.Nombre;
            txtLegajo.Text = empleado.Legajo;
            txtDireccion.Text = empleado.Direccion;
            txtCelular.Text = empleado.Telefono2;
            txtCuil.Text = empleado.Cuil;
            txtDni.Text = empleado.Dni;
            txtEmail.Text = empleado.Email;
            dtpFNacimiento.Value = (DateTime)empleado.FechaNacimiento;
            txtTelefono.Text = empleado.Telefono;
            dtpFIngreso.Value = empleado.FechaIngreso;
            if (entidadId.HasValue) { MessageBox.Show($"FI:{empleado.FechaIngreso}, FN: {empleado.FechaNacimiento}");}
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
                FechaIngreso = dtpFIngreso.Value
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

        public override bool EjecutarComandoEliminar()
        {
            if (!EntidadID.HasValue)
            {
                MessageBox.Show(@"´Por favor seleccione un empleado válido.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;

            }
            if (TipoOperacion == TipoOperacion.Eliminar)
            {
               var response = _empleadoServicio.Eliminar((long)EntidadID);
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
                MessageBox.Show(@"´Por favor seleccione un empleado válido.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;

            }
            if (TipoOperacion == TipoOperacion.Modificar)
            {

                var empleadoEditar = new EmpleadoDTO
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
                    FechaIngreso = dtpFIngreso.Value
                };

                    var response = _empleadoServicio.Modificar(empleadoEditar,EntidadID);

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
