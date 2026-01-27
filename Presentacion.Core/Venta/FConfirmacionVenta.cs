using AccesoDatos.Entidades;
using Presentacion.Core.Venta.TipoPago;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Cliente.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Venta
{
    public partial class FConfirmacionVenta : FBase.FBase
    {
        private decimal TotalVenta;
        public decimal MontoPendiente;
        public List<FormaPago> pagos = new List<FormaPago>();
        public DatosVenta _datosVenta = new DatosVenta();
        public long? idCliente;

        public bool PermitirMultiplesPagos { get; set; } = true;

        public FConfirmacionVenta(DatosVenta dv, long? clienteCargado = null)
        {
            InitializeComponent();
            TotalVenta = dv.Total;
            _datosVenta = dv;
            idCliente = clienteCargado;
        }


        private void FConfirmacionVenta_Load(object sender, EventArgs e)
        {
            // Inicializo pagos por defecto (3 slots, como antes)
            pagos = new List<FormaPago>
        {
            new FormaPago { Numero = 1, Monto = 0, TipoDePago = null },
            new FormaPago { Numero = 2, Monto = 0, TipoDePago = null },
            new FormaPago { Numero = 3, Monto = 0, TipoDePago = null }
        };

            // Si el caller indicó que NO se permiten múltiples pagos,
            // hacemos el flujo directo: pedir tipo de pago para 1 pago, asignar y cerrar OK.
            if (!PermitirMultiplesPagos)
            {
                // Abrimos el selector de forma de pago para el pago 1.
                using var fFormaPagoSeleccionada = new FTipoPagoSeleccionEnVenta(_datosVenta, pagos, 0, idCliente);
                if (fFormaPagoSeleccionada.ShowDialog() != DialogResult.OK)
                {
                    // El usuario canceló la selección => salimos cancelando el form.
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }

                // Si llegó OK, asignamos la forma y monto por el total de la venta
                var tipoSeleccionado = fFormaPagoSeleccionada.tipoPagoSeleccionado;
                pagos[0].TipoDePago = tipoSeleccionado;
                pagos[0].Monto = TotalVenta;

                // Actualizamos labels y estado mínimo para que el caller reciba todo en el mismo formato
                lblFormaPago1.Text = tipoSeleccionado.ToString();
                txtPago1.Text = pagos[0].Monto.ToString("N2");
                cbxConfirmPago1.Checked = true;

                // No hay pendientes
                MontoPendiente = 0m;
                lblMontoPendiente.Text = MontoPendiente.ToString("C2");

                // Dejamos la lista de pagos con un único pago relevante
                pagos = new List<FormaPago> { pagos[0] };

                // Devolvemos OK inmediatamente (el caller procesará los pagos como antes)
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
                       
        }

    }
}

