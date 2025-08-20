using Presentacion.Core.Venta.TipoPago;
using Servicios.Helpers;
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

        public FConfirmacionVenta(DatosVenta dv)
        {
            InitializeComponent();
            TotalVenta = dv.Total;
            _datosVenta = dv;

        }

        private void btnConfirmarPago_Click(object sender, EventArgs e)
        {
            pagos = pagos.Where(p => p.TipoDePago != null).ToList();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void FConfirmacionVenta_Load(object sender, EventArgs e)
        {
            pagos = new List<FormaPago>
    {
        new FormaPago { Numero = 1, Monto = 0, TipoDePago = null },
        new FormaPago { Numero = 2, Monto = 0, TipoDePago = null },
        new FormaPago { Numero = 3, Monto = 0, TipoDePago = null }
    };

            nudCantidadPagos.Minimum = 1;
            nudCantidadPagos.Maximum = 3;
            nudCantidadPagos.ReadOnly = true;
            nudCantidadPagos.Controls[0].Enabled = false; // Oculta botones +/- si usás WinForms

            lblTotal.Text = TotalVenta.ToString("C2");

            // Deshabilitar pagos 2 y 3 y sus checkboxes al iniciar
            txtPago2.Enabled = false;
            txtPago3.Enabled = false;

            cbxConfirmPago1.Checked = false;
            cbxConfirmPago2.Checked = false;
            cbxConfirmPago3.Checked = false;

            cbxConfirmPago2.Enabled = false;
            cbxConfirmPago3.Enabled = false;

            ActualizarMultiplesPagos();
        }


        private void nudCantidadPagos_ValueChanged(object sender, EventArgs e)
        {
            if (!cbxPorcentaje.Checked)
            {
                decimal.TryParse(txtPago1.Text, out var pago1);

                if (pago1 == TotalVenta)
                {
                    MessageBox.Show("El primer pago ya cubre el total. Modifique el monto para poder agregar más pagos.",
                                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    nudCantidadPagos.Value = 1;
                    return;
                }
            }

            if (cbxPorcentaje.Checked)
                cbxPorcentaje_CheckedChanged(sender, e);
            else
                ActualizarMultiplesPagos();
        }
        private void ActualizarMultiplesPagos()
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
        private void btnTipoPago1_Click(object sender, EventArgs e)
        {
            CargarFormaDePago(lblFormaPago1, 1);
        }

        private void CargarFormaDePago(Label label, int numeroPago)
        {
            var tipoSeleccionado = AsignarTipoPagoAPago();
            pagos[numeroPago - 1].TipoDePago = tipoSeleccionado;
            label.Text = tipoSeleccionado.ToString();
        }

        private TipoDePago AsignarTipoPagoAPago()
        {
            using var fFormaPagoSeleccionada = new FTipoPagoSeleccionEnVenta(_datosVenta.IncluirCtaCte);

            if (fFormaPagoSeleccionada.ShowDialog() == DialogResult.OK)
            {
                return fFormaPagoSeleccionada.tipoPagoSeleccionado;
            }

            return TipoDePago.Efectivo;
        }

        private void btnTipoPago2_Click(object sender, EventArgs e)
        {
            CargarFormaDePago(lblFormaPago2, 2);
        }

        private void btnTipoPago3_Click(object sender, EventArgs e)
        {
            CargarFormaDePago(lblFormaPago3, 3);
        }

     

        private void cbxPorcentaje_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxPorcentaje.Checked)
            {
                int cantidad = (int)nudCantidadPagos.Value;

                decimal porcentajeBase = Math.Floor(100m / cantidad);
                decimal resto = 100m - (porcentajeBase * (cantidad - 1));

                txtPorcentajePago1.Text = porcentajeBase.ToString("N0");
                txtPorcentajePago2.Text = cantidad >= 2 ? porcentajeBase.ToString("N0") : "0";
                txtPorcentajePago3.Text = cantidad == 3 ? resto.ToString("N0") : "0";

                txtPorcentajePago1.Enabled = true;
                txtPorcentajePago2.Enabled = cantidad >= 2;
                txtPorcentajePago3.Enabled = cantidad == 3;

                CalcularPagosPorPorcentaje();
            }
            else
            {
                txtPorcentajePago1.Enabled = false;
                txtPorcentajePago2.Enabled = false;
                txtPorcentajePago3.Enabled = false;

                txtPago1.ReadOnly = false;
                txtPago2.ReadOnly = false;
                txtPago3.ReadOnly = false;

                ActualizarMultiplesPagos();
            }
        }
        private void CalcularPagosPorPorcentaje()
        {
            decimal porcentaje1 = 0, porcentaje2 = 0, porcentaje3 = 0;

            // Parsear los valores de porcentaje
            decimal.TryParse(txtPorcentajePago1.Text, out porcentaje1);
            decimal.TryParse(txtPorcentajePago2.Text, out porcentaje2);
            decimal.TryParse(txtPorcentajePago3.Text, out porcentaje3);

            decimal totalPorcentaje = porcentaje1 + porcentaje2 + porcentaje3;

            // Calcular pagos a partir del porcentaje
            pagos[0].Monto = Math.Round((TotalVenta * porcentaje1) / 100, 2);
            pagos[1].Monto = Math.Round((TotalVenta * porcentaje2) / 100, 2);
            pagos[2].Monto = Math.Round((TotalVenta * porcentaje3) / 100, 2);

            txtPago1.Text = pagos[0].Monto.ToString("N2");
            txtPago2.Text = pagos[1].Monto.ToString("N2");
            txtPago3.Text = pagos[2].Monto.ToString("N2");

            txtPago1.ReadOnly = true;
            txtPago2.ReadOnly = true;
            txtPago3.ReadOnly = true;

            txtPago1.Enabled = true;
            txtPago2.Enabled = (int)nudCantidadPagos.Value >= 2;
            txtPago3.Enabled = (int)nudCantidadPagos.Value == 3;

            MontoPendiente = TotalVenta - pagos.Sum(p => p.Monto);
            lblMontoPendiente.Text = MontoPendiente.ToString("C2");
        }


        private void txtPorcentajePago1_Leave(object sender, EventArgs e)
        {
            if (cbxPorcentaje.Checked)
                AjustarPorcentajesDesde(txtPorcentajePago1, 1);
        }

        private void txtPorcentajePago2_Leave(object sender, EventArgs e)
        {
            if (cbxPorcentaje.Checked)
                AjustarPorcentajesDesde(txtPorcentajePago1, 2);
        }

        private void txtPorcentajePago3_Leave(object sender, EventArgs e)
        {
            if (cbxPorcentaje.Checked)
                AjustarPorcentajesDesde(txtPorcentajePago1, 3);
        }
        private void AjustarPorcentajesDesde(TextBox origen, int origenIndex)
        {
            int cantidad = (int)nudCantidadPagos.Value;

            // Leer el valor que el usuario modificó
            decimal.TryParse(origen.Text, out decimal porcentajeOrigen);

            if (porcentajeOrigen < 0 || porcentajeOrigen > 100)
            {
                MessageBox.Show("El porcentaje debe estar entre 0 y 100.");
                origen.Text = "0";
                return;
            }

            // Distribuir el resto entre los otros
            decimal restante = 100m - porcentajeOrigen;

            TextBox[] porcentajes = new[] { txtPorcentajePago1, txtPorcentajePago2, txtPorcentajePago3 };
            int[] indicesActivos = Enumerable.Range(0, cantidad).Where(i => i + 1 != origenIndex).ToArray();

            if (indicesActivos.Length == 0)
            {
                // Solo un campo, no hay que ajustar nada
                origen.Text = "100";
                CalcularPagosPorPorcentaje();
                return;
            }

            decimal porcentajeRepartido = Math.Floor(restante / indicesActivos.Length);
            decimal porcentajeUltimo = restante - porcentajeRepartido * (indicesActivos.Length - 1);

            for (int i = 0; i < indicesActivos.Length; i++)
            {
                int index = indicesActivos[i];
                if (i == indicesActivos.Length - 1)
                    porcentajes[index].Text = porcentajeUltimo.ToString("N0");
                else
                    porcentajes[index].Text = porcentajeRepartido.ToString("N0");
            }

            CalcularPagosPorPorcentaje();
        }

        private void cbxConfirmPago1_CheckedChanged(object sender, EventArgs e)
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
        }

        private void cbxConfirmPago2_CheckedChanged(object sender, EventArgs e)
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

        private void cbxConfirmPago3_CheckedChanged(object sender, EventArgs e)
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
            }
            else
            {
                pagos[2].Monto = 0;
                nudCantidadPagos.Value = 2;
            }

            ActualizarMultiplesPagos();
        }
    }
}
//me faltaria incorporar un auto calcular si le erre a algo
