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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FBaseConsulta));
            panel1 = new Panel();
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
            panel2 = new Panel();
            label1 = new Label();
            btnBuscar = new Button();
            txtBuscar = new TextBox();
            dgvGrilla = new DataGridView();
            cbxEstaEliminado = new CheckBox();
            panel1.SuspendLayout();
            BarraLateralBotones.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(BarraLateralBotones);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(86, 537);
            panel1.TabIndex = 0;
            // 
            // BarraLateralBotones
            // 
            BarraLateralBotones.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BarraLateralBotones.BackColor = SystemColors.AppWorkspace;
            BarraLateralBotones.CanOverflow = false;
            BarraLateralBotones.Dock = DockStyle.None;
            BarraLateralBotones.Items.AddRange(new ToolStripItem[] { btnNuevo, btnEliminar, btnModificar, toolStripSeparator1, btnActualizar, toolStripSeparator2, btnImprimir, toolStripSeparator3, btnSalir });
            BarraLateralBotones.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
            BarraLateralBotones.Location = new Point(0, 0);
            BarraLateralBotones.Name = "BarraLateralBotones";
            BarraLateralBotones.Size = new Size(64, 257);
            BarraLateralBotones.Stretch = true;
            BarraLateralBotones.TabIndex = 0;
            BarraLateralBotones.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            btnNuevo.Image = (Image)resources.GetObject("btnNuevo.Image");
            btnNuevo.ImageTransparentColor = Color.Magenta;
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(62, 35);
            btnNuevo.Text = "Nuevo";
            btnNuevo.TextImageRelation = TextImageRelation.TextAboveImage;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Image = (Image)resources.GetObject("btnEliminar.Image");
            btnEliminar.ImageTransparentColor = Color.Magenta;
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(62, 35);
            btnEliminar.Text = "Eliminar";
            btnEliminar.TextImageRelation = TextImageRelation.TextAboveImage;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Image = (Image)resources.GetObject("btnModificar.Image");
            btnModificar.ImageTransparentColor = Color.Magenta;
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(62, 35);
            btnModificar.Text = "Modificar";
            btnModificar.TextImageRelation = TextImageRelation.TextAboveImage;
            btnModificar.Click += btnModificar_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(62, 6);
            // 
            // btnActualizar
            // 
            btnActualizar.Image = (Image)resources.GetObject("btnActualizar.Image");
            btnActualizar.ImageTransparentColor = Color.Magenta;
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(62, 35);
            btnActualizar.Text = "Actualizar";
            btnActualizar.TextImageRelation = TextImageRelation.TextAboveImage;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(62, 6);
            // 
            // btnImprimir
            // 
            btnImprimir.Image = (Image)resources.GetObject("btnImprimir.Image");
            btnImprimir.ImageTransparentColor = Color.Magenta;
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(62, 35);
            btnImprimir.Text = "Imprimir";
            btnImprimir.TextImageRelation = TextImageRelation.TextAboveImage;
            btnImprimir.Click += btnImprimir_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(62, 6);
            // 
            // btnSalir
            // 
            btnSalir.Alignment = ToolStripItemAlignment.Right;
            btnSalir.Image = (Image)resources.GetObject("btnSalir.Image");
            btnSalir.ImageTransparentColor = Color.Magenta;
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(62, 35);
            btnSalir.Text = "Salir";
            btnSalir.TextImageRelation = TextImageRelation.TextAboveImage;
            btnSalir.Click += btnSalir_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(label1);
            panel2.Controls.Add(btnBuscar);
            panel2.Controls.Add(txtBuscar);
            panel2.Location = new Point(329, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(447, 32);
            panel2.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(41, 6);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 2;
            label1.Text = "Busqueda";
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(366, 4);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(75, 23);
            btnBuscar.TabIndex = 1;
            btnBuscar.Text = "buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click_1;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(106, 3);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(254, 23);
            txtBuscar.TabIndex = 0;
            // 
            // dgvGrilla
            // 
            dgvGrilla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvGrilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGrilla.Location = new Point(108, 50);
            dgvGrilla.Name = "dgvGrilla";
            dgvGrilla.Size = new Size(662, 499);
            dgvGrilla.TabIndex = 2;
            // 
            // cbxEstaEliminado
            // 
            cbxEstaEliminado.AutoSize = true;
            cbxEstaEliminado.Location = new Point(104, 19);
            cbxEstaEliminado.Name = "cbxEstaEliminado";
            cbxEstaEliminado.Size = new Size(211, 19);
            cbxEstaEliminado.TabIndex = 3;
            cbxEstaEliminado.Text = "Mostrar sólo elementos eliminados";
            cbxEstaEliminado.UseVisualStyleBackColor = true;
            cbxEstaEliminado.CheckedChanged += cbxEstaEliminado_CheckedChanged;
            // 
            // FBaseConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new Size(784, 561);
            Controls.Add(cbxEstaEliminado);
            Controls.Add(dgvGrilla);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FBaseConsulta";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FBaseConsulta";
            WindowState = FormWindowState.Maximized;
            Load += FBaseConsulta_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            BarraLateralBotones.ResumeLayout(false);
            BarraLateralBotones.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel2;
        private TextBox txtBuscar;
        private Label label1;
        private Button btnBuscar;
        private ToolStripButton btnNuevo;
        private ToolStripButton btnEliminar;
        private ToolStripButton btnModificar;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnActualizar;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnImprimir;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton btnSalir;
        protected DataGridView dgvGrilla;
        protected CheckBox cbxEstaEliminado;
        protected ToolStrip BarraLateralBotones;
        public Panel panel1;
    }
}