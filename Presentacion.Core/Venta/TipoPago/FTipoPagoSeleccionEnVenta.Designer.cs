namespace Presentacion.Core.Venta.TipoPago
{
    partial class FTipoPagoSeleccionEnVenta
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
            btnEfectivo = new Button();
            btnTransferencia = new Button();
            btnCredito = new Button();
            btnDébito = new Button();
            btnCtaCte = new Button();
            btnQR = new Button();
            btnCheque = new Button();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // btnEfectivo
            // 
            btnEfectivo.Location = new Point(33, 31);
            btnEfectivo.Name = "btnEfectivo";
            btnEfectivo.Size = new Size(144, 61);
            btnEfectivo.TabIndex = 0;
            btnEfectivo.Text = "Efectivo";
            btnEfectivo.UseVisualStyleBackColor = true;
            btnEfectivo.Click += btnEfectivo_Click;
            // 
            // btnTransferencia
            // 
            btnTransferencia.Location = new Point(33, 116);
            btnTransferencia.Name = "btnTransferencia";
            btnTransferencia.Size = new Size(144, 61);
            btnTransferencia.TabIndex = 1;
            btnTransferencia.Text = "Transferencia";
            btnTransferencia.UseVisualStyleBackColor = true;
            btnTransferencia.Click += btnTransferencia_Click;
            // 
            // btnCredito
            // 
            btnCredito.Location = new Point(252, 31);
            btnCredito.Name = "btnCredito";
            btnCredito.Size = new Size(144, 61);
            btnCredito.TabIndex = 2;
            btnCredito.Text = "T. Crédito";
            btnCredito.UseVisualStyleBackColor = true;
            btnCredito.Click += btnCredito_Click;
            // 
            // btnDébito
            // 
            btnDébito.Location = new Point(252, 116);
            btnDébito.Name = "btnDébito";
            btnDébito.Size = new Size(144, 61);
            btnDébito.TabIndex = 3;
            btnDébito.Text = "T. Débito";
            btnDébito.UseVisualStyleBackColor = true;
            btnDébito.Click += btnDébito_Click;
            // 
            // btnCtaCte
            // 
            btnCtaCte.Location = new Point(252, 194);
            btnCtaCte.Name = "btnCtaCte";
            btnCtaCte.Size = new Size(144, 61);
            btnCtaCte.TabIndex = 4;
            btnCtaCte.Text = "Cuenta Corriente";
            btnCtaCte.UseVisualStyleBackColor = true;
            btnCtaCte.Click += btnCtaCte_Click;
            // 
            // btnQR
            // 
            btnQR.Location = new Point(463, 116);
            btnQR.Name = "btnQR";
            btnQR.Size = new Size(144, 61);
            btnQR.TabIndex = 5;
            btnQR.Text = "QR";
            btnQR.UseVisualStyleBackColor = true;
            btnQR.Click += btnQR_Click;
            // 
            // btnCheque
            // 
            btnCheque.Location = new Point(463, 31);
            btnCheque.Name = "btnCheque";
            btnCheque.Size = new Size(144, 61);
            btnCheque.TabIndex = 6;
            btnCheque.Text = "Cheque";
            btnCheque.UseVisualStyleBackColor = true;
            btnCheque.Click += btnCheque_Click;
            // 
            // FTipoPagoSeleccionEnVenta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 287);
            Controls.Add(btnCheque);
            Controls.Add(btnQR);
            Controls.Add(btnCtaCte);
            Controls.Add(btnDébito);
            Controls.Add(btnCredito);
            Controls.Add(btnTransferencia);
            Controls.Add(btnEfectivo);
            Name = "FTipoPagoSeleccionEnVenta";
            Text = "FTipoPagoSeleccionEnVenta";
            Load += FTipoPagoSeleccionEnVenta_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnEfectivo;
        private Button btnTransferencia;
        private Button btnCredito;
        private Button btnDébito;
        private Button btnCtaCte;
        private Button btnQR;
        private Button btnCheque;
    }
}