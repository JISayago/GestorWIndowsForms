namespace Presentacion.Core.Venta
{
    partial class FDetalleVenta
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
            txtDetalleVenta = new TextBox();
            lblDetallesExtraDeVenta = new Label();
            btnConfirmarDetalle = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // txtDetalleVenta
            // 
            txtDetalleVenta.Location = new Point(30, 53);
            txtDetalleVenta.Multiline = true;
            txtDetalleVenta.Name = "txtDetalleVenta";
            txtDetalleVenta.Size = new Size(602, 126);
            txtDetalleVenta.TabIndex = 27;
            // 
            // lblDetallesExtraDeVenta
            // 
            lblDetallesExtraDeVenta.AutoSize = true;
            lblDetallesExtraDeVenta.Location = new Point(27, 31);
            lblDetallesExtraDeVenta.Name = "lblDetallesExtraDeVenta";
            lblDetallesExtraDeVenta.Size = new Size(108, 15);
            lblDetallesExtraDeVenta.TabIndex = 26;
            lblDetallesExtraDeVenta.Text = "Detalles de la Venta";
            // 
            // btnConfirmarDetalle
            // 
            btnConfirmarDetalle.Location = new Point(223, 202);
            btnConfirmarDetalle.Name = "btnConfirmarDetalle";
            btnConfirmarDetalle.Size = new Size(75, 23);
            btnConfirmarDetalle.TabIndex = 28;
            btnConfirmarDetalle.Text = "Confirmar";
            btnConfirmarDetalle.UseVisualStyleBackColor = true;
            btnConfirmarDetalle.Click += btnConfirmarDetalle_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(332, 202);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 29;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FDetalleVenta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(688, 256);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmarDetalle);
            Controls.Add(txtDetalleVenta);
            Controls.Add(lblDetallesExtraDeVenta);
            Name = "FDetalleVenta";
            Text = "FDetalleVenta";
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtDetalleVenta;
        private Label lblDetallesExtraDeVenta;
        private Button btnConfirmarDetalle;
        private Button btnCancelar;
    }
}