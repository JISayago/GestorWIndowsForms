using Presentacion.FBase.DTO;
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

        // 🔵 lista de acciones dinámicas
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

            ConfigurarAccionesPersonalizadas();   // 🔥 nuevas acciones
            CrearBotonesPersonalizados();
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
            btnActualizar_Click_Base();
        }

        public virtual void btnActualizar_Click_Base()
        {
            ActualizarDatos(dgvGrilla, txtBuscar.Text, cbxEstaEliminado, BarraLateralBotones);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region BUSQUEDA / FILTROS

        public virtual void btnBuscar_Click_1(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrilla, txtBuscar.Text, cbxEstaEliminado, BarraLateralBotones);
        }

        private void cbxEstaEliminado_CheckedChanged(object sender, EventArgs e)
        {
            EjecutarMostrarEliminados();
        }

        public virtual void EjecutarMostrarEliminados()
        {
            ActualizarDatos(dgvGrilla, txtBuscar.Text, cbxEstaEliminado, BarraLateralBotones);
        }

        #endregion

        #region METODOS BASE

        public virtual void ActualizarDatos(DataGridView dgv, string textoBuscar, CheckBox check, ToolStrip barra)
        {
            if (check.Checked)
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
        }

        public virtual void ResetearGrilla(DataGridView grilla)
        {
            for (int i = 0; i < grilla.ColumnCount; i++)
                grilla.Columns[i].Visible = false;
        }

        public virtual void EjecutarBtnEliminarLoad()
        {
            ActualizarDatos(dgvGrilla, string.Empty, cbxEstaEliminado, BarraLateralBotones);
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

        #region DOBLE CLICK

        private void DgvGrilla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EjecutarDobleClickFila();
        }

        public virtual void EjecutarDobleClickFila() { }

        #endregion

        #region 🔥 ACCIONES DINAMICAS

        // 👉 override en cada consulta
        protected virtual void ConfigurarAccionesPersonalizadas() { }

        private void CrearBotonesPersonalizados()
        {
            foreach (var accion in AccionesPersonalizadas)
            {
                var btn = new ToolStripButton
                {
                    Text = accion.Nombre,
                    Image = accion.Icono,
                    DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
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

        protected void AgregarAccion(string nombre, Image icono, Action<long?> ejecutar, bool requiereSeleccion = true)
        {
            AccionesPersonalizadas.Add(new AccionGrid
            {
                Nombre = nombre,
                Icono = icono,
                Ejecutar = ejecutar,
                RequiereSeleccion = requiereSeleccion
            });
        }

        #endregion

        #region 🔵 BOTONES POR FILA (OPCIONAL PRO)

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
            // override en formularios hijos
        }

        #endregion
    }
}
