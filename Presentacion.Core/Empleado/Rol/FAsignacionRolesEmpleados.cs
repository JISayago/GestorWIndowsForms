using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.Empleado.Rol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Empleado.Rol
{
    public partial class FAsignacionRolesEmpleados : FBase.FBase
    {
        protected TipoAsignacionRol TipoAsignacionRol;
        protected long? EntidadID;
        public bool RealizoAlgunaOperacion;

        private readonly IRolServicio _rolServicio;
        private readonly IEmpleadoServicio _empleadoServicio;

        public FAsignacionRolesEmpleados()
        {
            InitializeComponent();
        }
        public FAsignacionRolesEmpleados(TipoAsignacionRol tipoAsignacionRol, long? entidadID = null) : this()
        {
            _rolServicio = new RolServicio();
            _empleadoServicio = new EmpleadoServicio();
            TipoAsignacionRol = tipoAsignacionRol;
            EntidadID = entidadID;
            RealizoAlgunaOperacion = false;

            var empleados = _empleadoServicio.ObtenerEmpleados(string.Empty);
            CargarComboBox(cbxEmpleado, empleados, "Nombre", "PersonaId");

            if (tipoAsignacionRol == TipoAsignacionRol.Existente)
            {
                CargarDatos(entidadID);

                if (entidadID.HasValue && empleados.Any(e => e.PersonaId == entidadID.Value))
                {
                    cbxEmpleado.SelectedValue = entidadID.Value;
                    cbxEmpleado.Enabled = false;
                }
            }
        }


        private void CargarDatos(long? entidadId)
        {
            if (!entidadId.HasValue)
            {
                MessageBox.Show(@"Por favor seleccione un usuario válido", @"Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            if (entidadId.HasValue)
            {



                var empleado = _empleadoServicio.ObtenerEmpleadoPorId(entidadId.Value);

                // Datos Personales
                //txtNombre.Text = rol.Nombre;
                //txtCodigoRol.Text = Convert.ToString(rol.CodigoRol);
                //txtDescripcionRol.Text = Convert.ToString(rol.DetalleRol);
                MessageBox.Show($"Cargar Datos de Empelado {empleado.Apellido}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
        public  void CargarComboBox(ComboBox cmb, object datos, string propiedadMostrar, string propiedadDevolver)
        {
            cmb.DataSource = datos;
            cmb.DisplayMember = propiedadMostrar;
            cmb.ValueMember = propiedadDevolver;
        }
    }
}
