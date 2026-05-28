using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Articulo.Marca;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.Articulo.Marca
{
    public partial class FMarcaConsulta : FBaseConsulta
    {
        private readonly IMarcaServicio _marcaServicio;

        public long? marcaSeleccionada = null;

        private bool vieneDeCargaMarca = true;

        public FMarcaConsulta(bool vieneDeCargaMarca = true)
            : this(new MarcaServicio())
        {
            InitializeComponent();

            this.vieneDeCargaMarca = vieneDeCargaMarca;
        }

        public FMarcaConsulta(IMarcaServicio marcaServicio)
        {
            _marcaServicio = marcaServicio;

            InitializeComponent();
        }

        #region INIT

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            Text = "Consulta de Marcas";
        }

        #endregion

        #region 🔥 ACCIONES PERSONALIZADAS

        protected override void ConfigurarAccionesPersonalizadas()
        {
            if (vieneDeCargaMarca)
            {
                AgregarAccion(
                    "Seleccionar Marca",
                    SystemIcons.Information.ToBitmap(),
                    SeleccionMarca,
                    true
                );
            }
        }

        private void SeleccionMarca(long? id)
        {
            if (!id.HasValue)
                return;

            marcaSeleccionada = id;

            DialogResult = DialogResult.OK;

            Close();
        }

        #endregion

        #region 🧱 GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Count == 0)
                return;

            if (grilla.Columns.Contains("MarcaId"))
            {
                grilla.Columns["MarcaId"].Visible = false;
                grilla.Columns["MarcaId"].Name = "Id";
            }

            if (grilla.Columns.Contains("Nombre"))
            {
                grilla.Columns["Nombre"].Visible = true;
                grilla.Columns["Nombre"].HeaderText = "Marca";
                grilla.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

        }

        #endregion

        #region 🔥 ACTUALIZAR DATOS

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            filtros.Filtro1 ??= "";

            var resultado = _marcaServicio.ObtenerMarcas(filtros);

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

        #region 🧰 BOTONES BASE

        public override void EjecutarBtnNuevo()
        {
            var f = new FMarcaABM(TipoOperacion.Nuevo);

            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();

            if (!puedeEjecutarComando)
                return;

            var f = new FMarcaABM(TipoOperacion.Modificar, entidadID);

            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();

            if (!puedeEjecutarComando)
                return;

            var f = new FMarcaABM(TipoOperacion.Eliminar, entidadID);

            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        private void Recargar()
        {
            RefrescarGrilla();
        }

        #endregion

        #region 🔎 FILTROS

        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

            var opciones = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todas", Valor = "" },
                new OpcionFiltro { Texto = "Nombre", Valor = "Nombre" },
            };

            ActivarCombo(
                cbx1,
                lblcbx1,
                opciones,
                "Texto",
                "Valor",
                "Buscar marca por:"
            );

            ActivarCheck(chkBool1, "Ver eliminadas");
            ActivarCheck(chkBool2, "Mostrar Todas las Marcas");
            cbx1.SelectedValue = "";
        }
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

        protected override string TextoLblBuscar
      => "Buscar Marca:";

        protected override string TextoLblCbx1
            => "Filtrar por Nombre";

        protected override string TextoLblCbx2
            => "Filtrar por";

        protected override string TextoLblCbx3
            => "Filtrar por";

        protected override string TextoTitular
          => "Listado de las Marcas";

        #endregion
    }
}