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
            label3 = new Label();
            txtMarca = new TextBox();
            lblMarca = new Label();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Red;
            label3.Location = new Point(240, 149);
            label3.Name = "label3";
            label3.Size = new Size(125, 15);
            label3.TabIndex = 7;
            label3.Text = "Campo Obligatorio (*)";
            // 
            // txtMarca
            // 
            txtMarca.Font = new Font("Segoe UI", 15.75F);
            txtMarca.Location = new Point(146, 111);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(219, 35);
            txtMarca.TabIndex = 6;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Font = new Font("Segoe UI", 15.75F);
            lblMarca.Location = new Point(33, 114);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(71, 30);
            lblMarca.TabIndex = 5;
            lblMarca.Text = "Marca";
            // 
            // FMarcaABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 450);
            Controls.Add(label3);
            Controls.Add(txtMarca);
            Controls.Add(lblMarca);
            Name = "FMarcaABM";
            Text = "FMarcaABM";
            Controls.SetChildIndex(lblMarca, 0);
            Controls.SetChildIndex(txtMarca, 0);
            Controls.SetChildIndex(label3, 0);
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private TextBox txtMarca;
        private Label lblMarca;
    }
}