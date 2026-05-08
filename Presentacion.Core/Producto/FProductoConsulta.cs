using PdfSharp;
using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.Core.Producto.Lote;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Producto;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Articulo.Marca;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Producto.Rubro;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Producto
{
    public partial class FProductoConsulta : FBaseConsulta
    {
        private readonly IProductoServicio _ProductoServicio;
        private readonly IMarcaServicio _marcaServicio;
        private readonly IRubroServicio _rubroServicio;

        public long? productoSeleccionado = null;

        private bool vieneDeCargaProducto = false;

        public FProductoConsulta() : this(new ProductoServicio())
        {
            InitializeComponent();
        }

        public FProductoConsulta(IProductoServicio productoServicio)
        {
            _ProductoServicio = productoServicio;

            _marcaServicio = new MarcaServicio();
            _rubroServicio = new RubroServicio();

            InitializeComponent();
        }

        public FProductoConsulta(bool _vieneDeCargaProducto) : this(new ProductoServicio())
        {
            vieneDeCargaProducto = _vieneDeCargaProducto;

            InitializeComponent();
        }

        #region FILTROS

        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

            var filtrosBusqueda = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todos", Valor = "" },
                new OpcionFiltro { Texto = "Producto", Valor = "Descripcion" },
                new OpcionFiltro { Texto = "Marca", Valor = "MarcaNombre" },
                new OpcionFiltro { Texto = "Rubro", Valor = "RubroNombre" },
                new OpcionFiltro { Texto = "Código", Valor = "Codigo" }
            };

            ActivarCombo(
                cbx1,
                lblcbx1,
                filtrosBusqueda,
                "Texto",
                "Valor",
                "Buscar por"
            );

            cbx1.SelectedValue = "";

            var estados = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todos", Valor = "" }
            };

            foreach (EstadoProducto estado in Enum.GetValues(typeof(EstadoProducto)))
            {
                estados.Add(new OpcionFiltro
                {
                    Texto = estado.ToString(),
                    Valor = ((int)estado).ToString()
                });
            }

            ActivarCombo(
                cbx2,
                lblcbx2,
                estados,
                "Texto",
                "Valor",
                "Estado"
            );

            cbx2.SelectedValue = "";

            var marcas = _marcaServicio
                .ObtenerMarcas(new FiltroConsulta
                {
                    Page = 1,
                    PageSize = int.MaxValue
                })
                .Items
                .Select(x => new OpcionFiltro
                {
                    Texto = x.Nombre,
                    Valor = x.Id.ToString()
                })
                .ToList();

            marcas.Insert(0, new OpcionFiltro
            {
                Texto = "Todas",
                Valor = ""
            });

            ActivarCombo(
                cbx3,
                lblcbx3,
                marcas,
                "Texto",
                "Valor",
                "Marca"
            );

            cbx3.SelectedValue = "";

            ActivarCheck(chkBool1, "Ver eliminados");
        }

        #endregion

        #region ACCIONES DINAMICAS

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
            if (!id.HasValue)
                return;

            if (dgvGrilla.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            string nombreProducto = string.Empty;

            var celdaDescripcion = dgvGrilla.CurrentRow.Cells["Descripcion"];

            if (celdaDescripcion?.Value != null)
                nombreProducto = celdaDescripcion.Value.ToString();

            var controlPorLote = dgvGrilla.CurrentRow.Cells["ControlPorLote"];

            if (controlPorLote?.Value != null &&
                Convert.ToBoolean(controlPorLote.Value))
            {
                var fLotes = new FGestionStockLotes(
                    TipoOperacion.Nuevo,
                    id.Value
                );

                fLotes.ShowDialog();

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

        #region DATOS

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

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

            BarraLateralBotones.Enabled = !filtros.Bool1;
        }

        #endregion

        #region GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            grilla.ReadOnly = true;

            if (grilla.Columns.Contains("ProductoId"))
            {
                grilla.Columns["ProductoId"].Visible = false;
                grilla.Columns["ProductoId"].Name = "Id";
            }

            if (grilla.Columns.Contains("Descripcion"))
            {
                grilla.Columns["Descripcion"].Visible = true;
                grilla.Columns["Descripcion"].HeaderText = "Producto";
                grilla.Columns["Descripcion"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
            }

            if (grilla.Columns.Contains("MarcaNombre"))
            {
                grilla.Columns["MarcaNombre"].Visible = true;
                grilla.Columns["MarcaNombre"].HeaderText = "Marca";
                grilla.Columns["MarcaNombre"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
            }

            if (grilla.Columns.Contains("RubroNombre"))
            {
                grilla.Columns["RubroNombre"].Visible = true;
                grilla.Columns["RubroNombre"].HeaderText = "Rubro";
                grilla.Columns["RubroNombre"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
            }

            if (grilla.Columns.Contains("PrecioCosto"))
            {
                grilla.Columns["PrecioCosto"].Visible = true;
                grilla.Columns["PrecioCosto"].HeaderText = "Costo";
                grilla.Columns["PrecioCosto"].DefaultCellStyle.Format = "C2";
            }

            if (grilla.Columns.Contains("PrecioVenta"))
            {
                grilla.Columns["PrecioVenta"].Visible = true;
                grilla.Columns["PrecioVenta"].HeaderText = "Venta";
                grilla.Columns["PrecioVenta"].DefaultCellStyle.Format = "C2";
            }

            if (grilla.Columns.Contains("Stock"))
            {
                grilla.Columns["Stock"].Visible = true;
                grilla.Columns["Stock"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
            }

            if (grilla.Columns.Contains("Estado"))
            {
                grilla.Columns["Estado"].Visible = true;
                grilla.Columns["Estado"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
            }

            if (grilla.Columns.Contains("ControlPorLote"))
            {
                grilla.Columns["ControlPorLote"].Visible = true;
                grilla.Columns["ControlPorLote"].HeaderText =
                    "Control por Lote";

                grilla.Columns["ControlPorLote"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;

                grilla.Columns["ControlPorLote"].DefaultCellStyle.NullValue = false;

                grilla.Columns["ControlPorLote"].ReadOnly = true;
            }
        }

        #endregion

        #region BOTONES BASE

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

            if (!puedeEjecutarComando)
                return;

            var f = new FProductoABM(
                TipoOperacion.Modificar,
                entidadID
            );

            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                RefrescarGrilla();
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();

            if (!puedeEjecutarComando)
                return;

            var f = new FProductoABM(
                TipoOperacion.Eliminar,
                entidadID
            );

            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                RefrescarGrilla();
        }

        #endregion

        #region INTERACCIONES

        public override void EjecutarDobleClickFila(long? id)
        {

        }

        public override void EjecutarClickDerechoFila(long? id, Point pos)
        {
            if (!id.HasValue)
                return;

            ContextMenuStrip menu = new ContextMenuStrip();

            menu.Items.Add("Editar", null, (s, e) =>
            {
                var f = new FProductoABM(
                    TipoOperacion.Modificar,
                    id.Value
                );

                f.ShowDialog();

                RefrescarGrilla();
            });

            menu.Items.Add("Eliminar", null, (s, e) =>
            {
                var f = new FProductoABM(
                    TipoOperacion.Eliminar,
                    id.Value
                );

                f.ShowDialog();

                RefrescarGrilla();
            });

            menu.Items.Add("Detalle del Lote", null, (s, e) =>
            {
                var f = new FLoteConsulta(id.Value);

                f.ShowDialog();
            });

            menu.Show(dgvGrilla, pos);
        }

        #endregion
    }
}