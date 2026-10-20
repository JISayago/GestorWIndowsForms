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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCategoriaABM));
            lblCategoria = new Label();
            txtCategoria = new TextBox();
            label5 = new Label();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCategoria.Location = new Point(30, 99);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(77, 21);
            lblCategoria.TabIndex = 1;
            lblCategoria.Tag = "NoModificarConBase";
            lblCategoria.Text = "Categoria";
            // 
            // txtCategoria
            // 
            txtCategoria.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCategoria.Location = new Point(123, 96);
            txtCategoria.Name = "txtCategoria";
            txtCategoria.Size = new Size(262, 29);
            txtCategoria.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Red;
            label5.Location = new Point(123, 67);
            label5.Name = "label5";
            label5.Size = new Size(260, 17);
            label5.TabIndex = 48;
            label5.Tag = "NoModificarConBase";
            label5.Text = "El ícono  *  representa campo obligatorio.";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Red;
            label6.Location = new Point(391, 100);
            label6.Name = "label6";
            label6.Size = new Size(16, 20);
            label6.TabIndex = 49;
            label6.Tag = "NoModificarConBase";
            label6.Text = "*";
            // 
            // FCategoriaABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(422, 147);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(txtCategoria);
            Controls.Add(lblCategoria);
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FCategoriaABM";
            Text = "ABM Categoria";
            Controls.SetChildIndex(lblCategoria, 0);
            Controls.SetChildIndex(txtCategoria, 0);
            Controls.SetChildIndex(label5, 0);
            Controls.SetChildIndex(label6, 0);
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCategoria;
        private TextBox txtCategoria;
        private Label label5;
        private Label label6;
    }
}