using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.Empleado.Rol;
using Servicios.LogicaNegocio.Empleado.Rol.DTO;
using System;
using System.Linq;
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

        private List<RolDTO> _rolesDisponibles;
        private List<RolDTO> _rolesAsignados;
        private List<RolDTO> _rolesControl;
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

            var filtros = new FiltroConsulta
            {
                TextoBuscar = string.Empty,
                VerEliminados = false,
                Page = 1,
                PageSize = int.MaxValue // o un número alto si no querés romper memoria
            };

            var resultado = _empleadoServicio.ObtenerEmpleados(filtros);

            var empleados = resultado.Items;
            CargarComboBox(cbxEmpleado, empleados, "Nombre", "PersonaId");
            EntidadID = cbxEmpleado.SelectedValue as long?;

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
        public void CargarComboBox(ComboBox cmb, object datos, string propiedadMostrar, string propiedadDevolver)
        {
            cmb.DataSource = datos;
            cmb.DisplayMember = propiedadMostrar;
            cmb.ValueMember = propiedadDevolver;
        }

        private void FAsignacionRolesEmpleados_Load(object sender, EventArgs e)
        {
            InicializacionGrillas();
        }

        private void ActualizarGrillas()
        {

            dgvRolesDisponibles.DataSource = null;
            dgvRolesDisponibles.DataSource = _rolesDisponibles;

            dgvRolesAsignados.DataSource = null;
            dgvRolesAsignados.DataSource = _rolesAsignados;
        }

        private void ResetearGrillas(DataGridView grillaRolesDisponibles, DataGridView grillaRolesAsignados)
        {
            // 🔹 DISPONIBLES
            if (grillaRolesDisponibles.Columns.Count > 0)
            {
                if (grillaRolesDisponibles.Columns.Contains("RolId"))
                {
                    grillaRolesDisponibles.Columns["RolId"].Visible = false;
                    grillaRolesDisponibles.Columns["RolId"].Name = "Id";
                }

                if (grillaRolesDisponibles.Columns.Contains("Nombre"))
                {
                    grillaRolesDisponibles.Columns["Nombre"].Visible = true;
                    grillaRolesDisponibles.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                if (grillaRolesDisponibles.Columns.Contains("CodigoRol"))
                {
                    grillaRolesDisponibles.Columns["CodigoRol"].Visible = true;
                    grillaRolesDisponibles.Columns["CodigoRol"].Width = 100;
                    grillaRolesDisponibles.Columns["CodigoRol"].HeaderText = "Código";
                }

                if (grillaRolesDisponibles.Columns.Contains("DetalleRol"))
                {
                    grillaRolesDisponibles.Columns["DetalleRol"].Visible = false;
                }
            }

            // 🔹 ASIGNADOS
            if (!EntidadID.HasValue) return;

            if (grillaRolesAsignados.Columns.Count > 0)
            {
                if (grillaRolesAsignados.Columns.Contains("RolId"))
                {
                    grillaRolesAsignados.Columns["RolId"].Visible = false;
                    grillaRolesAsignados.Columns["RolId"].Name = "Id";
                }

                if (grillaRolesAsignados.Columns.Contains("Nombre"))
                {
                    grillaRolesAsignados.Columns["Nombre"].Visible = true;
                    grillaRolesAsignados.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                if (grillaRolesAsignados.Columns.Contains("CodigoRol"))
                {
                    grillaRolesAsignados.Columns["CodigoRol"].Visible = true;
                    grillaRolesAsignados.Columns["CodigoRol"].Width = 100;
                    grillaRolesAsignados.Columns["CodigoRol"].HeaderText = "Código";
                }

                if (grillaRolesAsignados.Columns.Contains("DetalleRol"))
                {
                    grillaRolesAsignados.Columns["DetalleRol"].Visible = false;
                }
            }
        }

        private void btnAsignarRol_Click(object sender, EventArgs e)
        {
            var empleado = _empleadoServicio.ObtenerEmpleadoPorId((long)EntidadID);
            var rolDisponibleSeleccionado = ObtenerRolDeGrilla(dgvRolesDisponibles);
            if (rolDisponibleSeleccionado == null)
            {
                MessageBox.Show("Por favor seleccione un rol disponible para asignar.", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            var respuesta = MessageBox.Show($"¿Esta seguro que desea asignar el rol {rolDisponibleSeleccionado.Nombre.ToUpper()} del empleado {empleado.Nombre} {empleado.Apellido} (usuario: {empleado.Username})?",
            "Confirmar acción",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {

                _rolesDisponibles.Remove(rolDisponibleSeleccionado);
                _rolesAsignados.Add(rolDisponibleSeleccionado);
                ActualizarGrillas();
                ResetearGrillas(dgvRolesDisponibles, dgvRolesAsignados);
            }
        }

        private RolDTO ObtenerRolDeGrilla(DataGridView grillaRoles)
        {
            if (grillaRoles.CurrentRow != null && grillaRoles.CurrentRow.DataBoundItem is RolDTO rol)
                return rol;

            return null;
        }

        private void btnQuitarRol_Click(object sender, EventArgs e)
        {
            var rolQuitableSeleccionado = ObtenerRolDeGrilla(dgvRolesAsignados);
            var empleado = _empleadoServicio.ObtenerEmpleadoPorId((long)EntidadID);
            if (rolQuitableSeleccionado == null)
            {
                MessageBox.Show("Por favor seleccione un rol disponible para quitar.", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            var respuesta = MessageBox.Show($"¿Esta seguro que desea quitar el rol {rolQuitableSeleccionado.Nombre.ToUpper()} del empleado {empleado.Nombre} {empleado.Apellido} (usuario: {empleado.Username})?",
            "Confirmar acción",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                _rolesAsignados.Remove(rolQuitableSeleccionado);
                _rolesDisponibles.Add(rolQuitableSeleccionado);
                ActualizarGrillas();
                ResetearGrillas(dgvRolesDisponibles, dgvRolesAsignados);
            }
        }

        private void btnActualizarRoles_Click(object sender, EventArgs e)
        {
            bool sonIguales = _rolesControl.Select(r => r.RolId).OrderBy(id => id)
     .SequenceEqual(_rolesAsignados.Select(r => r.RolId).OrderBy(id => id));
            var fechaAsignacionRol = DateTime.Now;
            if (sonIguales)
            {
                MessageBox.Show("Al no haber cambios, no se han realizado cambios en los roles asignados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (EntidadID.HasValue)
                {
                    var response = _rolServicio.ActualizarRolesDeEmpleado(_rolesAsignados, (long)EntidadID, fechaAsignacionRol);
                    if (response.Exitoso)
                    {
                        RealizoAlgunaOperacion = true;
                        MessageBox.Show(response.Mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(response.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Surgió un problema al obtener el empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


        }

        private void cbxEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntidadID = cbxEmpleado.SelectedValue as long?;
            InicializacionGrillas();
        }

        private void InicializacionGrillas()
        {
            var filtros = new FiltroConsulta
            {
                TextoBuscar = string.Empty,
                VerEliminados = false,
                Page = 1,
                PageSize = int.MaxValue // solo si lo usás para listas chicas
            };

            var resultado = _rolServicio.ObtenerRoles(filtros);

            _rolesDisponibles = resultado.Items.ToList();
            if (EntidadID.HasValue)
            {
                _rolesAsignados = _rolServicio.ObtenerRolesAsignadosAEmpleados(EntidadID.Value).ToList();
                _rolesDisponibles.RemoveAll(rd => _rolesAsignados.Any(ra => ra.RolId == rd.RolId));
                _rolesControl = _rolesAsignados
                .Select(r => new RolDTO
                {
                    RolId = r.RolId,
                    Nombre = r.Nombre,
                    CodigoRol = r.CodigoRol,
                    DetalleRol = r.DetalleRol
                })
                .ToList();
            }
            ActualizarGrillas();
            ResetearGrillas(dgvRolesDisponibles, dgvRolesAsignados);
        }
    }
}