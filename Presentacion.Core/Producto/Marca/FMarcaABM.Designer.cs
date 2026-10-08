namespace Presentacion.Core.Articulo.Marca
{
    partial class FMarcaABM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMarcaABM));
            txtMarca = new TextBox();
            lblMarca = new Label();
            label5 = new Label();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // txtMarca
            // 
            txtMarca.Font = new Font("Segoe UI", 12F);
            txtMarca.Location = new Point(101, 93);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(262, 29);
            txtMarca.TabIndex = 6;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Font = new Font("Segoe UI", 12F);
            lblMarca.Location = new Point(28, 100);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(53, 21);
            lblMarca.TabIndex = 5;
            lblMarca.Tag = "NoModificarConBase";
            lblMarca.Text = "Marca";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Red;
            label5.Location = new Point(103, 64);
            label5.Name = "label5";
            label5.Size = new Size(260, 17);
            label5.TabIndex = 49;
            label5.Tag = "NoModificarConBase";
            label5.Text = "El ícono  *  representa campo obligatorio.";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Red;
            label6.Location = new Point(369, 100);
            label6.Name = "label6";
            label6.Size = new Size(16, 20);
            label6.TabIndex = 50;
            label6.Tag = "NoModificarConBase";
            label6.Text = "*";
            // 
            // FMarcaABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(399, 147);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(txtMarca);
            Controls.Add(lblMarca);
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FMarcaABM";
            Text = "ABM Marca";
            Controls.SetChildIndex(lblMarca, 0);
            Controls.SetChildIndex(txtMarca, 0);
            Controls.SetChildIndex(label5, 0);
            Controls.SetChildIndex(label6, 0);
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtMarca;
        private Label lblMarca;
        private Label label5;
        private Label label6;
    }
}