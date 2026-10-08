namespace Presentacion.Core.Empleado.Rol
{
    partial class FRolABM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRolABM));
            txtNombre = new TextBox();
            txtCodigoRol = new TextBox();
            lblNombre = new Label();
            lblDescripcion = new Label();
            lblCodigoRol = new Label();
            txtDescripcionRol = new TextBox();
            label2 = new Label();
            label4 = new Label();
            label1 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 9.75F);
            txtNombre.Location = new Point(143, 90);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(399, 25);
            txtNombre.TabIndex = 19;
            // 
            // txtCodigoRol
            // 
            txtCodigoRol.Font = new Font("Segoe UI", 9.75F);
            txtCodigoRol.Location = new Point(143, 141);
            txtCodigoRol.Name = "txtCodigoRol";
            txtCodigoRol.Size = new Size(399, 25);
            txtCodigoRol.TabIndex = 18;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9.75F);
            lblNombre.Location = new Point(64, 98);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(57, 17);
            lblNombre.TabIndex = 17;
            lblNombre.Text = "Nombre";
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Segoe UI", 9.75F);
            lblDescripcion.Location = new Point(4, 200);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(121, 17);
            lblDescripcion.TabIndex = 16;
            lblDescripcion.Text = "Descripcion del Rol";
            // 
            // lblCodigoRol
            // 
            lblCodigoRol.AutoSize = true;
            lblCodigoRol.Font = new Font("Segoe UI", 9.75F);
            lblCodigoRol.Location = new Point(47, 144);
            lblCodigoRol.Name = "lblCodigoRol";
            lblCodigoRol.Size = new Size(74, 17);
            lblCodigoRol.TabIndex = 15;
            lblCodigoRol.Text = "Codigo Rol";
            // 
            // txtDescripcionRol
            // 
            txtDescripcionRol.Location = new Point(143, 199);
            txtDescripcionRol.Multiline = true;
            txtDescripcionRol.Name = "txtDescripcionRol";
            txtDescripcionRol.Size = new Size(399, 77);
            txtDescripcionRol.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Red;
            label2.Location = new Point(282, 65);
            label2.Name = "label2";
            label2.Size = new Size(260, 17);
            label2.TabIndex = 46;
            label2.Tag = "NoModificarConBase";
            label2.Text = "El ícono  *  representa campo obligatorio.";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Red;
            label4.Location = new Point(548, 95);
            label4.Name = "label4";
            label4.Size = new Size(16, 20);
            label4.TabIndex = 47;
            label4.Tag = "NoModificarConBase";
            label4.Text = "*";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(548, 146);
            label1.Name = "label1";
            label1.Size = new Size(16, 20);
            label1.TabIndex = 48;
            label1.Tag = "NoModificarConBase";
            label1.Text = "*";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Red;
            label3.Location = new Point(548, 200);
            label3.Name = "label3";
            label3.Size = new Size(16, 20);
            label3.TabIndex = 49;
            label3.Tag = "NoModificarConBase";
            label3.Text = "*";
            // 
            // FRolABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 300);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(txtDescripcionRol);
            Controls.Add(txtNombre);
            Controls.Add(txtCodigoRol);
            Controls.Add(lblNombre);
            Controls.Add(lblDescripcion);
            Controls.Add(lblCodigoRol);
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FRolABM";
            Text = "ABM Rol";
            Load += FRolABM_Load;
            Controls.SetChildIndex(lblCodigoRol, 0);
            Controls.SetChildIndex(lblDescripcion, 0);
            Controls.SetChildIndex(lblNombre, 0);
            Controls.SetChildIndex(txtCodigoRol, 0);
            Controls.SetChildIndex(txtNombre, 0);
            Controls.SetChildIndex(txtDescripcionRol, 0);
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(label4, 0);
            Controls.SetChildIndex(label1, 0);
            Controls.SetChildIndex(label3, 0);
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNombre;
        private TextBox txtCodigoRol;
        private Label lblNombre;
        private Label lblDescripcion;
        private Label lblCodigoRol;
        private TextBox txtDescripcionRol;
        private Label label2;
        private Label label4;
        private Label label1;
        private Label label3;
    }
}