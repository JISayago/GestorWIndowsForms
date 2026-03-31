namespace Presentacion.Core.Venta
{
    partial class FVentaLibreConsulta
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
            panel1.Size = new Size(800, 450);
            // 
            // pnlFiltrosAvanzados
            // 
            pnlFiltrosAvanzados.Size = new Size(765, 114);
            // 
            // chkUsarFecha
            // 
            chkUsarFecha.Size = new Size(61, 19);
            // 
            // dtpHasta
            // 
            dtpHasta.Location = new Point(70, 42);
            dtpHasta.Size = new Size(61, 23);
            // 
            // dtpDesde
            // 
            dtpDesde.Location = new Point(70, 3);
            dtpDesde.Size = new Size(61, 23);
            // 
            // cbxFiltroOpcional
            // 
            cbxFiltroOpcional.Location = new Point(47, 3);
            cbxFiltroOpcional.Size = new Size(39, 23);
            // 
            // cbxFiltroExtraEstado
            // 
            cbxFiltroExtraEstado.Location = new Point(177, 42);
            cbxFiltroExtraEstado.Size = new Size(89, 23);
            // 
            // FVentaLibreConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Name = "FVentaLibreConsulta";
            Text = "FVentaLibreConsulta";
            Load += FVentaLibreConsulta_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}