using AccesoDatos.Entidades;
using Presentacion.FBase.Helpers;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Presentacion.FBase
{
    public partial class FBaseConsulta : FBase
    {
        protected long? entidadID;
        protected bool puedeEjecutarComando;
        private FiltroConsulta ultimoFiltro;

        protected int paginaActual = 1;
        protected int pageSize = 10;
        protected int totalPaginas = 1;

        protected bool _actualizandoFiltros;

        public List<AccionGrid> AccionesPersonalizadas = new List<AccionGrid>();

        protected virtual string TextoLblBuscar => "Buscar:";

        protected virtual string TextoLblCbx1 => "Filtro 1";

        protected virtual string TextoLblCbx2 => "Filtro 2";

        protected virtual string TextoLblCbx3 => "Filtro 3";

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
            ActualizarTextosLabels();

            ResetearGrilla(dgvGrilla);

            ConfigurarAccionesPersonalizadas();
            CrearBotonesPersonalizados();

            RefrescarGrilla();
        }

        #endregion

        #region ENTER

        protected override void EjecutarEnter()
        {
            if (dgvGrilla.Focused && entidadID.HasValue)
            {
                EjecutarDobleClickFila(entidadID);
                return;
            }

            if (ActiveControl is ComboBox cb && cb.DroppedDown)
                return;

            if (ActiveControl is TextBox tb && tb.Multiline)
                return;

            paginaActual = 1;

            RefrescarGrilla();
        }

        #endregion

        #region CONFIG FILTROS UI

        protected virtual bool UsarCheck1 => false;
        protected virtual bool UsarCheck2 => false;


        protected virtual void ConfigurarFiltrosUI()
        {
            ConfigurarFechas();

            ConfigurarComboBase(cbx1);
            ConfigurarComboBase(cbx2);
            ConfigurarComboBase(cbx3);

            ConfigurarCheckBase(chkBool1);
            ConfigurarCheckBase(chkBool2);

            AsociarEventosCombos();

            if (UsarCheck1)
            {
                ActivarCheck(chkBool1, "Filtro Check 1");
            }
            if (UsarCheck2)
            {
                ActivarCheck(chkBool2, "Filtro Check 2");
            }
        }

        private void ConfigurarFechas()
        {
            if (chkUsarFecha != null)
                chkUsarFecha.Enabled = false;

            if (chkUsarRango != null)
                chkUsarRango.Enabled = false;

            if (dtpDesde != null)
                dtpDesde.Enabled = false;

            if (dtpHasta != null)
                dtpHasta.Enabled = false;
        }

        private void ConfigurarComboBase(ComboBox combo)
        {
            if (combo == null) return;

            combo.Enabled = false;
            combo.DataSource = null;
            combo.SelectedIndex = -1;
        }

        private void ConfigurarCheckBase(CheckBox check)
        {
            if (check == null) return;

            check.Enabled = false;
            check.Visible = false;
            check.Checked = false;
        }

        private void AsociarEventosCombos()
        {
            if (cbx1 != null)
            {
                cbx1.SelectedIndexChanged -= cbxFiltroOpcional_SelectedIndexChanged;
                cbx1.SelectedIndexChanged += cbxFiltroOpcional_SelectedIndexChanged;
            }

            if (cbx2 != null)
            {
                cbx2.SelectedIndexChanged -= cbxFiltroOpcional_SelectedIndexChanged;
                cbx2.SelectedIndexChanged += cbxFiltroOpcional_SelectedIndexChanged;
            }

            if (cbx3 != null)
            {
                cbx3.SelectedIndexChanged -= cbxFiltroOpcional_SelectedIndexChanged;
                cbx3.SelectedIndexChanged += cbxFiltroOpcional_SelectedIndexChanged;
            }
        }

        protected void ActivarFiltroFechas(string textoCheck)
        {
            chkUsarFecha.Enabled = true;
            chkUsarFecha.Text = textoCheck;

            chkUsarRango.Enabled = false;
            chkUsarRango.Checked = false;

            dtpDesde.Enabled = false;
            dtpHasta.Enabled = false;

            chkUsarFecha.CheckedChanged -= chkUsarFecha_CheckedChanged;
            chkUsarFecha.CheckedChanged += chkUsarFecha_CheckedChanged;

            chkUsarRango.CheckedChanged -= chkUsarRango_CheckedChanged;
            chkUsarRango.CheckedChanged += chkUsarRango_CheckedChanged;
        }

        protected void ActivarCombo(ComboBox combo, Label label, object data, string display, string value, string textoLabel)
        {
            if (combo == null) return;

            combo.Enabled = true;

            combo.DataSource = data;
            combo.DisplayMember = display;
            combo.ValueMember = value;

            combo.SelectedIndex = -1;

            if (label != null)
                label.Text = textoLabel;
        }

        protected void ActivarCheck(CheckBox check, string texto)
        {
            if (check == null) return;

            check.Visible = true;
            check.Enabled = true;
            check.Text = texto;
        }

        protected virtual void ActualizarTextosLabels()
        {
            ActualizarLabelCombo(lblcbx1, TextoLblCbx1);
            ActualizarLabelCombo(lblcbx2, TextoLblCbx2);
            ActualizarLabelCombo(lblcbx3, TextoLblCbx3);

            if (lblBuscar != null)
                lblBuscar.Text = TextoLblBuscar;
        }

        private void ActualizarLabelCombo(Label label, string texto)
        {
            if (label == null) return;

            label.Text = texto;
        }

        #endregion

        #region FILTROS

protected virtual string NormalizarTextoBusqueda(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return string.Empty;

        texto = texto.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD);

        var sb = new StringBuilder();

        foreach (var c in texto)
        {
            var categoria = CharUnicodeInfo.GetUnicodeCategory(c);
            if (categoria != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }
    protected virtual FiltroConsulta ObtenerFiltros()
        {
            return new FiltroConsulta
            {
                TextoBuscar = NormalizarTextoBusqueda(txtBuscar.Text),

                FechaDesde = ObtenerFechaDesdeUI(),
                FechaHasta = ObtenerFechaHastaUI(),

                Filtro1 = ObtenerFiltroCombo(cbx1),
                Filtro2 = ObtenerFiltroCombo(cbx2),
                Filtro3 = ObtenerFiltroCombo(cbx3),

                Bool1 = ObtenerFiltroBool(chkBool1),
                Bool2 = ObtenerFiltroBool(chkBool2),

                Page = paginaActual,
                PageSize = pageSize
            };
        }

        protected void RefrescarGrilla()
        {
            ultimoFiltro = ObtenerFiltros();

            ActualizarDatos(dgvGrilla, ultimoFiltro);

            EvaluarAccionesPorEstado(ultimoFiltro);
        }

        protected virtual object ObtenerFiltroCombo(ComboBox combo)
        {
            if (combo == null || !combo.Enabled)
                return null;

            return combo.SelectedValue;
        }

        protected virtual bool ObtenerFiltroBool(CheckBox check)
        {
            if (check == null || !check.Enabled)
                return false;

            return check.Checked;
        }

        protected virtual DateTime? ObtenerFechaDesdeUI()
        {
            if (!chkUsarFecha.Checked)
                return null;

            return dtpDesde.Value.Date;
        }

        protected virtual DateTime? ObtenerFechaHastaUI()
        {
            if (!chkUsarFecha.Checked)
                return null;

            if (!chkUsarRango.Checked)
                return dtpDesde.Value.Date;

            return dtpHasta.Value.Date;
        }

        protected virtual bool EsModoSoloLectura(FiltroConsulta filtro)
        {
            return filtro.Bool1;
        }

        #endregion

        #region DATOS

        public virtual void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            var soloLectura = EsModoSoloLectura(filtros);

            btnEliminar.Enabled = !soloLectura;
            btnNuevo.Enabled = !soloLectura;
            btnModificar.Enabled = !soloLectura;
        }

        public virtual void ResetearGrilla(DataGridView grilla)
        {
            for (int i = 0; i < grilla.ColumnCount; i++)
                grilla.Columns[i].Visible = false;
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

        #region GRILLA

        private void DgvGrilla_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            var hit = dgvGrilla.HitTest(e.X, e.Y);

            if (hit.RowIndex >= 0)
            {
                dgvGrilla.ClearSelection();

                dgvGrilla.Rows[hit.RowIndex].Selected = true;

                RowEnter(new DataGridViewCellEventArgs(0, hit.RowIndex));

                EjecutarClickDerechoFila(entidadID, e.Location);
            }
        }

        private void DgvGrilla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
        }

        private void DgvGrilla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            RowEnter(e);

            EjecutarDobleClickFila(entidadID);
        }

        private void DgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            RowEnter(e);
        }

        public virtual void EjecutarClickDerechoFila(long? id, Point posicionMouse)
        {

        }

        public virtual void EjecutarDobleClickFila(long? id)
        {

        }

        public virtual void RowEnter(DataGridViewCellEventArgs e)
        {
            try
            {
                entidadID = null;

                if (e.RowIndex < 0 || dgvGrilla.RowCount == 0)
                    return;

                if (!dgvGrilla.Columns.Contains("Id"))
                    return;

                var fila = dgvGrilla.Rows[e.RowIndex];

                if (fila?.Cells["Id"].Value == null)
                    return;

                entidadID = Convert.ToInt64(fila.Cells["Id"].Value);
            }
            catch
            {
                entidadID = null;
            }
        }

        #endregion

        #region VALIDACIONES

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

        protected virtual void ConfigurarAccionesPersonalizadas()
        {

        }

        protected void AgregarAccion(
            string nombre,
            Image icono,
            Action<long?> ejecutar,
            bool requiereSeleccion = true,
            bool soloSiNoEliminado = false)
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

        private void EvaluarAccionesPorEstado(FiltroConsulta filtros)
        {
            foreach (ToolStripItem item in BarraLateralBotones.Items)
            {
                if (item is ToolStripButton btn && btn.Tag is AccionGrid accion)
                {
                    btn.Enabled = accion.SoloSiNoEliminado
                        ? !EsModoSoloLectura(filtros)
                        : true;
                }
            }
        }

        #endregion

        #region PAGINADO

        protected void ActualizarPaginacionUI(DatosPaginacion resultado)
        {
            paginaActual = resultado.PaginaActual;
            totalPaginas = resultado.TotalPaginas;

            lblPagina.Text = $"Página {paginaActual} de {totalPaginas}";
            lblTotalRegistros.Text = $"Total: {resultado.CantidadRegistros}";

            btnAnterior.Enabled = paginaActual > 1;
            btnSiguiente.Enabled = paginaActual < totalPaginas;
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                RefrescarGrilla();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                RefrescarGrilla();
            }
        }

        #endregion

        #region EVENTOS FILTROS

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            paginaActual = 1;

            RefrescarGrilla();
        }
        private void chkBool2_CheckedChanged(object sender, EventArgs e)
        {
            if (_actualizandoFiltros)
                return;

            paginaActual = 1;
            AccionCheck2();
            RefrescarGrilla();
        }

        private void chkBool1_CheckedChanged(object sender, EventArgs e)
        {
            if (_actualizandoFiltros)
                return;

            paginaActual = 1;
            AccionCheck1();
            RefrescarGrilla();
        }

        protected virtual void AccionCheck1() { }
        protected virtual void AccionCheck2() { }

        private void chkUsarFecha_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkUsarFecha.Checked)
            {
                dtpDesde.Enabled = false;

                dtpHasta.Enabled = false;

                chkUsarRango.Checked = false;

                chkUsarRango.Enabled = false;

                return;
            }

            dtpDesde.Enabled = true;

            chkUsarRango.Enabled = true;

            dtpHasta.Enabled = chkUsarRango.Checked;
        }

        private void chkUsarRango_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkUsarFecha.Checked)
                return;

            dtpHasta.Enabled = chkUsarRango.Checked;
        }

        private void cbxFiltroOpcional_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarLabelBusqueda();
        }

        protected virtual void ActualizarLabelBusqueda()
        {
            if (lblBuscar == null)
                return;

            string texto = string.Empty;

            if (cbx1 != null && cbx1.Enabled && !string.IsNullOrWhiteSpace(cbx1.Text))
                texto = cbx1.Text;
            else if (cbx2 != null && cbx2.Enabled && !string.IsNullOrWhiteSpace(cbx2.Text))
                texto = cbx2.Text;
            else if (cbx3 != null && cbx3.Enabled && !string.IsNullOrWhiteSpace(cbx3.Text))
                texto = cbx3.Text;

            if (string.IsNullOrWhiteSpace(texto) || texto == "Todos")
            {
                lblBuscar.Text = "Buscar:";
            }
            else
            {
                lblBuscar.Text = $"Buscar por: {texto}";
            }
        }

        #endregion

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();

            paginaActual = 1;

            RefrescarGrilla();
        }

        protected virtual void LimpiarFiltros()
        {
            LimpiarTexto();
            LimpiarCombos();
            LimpiarChecks();
            LimpiarFechas();

            ActualizarLabelBusqueda();

            LimpiarFiltrosCustom();
        }

        protected virtual void LimpiarFiltrosCustom()
        {
            // para override en hijos
        }

        private void LimpiarTexto()
        {
            if (txtBuscar != null)
                txtBuscar.Text = string.Empty;
        }

        private void LimpiarCombos()
        {
            LimpiarCombo(cbx1);
            LimpiarCombo(cbx2);
            LimpiarCombo(cbx3);
        }

        private void LimpiarCombo(ComboBox combo)
        {
            if (combo == null || !combo.Enabled)
                return;

            combo.SelectedIndex = -1;

            if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
        }

        private void LimpiarChecks()
        {
            if (chkBool1 != null && chkBool1.Enabled)
                chkBool1.Checked = false;

            if (chkBool2 != null && chkBool2.Enabled)
                chkBool2.Checked = false;
        }

        private void LimpiarFechas()
        {
            if (chkUsarFecha != null)
                chkUsarFecha.Checked = false;

            if (chkUsarRango != null)
                chkUsarRango.Checked = false;

            if (dtpDesde != null)
                dtpDesde.Value = DateTime.Now;

            if (dtpHasta != null)
                dtpHasta.Value = DateTime.Now;
        }

       
    }
}