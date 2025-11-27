using AccesoDatos.Entidades;
using Servicios.LogicaNegocio.Cliente;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.Movimiento;
using Servicios.LogicaNegocio.Movimiento.DTO;
using Servicios.LogicaNegocio.Venta;
using Servicios.LogicaNegocio.Venta.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Movimiento
{
    public partial class FMovimientoDetallado : Form
    {
        public long entidadID;
        public long? venta;

        public FMovimientoDetallado(long? entidadId)
        {
            entidadID = (long)entidadId;

            var movimientoService = new MovimientoServicio();
            
            InitializeComponent();

            var info = movimientoService.CargarDatosMovimiento(entidadID);

            lblMontoMovimiento.Text = info.movimiento.Monto.ToString() + "$";
            lblNumeroMovimiento.Text = info.movimiento.NumeroMovimiento;
            lblTipoMovimiento.Text = info.movimiento.TipoMovimiento == 1 ? "Ingreso" : "Egreso";
            lblFechaMovimiento.Text = info.movimiento.FechaMovimiento.ToString();

            txtDetalle.Text = info.venta.Detalle;

            lblNombreEmpleado.Text = info.empleado.Nombre;

            dgvProductos.ReadOnly = true;
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "codigo",
                HeaderText = "Codigo",
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "descripcion",
                HeaderText = "Nombre del producto",
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "PrecioCosto",
                HeaderText = "Precio C",
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "PrecioVenta",
                HeaderText = "Precio V",
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "MarcaNombre",
                HeaderText = "Marca",
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "RubroNombre",
                HeaderText = "Rubro",
            });

            dgvProductos.DataSource = info.productos;

            //AGREGAR CANTIDAD DE PRODUCTOS VENDIDOS EN LA GRILLA
        }
    }
}
