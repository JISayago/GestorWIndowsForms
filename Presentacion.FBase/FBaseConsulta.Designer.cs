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
            txtBuscar = new TextBox();
            btnBuscar = new Button();
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
            cbxEstaEliminado = new CheckBox();
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel6 = new TableLayoutPanel();
            tableLayoutPanel7 = new TableLayoutPanel();
            lblCoilumnaPropiedad = new Label();
            cbxFiltroExtraEstado = new ComboBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblFiltrarPorColumna = new Label();
            cbxFiltroOpcional = new ComboBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            dtpHasta = new DateTimePicker();
            dtpDesde = new DateTimePicker();
            chkUsarFecha = new CheckBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel5 = new TableLayoutPanel();
            lblBuscar = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).BeginInit();
            BarraLateralBotones.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            SuspendLayout();
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.Location = new Point(3, 32);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(302, 23);
            txtBuscar.TabIndex = 0;
            // 
            // btnBuscar
            // 
            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnBuscar.Location = new Point(317, 3);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(214, 58);
            btnBuscar.TabIndex = 1;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click_1;
            // 
            // dgvGrilla
            // 
            dgvGrilla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvGrilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGrilla.Location = new Point(12, 212);
            dgvGrilla.Name = "dgvGrilla";
            dgvGrilla.Size = new Size(1094, 337);
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
            BarraLateralBotones.Size = new Size(1118, 27);
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
            // cbxEstaEliminado
            // 
            cbxEstaEliminado.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            cbxEstaEliminado.AutoSize = true;
            cbxEstaEliminado.Location = new Point(543, 3);
            cbxEstaEliminado.Name = "cbxEstaEliminado";
            cbxEstaEliminado.Size = new Size(211, 64);
            cbxEstaEliminado.TabIndex = 3;
            cbxEstaEliminado.Text = "Mostrar sólo elementos eliminados";
            cbxEstaEliminado.TextAlign = ContentAlignment.MiddleCenter;
            cbxEstaEliminado.UseVisualStyleBackColor = true;
            cbxEstaEliminado.CheckedChanged += cbxEstaEliminado_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Controls.Add(BarraLateralBotones);
            panel1.Controls.Add(dgvGrilla);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1118, 561);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel6, 1, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 1);
            tableLayoutPanel1.Controls.Add(cbxEstaEliminado, 2, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 0, 0);
            tableLayoutPanel1.Location = new Point(12, 58);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 47.5609741F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 52.4390259F));
            tableLayoutPanel1.Size = new Size(1080, 148);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(tableLayoutPanel7, 0, 1);
            tableLayoutPanel6.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel6.Location = new Point(543, 73);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Size = new Size(534, 72);
            tableLayoutPanel6.TabIndex = 7;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.ColumnCount = 2;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
            tableLayoutPanel7.Controls.Add(lblCoilumnaPropiedad, 0, 0);
            tableLayoutPanel7.Controls.Add(cbxFiltroExtraEstado, 1, 0);
            tableLayoutPanel7.Location = new Point(3, 39);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 1;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Size = new Size(528, 30);
            tableLayoutPanel7.TabIndex = 7;
            // 
            // lblCoilumnaPropiedad
            // 
            lblCoilumnaPropiedad.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lblCoilumnaPropiedad.AutoSize = true;
            lblCoilumnaPropiedad.Location = new Point(3, 0);
            lblCoilumnaPropiedad.Name = "lblCoilumnaPropiedad";
            lblCoilumnaPropiedad.Size = new Size(61, 30);
            lblCoilumnaPropiedad.TabIndex = 9;
            lblCoilumnaPropiedad.Text = "Propiedad";
            lblCoilumnaPropiedad.Visible = false;
            // 
            // cbxFiltroExtraEstado
            // 
            cbxFiltroExtraEstado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbxFiltroExtraEstado.FormattingEnabled = true;
            cbxFiltroExtraEstado.Location = new Point(179, 3);
            cbxFiltroExtraEstado.Name = "cbxFiltroExtraEstado";
            cbxFiltroExtraEstado.Size = new Size(346, 23);
            cbxFiltroExtraEstado.TabIndex = 5;
            cbxFiltroExtraEstado.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
            tableLayoutPanel2.Controls.Add(lblFiltrarPorColumna, 0, 0);
            tableLayoutPanel2.Controls.Add(cbxFiltroOpcional, 1, 0);
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(528, 30);
            tableLayoutPanel2.TabIndex = 7;
            // 
            // lblFiltrarPorColumna
            // 
            lblFiltrarPorColumna.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lblFiltrarPorColumna.AutoSize = true;
            lblFiltrarPorColumna.Location = new Point(3, 0);
            lblFiltrarPorColumna.Name = "lblFiltrarPorColumna";
            lblFiltrarPorColumna.Size = new Size(58, 30);
            lblFiltrarPorColumna.TabIndex = 8;
            lblFiltrarPorColumna.Text = "Filtrar por";
            lblFiltrarPorColumna.Visible = false;
            // 
            // cbxFiltroOpcional
            // 
            cbxFiltroOpcional.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbxFiltroOpcional.FormattingEnabled = true;
            cbxFiltroOpcional.Location = new Point(179, 3);
            cbxFiltroOpcional.Name = "cbxFiltroOpcional";
            cbxFiltroOpcional.Size = new Size(346, 23);
            cbxFiltroOpcional.TabIndex = 0;
            cbxFiltroOpcional.Visible = false;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(dtpHasta, 0, 2);
            tableLayoutPanel3.Controls.Add(dtpDesde, 0, 1);
            tableLayoutPanel3.Controls.Add(chkUsarFecha, 0, 0);
            tableLayoutPanel3.Location = new Point(3, 73);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 3;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.Size = new Size(534, 72);
            tableLayoutPanel3.TabIndex = 7;
            // 
            // dtpHasta
            // 
            dtpHasta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtpHasta.Location = new Point(3, 51);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(528, 23);
            dtpHasta.TabIndex = 2;
            dtpHasta.Visible = false;
            // 
            // dtpDesde
            // 
            dtpDesde.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtpDesde.Location = new Point(3, 27);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(528, 23);
            dtpDesde.TabIndex = 1;
            dtpDesde.Visible = false;
            // 
            // chkUsarFecha
            // 
            chkUsarFecha.AutoSize = true;
            chkUsarFecha.Location = new Point(3, 3);
            chkUsarFecha.Name = "chkUsarFecha";
            chkUsarFecha.Size = new Size(83, 18);
            chkUsarFecha.TabIndex = 3;
            chkUsarFecha.Text = "Usar Fecha";
            chkUsarFecha.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58.82353F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 41.17647F));
            tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 0, 0);
            tableLayoutPanel4.Controls.Add(btnBuscar, 1, 0);
            tableLayoutPanel4.Location = new Point(3, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(534, 64);
            tableLayoutPanel4.TabIndex = 7;
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
            tableLayoutPanel5.Size = new Size(308, 58);
            tableLayoutPanel5.TabIndex = 2;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(3, 0);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(42, 15);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar";
            lblBuscar.TextAlign = ContentAlignment.BottomLeft;
            // 
            // FBaseConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new Size(1118, 561);
            Controls.Add(panel1);
            Name = "FBaseConsulta";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FBaseConsulta";
            WindowState = FormWindowState.Maximized;
            Load += FBaseConsulta_Load;
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).EndInit();
            BarraLateralBotones.ResumeLayout(false);
            BarraLateralBotones.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        protected TextBox txtBuscar;
        protected Button btnBuscar;
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
        protected CheckBox cbxEstaEliminado;
        public Panel panel1;
        protected DateTimePicker dtpHasta;
        protected DateTimePicker dtpDesde;
        protected ComboBox cbxFiltroOpcional;
        protected ComboBox cbxFiltroExtraEstado;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        protected Label lblFiltrarPorColumna;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel5;
        private Label lblBuscar;
        public CheckBox chkUsarFecha;
        private TableLayoutPanel tableLayoutPanel7;
        protected Label lblCoilumnaPropiedad;
    }
}