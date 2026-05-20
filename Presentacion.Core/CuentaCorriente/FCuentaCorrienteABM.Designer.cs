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
            lblDni = new Label();
            lblCliente = new Label();
            cmbClientes = new ComboBox();
            lstDnis = new ListBox();
            txtNuevoDni = new TextBox();
            btnAgregarDni = new Button();
            btnEliminarDni = new Button();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // lblccorriente
            // 
            lblccorriente.AutoSize = true;
            lblccorriente.Location = new Point(47, 93);
            lblccorriente.Name = "lblccorriente";
            lblccorriente.Size = new Size(68, 15);
            lblccorriente.TabIndex = 0;
            lblccorriente.Text = "Nombre CC";
            // 
            // lblSaldo
            // 
            lblSaldo.AutoSize = true;
            lblSaldo.Location = new Point(47, 140);
            lblSaldo.Name = "lblSaldo";
            lblSaldo.Size = new Size(37, 15);
            lblSaldo.TabIndex = 1;
            lblSaldo.Text = "Saldo";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(83, 446);
            label1.Name = "label1";
            label1.Size = new Size(37, 15);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // lblLimiteCuenta
            // 
            lblLimiteCuenta.AutoSize = true;
            lblLimiteCuenta.Location = new Point(371, 143);
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
            chkLimiteDeuda.Location = new Point(484, 169);
            chkLimiteDeuda.Name = "chkLimiteDeuda";
            chkLimiteDeuda.Size = new Size(146, 19);
            chkLimiteDeuda.TabIndex = 7;
            chkLimiteDeuda.Text = "Limite de deuda activo";
            chkLimiteDeuda.UseVisualStyleBackColor = true;
            chkLimiteDeuda.CheckedChanged += chkbLimiteDeuda_CheckedChanged;
            // 
            // txtNombreCC
            // 
            txtNombreCC.Location = new Point(129, 90);
            txtNombreCC.Name = "txtNombreCC";
            txtNombreCC.Size = new Size(200, 23);
            txtNombreCC.TabIndex = 8;
            // 
            // txtLimiteDeuda
            // 
            txtLimiteDeuda.Location = new Point(484, 140);
            txtLimiteDeuda.Name = "txtLimiteDeuda";
            txtLimiteDeuda.Size = new Size(200, 23);
            txtLimiteDeuda.TabIndex = 9;
            // 
            // txtSaldo
            // 
            txtSaldo.Location = new Point(129, 137);
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
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Location = new Point(227, 238);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(246, 15);
            lblDni.TabIndex = 13;
            lblDni.Text = "DNI autorizados para usar la cuenta corriente";
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(47, 190);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(43, 15);
            lblCliente.TabIndex = 14;
            lblCliente.Text = "Cliente";
            // 
            // cmbClientes
            // 
            cmbClientes.FormattingEnabled = true;
            cmbClientes.Location = new Point(129, 187);
            cmbClientes.Name = "cmbClientes";
            cmbClientes.Size = new Size(200, 23);
            cmbClientes.TabIndex = 15;
            // 
            // lstDnis
            // 
            lstDnis.FormattingEnabled = true;
            lstDnis.ItemHeight = 15;
            lstDnis.Location = new Point(227, 298);
            lstDnis.Name = "lstDnis";
            lstDnis.Size = new Size(282, 109);
            lstDnis.TabIndex = 19;
            // 
            // txtNuevoDni
            // 
            txtNuevoDni.Location = new Point(227, 263);
            txtNuevoDni.Name = "txtNuevoDni";
            txtNuevoDni.Size = new Size(160, 23);
            txtNuevoDni.TabIndex = 16;
            // 
            // btnAgregarDni
            // 
            btnAgregarDni.Location = new Point(393, 262);
            btnAgregarDni.Name = "btnAgregarDni";
            btnAgregarDni.Size = new Size(55, 25);
            btnAgregarDni.TabIndex = 17;
            btnAgregarDni.Text = "+";
            btnAgregarDni.UseVisualStyleBackColor = true;
            btnAgregarDni.Click += btnAgregarDni_Click;
            // 
            // btnEliminarDni
            // 
            btnEliminarDni.Location = new Point(454, 262);
            btnEliminarDni.Name = "btnEliminarDni";
            btnEliminarDni.Size = new Size(55, 25);
            btnEliminarDni.TabIndex = 18;
            btnEliminarDni.Text = "-";
            btnEliminarDni.UseVisualStyleBackColor = true;
            btnEliminarDni.Click += btnEliminarDni_Click;
            // 
            // FCuentaCorrienteABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(736, 419);
            Controls.Add(btnEliminarDni);
            Controls.Add(btnAgregarDni);
            Controls.Add(txtNuevoDni);
            Controls.Add(lstDnis);
            Controls.Add(cmbClientes);
            Controls.Add(lblCliente);
            Controls.Add(lblDni);
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
            Controls.SetChildIndex(lblDni, 0);
            Controls.SetChildIndex(lblCliente, 0);
            Controls.SetChildIndex(cmbClientes, 0);
            Controls.SetChildIndex(lstDnis, 0);
            Controls.SetChildIndex(txtNuevoDni, 0);
            Controls.SetChildIndex(btnAgregarDni, 0);
            Controls.SetChildIndex(btnEliminarDni, 0);
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
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
        private Label lblDni;
        private Label lblCliente;
        private ComboBox cmbClientes;

        // 🔹 Nuevos controles incorporados para la lista de DNIs
        private ListBox lstDnis;
        private TextBox txtNuevoDni;
        private Button btnAgregarDni;
        private Button btnEliminarDni;
    }
}