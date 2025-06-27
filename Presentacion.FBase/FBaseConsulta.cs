using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.FBase
{
    public partial class FBaseConsulta : Form
    {
        protected long? entidadID;
        protected bool puedeEjecutarComando;
        protected string respuestaTest;
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
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            EjecutarBtnNuevo();
        }

        public virtual void EjecutarBtnNuevo()
        {
           
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EjecutarBtnEliminar();
        }

        public virtual void EjecutarBtnEliminar()
        {
            if (HayDatosCargados())
            {
                if (!entidadID.HasValue)
                {
                    MessageBox.Show("Por favor seleccione un registro.");
                    puedeEjecutarComando = false;
                    return;
                }
                else
                {
                    puedeEjecutarComando = true;
                }
            }
            else
            {
                MessageBox.Show("No hay Datos Cargados.");
            }
        }
        private bool HayDatosCargados()
        {
            
            return dgvGrilla.RowCount > 0;
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            EjecutarBtnModificar();
        }

        public virtual void EjecutarBtnModificar()
        {
            if (HayDatosCargados())
            {
                if (!entidadID.HasValue)
                {
                    MessageBox.Show("Por favor seleccione un registro.");
                    puedeEjecutarComando = false;
                    return;
                }
                else
                {
                    puedeEjecutarComando = true;
                }
            }
            else
            {
                MessageBox.Show("No hay Datos Cargados.");
            }
        }

        public virtual void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrilla, string.Empty, cbxEstaEliminado, BarraLateralBotones);
        }

        public virtual void ActualizarDatos(DataGridView dgvGrilla, string empty, CheckBox check, ToolStrip barraLateralBotones)
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

        private void DgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            RowEnter(e);
        }
        public virtual void RowEnter(DataGridViewCellEventArgs e)
        {
            try
            {
                entidadID = null;

                // Validamos índice y existencia de columna
                if (e.RowIndex < 0 || !HayDatosCargados() || !dgvGrilla.Columns.Contains("Id"))
                    return;

                var fila = dgvGrilla.Rows[e.RowIndex];

                // Validamos que la fila y la celda no sean nulas
                if (fila == null || fila.IsNewRow)
                    return;

                var celda = fila.Cells["Id"];
                if (celda == null || celda.Value == null || celda.Value == DBNull.Value)
                    return;

                entidadID = Convert.ToInt64(celda.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar obtener el ID de la fila seleccionada: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                entidadID = null;
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FBaseConsulta_Load(object sender, EventArgs e)
        {
            EjecutarBtnEliminarLoad();
            ResetearGrilla(dgvGrilla);
            //hacerlo en cada consulta
        }

        public virtual void ResetearGrilla(DataGridView grilla)
        {
            for (int i = 0; i < grilla.ColumnCount; i++)
            {
                grilla.Columns[i].Visible = false;
            }
        }

        public virtual void EjecutarBtnEliminarLoad()
        {
            ActualizarDatos(dgvGrilla, string.Empty, cbxEstaEliminado, BarraLateralBotones);
        }

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
    }
}
