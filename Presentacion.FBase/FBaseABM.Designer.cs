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
            panel1 = new Panel();
            BarraSuperiorBotones = new ToolStrip();
            btnEjecutar = new ToolStripButton();
            btnLimpiar = new ToolStripButton();
            toolStripSeparatorSpring = new ToolStripLabel();
            btnSalir = new ToolStripButton();
            panel1.SuspendLayout();
            BarraSuperiorBotones.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.Controls.Add(BarraSuperiorBotones);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(784, 28);
            panel1.TabIndex = 0;
            // 
            // BarraSuperiorBotones
            // 
            BarraSuperiorBotones.BackColor = SystemColors.AppWorkspace;
            BarraSuperiorBotones.Items.AddRange(new ToolStripItem[] { btnEjecutar, btnLimpiar, toolStripSeparatorSpring, btnSalir });
            BarraSuperiorBotones.Location = new Point(0, 0);
            BarraSuperiorBotones.Name = "BarraSuperiorBotones";
            BarraSuperiorBotones.Size = new Size(784, 28);
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
            // toolStripSeparatorSpring
            // 
            toolStripSeparatorSpring.BackColor = SystemColors.ActiveCaption;
            toolStripSeparatorSpring.Name = "toolStripSeparator1";
            toolStripSeparatorSpring.Size = new Size(6, 25);
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
            // FBaseABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximumSize = new Size(800, 600);
            Name = "FBaseABM";
            Text = "FBaseABM";
            Load += FBaseABM_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            BarraSuperiorBotones.ResumeLayout(false);
            BarraSuperiorBotones.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private ToolStrip BarraSuperiorBotones;
        private ToolStripButton btnEjecutar;
        private ToolStripButton btnLimpiar;
        private ToolStripButton btnSalir;
        private ToolStripLabel toolStripSeparatorSpring;
    }
}