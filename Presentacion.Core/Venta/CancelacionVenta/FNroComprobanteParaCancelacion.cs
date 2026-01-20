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
        public FNroComprobanteParaCancelacion(long logeadoID)
        {
            InitializeComponent();
            _usuarioLogeadoID = logeadoID;
            _ventaServicio = new VentaServicio();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Buscando comprobantes...");
            if (string.IsNullOrEmpty(txtNroComprobante.Text))
            {
                MessageBox.Show("Ingrese un número de comprobante.");
            }
            //buscar el/los comprobantes
            _comprobantesIDs = _ventaServicio.ObtenerComprobantesParaCancelacionPorNroComprobante(txtNroComprobante.Text.Trim());
            if(_comprobantesIDs.Count < 1)
            {
                MessageBox.Show("No se encontraron comprobantes para el número ingresado.");
            }
            var fGrillaDeComprobantes = new FGrillaDeComprobantes(_usuarioLogeadoID,_comprobantesIDs);
            fGrillaDeComprobantes.ShowDialog();
            this.Close();
        }
    }
}
