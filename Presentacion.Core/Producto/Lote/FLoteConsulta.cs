using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Producto;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Producto.Lote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Producto.Lote
{
    public partial class FLoteConsulta : FBaseConsulta
    {
        private readonly ILoteServicio _LoteServicio;
        private readonly IProductoServicio _ProductoServicio;
        public long? LoteSeleccionada = null;
        private bool vieneDeCargaLote = true;
        private long? productoId = null;
        public FLoteConsulta(ILoteServicio LoteServicio, IProductoServicio ProductoServicio)
        {
            _LoteServicio = LoteServicio;
            _ProductoServicio = ProductoServicio;
        }

        public FLoteConsulta(long productoLoteIid) : this(new LoteServicio(),new ProductoServicio())
        {
            InitializeComponent();
            productoId = productoLoteIid;

        }
        public FLoteConsulta(bool vieneDeCargaLote = true) : this(new LoteServicio(), new ProductoServicio())
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

        #region 🔥 ACCIONES PERSONALIZADAS DINAMICAS

        protected override void ConfigurarAccionesPersonalizadas()
        {
            if (vieneDeCargaLote)
            {

                // Seleccion ID Lote
                AgregarAccion(
                    "Seleccionar Lote",
                    SystemIcons.Information.ToBitmap(),
                    SeleccionLote,
                    false
                );
            }
        }

        private void SeleccionLote(long? id)
        {
            if (!id.HasValue) return;

            LoteSeleccionada = entidadID;
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region 🧱 GRILLA

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
                grilla.Columns["NumeroLote"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (grilla.Columns.Contains("NombreProducto"))
            {
                grilla.Columns["NombreProducto"].Visible = true;
                grilla.Columns["NombreProducto"].HeaderText = "Producto";
                grilla.Columns["NombreProducto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (grilla.Columns.Contains("FechaAlta"))
            {
                grilla.Columns["FechaAlta"].Visible = true;
                grilla.Columns["FechaAlta"].HeaderText = "Creado";
            }

            if (grilla.Columns.Contains("FechaVencimiento"))
            {
                grilla.Columns["FechaVencimiento"].Visible = true;
                grilla.Columns["FechaVencimiento"].HeaderText = "Vencimiento";
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
                grilla.Columns["EstaVencido"].DefaultCellStyle.NullValue = false;
                grilla.Columns["EstaVencido"].ReadOnly = true;
            }

            if (grilla.Columns.Contains("EstaActivo"))
            {
                grilla.Columns["EstaActivo"].Visible = true;
                grilla.Columns["EstaActivo"].HeaderText = "Activo";
                grilla.Columns["EstaActivo"].DefaultCellStyle.NullValue = false;
                grilla.Columns["EstaActivo"].ReadOnly = true;
            }
        }

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);
            string columnaBuscar = filtros.Extra as string ?? "Descripcion";

            var resultado = _LoteServicio.ObtenerLotes(filtros);

            dgv.DataSource = resultado.Items;

            // 🔴 CLAVE: volver a aplicar formato
            ResetearGrilla(dgv);

            var paginacion = new DatosPaginacion
            {
                PaginaActual = resultado.Page,
                PageSize = resultado.PageSize,
                CantidadRegistros = resultado.TotalRegistros,
            };

            ActualizarPaginacionUI(paginacion);

            BarraLateralBotones.Enabled = !filtros.VerEliminados;
        }
        #endregion

        #region 🧰 BOTONES BASE

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (!puedeEjecutarComando) return;

            var f = new FGestionStockLotes(TipoOperacion.Eliminar, entidadID); // deberia pasar tambien el producto? o directamente del service
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                RefrescarGrilla();
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (!puedeEjecutarComando) return;

            var f = new FGestionStockLotes(TipoOperacion.Modificar, entidadID); // deberia pasar tambien el producto? o directamente del service
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                RefrescarGrilla();
        }

        public override void EjecutarBtnNuevo()
        {
            var f = new FProductoConsulta(); //Abrimos Producto Consulta, de ahi arranca el ciclo para alta lote
            f.ShowDialog();
            
            RefrescarGrilla();
        }

        private void Recargar(bool realizoOperacion)
        {
            //if (realizoOperacion)
              //btnActualizar_Click_Base();
        }

        #endregion

        #region 🎯 SELECCIONAR


        #endregion

        private void FLoteConsulta_Load(object sender, EventArgs e)
        {
            if (productoId.HasValue)
            {
                var lotes = _LoteServicio.ObtenerLotesDeUnProducto((long)productoId);
                if (lotes != null && lotes.Any())
                {
                    dgvGrilla.DataSource = lotes;
                    ResetearGrilla(dgvGrilla);


                }
                else
                {
                    MessageBox.Show("No se ha encontrado un producto para mostrar sus lotes. Se muestran todos los lotes actuales", "Lote de producto no encontrado.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            
        }
        protected override void ConfigurarFiltrosUI()
        {

            base.ConfigurarFiltrosUI();

            ActivarFiltroEliminados("Mostrar movimientos eliminados.");

            var opciones = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todos", Valor = "" },
                new OpcionFiltro { Texto = "Número Lote", Valor = "NumeroLote" },
                new OpcionFiltro { Texto = "Descripción", Valor = "Descripcion" },
                new OpcionFiltro { Texto = "Producto", Valor = "Producto" }
            };

            ActivarFiltroCombo(opciones, "Texto", "Valor");

            ActivarFiltroFechas("Filtrar por fecha");

            var tiposFecha = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todas", Valor = "" },
                new OpcionFiltro { Texto = "Fecha Alta", Valor = ((int)TipoFiltroFechaLote.Alta).ToString() },
                new OpcionFiltro { Texto = "Vencimiento", Valor = ((int)TipoFiltroFechaLote.Vencimiento).ToString() }
            };

            ActivarComboOpcional(tiposFecha, "Texto", "Valor");

            cbx1.SelectedValue = "";
            cbxFiltroExtraEstado.SelectedValue = "";
        }

        protected override string ObtenerTextoLabelFiltroOpcional()
        {
            return "Buscar lote por:";
        }

        protected override string ObtenerTextoLabelFiltroExtra()
        {
            return "Tipo de fecha:";
        }

        protected override string ObtenerTextoLabelBusqueda()
        {
            return "Buscar lote:";
        }
        public override void EjecutarClickDerechoFila(long? id, Point pos)
        {
            //ejemplo
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
    }
}
