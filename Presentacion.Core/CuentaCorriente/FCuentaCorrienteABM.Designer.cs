namespace Presentacion.Core.CuentaCorriente
{
    partial class FCuentaCorrienteABM
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
            lblccorriente = new Label();
            lblSaldo = new Label();
            label1 = new Label();
            lblLimiteCuenta = new Label();
            lblFechaVencimiento = new Label();
            chkLimiteDeuda = new CheckBox();
            txtNombreCC = new TextBox();
            txtLimiteDeuda = new TextBox();
            txtSaldo = new TextBox();
            dtpFechaVencimiento = new DateTimePicker();
            dgvDni = new DataGridView();
            DNI = new DataGridViewTextBoxColumn();
            lblDni = new Label();
            lblCliente = new Label();
            cmbClientes = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDni).BeginInit();
            SuspendLayout();
            // 
            // lblccorriente
            // 
            lblccorriente.AutoSize = true;
            lblccorriente.Location = new Point(50, 93);
            lblccorriente.Name = "lblccorriente";
            lblccorriente.Size = new Size(70, 15);
            lblccorriente.TabIndex = 0;
            lblccorriente.Text = "Nombre CC";
            // 
            // lblSaldo
            // 
            lblSaldo.AutoSize = true;
            lblSaldo.Location = new Point(50, 141);
            lblSaldo.Name = "lblSaldo";
            lblSaldo.Size = new Size(36, 15);
            lblSaldo.TabIndex = 1;
            lblSaldo.Text = "Saldo";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(83, 446);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // lblLimiteCuenta
            // 
            lblLimiteCuenta.AutoSize = true;
            lblLimiteCuenta.Location = new Point(371, 144);
            lblLimiteCuenta.Name = "lblLimiteCuenta";
            lblLimiteCuenta.Size = new Size(92, 15);
            lblLimiteCuenta.TabIndex = 3;
            lblLimiteCuenta.Text = "Limite de deuda";
            // 
            // lblFechaVencimiento
            // 
            lblFechaVencimiento.AutoSize = true;
            lblFechaVencimiento.Location = new Point(371, 93);
            lblFechaVencimiento.Name = "lblFechaVencimiento";
            lblFechaVencimiento.Size = new Size(107, 15);
            lblFechaVencimiento.TabIndex = 5;
            lblFechaVencimiento.Text = "Fecha vencimiento";
            // 
            // chkLimiteDeuda
            // 
            chkLimiteDeuda.AutoSize = true;
            chkLimiteDeuda.Location = new Point(484, 184);
            chkLimiteDeuda.Name = "chkLimiteDeuda";
            chkLimiteDeuda.Size = new Size(146, 19);
            chkLimiteDeuda.TabIndex = 7;
            chkLimiteDeuda.Text = "Limite de deuda activo";
            chkLimiteDeuda.UseVisualStyleBackColor = true;
            chkLimiteDeuda.CheckedChanged += chkbLimiteDeuda_CheckedChanged;
            // 
            // txtNombreCC
            // 
            txtNombreCC.Location = new Point(132, 90);
            txtNombreCC.Name = "txtNombreCC";
            txtNombreCC.Size = new Size(200, 23);
            txtNombreCC.TabIndex = 8;
            // 
            // txtLimiteDeuda
            // 
            txtLimiteDeuda.Location = new Point(484, 141);
            txtLimiteDeuda.Name = "txtLimiteDeuda";
            txtLimiteDeuda.Size = new Size(200, 23);
            txtLimiteDeuda.TabIndex = 9;
            // 
            // txtSaldo
            // 
            txtSaldo.Location = new Point(132, 138);
            txtSaldo.Name = "txtSaldo";
            txtSaldo.Size = new Size(200, 23);
            txtSaldo.TabIndex = 10;
            // 
            // dtpFechaVencimiento
            // 
            dtpFechaVencimiento.Location = new Point(484, 90);
            dtpFechaVencimiento.Name = "dtpFechaVencimiento";
            dtpFechaVencimiento.Size = new Size(200, 23);
            dtpFechaVencimiento.TabIndex = 11;
            // 
            // dgvDni
            // 
            dgvDni.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDni.Columns.AddRange(new DataGridViewColumn[] { DNI });
            dgvDni.Location = new Point(184, 240);
            dgvDni.Name = "dgvDni";
            dgvDni.Size = new Size(409, 174);
            dgvDni.TabIndex = 12;
            // 
            // DNI
            // 
            DNI.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DNI.HeaderText = "Dni";
            DNI.Name = "DNI";
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Location = new Point(272, 222);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(243, 15);
            lblDni.TabIndex = 13;
            lblDni.Text = "DNI autorizados para usar la cuenta corriente";
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(50, 185);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(44, 15);
            lblCliente.TabIndex = 14;
            lblCliente.Text = "Cliente";
            // 
            // cmbClientes
            // 
            cmbClientes.FormattingEnabled = true;
            cmbClientes.Location = new Point(132, 182);
            cmbClientes.Name = "cmbClientes";
            cmbClientes.Size = new Size(200, 23);
            cmbClientes.TabIndex = 15;
            // 
            // FCuentaCorrienteABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 419);
            Controls.Add(cmbClientes);
            Controls.Add(lblCliente);
            Controls.Add(lblDni);
            Controls.Add(dgvDni);
            Controls.Add(dtpFechaVencimiento);
            Controls.Add(txtSaldo);
            Controls.Add(txtLimiteDeuda);
            Controls.Add(txtNombreCC);
            Controls.Add(chkLimiteDeuda);
            Controls.Add(lblFechaVencimiento);
            Controls.Add(lblLimiteCuenta);
            Controls.Add(label1);
            Controls.Add(lblSaldo);
            Controls.Add(lblccorriente);
            Name = "FCuentaCorrienteABM";
            Text = "FCuentaCorrienteABM";
            Load += FCuentaCorrienteABM_Load;
            Controls.SetChildIndex(lblccorriente, 0);
            Controls.SetChildIndex(lblSaldo, 0);
            Controls.SetChildIndex(label1, 0);
            Controls.SetChildIndex(lblLimiteCuenta, 0);
            Controls.SetChildIndex(lblFechaVencimiento, 0);
            Controls.SetChildIndex(chkLimiteDeuda, 0);
            Controls.SetChildIndex(txtNombreCC, 0);
            Controls.SetChildIndex(txtLimiteDeuda, 0);
            Controls.SetChildIndex(txtSaldo, 0);
            Controls.SetChildIndex(dtpFechaVencimiento, 0);
            Controls.SetChildIndex(dgvDni, 0);
            Controls.SetChildIndex(lblDni, 0);
            Controls.SetChildIndex(lblCliente, 0);
            Controls.SetChildIndex(cmbClientes, 0);
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDni).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblccorriente;
        private Label lblSaldo;
        private Label label1;
        private Label lblLimiteCuenta;
        private Label lblFechaVencimiento;
        private CheckBox chkLimiteDeuda;
        private TextBox txtNombreCC;
        private TextBox txtLimiteDeuda;
        private TextBox txtSaldo;
        private DateTimePicker dtpFechaVencimiento;
        private DataGridView dgvDni;
        private Label lblDni;
        private DataGridViewTextBoxColumn DNI;
        private Label lblCliente;
        private ComboBox cmbClientes;
    }
}