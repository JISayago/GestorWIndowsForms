namespace Presentacion.Core.Categoria
{
    partial class FCategoriaABM
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
            lblCategoria = new Label();
            txtCategoria = new TextBox();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.Font = new Font("Segoe UI", 15.75F);
            lblCategoria.Location = new Point(17, 165);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(102, 30);
            lblCategoria.TabIndex = 1;
            lblCategoria.Text = "Categoria";
            // 
            // txtCategoria
            // 
            txtCategoria.Font = new Font("Segoe UI", 15.75F);
            txtCategoria.Location = new Point(130, 162);
            txtCategoria.Name = "txtCategoria";
            txtCategoria.Size = new Size(219, 35);
            txtCategoria.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Red;
            label3.Location = new Point(224, 200);
            label3.Name = "label3";
            label3.Size = new Size(125, 15);
            label3.TabIndex = 4;
            label3.Text = "Campo Obligatorio (*)";
            // 
            // FCategoriaABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 450);
            Controls.Add(label3);
            Controls.Add(txtCategoria);
            Controls.Add(lblCategoria);
            Name = "FCategoriaABM";
            Text = "FCategoriaABM";
            Controls.SetChildIndex(lblCategoria, 0);
            Controls.SetChildIndex(txtCategoria, 0);
            Controls.SetChildIndex(label3, 0);
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCategoria;
        private TextBox txtCategoria;
        private Label label3;
    }
}