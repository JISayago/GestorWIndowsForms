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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FGastoABM));
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
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            lblOrigen = new Label();
            cmbOrigen = new ComboBox();
            lblTipoPago = new Label();
            cmbTipoPago = new ComboBox();
            lblExtraPago = new Label();
            txtExtraPago = new TextBox();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // txtMontoPago
            // 
            txtMontoPago.Location = new Point(118, 181);
            txtMontoPago.Name = "txtMontoPago";
            txtMontoPago.Size = new Size(115, 23);
            txtMontoPago.TabIndex = 1;
            // 
            // txtDetalle
            // 
            txtDetalle.Location = new Point(124, 98);
            txtDetalle.Multiline = true;
            txtDetalle.Name = "txtDetalle";
            txtDetalle.Size = new Size(439, 58);
            txtDetalle.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(276, 189);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 3;
            label1.Text = "Categoria";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 101);
            label2.Name = "label2";
            label2.Size = new Size(95, 15);
            label2.TabIndex = 4;
            label2.Text = "Detalle del gasto";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(64, 189);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 5;
            label3.Text = "Monto";
            // 
            // btnRegistrarGasto
            // 
            btnRegistrarGasto.Location = new Point(140, 384);
            btnRegistrarGasto.Name = "btnRegistrarGasto";
            btnRegistrarGasto.Size = new Size(126, 40);
            btnRegistrarGasto.TabIndex = 8;
            btnRegistrarGasto.Text = "Registrar Gasto";
            btnRegistrarGasto.UseVisualStyleBackColor = true;
            btnRegistrarGasto.Click += btnRegistrarGasto_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(363, 384);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(127, 40);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // dtpDiaGasto
            // 
            dtpDiaGasto.Location = new Point(363, 228);
            dtpDiaGasto.Name = "dtpDiaGasto";
            dtpDiaGasto.Size = new Size(200, 23);
            dtpDiaGasto.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(270, 234);
            label4.Name = "label4";
            label4.Size = new Size(87, 15);
            label4.TabIndex = 10;
            label4.Text = "Fecha del Pago";
            // 
            // cmbCategoriaGasto
            // 
            cmbCategoriaGasto.FormattingEnabled = true;
            cmbCategoriaGasto.Location = new Point(369, 181);
            cmbCategoriaGasto.Name = "cmbCategoriaGasto";
            cmbCategoriaGasto.Size = new Size(194, 23);
            cmbCategoriaGasto.TabIndex = 2;
            // 
            // cmbEstado
            // 
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(118, 231);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(115, 23);
            cmbEstado.TabIndex = 3;
            cmbEstado.SelectedIndexChanged += cmbEstado_SelectedIndexChanged;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(58, 239);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(42, 15);
            lblEstado.TabIndex = 12;
            lblEstado.Text = "Estado";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Red;
            label5.Location = new Point(309, 65);
            label5.Name = "label5";
            label5.Size = new Size(260, 17);
            label5.TabIndex = 47;
            label5.Tag = "NoModificarConBase";
            label5.Text = "El ícono  *  representa campo obligatorio.";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Red;
            label6.Location = new Point(569, 101);
            label6.Name = "label6";
            label6.Size = new Size(16, 20);
            label6.TabIndex = 48;
            label6.Tag = "NoModificarConBase";
            label6.Text = "*";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Red;
            label7.Location = new Point(239, 185);
            label7.Name = "label7";
            label7.Size = new Size(16, 20);
            label7.TabIndex = 49;
            label7.Tag = "NoModificarConBase";
            label7.Text = "*";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.Red;
            label8.Location = new Point(239, 234);
            label8.Name = "label8";
            label8.Size = new Size(16, 20);
            label8.TabIndex = 50;
            label8.Tag = "NoModificarConBase";
            label8.Text = "*";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.Red;
            label9.Location = new Point(569, 184);
            label9.Name = "label9";
            label9.Size = new Size(16, 20);
            label9.TabIndex = 51;
            label9.Tag = "NoModificarConBase";
            label9.Text = "*";
            // 
            // lblOrigen
            // 
            lblOrigen.AutoSize = true;
            lblOrigen.Location = new Point(58, 289);
            lblOrigen.Name = "lblOrigen";
            lblOrigen.Size = new Size(42, 15);
            lblOrigen.TabIndex = 52;
            lblOrigen.Text = "Origen";
            // 
            // cmbOrigen
            // 
            cmbOrigen.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrigen.FormattingEnabled = true;
            cmbOrigen.Location = new Point(118, 281);
            cmbOrigen.Name = "cmbOrigen";
            cmbOrigen.Size = new Size(115, 23);
            cmbOrigen.TabIndex = 5;
            cmbOrigen.SelectedIndexChanged += cmbOrigen_SelectedIndexChanged;
            // 
            // lblTipoPago
            // 
            lblTipoPago.AutoSize = true;
            lblTipoPago.Location = new Point(285, 289);
            lblTipoPago.Name = "lblTipoPago";
            lblTipoPago.Size = new Size(73, 15);
            lblTipoPago.TabIndex = 53;
            lblTipoPago.Text = "Tipo de pago";
            // 
            // cmbTipoPago
            // 
            cmbTipoPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoPago.FormattingEnabled = true;
            cmbTipoPago.Location = new Point(369, 281);
            cmbTipoPago.Name = "cmbTipoPago";
            cmbTipoPago.Size = new Size(194, 23);
            cmbTipoPago.TabIndex = 6;
            // 
            // lblExtraPago
            // 
            lblExtraPago.AutoSize = true;
            lblExtraPago.Location = new Point(21, 339);
            lblExtraPago.Name = "lblExtraPago";
            lblExtraPago.Size = new Size(88, 15);
            lblExtraPago.TabIndex = 54;
            lblExtraPago.Text = "Detalle del pago";
            // 
            // txtExtraPago
            // 
            txtExtraPago.Location = new Point(118, 331);
            txtExtraPago.MaxLength = 100;
            txtExtraPago.Name = "txtExtraPago";
            txtExtraPago.Size = new Size(445, 23);
            txtExtraPago.TabIndex = 7;
            // 
            // FGastoABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(620, 439);
            Controls.Add(txtExtraPago);
            Controls.Add(lblExtraPago);
            Controls.Add(cmbTipoPago);
            Controls.Add(lblTipoPago);
            Controls.Add(cmbOrigen);
            Controls.Add(lblOrigen);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
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
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FGastoABM";
            Text = "ABM Gasto";
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
            Controls.SetChildIndex(label5, 0);
            Controls.SetChildIndex(label6, 0);
            Controls.SetChildIndex(label7, 0);
            Controls.SetChildIndex(label8, 0);
            Controls.SetChildIndex(label9, 0);
            Controls.SetChildIndex(lblOrigen, 0);
            Controls.SetChildIndex(cmbOrigen, 0);
            Controls.SetChildIndex(lblTipoPago, 0);
            Controls.SetChildIndex(cmbTipoPago, 0);
            Controls.SetChildIndex(lblExtraPago, 0);
            Controls.SetChildIndex(txtExtraPago, 0);
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
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lblOrigen;
        private ComboBox cmbOrigen;
        private Label lblTipoPago;
        private ComboBox cmbTipoPago;
        private Label lblExtraPago;
        private TextBox txtExtraPago;
    }
}