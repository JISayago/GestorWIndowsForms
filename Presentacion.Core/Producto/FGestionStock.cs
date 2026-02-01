using Presentacion.Core.Venta;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Producto.DTO;
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
    public partial class FGestionStock : Form
    {
        private readonly IProductoServicio _productoServicio;
        private readonly long _productoID;
        private int tipoMovimientoStock = 0;
        private decimal monto = 0;

        public FGestionStock(long productoID)
        {

            InitializeComponent();
            _productoServicio = new ProductoServicio();
            _productoID = productoID;
        }

        private void FGestionStock_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdbAgregar_CheckedChanged(object sender, EventArgs e)
        {

            if (rdbAgregar.Checked)
                tipoMovimientoStock = 1;
            lblMotivo.Text = "Motivo de la carga de Stock:";
            lblDetalle.Text = "Detalle de la carga de Stock:";
            btnAccion.Text = "Agregar Stock";
        }

        private void rdbQuitar_CheckedChanged(object sender, EventArgs e)
        {

            if (rdbQuitar.Checked)
                tipoMovimientoStock = 2;
            lblMotivo.Text = "Motivo de la baja de Stock:";
            lblDetalle.Text = "Detalle de la baja de Stock:";
            btnAccion.Text = "Quitar Stock";
        }

        private void btnAccion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCantidad.Text) || Convert.ToDecimal(txtCantidad.Text) <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida mayor a cero.");
                return;
            }
            if(string.IsNullOrEmpty(txtMotivo.Text))
            {
                MessageBox.Show("Ingrese un motivo para la modificación de stock.");
                return;
            }
            var mstock = new MovilizacionStockDTO
            {
                Monto = Convert.ToDecimal(txtCantidad.Text),
                TipoMovimientoStock = tipoMovimientoStock,
                ProductoId = _productoID,
                Motivo = txtMotivo.Text

            };
            
            if (tipoMovimientoStock == 0)
            {
                MessageBox.Show("Seleccione una acción: Agregar o Quitar Stock.");
                return;
            }
            var respuesta = _productoServicio.AgregarQuitarStock(mstock);
            if (respuesta.Exitoso)
            {
                MessageBox.Show($"{respuesta.Mensaje}", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show($"{respuesta.Mensaje}", "Operación Fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
