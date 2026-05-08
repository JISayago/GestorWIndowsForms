using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Producto.Rubro;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Presentacion.Core.Producto.Rubro
{
    public partial class FRubroConsulta : FBaseConsulta
    {
        private readonly IRubroServicio _rubroServicio;

        public long? rubroSeleccionado = null;

        private bool vieneDeCargaRubro = true;

        public FRubroConsulta(bool vieneDeCargaRubro = true)
            : this(new RubroServicio())
        {
            InitializeComponent();

            this.vieneDeCargaRubro = vieneDeCargaRubro;
        }

        public FRubroConsulta(IRubroServicio rubroServicio)
        {
            _rubroServicio = rubroServicio;

            InitializeComponent();
        }

        #region 🔷 FILTROS

        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

            ActivarCheck(chkBool1, "Ver eliminados");

            var opcionesBusqueda = new List<OpcionFiltro>
            {
                new OpcionFiltro
                {
                    Texto = "Todos",
                    Valor = ""
                },
                new OpcionFiltro
                {
                    Texto = "Nombre",
                    Valor = "Nombre"
                }
            };

            ActivarCombo(
                cbx1,
                lblcbx1,
                opcionesBusqueda,
                "Texto",
                "Valor",
                "Buscar por"
            );

            cbx1.SelectedValue = "";
        }

        #endregion

        #region 🔷 GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Count == 0)
                return;

            if (grilla.Columns.Contains("RubroId"))
            {
                grilla.Columns["RubroId"].Visible = false;
                grilla.Columns["RubroId"].Name = "Id";
            }

            if (grilla.Columns.Contains("Nombre"))
            {
                grilla.Columns["Nombre"].Visible = true;
                grilla.Columns["Nombre"].HeaderText = "Rubro";
                grilla.Columns["Nombre"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        #endregion

        #region 🔥 DATOS

        public override void ActualizarDatos(
            DataGridView dgv,
            FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            var resultado = _rubroServicio.ObtenerRubros(filtros);

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
            var form = new FRubroABM(TipoOperacion.Nuevo);

            form.ShowDialog();

            ActualizarSegunOperacion(form.RealizoAlgunaOperacion);
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();

            if (!puedeEjecutarComando)
                return;

            var form = new FRubroABM(
                TipoOperacion.Modificar,
                entidadID);

            form.ShowDialog();

            ActualizarSegunOperacion(form.RealizoAlgunaOperacion);
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();

            if (!puedeEjecutarComando)
                return;

            var form = new FRubroABM(
                TipoOperacion.Eliminar,
                entidadID);

            form.ShowDialog();

            ActualizarSegunOperacion(form.RealizoAlgunaOperacion);
        }

        private void ActualizarSegunOperacion(bool realizoOperacion)
        {
            if (!realizoOperacion)
                return;

            RefrescarGrilla();
        }

        #endregion

        #region 🔵 ACCIONES DINÁMICAS

        protected override void ConfigurarAccionesPersonalizadas()
        {
            if (vieneDeCargaRubro)
            {
                AgregarAccion(
                    "Seleccionar Rubro",
                    Constantes.Imagenes.ImgPerfilUsuario,
                    SeleccionRubro,
                    true
                );
            }
        }

        private void SeleccionRubro(long? id)
        {
            if (!entidadID.HasValue)
            {
                MessageBox.Show("Seleccione un rubro.");
                return;
            }

            rubroSeleccionado = entidadID;

            DialogResult = DialogResult.OK;

            Close();
        }

        #endregion

        #region 🔷 EVENTOS

        private void btnRubroSeleccion_Click(object sender, EventArgs e)
        {
            SeleccionRubro(entidadID);
        }

        #endregion
    }
}