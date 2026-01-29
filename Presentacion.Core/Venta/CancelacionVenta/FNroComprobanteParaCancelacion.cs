using Presentacion.Core.Venta.CancelacionVenta;
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

namespace Presentacion.Core.Venta
{
    public partial class FNroComprobanteParaCancelacion : Form
    {
        private IVentaServicio _ventaServicio;
        private long _usuarioLogeadoID;
        private List<long> _comprobantesIDs;
        private bool _filtrarPorNroComprobante = false;
        public FNroComprobanteParaCancelacion(long logeadoID)
        {
            InitializeComponent();
            _usuarioLogeadoID = logeadoID;
            _ventaServicio = new VentaServicio();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var fecha = dtpFecha.Value.Date;
            var filtroNumero = txtNroComprobante.Text?.Trim();

            var comprobantes = _ventaServicio
                .ObtenerVentasParaCancelacion(fecha, filtroNumero);

            if (!comprobantes.Any())
            {
                MessageBox.Show("No se encontraron ventas para la fecha seleccionada.");
                return;
            }

            var fGrilla = new FGrillaDeComprobantes(
                _usuarioLogeadoID,
                comprobantes,
                filtroNumero
            );

            fGrilla.ShowDialog();
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbxSeleccionNroComprobante_CheckedChanged(object sender, EventArgs e)
        {
            txtNroComprobante.Enabled = cbxSeleccionNroComprobante.Checked;
        }

        private void FNroComprobanteParaCancelacion_Load(object sender, EventArgs e)
        {
            cbxSeleccionNroComprobante.Checked = false;
            txtNroComprobante.Enabled = false;
        }
    }
}
