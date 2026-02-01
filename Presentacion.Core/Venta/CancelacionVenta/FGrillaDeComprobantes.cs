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

namespace Presentacion.Core.Venta.CancelacionVenta
{
    public partial class FGrillaDeComprobantes : Form
    {
        private List<long> _comprobantesIDs;
        private long _usuarioLogeadoID;
        private List<VentaDTO> ventasConMismoNumero;
        private readonly IVentaServicio _ventaServicio;
        private string _nroComprobante;
        public FGrillaDeComprobantes(long logeadiId, List<long> comprobantesIDs, string nroComprobante)
        {
            InitializeComponent();
            _usuarioLogeadoID = logeadiId;
            _comprobantesIDs = comprobantesIDs;
            _nroComprobante = nroComprobante;
            _ventaServicio = new VentaServicio();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (dgvComprobantes.CurrentRow == null)
                return;

            var venta = (VentaDTO)dgvComprobantes.CurrentRow.DataBoundItem;

            var fVenta = new FVenta(_usuarioLogeadoID, venta.VentaId);
            fVenta.Show();
            Close();
        }
        private void FGrillaDeComprobantes_Load(object sender, EventArgs e)
        {
            // Optimización UX: si hay 1 sola, abrir directo
            if (_comprobantesIDs.Count == 1)
            {
                var ventaId = _comprobantesIDs[0];

                var fVenta = new FVenta(_usuarioLogeadoID, ventaId);
                fVenta.Show();
                Close();
                return;
            }

            // Caso normal: grilla
            var ventas = _ventaServicio
                .ObtenerVentasPorIds(_comprobantesIDs);

            if (!ventas.Any())
            {
                MessageBox.Show(
                    "No se encontraron comprobantes.",
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                Close();
                return;
            }

            dgvComprobantes.AutoGenerateColumns = true;
            dgvComprobantes.DataSource = ventas;
        }

    }
}
