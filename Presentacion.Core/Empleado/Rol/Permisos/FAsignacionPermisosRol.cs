using Presentacion.FBase.Helpers;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.Helpers.Sistema.Rol;
using Servicios.LogicaNegocio.Empleado.Rol;
using Servicios.LogicaNegocio.Empleado.Rol.Tareas;
using Servicios.LogicaNegocio.Empleado.Rol.Tareas.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Empleado.Rol.Permisos
{
    public partial class FAsignacionPermisosRol : FBase.FBase
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
                Bool1 = false,
                Page = 1,
                PageSize = 10000
            };

            var roles = _rolServicio.ObtenerRoles(filtros).Items.ToList();

            _cargandoRol = true;
            CargarComboBox(cbxRol, roles, "Nombre", "RolId");

            if (rolId.HasValue)
            {
                cbxRol.SelectedValue = rolId.Value;
                cbxRol.Enabled = false;
            }
            else if (cbxRol.SelectedValue != null && cbxRol.SelectedValue != DBNull.Value)
            {
                RolId = Convert.ToInt64(cbxRol.SelectedValue);
            }

            _cargandoRol = false;

            InicializacionGrillas();
        }

        protected override void AplicarTema(Control parent)
        {
            base.AplicarTema(parent);

            if (ReferenceEquals(parent, this))
                AplicarEstiloAsignacionPermisos();
        }

        private void AplicarEstiloAsignacionPermisos()
        {
            BackColor = TemaSistema.Fondo;
            ForeColor = TemaSistema.Texto;

            EstilarTitulo(lblTitulo);
            EstilarLabel(lblRol);
            EstilarLabel(lblBuscar);
            EstilarLabel(lblDisponibles);
            EstilarLabel(lblAsignados);

            if (txtBuscar != null)
            {
                txtBuscar.BackColor = TemaSistema.FondoControl;
                txtBuscar.ForeColor = TemaSistema.Texto;
                txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            }

            EstilarBotonPrimario(btnActualizar);
            EstilarBotonPrimario(btnAsignarPermisos);
            EstilarBotonSecundario(btnQuitarPersmisos);
            EstilarBotonSecundario(btnSalir);

            foreach (var grid in new[] { dgvPermisosDisponibles, dgvPermisosAsignadas })
            {
                if (grid == null) continue;
                // FBase.ConfigurarGrilla pone BorderStyle.None; lo reaplicamos para el marco inferior.
                grid.BorderStyle = BorderStyle.FixedSingle;
                grid.BackgroundColor = TemaSistema.FondoControl;
            }

            CentrarBotonesMedio();
            ActualizarContadores();
        }

        private void CentrarBotonesMedio()
        {
            if (pnlBotonesMedio == null)
                return;

            void Centrar(Button btn, int offsetY)
            {
                if (btn == null) return;
                btn.Left = Math.Max(0, (pnlBotonesMedio.ClientSize.Width - btn.Width) / 2);
                btn.Top = Math.Max(0, (pnlBotonesMedio.ClientSize.Height / 2) + offsetY - (btn.Height / 2));
            }

            Centrar(btnAsignarPermisos, -40);
            Centrar(btnQuitarPersmisos, 40);
        }

        private static void EstilarTitulo(Label lbl)
        {
            if (lbl == null) return;
            lbl.ForeColor = TemaSistema.Primario;
            lbl.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        }

        private static void EstilarLabel(Label lbl)
        {
            if (lbl == null) return;
            lbl.ForeColor = TemaSistema.Texto;
            lbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        private static void EstilarBotonPrimario(Button btn)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = TemaSistema.Primario;
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }

        private static void EstilarBotonSecundario(Button btn)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = TemaSistema.Borde;
            btn.BackColor = TemaSistema.Seleccion;
            btn.ForeColor = TemaSistema.Texto;
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }

        private void ConfigurarGrillas()
        {
            foreach (var grid in new[] { dgvPermisosDisponibles, dgvPermisosAsignadas })
            {
                ConfigurarGrillaPermisos(grid);
            }

            dgvPermisosDisponibles.DataSource = _bsPermisosDisponibles;
            dgvPermisosAsignadas.DataSource = _bsPermisosAsignadas;

            pnlBotonesMedio.Resize += (_, __) => CentrarBotonesMedio();
        }

        private static void ConfigurarGrillaPermisos(DataGridView grid)
        {
            grid.AutoGenerateColumns = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.ReadOnly = true;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.RowHeadersVisible = false;
            grid.BorderStyle = BorderStyle.FixedSingle;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            grid.Columns.Clear();
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(PermisoDTO.PermisoId),
                DataPropertyName = nameof(PermisoDTO.PermisoId),
                HeaderText = "Id Permiso",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 72,
                FillWeight = 1
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(PermisoDTO.Codigo),
                DataPropertyName = nameof(PermisoDTO.Codigo),
                HeaderText = "Código",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 28
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(PermisoDTO.Descripcion),
                DataPropertyName = nameof(PermisoDTO.Descripcion),
                HeaderText = "Descripción",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 72
            });
        }

        private void CargarComboBox(ComboBox cmb, object datos, string display, string value)
        {
            cmb.DisplayMember = display;
            cmb.ValueMember = value;
            cmb.DataSource = datos;
        }

        private void cbxRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoRol) return;
            if (cbxRol.SelectedValue == null || cbxRol.SelectedValue == DBNull.Value) return;

            RolId = Convert.ToInt64(cbxRol.SelectedValue);
            InicializacionGrillas();
        }

        private void InicializacionGrillas()
        {
            var filtros = new FiltroConsulta
            {
                TextoBuscar = "",
                Bool1 = false,
                Page = 1,
                PageSize = 10000
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

            RefrescarVistas();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            RefrescarVistas();
        }

        private void RefrescarVistas()
        {
            if (_permisosDisponibles == null || _permisosAsignados == null)
                return;

            string texto = (txtBuscar?.Text ?? string.Empty).Trim();

            _bsPermisosDisponibles.DataSource = new BindingList<PermisoDTO>(
                FiltrarPermisos(_permisosDisponibles, texto).ToList());
            _bsPermisosAsignadas.DataSource = new BindingList<PermisoDTO>(
                FiltrarPermisos(_permisosAsignados, texto).ToList());

            _bsPermisosDisponibles.ResetBindings(false);
            _bsPermisosAsignadas.ResetBindings(false);

            ActualizarContadores();
        }

        private static IEnumerable<PermisoDTO> FiltrarPermisos(IEnumerable<PermisoDTO> origen, string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return origen;

            texto = texto.Trim();
            return origen.Where(p =>
                (!string.IsNullOrEmpty(p.Codigo) &&
                 p.Codigo.Contains(texto, StringComparison.OrdinalIgnoreCase))
                ||
                (!string.IsNullOrEmpty(p.Descripcion) &&
                 p.Descripcion.Contains(texto, StringComparison.OrdinalIgnoreCase)));
        }

        private void ActualizarContadores()
        {
            int dispTotal = _permisosDisponibles?.Count ?? 0;
            int asigTotal = _permisosAsignados?.Count ?? 0;
            int dispVista = (_bsPermisosDisponibles.DataSource as BindingList<PermisoDTO>)?.Count ?? 0;
            int asigVista = (_bsPermisosAsignadas.DataSource as BindingList<PermisoDTO>)?.Count ?? 0;

            bool filtrando = !string.IsNullOrWhiteSpace(txtBuscar?.Text);

            if (lblDisponibles != null)
            {
                lblDisponibles.Text = filtrando
                    ? $"Permisos disponibles ({dispVista}/{dispTotal})"
                    : $"Permisos disponibles ({dispTotal})";
            }

            if (lblAsignados != null)
            {
                lblAsignados.Text = filtrando
                    ? $"Permisos asignados ({asigVista}/{asigTotal})"
                    : $"Permisos asignados ({asigTotal})";
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            MoverPermiso(desdeDisponibles: true);
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            MoverPermiso(desdeDisponibles: false);
        }

        private void dgvPermisosDisponibles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            MoverPermiso(desdeDisponibles: true);
        }

        private void dgvPermisosAsignadas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            MoverPermiso(desdeDisponibles: false);
        }

        private void MoverPermiso(bool desdeDisponibles)
        {
            if (_permisosDisponibles == null || _permisosAsignados == null)
            {
                MessageBox.Show("No se pudieron cargar los permisos. Reabrí la ventana.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var seleccionado = ObtenerDeGrilla(
                desdeDisponibles ? dgvPermisosDisponibles : dgvPermisosAsignadas);
            if (seleccionado == null) return;

            var origen = desdeDisponibles ? _permisosDisponibles : _permisosAsignados;
            var destino = desdeDisponibles ? _permisosAsignados : _permisosDisponibles;

            var master = origen.FirstOrDefault(p => p.PermisoId == seleccionado.PermisoId);
            if (master == null) return;

            origen.Remove(master);
            destino.Add(master);
            RefrescarVistas();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!RolId.HasValue)
            {
                MessageBox.Show("No hay rol seleccionado");
                return;
            }

            if (_permisosControl == null || _permisosAsignados == null)
            {
                MessageBox.Show("No se pudieron cargar los permisos. Reabrí la ventana.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool sonIguales = _permisosControl.Select(t => t.PermisoId).OrderBy(x => x)
                .SequenceEqual(_permisosAsignados.Select(t => t.PermisoId).OrderBy(x => x));

            if (sonIguales)
            {
                MessageBox.Show("Sin cambios");
                return;
            }

            bool contieneAdmin = _permisosAsignados
                .Any(p => p.Codigo != null && p.Codigo.StartsWith("Admin."));

            if (contieneAdmin && (AuthHelper.UsuarioActual == null || !AuthHelper.UsuarioActual.EsSuperAdmin))
            {
                MessageBox.Show("Solo un Super Administrador puede asignar permisos de administración",
                    "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                _permisosAsignados.Clear();
                foreach (var p in _permisosControl)
                    _permisosAsignados.Add(p);

                RefrescarVistas();
                return;
            }

            var response = _permisoServicio.ActualizarPermisosDeRol(_permisosAsignados.ToList(), RolId.Value);

            if (response != null && response.Exitoso)
            {
                MessageBox.Show(string.IsNullOrWhiteSpace(response.Mensaje)
                        ? "Permisos actualizados correctamente"
                        : response.Mensaje,
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                _permisosControl = new BindingList<PermisoDTO>(
                    _permisosAsignados.Select(t => new PermisoDTO
                    {
                        PermisoId = t.PermisoId,
                        Codigo = t.Codigo,
                        Descripcion = t.Descripcion
                    }).ToList());
            }
            else
            {
                MessageBox.Show(response?.Mensaje ?? "No se pudieron actualizar los permisos.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private PermisoDTO ObtenerDeGrilla(DataGridView grilla)
        {
            if (grilla.SelectedRows.Count == 0)
                return null;

            return grilla.SelectedRows[0].DataBoundItem as PermisoDTO;
        }

    }
}
