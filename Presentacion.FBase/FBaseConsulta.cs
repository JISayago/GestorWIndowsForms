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
            dgvGrilla.MouseDown += DgvGrilla_MouseDown;
        }

        #region LOAD

        private void FBaseConsulta_Load(object sender, EventArgs e)
        {
            ConfigurarFiltrosUI();

            ResetearGrilla(dgvGrilla);

            ConfigurarAccionesPersonalizadas();
            CrearBotonesPersonalizados();

            RefrescarGrilla();
        }

        #endregion

        #region CONFIG FILTROS UI

        protected virtual void ConfigurarFiltrosUI()
        {
            // Todo visible, pero deshabilitado por defecto
            if (chkUsarFecha != null)
                chkUsarFecha.Enabled = false;

            if (dtpDesde != null)
                dtpDesde.Enabled = false;

            if (dtpHasta != null)
                dtpHasta.Enabled = false;

            if (cbxFiltroOpcional != null)
                cbxFiltroOpcional.Enabled = false;

            if (cbxFiltroExtraEstado != null)
                cbxFiltroExtraEstado.Enabled = false;
        }

        protected void ActivarFiltroFechas(string textoCheck)
        {
            chkUsarFecha.Enabled = true;
            chkUsarFecha.Text = textoCheck;

            dtpDesde.Enabled = false;
            dtpHasta.Enabled = false;

            chkUsarFecha.CheckedChanged -= ChkUsarFecha_CheckedChanged;
            chkUsarFecha.CheckedChanged += ChkUsarFecha_CheckedChanged;
        }

        private void ChkUsarFecha_CheckedChanged(object sender, EventArgs e)
        {
            dtpDesde.Enabled = chkUsarFecha.Checked;
            dtpHasta.Enabled = chkUsarFecha.Checked;
        }

        protected void ActivarFiltroCombo(object data, string display, string value)
        {
            cbxFiltroOpcional.Enabled = true;
            cbxFiltroOpcional.DataSource = data;
            cbxFiltroOpcional.DisplayMember = display;
            cbxFiltroOpcional.ValueMember = value;
            cbxFiltroOpcional.SelectedIndex = -1;
        }

        protected void ActivarComboOpcional(object data, string display, string value)
        {
            if (cbxFiltroExtraEstado != null)
            {
                cbxFiltroExtraEstado.Enabled = true;
                cbxFiltroExtraEstado.DataSource = data;
                cbxFiltroExtraEstado.DisplayMember = display;
                cbxFiltroExtraEstado.ValueMember = value;
                cbxFiltroExtraEstado.SelectedIndex = -1;
            }
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
            EvaluarAccionesPorEstado(filtros);
        }

        #endregion

        #region LECTURA FILTROS

        protected virtual DateTime? ObtenerFechaDesdeUI()
        {
            if (dtpDesde == null || !dtpDesde.Enabled) return null;
            if (chkUsarFecha != null && !chkUsarFecha.Checked) return null;

            return dtpDesde.Value.Date;
        }

        protected virtual DateTime? ObtenerFechaHastaUI()
        {
            if (dtpHasta == null || !dtpHasta.Enabled) return null;
            if (chkUsarFecha != null && !chkUsarFecha.Checked) return null;

            return dtpHasta.Value.Date;
        }

        protected virtual object ObtenerFiltroExtraUI()
        {
            if (cbxFiltroOpcional == null || !cbxFiltroOpcional.Enabled) return null;
            return cbxFiltroOpcional.SelectedValue;
        }

        protected virtual object ObtenerComboOpcionalUI()
        {
            if (cbxFiltroExtraEstado == null || !cbxFiltroExtraEstado.Enabled) return null;
            return cbxFiltroExtraEstado.SelectedValue;
        }

        #endregion

        #region DATOS

        public virtual void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            btnEliminar.Enabled = !filtros.VerEliminados;
            btnNuevo.Enabled = !filtros.VerEliminados;
            btnModificar.Enabled = !filtros.VerEliminados;
        }

        public virtual void ResetearGrilla(DataGridView grilla)
        {
            for (int i = 0; i < grilla.ColumnCount; i++)
                grilla.Columns[i].Visible = false;
        }

        #endregion

        #region SELECCION

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

        #region ACCIONES DINAMICAS

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
    }
}