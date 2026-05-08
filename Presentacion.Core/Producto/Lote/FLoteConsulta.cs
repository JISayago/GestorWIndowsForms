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

        #region CONFIG FILTROS

        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

            ActivarCheck(chkBool1, "Ver eliminados");

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

            var tiposFecha = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Fecha Alta", Valor = "Alta" },
                new OpcionFiltro { Texto = "Fecha Vencimiento", Valor = "Vencimiento" }
            };

            ActivarCombo(
                cbx2,
                lblcbx2,
                tiposFecha,
                "Texto",
                "Valor",
                "Tipo Fecha"
            );

            cbx1.SelectedValue = "";
            cbx2.SelectedValue = "Alta";
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

            if (grilla.Columns.Contains("LoteId"))
            {
                grilla.Columns["LoteId"].Visible = false;
                grilla.Columns["LoteId"].Name = "Id";
            }

            if (grilla.Columns.Contains("NumeroLote"))
            {
                grilla.Columns["NumeroLote"].Visible = true;
                grilla.Columns["NumeroLote"].HeaderText = "Número Lote";
                grilla.Columns["NumeroLote"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
            }

            if (grilla.Columns.Contains("NombreProducto"))
            {
                grilla.Columns["NombreProducto"].Visible = true;
                grilla.Columns["NombreProducto"].HeaderText = "Producto";
                grilla.Columns["NombreProducto"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
            }

            if (grilla.Columns.Contains("FechaAlta"))
            {
                grilla.Columns["FechaAlta"].Visible = true;
                grilla.Columns["FechaAlta"].HeaderText = "Creado";
                grilla.Columns["FechaAlta"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (grilla.Columns.Contains("FechaVencimiento"))
            {
                grilla.Columns["FechaVencimiento"].Visible = true;
                grilla.Columns["FechaVencimiento"].HeaderText = "Vencimiento";
                grilla.Columns["FechaVencimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (grilla.Columns.Contains("StockInicial"))
            {
                grilla.Columns["StockInicial"].Visible = true;
                grilla.Columns["StockInicial"].HeaderText = "Stock Inicial";
            }

            if (grilla.Columns.Contains("StockActual"))
            {
                grilla.Columns["StockActual"].Visible = true;
                grilla.Columns["StockActual"].HeaderText = "Stock Actual";
            }

            if (grilla.Columns.Contains("EstaVencido"))
            {
                grilla.Columns["EstaVencido"].Visible = true;
                grilla.Columns["EstaVencido"].HeaderText = "Vencido";
                grilla.Columns["EstaVencido"].ReadOnly = true;
            }

            if (grilla.Columns.Contains("EstaActivo"))
            {
                grilla.Columns["EstaActivo"].Visible = true;
                grilla.Columns["EstaActivo"].HeaderText = "Activo";
                grilla.Columns["EstaActivo"].ReadOnly = true;
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