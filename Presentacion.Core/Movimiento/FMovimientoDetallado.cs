using AccesoDatos.Entidades;
using Servicios.LogicaNegocio.Cliente;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.Movimiento;
using Servicios.LogicaNegocio.Movimiento.DTO;
using Servicios.LogicaNegocio.Venta;
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
        public MovimientoDTO movimiento;

        public FMovimientoDetallado(long? entidadId)
        {
            entidadID = (long)entidadId;

            var movimientoService = new MovimientoServicio();
            var ventaService = new VentaServicio();
            var empleadoService = new EmpleadoServicio();

            movimiento = movimientoService.ObtenerMovimientoPorId(entidadID);
            var venta = movimiento.IdVenta.HasValue ? ventaService.ObtenerVentaPorId(movimiento.IdVenta.Value) : null;
            var empleado = venta != null ? empleadoService.ObtenerEmpleadoPorId(venta.IdEmpleado) : null;

            InitializeComponent();

            lblMontoMovimiento.Text = movimiento.Monto.ToString() + "$";
            lblNumeroMovimiento.Text = movimiento.NumeroMovimiento;
            lblTipoMovimiento.Text = movimiento.TipoMovimiento == 1 ? "Ingreso" : "Egreso";
            lblFechaMovimiento.Text = movimiento.FechaMovimiento.ToString();

            lblNombreEmpleado.Text = empleado.Nombre;
            txtDetalle.Text = venta.Detalle;


        }

        private void tabPageVenta1_Click(object sender, EventArgs e)
        {
        }
    }
}
