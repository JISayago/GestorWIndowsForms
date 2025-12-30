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

        // NUEVO: flag que el caller debe setear antes de ShowDialog().
        // true = permitir la UI de múltiples pagos (comportamiento actual)
        // false = modo "1 pago" -> abrir selector de forma de pago y retornar.
        public bool PermitirMultiplesPagos { get; set; } = true;

        public FConfirmacionVenta(DatosVenta dv, long? clienteCargado = null)
        {
            InitializeComponent();
            TotalVenta = dv.Total;
            _datosVenta = dv;
            idCliente = clienteCargado;
        }

        /*private void btnConfirmarPago_Click(object sender, EventArgs e)
        {
            pagos = pagos.Where(p => p.TipoDePago != null).ToList();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }*/

        private void FConfirmacionVenta_Load(object sender, EventArgs e)
        {
            // Inicializo pagos por defecto (3 slots, como antes)
            pagos = new List<FormaPago>
        {
            new FormaPago { Numero = 1, Monto = 0, TipoDePago = null },
            new FormaPago { Numero = 2, Monto = 0, TipoDePago = null },
            new FormaPago { Numero = 3, Monto = 0, TipoDePago = null }
        };

            // Configuración UI original
            //nudCantidadPagos.Minimum = 1;
            //nudCantidadPagos.Maximum = 3;
            //nudCantidadPagos.ReadOnly = true;
            //if (nudCantidadPagos.Controls.Count > 0)
            //    nudCantidadPagos.Controls[0].Enabled = false; // Oculta botones +/- si usás WinForms

//            lblTotal.Text = TotalVenta.ToString("C2");

            // Deshabilitar pagos 2 y 3 y sus checkboxes al iniciar
  //          txtPago2.Enabled = false;
    //        txtPago3.Enabled = false;

      //      cbxConfirmPago1.Checked = false;
        //    cbxConfirmPago2.Checked = false;
          //  cbxConfirmPago3.Checked = false;

            //cbxConfirmPago2.Enabled = false;
           // cbxConfirmPago3.Enabled = false;

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

            // Si permite múltiples pagos, ir con la lógica clásica
            //ActualizarMultiplesPagos();
        }

        /*private void nudCantidadPagos_ValueChanged(object sender, EventArgs e)
        {
            decimal.TryParse(txtPago1.Text, out var pago1);

            if (pago1 == TotalVenta)
            {
                MessageBox.Show("El primer pago ya cubre el total. Modifique el monto para poder agregar más pagos.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                nudCantidadPagos.Value = 1;
                return;
            }

           // ActualizarMultiplesPagos();
        }
        */
        /*private void ActualizarMultiplesPagos()
        {
            decimal.TryParse(txtPago1.Text, out decimal pago1);
            decimal.TryParse(txtPago2.Text, out decimal pago2);
            decimal.TryParse(txtPago3.Text, out decimal pago3);

            pagos[0].Monto = pago1;
            pagos[1].Monto = pago2;
            pagos[2].Monto = pago3;

            int cantidad = (int)nudCantidadPagos.Value;
            decimal restante = TotalVenta;

            txtPago1.Enabled = true;
            txtPago2.Enabled = cantidad >= 2;
            txtPago3.Enabled = cantidad == 3;

            lblPago2.Enabled = cantidad >= 2;
            btnTipoPago2.Enabled = cantidad >= 2;

            lblPago3.Enabled = cantidad == 3;
            btnTipoPago3.Enabled = cantidad == 3;

            if (cantidad == 1)
            {
                if (pagos[0].Monto <= 0 || pagos[0].Monto > TotalVenta)
                    pagos[0].Monto = TotalVenta;

                pagos[1].Monto = 0;
                pagos[2].Monto = 0;
            }
            else if (cantidad == 2)
            {
                if (pagos[0].Monto + pagos[1].Monto > TotalVenta)
                {
                    pagos[2].Monto = TotalVenta - pagos[0].Monto;
                }
                else if (pago2 <= 0)
                {
                    pagos[1].Monto = TotalVenta - pagos[0].Monto;
                }

                pagos[2].Monto = 0;
            }
            else if (cantidad == 3)
            {
                decimal sumaParcial = pagos[0].Monto + pagos[1].Monto;

                if (sumaParcial >= TotalVenta)
                {
                    // Ya se cubrió el total con pago1 + pago2
                    pagos[2].Monto = 0;
                    txtPago3.Enabled = false;
                    lblPago3.Enabled = true;
                    btnTipoPago3.Enabled = false;
                    nudCantidadPagos.Value = 2; // Forzar a 2 pagos

                    MessageBox.Show("Ya se completó el total con los dos primeros pagos. El tercero se desactiva automáticamente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Solo si queda algo pendiente, se habilita y ajusta pago3
                    txtPago3.Enabled = true;
                    btnTipoPago3.Enabled = true;

                    if (pagos.Sum(p => p.Monto) > TotalVenta)
                    {
                        pagos[2].Monto = TotalVenta - (pagos[0].Monto + pagos[1].Monto);
                    }
                    else if (pagos[2].Monto <= 0)
                    {
                        pagos[2].Monto = TotalVenta - (pagos[0].Monto + pagos[1].Monto);
                    }
                }
            }

            txtPago1.Text = pagos[0].Monto.ToString("N2");
            txtPago2.Text = pagos[1].Monto.ToString("N2");
            txtPago3.Text = pagos[2].Monto.ToString("N2");

            MontoPendiente = TotalVenta - (pagos.Sum(p => p.Monto));
            lblMontoPendiente.Text = MontoPendiente.ToString("C2");
        }
        */
       /* private void btnTipoPago1_Click(object sender, EventArgs e)
        {
            if (cbxConfirmPago1.Checked)
            {
                CargarFormaDePago(lblFormaPago1, 1, btnTipoPago1);
            }
            else
            {
                MessageBox.Show("Debe confirmar el Monto a pagar antes de seleccionar la forma de pago.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
       */
       /* private TipoDePago AsignarTipoPagoAPago(int numeroPago)
        {
            // Este helper ya EXISTÍA, pero ojo: devuelve Efectivo si el diálogo se cierra sin OK.
            // Preferimos usar el flujo inline cuando necesitemos detectar cancelación.
            using var fFormaPagoSeleccionada = new FTipoPagoSeleccionEnVenta(_datosVenta.IncluirCtaCte, pagos, numeroPago - 1, idCliente);

            if (fFormaPagoSeleccionada.ShowDialog() == DialogResult.OK)
            {
                return fFormaPagoSeleccionada.tipoPagoSeleccionado;
            }

            return TipoDePago.Efectivo;
        }*/

       /* private void CargarFormaDePago(Label label, int numeroPago, Button btn)
        {
            var tipoSeleccionado = AsignarTipoPagoAPago(numeroPago);
            pagos[numeroPago - 1].TipoDePago = tipoSeleccionado;

            label.Text = tipoSeleccionado.ToString();
            btn.Text = "Cambiar Forma de Pago";
        }
       */
       /* private void btnTipoPago2_Click(object sender, EventArgs e)
        {
            if (cbxConfirmPago2.Checked)
            {
                CargarFormaDePago(lblFormaPago2, 2, btnTipoPago2);
            }
            else
            {
                MessageBox.Show("Debe confirmar el Monto a pagar antes de seleccionar la forma de pago.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }*/

       /* private void btnTipoPago3_Click(object sender, EventArgs e)
        {
            if (cbxConfirmPago3.Checked)
            {
                CargarFormaDePago(lblFormaPago3, 3, btnTipoPago3);
            }
            else
            {
                MessageBox.Show("Debe confirmar el Monto a pagar antes de seleccionar la forma de pago.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }*/

     /*   private void cbxConfirmPago1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxConfirmPago1.Checked)
            {
                if (!decimal.TryParse(txtPago1.Text, out var pago1) || pago1 <= 0)
                {
                    MessageBox.Show("Ingrese un monto válido para el primer pago.");
                    cbxConfirmPago1.Checked = false;
                    cbxConfirmPago1.Enabled = true;
                    return;
                }

                if (pago1 < 10)
                {
                    MessageBox.Show("El monto mínimo permitido es $10.");
                    cbxConfirmPago1.Checked = false;
                    cbxConfirmPago1.Enabled = true;
                    return;
                }

                pagos[0].Monto = pago1;

                var restante = TotalVenta - pago1;
                nudCantidadPagos.Value = restante <= 0 ? 1 : 2;

                // Habilitar pago2 y su checkbox si queda restante
                txtPago2.Enabled = restante > 0;
                cbxConfirmPago2.Enabled = restante > 0;

                if (restante <= 0)
                {
                    // Resetear pagos y checkboxes 2 y 3 si no quedan pendientes
                    pagos[1].Monto = 0;
                    pagos[2].Monto = 0;
                    txtPago2.Text = "";
                    txtPago3.Text = "";
                    cbxConfirmPago2.Checked = false;
                    cbxConfirmPago3.Checked = false;
                    txtPago2.Enabled = false;
                    txtPago3.Enabled = false;
                    cbxConfirmPago2.Enabled = false;
                    cbxConfirmPago3.Enabled = false;
                }
                if (cbxConfirmPago1.Checked)
                {
                    CargarFormaDePago(lblFormaPago1, 1, btnTipoPago1);
                }
            }
            else
            {
                // Si desmarcan pago1, deshabilitar todo lo demás
                pagos[0].Monto = 0;
                nudCantidadPagos.Value = 1;

                cbxConfirmPago2.Checked = false;
                cbxConfirmPago3.Checked = false;

                cbxConfirmPago2.Enabled = false;
                cbxConfirmPago3.Enabled = false;

                txtPago2.Enabled = false;
                txtPago3.Enabled = false;

                txtPago2.Text = "";
                txtPago3.Text = "";
            }

            ActualizarMultiplesPagos();
        }*/

       /* private void cbxConfirmPago2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxConfirmPago2.Checked)
            {
                if (!cbxConfirmPago1.Checked)
                {
                    MessageBox.Show("Debe confirmar primero el pago 1.");
                    cbxConfirmPago2.Checked = false;
                    txtPago2.Enabled = true; // <-- volver a habilitar
                    return;
                }

                if (!decimal.TryParse(txtPago2.Text, out var pago2) || pago2 <= 0)
                {
                    MessageBox.Show("Ingrese un monto válido para el segundo pago.");
                    cbxConfirmPago2.Checked = false;
                    txtPago2.Enabled = true; // <-- volver a habilitar
                    return;
                }

                if (pago2 < 500)
                {
                    MessageBox.Show("El monto mínimo permitido es $500.");
                    cbxConfirmPago2.Checked = false;
                    txtPago2.Enabled = true; // <-- volver a habilitar
                    return;
                }

                pagos[1].Monto = pago2;

                var restante = TotalVenta - pagos[0].Monto - pagos[1].Monto;
                nudCantidadPagos.Value = restante <= 0 ? 2 : 3;

                txtPago3.Enabled = restante > 0;
                cbxConfirmPago3.Enabled = restante > 0;

                if (restante <= 0)
                {
                    pagos[2].Monto = 0;
                    txtPago3.Text = "";
                    cbxConfirmPago3.Checked = false;
                    txtPago3.Enabled = false;
                    cbxConfirmPago3.Enabled = false;
                }
                if (cbxConfirmPago2.Checked)
                {
                    CargarFormaDePago(lblFormaPago2, 2, btnTipoPago2);
                }
            }
            else
            {
                // Si desmarcan pago2, resetear pago 2 y 3
                pagos[1].Monto = 0;
                pagos[2].Monto = 0;

                nudCantidadPagos.Value = 1;

                cbxConfirmPago3.Checked = false;
                cbxConfirmPago3.Enabled = false;

                txtPago3.Enabled = false;
                txtPago3.Text = "";

                txtPago2.Enabled = true; // <-- volver a habilitar también en el else
            }

            ActualizarMultiplesPagos();
        }
       */
       /* private void cbxConfirmPago3_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxConfirmPago3.Checked)
            {
                if (!cbxConfirmPago1.Checked || !cbxConfirmPago2.Checked)
                {
                    MessageBox.Show("Debe confirmar primero los pagos 1 y 2.");
                    cbxConfirmPago3.Checked = false;
                    cbxConfirmPago3.Enabled = true;
                    return;
                }

                if (!decimal.TryParse(txtPago3.Text, out var pago3) || pago3 <= 0)
                {
                    MessageBox.Show("Ingrese un monto válido para el tercer pago.");
                    cbxConfirmPago3.Checked = false;
                    cbxConfirmPago3.Enabled = true;
                    return;
                }

                if (pago3 < 500)
                {
                    MessageBox.Show("El monto mínimo permitido es $500.");
                    cbxConfirmPago3.Checked = false;
                    cbxConfirmPago3.Enabled = true;
                    return;
                }

                pagos[2].Monto = pago3;
                nudCantidadPagos.Value = 3;
                if (cbxConfirmPago3.Checked)
                {
                    CargarFormaDePago(lblFormaPago3, 3, btnTipoPago3);
                }
            }
            else
            {
                pagos[2].Monto = 0;
                nudCantidadPagos.Value = 2;
            }

            ActualizarMultiplesPagos();
        }*/
    }
}
//me faltaria incorporar un auto calcular si le erre a algo
