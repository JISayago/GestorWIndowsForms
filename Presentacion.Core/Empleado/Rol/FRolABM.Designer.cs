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
            txtNombre = new TextBox();
            txtCodigoRol = new TextBox();
            lblNombre = new Label();
            lblDescripcion = new Label();
            lblCodigoRol = new Label();
            txtDescripcionRol = new TextBox();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 9.75F);
            txtNombre.Location = new Point(170, 149);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(498, 25);
            txtNombre.TabIndex = 19;
            // 
            // txtCodigoRol
            // 
            txtCodigoRol.Font = new Font("Segoe UI", 9.75F);
            txtCodigoRol.Location = new Point(170, 180);
            txtCodigoRol.Name = "txtCodigoRol";
            txtCodigoRol.Size = new Size(498, 25);
            txtCodigoRol.TabIndex = 18;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9.75F);
            lblNombre.Location = new Point(103, 151);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(57, 17);
            lblNombre.TabIndex = 17;
            lblNombre.Text = "Nombre";
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Segoe UI", 9.75F);
            lblDescripcion.Location = new Point(39, 219);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(121, 17);
            lblDescripcion.TabIndex = 16;
            lblDescripcion.Text = "Descripcion del Rol";
            // 
            // lblCodigoRol
            // 
            lblCodigoRol.AutoSize = true;
            lblCodigoRol.Font = new Font("Segoe UI", 9.75F);
            lblCodigoRol.Location = new Point(86, 183);
            lblCodigoRol.Name = "lblCodigoRol";
            lblCodigoRol.Size = new Size(74, 17);
            lblCodigoRol.TabIndex = 15;
            lblCodigoRol.Text = "Codigo Rol";
            // 
            // txtDescripcionRol
            // 
            txtDescripcionRol.Location = new Point(172, 218);
            txtDescripcionRol.Multiline = true;
            txtDescripcionRol.Name = "txtDescripcionRol";
            txtDescripcionRol.Size = new Size(496, 77);
            txtDescripcionRol.TabIndex = 20;
            // 
            // FRolABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 450);
            Controls.Add(txtDescripcionRol);
            Controls.Add(txtNombre);
            Controls.Add(txtCodigoRol);
            Controls.Add(lblNombre);
            Controls.Add(lblDescripcion);
            Controls.Add(lblCodigoRol);
            Name = "FRolABM";
            Text = "FRolABM";
            Controls.SetChildIndex(lblCodigoRol, 0);
            Controls.SetChildIndex(lblDescripcion, 0);
            Controls.SetChildIndex(lblNombre, 0);
            Controls.SetChildIndex(txtCodigoRol, 0);
            Controls.SetChildIndex(txtNombre, 0);
            Controls.SetChildIndex(txtDescripcionRol, 0);
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
    }
}