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
            rbVencimientoAutomatico = new RadioButton();
            nudCantidadMeses = new NumericUpDown();
            lblFechaVencimiento = new Label();
            label2 = new Label();
            btnCargarSaldoCtaCte = new Button();
            btnCargarLimite = new Button();
            tbcBase = new TabControl();
            tbpInicio = new TabPage();
            tableLayoutPanel8 = new TableLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            flowLayoutPanel3 = new FlowLayoutPanel();
            lblEstadoTitulo = new Label();
            lblEstado = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnCerrarCtacte = new Button();
            btnActivar = new Button();
            tableLayoutPanel5 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel6 = new TableLayoutPanel();
            flowLayoutPanel5 = new FlowLayoutPanel();
            tableLayoutPanel7 = new TableLayoutPanel();
            flowLayoutPanel6 = new FlowLayoutPanel();
            lblFechaCreacionTitulo = new Label();
            lblFechaCreacion = new Label();
            flowLayoutPanel7 = new FlowLayoutPanel();
            flowLayoutPanel8 = new FlowLayoutPanel();
            lblFechaUltimaActivacionTitulo = new Label();
            lblFechaUltimaActivacion = new Label();
            tableLayoutPanel12 = new TableLayoutPanel();
            lblNombreCliente2 = new Label();
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
            tableLayoutPanel1 = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            pbxLogo = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidadMeses).BeginInit();
            tbcBase.SuspendLayout();
            tbpInicio.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            flowLayoutPanel7.SuspendLayout();
            flowLayoutPanel8.SuspendLayout();
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
            // lblccorriente
            // 
            lblccorriente.Anchor = AnchorStyles.None;
            lblccorriente.AutoSize = true;
            lblccorriente.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblccorriente.ForeColor = Color.FromArgb(31, 26, 43);
            lblccorriente.Location = new Point(0, 6);
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
            lblSaldo.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblSaldo.ForeColor = Color.FromArgb(31, 26, 43);
            lblSaldo.Location = new Point(8, 53);
            lblSaldo.Margin = new Padding(8, 0, 0, 0);
            lblSaldo.Name = "lblSaldo";
            lblSaldo.Size = new Size(115, 19);
            lblSaldo.TabIndex = 1;
            lblSaldo.Text = "Saldo a favor: $0";
            lblSaldo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLimiteDeuda
            // 
            lblLimiteDeuda.AutoSize = true;
            lblLimiteDeuda.Dock = DockStyle.Fill;
            lblLimiteDeuda.Font = new Font("Segoe UI", 10F);
            lblLimiteDeuda.ForeColor = Color.FromArgb(95, 89, 105);
            lblLimiteDeuda.Location = new Point(12, 42);
            lblLimiteDeuda.Margin = new Padding(12, 0, 0, 0);
            lblLimiteDeuda.Name = "lblLimiteDeuda";
            lblLimiteDeuda.Size = new Size(373, 51);
            lblLimiteDeuda.TabIndex = 3;
            lblLimiteDeuda.Text = "Deuda máxima permitida: Deshabilitada";
            lblLimiteDeuda.TextAlign = ContentAlignment.MiddleLeft;
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
            lblFechaVencimientoTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // chkLimiteDeuda
            // 
            chkLimiteDeuda.Anchor = AnchorStyles.Left;
            chkLimiteDeuda.AutoSize = true;
            chkLimiteDeuda.Font = new Font("Segoe UI", 10F);
            chkLimiteDeuda.ForeColor = Color.FromArgb(31, 26, 43);
            chkLimiteDeuda.Location = new Point(0, 9);
            chkLimiteDeuda.Margin = new Padding(0);
            chkLimiteDeuda.Name = "chkLimiteDeuda";
            chkLimiteDeuda.Size = new Size(117, 23);
            chkLimiteDeuda.TabIndex = 7;
            chkLimiteDeuda.Text = "Permitir deuda";
            chkLimiteDeuda.UseVisualStyleBackColor = true;
            chkLimiteDeuda.CheckedChanged += chkLimiteDeuda_CheckedChanged_1;
            // 
            // txtNombreCC
            // 
            txtNombreCC.Anchor = AnchorStyles.None;
            txtNombreCC.BackColor = Color.White;
            txtNombreCC.BorderStyle = BorderStyle.FixedSingle;
            txtNombreCC.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            txtNombreCC.ForeColor = Color.FromArgb(31, 26, 43);
            txtNombreCC.Location = new Point(101, 3);
            txtNombreCC.Margin = new Padding(0);
            txtNombreCC.Name = "txtNombreCC";
            txtNombreCC.Size = new Size(245, 26);
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
            lblDni.Size = new Size(744, 33);
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
            groupBox1.Padding = new Padding(10, 6, 10, 6);
            groupBox1.Size = new Size(385, 57);
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
            // nudCantidadMeses
            // 
            nudCantidadMeses.Anchor = AnchorStyles.Left;
            nudCantidadMeses.BackColor = Color.White;
            nudCantidadMeses.BorderStyle = BorderStyle.FixedSingle;
            nudCantidadMeses.Font = new Font("Segoe UI", 10F);
            nudCantidadMeses.Location = new Point(131, 8);
            nudCantidadMeses.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            nudCantidadMeses.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidadMeses.Name = "nudCantidadMeses";
            nudCantidadMeses.Size = new Size(70, 25);
            nudCantidadMeses.TabIndex = 25;
            nudCantidadMeses.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidadMeses.ValueChanged += nudCantidadMeses_ValueChanged_1;
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
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.ForeColor = Color.FromArgb(95, 89, 105);
            label2.Location = new Point(0, 5);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(128, 19);
            label2.TabIndex = 27;
            label2.Text = "Cantidad de meses:";
            // 
            // btnCargarSaldoCtaCte
            // 
            btnCargarSaldoCtaCte.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            btnCargarSaldoCtaCte.BackColor = Color.White;
            btnCargarSaldoCtaCte.FlatAppearance.BorderColor = Color.FromArgb(67, 20, 135);
            btnCargarSaldoCtaCte.FlatStyle = FlatStyle.Flat;
            btnCargarSaldoCtaCte.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCargarSaldoCtaCte.ForeColor = Color.FromArgb(67, 20, 135);
            btnCargarSaldoCtaCte.Location = new Point(231, 42);
            btnCargarSaldoCtaCte.Margin = new Padding(0);
            btnCargarSaldoCtaCte.Name = "btnCargarSaldoCtaCte";
            btnCargarSaldoCtaCte.Size = new Size(124, 42);
            btnCargarSaldoCtaCte.TabIndex = 32;
            btnCargarSaldoCtaCte.Text = "Cargar saldo";
            btnCargarSaldoCtaCte.UseVisualStyleBackColor = false;
            btnCargarSaldoCtaCte.Click += btnCargarSaldoCtaCte_Click;
            // 
            // btnCargarLimite
            // 
            btnCargarLimite.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            btnCargarLimite.BackColor = Color.White;
            btnCargarLimite.FlatAppearance.BorderColor = Color.FromArgb(67, 20, 135);
            btnCargarLimite.FlatStyle = FlatStyle.Flat;
            btnCargarLimite.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCargarLimite.ForeColor = Color.FromArgb(67, 20, 135);
            btnCargarLimite.Location = new Point(236, 0);
            btnCargarLimite.Margin = new Padding(0);
            btnCargarLimite.Name = "btnCargarLimite";
            btnCargarLimite.Size = new Size(124, 42);
            btnCargarLimite.TabIndex = 33;
            btnCargarLimite.Text = "Cargar límite";
            btnCargarLimite.UseVisualStyleBackColor = false;
            btnCargarLimite.Click += btnCargarLimite_Click;
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
            tbcBase.Size = new Size(784, 379);
            tbcBase.TabIndex = 34;
            // 
            // tbpInicio
            // 
            tbpInicio.Controls.Add(tableLayoutPanel8);
            tbpInicio.Font = new Font("Segoe UI", 10F);
            tbpInicio.Location = new Point(4, 31);
            tbpInicio.Name = "tbpInicio";
            tbpInicio.Padding = new Padding(3);
            tbpInicio.Size = new Size(776, 344);
            tbpInicio.TabIndex = 0;
            tbpInicio.Text = "Configuración";
            tbpInicio.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.ColumnCount = 2;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.Controls.Add(flowLayoutPanel2, 0, 1);
            tableLayoutPanel8.Controls.Add(tableLayoutPanel3, 1, 1);
            tableLayoutPanel8.Controls.Add(tableLayoutPanel5, 0, 2);
            tableLayoutPanel8.Controls.Add(tableLayoutPanel6, 1, 2);
            tableLayoutPanel8.Controls.Add(tableLayoutPanel7, 1, 3);
            tableLayoutPanel8.Controls.Add(tableLayoutPanel12, 0, 3);
            tableLayoutPanel8.Controls.Add(lblNombreCliente2, 0, 0);
            tableLayoutPanel8.Dock = DockStyle.Fill;
            tableLayoutPanel8.Location = new Point(3, 3);
            tableLayoutPanel8.Margin = new Padding(0);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 4;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 102F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 93F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 66F));
            tableLayoutPanel8.Size = new Size(770, 338);
            tableLayoutPanel8.TabIndex = 57;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Anchor = AnchorStyles.None;
            flowLayoutPanel2.Controls.Add(lblccorriente);
            flowLayoutPanel2.Controls.Add(txtNombreCC);
            flowLayoutPanel2.Location = new Point(0, 56);
            flowLayoutPanel2.Margin = new Padding(0);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Padding = new Padding(0, 3, 0, 0);
            flowLayoutPanel2.Size = new Size(385, 57);
            flowLayoutPanel2.TabIndex = 52;
            flowLayoutPanel2.WrapContents = false;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62.07792F));
            tableLayoutPanel3.Controls.Add(flowLayoutPanel3, 0, 1);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(385, 34);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel3.Size = new Size(385, 102);
            tableLayoutPanel3.TabIndex = 53;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Anchor = AnchorStyles.None;
            flowLayoutPanel3.Controls.Add(lblEstadoTitulo);
            flowLayoutPanel3.Controls.Add(lblEstado);
            flowLayoutPanel3.Location = new Point(73, 63);
            flowLayoutPanel3.Margin = new Padding(0);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Padding = new Padding(0, 3, 0, 0);
            flowLayoutPanel3.Size = new Size(238, 30);
            flowLayoutPanel3.TabIndex = 52;
            flowLayoutPanel3.WrapContents = false;
            // 
            // lblEstadoTitulo
            // 
            lblEstadoTitulo.Anchor = AnchorStyles.Left;
            lblEstadoTitulo.AutoSize = true;
            lblEstadoTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEstadoTitulo.ForeColor = Color.FromArgb(31, 26, 43);
            lblEstadoTitulo.Location = new Point(0, 3);
            lblEstadoTitulo.Margin = new Padding(0);
            lblEstadoTitulo.Name = "lblEstadoTitulo";
            lblEstadoTitulo.Size = new Size(57, 19);
            lblEstadoTitulo.TabIndex = 44;
            lblEstadoTitulo.Tag = "NoModificarConBase";
            lblEstadoTitulo.Text = "Estado:";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblEstado.ForeColor = Color.FromArgb(67, 20, 135);
            lblEstado.Location = new Point(65, 3);
            lblEstado.Margin = new Padding(8, 0, 0, 0);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(69, 19);
            lblEstado.TabIndex = 45;
            lblEstado.Tag = "NoModificarConBase";
            lblEstado.Text = "**********";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(btnActivar, 1, 0);
            tableLayoutPanel2.Controls.Add(btnCerrarCtacte, 0, 0);
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(379, 42);
            tableLayoutPanel2.TabIndex = 53;
            // 
            // btnCerrarCtacte
            // 
            btnCerrarCtacte.Anchor = AnchorStyles.None;
            btnCerrarCtacte.BackColor = Color.FromArgb(245, 242, 248);
            btnCerrarCtacte.FlatAppearance.BorderColor = Color.FromArgb(150, 140, 160);
            btnCerrarCtacte.FlatStyle = FlatStyle.Flat;
            btnCerrarCtacte.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCerrarCtacte.ForeColor = Color.FromArgb(31, 26, 43);
            btnCerrarCtacte.Location = new Point(30, 4);
            btnCerrarCtacte.Margin = new Padding(0);
            btnCerrarCtacte.Name = "btnCerrarCtacte";
            btnCerrarCtacte.Size = new Size(128, 34);
            btnCerrarCtacte.TabIndex = 48;
            btnCerrarCtacte.Text = "Cerrar cuenta";
            btnCerrarCtacte.UseVisualStyleBackColor = false;
            btnCerrarCtacte.Click += btnCerrarCtacte_Click;
            // 
            // btnActivar
            // 
            btnActivar.Anchor = AnchorStyles.None;
            btnActivar.BackColor = Color.FromArgb(67, 20, 135);
            btnActivar.FlatAppearance.BorderSize = 0;
            btnActivar.FlatStyle = FlatStyle.Flat;
            btnActivar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnActivar.ForeColor = Color.White;
            btnActivar.Location = new Point(225, 5);
            btnActivar.Margin = new Padding(0);
            btnActivar.Name = "btnActivar";
            btnActivar.Size = new Size(118, 31);
            btnActivar.TabIndex = 49;
            btnActivar.Text = "Activar";
            btnActivar.UseVisualStyleBackColor = false;
            btnActivar.Click += btnActivar_Click;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(tableLayoutPanel4, 0, 0);
            tableLayoutPanel5.Controls.Add(lblLimiteDeuda, 0, 1);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(0, 136);
            tableLayoutPanel5.Margin = new Padding(0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(385, 93);
            tableLayoutPanel5.TabIndex = 53;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel4.Controls.Add(chkLimiteDeuda, 0, 0);
            tableLayoutPanel4.Controls.Add(btnCargarLimite, 1, 0);
            tableLayoutPanel4.Location = new Point(0, 0);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tableLayoutPanel4.Size = new Size(385, 42);
            tableLayoutPanel4.TabIndex = 52;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel6.Controls.Add(flowLayoutPanel5, 0, 1);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(385, 136);
            tableLayoutPanel6.Margin = new Padding(0);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 57F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 37F));
            tableLayoutPanel6.Size = new Size(385, 93);
            tableLayoutPanel6.TabIndex = 52;
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.Controls.Add(label2);
            flowLayoutPanel5.Controls.Add(nudCantidadMeses);
            flowLayoutPanel5.Dock = DockStyle.Fill;
            flowLayoutPanel5.Location = new Point(0, 57);
            flowLayoutPanel5.Margin = new Padding(0);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Padding = new Padding(0, 5, 0, 0);
            flowLayoutPanel5.Size = new Size(385, 37);
            flowLayoutPanel5.TabIndex = 53;
            flowLayoutPanel5.WrapContents = false;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.ColumnCount = 1;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Controls.Add(flowLayoutPanel6, 0, 0);
            tableLayoutPanel7.Controls.Add(flowLayoutPanel7, 0, 1);
            tableLayoutPanel7.Controls.Add(flowLayoutPanel8, 0, 2);
            tableLayoutPanel7.Dock = DockStyle.Fill;
            tableLayoutPanel7.Location = new Point(385, 229);
            tableLayoutPanel7.Margin = new Padding(0);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 3;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tableLayoutPanel7.Size = new Size(385, 109);
            tableLayoutPanel7.TabIndex = 56;
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.Controls.Add(lblFechaCreacionTitulo);
            flowLayoutPanel6.Controls.Add(lblFechaCreacion);
            flowLayoutPanel6.Dock = DockStyle.Fill;
            flowLayoutPanel6.Location = new Point(0, 0);
            flowLayoutPanel6.Margin = new Padding(0);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Padding = new Padding(0, 4, 0, 0);
            flowLayoutPanel6.Size = new Size(385, 29);
            flowLayoutPanel6.TabIndex = 53;
            flowLayoutPanel6.WrapContents = false;
            // 
            // lblFechaCreacionTitulo
            // 
            lblFechaCreacionTitulo.AutoSize = true;
            lblFechaCreacionTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFechaCreacionTitulo.ForeColor = Color.FromArgb(31, 26, 43);
            lblFechaCreacionTitulo.Location = new Point(0, 4);
            lblFechaCreacionTitulo.Margin = new Padding(0);
            lblFechaCreacionTitulo.Name = "lblFechaCreacionTitulo";
            lblFechaCreacionTitulo.Size = new Size(112, 19);
            lblFechaCreacionTitulo.TabIndex = 46;
            lblFechaCreacionTitulo.Tag = "NoModificarConBase";
            lblFechaCreacionTitulo.Text = "Fecha creación:";
            // 
            // lblFechaCreacion
            // 
            lblFechaCreacion.AutoSize = true;
            lblFechaCreacion.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblFechaCreacion.ForeColor = Color.FromArgb(67, 20, 135);
            lblFechaCreacion.Location = new Point(120, 4);
            lblFechaCreacion.Margin = new Padding(8, 0, 0, 0);
            lblFechaCreacion.Name = "lblFechaCreacion";
            lblFechaCreacion.Size = new Size(83, 19);
            lblFechaCreacion.TabIndex = 47;
            lblFechaCreacion.Tag = "NoModificarConBase";
            lblFechaCreacion.Text = "01/08/2026";
            // 
            // flowLayoutPanel7
            // 
            flowLayoutPanel7.Controls.Add(lblFechaVencimientoTitulo);
            flowLayoutPanel7.Controls.Add(lblFechaVencimiento);
            flowLayoutPanel7.Dock = DockStyle.Fill;
            flowLayoutPanel7.Location = new Point(0, 29);
            flowLayoutPanel7.Margin = new Padding(0);
            flowLayoutPanel7.Name = "flowLayoutPanel7";
            flowLayoutPanel7.Padding = new Padding(0, 4, 0, 0);
            flowLayoutPanel7.Size = new Size(385, 29);
            flowLayoutPanel7.TabIndex = 54;
            flowLayoutPanel7.WrapContents = false;
            // 
            // flowLayoutPanel8
            // 
            flowLayoutPanel8.Controls.Add(lblFechaUltimaActivacionTitulo);
            flowLayoutPanel8.Controls.Add(lblFechaUltimaActivacion);
            flowLayoutPanel8.Dock = DockStyle.Fill;
            flowLayoutPanel8.Location = new Point(0, 58);
            flowLayoutPanel8.Margin = new Padding(0);
            flowLayoutPanel8.Name = "flowLayoutPanel8";
            flowLayoutPanel8.Padding = new Padding(0, 4, 0, 0);
            flowLayoutPanel8.Size = new Size(385, 51);
            flowLayoutPanel8.TabIndex = 55;
            flowLayoutPanel8.WrapContents = false;
            // 
            // lblFechaUltimaActivacionTitulo
            // 
            lblFechaUltimaActivacionTitulo.AutoSize = true;
            lblFechaUltimaActivacionTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFechaUltimaActivacionTitulo.ForeColor = Color.FromArgb(31, 26, 43);
            lblFechaUltimaActivacionTitulo.Location = new Point(0, 4);
            lblFechaUltimaActivacionTitulo.Margin = new Padding(0);
            lblFechaUltimaActivacionTitulo.Name = "lblFechaUltimaActivacionTitulo";
            lblFechaUltimaActivacionTitulo.Size = new Size(129, 19);
            lblFechaUltimaActivacionTitulo.TabIndex = 50;
            lblFechaUltimaActivacionTitulo.Tag = "NoModificarConBase";
            lblFechaUltimaActivacionTitulo.Text = "Última activación:";
            // 
            // lblFechaUltimaActivacion
            // 
            lblFechaUltimaActivacion.AutoSize = true;
            lblFechaUltimaActivacion.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblFechaUltimaActivacion.ForeColor = Color.FromArgb(67, 20, 135);
            lblFechaUltimaActivacion.Location = new Point(137, 4);
            lblFechaUltimaActivacion.Margin = new Padding(8, 0, 0, 0);
            lblFechaUltimaActivacion.Name = "lblFechaUltimaActivacion";
            lblFechaUltimaActivacion.Size = new Size(83, 19);
            lblFechaUltimaActivacion.TabIndex = 51;
            lblFechaUltimaActivacion.Tag = "NoModificarConBase";
            lblFechaUltimaActivacion.Text = "01/08/2026";
            // 
            // tableLayoutPanel12
            // 
            tableLayoutPanel12.ColumnCount = 2;
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54.6174126F));
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45.3825874F));
            tableLayoutPanel12.Controls.Add(btnCargarSaldoCtaCte, 1, 1);
            tableLayoutPanel12.Controls.Add(lblSaldo, 0, 1);
            tableLayoutPanel12.Location = new Point(3, 232);
            tableLayoutPanel12.Name = "tableLayoutPanel12";
            tableLayoutPanel12.RowCount = 2;
            tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel12.Size = new Size(379, 84);
            tableLayoutPanel12.TabIndex = 57;
            // 
            // lblNombreCliente2
            // 
            lblNombreCliente2.AutoSize = true;
            lblNombreCliente2.Location = new Point(3, 0);
            lblNombreCliente2.Name = "lblNombreCliente2";
            lblNombreCliente2.Size = new Size(45, 19);
            lblNombreCliente2.TabIndex = 58;
            lblNombreCliente2.Text = "label1";
            // 
            // tbpMovimientos
            // 
            tbpMovimientos.Controls.Add(dgvGrilla);
            tbpMovimientos.Controls.Add(tableLayoutPanel10);
            tbpMovimientos.Controls.Add(lblListadoMovimientos);
            tbpMovimientos.Location = new Point(4, 31);
            tbpMovimientos.Name = "tbpMovimientos";
            tbpMovimientos.Padding = new Padding(10);
            tbpMovimientos.Size = new Size(776, 344);
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
            dgvGrilla.Size = new Size(756, 244);
            dgvGrilla.TabIndex = 35;
            dgvGrilla.MouseDown += dgvGrilla_MouseDown;
            // 
            // tableLayoutPanel10
            // 
            tableLayoutPanel10.ColumnCount = 1;
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel10.Controls.Add(tableLayoutPanel9, 0, 0);
            tableLayoutPanel10.Dock = DockStyle.Bottom;
            tableLayoutPanel10.Location = new Point(10, 288);
            tableLayoutPanel10.Margin = new Padding(0);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.Padding = new Padding(0, 6, 0, 0);
            tableLayoutPanel10.RowCount = 1;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel10.Size = new Size(756, 46);
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
            tableLayoutPanel9.Size = new Size(756, 40);
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
            tableLayoutPanel11.Location = new Point(302, 0);
            tableLayoutPanel11.Margin = new Padding(0);
            tableLayoutPanel11.Name = "tableLayoutPanel11";
            tableLayoutPanel11.RowCount = 1;
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel11.Size = new Size(454, 40);
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
            btnAnterior.Location = new Point(52, 6);
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
            lblPagina.Location = new Point(195, 11);
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
            btnSiguiente.Location = new Point(356, 6);
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
            lblListadoMovimientos.Size = new Size(756, 34);
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
            tbpDnis.Size = new Size(776, 344);
            tbpDnis.TabIndex = 2;
            tbpDnis.Text = "DNI habilitados";
            tbpDnis.UseVisualStyleBackColor = true;
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
            ClientSize = new Size(784, 432);
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
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel8.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            flowLayoutPanel7.ResumeLayout(false);
            flowLayoutPanel7.PerformLayout();
            flowLayoutPanel8.ResumeLayout(false);
            flowLayoutPanel8.PerformLayout();
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
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel7;
        private TableLayoutPanel tableLayoutPanel8;
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

        private PictureBox pbxLogo;
        private TableLayoutPanel tableLayoutPanel12;
        private Label lblNombreCliente2;
        private TableLayoutPanel tableLayoutPanel2;
    }
}