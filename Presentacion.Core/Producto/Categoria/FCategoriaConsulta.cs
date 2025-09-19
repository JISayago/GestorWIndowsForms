using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Articulo.Categoria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Categoria
{
    public partial class FCategoriaConsulta : FBaseConsulta
    {
        private readonly ICategoriaServicio _CategoriaServicio;
        public long? categoriaSeleccionada = null;

        public FCategoriaConsulta() : this(new CategoriaServicio())
        {
            InitializeComponent();
        }

        public FCategoriaConsulta(ICategoriaServicio CategoriaServicio)
        {
            _CategoriaServicio = CategoriaServicio;
        }

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);


            grilla.Columns["Nombre"].Visible = true;
            grilla.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Nombre"].HeaderText = "Categoria";

        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        {

            base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

            if (check.Checked)
            {
                grilla.DataSource = _CategoriaServicio.ObtenerCategoriaEliminada(cadenaBuscar);
                toolStrip.Enabled = false;
            }
            else
            {
                grilla.DataSource = _CategoriaServicio.ObtenerCategoria(cadenaBuscar);
                toolStrip.Enabled = true;
            }
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (puedeEjecutarComando)
            {
                var FABMCategoria = new FCategoriaABM(TipoOperacion.Eliminar, entidadID);
                FABMCategoria.ShowDialog();
                ActualizarSegunOperacion(FABMCategoria.RealizoAlgunaOperacion);
            }
        }

        private void ActualizarSegunOperacion(bool realizoOperacion)
        {
            if (realizoOperacion)
            {
                ActualizarDatos(dgvGrilla, string.Empty, cbxEstaEliminado, BarraLateralBotones);
            }
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (puedeEjecutarComando)
            {
                var FABMCategoria = new FCategoriaABM(TipoOperacion.Modificar, entidadID);
                FABMCategoria.ShowDialog();
                ActualizarSegunOperacion(FABMCategoria.RealizoAlgunaOperacion);
            }
        }

        public override void EjecutarBtnNuevo()
        {
            var FABMCategoria = new FCategoriaABM(TipoOperacion.Nuevo);
            FABMCategoria.ShowDialog();
            ActualizarSegunOperacion(FABMCategoria.RealizoAlgunaOperacion);
        }

        private void btnSeleccionarCategoria_Click(object sender, EventArgs e)
        {
            categoriaSeleccionada = (long)entidadID;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
