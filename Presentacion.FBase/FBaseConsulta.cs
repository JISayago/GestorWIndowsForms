using Presentacion.FBase.Helpers;
using Servicios.Helpers.Sistema.FiltrosConsulta;
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
        private FiltroConsulta ultimoFiltro;
        protected int paginaActual = 1;
        protected int pageSize = 10;
        protected int totalPaginas = 1;

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
        private void DgvGrilla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            //EjecutarAccionGrilla(dgvGrilla.Columns[e.ColumnIndex].Name);
        }
        #region LOAD
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
            if (chkUsarFecha != null)
                chkUsarFecha.Enabled = false;

            if (chkUsarRango != null)
                chkUsarRango.Enabled = false;

            if (dtpDesde != null)
                dtpDesde.Enabled = false;

            if (dtpHasta != null)
                dtpHasta.Enabled = false;

            if (cbxFiltroOpcional != null)
                cbxFiltroOpcional.Enabled = false;

            if (cbxFiltroExtraEstado != null)
                cbxFiltroExtraEstado.Enabled = false;

            if (cbxEstaEliminado != null)
            {
                cbxEstaEliminado.Visible = UsarFiltroEliminados;
                cbxEstaEliminado.Enabled = UsarFiltroEliminados;
                if (UsarFiltroEliminados)
                    cbxEstaEliminado.Checked = false;
            }

            if (cbxFiltroOpcional != null)
            {
                cbxFiltroOpcional.SelectedIndexChanged -= cbxFiltroOpcional_SelectedIndexChanged;
                cbxFiltroOpcional.SelectedIndexChanged += cbxFiltroOpcional_SelectedIndexChanged;
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


        protected void ActivarFiltroCombo(object data, string display, string value)
        {
            cbxFiltroOpcional.Enabled = true;
            cbxFiltroOpcional.DataSource = data;
            cbxFiltroOpcional.DisplayMember = display;
            cbxFiltroOpcional.ValueMember = value;
            cbxFiltroOpcional.SelectedIndex = -1;
        }
        protected void ActivarFiltroEliminados(string texto = "Ver eliminados")
        {
            if (cbxEstaEliminado == null) return;

            cbxEstaEliminado.Visible = true;
            cbxEstaEliminado.Enabled = true;
            cbxEstaEliminado.Text = texto;
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


        #region FILTROS
        protected virtual bool UsarFiltroEliminados => true;
        protected virtual FiltroConsulta ObtenerFiltros()
        {
            return new FiltroConsulta
            {
                TextoBuscar = txtBuscar.Text,
                VerEliminados = UsarFiltroEliminados && cbxEstaEliminado.Checked,
                FechaDesde = ObtenerFechaDesdeUI(),
                FechaHasta = ObtenerFechaHastaUI(),
                Extra = ObtenerFiltroExtraUI(),
                Extra2 = ObtenerComboOpcionalUI(),

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

        #endregion

        #region LECTURA FILTROS

        protected virtual DateTime? ObtenerFechaDesdeUI()
        {
            if (!chkUsarFecha.Checked) return null;

            return dtpDesde.Value.Date;
        }

        protected virtual DateTime? ObtenerFechaHastaUI()
        {
            if (!chkUsarFecha.Checked) return null;

            if (!chkUsarRango.Checked)
                return dtpDesde.Value.Date;

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

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

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

                ultimoFiltro.Page = paginaActual;

                ActualizarDatos(dgvGrilla, ultimoFiltro);
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;

                ultimoFiltro.Page = paginaActual;

                ActualizarDatos(dgvGrilla, ultimoFiltro);
            }
        }

        private void cbxEstaEliminado_CheckedChanged(object sender, EventArgs e)
        {
            paginaActual = 1;
            RefrescarGrilla();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            paginaActual = 1;
            RefrescarGrilla();
        }

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

            // Si activa filtro
            dtpDesde.Enabled = true;
            chkUsarRango.Enabled = true;

            // Si no es rango, dtpHasta apagado
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
            if (cbxFiltroOpcional == null || lblBuscar == null)
                return;

            var texto = cbxFiltroOpcional.Text;

            if (string.IsNullOrWhiteSpace(texto) || texto == "Todos")
            {
                lblBuscar.Text = "Buscar:";
            }
            else
            {
                lblBuscar.Text = $"Buscar por: {texto}";
            }
        }
    }
}


