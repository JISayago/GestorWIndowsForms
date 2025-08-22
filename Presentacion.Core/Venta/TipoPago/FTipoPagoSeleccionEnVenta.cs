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
        private int _indexActual; // índice que estamos editando (puede ser -1 si es nuevo)

        // array de límites: si ya lo tenés en otro lado, usá ese. Aquí lo dejo como ejemplo.
        private readonly int[] listPagosCantidades = new int[8] { 1, 2, 1, 1, 1, 1, 1, 1 };

        public FTipoPagoSeleccionEnVenta(bool incluirCtaCte, List<FormaPago> pagos, int indexActual)
        {
            InitializeComponent();
            _incluirCtaCte = incluirCtaCte;
            _pagos = pagos;
            _indexActual = indexActual;
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
            if (!PuedeSeleccionar(tp, out int permitido, out int yaUsados))
            {
                MessageBox.Show($"No se puede seleccionar '{tp}'. Solo se permiten {permitido} de este tipo y ya hay {yaUsados}.", "Límite alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            tipoPagoSeleccionado = tp;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private bool PuedeSeleccionar(TipoDePago tp, out int permitido, out int yaUsados)
        {
            permitido = 0;
            yaUsados = 0;

            // índice en el array (enum empieza en 1)
            int idx = (int)tp - 1;

            if (idx < 0 || idx >= listPagosCantidades.Length)
            {
                // si el enum no mapea al array, permitimos por defecto (o seteálo como quieras)
                permitido = int.MaxValue;
                return true;
            }

            permitido = listPagosCantidades[idx];

            if (_pagos == null)
            {
                yaUsados = 0;
                return permitido > 0;
            }

            // contamos cuántas veces aparece 'tp' en _pagos EXCLUYENDO el índice que estamos editando
            yaUsados = _pagos
                .Select((p, index) => new { p, index })
                .Count(x => x.index != _indexActual && x.p != null && x.p.TipoDePago == tp);

            // si yaUsados < permitido -> podemos agregar/usar
            return yaUsados < permitido;
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
