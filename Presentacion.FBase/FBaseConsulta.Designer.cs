namespace Presentacion.FBase
{
    partial class FBaseConsulta
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
            dgvGrilla = new DataGridView();
            BarraLateralBotones = new ToolStrip();
            btnNuevo = new ToolStripButton();
            btnEliminar = new ToolStripButton();
            btnModificar = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnActualizar = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnImprimir = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            btnSalir = new ToolStripButton();
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel9 = new TableLayoutPanel();
            tableLayoutPanel13 = new TableLayoutPanel();
            chkBool2 = new CheckBox();
            chkBool1 = new CheckBox();
            tableLayoutPanel10 = new TableLayoutPanel();
            tableLayoutPanel11 = new TableLayoutPanel();
            btnSiguiente = new Button();
            lblPagina = new Label();
            btnAnterior = new Button();
            lblTotalRegistros = new Label();
            tableLayoutPanel6 = new TableLayoutPanel();
            tableLayoutPanel12 = new TableLayoutPanel();
            lblcbx3 = new Label();
            cbx3 = new ComboBox();
            tableLayoutPanel7 = new TableLayoutPanel();
            lblcbx2 = new Label();
            cbx2 = new ComboBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblcbx1 = new Label();
            cbx1 = new ComboBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel8 = new TableLayoutPanel();
            chkUsarRango = new CheckBox();
            chkUsarFecha = new CheckBox();
            dtpHasta = new DateTimePicker();
            dtpDesde = new DateTimePicker();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel14 = new TableLayoutPanel();
            btnLimpiar = new Button();
            btnBuscar = new Button();
            tableLayoutPanel5 = new TableLayoutPanel();
            txtBuscar = new TextBox();
            lblBuscar = new Label();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).BeginInit();
            BarraLateralBotones.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel9.SuspendLayout();
            tableLayoutPanel13.SuspendLayout();
            tableLayoutPanel10.SuspendLayout();
            tableLayoutPanel11.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel12.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel14.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            SuspendLayout();
            // 
            // dgvGrilla
            // 
            dgvGrilla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvGrilla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvGrilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ActiveCaptionText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvGrilla.DefaultCellStyle = dataGridViewCellStyle2;
            dgvGrilla.Location = new Point(12, 302);
            dgvGrilla.Name = "dgvGrilla";
            dgvGrilla.Size = new Size(1427, 247);
            dgvGrilla.TabIndex = 2;
            // 
            // BarraLateralBotones
            // 
            BarraLateralBotones.BackColor = SystemColors.AppWorkspace;
            BarraLateralBotones.CanOverflow = false;
            BarraLateralBotones.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BarraLateralBotones.Items.AddRange(new ToolStripItem[] { btnNuevo, btnEliminar, btnModificar, toolStripSeparator1, btnActualizar, toolStripSeparator2, btnImprimir, toolStripSeparator3, btnSalir });
            BarraLateralBotones.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            BarraLateralBotones.Location = new Point(0, 0);
            BarraLateralBotones.Name = "BarraLateralBotones";
            BarraLateralBotones.Size = new Size(1451, 27);
            BarraLateralBotones.Stretch = true;
            BarraLateralBotones.TabIndex = 0;
            BarraLateralBotones.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(59, 24);
            btnNuevo.Text = "Nuevo";
            btnNuevo.TextImageRelation = TextImageRelation.TextAboveImage;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(70, 24);
            btnEliminar.Text = "Eliminar";
            btnEliminar.TextImageRelation = TextImageRelation.TextAboveImage;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(80, 24);
            btnModificar.Text = "Modificar";
            btnModificar.TextImageRelation = TextImageRelation.TextAboveImage;
            btnModificar.Click += btnModificar_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 27);
            // 
            // btnActualizar
            // 
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(83, 24);
            btnActualizar.Text = "Actualizar";
            btnActualizar.TextImageRelation = TextImageRelation.TextAboveImage;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 27);
            // 
            // btnImprimir
            // 
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(75, 24);
            btnImprimir.Text = "Imprimir";
            btnImprimir.TextImageRelation = TextImageRelation.TextAboveImage;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 27);
            // 
            // btnSalir
            // 
            btnSalir.Alignment = ToolStripItemAlignment.Right;
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(43, 24);
            btnSalir.Text = "Salir";
            btnSalir.TextImageRelation = TextImageRelation.TextAboveImage;
            btnSalir.Click += btnSalir_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Controls.Add(BarraLateralBotones);
            panel1.Controls.Add(dgvGrilla);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1451, 561);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel9, 1, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel6, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 0, 1);
            tableLayoutPanel1.Location = new Point(12, 47);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.Size = new Size(1424, 228);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // tableLayoutPanel9
            // 
            tableLayoutPanel9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel9.ColumnCount = 1;
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel9.Controls.Add(tableLayoutPanel13, 0, 0);
            tableLayoutPanel9.Controls.Add(tableLayoutPanel10, 0, 1);
            tableLayoutPanel9.Location = new Point(715, 139);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.RowCount = 2;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel9.Size = new Size(706, 86);
            tableLayoutPanel9.TabIndex = 7;
            // 
            // tableLayoutPanel13
            // 
            tableLayoutPanel13.ColumnCount = 2;
            tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel13.Controls.Add(chkBool2, 1, 0);
            tableLayoutPanel13.Controls.Add(chkBool1, 0, 0);
            tableLayoutPanel13.Location = new Point(3, 3);
            tableLayoutPanel13.Name = "tableLayoutPanel13";
            tableLayoutPanel13.RowCount = 1;
            tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel13.Size = new Size(700, 37);
            tableLayoutPanel13.TabIndex = 8;
            // 
            // chkBool2
            // 
            chkBool2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            chkBool2.AutoSize = true;
            chkBool2.Enabled = false;
            chkBool2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkBool2.Location = new Point(353, 3);
            chkBool2.Name = "chkBool2";
            chkBool2.Size = new Size(80, 31);
            chkBool2.TabIndex = 4;
            chkBool2.Text = "cbxBool2";
            chkBool2.TextAlign = ContentAlignment.MiddleCenter;
            chkBool2.UseVisualStyleBackColor = true;
            chkBool2.CheckedChanged += chkBool2_CheckedChanged;
            // 
            // chkBool1
            // 
            chkBool1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            chkBool1.AutoSize = true;
            chkBool1.Enabled = false;
            chkBool1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkBool1.Location = new Point(3, 3);
            chkBool1.Name = "chkBool1";
            chkBool1.Size = new Size(80, 31);
            chkBool1.TabIndex = 3;
            chkBool1.Text = "cbxBool1";
            chkBool1.TextAlign = ContentAlignment.MiddleCenter;
            chkBool1.UseVisualStyleBackColor = true;
            chkBool1.CheckedChanged += chkBool1_CheckedChanged;
            // 
            // tableLayoutPanel10
            // 
            tableLayoutPanel10.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel10.ColumnCount = 2;
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel10.Controls.Add(tableLayoutPanel11, 1, 0);
            tableLayoutPanel10.Controls.Add(lblTotalRegistros, 0, 0);
            tableLayoutPanel10.Location = new Point(3, 46);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.RowCount = 1;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel10.Size = new Size(700, 37);
            tableLayoutPanel10.TabIndex = 7;
            // 
            // tableLayoutPanel11
            // 
            tableLayoutPanel11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel11.ColumnCount = 3;
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel11.Controls.Add(btnSiguiente, 2, 0);
            tableLayoutPanel11.Controls.Add(lblPagina, 1, 0);
            tableLayoutPanel11.Controls.Add(btnAnterior, 0, 0);
            tableLayoutPanel11.Location = new Point(283, 3);
            tableLayoutPanel11.Name = "tableLayoutPanel11";
            tableLayoutPanel11.RowCount = 1;
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel11.Size = new Size(414, 31);
            tableLayoutPanel11.TabIndex = 7;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnSiguiente.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSiguiente.Location = new Point(313, 3);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(98, 25);
            btnSiguiente.TabIndex = 7;
            btnSiguiente.Text = ">";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // lblPagina
            // 
            lblPagina.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblPagina.AutoSize = true;
            lblPagina.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPagina.Location = new Point(106, 0);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(201, 31);
            lblPagina.TabIndex = 1;
            lblPagina.Text = "label2";
            lblPagina.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnAnterior
            // 
            btnAnterior.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnAnterior.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAnterior.Location = new Point(3, 3);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(97, 25);
            btnAnterior.TabIndex = 2;
            btnAnterior.Text = "<";
            btnAnterior.UseVisualStyleBackColor = true;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // lblTotalRegistros
            // 
            lblTotalRegistros.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblTotalRegistros.AutoSize = true;
            lblTotalRegistros.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalRegistros.Location = new Point(3, 0);
            lblTotalRegistros.Name = "lblTotalRegistros";
            lblTotalRegistros.Size = new Size(274, 37);
            lblTotalRegistros.TabIndex = 0;
            lblTotalRegistros.Text = "label1";
            lblTotalRegistros.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(tableLayoutPanel12, 0, 2);
            tableLayoutPanel6.Controls.Add(tableLayoutPanel7, 0, 1);
            tableLayoutPanel6.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel6.Location = new Point(3, 3);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 3;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel6.Size = new Size(706, 130);
            tableLayoutPanel6.TabIndex = 7;
            // 
            // tableLayoutPanel12
            // 
            tableLayoutPanel12.ColumnCount = 2;
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
            tableLayoutPanel12.Controls.Add(lblcbx3, 0, 0);
            tableLayoutPanel12.Controls.Add(cbx3, 1, 0);
            tableLayoutPanel12.Location = new Point(3, 89);
            tableLayoutPanel12.Name = "tableLayoutPanel12";
            tableLayoutPanel12.RowCount = 1;
            tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel12.Size = new Size(700, 26);
            tableLayoutPanel12.TabIndex = 8;
            // 
            // lblcbx3
            // 
            lblcbx3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lblcbx3.AutoSize = true;
            lblcbx3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblcbx3.Location = new Point(3, 0);
            lblcbx3.Name = "lblcbx3";
            lblcbx3.Size = new Size(46, 26);
            lblcbx3.TabIndex = 9;
            lblcbx3.Text = "cbx3";
            // 
            // cbx3
            // 
            cbx3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbx3.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx3.Enabled = false;
            cbx3.FormattingEnabled = true;
            cbx3.Location = new Point(236, 3);
            cbx3.Name = "cbx3";
            cbx3.Size = new Size(461, 23);
            cbx3.TabIndex = 5;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.ColumnCount = 2;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
            tableLayoutPanel7.Controls.Add(lblcbx2, 0, 0);
            tableLayoutPanel7.Controls.Add(cbx2, 1, 0);
            tableLayoutPanel7.Location = new Point(3, 46);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 1;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Size = new Size(700, 26);
            tableLayoutPanel7.TabIndex = 7;
            // 
            // lblcbx2
            // 
            lblcbx2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lblcbx2.AutoSize = true;
            lblcbx2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblcbx2.Location = new Point(3, 0);
            lblcbx2.Name = "lblcbx2";
            lblcbx2.Size = new Size(46, 26);
            lblcbx2.TabIndex = 9;
            lblcbx2.Text = "cbx2";
            // 
            // cbx2
            // 
            cbx2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbx2.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx2.Enabled = false;
            cbx2.FormattingEnabled = true;
            cbx2.Location = new Point(236, 3);
            cbx2.Name = "cbx2";
            cbx2.Size = new Size(461, 23);
            cbx2.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
            tableLayoutPanel2.Controls.Add(lblcbx1, 0, 0);
            tableLayoutPanel2.Controls.Add(cbx1, 1, 0);
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(700, 26);
            tableLayoutPanel2.TabIndex = 7;
            // 
            // lblcbx1
            // 
            lblcbx1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lblcbx1.AutoSize = true;
            lblcbx1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblcbx1.Location = new Point(3, 0);
            lblcbx1.Name = "lblcbx1";
            lblcbx1.Size = new Size(46, 26);
            lblcbx1.TabIndex = 8;
            lblcbx1.Text = "cbx1";
            // 
            // cbx1
            // 
            cbx1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbx1.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx1.Enabled = false;
            cbx1.FormattingEnabled = true;
            cbx1.Location = new Point(236, 3);
            cbx1.Name = "cbx1";
            cbx1.Size = new Size(461, 23);
            cbx1.TabIndex = 0;
            cbx1.SelectedIndexChanged += cbxFiltroOpcional_SelectedIndexChanged;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(tableLayoutPanel8, 0, 0);
            tableLayoutPanel3.Controls.Add(dtpHasta, 0, 2);
            tableLayoutPanel3.Controls.Add(dtpDesde, 0, 1);
            tableLayoutPanel3.Location = new Point(715, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 3;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.Size = new Size(706, 130);
            tableLayoutPanel3.TabIndex = 7;
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.ColumnCount = 2;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.Controls.Add(chkUsarRango, 1, 0);
            tableLayoutPanel8.Controls.Add(chkUsarFecha, 0, 0);
            tableLayoutPanel8.Location = new Point(3, 3);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 1;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel8.Size = new Size(700, 24);
            tableLayoutPanel8.TabIndex = 7;
            // 
            // chkUsarRango
            // 
            chkUsarRango.AutoSize = true;
            chkUsarRango.Enabled = false;
            chkUsarRango.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkUsarRango.Location = new Point(353, 3);
            chkUsarRango.Name = "chkUsarRango";
            chkUsarRango.Size = new Size(96, 18);
            chkUsarRango.TabIndex = 4;
            chkUsarRango.Text = "Usar Rango";
            chkUsarRango.TextAlign = ContentAlignment.TopLeft;
            chkUsarRango.UseVisualStyleBackColor = true;
            chkUsarRango.CheckedChanged += chkUsarRango_CheckedChanged;
            // 
            // chkUsarFecha
            // 
            chkUsarFecha.AutoSize = true;
            chkUsarFecha.Enabled = false;
            chkUsarFecha.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkUsarFecha.Location = new Point(3, 3);
            chkUsarFecha.Name = "chkUsarFecha";
            chkUsarFecha.Size = new Size(91, 18);
            chkUsarFecha.TabIndex = 3;
            chkUsarFecha.Text = "Usar Fecha";
            chkUsarFecha.TextAlign = ContentAlignment.TopLeft;
            chkUsarFecha.UseVisualStyleBackColor = true;
            chkUsarFecha.CheckedChanged += chkUsarFecha_CheckedChanged;
            // 
            // dtpHasta
            // 
            dtpHasta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtpHasta.Enabled = false;
            dtpHasta.Location = new Point(3, 89);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(700, 23);
            dtpHasta.TabIndex = 2;
            // 
            // dtpDesde
            // 
            dtpDesde.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtpDesde.Enabled = false;
            dtpDesde.Location = new Point(3, 46);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(700, 23);
            dtpDesde.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58.82353F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 41.17647F));
            tableLayoutPanel4.Controls.Add(tableLayoutPanel14, 1, 0);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 0, 0);
            tableLayoutPanel4.Location = new Point(3, 139);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(704, 74);
            tableLayoutPanel4.TabIndex = 7;
            // 
            // tableLayoutPanel14
            // 
            tableLayoutPanel14.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel14.ColumnCount = 1;
            tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel14.Controls.Add(btnLimpiar, 0, 1);
            tableLayoutPanel14.Controls.Add(btnBuscar, 0, 0);
            tableLayoutPanel14.Location = new Point(417, 3);
            tableLayoutPanel14.Name = "tableLayoutPanel14";
            tableLayoutPanel14.RowCount = 2;
            tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel14.Size = new Size(284, 68);
            tableLayoutPanel14.TabIndex = 8;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnLimpiar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiar.Location = new Point(3, 37);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(278, 28);
            btnLimpiar.TabIndex = 2;
            btnLimpiar.Text = "Limpiar Filtros";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnBuscar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBuscar.Location = new Point(3, 3);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(278, 28);
            btnBuscar.TabIndex = 1;
            btnBuscar.Text = "Aplicar Filtros";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Controls.Add(txtBuscar, 0, 1);
            tableLayoutPanel5.Controls.Add(lblBuscar, 0, 0);
            tableLayoutPanel5.Location = new Point(3, 3);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(408, 68);
            tableLayoutPanel5.TabIndex = 2;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscar.Location = new Point(3, 37);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(402, 29);
            txtBuscar.TabIndex = 0;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBuscar.ImageAlign = ContentAlignment.BottomCenter;
            lblBuscar.Location = new Point(3, 0);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(68, 21);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar :";
            lblBuscar.TextAlign = ContentAlignment.BottomLeft;
            // 
            // FBaseConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new Size(1451, 561);
            Controls.Add(panel1);
            Name = "FBaseConsulta";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FBaseConsulta";
            WindowState = FormWindowState.Maximized;
            Load += FBaseConsulta_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).EndInit();
            BarraLateralBotones.ResumeLayout(false);
            BarraLateralBotones.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel9.ResumeLayout(false);
            tableLayoutPanel13.ResumeLayout(false);
            tableLayoutPanel13.PerformLayout();
            tableLayoutPanel10.ResumeLayout(false);
            tableLayoutPanel10.PerformLayout();
            tableLayoutPanel11.ResumeLayout(false);
            tableLayoutPanel11.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel12.ResumeLayout(false);
            tableLayoutPanel12.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel8.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel14.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        protected DataGridView dgvGrilla;
        protected ToolStrip BarraLateralBotones;
        protected ToolStripButton btnNuevo;
        protected ToolStripButton btnEliminar;
        protected ToolStripButton btnModificar;
        protected ToolStripSeparator toolStripSeparator1;
        protected ToolStripButton btnActualizar;
        protected ToolStripSeparator toolStripSeparator2;
        protected ToolStripButton btnImprimir;
        protected ToolStripSeparator toolStripSeparator3;
        protected ToolStripButton btnSalir;
        public Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel9;
        private TableLayoutPanel tableLayoutPanel10;
        private TableLayoutPanel tableLayoutPanel11;
        private Button btnSiguiente;
        protected Label lblPagina;
        private Button btnAnterior;
        protected Label lblTotalRegistros;
        protected CheckBox chkBool1;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel7;
        protected Label lblcbx2;
        protected ComboBox cbxFiltroExtraEstado;
        private TableLayoutPanel tableLayoutPanel2;
        protected Label lblcbx1;
        protected ComboBox cbx1;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel8;
        public CheckBox chkUsarRango;
        public CheckBox chkUsarFecha;
        protected DateTimePicker dtpHasta;
        protected DateTimePicker dtpDesde;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel5;
        protected TextBox txtBuscar;
        private Label lblBuscar;
        protected Button btnBuscar;
        private TableLayoutPanel tableLayoutPanel13;
        protected CheckBox chkBool2;
        private TableLayoutPanel tableLayoutPanel12;
        protected Label lblcbx3;
        protected ComboBox comboBox1;
        protected ComboBox cbx3;
        protected ComboBox cbx2;
        private TableLayoutPanel tableLayoutPanel14;
        protected Button btnLimpiar;
    }
}