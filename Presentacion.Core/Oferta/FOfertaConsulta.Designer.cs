namespace Presentacion.Core.Oferta
{
    partial class FOfertaConsulta
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
            pnlFiltrosAvanzados.SuspendLayout();
            SuspendLayout();
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(106, 5);
            // 
            // panel1
            // 
            panel1.Size = new Size(1052, 561);
            // 
            // lblFiltro
            // 
            lblFiltro.Location = new Point(22, 83);
            // 
            // chkUsarFecha
            // 
            chkUsarFecha.Location = new Point(218, 11);
            // 
            // dtpHasta
            // 
            dtpHasta.Location = new Point(513, 6);
            // 
            // dtpDesde
            // 
            dtpDesde.Location = new Point(307, 6);
            // 
            // FOfertaConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1052, 561);
            Name = "FOfertaConsulta";
            Text = "FOfertaConsulta";
            Load += FOfertaConsulta_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            pnlFiltrosAvanzados.ResumeLayout(false);
            pnlFiltrosAvanzados.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}