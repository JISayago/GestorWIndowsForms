namespace Presentacion.Core.Gasto
{
    partial class FGastoABM
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
            txtCategoriaGasto = new TextBox();
            txtMontoPago = new TextBox();
            txtDetalle = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnRegistrarGasto = new Button();
            btnCancelar = new Button();
            btnPagoPendiente = new Button();
            dtpDiaGasto = new DateTimePicker();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // txtCategoriaGasto
            // 
            txtCategoriaGasto.Location = new Point(149, 35);
            txtCategoriaGasto.Name = "txtCategoriaGasto";
            txtCategoriaGasto.Size = new Size(100, 23);
            txtCategoriaGasto.TabIndex = 0;
            // 
            // txtMontoPago
            // 
            txtMontoPago.Location = new Point(164, 126);
            txtMontoPago.Name = "txtMontoPago";
            txtMontoPago.Size = new Size(100, 23);
            txtMontoPago.TabIndex = 1;
            // 
            // txtDetalle
            // 
            txtDetalle.Location = new Point(164, 81);
            txtDetalle.Name = "txtDetalle";
            txtDetalle.Size = new Size(483, 23);
            txtDetalle.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(55, 38);
            label1.Name = "label1";
            label1.Size = new Size(88, 15);
            label1.TabIndex = 3;
            label1.Text = "categoria gasto";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(55, 89);
            label2.Name = "label2";
            label2.Size = new Size(94, 15);
            label2.TabIndex = 4;
            label2.Text = "Detalle del gasto";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(100, 134);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 5;
            label3.Text = "Monto";
            // 
            // btnRegistrarGasto
            // 
            btnRegistrarGasto.Location = new Point(138, 207);
            btnRegistrarGasto.Name = "btnRegistrarGasto";
            btnRegistrarGasto.Size = new Size(126, 51);
            btnRegistrarGasto.TabIndex = 6;
            btnRegistrarGasto.Text = "Registrar Gasto";
            btnRegistrarGasto.UseVisualStyleBackColor = true;
            btnRegistrarGasto.Click += btnRegistrarGasto_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(302, 207);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(126, 51);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnPagoPendiente
            // 
            btnPagoPendiente.Location = new Point(469, 207);
            btnPagoPendiente.Name = "btnPagoPendiente";
            btnPagoPendiente.Size = new Size(126, 51);
            btnPagoPendiente.TabIndex = 8;
            btnPagoPendiente.Text = "Pago Pendiente";
            btnPagoPendiente.UseVisualStyleBackColor = true;
            btnPagoPendiente.Click += btnPagoPendiente_Click;
            // 
            // dtpDiaGasto
            // 
            dtpDiaGasto.Location = new Point(469, 128);
            dtpDiaGasto.Name = "dtpDiaGasto";
            dtpDiaGasto.Size = new Size(200, 23);
            dtpDiaGasto.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(368, 134);
            label4.Name = "label4";
            label4.Size = new Size(76, 15);
            label4.TabIndex = 10;
            label4.Text = "Dia del Gasto";
            // 
            // FGastoABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(776, 289);
            Controls.Add(label4);
            Controls.Add(dtpDiaGasto);
            Controls.Add(btnPagoPendiente);
            Controls.Add(btnCancelar);
            Controls.Add(btnRegistrarGasto);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtDetalle);
            Controls.Add(txtMontoPago);
            Controls.Add(txtCategoriaGasto);
            Name = "FGastoABM";
            Text = "FGastoABM";
            Controls.SetChildIndex(txtCategoriaGasto, 0);
            Controls.SetChildIndex(txtMontoPago, 0);
            Controls.SetChildIndex(txtDetalle, 0);
            Controls.SetChildIndex(label1, 0);
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(label3, 0);
            Controls.SetChildIndex(btnRegistrarGasto, 0);
            Controls.SetChildIndex(btnCancelar, 0);
            Controls.SetChildIndex(btnPagoPendiente, 0);
            Controls.SetChildIndex(dtpDiaGasto, 0);
            Controls.SetChildIndex(label4, 0);
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCategoriaGasto;
        private TextBox txtMontoPago;
        private TextBox txtDetalle;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnRegistrarGasto;
        private Button btnCancelar;
        private Button btnPagoPendiente;
        private DateTimePicker dtpDiaGasto;
        private Label label4;
    }
}