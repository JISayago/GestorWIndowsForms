using AccesoDatos.Entidades;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.Helpers.Sistema.Rol;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.Empleado.Rol;
using Servicios.LogicaNegocio.Empleado.Rol.DTO;
using Servicios.LogicaNegocio.Empleado.Rol.Tareas;
using System;
using System.Collections;
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
        private readonly IPermisoServicio _permisoServicio;

        private BindingList<RolDTO> _rolesDisponibles;
        private BindingList<RolDTO> _rolesAsignados;
        private BindingList<RolDTO> _rolesControl;

        private readonly BindingSource _bsRolesDisponibles = new BindingSource();
        private readonly BindingSource _bsRolesAsignados = new BindingSource();

        private bool _cargandoEmpleado;
        public FAsignacionRolesEmpleados()
        {
            InitializeComponent();
        }
        public FAsignacionRolesEmpleados(TipoAsignacionRol tipoAsignacionRol, long? entidadID = null) : this()
        {
            _rolServicio = new RolServicio();
            _empleadoServicio = new EmpleadoServicio();
            _permisoServicio = new PermisoServicio();

            TipoAsignacionRol = tipoAsignacionRol;
            EntidadID = entidadID;
            RealizoAlgunaOperacion = false;

            ConfigurarGrillas();

            var filtros = new FiltroConsulta
            {
                TextoBuscar = string.Empty,
                VerEliminados = false,
                Page = 1,
                PageSize = int.MaxValue
            };

            var resultado = _empleadoServicio.ObtenerEmpleados(filtros);
            var empleados = resultado.Items.ToList();

            _cargandoEmpleado = true;
            CargarComboBox(cbxEmpleado, empleados, "Nombre", "PersonaId");
            _cargandoEmpleado = false;

            if (tipoAsignacionRol == TipoAsignacionRol.Existente && entidadID.HasValue)
            {
                cbxEmpleado.SelectedValue = entidadID.Value;
                cbxEmpleado.Enabled = false;
                EntidadID = entidadID;
            }

            InicializacionGrillas();
        }

        private void ConfigurarGrillas()
        {
            dgvRolesDisponibles.AutoGenerateColumns = true;
            dgvRolesAsignados.AutoGenerateColumns = true;

            dgvRolesDisponibles.DataSource = _bsRolesDisponibles;
            dgvRolesAsignados.DataSource = _bsRolesAsignados;
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
            if (!EntidadID.HasValue)
            {
                MessageBox.Show("No hay empleado seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var empleado = _empleadoServicio.ObtenerEmpleadoPorId(EntidadID.Value);
            var rolDisponibleSeleccionado = ObtenerRolDeGrilla(dgvRolesDisponibles);

            if (rolDisponibleSeleccionado == null)
            {
                MessageBox.Show("Por favor seleccione un rol disponible para asignar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // 🔥 1. Validación por rol SADMIN
            bool esRolSAdmin = rolDisponibleSeleccionado.CodigoRol == "SADMIN";

            // 🔥 2. Validación por permisos del rol (Admin.*)
            var permisosDelRol = _permisoServicio.ObtenerPermisosAsignadosARol(rolDisponibleSeleccionado.RolId);

            bool tienePermisoAdmin = permisosDelRol
                .Any(p => p.Codigo.StartsWith("Admin."));

            // 🔥 Validación final
            if ((esRolSAdmin || tienePermisoAdmin) && !AuthHelper.UsuarioActual.EsSuperAdmin)
            {
                MessageBox.Show("Solo un Super Administrador puede asignar roles con permisos de administración",
                    "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var respuesta = MessageBox.Show(
                $"¿Está seguro que desea asignar el rol {rolDisponibleSeleccionado.Nombre.ToUpper()} del empleado {empleado.Nombre} {empleado.Apellido} (usuario: {empleado.Username})?",
                "Confirmar acción",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes) return;

            _rolesDisponibles.Remove(rolDisponibleSeleccionado);
            _rolesAsignados.Add(rolDisponibleSeleccionado);

            _bsRolesDisponibles.ResetBindings(false);
            _bsRolesAsignados.ResetBindings(false);
        }

        private RolDTO ObtenerRolDeGrilla(DataGridView grilla)
        {
            if (grilla.SelectedRows.Count == 0)
                return null;

            return grilla.SelectedRows[0].DataBoundItem as RolDTO;
        }

        private void btnQuitarRol_Click(object sender, EventArgs e)
        {
            if (!EntidadID.HasValue)
            {
                MessageBox.Show("No hay empleado seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var empleado = _empleadoServicio.ObtenerEmpleadoPorId(EntidadID.Value);
            var rolQuitableSeleccionado = ObtenerRolDeGrilla(dgvRolesAsignados);

            if (rolQuitableSeleccionado == null)
            {
                MessageBox.Show("Por favor seleccione un rol asignado para quitar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var respuesta = MessageBox.Show(
                $"¿Está seguro que desea quitar el rol {rolQuitableSeleccionado.Nombre.ToUpper()} del empleado {empleado.Nombre} {empleado.Apellido} (usuario: {empleado.Username})?",
                "Confirmar acción",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes) return;

            _rolesAsignados.Remove(rolQuitableSeleccionado);
            _rolesDisponibles.Add(rolQuitableSeleccionado);

            _bsRolesDisponibles.ResetBindings(false);
            _bsRolesAsignados.ResetBindings(false);
        }

        private void btnActualizarRoles_Click(object sender, EventArgs e)
        {
            bool sonIguales = _rolesControl.Select(r => r.RolId).OrderBy(id => id)
                .SequenceEqual(_rolesAsignados.Select(r => r.RolId).OrderBy(id => id));

            var fechaAsignacionRol = DateTime.Now;

            if (sonIguales)
            {
                MessageBox.Show("Al no haber cambios, no se han realizado cambios en los roles asignados.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 🔥 Validación pedida
            bool contieneSAdmin = _rolesAsignados
                .Any(r => r.CodigoRol == "SADMIN");

            if (contieneSAdmin && !AuthHelper.UsuarioActual.EsSuperAdmin)
            {
                MessageBox.Show("Solo un Super Administrador puede asignar el rol SADMIN",
                    "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (EntidadID.HasValue)
            {
                var response = _rolServicio.ActualizarRolesDeEmpleado(
                    _rolesAsignados.ToList(),
                    (long)EntidadID,
                    fechaAsignacionRol
                );

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

        //private void cbxEmpleado_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    if (_cargandoEmpleado) return;
        //    if (cbxEmpleado.SelectedValue == null) return;

        //    EntidadID = Convert.ToInt64(cbxEmpleado.SelectedValue);
        //    InicializacionGrillas();
        //}
        private void cbxEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoEmpleado) return;
            if (cbxEmpleado.SelectedValue == null) return;

            EntidadID = Convert.ToInt64(cbxEmpleado.SelectedValue);
            InicializacionGrillas();
        }

        private void InicializacionGrillas()
        {
            var filtros = new FiltroConsulta
            {
                TextoBuscar = string.Empty,
                VerEliminados = false,
                Page = 1,
                PageSize = int.MaxValue
            };

            var todosLosRoles = _rolServicio.ObtenerRoles(filtros).Items.ToList();

            var rolesAsignados = new List<RolDTO>();

            if (EntidadID.HasValue)
            {
                rolesAsignados = _rolServicio.ObtenerRolesAsignadosAEmpleados(EntidadID.Value).ToList();
            }

            var idsAsignados = new HashSet<long>(rolesAsignados.Select(r => r.RolId));

            _rolesAsignados = new BindingList<RolDTO>(rolesAsignados);
            _rolesDisponibles = new BindingList<RolDTO>(
                todosLosRoles.Where(r => !idsAsignados.Contains(r.RolId)).ToList()
            );

            _rolesControl = new BindingList<RolDTO>(
                rolesAsignados.Select(r => new RolDTO
                {
                    RolId = r.RolId,
                    Nombre = r.Nombre,
                    CodigoRol = r.CodigoRol,
                    DetalleRol = r.DetalleRol
                }).ToList()
            );

            _bsRolesDisponibles.DataSource = _rolesDisponibles;
            _bsRolesAsignados.DataSource = _rolesAsignados;

            _bsRolesDisponibles.ResetBindings(false);
            _bsRolesAsignados.ResetBindings(false);

            AjustarColumnas();
        }
        private void AjustarColumnas()
        {
            AjustarGrilla(dgvRolesDisponibles);
            AjustarGrilla(dgvRolesAsignados);
        }

        private void AjustarGrilla(DataGridView grid)
        {
            if (grid.Columns.Contains(nameof(RolDTO.RolId)))
            {
                grid.Columns[nameof(RolDTO.RolId)].Visible = false;
            }

            if (grid.Columns.Contains(nameof(RolDTO.Nombre)))
            {
                grid.Columns[nameof(RolDTO.Nombre)].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (grid.Columns.Contains(nameof(RolDTO.CodigoRol)))
            {
                grid.Columns[nameof(RolDTO.CodigoRol)].Width = 100;
                grid.Columns[nameof(RolDTO.CodigoRol)].HeaderText = "Código";
            }

            if (grid.Columns.Contains(nameof(RolDTO.DetalleRol)))
            {
                grid.Columns[nameof(RolDTO.DetalleRol)].Visible = false;
            }
        }
    }
}
