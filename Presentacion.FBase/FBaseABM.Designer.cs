namespace Presentacion.FBase
{
    partial class FBaseABM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FBaseABM));
            panel1 = new Panel();
            BarraSuperiorBotones = new ToolStrip();
            btnEjecutar = new ToolStripButton();
            btnVolver = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnSalir = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnLimpiar = new ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            panel1.SuspendLayout();
            BarraSuperiorBotones.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(BarraSuperiorBotones);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(784, 53);
            panel1.TabIndex = 0;
            // 
            // BarraSuperiorBotones
            // 
            BarraSuperiorBotones.BackColor = SystemColors.AppWorkspace;
            BarraSuperiorBotones.Items.AddRange(new ToolStripItem[] { btnEjecutar, toolStripSeparator2, btnSalir, toolStripSeparator1, btnLimpiar, btnVolver });
            BarraSuperiorBotones.Location = new Point(0, 0);
            BarraSuperiorBotones.Name = "BarraSuperiorBotones";
            BarraSuperiorBotones.Size = new Size(784, 25);
            BarraSuperiorBotones.TabIndex = 0;
            BarraSuperiorBotones.Text = "toolStrip1";
            // 
            // btnEjecutar
            // 
            btnEjecutar.BackColor = SystemColors.ButtonHighlight;
            btnEjecutar.ImageTransparentColor = Color.Magenta;
            btnEjecutar.Name = "btnEjecutar";
            btnEjecutar.Size = new Size(53, 22);
            btnEjecutar.Text = "Ejecutar";
            btnEjecutar.TextImageRelation = TextImageRelation.ImageAboveText;
            btnEjecutar.Click += btnEjecutar_Click;
            // 
            // btnVolver
            // 
            btnVolver.Alignment = ToolStripItemAlignment.Right;
            btnVolver.BackColor = SystemColors.ButtonHighlight;
            btnVolver.ImageTransparentColor = Color.Magenta;
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(87, 22);
            btnVolver.Text = "Volver al Inicio";
            btnVolver.TextImageRelation = TextImageRelation.ImageAboveText;
            btnVolver.Click += toolStripButton1_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.BackColor = SystemColors.ActiveCaption;
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // btnSalir
            // 
            btnSalir.Alignment = ToolStripItemAlignment.Right;
            btnSalir.BackColor = SystemColors.ButtonHighlight;
            btnSalir.ImageTransparentColor = Color.Magenta;
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(33, 22);
            btnSalir.Text = "Salir";
            btnSalir.TextImageRelation = TextImageRelation.ImageAboveText;
            btnSalir.Click += btnSalir_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Alignment = ToolStripItemAlignment.Right;
            toolStripSeparator1.BackColor = SystemColors.ActiveCaption;
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = SystemColors.ButtonHighlight;
            btnLimpiar.ImageTransparentColor = Color.Magenta;
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(51, 22);
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.TextImageRelation = TextImageRelation.ImageAboveText;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // FBaseABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(panel1);
            Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(31, 26, 43);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(800, 600);
            Name = "FBaseABM";
            Text = "FBaseABM";
            Load += FBaseABM_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            BarraSuperiorBotones.ResumeLayout(false);
            BarraSuperiorBotones.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private ToolStrip BarraSuperiorBotones;
        private ToolStripButton btnEjecutar;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnSalir;
        protected ToolStripButton btnLimpiar;
        protected ToolStripButton btnVolver;
        private ToolStripSeparator toolStripSeparator2;
    }
}