using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Producto;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.Producto
{
    public partial class FProductoConsulta : FBaseConsulta
    {
        private readonly IProductoServicio _ProductoServicio;

        public long? productoSeleccionado = null;
        private bool vieneDeCargaProducto = false;

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

        #region 🔵 ACCIONES DINÁMICAS EXTRA

        protected override void ConfigurarAccionesPersonalizadas()
        {
            // BOTON STOCK
            AgregarAccion(
                "Stock",
                Constantes.Imagenes.ImgActualizar,
                AbrirGestionStock,
                true
            );

            // BOTON PRECIOS (ejemplo)
            AgregarAccion(
                "Precios",
                Constantes.Imagenes.ImgCerrar,
                AbrirGestionPrecios,
                true
            );
        }

        private void AbrirGestionStock(long? id)
        {
            if (!id.HasValue) return;

            var f = new FGestionStock(id.Value);
            f.ShowDialog();

            if (f.RealizoOperacion)
                Recargar();
        }

        private void AbrirGestionPrecios(long? id)
        {
            if (!id.HasValue) return;

           // var f = new FProductoPrecios(id.Value);
            //f.ShowDialog();

           // if (f.RealizoOperacion)
             //   Recargar();
        }

        #endregion

        #region 🔷 GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (!grilla.Columns.Contains("ProductoId"))
                return;

            grilla.Columns["ProductoId"].Visible = false;
            grilla.Columns["ProductoId"].Name = "Id";

            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].HeaderText = "Producto";
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            grilla.Columns["MarcaNombre"].Visible = true;
            grilla.Columns["MarcaNombre"].HeaderText = "Marca";
            grilla.Columns["MarcaNombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grilla.Columns["RubroNombre"].Visible = true;
            grilla.Columns["RubroNombre"].HeaderText = "Rubro";
            grilla.Columns["RubroNombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grilla.Columns["PrecioCosto"].Visible = true;
            grilla.Columns["PrecioVenta"].Visible = true;
            grilla.Columns["Stock"].Visible = true;
            grilla.Columns["Estado"].Visible = true;
        }

        #endregion

        #region 🔥 ACTUALIZAR DATOS (CON FILTRO NUEVO)

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            if (filtros.VerEliminados)
            {
                dgv.DataSource = _ProductoServicio.ObtenerProductosEliminados(filtros.TextoBuscar);
                BarraLateralBotones.Enabled = false;
            }
            else
            {
                dgv.DataSource = _ProductoServicio.ObtenerProductos(filtros.TextoBuscar);
                BarraLateralBotones.Enabled = true;
            }
        }

        #endregion

        #region 🔷 BOTONES BASE

        public override void EjecutarBtnNuevo()
        {
            var f = new FProductoABM(TipoOperacion.Nuevo);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (!puedeEjecutarComando) return;

            var f = new FProductoABM(TipoOperacion.Modificar, entidadID);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (!puedeEjecutarComando) return;

            var f = new FProductoABM(TipoOperacion.Eliminar, entidadID);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        private void Recargar()
        {
            //btnActualizar_Click_Base();
        }

        #endregion

        #region 🔷 SELECCIONAR PRODUCTO (MODO PICKER)

        private void btnSeleccionarProducto_Click(object sender, EventArgs e)
        {
            if (!entidadID.HasValue)
            {
                MessageBox.Show("Seleccione un registro.");
                return;
            }

            productoSeleccionado = entidadID;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FProductoConsulta_Load(object sender, EventArgs e)
        {
            btnSeleccionarProducto.Visible = vieneDeCargaProducto;
        }

        #endregion
    }
}
