namespace Presentacion.Core.Producto
{
    partial class FProductoConsulta
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
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Size = new Size(1258, 561);
            // 
            // pnlFiltrosAvanzados
            // 
            pnlFiltrosAvanzados.Size = new Size(1258, 114);
            // 
            // dtpHasta
            // 
            dtpHasta.Location = new Point(193, 42);
            dtpHasta.Size = new Size(184, 23);
            // 
            // dtpDesde
            // 
            dtpDesde.Location = new Point(193, 3);
            dtpDesde.Size = new Size(184, 23);
            // 
            // cbxFiltroOpcional
            // 
            cbxFiltroOpcional.Location = new Point(134, 3);
            cbxFiltroOpcional.Size = new Size(125, 23);
            // 
            // cbxFiltroExtraEstado
            // 
            cbxFiltroExtraEstado.Location = new Point(497, 42);
            cbxFiltroExtraEstado.Size = new Size(262, 23);
            // 
            // FProductoConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1258, 561);
            Name = "FProductoConsulta";
            Text = "FProductoConsulta";
            Load += FProductoConsulta_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}