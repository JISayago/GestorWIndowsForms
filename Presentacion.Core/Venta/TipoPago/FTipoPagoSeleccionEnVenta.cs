using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Servicios.Helpers;

namespace Presentacion.Core.Venta.TipoPago
{
    public partial class FTipoPagoSeleccionEnVenta : FBase.FBase
    {
        public TipoDePago tipoPagoSeleccionado { get; private set; }
        public bool _incluirCtaCte { get; private set; }
        public List<FormaPago> _pagos { get; private set; }
        public FTipoPagoSeleccionEnVenta(bool incluirCtaCte,List<FormaPago> pagos)
        {
            InitializeComponent();
            _incluirCtaCte = incluirCtaCte;
            _pagos = pagos;
        }

        private void btnEfectivo_Click(object sender, EventArgs e)
        {
            SeleccionTipoPago(TipoDePago.Efectivo);
        }

        private void btnCredito_Click(object sender, EventArgs e)
        {
            SeleccionTipoPago(TipoDePago.Credito);
        }

        private void btnCtaCte_Click(object sender, EventArgs e)
        {
            SeleccionTipoPago(TipoDePago.CtaCte);
        }

        private void btnTransferencia_Click(object sender, EventArgs e)
        {
            SeleccionTipoPago(TipoDePago.Transferencia);
        }

        private void btnDébito_Click(object sender, EventArgs e)
        {
            SeleccionTipoPago(TipoDePago.Debito);
        }

        private void SeleccionTipoPago(TipoDePago tp)
        {
            //el tipo pago es la uicacion del array del limitador de pagos 
            // tengo que pasar el array de pagos para ver si ya contiene ese tipo de pago
            // y si l
            tipoPagoSeleccionado = tp;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FTipoPagoSeleccionEnVenta_Load(object sender, EventArgs e)
        {
            if (!_incluirCtaCte)
            {
                btnCtaCte.Visible = false;
                btnCtaCte.Enabled = false;
            }
        }

        private void btnQR_Click(object sender, EventArgs e)
        {
            SeleccionTipoPago(TipoDePago.QR);
        }

        private void btnCheque_Click(object sender, EventArgs e)
        {
            SeleccionTipoPago(TipoDePago.Cheque);
        }
    }
}
