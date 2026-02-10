using Presentacion.Core.Producto;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Articulo.Categoria;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Categoria
{
    public partial class FCategoriaConsulta : FBaseConsulta
    {
        private readonly ICategoriaServicio _CategoriaServicio;
        public long? categoriaSeleccionada = null;
        private bool vieneDeCargaCategoria = true;

        public FCategoriaConsulta(bool vieneDeCargaCategoria = true) : this(new CategoriaServicio())
        {
            InitializeComponent();
            this.vieneDeCargaCategoria = vieneDeCargaCategoria;
        }

        public FCategoriaConsulta(ICategoriaServicio categoriaServicio)
        {
            _CategoriaServicio = categoriaServicio;
            InitializeComponent();
        }

        #region 🔷 GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (!grilla.Columns.Contains("CategoriaId") && !grilla.Columns.Contains("Id"))
                return;

            // ocultar id
            if (grilla.Columns.Contains("CategoriaId"))
            {
                grilla.Columns["CategoriaId"].Visible = false;
                grilla.Columns["CategoriaId"].Name = "Id";
            }

            grilla.Columns["Nombre"].Visible = true;
            grilla.Columns["Nombre"].HeaderText = "Categoria";
            grilla.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        #endregion

        #region 🔥 ACTUALIZAR DATOS (NUEVO SISTEMA)

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            if (filtros.VerEliminados)
            {
                dgv.DataSource = _CategoriaServicio.ObtenerCategoriaEliminada(filtros.TextoBuscar);
                BarraLateralBotones.Enabled = false;
            }
            else
            {
                dgv.DataSource = _CategoriaServicio.ObtenerCategoria(filtros.TextoBuscar);
                BarraLateralBotones.Enabled = true;
            }
        }

        #endregion

        #region 🔷 BOTONES BASE

        public override void EjecutarBtnNuevo()
        {
            var f = new FCategoriaABM(TipoOperacion.Nuevo);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (!puedeEjecutarComando) return;

            var f = new FCategoriaABM(TipoOperacion.Modificar, entidadID);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (!puedeEjecutarComando) return;

            var f = new FCategoriaABM(TipoOperacion.Eliminar, entidadID);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        private void Recargar()
        {
           // btnActualizar_Click_Base();
        }

        #endregion
        #region 🔵 ACCIONES DINÁMICAS EXTRA

        protected override void ConfigurarAccionesPersonalizadas()
        {
            // BOTON Seleccionar
            if (vieneDeCargaCategoria)
            {
                AgregarAccion(
                    "Seleccionar Categoria",
                    Constantes.Imagenes.ImgPerfilUsuario,
                    SeleccionCategoria,
                    true
                );
            }

        }
        

        private void SeleccionCategoria(long? id)
        {
            if (!entidadID.HasValue)
            {
                MessageBox.Show("Seleccione una categoria");
                return;
            }

            categoriaSeleccionada = entidadID;
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

    }
}
