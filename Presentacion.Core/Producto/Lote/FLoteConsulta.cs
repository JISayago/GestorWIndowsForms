using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Producto;
using Servicios.Helpers.Sistema.FiltrosConsulta;
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
        public long? LoteSeleccionada = null;
        private bool vieneDeCargaLote = true;

        public FLoteConsulta(bool vieneDeCargaLote = true) : this(new LoteServicio())
        {
            InitializeComponent();
            this.vieneDeCargaLote = vieneDeCargaLote;
        }

        public FLoteConsulta(ILoteServicio LoteServicio)
        {
            _LoteServicio = LoteServicio;
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

            if (!grilla.Columns.Contains("NumeroLote")) return;

            grilla.Columns["NumeroLote"].Visible = true;
            grilla.Columns["NumeroLote"].HeaderText = "Numero Lote";
            grilla.Columns["NumeroLote"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            grilla.Columns["NombreProducto"].Visible = true;
            grilla.Columns["NombreProducto"].HeaderText = "Producto";
            grilla.Columns["NombreProducto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            grilla.Columns["FechaAlta"].Visible = true;
            grilla.Columns["FechaAlta"].HeaderText = "Creado";
            grilla.Columns["FechaAlta"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            grilla.Columns["FechaVencimiento"].Visible = true;
            grilla.Columns["FechaVencimiento"].HeaderText = "Vencimiento";
            grilla.Columns["FechaVencimiento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            grilla.Columns["StockInicial"].Visible = true;
            grilla.Columns["StockInicial"].HeaderText = "Stock Incial";
            grilla.Columns["StockInicial"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            grilla.Columns["StockActual"].Visible = true;
            grilla.Columns["StockActual"].HeaderText = "Stock Actual";
            grilla.Columns["StockActual"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            grilla.Columns["EstaVencido"].Visible = true;
            grilla.Columns["EstaVencido"].HeaderText = "Vencido";
            grilla.Columns["EstaVencido"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["EstaVencido"].DefaultCellStyle.NullValue = false;
            grilla.Columns["EstaVencido"].ReadOnly = true;

            grilla.Columns["EstaActivo"].Visible = true;
            grilla.Columns["EstaActivo"].HeaderText = "Activo";
            grilla.Columns["EstaActivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["EstaActivo"].DefaultCellStyle.NullValue = false;
            grilla.Columns["EstaActivo"].ReadOnly = true;
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

            Recargar(f.RealizoAlgunaOperacion);
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (!puedeEjecutarComando) return;

            var f = new FGestionStockLotes(TipoOperacion.Modificar, entidadID); // deberia pasar tambien el producto? o directamente del service
            f.ShowDialog();

            Recargar(f.RealizoAlgunaOperacion);
        }

        public override void EjecutarBtnNuevo()
        {
            var f = new FProductoConsulta(); //Abrimos Producto Consulta, de ahi arranca el ciclo para alta lote
            f.ShowDialog();

            //Recargar(f.RealizoAlgunaOperacion);
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
        }
        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

            // 🔵 combo búsqueda
            var opciones = new List<OpcionFiltro>
    {
        new OpcionFiltro { Texto = "Número Lote", Valor = "NumeroLote" },
        new OpcionFiltro { Texto = "Descripción", Valor = "Descripcion" },
        new OpcionFiltro { Texto = "Producto", Valor = "Producto" }
    };

            ActivarFiltroCombo(opciones, "Texto", "Valor");

            // 🔵 fechas (clave para lotes)
            ActivarFiltroFechas("Filtrar por fecha");

            // 🔵 combo tipo fecha (Alta / Vencimiento)
    //        var tiposFecha = new List<OpcionFiltro>
    //{
    //    new OpcionFiltro { Texto = "Ninguno", Valor = TipoFiltroFecha.Ninguno },
    //    new OpcionFiltro { Texto = "Fecha Alta", Valor = TipoFiltroFecha.Alta },
    //    new OpcionFiltro { Texto = "Vencimiento", Valor = TipoFiltroFecha.Vencimiento }
    //};

    //        ActivarComboOpcional(tiposFecha, "Texto", "Valor");

            // 👉 default útil
            cbxFiltroOpcional.SelectedValue = "NumeroLote";
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
