using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.Core.Producto;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Articulo.Categoria;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Presentacion.Core.Categoria
{
    public partial class FCategoriaConsulta : FBaseConsulta
    {
        private readonly ICategoriaServicio _CategoriaServicio;

        public long? categoriaSeleccionada = null;

        private bool vieneDeCargaCategoria = true;

        public FCategoriaConsulta(bool vieneDeCargaCategoria = true)
            : this(new CategoriaServicio())
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

            if (grilla.Columns.Count == 0)
                return;

            if (grilla.Columns.Contains("CategoriaId"))
            {
                grilla.Columns["CategoriaId"].Visible = false;
                grilla.Columns["CategoriaId"].Name = "Id";
            }

            if (grilla.Columns.Contains("Nombre"))
            {
                grilla.Columns["Nombre"].Visible = true;
                grilla.Columns["Nombre"].HeaderText = "Categoria";
                grilla.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (grilla.Columns.Contains("Descripcion"))
            {
                grilla.Columns["Descripcion"].Visible = true;
                grilla.Columns["Descripcion"].HeaderText = "Descripción";
                grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (grilla.Columns.Contains("EstadoDescripcion"))
            {
                grilla.Columns["EstadoDescripcion"].Visible = true;
                grilla.Columns["EstadoDescripcion"].HeaderText = "Estado";
                grilla.Columns["EstadoDescripcion"].Width = 120;
            }
        }

        #endregion

        #region 🔥 ACTUALIZAR DATOS

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            filtros.Filtro1 ??= "";

            var resultado = _CategoriaServicio.ObtenerCategorias(filtros);

            dgv.DataSource = resultado.Items;

            ResetearGrilla(dgv);

            var paginacion = new DatosPaginacion
            {
                PaginaActual = resultado.Page,
                PageSize = resultado.PageSize,
                CantidadRegistros = resultado.TotalRegistros
            };

            ActualizarPaginacionUI(paginacion);

            BarraLateralBotones.Enabled = !filtros.Bool1;
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

            if (!puedeEjecutarComando)
                return;

            var f = new FCategoriaABM(TipoOperacion.Modificar, entidadID);

            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();

            if (!puedeEjecutarComando)
                return;

            var f = new FCategoriaABM(TipoOperacion.Eliminar, entidadID);

            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        private void Recargar()
        {
            RefrescarGrilla();
        }

        #endregion

        #region 🔵 ACCIONES DINÁMICAS

        protected override void ConfigurarAccionesPersonalizadas()
        {
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
            if (!id.HasValue)
            {
                MessageBox.Show("Seleccione una categoria");
                return;
            }

            categoriaSeleccionada = id;

            DialogResult = DialogResult.OK;

            Close();
        }

        #endregion

        #region 🔎 FILTROS

        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

            var opciones = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todos", Valor = "" },
                new OpcionFiltro { Texto = "Nombre", Valor = "Nombre" },
                new OpcionFiltro { Texto = "Descripción", Valor = "Descripcion" }
            };

            ActivarCombo(
                cbx1,
                lblcbx1,
                opciones,
                "Texto",
                "Valor",
                "Buscar categoria por:"
            );

            ActivarCheck(
                chkBool1,
                "Ver eliminadas"
            );

            cbx1.SelectedValue = "";
        }

        #endregion
    }
}