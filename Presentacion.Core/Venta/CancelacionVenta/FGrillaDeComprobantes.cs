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
        public FGrillaDeComprobantes(long logeadiId, List<long> comprobantesIDs)
        {
            InitializeComponent();
            _usuarioLogeadoID = logeadiId;
            _comprobantesIDs = comprobantesIDs;
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {

        }
        private void FGrillaDeComprobantes_Load(object sender, EventArgs e)
        {
            if (_comprobantesIDs.Count == 1)
            {
                var comprobanteVentaId = _comprobantesIDs[0];

                var fVenta = new FVenta(_usuarioLogeadoID, comprobanteVentaId);
                fVenta.Show();

                Close();
            }
        }
    }
}
