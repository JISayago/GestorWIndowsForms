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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCuentaCorrienteABM));
            lblccorriente = new Label();
            lblSaldo = new Label();
            lblLimiteCuenta = new Label();
            lblFechaVencimiento = new Label();
            chkLimiteDeuda = new CheckBox();
            txtNombreCC = new TextBox();
            txtLimiteDeuda = new TextBox();
            txtSaldo = new TextBox();
            lblDni = new Label();
            lstDnis = new ListBox();
            txtNuevoDni = new TextBox();
            btnAgregarDni = new Button();
            btnEliminarDni = new Button();
            lblCliente = new Label();
            lblNombreCliente = new Label();
            groupBox1 = new GroupBox();
            rbVencimientoManual = new RadioButton();
            rbVencimientoMensual = new RadioButton();
            nudCantidadMeses = new NumericUpDown();
            lblFechaVTO = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidadMeses).BeginInit();
            SuspendLayout();
            // 
            // lblccorriente
            // 
            lblccorriente.AutoSize = true;
            lblccorriente.Location = new Point(47, 120);
            lblccorriente.Name = "lblccorriente";
            lblccorriente.Size = new Size(68, 15);
            lblccorriente.TabIndex = 0;
            lblccorriente.Text = "Nombre CC";
            // 
            // lblSaldo
            // 
            lblSaldo.AutoSize = true;
            lblSaldo.Location = new Point(452, 294);
            lblSaldo.Name = "lblSaldo";
            lblSaldo.Size = new Size(76, 15);
            lblSaldo.TabIndex = 1;
            lblSaldo.Text = "Saldo a favor";
            // 
            // lblLimiteCuenta
            // 
            lblLimiteCuenta.AutoSize = true;
            lblLimiteCuenta.Location = new Point(406, 330);
            lblLimiteCuenta.Name = "lblLimiteCuenta";
            lblLimiteCuenta.Size = new Size(142, 15);
            lblLimiteCuenta.TabIndex = 3;
            lblLimiteCuenta.Text = "Deuda máxima permitida";
            // 
            // lblFechaVencimiento
            // 
            lblFechaVencimiento.AutoSize = true;
            lblFechaVencimiento.Location = new Point(491, 199);
            lblFechaVencimiento.Name = "lblFechaVencimiento";
            lblFechaVencimiento.Size = new Size(107, 15);
            lblFechaVencimiento.TabIndex = 5;
            lblFechaVencimiento.Text = "Fecha vencimiento";
            // 
            // chkLimiteDeuda
            // 
            chkLimiteDeuda.AutoSize = true;
            chkLimiteDeuda.Location = new Point(554, 356);
            chkLimiteDeuda.Name = "chkLimiteDeuda";
            chkLimiteDeuda.Size = new Size(104, 19);
            chkLimiteDeuda.TabIndex = 7;
            chkLimiteDeuda.Text = "Permitir deuda";
            chkLimiteDeuda.UseVisualStyleBackColor = true;
            chkLimiteDeuda.CheckedChanged += chkbLimiteDeuda_CheckedChanged;
            // 
            // txtNombreCC
            // 
            txtNombreCC.Location = new Point(129, 117);
            txtNombreCC.Name = "txtNombreCC";
            txtNombreCC.Size = new Size(200, 23);
            txtNombreCC.TabIndex = 8;
            // 
            // txtLimiteDeuda
            // 
            txtLimiteDeuda.Location = new Point(554, 327);
            txtLimiteDeuda.Name = "txtLimiteDeuda";
            txtLimiteDeuda.Size = new Size(200, 23);
            txtLimiteDeuda.TabIndex = 9;
            // 
            // txtSaldo
            // 
            txtSaldo.Location = new Point(544, 286);
            txtSaldo.Name = "txtSaldo";
            txtSaldo.Size = new Size(200, 23);
            txtSaldo.TabIndex = 10;
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Location = new Point(47, 228);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(246, 15);
            lblDni.TabIndex = 13;
            lblDni.Text = "DNI autorizados para usar la cuenta corriente";
            // 
            // lstDnis
            // 
            lstDnis.FormattingEnabled = true;
            lstDnis.ItemHeight = 15;
            lstDnis.Location = new Point(47, 288);
            lstDnis.Name = "lstDnis";
            lstDnis.Size = new Size(282, 109);
            lstDnis.TabIndex = 19;
            // 
            // txtNuevoDni
            // 
            txtNuevoDni.Location = new Point(47, 253);
            txtNuevoDni.Name = "txtNuevoDni";
            txtNuevoDni.Size = new Size(160, 23);
            txtNuevoDni.TabIndex = 16;
            // 
            // btnAgregarDni
            // 
            btnAgregarDni.Location = new Point(213, 252);
            btnAgregarDni.Name = "btnAgregarDni";
            btnAgregarDni.Size = new Size(55, 25);
            btnAgregarDni.TabIndex = 17;
            btnAgregarDni.Text = "+";
            btnAgregarDni.UseVisualStyleBackColor = true;
            btnAgregarDni.Click += btnAgregarDni_Click;
            // 
            // btnEliminarDni
            // 
            btnEliminarDni.Location = new Point(274, 252);
            btnEliminarDni.Name = "btnEliminarDni";
            btnEliminarDni.Size = new Size(55, 25);
            btnEliminarDni.TabIndex = 18;
            btnEliminarDni.Text = "-";
            btnEliminarDni.UseVisualStyleBackColor = true;
            btnEliminarDni.Click += btnEliminarDni_Click;
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblCliente.Location = new Point(47, 67);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(96, 32);
            lblCliente.TabIndex = 20;
            lblCliente.Tag = "NoModificarConBase";
            lblCliente.Text = "Cliente:";
            // 
            // lblNombreCliente
            // 
            lblNombreCliente.AutoSize = true;
            lblNombreCliente.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblNombreCliente.Location = new Point(149, 67);
            lblNombreCliente.Name = "lblNombreCliente";
            lblNombreCliente.Size = new Size(114, 32);
            lblNombreCliente.TabIndex = 21;
            lblNombreCliente.Tag = "NoModificarConBase";
            lblNombreCliente.Text = "**********";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rbVencimientoManual);
            groupBox1.Controls.Add(rbVencimientoMensual);
            groupBox1.Location = new Point(391, 103);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(333, 51);
            groupBox1.TabIndex = 24;
            groupBox1.TabStop = false;
            // 
            // rbVencimientoManual
            // 
            rbVencimientoManual.AutoSize = true;
            rbVencimientoManual.Location = new Point(188, 22);
            rbVencimientoManual.Name = "rbVencimientoManual";
            rbVencimientoManual.Size = new Size(135, 19);
            rbVencimientoManual.TabIndex = 1;
            rbVencimientoManual.TabStop = true;
            rbVencimientoManual.Text = "Vencimiento manual";
            rbVencimientoManual.UseVisualStyleBackColor = true;
            rbVencimientoManual.CheckedChanged += rbVencimientoManual_CheckedChanged;
            // 
            // rbVencimientoMensual
            // 
            rbVencimientoMensual.AutoSize = true;
            rbVencimientoMensual.Location = new Point(7, 22);
            rbVencimientoMensual.Name = "rbVencimientoMensual";
            rbVencimientoMensual.Size = new Size(158, 19);
            rbVencimientoMensual.TabIndex = 0;
            rbVencimientoMensual.TabStop = true;
            rbVencimientoMensual.Text = "Vencimiento fijo por mes";
            rbVencimientoMensual.UseVisualStyleBackColor = true;
            rbVencimientoMensual.CheckedChanged += rbVencimientoMensual_CheckedChanged;
            // 
            // nudCantidadMeses
            // 
            nudCantidadMeses.Location = new Point(604, 160);
            nudCantidadMeses.Name = "nudCantidadMeses";
            nudCantidadMeses.Size = new Size(120, 23);
            nudCantidadMeses.TabIndex = 25;
            // 
            // lblFechaVTO
            // 
            lblFechaVTO.AutoSize = true;
            lblFechaVTO.Location = new Point(605, 199);
            lblFechaVTO.Name = "lblFechaVTO";
            lblFechaVTO.Size = new Size(53, 15);
            lblFechaVTO.TabIndex = 26;
            lblFechaVTO.Text = "xx/xx/xx";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(389, 168);
            label2.Name = "label2";
            label2.Size = new Size(209, 15);
            label2.TabIndex = 27;
            label2.Text = "Cantidad de Meses (para vencimiento)";
            // 
            // FCuentaCorrienteABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(781, 478);
            Controls.Add(label2);
            Controls.Add(lblFechaVTO);
            Controls.Add(nudCantidadMeses);
            Controls.Add(groupBox1);
            Controls.Add(lblNombreCliente);
            Controls.Add(lblCliente);
            Controls.Add(btnEliminarDni);
            Controls.Add(btnAgregarDni);
            Controls.Add(txtNuevoDni);
            Controls.Add(lstDnis);
            Controls.Add(lblDni);
            Controls.Add(txtSaldo);
            Controls.Add(txtLimiteDeuda);
            Controls.Add(txtNombreCC);
            Controls.Add(chkLimiteDeuda);
            Controls.Add(lblFechaVencimiento);
            Controls.Add(lblLimiteCuenta);
            Controls.Add(lblSaldo);
            Controls.Add(lblccorriente);
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FCuentaCorrienteABM";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ABM Cuenta Corriente";
            Load += FCuentaCorrienteABM_Load;
            Controls.SetChildIndex(lblccorriente, 0);
            Controls.SetChildIndex(lblSaldo, 0);
            Controls.SetChildIndex(lblLimiteCuenta, 0);
            Controls.SetChildIndex(lblFechaVencimiento, 0);
            Controls.SetChildIndex(chkLimiteDeuda, 0);
            Controls.SetChildIndex(txtNombreCC, 0);
            Controls.SetChildIndex(txtLimiteDeuda, 0);
            Controls.SetChildIndex(txtSaldo, 0);
            Controls.SetChildIndex(lblDni, 0);
            Controls.SetChildIndex(lstDnis, 0);
            Controls.SetChildIndex(txtNuevoDni, 0);
            Controls.SetChildIndex(btnAgregarDni, 0);
            Controls.SetChildIndex(btnEliminarDni, 0);
            Controls.SetChildIndex(lblCliente, 0);
            Controls.SetChildIndex(lblNombreCliente, 0);
            Controls.SetChildIndex(groupBox1, 0);
            Controls.SetChildIndex(nudCantidadMeses, 0);
            Controls.SetChildIndex(lblFechaVTO, 0);
            Controls.SetChildIndex(label2, 0);
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidadMeses).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblccorriente;
        private Label lblSaldo;
        private Label lblLimiteCuenta;
        private Label lblFechaVencimiento;
        private CheckBox chkLimiteDeuda;
        private TextBox txtNombreCC;
        private TextBox txtLimiteDeuda;
        private TextBox txtSaldo;
        private Label lblDni;

        // 🔹 Nuevos controles incorporados para la lista de DNIs
        private ListBox lstDnis;
        private TextBox txtNuevoDni;
        private Button btnAgregarDni;
        private Button btnEliminarDni;
        private Label lblCliente;
        private Label lblNombreCliente;
        private GroupBox groupBox1;
        private RadioButton rbVencimientoManual;
        private RadioButton rbVencimientoMensual;
        private NumericUpDown nudCantidadMeses;
        private Label lblFechaVTO;
        private Label label2;
    }
}