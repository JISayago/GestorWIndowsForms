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
            txtMontoPago = new TextBox();
            txtDetalle = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnRegistrarGasto = new Button();
            btnCancelar = new Button();
            dtpDiaGasto = new DateTimePicker();
            label4 = new Label();
            cmbCategoriaGasto = new ComboBox();
            cmbEstado = new ComboBox();
            lblEstado = new Label();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // txtMontoPago
            // 
            txtMontoPago.Location = new Point(157, 128);
            txtMontoPago.Name = "txtMontoPago";
            txtMontoPago.Size = new Size(100, 23);
            txtMontoPago.TabIndex = 1;
            // 
            // txtDetalle
            // 
            txtDetalle.Location = new Point(157, 81);
            txtDetalle.Name = "txtDetalle";
            txtDetalle.Size = new Size(483, 23);
            txtDetalle.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(86, 183);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 3;
            label1.Text = "Categoria";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(48, 89);
            label2.Name = "label2";
            label2.Size = new Size(95, 15);
            label2.TabIndex = 4;
            label2.Text = "Detalle del gasto";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(100, 131);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 5;
            label3.Text = "Monto";
            // 
            // btnRegistrarGasto
            // 
            btnRegistrarGasto.Location = new Point(157, 226);
            btnRegistrarGasto.Name = "btnRegistrarGasto";
            btnRegistrarGasto.Size = new Size(126, 40);
            btnRegistrarGasto.TabIndex = 6;
            btnRegistrarGasto.Text = "Registrar Gasto";
            btnRegistrarGasto.UseVisualStyleBackColor = true;
            btnRegistrarGasto.Click += btnRegistrarGasto_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(434, 226);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(127, 40);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // dtpDiaGasto
            // 
            dtpDiaGasto.Location = new Point(440, 129);
            dtpDiaGasto.Name = "dtpDiaGasto";
            dtpDiaGasto.Size = new Size(200, 23);
            dtpDiaGasto.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(357, 131);
            label4.Name = "label4";
            label4.Size = new Size(77, 15);
            label4.TabIndex = 10;
            label4.Text = "Dia del Gasto";
            // 
            // cmbCategoriaGasto
            // 
            cmbCategoriaGasto.FormattingEnabled = true;
            cmbCategoriaGasto.Location = new Point(157, 175);
            cmbCategoriaGasto.Name = "cmbCategoriaGasto";
            cmbCategoriaGasto.Size = new Size(121, 23);
            cmbCategoriaGasto.TabIndex = 11;
            // 
            // cmbEstado
            // 
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(440, 170);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(121, 23);
            cmbEstado.TabIndex = 13;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(392, 175);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(42, 15);
            lblEstado.TabIndex = 12;
            lblEstado.Text = "Estado";
            // 
            // FGastoABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(703, 289);
            Controls.Add(cmbEstado);
            Controls.Add(lblEstado);
            Controls.Add(cmbCategoriaGasto);
            Controls.Add(label4);
            Controls.Add(dtpDiaGasto);
            Controls.Add(btnCancelar);
            Controls.Add(btnRegistrarGasto);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtDetalle);
            Controls.Add(txtMontoPago);
            Name = "FGastoABM";
            Text = "FGastoABM";
            Load += FGastoABM_Load;
            Controls.SetChildIndex(txtMontoPago, 0);
            Controls.SetChildIndex(txtDetalle, 0);
            Controls.SetChildIndex(label1, 0);
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(label3, 0);
            Controls.SetChildIndex(btnRegistrarGasto, 0);
            Controls.SetChildIndex(btnCancelar, 0);
            Controls.SetChildIndex(dtpDiaGasto, 0);
            Controls.SetChildIndex(label4, 0);
            Controls.SetChildIndex(cmbCategoriaGasto, 0);
            Controls.SetChildIndex(lblEstado, 0);
            Controls.SetChildIndex(cmbEstado, 0);
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtMontoPago;
        private TextBox txtDetalle;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnRegistrarGasto;
        private Button btnCancelar;
        private DateTimePicker dtpDiaGasto;
        private Label label4;
        private ComboBox cmbCategoriaGasto;
        private ComboBox cmbEstado;
        private Label lblEstado;
    }
}