namespace Presentacion.Core.Marca
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
            textBox1 = new TextBox();
            lblMarca = new Label();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Red;
            label3.Location = new Point(230, 117);
            label3.Name = "label3";
            label3.Size = new Size(125, 15);
            label3.TabIndex = 7;
            label3.Text = "Campo Obligatorio (*)";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 15.75F);
            textBox1.Location = new Point(130, 79);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(219, 35);
            textBox1.TabIndex = 6;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Font = new Font("Segoe UI", 15.75F);
            lblMarca.Location = new Point(42, 84);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(82, 30);
            lblMarca.TabIndex = 5;
            lblMarca.Text = "Marca: ";
            // 
            // FMarcaABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(404, 171);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(lblMarca);
            Name = "FMarcaABM";
            Text = "FMarcaABM";
            Controls.SetChildIndex(lblMarca, 0);
            Controls.SetChildIndex(textBox1, 0);
            Controls.SetChildIndex(label3, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private TextBox textBox1;
        private Label lblMarca;
    }
}