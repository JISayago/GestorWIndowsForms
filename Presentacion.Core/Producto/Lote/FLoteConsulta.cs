using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Producto;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Producto.Lote;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Producto.Lote
{
    public partial class FLoteConsulta : FBaseConsulta
    {
        private readonly ILoteServicio _loteServicio;
        private readonly IProductoServicio _productoServicio;

        public long? LoteSeleccionada = null;

        private bool vieneDeCargaLote = true;
        private long? productoId = null;

        public FLoteConsulta(
            ILoteServicio loteServicio,
            IProductoServicio productoServicio)
        {
            _loteServicio = loteServicio;
            _productoServicio = productoServicio;
        }

        public FLoteConsulta(long productoLoteId)
            : this(new LoteServicio(), new ProductoServicio())
        {
            InitializeComponent();

            productoId = productoLoteId;
        }

        public FLoteConsulta(bool vieneDeCargaLote = true)
            : this(new LoteServicio(), new ProductoServicio())
        {
            InitializeComponent();

            this.vieneDeCargaLote = vieneDeCargaLote;
        }

        #region INIT

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            Text = "Consulta de Lotes";
        }

        #endregion



        protected override string TextoLblBuscar
      => "Buscar Lote:";

        protected override string TextoLblCbx1
            => "Filtrar por Propiedad";

        protected override string TextoLblCbx2
            => "Filtrar por Estado";

        protected override string TextoLblCbx3
            => "Filtrar por Fecha";
        #region ACCIONES DINAMICAS
        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

            ActivarCheck(chkBool1, "Mostrar Vencidos");
            ActivarCheck(chkBool2, "Mostrar Todos los Lotes (histórico)");

            ActivarFiltroFechas("Filtrar por fecha");

            var opcionesBusqueda = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todos", Valor = "" },
                new OpcionFiltro { Texto = "Número Lote", Valor = "NumeroLote" },
                new OpcionFiltro { Texto = "Producto", Valor = "Producto" }
            };

            ActivarCombo(
                cbx1,
                lblcbx1,
                opcionesBusqueda,
                "Texto",
                "Valor",
                "Buscar por"
            );
            var estado = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todos", Valor = "" },
                new OpcionFiltro { Texto = "Activo", Valor = "Activo" },
                new OpcionFiltro { Texto = "Desactivado", Valor = "Desactivado" },
                new OpcionFiltro { Texto = "Vencido", Valor = "Vencido" },
                new OpcionFiltro { Texto = "Válido", Valor = "Valido" }
            };

            ActivarCombo(
                cbx2,
                lblcbx2,
                estado,
                "Texto",
                "Valor",
                "Estado Lote"
            );

            var tiposFecha = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todos", Valor = "" },
                new OpcionFiltro { Texto = "Fecha Alta", Valor = "Alta" },
                new OpcionFiltro { Texto = "Fecha Vencimiento", Valor = "Vencimiento" }
            };

            ActivarCombo(
                cbx3,
                lblcbx3,
                tiposFecha,
                "Texto",
                "Valor",
                "Tipo Fecha"
            );

            cbx1.SelectedValue = "";
            cbx2.SelectedValue = "";
            cbx3.SelectedValue = "";
        }
        protected override void AccionCheck2()
        {
            if (chkBool2.Checked)
            {
                _actualizandoFiltros = true;

                chkBool1.Checked = false;

                _actualizandoFiltros = false;

                LimpiarFiltrosEspeciales();
            }

            paginaActual = 1;

            var filtros = ObtenerFiltros();

            ActualizarDatos(dgvGrilla, filtros);
        }
        protected override void AccionCheck1()
        {
            if (chkBool1.Checked)
            {
                _actualizandoFiltros = true;

                chkBool2.Checked = false;

                _actualizandoFiltros = false;

                LimpiarFiltrosEspeciales();
            }

            paginaActual = 1;

            var filtros = ObtenerFiltros();

            ActualizarDatos(dgvGrilla, filtros);
        }

        private void LimpiarFiltrosEspeciales()
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
        #endregion

        #region ACCIONES PERSONALIZADAS

        protected override void ConfigurarAccionesPersonalizadas()
        {
            if (vieneDeCargaLote)
            {
                AgregarAccion(
                    "Seleccionar Lote",
                    SystemIcons.Information.ToBitmap(),
                    SeleccionLote,
                    true
                );
            }
        }

        private void SeleccionLote(long? id)
        {
            if (!id.HasValue)
                return;

            LoteSeleccionada = entidadID;

            DialogResult = DialogResult.OK;

            Close();
        }

        #endregion

        #region DATOS

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            ResultadoPaginacion<LoteDTO> resultado;

            if (productoId.HasValue)
            {
                var lotes = _loteServicio.ObtenerLotesDeUnProducto(productoId.Value);

                dgv.DataSource = lotes;

                ResetearGrilla(dgv);

                ActualizarPaginacionUI(new DatosPaginacion
                {
                    PaginaActual = 1,
                    CantidadRegistros = lotes.Count(),
                    PageSize = lotes.Count(),
                });

                return;
            }

            resultado = _loteServicio.ObtenerLotes(filtros);

            dgv.DataSource = resultado.Items;

            ResetearGrilla(dgv);

            var paginacion = new DatosPaginacion
            {
                PaginaActual = resultado.Page,
                PageSize = resultado.PageSize,
                CantidadRegistros = resultado.TotalRegistros,
            };

            ActualizarPaginacionUI(paginacion);
        }

        #endregion

        #region GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            grilla.ReadOnly = true;

            if (grilla.Columns.Count == 0)
                return;

            grilla.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            // ID
            if (grilla.Columns.Contains("Id"))
            {
                grilla.Columns["Id"].Visible = false;
            }

            // ID PRODUCTO
            if (grilla.Columns.Contains("IdProducto"))
            {
                grilla.Columns["IdProducto"].Visible = false;
            }

            // NUMERO LOTE
            if (grilla.Columns.Contains("NumeroLote"))
            {
                var col = grilla.Columns["NumeroLote"];

                col.Visible = true;
                col.HeaderText = "Número Lote";

                col.FillWeight = 120;
                col.MinimumWidth = 120;
            }

            // PRODUCTO
            if (grilla.Columns.Contains("NombreProducto"))
            {
                var col = grilla.Columns["NombreProducto"];

                col.Visible = true;
                col.HeaderText = "Producto";

                col.FillWeight = 220;
                col.MinimumWidth = 220;
            }

            // DESCRIPCION
            if (grilla.Columns.Contains("Descripcion"))
            {
                var col = grilla.Columns["Descripcion"];

                col.Visible = true;
                col.HeaderText = "Descripción";

                col.FillWeight = 180;
                col.MinimumWidth = 180;
            }

            // FECHA ALTA
            if (grilla.Columns.Contains("FechaAlta"))
            {
                var col = grilla.Columns["FechaAlta"];

                col.Visible = true;
                col.HeaderText = "Fecha Alta";

                col.DefaultCellStyle.Format = "dd/MM/yyyy";

                col.FillWeight = 90;
                col.MinimumWidth = 90;
            }

            // FECHA VENCIMIENTO
            if (grilla.Columns.Contains("FechaVencimiento"))
            {
                var col = grilla.Columns["FechaVencimiento"];

                col.Visible = true;
                col.HeaderText = "Fecha Vencimiento";

                col.DefaultCellStyle.Format = "dd/MM/yyyy";

                col.FillWeight = 110;
                col.MinimumWidth = 110;
            }

            // ESTADO VENCIMIENTO DESCRIPCION
            if (grilla.Columns.Contains("EstaVencidoDescripcion"))
            {
                var col = grilla.Columns["EstaVencidoDescripcion"];

                col.Visible = true;
                col.HeaderText = "Estado Vencimiento";

                col.FillWeight = 150;
                col.MinimumWidth = 150;
            }

            // ESTADO ACTIVO DESCRIPCION
            if (grilla.Columns.Contains("EstaActivoDescripcion"))
            {
                var col = grilla.Columns["EstaActivoDescripcion"];

                col.Visible = true;
                col.HeaderText = "Estado";

                col.FillWeight = 100;
                col.MinimumWidth = 100;
            }

            // STOCK INICIAL
            if (grilla.Columns.Contains("StockInicial"))
            {
                var col = grilla.Columns["StockInicial"];

                col.Visible = true;
                col.HeaderText = "Stock Inicial";

                col.DefaultCellStyle.Format = "N2";

                col.FillWeight = 90;
                col.MinimumWidth = 90;
            }

            // STOCK ACTUAL
            if (grilla.Columns.Contains("StockActual"))
            {
                var col = grilla.Columns["StockActual"];

                col.Visible = true;
                col.HeaderText = "Stock Actual";

                col.DefaultCellStyle.Format = "N2";

                col.FillWeight = 90;
                col.MinimumWidth = 90;
            }

            // CAMPOS BOOLEANOS CRUDOS
            if (grilla.Columns.Contains("EstaVencido"))
            {
                grilla.Columns["EstaVencido"].Visible = false;
            }

            if (grilla.Columns.Contains("EstaActivo"))
            {
                grilla.Columns["EstaActivo"].Visible = false;
            }
        }
        #endregion

        #region BOTONES BASE

        public override void EjecutarBtnNuevo()
        {
            var f = new FProductoConsulta();

            f.ShowDialog();

            RefrescarGrilla();
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();

            if (!puedeEjecutarComando)
                return;

            var f = new FGestionStockLotes(
                TipoOperacion.Modificar,
                entidadID);

            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                RefrescarGrilla();
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();

            if (!puedeEjecutarComando)
                return;

            var f = new FGestionStockLotes(
                TipoOperacion.Eliminar,
                entidadID);

            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                RefrescarGrilla();
        }

        #endregion

        #region MENU CONTEXTUAL

        public override void EjecutarClickDerechoFila(long? id, Point pos)
        {
            if (!id.HasValue)
                return;

            ContextMenuStrip menu = new ContextMenuStrip();

            menu.Items.Add("Modificar", null, (s, e) =>
            {
                var f = new FGestionStockLotes(
                    TipoOperacion.Modificar,
                    id.Value);

                f.ShowDialog();

                RefrescarGrilla();
            });

            menu.Items.Add("Eliminar", null, (s, e) =>
            {
                var f = new FGestionStockLotes(
                    TipoOperacion.Eliminar,
                    id.Value);

                f.ShowDialog();

                RefrescarGrilla();
            });

            menu.Show(dgvGrilla, pos);
        }

        #endregion
    }
}