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
            label1 = new Label();
            panel2 = new Panel();
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
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).BeginInit();
            BarraLateralBotones.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(106, 3);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(254, 23);
            txtBuscar.TabIndex = 0;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(41, 6);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 2;
            label1.Text = "Busqueda";
            // 
            // panel2
            // 
            panel2.Controls.Add(label1);
            panel2.Controls.Add(btnBuscar);
            panel2.Controls.Add(txtBuscar);
            panel2.Location = new Point(325, 48);
            panel2.Name = "panel2";
            panel2.Size = new Size(447, 32);
            panel2.TabIndex = 1;
            // 
            // dgvGrilla
            // 
            dgvGrilla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvGrilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGrilla.Location = new Point(10, 112);
            dgvGrilla.Name = "dgvGrilla";
            dgvGrilla.Size = new Size(760, 337);
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
            BarraLateralBotones.Size = new Size(784, 27);
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
            cbxEstaEliminado.AutoSize = true;
            cbxEstaEliminado.Location = new Point(23, 61);
            cbxEstaEliminado.Name = "cbxEstaEliminado";
            cbxEstaEliminado.Size = new Size(211, 19);
            cbxEstaEliminado.TabIndex = 3;
            cbxEstaEliminado.Text = "Mostrar sólo elementos eliminados";
            cbxEstaEliminado.UseVisualStyleBackColor = true;
            cbxEstaEliminado.CheckedChanged += cbxEstaEliminado_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(cbxEstaEliminado);
            panel1.Controls.Add(BarraLateralBotones);
            panel1.Controls.Add(dgvGrilla);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(784, 561);
            panel1.TabIndex = 0;
            // 
            // FBaseConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new Size(784, 561);
            Controls.Add(panel1);
            Name = "FBaseConsulta";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FBaseConsulta";
            WindowState = FormWindowState.Maximized;
            Load += FBaseConsulta_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).EndInit();
            BarraLateralBotones.ResumeLayout(false);
            BarraLateralBotones.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtBuscar;
        private Button btnBuscar;
        private Label label1;
        private Panel panel2;
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
    }
}