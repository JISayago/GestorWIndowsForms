using Presentacion.FBase.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.FBase
{
    public partial class FBaseConsulta : Form
    {
        protected long? entidadID;
        protected bool puedeEjecutarComando;

        public List<AccionGrid> AccionesPersonalizadas = new List<AccionGrid>();

        public FBaseConsulta()
        {
            InitializeComponent();

            btnImprimir.Visible = false;

            btnNuevo.Image = Constantes.Imagenes.ImgNuevo;
            btnModificar.Image = Constantes.Imagenes.ImgModificar;
            btnEliminar.Image = Constantes.Imagenes.ImgEliminar;
            btnImprimir.Image = Constantes.Imagenes.ImgImprimir;
            btnActualizar.Image = Constantes.Imagenes.ImgActualizar;
            btnSalir.Image = Constantes.Imagenes.ImgCerrar;

            entidadID = null;
            puedeEjecutarComando = false;

            dgvGrilla.RowEnter += DgvGrilla_RowEnter;
            dgvGrilla.CellDoubleClick += DgvGrilla_CellDoubleClick;
            dgvGrilla.CellClick += DgvGrilla_CellClick;
            dgvGrilla.MouseDown += DgvGrilla_MouseDown; // 🔥 CLICK DERECHO
        }

        #region LOAD

        private void FBaseConsulta_Load(object sender, EventArgs e)
        {
            EjecutarBtnEliminarLoad();
            ResetearGrilla(dgvGrilla);

            ConfigurarAccionesPersonalizadas();
            CrearBotonesPersonalizados();

            RefrescarGrilla();
        }

        #endregion

        #region BOTONES BASE

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            EjecutarBtnNuevo();
        }

        public virtual void EjecutarBtnNuevo() { }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            EjecutarBtnModificar();
        }

        public virtual void EjecutarBtnModificar()
        {
            if (!ValidarSeleccion()) return;
            puedeEjecutarComando = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EjecutarBtnEliminar();
        }

        public virtual void EjecutarBtnEliminar()
        {
            if (!ValidarSeleccion()) return;
            puedeEjecutarComando = true;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            RefrescarGrilla();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region BUSQUEDA

        public virtual void btnBuscar_Click_1(object sender, EventArgs e)
        {
            RefrescarGrilla();
        }

        private void cbxEstaEliminado_CheckedChanged(object sender, EventArgs e)
        {
            RefrescarGrilla();
        }

        #endregion

        #region FILTROS

        protected virtual FiltroConsulta ObtenerFiltros()
        {
            return new FiltroConsulta
            {
                TextoBuscar = txtBuscar.Text,
                VerEliminados = cbxEstaEliminado.Checked,
                FechaDesde = ObtenerFechaDesdeUI(),
                FechaHasta = ObtenerFechaHastaUI(),
                Extra = ObtenerFiltroExtraUI(),
                Extra2 = ObtenerComboOpcionalUI()
            };
        }

        protected void RefrescarGrilla()
        {
            var filtros = ObtenerFiltros();
            ActualizarDatos(dgvGrilla, filtros);
        }

        #endregion

        #region LECTURA FILTROS UI

        protected virtual DateTime? ObtenerFechaDesdeUI()
        {
            if (dtpDesde == null || !dtpDesde.Visible) return null;
            if (chkUsarFecha != null && !chkUsarFecha.Checked) return null;
            return dtpDesde.Value.Date;
        }

        protected virtual DateTime? ObtenerFechaHastaUI()
        {
            if (dtpHasta == null || !dtpHasta.Visible) return null;
            if (chkUsarFecha != null && !chkUsarFecha.Checked) return null;
            return dtpHasta.Value.Date;
        }

        protected virtual object ObtenerFiltroExtraUI()
        {
            if (cbxFiltroOpcional == null || !cbxFiltroOpcional.Visible) return null;
            if (cbxFiltroOpcional.SelectedValue == null) return null;
            return cbxFiltroOpcional.SelectedValue;
        }

        protected virtual object ObtenerComboOpcionalUI()
        {
            if (cbxFiltroExtraEstado == null || !cbxFiltroExtraEstado.Visible) return null;
            if (cbxFiltroExtraEstado.SelectedValue == null) return null;
            return cbxFiltroExtraEstado.SelectedValue;
        }

        #endregion

        #region CONFIG FILTROS UI

        protected void ActivarFiltroCombo(string label, object data, string display, string value)
        {
            pnlFiltrosAvanzados.Visible = true;

            lblFiltro.Text = label;
            lblFiltro.Visible = true;

            cbxFiltroOpcional.Visible = true;
            cbxFiltroOpcional.DataSource = data;
            cbxFiltroOpcional.DisplayMember = display;
            cbxFiltroOpcional.ValueMember = value;
            cbxFiltroOpcional.SelectedIndex = -1;
        }

        protected void ActivarComboOpcional(string label, object data, string display, string value)
        {
            pnlFiltrosAvanzados.Visible = true;

            if (cbxFiltroExtraEstado != null)
            {
                cbxFiltroExtraEstado.Visible = true;
                cbxFiltroExtraEstado.DataSource = data;
                cbxFiltroExtraEstado.DisplayMember = display;
                cbxFiltroExtraEstado.ValueMember = value;
                cbxFiltroExtraEstado.SelectedIndex = -1;
            }
        }

        #endregion

        #region DATOS

        public virtual void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            if (filtros.VerEliminados)
            {
                btnEliminar.Enabled = false;
                btnNuevo.Enabled = false;
                btnModificar.Enabled = false;
            }
            else
            {
                btnEliminar.Enabled = true;
                btnNuevo.Enabled = true;
                btnModificar.Enabled = true;
            }

            EvaluarAccionesPorEstado(filtros);
            BarraLateralBotones.Enabled = AccionesPersonalizadas.Count > 0;
        }

        public virtual void ResetearGrilla(DataGridView grilla)
        {
            for (int i = 0; i < grilla.ColumnCount; i++)
                grilla.Columns[i].Visible = false;
        }

        public virtual void EjecutarBtnEliminarLoad()
        {
            RefrescarGrilla();
        }

        #endregion

        #region SELECCION FILA

        private void DgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            RowEnter(e);
        }

        public virtual void RowEnter(DataGridViewCellEventArgs e)
        {
            try
            {
                entidadID = null;

                if (e.RowIndex < 0 || dgvGrilla.RowCount == 0) return;
                if (!dgvGrilla.Columns.Contains("Id")) return;

                var fila = dgvGrilla.Rows[e.RowIndex];
                if (fila?.Cells["Id"].Value == null) return;

                entidadID = Convert.ToInt64(fila.Cells["Id"].Value);
            }
            catch
            {
                entidadID = null;
            }
        }

        private bool ValidarSeleccion()
        {
            if (dgvGrilla.RowCount == 0)
            {
                MessageBox.Show("No hay datos cargados.");
                return false;
            }

            if (!entidadID.HasValue)
            {
                MessageBox.Show("Seleccione un registro.");
                return false;
            }

            return true;
        }

        #endregion

        #region 🔥 DOBLE CLICK ADAPTABLE

        private void DgvGrilla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            RowEnter(e);
            EjecutarDobleClickFila(entidadID);
        }

        public virtual void EjecutarDobleClickFila(long? id)
        {
            // override en hijo
        }

        #endregion

        #region 🔥 CLICK DERECHO ADAPTABLE

        private void DgvGrilla_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var hit = dgvGrilla.HitTest(e.X, e.Y);

            if (hit.RowIndex >= 0)
            {
                dgvGrilla.ClearSelection();
                dgvGrilla.Rows[hit.RowIndex].Selected = true;

                RowEnter(new DataGridViewCellEventArgs(0, hit.RowIndex));

                EjecutarClickDerechoFila(entidadID, e.Location);
            }
        }

        public virtual void EjecutarClickDerechoFila(long? id, Point posicionMouse)
        {
            // override en hijo
        }

        #endregion

        #region ACCIONES TOOLBAR

        protected virtual void ConfigurarAccionesPersonalizadas() { }

        private void CrearBotonesPersonalizados()
        {
            foreach (var accion in AccionesPersonalizadas)
            {
                var btn = new ToolStripButton
                {
                    Text = accion.Nombre,
                    Image = accion.Icono,
                    DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
                    TextImageRelation = TextImageRelation.TextAboveImage,
                    Enabled = true,
                    Tag = accion
                };

                btn.Click += (s, e) =>
                {
                    if (accion.RequiereSeleccion && !entidadID.HasValue)
                    {
                        MessageBox.Show("Seleccione un registro.");
                        return;
                    }

                    accion.Ejecutar?.Invoke(entidadID);
                };

                BarraLateralBotones.Items.Add(new ToolStripSeparator());
                BarraLateralBotones.Items.Add(btn);
            }
        }

        protected void AgregarAccion(string nombre, Image icono, Action<long?> ejecutar, bool requiereSeleccion = true, bool soloSiNoEliminado = false)
        {
            AccionesPersonalizadas.Add(new AccionGrid
            {
                Nombre = nombre,
                Icono = icono,
                Ejecutar = ejecutar,
                RequiereSeleccion = requiereSeleccion,
                SoloSiNoEliminado = soloSiNoEliminado
            });
        }

        private void EvaluarAccionesPorEstado(FiltroConsulta filtros)
        {
            foreach (ToolStripItem item in BarraLateralBotones.Items)
            {
                if (item is ToolStripButton btn && btn.Tag is AccionGrid accion)
                {
                    btn.Enabled = accion.SoloSiNoEliminado ? !filtros.VerEliminados : true;
                }
            }
        }

        #endregion

        #region BOTONES GRID

        protected void AgregarBotonGrilla(string nombreColumna, string texto)
        {
            var col = new DataGridViewButtonColumn
            {
                Name = nombreColumna,
                HeaderText = texto,
                Text = texto,
                UseColumnTextForButtonValue = true
            };

            dgvGrilla.Columns.Add(col);
        }

        private void DgvGrilla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            EjecutarAccionGrilla(dgvGrilla.Columns[e.ColumnIndex].Name);
        }

        protected virtual void EjecutarAccionGrilla(string nombreColumna)
        {
        }

        #endregion

        #region FECHAS

        protected void ActivarFiltroFechas(string textoCheck)
        {
            chkUsarFecha.Visible = true;
            chkUsarFecha.Text = textoCheck;

            dtpDesde.Visible = true;
            dtpHasta.Visible = true;

            dtpDesde.Enabled = false;
            dtpHasta.Enabled = false;

            chkUsarFecha.CheckedChanged += (s, e) =>
            {
                dtpDesde.Enabled = chkUsarFecha.Checked;
                dtpHasta.Enabled = chkUsarFecha.Checked;
            };
        }

        #endregion
    }
}
