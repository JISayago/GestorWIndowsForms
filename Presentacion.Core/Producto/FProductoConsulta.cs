using PdfSharp;
using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.Core.Producto;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Producto;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Producto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FiltroConsulta = Servicios.Helpers.Sistema.FiltrosConsulta.FiltroConsulta;

namespace Presentacion.Core.Producto
{
    public partial class FProductoConsulta : FBaseConsulta
    {
        private readonly IProductoServicio _ProductoServicio;

        public long? productoSeleccionado = null;
        private bool vieneDeCargaProducto = false;
         
        // PAGINACIÓN 

        public FProductoConsulta() : this(new ProductoServicio())
        {
            InitializeComponent();
        }

        public FProductoConsulta(IProductoServicio productoServicio)
        {
            _ProductoServicio = productoServicio;
            InitializeComponent();
        }

        public FProductoConsulta(bool _vieneDeCargaProducto) : this(new ProductoServicio())
        {
            vieneDeCargaProducto = _vieneDeCargaProducto;
            InitializeComponent();
        }

        #region 🔧 CONFIG FILTROS

        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

            // 🔹 Filtro principal (columna de búsqueda)
            var opciones = new List<OpcionFiltro>
    {
        new OpcionFiltro { Texto = "Todos", Valor = "" },
        new OpcionFiltro { Texto = "Producto", Valor = "Descripcion" },
        new OpcionFiltro { Texto = "Marca", Valor = "MarcaNombre" },
        new OpcionFiltro { Texto = "Rubro", Valor = "RubroNombre" },
        new OpcionFiltro { Texto = "Código", Valor = "Codigo" }
    };

            ActivarFiltroCombo(opciones, "Texto", "Valor");
            cbxFiltroOpcional.SelectedValue = "";
            var opcionesEstado = new List<OpcionFiltro>
    {
        new OpcionFiltro { Texto = "Todos", Valor = "" }
    };

            foreach (EstadoProducto estado in Enum.GetValues(typeof(EstadoProducto)))
            {
                opcionesEstado.Add(new OpcionFiltro
                {
                    Texto = estado.ToString(),
                    Valor = ((int)estado).ToString()
                });
            }

            ActivarComboOpcional(opcionesEstado, "Texto", "Valor");

            if (cbxFiltroExtraEstado != null)
                cbxFiltroExtraEstado.SelectedValue = "";

        }

        #endregion

        #region 🔵 ACCIONES DINÁMICAS

        protected override void ConfigurarAccionesPersonalizadas()
        {
            AgregarAccion(
                "Stock",
                Constantes.Imagenes.ImgActualizar,
                AbrirGestionStock,
                true
            );

            if (vieneDeCargaProducto)
            {
                AgregarAccion(
                    "Seleccionar Producto",
                    Constantes.Imagenes.ImgPerfilUsuario,
                    SeleccionProducto,
                    true
                );
            }
        }

        private void AbrirGestionStock(long? id)
        {
            if (!id.HasValue) return;

            string nombreProducto = "";

            if (dgvGrilla.CurrentRow != null)
            {
                var celda = dgvGrilla.CurrentRow.Cells["Descripcion"];
                if (celda?.Value != null)
                    nombreProducto = celda.Value.ToString();
            }

            var controlPorLotes = dgvGrilla.CurrentRow.Cells["ControlPorLote"];

            if ((bool)controlPorLotes.Value)
            {
                var fLotes = new FGestionStockLotes(TipoOperacion.Nuevo, id.Value);
                fLotes.ShowDialog();

                if (fLotes.RealizoOperacion)
                    RefrescarGrilla();
            }
            else
            {
                var fStock = new FGestionStock(id.Value, nombreProducto);
                fStock.ShowDialog();

                if (fStock.RealizoOperacion)
                    RefrescarGrilla();
            }
        }

        private void SeleccionProducto(long? id)
        {
            if (!id.HasValue)
            {
                MessageBox.Show("Seleccione un registro.");
                return;
            }

            productoSeleccionado = entidadID;
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region 🔷 GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Contains("ProductoId"))
            {
                grilla.Columns["ProductoId"].Visible = false;
                grilla.Columns["ProductoId"].Name = "Id";
            }

