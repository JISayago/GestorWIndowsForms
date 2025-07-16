namespace Presentacion.Core.TipoPago
{
    partial class FTipoPagoABM
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
            txtDescripcion = new TextBox();
            txtNombre = new TextBox();
            txtCodigo = new TextBox();
            lblNombre = new Label();
            lblDescripcion = new Label();
            lblCodigo = new Label();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(211, 221);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(496, 77);
            txtDescripcion.TabIndex = 26;
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 9.75F);
            txtNombre.Location = new Point(209, 152);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(498, 25);
            txtNombre.TabIndex = 25;
            // 
            // txtCodigo
            // 
            txtCodigo.Font = new Font("Segoe UI", 9.75F);
            txtCodigo.Location = new Point(209, 183);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(498, 25);
            txtCodigo.TabIndex = 24;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9.75F);
            lblNombre.Location = new Point(142, 154);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(57, 17);
            lblNombre.TabIndex = 23;
            lblNombre.Text = "Nombre";
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Segoe UI", 9.75F);
            lblDescripcion.Location = new Point(123, 222);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(76, 17);
            lblDescripcion.TabIndex = 22;
            lblDescripcion.Text = "Descripcion";
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Font = new Font("Segoe UI", 9.75F);
            lblCodigo.Location = new Point(142, 186);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(51, 17);
            lblCodigo.TabIndex = 21;
            lblCodigo.Text = "Codigo";
            // 
            // FTipoPagoABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 450);
            Controls.Add(txtDescripcion);
            Controls.Add(txtNombre);
            Controls.Add(txtCodigo);
            Controls.Add(lblNombre);
            Controls.Add(lblDescripcion);
            Controls.Add(lblCodigo);
            Name = "FTipoPagoABM";
            Text = "FTipoPagoABM";
            Controls.SetChildIndex(lblCodigo, 0);
            Controls.SetChildIndex(lblDescripcion, 0);
            Controls.SetChildIndex(lblNombre, 0);
            Controls.SetChildIndex(txtCodigo, 0);
            Controls.SetChildIndex(txtNombre, 0);
            Controls.SetChildIndex(txtDescripcion, 0);
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtDescripcion;
        private TextBox txtNombre;
        private TextBox txtCodigo;
        private Label lblNombre;
        private Label lblDescripcion;
        private Label lblCodigo;
    }
}