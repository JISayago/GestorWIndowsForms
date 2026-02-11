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

        #region BUSQUEDA / FILTROS UI

        public virtual void btnBuscar_Click_1(object sender, EventArgs e)
        {
            RefrescarGrilla();
        }

        private void cbxEstaEliminado_CheckedChanged(object sender, EventArgs e)
        {
            RefrescarGrilla();
        }

        #endregion

        #region FILTROS DINAMICOS BASE

        protected virtual FiltroConsulta ObtenerFiltros()
        {
            return new FiltroConsulta
            {
                TextoBuscar = txtBuscar.Text,
                VerEliminados = cbxEstaEliminado.Checked,
                FechaDesde = ObtenerFechaDesdeUI(),
                FechaHasta = ObtenerFechaHastaUI(),
                Extra = ObtenerFiltroExtraUI()
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
            if (dtpDesde == null) return null;
            if (!dtpDesde.Visible) return null;
            if (chkUsarFecha != null && !chkUsarFecha.Checked) return null;

            return dtpDesde.Value.Date;
        }

        protected virtual DateTime? ObtenerFechaHastaUI()
        {
            if (dtpHasta == null) return null;
            if (!dtpHasta.Visible) return null;
            if (chkUsarFecha != null && !chkUsarFecha.Checked) return null;

            return dtpHasta.Value.Date;
        }

        protected virtual object ObtenerFiltroExtraUI()
        {
            if (cbxFiltroOpcional == null) return null;
            if (!cbxFiltroOpcional.Visible) return null;
            if (cbxFiltroOpcional.SelectedValue == null) return null;

            return cbxFiltroOpcional.SelectedValue;
        }

        #endregion

        #region CONFIGURAR FILTROS OPCIONALES (PARA LOS FORMS)

        protected void ActivarFiltroCombo(string label, object data, string display, string value)
        {
            if (pnlFiltrosAvanzados != null)
                pnlFiltrosAvanzados.Visible = true;

            if (lblFiltro != null)
            {
                lblFiltro.Text = label;
                lblFiltro.Visible = true;
            }

            if (cbxFiltroOpcional != null)
            {
                cbxFiltroOpcional.Visible = true;
                cbxFiltroOpcional.DataSource = data;
                cbxFiltroOpcional.DisplayMember = display;
                cbxFiltroOpcional.ValueMember = value;
                cbxFiltroOpcional.SelectedIndex = -1;
            }
        }

        protected void ActivarFiltroFecha(bool rango = true)
        {
            if (pnlFiltrosAvanzados != null)
                pnlFiltrosAvanzados.Visible = true;

            if (chkUsarFecha != null)
            {
                chkUsarFecha.Visible = true;
                chkUsarFecha.Checked = false;
            }

            if (dtpDesde != null)
                dtpDesde.Visible = true;

            if (dtpHasta != null)
                dtpHasta.Visible = rango;
        }

        #endregion

        #region METODOS BASE DATOS

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
                if (fila == null || fila.IsNewRow) return;

                var celda = fila.Cells["Id"];
                if (celda?.Value == null || celda.Value == DBNull.Value) return;

                entidadID = Convert.ToInt64(celda.Value);
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

        #region DOBLE CLICK FILA

        private void DgvGrilla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EjecutarDobleClickFila();
        }

        public virtual void EjecutarDobleClickFila() { }

        #endregion

        #region ACCIONES DINAMICAS TOOLBAR

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
                    if (accion.SoloSiNoEliminado)
                        btn.Enabled = !filtros.VerEliminados;
                    else
                        btn.Enabled = true;
                }
            }
        }

        #endregion

        #region BOTONES POR FILA GRID (OPCIONAL)

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
    }
}
