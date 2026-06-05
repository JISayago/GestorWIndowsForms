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
                new OpcionFiltro { Texto = "Todas", Valor = "" },
                new OpcionFiltro { Texto = "Nombre", Valor = "Nombre" }
            };

            ActivarCombo(
                cbx1,
                lblcbx1,
                opcionesBusqueda,
                "Texto",
                "Valor",
                "Buscar por"
            );


            ActivarCheck(chkBool1, "Ver eliminados");
            ActivarCheck(chkBool2, "Mostrar Todos los Rubros");
            cbx1.SelectedValue = "";
        }
        protected override string TextoLblBuscar
    => "Buscar Rubro:";

        protected override string TextoLblCbx1
            => "Filtrar por Nombre";

        protected override string TextoLblCbx2
            => "Filtrar por";

        protected override string TextoLblCbx3
            => "Filtrar por";
        protected override string TextoTitular
          => "Listado de los Rubros";
        #endregion

        #region 🔷 GRILLA
        protected override void AccionCheck2()
        {
            if (chkBool2.Checked)
            {
                _actualizandoFiltros = true;
                chkBool1.Checked = false;
                _actualizandoFiltros = false;

                LimpiarFiltrosParaTodos();
            }
        }

        protected override void AccionCheck1()
        {
            if (chkBool1.Checked)
            {
                _actualizandoFiltros = true;
                chkBool2.Checked = false;
                _actualizandoFiltros = false;

                LimpiarFiltrosParaTodos();
            }
        }

        private void LimpiarFiltrosParaTodos()
        {
            _actualizandoFiltros = true;

            txtBuscar.Clear();

            if (cbx1.Enabled)
                cbx1.SelectedIndex = 0;

            if (cbx2.Enabled)
                cbx2.SelectedIndex = 0;

            if (cbx3.Enabled)
                cbx3.SelectedIndex = 0;

            chkUsarFecha.Checked = false;

            _actualizandoFiltros = false;
        }

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