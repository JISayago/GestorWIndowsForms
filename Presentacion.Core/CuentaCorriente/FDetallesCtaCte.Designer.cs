namespace Presentacion.Core.CuentaCorriente
{
    partial class FDetallesCtaCte
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FDetallesCtaCte));
            lblNombreCliente = new Label();
            lblCliente = new Label();
            dgvGrilla = new DataGridView();
            lblListadoMovimientos = new Label();
            tableLayoutPanel10 = new TableLayoutPanel();
            tableLayoutPanel9 = new TableLayoutPanel();
            tableLayoutPanel11 = new TableLayoutPanel();
            lblPagina = new Label();
            btnAnterior = new Button();
            btnSiguiente = new Button();
            lblTotalRegistros = new Label();
            pbxLogo = new PictureBox();
            lblSaldoCuenta = new Label();
            label2 = new Label();
            lblFechaCreacion = new Label();
            label5 = new Label();
            lblNombreCuenta = new Label();
            label7 = new Label();
            label8 = new Label();
            lblFechaVto = new Label();
            lstDnis = new ListBox();
            lblDetallesExtra = new Label();
            lblEstado = new Label();
            label4 = new Label();
            btnCargarSaldo = new Button();
            btnCerrarCtacte = new Button();
            btnActivar = new Button();
            btnEliminarDni = new Button();
            btnAgregarDni = new Button();
            txtNuevoDni = new TextBox();
            lblDni = new Label();
            cbxModificarDni = new CheckBox();
            btnCargarLimite = new Button();
            checkBox1 = new CheckBox();
            btnGuardarCambios = new Button();
            btnSalir = new Button();
            label1 = new Label();
            nudCantidadMeses = new NumericUpDown();
            checkBox2 = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).BeginInit();
            tableLayoutPanel10.SuspendLayout();
            tableLayoutPanel9.SuspendLayout();
            tableLayoutPanel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidadMeses).BeginInit();
            SuspendLayout();
            // 
            // lblNombreCliente
            // 
            lblNombreCliente.AutoSize = true;
            lblNombreCliente.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblNombreCliente.Location = new Point(116, 136);
            lblNombreCliente.Name = "lblNombreCliente";
            lblNombreCliente.Size = new Size(114, 32);
            lblNombreCliente.TabIndex = 23;
            lblNombreCliente.Tag = "NoModificarConBase";
            lblNombreCliente.Text = "**********";
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblCliente.Location = new Point(14, 136);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(96, 32);
            lblCliente.TabIndex = 22;
            lblCliente.Tag = "NoModificarConBase";
            lblCliente.Text = "Cliente:";
            // 
            // dgvGrilla
            // 
            dgvGrilla.AllowUserToAddRows = false;
            dgvGrilla.AllowUserToDeleteRows = false;
            dgvGrilla.Anchor = AnchorStyles.None;
            dgvGrilla.BackgroundColor = SystemColors.ButtonFace;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvGrilla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvGrilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(31, 26, 43);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvGrilla.DefaultCellStyle = dataGridViewCellStyle2;
            dgvGrilla.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvGrilla.Location = new Point(12, 509);
            dgvGrilla.Name = "dgvGrilla";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvGrilla.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvGrilla.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Padding = new Padding(2, 4, 2, 4);
            dgvGrilla.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvGrilla.RowTemplate.Height = 40;
            dgvGrilla.Size = new Size(1159, 228);
            dgvGrilla.TabIndex = 24;
            dgvGrilla.MouseDown += dgvGrilla_MouseDown;
            // 
            // lblListadoMovimientos
            // 
            lblListadoMovimientos.AutoSize = true;
            lblListadoMovimientos.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblListadoMovimientos.Location = new Point(12, 467);
            lblListadoMovimientos.Name = "lblListadoMovimientos";
            lblListadoMovimientos.Size = new Size(155, 32);
            lblListadoMovimientos.TabIndex = 25;
            lblListadoMovimientos.Tag = "NoModificarConBase";
            lblListadoMovimientos.Text = "Movimientos";
            // 
            // tableLayoutPanel10
            // 
            tableLayoutPanel10.Anchor = AnchorStyles.None;
            tableLayoutPanel10.ColumnCount = 2;
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.66667F));
            tableLayoutPanel10.Controls.Add(tableLayoutPanel9, 1, 0);
            tableLayoutPanel10.Controls.Add(pbxLogo, 0, 0);
            tableLayoutPanel10.Location = new Point(12, 743);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.RowCount = 1;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel10.Size = new Size(569, 58);
            tableLayoutPanel10.TabIndex = 26;
            // 
            // tableLayoutPanel9
            // 
            tableLayoutPanel9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel9.ColumnCount = 2;
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel9.Controls.Add(tableLayoutPanel11, 1, 0);
            tableLayoutPanel9.Controls.Add(lblTotalRegistros, 0, 0);
            tableLayoutPanel9.Location = new Point(192, 3);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.RowCount = 1;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel9.Size = new Size(374, 52);
            tableLayoutPanel9.TabIndex = 9;
            // 
            // tableLayoutPanel11
            // 
            tableLayoutPanel11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel11.ColumnCount = 3;
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel11.Controls.Add(lblPagina, 1, 0);
            tableLayoutPanel11.Controls.Add(btnAnterior, 0, 0);
            tableLayoutPanel11.Controls.Add(btnSiguiente, 2, 0);
            tableLayoutPanel11.Location = new Point(152, 3);
            tableLayoutPanel11.Name = "tableLayoutPanel11";
            tableLayoutPanel11.RowCount = 1;
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel11.Size = new Size(219, 46);
            tableLayoutPanel11.TabIndex = 7;
            // 
            // lblPagina
            // 
            lblPagina.Anchor = AnchorStyles.None;
            lblPagina.AutoSize = true;
            lblPagina.Font = new Font("Segoe UI Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPagina.Location = new Point(74, 10);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(69, 25);
            lblPagina.TabIndex = 1;
            lblPagina.Text = "label2";
            lblPagina.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnAnterior
            // 
            btnAnterior.Anchor = AnchorStyles.None;
            btnAnterior.Font = new Font("Segoe UI Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAnterior.Location = new Point(3, 3);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(48, 40);
            btnAnterior.TabIndex = 2;
            btnAnterior.Text = "<";
            btnAnterior.UseVisualStyleBackColor = true;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Anchor = AnchorStyles.None;
            btnSiguiente.Font = new Font("Segoe UI Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSiguiente.Location = new Point(166, 3);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(50, 40);
            btnSiguiente.TabIndex = 7;
            btnSiguiente.Text = ">";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // lblTotalRegistros
            // 
            lblTotalRegistros.Anchor = AnchorStyles.None;
            lblTotalRegistros.AutoSize = true;
            lblTotalRegistros.Font = new Font("Segoe UI Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalRegistros.Location = new Point(41, 13);
            lblTotalRegistros.Name = "lblTotalRegistros";
            lblTotalRegistros.Size = new Size(67, 25);
            lblTotalRegistros.TabIndex = 0;
            lblTotalRegistros.Text = "label1";
            lblTotalRegistros.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pbxLogo
            // 
            pbxLogo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pbxLogo.Location = new Point(80, 3);
            pbxLogo.Margin = new Padding(80, 3, 3, 3);
            pbxLogo.Name = "pbxLogo";
            pbxLogo.Size = new Size(106, 52);
            pbxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pbxLogo.TabIndex = 8;
            pbxLogo.TabStop = false;
            // 
            // lblSaldoCuenta
            // 
            lblSaldoCuenta.AutoSize = true;
            lblSaldoCuenta.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblSaldoCuenta.Location = new Point(589, 9);
            lblSaldoCuenta.Name = "lblSaldoCuenta";
            lblSaldoCuenta.Size = new Size(40, 32);
            lblSaldoCuenta.TabIndex = 28;
            lblSaldoCuenta.Tag = "NoModificarConBase";
            lblSaldoCuenta.Text = "$0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label2.Location = new Point(487, 9);
            label2.Name = "label2";
            label2.Size = new Size(80, 32);
            label2.TabIndex = 27;
            label2.Tag = "NoModificarConBase";
            label2.Text = "Saldo:";
            // 
            // lblFechaCreacion
            // 
            lblFechaCreacion.AutoSize = true;
            lblFechaCreacion.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblFechaCreacion.Location = new Point(205, 180);
            lblFechaCreacion.Name = "lblFechaCreacion";
            lblFechaCreacion.Size = new Size(135, 32);
            lblFechaCreacion.TabIndex = 34;
            lblFechaCreacion.Tag = "NoModificarConBase";
            lblFechaCreacion.Text = "01/08/2026";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label5.Location = new Point(14, 180);
            label5.Name = "label5";
            label5.Size = new Size(185, 32);
            label5.TabIndex = 33;
            label5.Tag = "NoModificarConBase";
            label5.Text = "Fecha Creación:";
            // 
            // lblNombreCuenta
            // 
            lblNombreCuenta.AutoSize = true;
            lblNombreCuenta.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblNombreCuenta.Location = new Point(121, 9);
            lblNombreCuenta.Name = "lblNombreCuenta";
            lblNombreCuenta.Size = new Size(114, 32);
            lblNombreCuenta.TabIndex = 36;
            lblNombreCuenta.Tag = "NoModificarConBase";
            lblNombreCuenta.Text = "**********";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label7.Location = new Point(12, 9);
            label7.Name = "label7";
            label7.Size = new Size(98, 32);
            label7.TabIndex = 35;
            label7.Tag = "NoModificarConBase";
            label7.Text = "Cuenta:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label8.Location = new Point(12, 51);
            label8.Name = "label8";
            label8.Size = new Size(251, 32);
            label8.TabIndex = 37;
            label8.Tag = "NoModificarConBase";
            label8.Text = "Próximo Vencimiento:";
            // 
            // lblFechaVto
            // 
            lblFechaVto.AutoSize = true;
            lblFechaVto.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblFechaVto.Location = new Point(270, 51);
            lblFechaVto.Name = "lblFechaVto";
            lblFechaVto.Size = new Size(135, 32);
            lblFechaVto.TabIndex = 38;
            lblFechaVto.Tag = "NoModificarConBase";
            lblFechaVto.Text = "01/08/2026";
            // 
            // lstDnis
            // 
            lstDnis.FormattingEnabled = true;
            lstDnis.ItemHeight = 15;
            lstDnis.Location = new Point(439, 180);
            lstDnis.Name = "lstDnis";
            lstDnis.Size = new Size(282, 229);
            lstDnis.TabIndex = 40;
            // 
            // lblDetallesExtra
            // 
            lblDetallesExtra.AutoSize = true;
            lblDetallesExtra.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblDetallesExtra.Location = new Point(10, 425);
            lblDetallesExtra.Name = "lblDetallesExtra";
            lblDetallesExtra.Size = new Size(711, 32);
            lblDetallesExtra.TabIndex = 41;
            lblDetallesExtra.Tag = "NoModificarConBase";
            lblDetallesExtra.Text = "Se le permite tener saldo negativo. Monto de deuda maximo 0$";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblEstado.Location = new Point(127, 226);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(114, 32);
            lblEstado.TabIndex = 43;
            lblEstado.Tag = "NoModificarConBase";
            lblEstado.Text = "**********";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label4.Location = new Point(18, 226);
            label4.Name = "label4";
            label4.Size = new Size(92, 32);
            label4.TabIndex = 42;
            label4.Tag = "NoModificarConBase";
            label4.Text = "Estado:";
            // 
            // btnCargarSaldo
            // 
            btnCargarSaldo.Location = new Point(808, 257);
            btnCargarSaldo.Name = "btnCargarSaldo";
            btnCargarSaldo.Size = new Size(127, 54);
            btnCargarSaldo.TabIndex = 44;
            btnCargarSaldo.Text = "Cargar Saldo";
            btnCargarSaldo.UseVisualStyleBackColor = true;
            btnCargarSaldo.Click += btnCargarSaldo_Click;
            // 
            // btnCerrarCtacte
            // 
            btnCerrarCtacte.Location = new Point(808, 82);
            btnCerrarCtacte.Name = "btnCerrarCtacte";
            btnCerrarCtacte.Size = new Size(127, 54);
            btnCerrarCtacte.TabIndex = 45;
            btnCerrarCtacte.Text = "Cerrar Cuenta";
            btnCerrarCtacte.UseVisualStyleBackColor = true;
            // 
            // btnActivar
            // 
            btnActivar.Location = new Point(808, 174);
            btnActivar.Name = "btnActivar";
            btnActivar.Size = new Size(127, 54);
            btnActivar.TabIndex = 46;
            btnActivar.Text = "Activar";
            btnActivar.UseVisualStyleBackColor = true;
            // 
            // btnEliminarDni
            // 
            btnEliminarDni.Location = new Point(666, 144);
            btnEliminarDni.Name = "btnEliminarDni";
            btnEliminarDni.Size = new Size(55, 25);
            btnEliminarDni.TabIndex = 50;
            btnEliminarDni.Text = "-";
            btnEliminarDni.UseVisualStyleBackColor = true;
            btnEliminarDni.Click += btnEliminarDni_Click;
            // 
            // btnAgregarDni
            // 
            btnAgregarDni.Location = new Point(605, 144);
            btnAgregarDni.Name = "btnAgregarDni";
            btnAgregarDni.Size = new Size(55, 25);
            btnAgregarDni.TabIndex = 49;
            btnAgregarDni.Text = "+";
            btnAgregarDni.UseVisualStyleBackColor = true;
            btnAgregarDni.Click += btnAgregarDni_Click;
            // 
            // txtNuevoDni
            // 
            txtNuevoDni.Location = new Point(439, 145);
            txtNuevoDni.Name = "txtNuevoDni";
            txtNuevoDni.Size = new Size(160, 23);
            txtNuevoDni.TabIndex = 48;
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Location = new Point(439, 120);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(243, 15);
            lblDni.TabIndex = 47;
            lblDni.Text = "DNI autorizados para usar la cuenta corriente";
            // 
            // cbxModificarDni
            // 
            cbxModificarDni.AutoSize = true;
            cbxModificarDni.Location = new Point(438, 88);
            cbxModificarDni.Name = "cbxModificarDni";
            cbxModificarDni.Size = new Size(129, 19);
            cbxModificarDni.TabIndex = 51;
            cbxModificarDni.Text = "Agregar/Quitar DNI";
            cbxModificarDni.UseVisualStyleBackColor = true;
            // 
            // btnCargarLimite
            // 
            btnCargarLimite.Location = new Point(808, 326);
            btnCargarLimite.Name = "btnCargarLimite";
            btnCargarLimite.Size = new Size(127, 52);
            btnCargarLimite.TabIndex = 52;
            btnCargarLimite.Text = "Cargar Límite";
            btnCargarLimite.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(744, 438);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(200, 19);
            checkBox1.TabIndex = 53;
            checkBox1.Text = "Permitir/Cancelar deuda máxima";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnGuardarCambios
            // 
            btnGuardarCambios.Location = new Point(311, 876);
            btnGuardarCambios.Name = "btnGuardarCambios";
            btnGuardarCambios.Size = new Size(129, 50);
            btnGuardarCambios.TabIndex = 54;
            btnGuardarCambios.Text = "Guardar Cambios";
            btnGuardarCambios.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(553, 876);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(129, 50);
            btnSalir.TabIndex = 55;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 345);
            label1.Name = "label1";
            label1.Size = new Size(210, 15);
            label1.TabIndex = 57;
            label1.Text = "Cantidad de Meses (para vencimiento)";
            // 
            // nudCantidadMeses
            // 
            nudCantidadMeses.Location = new Point(234, 343);
            nudCantidadMeses.Name = "nudCantidadMeses";
            nudCantidadMeses.Size = new Size(35, 23);
            nudCantidadMeses.TabIndex = 56;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(53, 309);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(208, 19);
            checkBox2.TabIndex = 58;
            checkBox2.Text = "Modificar meses para vencimiento";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // FDetallesCtaCte
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1513, 973);
            Controls.Add(checkBox2);
            Controls.Add(label1);
            Controls.Add(nudCantidadMeses);
            Controls.Add(btnSalir);
            Controls.Add(btnGuardarCambios);
            Controls.Add(checkBox1);
            Controls.Add(btnCargarLimite);
            Controls.Add(cbxModificarDni);
            Controls.Add(btnEliminarDni);
            Controls.Add(btnAgregarDni);
            Controls.Add(txtNuevoDni);
            Controls.Add(lblDni);
            Controls.Add(btnActivar);
            Controls.Add(btnCerrarCtacte);
            Controls.Add(btnCargarSaldo);
            Controls.Add(lblEstado);
            Controls.Add(label4);
            Controls.Add(lblDetallesExtra);
            Controls.Add(lstDnis);
            Controls.Add(lblFechaVto);
            Controls.Add(label8);
            Controls.Add(lblNombreCuenta);
            Controls.Add(label7);
            Controls.Add(lblFechaCreacion);
            Controls.Add(label5);
            Controls.Add(lblSaldoCuenta);
            Controls.Add(label2);
            Controls.Add(tableLayoutPanel10);
            Controls.Add(lblListadoMovimientos);
            Controls.Add(dgvGrilla);
            Controls.Add(lblNombreCliente);
            Controls.Add(lblCliente);
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FDetallesCtaCte";
            Text = "FDetallesCtaCte";
            Load += FDetallesCtaCte_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).EndInit();
            tableLayoutPanel10.ResumeLayout(false);
            tableLayoutPanel9.ResumeLayout(false);
            tableLayoutPanel9.PerformLayout();
            tableLayoutPanel11.ResumeLayout(false);
            tableLayoutPanel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbxLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidadMeses).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNombreCliente;
        private Label lblCliente;
        protected DataGridView dgvGrilla;
        private Label lblListadoMovimientos;
        private TableLayoutPanel tableLayoutPanel10;
        private TableLayoutPanel tableLayoutPanel9;
        private TableLayoutPanel tableLayoutPanel11;
        protected Label lblPagina;
        private Button btnAnterior;
        private Button btnSiguiente;
        protected Label lblTotalRegistros;
        private PictureBox pbxLogo;
        private Label lblSaldoCuenta;
        private Label label2;
        private Label lblFechaCreacion;
        private Label label5;
        private Label lblNombreCuenta;
        private Label label7;
        private Label label8;
        private Label lblFechaVto;
        private ListBox lstDnis;
        private Label lblDetallesExtra;
        private Label lblEstado;
        private Label label4;
        private Button btnCargarSaldo;
        private Button btnCerrarCtacte;
        private Button btnActivar;
        private Button btnEliminarDni;
        private Button btnAgregarDni;
        private TextBox txtNuevoDni;
        private Label lblDni;
        private CheckBox cbxModificarDni;
        private Button btnCargarLimite;
        private CheckBox checkBox1;
        private Button btnGuardarCambios;
        private Button btnSalir;
        private Label label1;
        private NumericUpDown nudCantidadMeses;
        private CheckBox checkBox2;
    }
}