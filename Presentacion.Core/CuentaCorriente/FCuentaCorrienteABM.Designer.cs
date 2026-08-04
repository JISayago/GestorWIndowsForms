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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCuentaCorrienteABM));
            lblccorriente = new Label();
            lblSaldo = new Label();
            lblLimiteDeuda = new Label();
            lblFechaVencimientoTitulo = new Label();
            chkLimiteDeuda = new CheckBox();
            txtNombreCC = new TextBox();
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
            lblFechaVencimiento = new Label();
            label2 = new Label();
            btnCargarSaldoCtaCte = new Button();
            btnCargarLimite = new Button();
            tbcBase = new TabControl();
            tbpInicio = new TabPage();
            btnActivar = new Button();
            btnCerrarCtacte = new Button();
            lblFechaCreacion = new Label();
            lblFechaCreacionTitulo = new Label();
            lblEstado = new Label();
            lblEstadoTitulo = new Label();
            tbpMovimientos = new TabPage();
            tableLayoutPanel10 = new TableLayoutPanel();
            tableLayoutPanel9 = new TableLayoutPanel();
            tableLayoutPanel11 = new TableLayoutPanel();
            lblPagina = new Label();
            btnAnterior = new Button();
            btnSiguiente = new Button();
            lblTotalRegistros = new Label();
            pbxLogo = new PictureBox();
            dgvGrilla = new DataGridView();
            lblListadoMovimientos = new Label();
            tbpDnis = new TabPage();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidadMeses).BeginInit();
            tbcBase.SuspendLayout();
            tbpInicio.SuspendLayout();
            tbpMovimientos.SuspendLayout();
            tableLayoutPanel10.SuspendLayout();
            tableLayoutPanel9.SuspendLayout();
            tableLayoutPanel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).BeginInit();
            tbpDnis.SuspendLayout();
            SuspendLayout();
            // 
            // lblccorriente
            // 
            lblccorriente.AutoSize = true;
            lblccorriente.Location = new Point(6, 65);
            lblccorriente.Name = "lblccorriente";
            lblccorriente.Size = new Size(68, 15);
            lblccorriente.TabIndex = 0;
            lblccorriente.Text = "Nombre CC";
            // 
            // lblSaldo
            // 
            lblSaldo.AutoSize = true;
            lblSaldo.Location = new Point(584, 369);
            lblSaldo.Name = "lblSaldo";
            lblSaldo.Size = new Size(96, 15);
            lblSaldo.TabIndex = 1;
            lblSaldo.Text = "Saldo a favor: $0";
            // 
            // lblLimiteDeuda
            // 
            lblLimiteDeuda.AutoSize = true;
            lblLimiteDeuda.Location = new Point(173, 370);
            lblLimiteDeuda.Name = "lblLimiteDeuda";
            lblLimiteDeuda.Size = new Size(220, 15);
            lblLimiteDeuda.TabIndex = 3;
            lblLimiteDeuda.Text = "Deuda máxima permitida: Deshabilitada";
            // 
            // lblFechaVencimientoTitulo
            // 
            lblFechaVencimientoTitulo.AutoSize = true;
            lblFechaVencimientoTitulo.Location = new Point(22, 125);
            lblFechaVencimientoTitulo.Name = "lblFechaVencimientoTitulo";
            lblFechaVencimientoTitulo.Size = new Size(107, 15);
            lblFechaVencimientoTitulo.TabIndex = 5;
            lblFechaVencimientoTitulo.Text = "Fecha vencimiento";
            // 
            // chkLimiteDeuda
            // 
            chkLimiteDeuda.AutoSize = true;
            chkLimiteDeuda.Location = new Point(22, 335);
            chkLimiteDeuda.Name = "chkLimiteDeuda";
            chkLimiteDeuda.Size = new Size(104, 19);
            chkLimiteDeuda.TabIndex = 7;
            chkLimiteDeuda.Text = "Permitir deuda";
            chkLimiteDeuda.UseVisualStyleBackColor = true;
            chkLimiteDeuda.CheckedChanged += chkLimiteDeuda_CheckedChanged_1;
            // 
            // txtNombreCC
            // 
            txtNombreCC.Location = new Point(88, 62);
            txtNombreCC.Name = "txtNombreCC";
            txtNombreCC.Size = new Size(200, 23);
            txtNombreCC.TabIndex = 8;
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Location = new Point(201, 30);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(246, 15);
            lblDni.TabIndex = 13;
            lblDni.Text = "DNI autorizados para usar la cuenta corriente";
            // 
            // lstDnis
            // 
            lstDnis.FormattingEnabled = true;
            lstDnis.ItemHeight = 15;
            lstDnis.Location = new Point(201, 90);
            lstDnis.Name = "lstDnis";
            lstDnis.Size = new Size(282, 109);
            lstDnis.TabIndex = 19;
            // 
            // txtNuevoDni
            // 
            txtNuevoDni.Location = new Point(201, 55);
            txtNuevoDni.Name = "txtNuevoDni";
            txtNuevoDni.Size = new Size(160, 23);
            txtNuevoDni.TabIndex = 16;
            // 
            // btnAgregarDni
            // 
            btnAgregarDni.Location = new Point(367, 54);
            btnAgregarDni.Name = "btnAgregarDni";
            btnAgregarDni.Size = new Size(55, 25);
            btnAgregarDni.TabIndex = 17;
            btnAgregarDni.Text = "+";
            btnAgregarDni.UseVisualStyleBackColor = true;
            btnAgregarDni.Click += btnAgregarDni_Click;
            // 
            // btnEliminarDni
            // 
            btnEliminarDni.Location = new Point(428, 54);
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
            lblCliente.Location = new Point(6, 12);
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
            lblNombreCliente.Location = new Point(108, 12);
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
            groupBox1.Location = new Point(397, 29);
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
            rbVencimientoMensual.Location = new Point(6, 22);
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
            nudCantidadMeses.Location = new Point(695, 94);
            nudCantidadMeses.Name = "nudCantidadMeses";
            nudCantidadMeses.Size = new Size(35, 23);
            nudCantidadMeses.TabIndex = 25;
            nudCantidadMeses.ValueChanged += nudCantidadMeses_ValueChanged_1;
            // 
            // lblFechaVencimiento
            // 
            lblFechaVencimiento.AutoSize = true;
            lblFechaVencimiento.Location = new Point(223, 125);
            lblFechaVencimiento.Name = "lblFechaVencimiento";
            lblFechaVencimiento.Size = new Size(53, 15);
            lblFechaVencimiento.TabIndex = 26;
            lblFechaVencimiento.Text = "xx/xx/xx";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(395, 94);
            label2.Name = "label2";
            label2.Size = new Size(209, 15);
            label2.TabIndex = 27;
            label2.Text = "Cantidad de Meses (para vencimiento)";
            // 
            // btnCargarSaldoCtaCte
            // 
            btnCargarSaldoCtaCte.Location = new Point(433, 360);
            btnCargarSaldoCtaCte.Name = "btnCargarSaldoCtaCte";
            btnCargarSaldoCtaCte.Size = new Size(127, 33);
            btnCargarSaldoCtaCte.TabIndex = 32;
            btnCargarSaldoCtaCte.Text = "Cargar Saldo";
            btnCargarSaldoCtaCte.UseVisualStyleBackColor = true;
            btnCargarSaldoCtaCte.Click += btnCargarSaldoCtaCte_Click;
            // 
            // btnCargarLimite
            // 
            btnCargarLimite.Location = new Point(22, 360);
            btnCargarLimite.Name = "btnCargarLimite";
            btnCargarLimite.Size = new Size(127, 33);
            btnCargarLimite.TabIndex = 33;
            btnCargarLimite.Text = "Cargar Límite";
            btnCargarLimite.UseVisualStyleBackColor = true;
            btnCargarLimite.Click += btnCargarLimite_Click;
            // 
            // tbcBase
            // 
            tbcBase.Controls.Add(tbpInicio);
            tbcBase.Controls.Add(tbpMovimientos);
            tbcBase.Controls.Add(tbpDnis);
            tbcBase.Location = new Point(12, 62);
            tbcBase.Name = "tbcBase";
            tbcBase.SelectedIndex = 0;
            tbcBase.Size = new Size(772, 449);
            tbcBase.TabIndex = 34;
            // 
            // tbpInicio
            // 
            tbpInicio.Controls.Add(btnActivar);
            tbpInicio.Controls.Add(btnCerrarCtacte);
            tbpInicio.Controls.Add(lblFechaCreacion);
            tbpInicio.Controls.Add(lblFechaCreacionTitulo);
            tbpInicio.Controls.Add(lblEstado);
            tbpInicio.Controls.Add(lblEstadoTitulo);
            tbpInicio.Controls.Add(lblccorriente);
            tbpInicio.Controls.Add(btnCargarLimite);
            tbpInicio.Controls.Add(txtNombreCC);
            tbpInicio.Controls.Add(btnCargarSaldoCtaCte);
            tbpInicio.Controls.Add(lblCliente);
            tbpInicio.Controls.Add(label2);
            tbpInicio.Controls.Add(lblNombreCliente);
            tbpInicio.Controls.Add(lblLimiteDeuda);
            tbpInicio.Controls.Add(lblFechaVencimiento);
            tbpInicio.Controls.Add(lblFechaVencimientoTitulo);
            tbpInicio.Controls.Add(chkLimiteDeuda);
            tbpInicio.Controls.Add(nudCantidadMeses);
            tbpInicio.Controls.Add(groupBox1);
            tbpInicio.Controls.Add(lblSaldo);
            tbpInicio.Location = new Point(4, 24);
            tbpInicio.Name = "tbpInicio";
            tbpInicio.Padding = new Padding(3);
            tbpInicio.Size = new Size(764, 421);
            tbpInicio.TabIndex = 0;
            tbpInicio.Text = "Configuración";
            tbpInicio.UseVisualStyleBackColor = true;
            // 
            // btnActivar
            // 
            btnActivar.Location = new Point(536, 247);
            btnActivar.Name = "btnActivar";
            btnActivar.Size = new Size(127, 54);
            btnActivar.TabIndex = 49;
            btnActivar.Text = "Activar";
            btnActivar.UseVisualStyleBackColor = true;
            btnActivar.Click += btnActivar_Click;
            // 
            // btnCerrarCtacte
            // 
            btnCerrarCtacte.Location = new Point(536, 155);
            btnCerrarCtacte.Name = "btnCerrarCtacte";
            btnCerrarCtacte.Size = new Size(127, 54);
            btnCerrarCtacte.TabIndex = 48;
            btnCerrarCtacte.Text = "Cerrar Cuenta";
            btnCerrarCtacte.UseVisualStyleBackColor = true;
            btnCerrarCtacte.Click += btnCerrarCtacte_Click;
            // 
            // lblFechaCreacion
            // 
            lblFechaCreacion.AutoSize = true;
            lblFechaCreacion.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblFechaCreacion.Location = new Point(213, 163);
            lblFechaCreacion.Name = "lblFechaCreacion";
            lblFechaCreacion.Size = new Size(135, 32);
            lblFechaCreacion.TabIndex = 47;
            lblFechaCreacion.Tag = "NoModificarConBase";
            lblFechaCreacion.Text = "01/08/2026";
            // 
            // lblFechaCreacionTitulo
            // 
            lblFechaCreacionTitulo.AutoSize = true;
            lblFechaCreacionTitulo.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblFechaCreacionTitulo.Location = new Point(22, 163);
            lblFechaCreacionTitulo.Name = "lblFechaCreacionTitulo";
            lblFechaCreacionTitulo.Size = new Size(185, 32);
            lblFechaCreacionTitulo.TabIndex = 46;
            lblFechaCreacionTitulo.Tag = "NoModificarConBase";
            lblFechaCreacionTitulo.Text = "Fecha Creación:";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblEstado.Location = new Point(131, 227);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(114, 32);
            lblEstado.TabIndex = 45;
            lblEstado.Tag = "NoModificarConBase";
            lblEstado.Text = "**********";
            // 
            // lblEstadoTitulo
            // 
            lblEstadoTitulo.AutoSize = true;
            lblEstadoTitulo.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblEstadoTitulo.Location = new Point(22, 227);
            lblEstadoTitulo.Name = "lblEstadoTitulo";
            lblEstadoTitulo.Size = new Size(92, 32);
            lblEstadoTitulo.TabIndex = 44;
            lblEstadoTitulo.Tag = "NoModificarConBase";
            lblEstadoTitulo.Text = "Estado:";
            // 
            // tbpMovimientos
            // 
            tbpMovimientos.Controls.Add(tableLayoutPanel10);
            tbpMovimientos.Controls.Add(dgvGrilla);
            tbpMovimientos.Controls.Add(lblListadoMovimientos);
            tbpMovimientos.Location = new Point(4, 24);
            tbpMovimientos.Name = "tbpMovimientos";
            tbpMovimientos.Padding = new Padding(3);
            tbpMovimientos.Size = new Size(764, 421);
            tbpMovimientos.TabIndex = 1;
            tbpMovimientos.Text = "Movimientos";
            tbpMovimientos.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel10
            // 
            tableLayoutPanel10.Anchor = AnchorStyles.None;
            tableLayoutPanel10.ColumnCount = 2;
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.66667F));
            tableLayoutPanel10.Controls.Add(tableLayoutPanel9, 1, 0);
            tableLayoutPanel10.Controls.Add(pbxLogo, 0, 0);
            tableLayoutPanel10.Location = new Point(172, 317);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.RowCount = 1;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel10.Size = new Size(499, 58);
            tableLayoutPanel10.TabIndex = 37;
            // 
            // tableLayoutPanel9
            // 
            tableLayoutPanel9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel9.ColumnCount = 2;
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel9.Controls.Add(tableLayoutPanel11, 1, 0);
            tableLayoutPanel9.Controls.Add(lblTotalRegistros, 0, 0);
            tableLayoutPanel9.Location = new Point(169, 3);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.RowCount = 1;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel9.Size = new Size(327, 52);
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
            tableLayoutPanel11.Location = new Point(133, 3);
            tableLayoutPanel11.Name = "tableLayoutPanel11";
            tableLayoutPanel11.RowCount = 1;
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel11.Size = new Size(191, 46);
            tableLayoutPanel11.TabIndex = 7;
            // 
            // lblPagina
            // 
            lblPagina.Anchor = AnchorStyles.None;
            lblPagina.AutoSize = true;
            lblPagina.Font = new Font("Segoe UI Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPagina.Location = new Point(60, 10);
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
            btnAnterior.Size = new Size(41, 40);
            btnAnterior.TabIndex = 2;
            btnAnterior.Text = "<";
            btnAnterior.UseVisualStyleBackColor = true;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Anchor = AnchorStyles.None;
            btnSiguiente.Font = new Font("Segoe UI Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSiguiente.Location = new Point(145, 3);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(43, 40);
            btnSiguiente.TabIndex = 7;
            btnSiguiente.Text = ">";
            btnSiguiente.UseVisualStyleBackColor = true;
            // 
            // lblTotalRegistros
            // 
            lblTotalRegistros.Anchor = AnchorStyles.None;
            lblTotalRegistros.AutoSize = true;
            lblTotalRegistros.Font = new Font("Segoe UI Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalRegistros.Location = new Point(31, 13);
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
            pbxLogo.Size = new Size(83, 52);
            pbxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pbxLogo.TabIndex = 8;
            pbxLogo.TabStop = false;
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
            dgvGrilla.Location = new Point(6, 66);
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
            dgvGrilla.Size = new Size(665, 228);
            dgvGrilla.TabIndex = 35;
            dgvGrilla.MouseDown += dgvGrilla_MouseDown;
            // 
            // lblListadoMovimientos
            // 
            lblListadoMovimientos.AutoSize = true;
            lblListadoMovimientos.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblListadoMovimientos.Location = new Point(6, 20);
            lblListadoMovimientos.Name = "lblListadoMovimientos";
            lblListadoMovimientos.Size = new Size(155, 32);
            lblListadoMovimientos.TabIndex = 36;
            lblListadoMovimientos.Tag = "NoModificarConBase";
            lblListadoMovimientos.Text = "Movimientos";
            // 
            // tbpDnis
            // 
            tbpDnis.Controls.Add(lstDnis);
            tbpDnis.Controls.Add(btnEliminarDni);
            tbpDnis.Controls.Add(lblDni);
            tbpDnis.Controls.Add(btnAgregarDni);
            tbpDnis.Controls.Add(txtNuevoDni);
            tbpDnis.Location = new Point(4, 24);
            tbpDnis.Name = "tbpDnis";
            tbpDnis.Padding = new Padding(3);
            tbpDnis.Size = new Size(764, 421);
            tbpDnis.TabIndex = 2;
            tbpDnis.Text = "DNI habilitados";
            tbpDnis.UseVisualStyleBackColor = true;
            // 
            // FCuentaCorrienteABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 523);
            Controls.Add(tbcBase);
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FCuentaCorrienteABM";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ABM Cuenta Corriente";
            Load += FCuentaCorrienteABM_Load;
            Controls.SetChildIndex(tbcBase, 0);
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidadMeses).EndInit();
            tbcBase.ResumeLayout(false);
            tbpInicio.ResumeLayout(false);
            tbpInicio.PerformLayout();
            tbpMovimientos.ResumeLayout(false);
            tbpMovimientos.PerformLayout();
            tableLayoutPanel10.ResumeLayout(false);
            tableLayoutPanel9.ResumeLayout(false);
            tableLayoutPanel9.PerformLayout();
            tableLayoutPanel11.ResumeLayout(false);
            tableLayoutPanel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbxLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).EndInit();
            tbpDnis.ResumeLayout(false);
            tbpDnis.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblccorriente;
        private Label lblSaldo;
        private Label lblLimiteDeuda;
        private Label lblFechaVencimientoTitulo;
        private CheckBox chkLimiteDeuda;
        private TextBox txtNombreCC;
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
        private Label lblFechaVencimiento;
        private Label label2;
        private Button btnCargarSaldoCtaCte;
        private Button btnCargarLimite;
        private TabControl tbcBase;
        private TabPage tbpInicio;
        private TabPage tbpMovimientos;
        private TableLayoutPanel tableLayoutPanel10;
        private TableLayoutPanel tableLayoutPanel9;
        private TableLayoutPanel tableLayoutPanel11;
        protected Label lblPagina;
        private Button btnAnterior;
        private Button btnSiguiente;
        protected Label lblTotalRegistros;
        private PictureBox pbxLogo;
        protected DataGridView dgvGrilla;
        private Label lblListadoMovimientos;
        private TabPage tbpDnis;
        private Label lblEstado;
        private Label lblEstadoTitulo;
        private Label lblFechaCreacion;
        private Label lblFechaCreacionTitulo;
        private Button btnActivar;
        private Button btnCerrarCtacte;
    }
}