using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.Producto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public FProductoConsulta(IProductoServicio ProductoServicio)
        {
            _ProductoServicio = ProductoServicio;
        }
        public FProductoConsulta(bool _vieneDeCargaProducto) : this(new ProductoServicio())
        {
            vieneDeCargaProducto = _vieneDeCargaProducto;
            InitializeComponent();


        }

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            grilla.Columns["ProductoId"].Visible = false;
            grilla.Columns["ProductoId"].Name = "Id";

            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].HeaderText = "Producto";

            grilla.Columns["MarcaNombre"].Visible = true;
            grilla.Columns["MarcaNombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grilla.Columns["MarcaNombre"].HeaderText = "Marca";

            grilla.Columns["RubroNombre"].Visible = true;
            grilla.Columns["RubroNombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grilla.Columns["RubroNombre"].HeaderText = "Rubro";

            grilla.Columns["PrecioCosto"].Visible = true;
            grilla.Columns["PrecioCosto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grilla.Columns["PrecioCosto"].HeaderText = "Precio Costo";

            grilla.Columns["PrecioVenta"].Visible = true;
            grilla.Columns["PrecioVenta"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grilla.Columns["PrecioVenta"].HeaderText = "Precio Venta";

            grilla.Columns["Stock"].Visible = true;
            grilla.Columns["Stock"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grilla.Columns["Stock"].HeaderText = "Stock";

            grilla.Columns["Estado"].Visible = true;
            grilla.Columns["Estado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grilla.Columns["Estado"].HeaderText = "Estado";

        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        {

            base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

            if (check.Checked)
            {
                grilla.DataSource = _ProductoServicio.ObtenerProductosEliminados(cadenaBuscar);
                toolStrip.Enabled = false;
            }
            else
            {
                grilla.DataSource = _ProductoServicio.ObtenerProductos(cadenaBuscar);
                toolStrip.Enabled = true;
            }
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (puedeEjecutarComando)
            {
                var FormularioABMProducto = new FProductoABM(TipoOperacion.Eliminar, entidadID);
                FormularioABMProducto.ShowDialog();
                ActualizarSegunOperacion(FormularioABMProducto.RealizoAlgunaOperacion);
            }
        }

        private void ActualizarSegunOperacion(bool realizoOperacion)
        {
            if (realizoOperacion)
            {
                ActualizarDatos(dgvGrilla, string.Empty, cbxEstaEliminado, BarraLateralBotones);
            }
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (puedeEjecutarComando)
            {
                var FormularioABMProducto = new FProductoABM(TipoOperacion.Modificar, entidadID);
                FormularioABMProducto.ShowDialog();
                ActualizarSegunOperacion(FormularioABMProducto.RealizoAlgunaOperacion);
            }
        }

        public override void EjecutarBtnNuevo()
        {
            var FormularioABMProducto = new FProductoABM(TipoOperacion.Nuevo);
            FormularioABMProducto.ShowDialog();
            ActualizarSegunOperacion(FormularioABMProducto.RealizoAlgunaOperacion);
        }
        public void ControlCargaExistencaDatos()
        {
            if (dgvGrilla.RowCount > 0)
            {
                if (!entidadID.HasValue)
                {
                    MessageBox.Show("Por favor seleccione un registro.");
                    puedeEjecutarComando = false;
                    return;
                }
                else
                {
                    puedeEjecutarComando = true;
                }
            }
            else
            {
                MessageBox.Show("No hay Datos Cargados.");
            }
        }

        private void btnSeleccionarProducto_Click(object sender, EventArgs e)
        {
            ControlCargaExistencaDatos();
            if (!puedeEjecutarComando) return;

            productoSeleccionado = (long)entidadID;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FProductoConsulta_Load(object sender, EventArgs e)
        {
            if (vieneDeCargaProducto)
            {
                btnSeleccionarProducto.Visible = true;
                btnSeleccionarProducto.Enabled = true;
            }
            else
            {
                btnSeleccionarProducto.Visible = false;
                btnSeleccionarProducto.Enabled = false;
            }
        }

        private void btnGestionStock_Click(object sender, EventArgs e)
        {
            ControlCargaExistencaDatos();
            if (!puedeEjecutarComando) return;
            var formularioGestionStock = new FGestionStock(entidadID.Value);
            formularioGestionStock.ShowDialog();


        }
    }
}
