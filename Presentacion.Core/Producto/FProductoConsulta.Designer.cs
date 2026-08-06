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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FProductoConsulta));
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(234, 234, 234);
            panel1.Size = new Size(1365, 561);
            // 
            // chkBool1
            // 
            chkBool1.ForeColor = Color.FromArgb(31, 26, 43);
            chkBool1.Location = new Point(666, 90);
            chkBool1.Size = new Size(17, 1);
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.FromArgb(220, 199, 255);
            btnBuscar.FlatAppearance.BorderColor = Color.Black;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.ForeColor = Color.Black;
            btnBuscar.Location = new Point(27, 3);
            btnBuscar.Size = new Size(305, 28);
            btnBuscar.UseVisualStyleBackColor = false;
            // 
            // lblTotalRegistros
            // 
            lblTotalRegistros.Location = new Point(145, 10);
            // 
            // FProductoConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1365, 561);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FProductoConsulta";
            Text = "Consulta Productos";
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}