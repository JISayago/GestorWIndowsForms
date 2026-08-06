namespace Presentacion.Core.Producto.Rubro
{
    partial class FRubroABM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRubroABM));
            label3 = new Label();
            txtRubro = new TextBox();
            lblRubro = new Label();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Red;
            label3.Location = new Point(253, 144);
            label3.Name = "label3";
            label3.Size = new Size(124, 15);
            label3.TabIndex = 10;
            label3.Text = "Campo Obligatorio (*)";
            // 
            // txtRubro
            // 
            txtRubro.Font = new Font("Segoe UI", 15.75F);
            txtRubro.Location = new Point(159, 106);
            txtRubro.Name = "txtRubro";
            txtRubro.Size = new Size(219, 35);
            txtRubro.TabIndex = 9;
            // 
            // lblRubro
            // 
            lblRubro.AutoSize = true;
            lblRubro.Font = new Font("Segoe UI", 15.75F);
            lblRubro.Location = new Point(62, 109);
            lblRubro.Name = "lblRubro";
            lblRubro.Size = new Size(69, 30);
            lblRubro.TabIndex = 8;
            lblRubro.Tag = "NoModificarConBase";
            lblRubro.Text = "Rubro";
            // 
            // FRubroABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 211);
            Controls.Add(label3);
            Controls.Add(txtRubro);
            Controls.Add(lblRubro);
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FRubroABM";
            Text = "ABM Rubro";
            Controls.SetChildIndex(lblRubro, 0);
            Controls.SetChildIndex(txtRubro, 0);
            Controls.SetChildIndex(label3, 0);
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private TextBox txtRubro;
        private Label lblRubro;
    }
}