            if (grilla.Columns.Contains("Descripcion"))
            {

            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].HeaderText = "Producto";
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (grilla.Columns.Contains("MarcaNombre"))
            {

            grilla.Columns["MarcaNombre"].Visible = true;
            grilla.Columns["MarcaNombre"].HeaderText = "Marca";
            grilla.Columns["MarcaNombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            if (grilla.Columns.Contains("RubroNombre"))
            {

            grilla.Columns["RubroNombre"].Visible = true;
            grilla.Columns["RubroNombre"].HeaderText = "Rubro";
            grilla.Columns["RubroNombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            if (grilla.Columns.Contains("PrecioCosto"))
            {
            grilla.Columns["PrecioCosto"].Visible = true;
            }
            if (grilla.Columns.Contains("PrecioVenta"))
            {

            grilla.Columns["PrecioVenta"].Visible = true;
            }
            if (grilla.Columns.Contains("Stock"))
            {

            grilla.Columns["Stock"].Visible = true;
            }
            if (grilla.Columns.Contains("Estado"))
            {
            grilla.Columns["Estado"].Visible = true;
            }

            if (grilla.Columns.Contains("ControlPorLote"))
            {

            grilla.Columns["ControlPorLote"].Visible = true;
            grilla.Columns["ControlPorLote"].HeaderText = "Control por Lote";
            grilla.Columns["ControlPorLote"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grilla.Columns["ControlPorLote"].DefaultCellStyle.NullValue = false;
            grilla.Columns["ControlPorLote"].ReadOnly = true;
            }
        }

        #endregion

        #region 🔥 DATOS

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            filtros.Extra ??= "Descripcion";

            var resultado = _ProductoServicio.ObtenerProductos(filtros);

            dgv.DataSource = resultado.Items;

            ResetearGrilla(dgv);

            var paginacion = new DatosPaginacion
            {
                PaginaActual = resultado.Page,
                PageSize = resultado.PageSize,
                CantidadRegistros = resultado.TotalRegistros
            };

            ActualizarPaginacionUI(paginacion);

            BarraLateralBotones.Enabled = !filtros.VerEliminados;
        }
        #endregion
        protected override FiltroConsulta ObtenerFiltros()
        {
            return new FiltroConsulta
            {
                TextoBuscar = txtBuscar.Text,
                VerEliminados = cbxEstaEliminado.Checked,

                FechaDesde = ObtenerFechaDesdeUI(),
                FechaHasta = ObtenerFechaHastaUI(),

                Extra = cbxFiltroOpcional?.SelectedValue,
                Extra2 = cbxFiltroExtraEstado?.SelectedValue,

                Page = paginaActual,
                PageSize = pageSize
            };
        }

        #region 🔷 BOTONES BASE

        public override void EjecutarBtnNuevo()
        {
            var f = new FProductoABM(TipoOperacion.Nuevo);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                RefrescarGrilla();
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (!puedeEjecutarComando) return;

            var f = new FProductoABM(TipoOperacion.Modificar, entidadID);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                RefrescarGrilla();
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (!puedeEjecutarComando) return;

            var f = new FProductoABM(TipoOperacion.Eliminar, entidadID);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                RefrescarGrilla();
        }

        #endregion

        #region 🔷 INTERACCIONES

        public override void EjecutarDobleClickFila(long? id)
        {
            // opcional
        }

        public override void EjecutarClickDerechoFila(long? id, Point pos)
        {
            if (!id.HasValue) return;

            ContextMenuStrip menu = new ContextMenuStrip();

            menu.Items.Add("Editar", null, (s, e) =>
            {
                var f = new FProductoABM(TipoOperacion.Modificar, id.Value);
                f.ShowDialog();
                RefrescarGrilla();
            });

            menu.Items.Add("Eliminar", null, (s, e) =>
            {
                MessageBox.Show("Eliminar " + id);
            });

            menu.Show(dgvGrilla, pos);
        }

        #endregion
    }
}