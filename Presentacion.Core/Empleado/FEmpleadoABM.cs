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
