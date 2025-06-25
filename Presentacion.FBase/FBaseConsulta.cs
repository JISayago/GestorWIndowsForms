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

        private void EjecutarBtnModificar()
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrilla, string.Empty, cbxEstaEliminado, BarraLateralBotones);
        }

        private void ActualizarDatos(DataGridView dgvGrilla, string empty, CheckBox check, ToolStrip barraLateralBotones)
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
            if (HayDatosCargados())
            {
                entidadID = (long?)dgvGrilla["Id", e.RowIndex].Value;
            }
            else
            {
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
        }

        private void ResetearGrilla(DataGridView grilla)
        {
            for (int i = 0; i < grilla.ColumnCount; i++)
            {
                grilla.Columns[i].Visible = false;
            }
        }

        private void EjecutarBtnEliminarLoad()
        {
            ActualizarDatos(dgvGrilla, string.Empty, cbxEstaEliminado, BarraLateralBotones);
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrilla, txtBuscar.Text, cbxEstaEliminado, BarraLateralBotones);
        }

        private void cbxEstaEliminado_CheckedChanged(object sender, EventArgs e)
        {
            EjecutarMostrarEliminados();
        }

        public virtual void EjecutarMostrarEliminados()
        {
            throw new NotImplementedException();
        }
    }
}
