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
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCuentaCorrienteABM));
            grpLimite = new GroupBox();
            tableLayoutPanel15 = new TableLayoutPanel();
            chkLimiteDeuda = new CheckBox();
            btnCargarLimite = new Button();
            lblLimiteDeuda = new Label();
            grpVencimiento = new GroupBox();
            tableLayoutPanel17 = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            rbVencimientoManual = new RadioButton();
            rbVencimientoAutomatico = new RadioButton();
            flowLayoutPanel5 = new FlowLayoutPanel();
            label2 = new Label();
            nudCantidadMeses = new NumericUpDown();
            flowLayoutPanel7 = new FlowLayoutPanel();
            lblFechaVencimientoTitulo = new Label();
            lblFechaVencimiento = new Label();
            grpEstado = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            flowLayoutPanel3 = new FlowLayoutPanel();
            lblEstadoTitulo = new Label();
            lblEstado = new Label();
            flowLayoutPanel8 = new FlowLayoutPanel();
            lblFechaUltimaActivacionTitulo = new Label();
            lblFechaUltimaActivacion = new Label();
            btnActivar = new Button();
            btnCerrarCtacte = new Button();
            lblccorriente = new Label();
            lblSaldo = new Label();
            txtNombreCC = new TextBox();
            lblDni = new Label();
            lstDnis = new ListBox();
            txtNuevoDni = new TextBox();
            btnAgregarDni = new Button();
            btnEliminarDni = new Button();
            lblCliente = new Label();
            lblNombreCliente = new Label();
            btnCargarSaldoCtaCte = new Button();
            tbcBase = new TabControl();
            tbpInicio = new TabPage();
            tableLayoutPanel5 = new TableLayoutPanel();
            tableLayoutPanel13 = new TableLayoutPanel();
            lblNombreCliente2 = new Label();
            flowLayoutPanel6 = new FlowLayoutPanel();
            lblFechaCreacionTitulo = new Label();
            lblFechaCreacion = new Label();
            flowLayoutPanel2 = new FlowLayoutPanel();
            tableLayoutPanel14 = new TableLayoutPanel();
            tableLayoutPanel12 = new TableLayoutPanel();
            tbpMovimientos = new TabPage();
            dgvGrilla = new DataGridView();
            tableLayoutPanel10 = new TableLayoutPanel();
            tableLayoutPanel9 = new TableLayoutPanel();
            lblTotalRegistros = new Label();
            tableLayoutPanel11 = new TableLayoutPanel();
            btnAnterior = new Button();
            lblPagina = new Label();
            btnSiguiente = new Button();
            lblListadoMovimientos = new Label();
            tbpDnis = new TabPage();
            tableLayoutPanel18 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel16 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            pbxLogo = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            grpLimite.SuspendLayout();
            tableLayoutPanel15.SuspendLayout();
            grpVencimiento.SuspendLayout();
            tableLayoutPanel17.SuspendLayout();
            groupBox1.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidadMeses).BeginInit();
            flowLayoutPanel7.SuspendLayout();
            grpEstado.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel8.SuspendLayout();
            tbcBase.SuspendLayout();
            tbpInicio.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel13.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            tableLayoutPanel14.SuspendLayout();
            tableLayoutPanel12.SuspendLayout();
            tbpMovimientos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).BeginInit();
            tableLayoutPanel10.SuspendLayout();
            tableLayoutPanel9.SuspendLayout();
            tableLayoutPanel11.SuspendLayout();
            tbpDnis.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxLogo).BeginInit();
            SuspendLayout();
            // 
            // grpLimite
            // 
            grpLimite.Controls.Add(tableLayoutPanel15);
            grpLimite.Dock = DockStyle.Fill;
            grpLimite.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpLimite.ForeColor = Color.FromArgb(31, 26, 43);
            grpLimite.Location = new Point(0, 0);
            grpLimite.Margin = new Padding(0, 0, 6, 0);
            grpLimite.Name = "grpLimite";
            grpLimite.Padding = new Padding(10, 6, 10, 6);
            grpLimite.Size = new Size(345, 192);
            grpLimite.TabIndex = 64;
            grpLimite.TabStop = false;
            grpLimite.Text = "Límite de deuda";
            // 
            // tableLayoutPanel15
            // 
            tableLayoutPanel15.ColumnCount = 2;
            tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel15.Controls.Add(chkLimiteDeuda, 0, 0);
            tableLayoutPanel15.Controls.Add(btnCargarLimite, 1, 0);
            tableLayoutPanel15.Controls.Add(lblLimiteDeuda, 0, 1);
            tableLayoutPanel15.Dock = DockStyle.Fill;
            tableLayoutPanel15.Location = new Point(10, 24);
            tableLayoutPanel15.Margin = new Padding(0);
            tableLayoutPanel15.Name = "tableLayoutPanel15";
            tableLayoutPanel15.RowCount = 2;
            tableLayoutPanel15.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            tableLayoutPanel15.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel15.Size = new Size(325, 162);
            tableLayoutPanel15.TabIndex = 60;
            // 
            // chkLimiteDeuda
            // 
            chkLimiteDeuda.Anchor = AnchorStyles.Left;
            chkLimiteDeuda.AutoSize = true;
            chkLimiteDeuda.Font = new Font("Segoe UI", 10F);
            chkLimiteDeuda.ForeColor = Color.FromArgb(31, 26, 43);
            chkLimiteDeuda.Location = new Point(0, 25);
            chkLimiteDeuda.Margin = new Padding(0);
            chkLimiteDeuda.Name = "chkLimiteDeuda";
            chkLimiteDeuda.Size = new Size(117, 23);
            chkLimiteDeuda.TabIndex = 7;
            chkLimiteDeuda.Text = "Permitir deuda";
            chkLimiteDeuda.UseVisualStyleBackColor = true;
            chkLimiteDeuda.CheckedChanged += chkLimiteDeuda_CheckedChanged_1;
            // 
            // btnCargarLimite
            // 
            btnCargarLimite.Anchor = AnchorStyles.None;
            btnCargarLimite.BackColor = Color.White;
            btnCargarLimite.FlatAppearance.BorderColor = Color.FromArgb(67, 20, 135);
            btnCargarLimite.FlatStyle = FlatStyle.Flat;
            btnCargarLimite.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCargarLimite.ForeColor = Color.FromArgb(67, 20, 135);
            btnCargarLimite.Location = new Point(189, 20);
            btnCargarLimite.Margin = new Padding(0);
            btnCargarLimite.Name = "btnCargarLimite";
            btnCargarLimite.Size = new Size(124, 34);
            btnCargarLimite.TabIndex = 33;
            btnCargarLimite.Text = "Cargar límite";
            btnCargarLimite.UseVisualStyleBackColor = false;
            btnCargarLimite.Click += btnCargarLimite_Click;
            // 
            // lblLimiteDeuda
            // 
            tableLayoutPanel15.SetColumnSpan(lblLimiteDeuda, 2);
            lblLimiteDeuda.Dock = DockStyle.Fill;
            lblLimiteDeuda.Font = new Font("Segoe UI", 10F);
            lblLimiteDeuda.ForeColor = Color.FromArgb(95, 89, 105);
            lblLimiteDeuda.Location = new Point(0, 74);
            lblLimiteDeuda.Margin = new Padding(0);
            lblLimiteDeuda.Name = "lblLimiteDeuda";
            lblLimiteDeuda.Size = new Size(325, 88);
            lblLimiteDeuda.TabIndex = 3;
            lblLimiteDeuda.Text = "Deuda máxima permitida: Deshabilitada";
            lblLimiteDeuda.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // grpVencimiento
            // 
            grpVencimiento.Controls.Add(tableLayoutPanel17);
            grpVencimiento.Dock = DockStyle.Fill;
            grpVencimiento.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpVencimiento.ForeColor = Color.FromArgb(31, 26, 43);
            grpVencimiento.Location = new Point(357, 0);
            grpVencimiento.Margin = new Padding(6, 0, 0, 0);
            grpVencimiento.Name = "grpVencimiento";
            grpVencimiento.Padding = new Padding(10, 6, 10, 6);
            grpVencimiento.Size = new Size(345, 192);
            grpVencimiento.TabIndex = 65;
            grpVencimiento.TabStop = false;
            grpVencimiento.Text = "Vencimiento";
            // 
            // tableLayoutPanel17
            // 
            tableLayoutPanel17.ColumnCount = 1;
            tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel17.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel17.Controls.Add(flowLayoutPanel5, 0, 1);
            tableLayoutPanel17.Controls.Add(flowLayoutPanel7, 0, 2);
            tableLayoutPanel17.Dock = DockStyle.Fill;
            tableLayoutPanel17.Location = new Point(10, 24);
            tableLayoutPanel17.Margin = new Padding(0);
            tableLayoutPanel17.Name = "tableLayoutPanel17";
            tableLayoutPanel17.RowCount = 3;
            tableLayoutPanel17.RowStyles.Add(new RowStyle(SizeType.Absolute, 67F));
            tableLayoutPanel17.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel17.RowStyles.Add(new RowStyle(SizeType.Absolute, 17F));
            tableLayoutPanel17.Size = new Size(325, 162);
            tableLayoutPanel17.TabIndex = 61;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rbVencimientoManual);
            groupBox1.Controls.Add(rbVencimientoAutomatico);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBox1.ForeColor = Color.FromArgb(31, 26, 43);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(0);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(10, 4, 10, 4);
            groupBox1.Size = new Size(325, 67);
            groupBox1.TabIndex = 24;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tipo de vencimiento";
            // 
            // rbVencimientoManual
            // 
            rbVencimientoManual.AutoSize = true;
            rbVencimientoManual.Font = new Font("Segoe UI", 10F);
            rbVencimientoManual.Location = new Point(175, 22);
            rbVencimientoManual.Name = "rbVencimientoManual";
            rbVencimientoManual.Size = new Size(73, 23);
            rbVencimientoManual.TabIndex = 1;
            rbVencimientoManual.TabStop = true;
            rbVencimientoManual.Text = "Manual";
            rbVencimientoManual.UseVisualStyleBackColor = true;
            rbVencimientoManual.CheckedChanged += rbVencimientoManual_CheckedChanged;
            // 
            // rbVencimientoAutomatico
            // 
            rbVencimientoAutomatico.AutoSize = true;
            rbVencimientoAutomatico.Font = new Font("Segoe UI", 10F);
            rbVencimientoAutomatico.Location = new Point(10, 22);
            rbVencimientoAutomatico.Name = "rbVencimientoAutomatico";
            rbVencimientoAutomatico.Size = new Size(98, 23);
            rbVencimientoAutomatico.TabIndex = 0;
            rbVencimientoAutomatico.TabStop = true;
            rbVencimientoAutomatico.Text = "Automático";
            rbVencimientoAutomatico.UseVisualStyleBackColor = true;
            rbVencimientoAutomatico.CheckedChanged += rbVencimientoMensual_CheckedChanged;
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.Controls.Add(label2);
            flowLayoutPanel5.Controls.Add(nudCantidadMeses);
            flowLayoutPanel5.Dock = DockStyle.Fill;
            flowLayoutPanel5.Location = new Point(0, 67);
            flowLayoutPanel5.Margin = new Padding(0);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Padding = new Padding(0, 4, 0, 0);
            flowLayoutPanel5.Size = new Size(325, 32);
            flowLayoutPanel5.TabIndex = 53;
            flowLayoutPanel5.WrapContents = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.ForeColor = Color.FromArgb(95, 89, 105);
            label2.Location = new Point(0, 7);
            label2.Margin = new Padding(0, 3, 0, 0);
            label2.Name = "label2";
            label2.Size = new Size(128, 19);
            label2.TabIndex = 27;
            label2.Text = "Cantidad de meses:";
            // 
            // nudCantidadMeses
            // 
            nudCantidadMeses.Anchor = AnchorStyles.Left;
            nudCantidadMeses.BackColor = Color.White;
            nudCantidadMeses.BorderStyle = BorderStyle.FixedSingle;
            nudCantidadMeses.Font = new Font("Segoe UI", 10F);
            nudCantidadMeses.Location = new Point(134, 4);
            nudCantidadMeses.Margin = new Padding(6, 0, 0, 0);
            nudCantidadMeses.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            nudCantidadMeses.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidadMeses.Name = "nudCantidadMeses";
            nudCantidadMeses.Size = new Size(70, 25);
            nudCantidadMeses.TabIndex = 25;
            nudCantidadMeses.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidadMeses.ValueChanged += nudCantidadMeses_ValueChanged_1;
            // 
            // flowLayoutPanel7
            // 
            flowLayoutPanel7.Anchor = AnchorStyles.Left;
            flowLayoutPanel7.Controls.Add(lblFechaVencimientoTitulo);
            flowLayoutPanel7.Controls.Add(lblFechaVencimiento);
            flowLayoutPanel7.Location = new Point(0, 110);
            flowLayoutPanel7.Margin = new Padding(0);
            flowLayoutPanel7.Name = "flowLayoutPanel7";
            flowLayoutPanel7.Padding = new Padding(0, 4, 0, 0);
            flowLayoutPanel7.Size = new Size(325, 40);
            flowLayoutPanel7.TabIndex = 54;
            flowLayoutPanel7.WrapContents = false;
            // 
            // lblFechaVencimientoTitulo
            // 
            lblFechaVencimientoTitulo.AutoSize = true;
            lblFechaVencimientoTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFechaVencimientoTitulo.ForeColor = Color.FromArgb(31, 26, 43);
            lblFechaVencimientoTitulo.Location = new Point(0, 4);
            lblFechaVencimientoTitulo.Margin = new Padding(0);
            lblFechaVencimientoTitulo.Name = "lblFechaVencimientoTitulo";
            lblFechaVencimientoTitulo.Size = new Size(157, 19);
            lblFechaVencimientoTitulo.TabIndex = 5;
            lblFechaVencimientoTitulo.Text = "Próximo Vencimiento:";
            lblFechaVencimientoTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblFechaVencimiento
            // 
            lblFechaVencimiento.AutoSize = true;
            lblFechaVencimiento.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblFechaVencimiento.ForeColor = Color.FromArgb(67, 20, 135);
            lblFechaVencimiento.Location = new Point(165, 4);
            lblFechaVencimiento.Margin = new Padding(8, 0, 0, 0);
            lblFechaVencimiento.Name = "lblFechaVencimiento";
            lblFechaVencimiento.Size = new Size(83, 19);
            lblFechaVencimiento.TabIndex = 26;
            lblFechaVencimiento.Text = "01/08/2026";
            lblFechaVencimiento.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpEstado
            // 
            grpEstado.Controls.Add(tableLayoutPanel3);
            grpEstado.Dock = DockStyle.Fill;
            grpEstado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpEstado.ForeColor = Color.FromArgb(31, 26, 43);
            grpEstado.Location = new Point(0, 290);
            grpEstado.Margin = new Padding(0, 4, 0, 4);
            grpEstado.Name = "grpEstado";
            grpEstado.Padding = new Padding(10, 4, 10, 6);
            grpEstado.Size = new Size(702, 72);
            grpEstado.TabIndex = 66;
            grpEstado.TabStop = false;
            grpEstado.Text = "Estado de la cuenta";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel3.ColumnCount = 4;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.29932F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.70068F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tableLayoutPanel3.Controls.Add(flowLayoutPanel3, 0, 0);
            tableLayoutPanel3.Controls.Add(flowLayoutPanel8, 1, 0);
            tableLayoutPanel3.Controls.Add(btnActivar, 2, 0);
            tableLayoutPanel3.Controls.Add(btnCerrarCtacte, 3, 0);
            tableLayoutPanel3.Location = new Point(10, 22);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(682, 44);
            tableLayoutPanel3.TabIndex = 53;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Anchor = AnchorStyles.Left;
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.Controls.Add(lblEstadoTitulo);
            flowLayoutPanel3.Controls.Add(lblEstado);
            flowLayoutPanel3.Location = new Point(0, 10);
            flowLayoutPanel3.Margin = new Padding(0);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(161, 23);
            flowLayoutPanel3.TabIndex = 52;
            flowLayoutPanel3.WrapContents = false;
            // 
            // lblEstadoTitulo
            // 
            lblEstadoTitulo.FlatStyle = FlatStyle.Flat;
            lblEstadoTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEstadoTitulo.Location = new Point(3, 0);
            lblEstadoTitulo.Name = "lblEstadoTitulo";
            lblEstadoTitulo.Size = new Size(78, 23);
            lblEstadoTitulo.TabIndex = 0;
            lblEstadoTitulo.Text = "Estado:";
            // 
            // lblEstado
            // 
            lblEstado.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblEstado.Location = new Point(87, 0);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(71, 23);
            lblEstado.TabIndex = 1;
            lblEstado.Text = "Estado";
            // 
            // flowLayoutPanel8
            // 
            flowLayoutPanel8.Anchor = AnchorStyles.Left;
            flowLayoutPanel8.AutoSize = true;
            flowLayoutPanel8.Controls.Add(lblFechaUltimaActivacionTitulo);
            flowLayoutPanel8.Controls.Add(lblFechaUltimaActivacion);
            flowLayoutPanel8.Location = new Point(213, 10);
            flowLayoutPanel8.Margin = new Padding(0);
            flowLayoutPanel8.Name = "flowLayoutPanel8";
            flowLayoutPanel8.Size = new Size(228, 23);
            flowLayoutPanel8.TabIndex = 55;
            flowLayoutPanel8.WrapContents = false;
            // 
            // lblFechaUltimaActivacionTitulo
            // 
            lblFechaUltimaActivacionTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFechaUltimaActivacionTitulo.ForeColor = Color.Black;
            lblFechaUltimaActivacionTitulo.Location = new Point(3, 0);
            lblFechaUltimaActivacionTitulo.Name = "lblFechaUltimaActivacionTitulo";
            lblFechaUltimaActivacionTitulo.Size = new Size(132, 23);
            lblFechaUltimaActivacionTitulo.TabIndex = 0;
            lblFechaUltimaActivacionTitulo.Text = "Última Activación:";
            // 
            // lblFechaUltimaActivacion
            // 
            lblFechaUltimaActivacion.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblFechaUltimaActivacion.ForeColor = Color.FromArgb(67, 20, 135);
            lblFechaUltimaActivacion.Location = new Point(141, 0);
            lblFechaUltimaActivacion.Name = "lblFechaUltimaActivacion";
            lblFechaUltimaActivacion.Size = new Size(100, 23);
            lblFechaUltimaActivacion.TabIndex = 1;
            lblFechaUltimaActivacion.Text = "01/08/2026";
            // 
            // btnActivar
            // 
            btnActivar.Anchor = AnchorStyles.None;
            btnActivar.Location = new Point(446, 5);
            btnActivar.Margin = new Padding(0);
            btnActivar.Name = "btnActivar";
            btnActivar.Size = new Size(100, 34);
            btnActivar.TabIndex = 56;
            btnActivar.Text = "Activar";
            btnActivar.Click += btnActivar_Click;
            // 
            // btnCerrarCtacte
            // 
            btnCerrarCtacte.Anchor = AnchorStyles.None;
            btnCerrarCtacte.Location = new Point(556, 5);
            btnCerrarCtacte.Margin = new Padding(0);
            btnCerrarCtacte.Name = "btnCerrarCtacte";
            btnCerrarCtacte.Size = new Size(120, 34);
            btnCerrarCtacte.TabIndex = 57;
            btnCerrarCtacte.Text = "Cerrar Cuenta";
            btnCerrarCtacte.Click += btnCerrarCtacte_Click;
            // 
            // lblccorriente
            // 
            lblccorriente.Anchor = AnchorStyles.Left;
            lblccorriente.AutoSize = true;
            lblccorriente.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblccorriente.ForeColor = Color.FromArgb(31, 26, 43);
            lblccorriente.Location = new Point(0, 11);
            lblccorriente.Margin = new Padding(0, 0, 10, 0);
            lblccorriente.Name = "lblccorriente";
            lblccorriente.Size = new Size(91, 19);
            lblccorriente.TabIndex = 0;
            lblccorriente.Text = "Nombre CC:";
            lblccorriente.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSaldo
            // 
            lblSaldo.Anchor = AnchorStyles.Left;
            lblSaldo.AutoSize = true;
            lblSaldo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSaldo.ForeColor = Color.FromArgb(31, 26, 43);
            lblSaldo.Location = new Point(99, 14);
            lblSaldo.Margin = new Padding(0);
            lblSaldo.Name = "lblSaldo";
            lblSaldo.Size = new Size(207, 32);
            lblSaldo.TabIndex = 1;
            lblSaldo.Text = "Saldo a favor: $0";
            lblSaldo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtNombreCC
            // 
            txtNombreCC.Anchor = AnchorStyles.Left;
            txtNombreCC.BackColor = Color.White;
            txtNombreCC.BorderStyle = BorderStyle.FixedSingle;
            txtNombreCC.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            txtNombreCC.ForeColor = Color.FromArgb(31, 26, 43);
            txtNombreCC.Location = new Point(101, 8);
            txtNombreCC.Margin = new Padding(0);
            txtNombreCC.Name = "txtNombreCC";
            txtNombreCC.Size = new Size(300, 26);
            txtNombreCC.TabIndex = 8;
            // 
            // lblDni
            // 
            lblDni.Dock = DockStyle.Top;
            lblDni.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDni.ForeColor = Color.FromArgb(31, 26, 43);
            lblDni.Location = new Point(16, 16);
            lblDni.Margin = new Padding(0);
            lblDni.Name = "lblDni";
            lblDni.Padding = new Padding(0, 0, 0, 8);
            lblDni.Size = new Size(690, 33);
            lblDni.TabIndex = 13;
            lblDni.Text = "DNI autorizados para usar la cuenta corriente";
            lblDni.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lstDnis
            // 
            lstDnis.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstDnis.BackColor = Color.White;
            lstDnis.BorderStyle = BorderStyle.FixedSingle;
            lstDnis.Font = new Font("Segoe UI", 10F);
            lstDnis.ForeColor = Color.FromArgb(31, 26, 43);
            lstDnis.FormattingEnabled = true;
            lstDnis.ItemHeight = 17;
            lstDnis.Location = new Point(16, 93);
            lstDnis.Name = "lstDnis";
            lstDnis.Size = new Size(744, 223);
            lstDnis.TabIndex = 19;
            // 
            // txtNuevoDni
            // 
            txtNuevoDni.BackColor = Color.White;
            txtNuevoDni.BorderStyle = BorderStyle.FixedSingle;
            txtNuevoDni.Font = new Font("Segoe UI", 10F);
            txtNuevoDni.ForeColor = Color.FromArgb(31, 26, 43);
            txtNuevoDni.Location = new Point(16, 56);
            txtNuevoDni.Name = "txtNuevoDni";
            txtNuevoDni.PlaceholderText = "Ingresar DNI...";
            txtNuevoDni.Size = new Size(220, 25);
            txtNuevoDni.TabIndex = 16;
            // 
            // btnAgregarDni
            // 
            btnAgregarDni.BackColor = Color.FromArgb(67, 20, 135);
            btnAgregarDni.FlatAppearance.BorderSize = 0;
            btnAgregarDni.FlatStyle = FlatStyle.Flat;
            btnAgregarDni.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAgregarDni.ForeColor = Color.White;
            btnAgregarDni.Location = new Point(248, 53);
            btnAgregarDni.Margin = new Padding(8, 0, 0, 0);
            btnAgregarDni.Name = "btnAgregarDni";
            btnAgregarDni.Size = new Size(100, 30);
            btnAgregarDni.TabIndex = 17;
            btnAgregarDni.Text = "Agregar";
            btnAgregarDni.UseVisualStyleBackColor = false;
            btnAgregarDni.Click += btnAgregarDni_Click;
            // 
            // btnEliminarDni
            // 
            btnEliminarDni.BackColor = Color.White;
            btnEliminarDni.FlatAppearance.BorderColor = Color.FromArgb(67, 20, 135);
            btnEliminarDni.FlatStyle = FlatStyle.Flat;
            btnEliminarDni.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEliminarDni.ForeColor = Color.FromArgb(67, 20, 135);
            btnEliminarDni.Location = new Point(356, 53);
            btnEliminarDni.Margin = new Padding(8, 0, 0, 0);
            btnEliminarDni.Name = "btnEliminarDni";
            btnEliminarDni.Size = new Size(100, 30);
            btnEliminarDni.TabIndex = 18;
            btnEliminarDni.Text = "Quitar";
            btnEliminarDni.UseVisualStyleBackColor = false;
            btnEliminarDni.Click += btnEliminarDni_Click;
            // 
            // lblCliente
            // 
            lblCliente.Dock = DockStyle.Fill;
            lblCliente.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCliente.ForeColor = Color.FromArgb(31, 26, 43);
            lblCliente.Location = new Point(0, 0);
            lblCliente.Margin = new Padding(0);
            lblCliente.Name = "lblCliente";
            lblCliente.Padding = new Padding(0, 4, 0, 0);
            lblCliente.Size = new Size(96, 0);
            lblCliente.TabIndex = 20;
            lblCliente.Tag = "NoModificarConBase";
            lblCliente.Text = "Cliente:";
            lblCliente.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblNombreCliente
            // 
            lblNombreCliente.Dock = DockStyle.Fill;
            lblNombreCliente.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblNombreCliente.ForeColor = Color.FromArgb(67, 20, 135);
            lblNombreCliente.Location = new Point(0, 0);
            lblNombreCliente.Margin = new Padding(0);
            lblNombreCliente.Name = "lblNombreCliente";
            lblNombreCliente.Padding = new Padding(8, 4, 0, 0);
            lblNombreCliente.Size = new Size(636, 0);
            lblNombreCliente.TabIndex = 21;
            lblNombreCliente.Tag = "NoModificarConBase";
            lblNombreCliente.Text = "**********";
            lblNombreCliente.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnCargarSaldoCtaCte
            // 
            btnCargarSaldoCtaCte.Anchor = AnchorStyles.None;
            btnCargarSaldoCtaCte.BackColor = Color.White;
            btnCargarSaldoCtaCte.FlatAppearance.BorderColor = Color.FromArgb(67, 20, 135);
            btnCargarSaldoCtaCte.FlatStyle = FlatStyle.Flat;
            btnCargarSaldoCtaCte.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCargarSaldoCtaCte.ForeColor = Color.FromArgb(67, 20, 135);
            btnCargarSaldoCtaCte.Location = new Point(477, 10);
            btnCargarSaldoCtaCte.Margin = new Padding(0);
            btnCargarSaldoCtaCte.Name = "btnCargarSaldoCtaCte";
            btnCargarSaldoCtaCte.Size = new Size(140, 40);
            btnCargarSaldoCtaCte.TabIndex = 32;
            btnCargarSaldoCtaCte.Text = "Cargar saldo";
            btnCargarSaldoCtaCte.UseVisualStyleBackColor = false;
            btnCargarSaldoCtaCte.Click += btnCargarSaldoCtaCte_Click;
            // 
            // tbcBase
            // 
            tbcBase.Controls.Add(tbpInicio);
            tbcBase.Controls.Add(tbpMovimientos);
            tbcBase.Controls.Add(tbpDnis);
            tbcBase.Dock = DockStyle.Fill;
            tbcBase.ItemSize = new Size(120, 27);
            tbcBase.Location = new Point(0, 53);
            tbcBase.Name = "tbcBase";
            tbcBase.Padding = new Point(12, 5);
            tbcBase.SelectedIndex = 0;
            tbcBase.Size = new Size(730, 481);
            tbcBase.TabIndex = 34;
            // 
            // tbpInicio
            // 
            tbpInicio.Controls.Add(tableLayoutPanel5);
            tbpInicio.Font = new Font("Segoe UI", 10F);
            tbpInicio.Location = new Point(4, 31);
            tbpInicio.Name = "tbpInicio";
            tbpInicio.Padding = new Padding(10);
            tbpInicio.Size = new Size(722, 446);
            tbpInicio.TabIndex = 0;
            tbpInicio.Text = "Configuración";
            tbpInicio.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(tableLayoutPanel13, 0, 0);
            tableLayoutPanel5.Controls.Add(flowLayoutPanel2, 0, 1);
            tableLayoutPanel5.Controls.Add(tableLayoutPanel14, 0, 2);
            tableLayoutPanel5.Controls.Add(grpEstado, 0, 3);
            tableLayoutPanel5.Controls.Add(tableLayoutPanel12, 0, 4);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(10, 10);
            tableLayoutPanel5.Margin = new Padding(0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 5;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel5.Size = new Size(702, 426);
            tableLayoutPanel5.TabIndex = 63;
            // 
            // tableLayoutPanel13
            // 
            tableLayoutPanel13.ColumnCount = 2;
            tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel13.Controls.Add(lblNombreCliente2, 0, 0);
            tableLayoutPanel13.Controls.Add(flowLayoutPanel6, 1, 0);
            tableLayoutPanel13.Dock = DockStyle.Fill;
            tableLayoutPanel13.Location = new Point(0, 0);
            tableLayoutPanel13.Margin = new Padding(0);
            tableLayoutPanel13.Name = "tableLayoutPanel13";
            tableLayoutPanel13.RowCount = 1;
            tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel13.Size = new Size(702, 50);
            tableLayoutPanel13.TabIndex = 58;
            // 
            // lblNombreCliente2
            // 
            lblNombreCliente2.Anchor = AnchorStyles.Left;
            lblNombreCliente2.AutoSize = true;
            lblNombreCliente2.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNombreCliente2.ForeColor = Color.FromArgb(67, 20, 135);
            lblNombreCliente2.Location = new Point(0, 2);
            lblNombreCliente2.Margin = new Padding(0);
            lblNombreCliente2.Name = "lblNombreCliente2";
            lblNombreCliente2.Size = new Size(102, 45);
            lblNombreCliente2.TabIndex = 58;
            lblNombreCliente2.Text = "label1";
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.Anchor = AnchorStyles.Right;
            flowLayoutPanel6.AutoSize = true;
            flowLayoutPanel6.Controls.Add(lblFechaCreacionTitulo);
            flowLayoutPanel6.Controls.Add(lblFechaCreacion);
            flowLayoutPanel6.Location = new Point(442, 13);
            flowLayoutPanel6.Margin = new Padding(0);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Size = new Size(260, 23);
            flowLayoutPanel6.TabIndex = 53;
            flowLayoutPanel6.WrapContents = false;
            // 
            // lblFechaCreacionTitulo
            // 
            lblFechaCreacionTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFechaCreacionTitulo.ForeColor = Color.Black;
            lblFechaCreacionTitulo.Location = new Point(3, 0);
            lblFechaCreacionTitulo.Name = "lblFechaCreacionTitulo";
            lblFechaCreacionTitulo.Size = new Size(148, 23);
            lblFechaCreacionTitulo.TabIndex = 0;
            lblFechaCreacionTitulo.Text = "Fecha de Creación:";
            lblFechaCreacionTitulo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblFechaCreacion
            // 
            lblFechaCreacion.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblFechaCreacion.ForeColor = Color.FromArgb(67, 20, 135);
            lblFechaCreacion.Location = new Point(157, 0);
            lblFechaCreacion.Name = "lblFechaCreacion";
            lblFechaCreacion.Size = new Size(100, 23);
            lblFechaCreacion.TabIndex = 1;
            lblFechaCreacion.Text = "01/08/2026";
            lblFechaCreacion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(lblccorriente);
            flowLayoutPanel2.Controls.Add(txtNombreCC);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.Location = new Point(0, 50);
            flowLayoutPanel2.Margin = new Padding(0);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Padding = new Padding(0, 8, 0, 0);
            flowLayoutPanel2.Size = new Size(702, 44);
            flowLayoutPanel2.TabIndex = 52;
            flowLayoutPanel2.WrapContents = false;
            // 
            // tableLayoutPanel14
            // 
            tableLayoutPanel14.ColumnCount = 2;
            tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel14.Controls.Add(grpLimite, 0, 0);
            tableLayoutPanel14.Controls.Add(grpVencimiento, 1, 0);
            tableLayoutPanel14.Dock = DockStyle.Fill;
            tableLayoutPanel14.Location = new Point(0, 94);
            tableLayoutPanel14.Margin = new Padding(0);
            tableLayoutPanel14.Name = "tableLayoutPanel14";
            tableLayoutPanel14.RowCount = 1;
            tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel14.Size = new Size(702, 192);
            tableLayoutPanel14.TabIndex = 59;
            // 
            // tableLayoutPanel12
            // 
            tableLayoutPanel12.ColumnCount = 3;
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 293F));
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 310F));
            tableLayoutPanel12.Controls.Add(btnCargarSaldoCtaCte, 2, 0);
            tableLayoutPanel12.Controls.Add(lblSaldo, 1, 0);
            tableLayoutPanel12.Dock = DockStyle.Fill;
            tableLayoutPanel12.Location = new Point(0, 366);
            tableLayoutPanel12.Margin = new Padding(0);
            tableLayoutPanel12.Name = "tableLayoutPanel12";
            tableLayoutPanel12.RowCount = 1;
            tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel12.Size = new Size(702, 60);
            tableLayoutPanel12.TabIndex = 57;
            // 
            // tbpMovimientos
            // 
            tbpMovimientos.Controls.Add(dgvGrilla);
            tbpMovimientos.Controls.Add(tableLayoutPanel10);
            tbpMovimientos.Controls.Add(lblListadoMovimientos);
            tbpMovimientos.Location = new Point(4, 31);
            tbpMovimientos.Name = "tbpMovimientos";
            tbpMovimientos.Padding = new Padding(10);
            tbpMovimientos.Size = new Size(722, 446);
            tbpMovimientos.TabIndex = 1;
            tbpMovimientos.Text = "Movimientos";
            tbpMovimientos.UseVisualStyleBackColor = true;
            // 
            // dgvGrilla
            // 
            dgvGrilla.AllowUserToAddRows = false;
            dgvGrilla.AllowUserToDeleteRows = false;
            dgvGrilla.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(250, 248, 252);
            dgvGrilla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvGrilla.BackgroundColor = Color.White;
            dgvGrilla.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvGrilla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(67, 20, 135);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(67, 20, 135);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvGrilla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvGrilla.ColumnHeadersHeight = 34;
            dgvGrilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(31, 26, 43);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(232, 222, 244);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(31, 26, 43);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvGrilla.DefaultCellStyle = dataGridViewCellStyle3;
            dgvGrilla.Dock = DockStyle.Fill;
            dgvGrilla.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvGrilla.GridColor = Color.FromArgb(230, 226, 234);
            dgvGrilla.Location = new Point(10, 44);
            dgvGrilla.MultiSelect = false;
            dgvGrilla.Name = "dgvGrilla";
            dgvGrilla.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(247, 245, 250);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(31, 26, 43);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(247, 245, 250);
            dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(31, 26, 43);
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvGrilla.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvGrilla.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle5.Padding = new Padding(4, 0, 4, 0);
            dgvGrilla.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dgvGrilla.RowTemplate.Height = 30;
            dgvGrilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGrilla.Size = new Size(702, 346);
            dgvGrilla.TabIndex = 35;
            dgvGrilla.MouseDown += dgvGrilla_MouseDown;
            // 
            // tableLayoutPanel10
            // 
            tableLayoutPanel10.ColumnCount = 1;
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel10.Controls.Add(tableLayoutPanel9, 0, 0);
            tableLayoutPanel10.Dock = DockStyle.Bottom;
            tableLayoutPanel10.Location = new Point(10, 390);
            tableLayoutPanel10.Margin = new Padding(0);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.Padding = new Padding(0, 6, 0, 0);
            tableLayoutPanel10.RowCount = 1;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel10.Size = new Size(702, 46);
            tableLayoutPanel10.TabIndex = 37;
            // 
            // tableLayoutPanel9
            // 
            tableLayoutPanel9.ColumnCount = 2;
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel9.Controls.Add(lblTotalRegistros, 0, 0);
            tableLayoutPanel9.Controls.Add(tableLayoutPanel11, 1, 0);
            tableLayoutPanel9.Dock = DockStyle.Fill;
            tableLayoutPanel9.Location = new Point(0, 6);
            tableLayoutPanel9.Margin = new Padding(0);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.RowCount = 1;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel9.Size = new Size(702, 40);
            tableLayoutPanel9.TabIndex = 9;
            // 
            // lblTotalRegistros
            // 
            lblTotalRegistros.Anchor = AnchorStyles.Left;
            lblTotalRegistros.AutoSize = true;
            lblTotalRegistros.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTotalRegistros.ForeColor = Color.FromArgb(95, 89, 105);
            lblTotalRegistros.Location = new Point(0, 11);
            lblTotalRegistros.Margin = new Padding(0);
            lblTotalRegistros.Name = "lblTotalRegistros";
            lblTotalRegistros.Size = new Size(54, 17);
            lblTotalRegistros.TabIndex = 0;
            lblTotalRegistros.Text = "Total: 0";
            lblTotalRegistros.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel11
            // 
            tableLayoutPanel11.ColumnCount = 3;
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel11.Controls.Add(btnAnterior, 0, 0);
            tableLayoutPanel11.Controls.Add(lblPagina, 1, 0);
            tableLayoutPanel11.Controls.Add(btnSiguiente, 2, 0);
            tableLayoutPanel11.Dock = DockStyle.Fill;
            tableLayoutPanel11.Location = new Point(280, 0);
            tableLayoutPanel11.Margin = new Padding(0);
            tableLayoutPanel11.Name = "tableLayoutPanel11";
            tableLayoutPanel11.RowCount = 1;
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel11.Size = new Size(422, 40);
            tableLayoutPanel11.TabIndex = 7;
            // 
            // btnAnterior
            // 
            btnAnterior.Anchor = AnchorStyles.None;
            btnAnterior.BackColor = Color.White;
            btnAnterior.FlatAppearance.BorderColor = Color.FromArgb(218, 212, 226);
            btnAnterior.FlatStyle = FlatStyle.Flat;
            btnAnterior.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAnterior.ForeColor = Color.FromArgb(67, 20, 135);
            btnAnterior.Location = new Point(47, 6);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(45, 28);
            btnAnterior.TabIndex = 2;
            btnAnterior.Text = "‹";
            btnAnterior.UseVisualStyleBackColor = false;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // lblPagina
            // 
            lblPagina.Anchor = AnchorStyles.None;
            lblPagina.AutoSize = true;
            lblPagina.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPagina.ForeColor = Color.FromArgb(31, 26, 43);
            lblPagina.Location = new Point(180, 11);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(61, 17);
            lblPagina.TabIndex = 1;
            lblPagina.Text = "Página 1";
            lblPagina.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Anchor = AnchorStyles.None;
            btnSiguiente.BackColor = Color.White;
            btnSiguiente.FlatAppearance.BorderColor = Color.FromArgb(218, 212, 226);
            btnSiguiente.FlatStyle = FlatStyle.Flat;
            btnSiguiente.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSiguiente.ForeColor = Color.FromArgb(67, 20, 135);
            btnSiguiente.Location = new Point(329, 6);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(45, 28);
            btnSiguiente.TabIndex = 7;
            btnSiguiente.Text = "›";
            btnSiguiente.UseVisualStyleBackColor = false;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // lblListadoMovimientos
            // 
            lblListadoMovimientos.Dock = DockStyle.Top;
            lblListadoMovimientos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblListadoMovimientos.ForeColor = Color.FromArgb(31, 26, 43);
            lblListadoMovimientos.Location = new Point(10, 10);
            lblListadoMovimientos.Margin = new Padding(0);
            lblListadoMovimientos.Name = "lblListadoMovimientos";
            lblListadoMovimientos.Padding = new Padding(0, 0, 0, 8);
            lblListadoMovimientos.Size = new Size(702, 34);
            lblListadoMovimientos.TabIndex = 36;
            lblListadoMovimientos.Tag = "NoModificarConBase";
            lblListadoMovimientos.Text = "Movimientos";
            lblListadoMovimientos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tbpDnis
            // 
            tbpDnis.Controls.Add(lstDnis);
            tbpDnis.Controls.Add(btnEliminarDni);
            tbpDnis.Controls.Add(btnAgregarDni);
            tbpDnis.Controls.Add(txtNuevoDni);
            tbpDnis.Controls.Add(lblDni);
            tbpDnis.Location = new Point(4, 31);
            tbpDnis.Name = "tbpDnis";
            tbpDnis.Padding = new Padding(16);
            tbpDnis.Size = new Size(722, 446);
            tbpDnis.TabIndex = 2;
            tbpDnis.Text = "DNI habilitados";
            tbpDnis.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel18
            // 
            tableLayoutPanel18.Location = new Point(0, 0);
            tableLayoutPanel18.Name = "tableLayoutPanel18";
            tableLayoutPanel18.Size = new Size(200, 100);
            tableLayoutPanel18.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.Location = new Point(0, 0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.Size = new Size(200, 100);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // tableLayoutPanel16
            // 
            tableLayoutPanel16.Location = new Point(0, 0);
            tableLayoutPanel16.Name = "tableLayoutPanel16";
            tableLayoutPanel16.Size = new Size(200, 100);
            tableLayoutPanel16.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.Size = new Size(200, 100);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(744, 321);
            tableLayoutPanel1.TabIndex = 35;
            tableLayoutPanel1.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(lblCliente);
            flowLayoutPanel1.Controls.Add(lblNombreCliente);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Margin = new Padding(0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(200, 30);
            flowLayoutPanel1.TabIndex = 52;
            flowLayoutPanel1.Visible = false;
            // 
            // pbxLogo
            // 
            pbxLogo.Location = new Point(0, 0);
            pbxLogo.Name = "pbxLogo";
            pbxLogo.Size = new Size(1, 1);
            pbxLogo.TabIndex = 8;
            pbxLogo.TabStop = false;
            pbxLogo.Visible = false;
            // 
            // FCuentaCorrienteABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(730, 534);
            Controls.Add(tbcBase);
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(746, 573);
            Name = "FCuentaCorrienteABM";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ABM Cuenta Corriente";
            Load += FCuentaCorrienteABM_Load;
            Controls.SetChildIndex(tbcBase, 0);
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            grpLimite.ResumeLayout(false);
            tableLayoutPanel15.ResumeLayout(false);
            tableLayoutPanel15.PerformLayout();
            grpVencimiento.ResumeLayout(false);
            tableLayoutPanel17.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidadMeses).EndInit();
            flowLayoutPanel7.ResumeLayout(false);
            flowLayoutPanel7.PerformLayout();
            grpEstado.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel8.ResumeLayout(false);
            tbcBase.ResumeLayout(false);
            tbpInicio.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel13.ResumeLayout(false);
            tableLayoutPanel13.PerformLayout();
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            tableLayoutPanel14.ResumeLayout(false);
            tableLayoutPanel12.ResumeLayout(false);
            tableLayoutPanel12.PerformLayout();
            tbpMovimientos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).EndInit();
            tableLayoutPanel10.ResumeLayout(false);
            tableLayoutPanel9.ResumeLayout(false);
            tableLayoutPanel9.PerformLayout();
            tableLayoutPanel11.ResumeLayout(false);
            tableLayoutPanel11.PerformLayout();
            tbpDnis.ResumeLayout(false);
            tbpDnis.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbxLogo).EndInit();
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

        // Controles de DNIs
        private ListBox lstDnis;
        private TextBox txtNuevoDni;
        private Button btnAgregarDni;
        private Button btnEliminarDni;

        private Label lblCliente;
        private Label lblNombreCliente;

        private GroupBox groupBox1;
        private RadioButton rbVencimientoManual;
        private RadioButton rbVencimientoAutomatico;
        private NumericUpDown nudCantidadMeses;

        private Label lblFechaVencimiento;
        private Label label2;

        private Button btnCargarSaldoCtaCte;
        private Button btnCargarLimite;

        private TabControl tbcBase;
        private TabPage tbpInicio;
        private TabPage tbpMovimientos;
        private TabPage tbpDnis;

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel9;
        private TableLayoutPanel tableLayoutPanel10;
        private TableLayoutPanel tableLayoutPanel11;

        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel3;
        private FlowLayoutPanel flowLayoutPanel5;
        private FlowLayoutPanel flowLayoutPanel6;
        private FlowLayoutPanel flowLayoutPanel7;
        private FlowLayoutPanel flowLayoutPanel8;

        private Label lblEstado;
        private Label lblEstadoTitulo;

        private Label lblFechaCreacion;
        private Label lblFechaCreacionTitulo;

        private Button btnActivar;
        private Button btnCerrarCtacte;

        private Label lblFechaUltimaActivacion;
        private Label lblFechaUltimaActivacionTitulo;

        protected Label lblPagina;
        private Button btnAnterior;
        private Button btnSiguiente;
        protected Label lblTotalRegistros;

        protected DataGridView dgvGrilla;

        private Label lblListadoMovimientos;
        private GroupBox grpLimite;
        private GroupBox grpVencimiento;
        private GroupBox grpEstado;

        private PictureBox pbxLogo;
        private TableLayoutPanel tableLayoutPanel12;
        private Label lblNombreCliente2;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel14;
        private TableLayoutPanel tableLayoutPanel15;
        private TableLayoutPanel tableLayoutPanel13;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel18;
        private TableLayoutPanel tableLayoutPanel17;
        private TableLayoutPanel tableLayoutPanel16;
    }
}