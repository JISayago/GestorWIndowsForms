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
            panel2 = new Panel();
            label1 = new Label();
            button1 = new Button();
            textBox1 = new TextBox();
            dgvGrilla = new DataGridView();
            checkBox1 = new CheckBox();
            btnNuevo = new ToolStripButton();
            btnEliminar = new ToolStripButton();
            btnModificar = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnActualizar = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnImprimir = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            btnSalir = new ToolStripButton();
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
            BarraLateralBotones.BackColor = SystemColors.AppWorkspace;
            BarraLateralBotones.Dock = DockStyle.Left;
            BarraLateralBotones.Items.AddRange(new ToolStripItem[] { btnNuevo, btnEliminar, btnModificar, toolStripSeparator1, btnActualizar, toolStripSeparator2, btnImprimir, toolStripSeparator3, btnSalir });
            BarraLateralBotones.Location = new Point(0, 0);
            BarraLateralBotones.Name = "BarraLateralBotones";
            BarraLateralBotones.Size = new Size(64, 537);
            BarraLateralBotones.TabIndex = 0;
            BarraLateralBotones.Text = "toolStrip1";
            // 
            // panel2
            // 
            panel2.Controls.Add(label1);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(textBox1);
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
            // button1
            // 
            button1.Location = new Point(366, 4);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(106, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(254, 23);
            textBox1.TabIndex = 0;
            // 
            // dgvGrilla
            // 
            dgvGrilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGrilla.Location = new Point(108, 50);
            dgvGrilla.Name = "dgvGrilla";
            dgvGrilla.Size = new Size(662, 499);
            dgvGrilla.TabIndex = 2;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(104, 19);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(186, 19);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "Mostrar elementos eliminados";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnNuevo
            // 
            btnNuevo.Image = (Image)resources.GetObject("btnNuevo.Image");
            btnNuevo.ImageTransparentColor = Color.Magenta;
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(61, 35);
            btnNuevo.Text = "Nuevo";
            btnNuevo.TextImageRelation = TextImageRelation.TextAboveImage;
            // 
            // btnEliminar
            // 
            btnEliminar.Image = (Image)resources.GetObject("btnEliminar.Image");
            btnEliminar.ImageTransparentColor = Color.Magenta;
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(61, 35);
            btnEliminar.Text = "Eliminar";
            btnEliminar.TextImageRelation = TextImageRelation.TextAboveImage;
            // 
            // btnModificar
            // 
            btnModificar.Image = (Image)resources.GetObject("btnModificar.Image");
            btnModificar.ImageTransparentColor = Color.Magenta;
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(61, 35);
            btnModificar.Text = "Modificar";
            btnModificar.TextImageRelation = TextImageRelation.TextAboveImage;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(61, 6);
            // 
            // btnActualizar
            // 
            btnActualizar.Image = (Image)resources.GetObject("btnActualizar.Image");
            btnActualizar.ImageTransparentColor = Color.Magenta;
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(61, 35);
            btnActualizar.Text = "Actualizar";
            btnActualizar.TextImageRelation = TextImageRelation.TextAboveImage;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(61, 6);
            // 
            // btnImprimir
            // 
            btnImprimir.Image = (Image)resources.GetObject("btnImprimir.Image");
            btnImprimir.ImageTransparentColor = Color.Magenta;
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(61, 35);
            btnImprimir.Text = "Imprimir";
            btnImprimir.TextImageRelation = TextImageRelation.TextAboveImage;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(61, 6);
            // 
            // btnSalir
            // 
            btnSalir.Alignment = ToolStripItemAlignment.Right;
            btnSalir.Image = (Image)resources.GetObject("btnSalir.Image");
            btnSalir.ImageTransparentColor = Color.Magenta;
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(61, 35);
            btnSalir.Text = "Salir";
            btnSalir.TextImageRelation = TextImageRelation.TextAboveImage;
            // 
            // FBaseConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(checkBox1);
            Controls.Add(dgvGrilla);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FBaseConsulta";
            Text = "FBaseConsulta";
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

        private Panel panel1;
        private ToolStrip BarraLateralBotones;
        private Panel panel2;
        private TextBox textBox1;
        private Label label1;
        private Button button1;
        private DataGridView dgvGrilla;
        private CheckBox checkBox1;
        private ToolStripButton btnNuevo;
        private ToolStripButton btnEliminar;
        private ToolStripButton btnModificar;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnActualizar;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnImprimir;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton btnSalir;
    }
}