namespace Presentacion.Core.Cliente
{
    partial class FClienteConsulta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FClienteConsulta));
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(234, 234, 234);
            panel1.Size = new Size(1226, 561);
            // 
            // chkBool1
            // 
            chkBool1.Font = new Font("Segoe UI", 9.75F);
            chkBool1.ForeColor = Color.FromArgb(31, 26, 43);
            chkBool1.Size = new Size(17, 1);
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.FromArgb(220, 199, 255);
            btnBuscar.FlatAppearance.BorderColor = Color.Black;
            btnBuscar.FlatAppearance.MouseOverBackColor = Color.FromArgb(95, 48, 163);
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnBuscar.ForeColor = Color.Black;
            // 
            // lblTotalRegistros
            // 
            lblTotalRegistros.Font = new Font("Segoe UI Semibold", 10.25F, FontStyle.Bold);
            lblTotalRegistros.ForeColor = Color.FromArgb(31, 26, 43);
            lblTotalRegistros.Location = new Point(256, 39);
            lblTotalRegistros.Size = new Size(55, 19);
            // 
            // FClienteConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1226, 561);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FClienteConsulta";
            Text = "Consulta Clientes";
            Load += FClienteConsulta_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}