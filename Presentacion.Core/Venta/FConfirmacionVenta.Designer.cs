namespace Presentacion.Core.Venta
{
    partial class FConfirmacionVenta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            nudCantidadPagos = new NumericUpDown();
            btnTipoPago1 = new Button();
            btnConfirmarPago = new Button();
            btnCancelar = new Button();
            lblPago1 = new Label();
            lblPago2 = new Label();
            lblPago3 = new Label();
            lblNroPago = new Label();
            txtPago1 = new TextBox();
            txtPago2 = new TextBox();
            txtPago3 = new TextBox();
            lblMonto = new Label();
            btnTipoPago2 = new Button();
            btnTipoPago3 = new Button();
            lblPagoSeleccionado = new Label();
            lblFormaPago1 = new Label();
            lblFormaPago2 = new Label();
            lblFormaPago3 = new Label();
            lblTextTotal = new Label();
            lblTotal = new Label();
            lblPendienteDePago = new Label();
            lblMontoPendiente = new Label();
            cbxPorcentaje = new CheckBox();
            txtPorcentajePago1 = new TextBox();
            txtPorcentajePago2 = new TextBox();
            txtPorcentajePago3 = new TextBox();
            cbxConfirmPago1 = new CheckBox();
            cbxConfirmPago2 = new CheckBox();
            cbxConfirmPago3 = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidadPagos).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(331, 9);
            label1.Name = "label1";
            label1.Size = new Size(106, 15);
            label1.TabIndex = 36;
            label1.Text = "Cantidad de Pagos";
            // 
            // nudCantidadPagos
            // 
            nudCantidadPagos.Location = new Point(439, 6);
            nudCantidadPagos.Name = "nudCantidadPagos";
            nudCantidadPagos.Size = new Size(38, 23);
            nudCantidadPagos.TabIndex = 35;
            nudCantidadPagos.ValueChanged += nudCantidadPagos_ValueChanged;
            // 
            // btnTipoPago1
            // 
            btnTipoPago1.Location = new Point(394, 63);
            btnTipoPago1.Name = "btnTipoPago1";
            btnTipoPago1.Size = new Size(162, 23);
            btnTipoPago1.TabIndex = 33;
            btnTipoPago1.Text = "Selección Forma de Pago";
            btnTipoPago1.UseVisualStyleBackColor = true;
            btnTipoPago1.Click += btnTipoPago1_Click;
            // 
            // btnConfirmarPago
            // 
            btnConfirmarPago.Location = new Point(137, 169);
            btnConfirmarPago.Name = "btnConfirmarPago";
            btnConfirmarPago.Size = new Size(75, 40);
            btnConfirmarPago.TabIndex = 37;
            btnConfirmarPago.Text = "Confirmar";
            btnConfirmarPago.UseVisualStyleBackColor = true;
            btnConfirmarPago.Click += btnConfirmarPago_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(282, 169);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 40);
            btnCancelar.TabIndex = 38;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // lblPago1
            // 
            lblPago1.AutoSize = true;
            lblPago1.Location = new Point(9, 75);
            lblPago1.Name = "lblPago1";
            lblPago1.Size = new Size(43, 15);
            lblPago1.TabIndex = 39;
            lblPago1.Text = "Pago 1";
            // 
            // lblPago2
            // 
            lblPago2.AutoSize = true;
            lblPago2.Location = new Point(9, 100);
            lblPago2.Name = "lblPago2";
            lblPago2.Size = new Size(43, 15);
            lblPago2.TabIndex = 40;
            lblPago2.Text = "Pago 2";
            // 
            // lblPago3
            // 
            lblPago3.AutoSize = true;
            lblPago3.Location = new Point(9, 130);
            lblPago3.Name = "lblPago3";
            lblPago3.Size = new Size(43, 15);
            lblPago3.TabIndex = 41;
            lblPago3.Text = "Pago 3";
            // 
            // lblNroPago
            // 
            lblNroPago.AutoSize = true;
            lblNroPago.Location = new Point(9, 48);
            lblNroPago.Name = "lblNroPago";
            lblNroPago.Size = new Size(57, 15);
            lblNroPago.TabIndex = 42;
            lblNroPago.Text = "Nro Pago";
            // 
            // txtPago1
            // 
            txtPago1.Location = new Point(91, 62);
            txtPago1.Name = "txtPago1";
            txtPago1.Size = new Size(65, 23);
            txtPago1.TabIndex = 43;
            // 
            // txtPago2
            // 
            txtPago2.Location = new Point(91, 92);
            txtPago2.Name = "txtPago2";
            txtPago2.Size = new Size(65, 23);
            txtPago2.TabIndex = 44;
            // 
            // txtPago3
            // 
            txtPago3.Location = new Point(91, 122);
            txtPago3.Name = "txtPago3";
            txtPago3.Size = new Size(65, 23);
            txtPago3.TabIndex = 45;
            // 
            // lblMonto
            // 
            lblMonto.AutoSize = true;
            lblMonto.Location = new Point(99, 43);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(43, 15);
            lblMonto.TabIndex = 46;
            lblMonto.Text = "Monto";
            // 
            // btnTipoPago2
            // 
            btnTipoPago2.Location = new Point(394, 96);
            btnTipoPago2.Name = "btnTipoPago2";
            btnTipoPago2.Size = new Size(162, 23);
            btnTipoPago2.TabIndex = 47;
            btnTipoPago2.Text = "Selección Forma de Pago";
            btnTipoPago2.UseVisualStyleBackColor = true;
            btnTipoPago2.Click += btnTipoPago2_Click;
            // 
            // btnTipoPago3
            // 
            btnTipoPago3.Location = new Point(394, 126);
            btnTipoPago3.Name = "btnTipoPago3";
            btnTipoPago3.Size = new Size(162, 23);
            btnTipoPago3.TabIndex = 48;
            btnTipoPago3.Text = "Selección Forma de Pago";
            btnTipoPago3.UseVisualStyleBackColor = true;
            btnTipoPago3.Click += btnTipoPago3_Click;
            // 
            // lblPagoSeleccionado
            // 
            lblPagoSeleccionado.AutoSize = true;
            lblPagoSeleccionado.Location = new Point(288, 44);
            lblPagoSeleccionado.Name = "lblPagoSeleccionado";
            lblPagoSeleccionado.Size = new Size(90, 15);
            lblPagoSeleccionado.TabIndex = 49;
            lblPagoSeleccionado.Text = "Forma de Pago ";
            // 
            // lblFormaPago1
            // 
            lblFormaPago1.AutoSize = true;
            lblFormaPago1.Location = new Point(284, 70);
            lblFormaPago1.Name = "lblFormaPago1";
            lblFormaPago1.Size = new Size(94, 15);
            lblFormaPago1.TabIndex = 50;
            lblFormaPago1.Text = "No seleccionada";
            // 
            // lblFormaPago2
            // 
            lblFormaPago2.AutoSize = true;
            lblFormaPago2.Location = new Point(284, 100);
            lblFormaPago2.Name = "lblFormaPago2";
            lblFormaPago2.Size = new Size(94, 15);
            lblFormaPago2.TabIndex = 51;
            lblFormaPago2.Text = "No seleccionada";
            // 
            // lblFormaPago3
            // 
            lblFormaPago3.AutoSize = true;
            lblFormaPago3.Location = new Point(284, 130);
            lblFormaPago3.Name = "lblFormaPago3";
            lblFormaPago3.Size = new Size(94, 15);
            lblFormaPago3.TabIndex = 52;
            lblFormaPago3.Text = "No seleccionada";
            // 
            // lblTextTotal
            // 
            lblTextTotal.AutoSize = true;
            lblTextTotal.Location = new Point(12, 9);
            lblTextTotal.Name = "lblTextTotal";
            lblTextTotal.Size = new Size(81, 15);
            lblTextTotal.TabIndex = 53;
            lblTextTotal.Text = "Total a Pagar: ";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(94, 9);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(13, 15);
            lblTotal.TabIndex = 54;
            lblTotal.Text = "0";
            // 
            // lblPendienteDePago
            // 
            lblPendienteDePago.AutoSize = true;
            lblPendienteDePago.Location = new Point(171, 9);
            lblPendienteDePago.Name = "lblPendienteDePago";
            lblPendienteDePago.Size = new Size(66, 15);
            lblPendienteDePago.TabIndex = 55;
            lblPendienteDePago.Text = "Pendiente: ";
            // 
            // lblMontoPendiente
            // 
            lblMontoPendiente.AutoSize = true;
            lblMontoPendiente.Location = new Point(243, 9);
            lblMontoPendiente.Name = "lblMontoPendiente";
            lblMontoPendiente.Size = new Size(13, 15);
            lblMontoPendiente.TabIndex = 56;
            lblMontoPendiente.Text = "0";
            // 
            // cbxPorcentaje
            // 
            cbxPorcentaje.AutoSize = true;
            cbxPorcentaje.Location = new Point(223, 40);
            cbxPorcentaje.Name = "cbxPorcentaje";
            cbxPorcentaje.Size = new Size(36, 19);
            cbxPorcentaje.TabIndex = 57;
            cbxPorcentaje.Text = "%";
            cbxPorcentaje.UseVisualStyleBackColor = true;
            cbxPorcentaje.CheckedChanged += cbxPorcentaje_CheckedChanged;
            // 
            // txtPorcentajePago1
            // 
            txtPorcentajePago1.Enabled = false;
            txtPorcentajePago1.Location = new Point(220, 63);
            txtPorcentajePago1.Name = "txtPorcentajePago1";
            txtPorcentajePago1.Size = new Size(36, 23);
            txtPorcentajePago1.TabIndex = 58;
            txtPorcentajePago1.Leave += txtPorcentajePago1_Leave;
            // 
            // txtPorcentajePago2
            // 
            txtPorcentajePago2.Enabled = false;
            txtPorcentajePago2.Location = new Point(220, 93);
            txtPorcentajePago2.Name = "txtPorcentajePago2";
            txtPorcentajePago2.Size = new Size(36, 23);
            txtPorcentajePago2.TabIndex = 59;
            txtPorcentajePago2.Leave += txtPorcentajePago2_Leave;
            // 
            // txtPorcentajePago3
            // 
            txtPorcentajePago3.Enabled = false;
            txtPorcentajePago3.Location = new Point(220, 122);
            txtPorcentajePago3.Name = "txtPorcentajePago3";
            txtPorcentajePago3.Size = new Size(36, 23);
            txtPorcentajePago3.TabIndex = 60;
            txtPorcentajePago3.Leave += txtPorcentajePago3_Leave;
            // 
            // cbxConfirmPago1
            // 
            cbxConfirmPago1.AutoSize = true;
            cbxConfirmPago1.Location = new Point(162, 67);
            cbxConfirmPago1.Name = "cbxConfirmPago1";
            cbxConfirmPago1.Size = new Size(15, 14);
            cbxConfirmPago1.TabIndex = 61;
            cbxConfirmPago1.UseVisualStyleBackColor = true;
            cbxConfirmPago1.CheckedChanged += cbxConfirmPago1_CheckedChanged;
            // 
            // cbxConfirmPago2
            // 
            cbxConfirmPago2.AutoSize = true;
            cbxConfirmPago2.Location = new Point(162, 97);
            cbxConfirmPago2.Name = "cbxConfirmPago2";
            cbxConfirmPago2.Size = new Size(15, 14);
            cbxConfirmPago2.TabIndex = 62;
            cbxConfirmPago2.UseVisualStyleBackColor = true;
            cbxConfirmPago2.CheckedChanged += cbxConfirmPago2_CheckedChanged;
            // 
            // cbxConfirmPago3
            // 
            cbxConfirmPago3.AutoSize = true;
            cbxConfirmPago3.Location = new Point(162, 128);
            cbxConfirmPago3.Name = "cbxConfirmPago3";
            cbxConfirmPago3.Size = new Size(15, 14);
            cbxConfirmPago3.TabIndex = 63;
            cbxConfirmPago3.UseVisualStyleBackColor = true;
            cbxConfirmPago3.CheckedChanged += cbxConfirmPago3_CheckedChanged;
            // 
            // FConfirmacionVenta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(578, 221);
            Controls.Add(cbxConfirmPago3);
            Controls.Add(cbxConfirmPago2);
            Controls.Add(cbxConfirmPago1);
            Controls.Add(txtPorcentajePago3);
            Controls.Add(txtPorcentajePago2);
            Controls.Add(txtPorcentajePago1);
            Controls.Add(cbxPorcentaje);
            Controls.Add(lblMontoPendiente);
            Controls.Add(lblPendienteDePago);
            Controls.Add(lblTotal);
            Controls.Add(lblTextTotal);
            Controls.Add(lblFormaPago3);
            Controls.Add(lblFormaPago2);
            Controls.Add(lblFormaPago1);
            Controls.Add(lblPagoSeleccionado);
            Controls.Add(btnTipoPago3);
            Controls.Add(btnTipoPago2);
            Controls.Add(lblMonto);
            Controls.Add(txtPago3);
            Controls.Add(txtPago2);
            Controls.Add(txtPago1);
            Controls.Add(lblNroPago);
            Controls.Add(lblPago3);
            Controls.Add(lblPago2);
            Controls.Add(lblPago1);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmarPago);
            Controls.Add(label1);
            Controls.Add(nudCantidadPagos);
            Controls.Add(btnTipoPago1);
            Name = "FConfirmacionVenta";
            Text = "FConfirmacionVenta";
            Load += FConfirmacionVenta_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidadPagos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private NumericUpDown nudCantidadPagos;
        private Button btnTipoPago1;
        private Button btnConfirmarPago;
        private Button btnCancelar;
        private Label lblPago1;
        private Label lblPago2;
        private Label lblPago3;
        private Label lblNroPago;
        private TextBox txtPago1;
        private TextBox txtPago2;
        private TextBox txtPago3;
        private Label lblMonto;
        private Button btnTipoPago2;
        private Button btnTipoPago3;
        private Label lblPagoSeleccionado;
        private Label lblFormaPago1;
        private Label lblFormaPago2;
        private Label lblFormaPago3;
        private Label lblTextTotal;
        private Label lblTotal;
        private Label lblPendienteDePago;
        private Label lblMontoPendiente;
        private CheckBox cbxPorcentaje;
        private TextBox txtPorcentajePago1;
        private TextBox txtPorcentajePago2;
        private TextBox txtPorcentajePago3;
        private CheckBox cbxConfirmPago1;
        private CheckBox cbxConfirmPago2;
        private CheckBox cbxConfirmPago3;
    }
}