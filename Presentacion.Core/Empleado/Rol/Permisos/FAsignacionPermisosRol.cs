using AccesoDatos.Entidades;
using Presentacion.FBase.Helpers;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Articulo.Marca;
using Servicios.LogicaNegocio.Articulo.Marca.DTO;
using Servicios.LogicaNegocio.Empleado.Rol;
using Servicios.LogicaNegocio.Empleado.Rol.DTO;
using Servicios.LogicaNegocio.Empleado.Rol.Tareas;
using Servicios.LogicaNegocio.Empleado.Rol.Tareas.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Empleado.Rol.Permisos
{
    public partial class FAsignacionPermisosRol : Form
    {
        private readonly IRolServicio _rolServicio;
        private readonly IPermisoServicio _permisoServicio;

        private BindingList<PermisoDTO> _permisosDisponibles;
        private BindingList<PermisoDTO> _permisosAsignados;
        private BindingList<PermisoDTO> _permisosControl;

        private readonly BindingSource _bsPermisosDisponibles = new BindingSource();
        private readonly BindingSource _bsPermisosAsignadas = new BindingSource();

        private long? RolId;
        private bool _cargandoRol;

        public FAsignacionPermisosRol()
        {
            InitializeComponent();
        }

        public FAsignacionPermisosRol(long? rolId = null) : this()
        {

            _rolServicio = new RolServicio();  
            _permisoServicio = new PermisoServicio();
            RolId = rolId;

            ConfigurarGrillas();

            var filtros = new FiltroConsulta
            {
                TextoBuscar = "",
                VerEliminados = false,
                Page = 1,
                PageSize = int.MaxValue
            };

            var roles = _rolServicio.ObtenerRoles(filtros).Items.ToList();

            _cargandoRol = true;
            CargarComboBox(cbxRol, roles, "Nombre", "RolId");
            _cargandoRol = false;
            if (rolId.HasValue)
            {
                cbxRol.SelectedValue = rolId.Value;
            }

            _cargandoRol = false;

            InicializacionGrillas();


        }

        private void ConfigurarGrillas()
        {
            dgvPermisosDisponibles.AutoGenerateColumns = true;
            dgvPermisosAsignadas.AutoGenerateColumns = true;

            dgvPermisosDisponibles.DataSource = _bsPermisosDisponibles;
            dgvPermisosAsignadas.DataSource = _bsPermisosAsignadas;
        }

        private void CargarComboBox(ComboBox cmb, object datos, string display, string value)
        {
            cmb.DataSource = datos;
            cmb.DisplayMember = display;
            cmb.ValueMember = value;
        }

        private void cbxRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoRol) return;
            if (cbxRol.SelectedValue == null) return;

            RolId = Convert.ToInt64(cbxRol.SelectedValue);
            InicializacionGrillas();
        }

        private void InicializacionGrillas()
        {
            var filtros = new FiltroConsulta
            {
                TextoBuscar = "",
                VerEliminados = false,
                Page = 1,
                PageSize = int.MaxValue
            };

            var todosPermisos = _permisoServicio.ObtenerPermisos(filtros);

            var permisosAsignados = new List<PermisoDTO>();

            if (RolId.HasValue)
            {
                permisosAsignados = _permisoServicio
                    .ObtenerPermisosAsignadosARol(RolId.Value)
                    .ToList();
            }

            var idsAsignados = new HashSet<long>(permisosAsignados.Select(t => t.PermisoId));

            _permisosAsignados = new BindingList<PermisoDTO>(permisosAsignados);

            _permisosDisponibles = new BindingList<PermisoDTO>(
                todosPermisos.Where(t => !idsAsignados.Contains(t.PermisoId)).ToList()
            );

            _permisosControl = new BindingList<PermisoDTO>(
                permisosAsignados.Select(t => new PermisoDTO
                {
                    PermisoId = t.PermisoId,
                    Codigo = t.Codigo,
                    Descripcion = t.Descripcion
                }).ToList()
            );

            _bsPermisosDisponibles.DataSource = _permisosDisponibles;
            _bsPermisosAsignadas.DataSource = _permisosAsignados;

            _bsPermisosDisponibles.ResetBindings(false);
            _bsPermisosAsignadas.ResetBindings(false);

            AjustarColumnas();
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            var tarea = ObtenerDeGrilla(dgvPermisosDisponibles);
            if (tarea == null) return;

            _permisosDisponibles.Remove(tarea);
            _permisosAsignados.Add(tarea);

            _bsPermisosDisponibles.ResetBindings(false);
            _bsPermisosAsignadas.ResetBindings(false);
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            var tarea = ObtenerDeGrilla(dgvPermisosAsignadas);
            if (tarea == null) return;
            _permisosAsignados.Remove(tarea);
            _permisosDisponibles.Add(tarea);

            _bsPermisosDisponibles.ResetBindings(false);
            _bsPermisosAsignadas.ResetBindings(false);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!RolId.HasValue)
            {
                MessageBox.Show("No hay rol seleccionado");
                return;
            }

            bool sonIguales = _permisosControl.Select(t => t.PermisoId).OrderBy(x => x)
                .SequenceEqual(_permisosAsignados.Select(t => t.PermisoId).OrderBy(x => x));

            if (sonIguales)
            {
                MessageBox.Show("Sin cambios");
                return;
            }

            _permisoServicio.ActualizarPermisosDeRol(_permisosAsignados.ToList(), RolId.Value);

            MessageBox.Show("Permisos actualizados correctamente");
        }

        private PermisoDTO ObtenerDeGrilla(DataGridView grilla)
        {
            if (grilla.SelectedRows.Count == 0)
                return null;

            return grilla.SelectedRows[0].DataBoundItem as PermisoDTO;
        }

        private void AjustarColumnas()
        {
            AjustarGrilla(dgvPermisosDisponibles);
            AjustarGrilla(dgvPermisosAsignadas);
        }

        private void AjustarGrilla(DataGridView grid)
        {
            if (grid.Columns.Contains(nameof(PermisoDTO.PermisoId)))
                grid.Columns[nameof(PermisoDTO.PermisoId)].Visible = false;

            if (grid.Columns.Contains(nameof(PermisoDTO.Descripcion)))
                grid.Columns[nameof(PermisoDTO.Descripcion)].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (grid.Columns.Contains(nameof(PermisoDTO.Codigo)))
            {
                grid.Columns[nameof(PermisoDTO.Codigo)].Width = 100;
                grid.Columns[nameof(PermisoDTO.Codigo)].HeaderText = "Código";
            }
        }
    }
}