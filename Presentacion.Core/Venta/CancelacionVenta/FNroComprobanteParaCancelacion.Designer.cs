namespace Presentacion.Core.Venta
{
    partial class FNroComprobanteParaCancelacion
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
            lblTitulo = new Label();
            txtNroComprobante = new TextBox();
            btnCancelar = new Button();
            btnBuscar = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(109, 19);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(359, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Ingrese el número de comprobante";
            // 
            // txtNroComprobante
            // 
            txtNroComprobante.BorderStyle = BorderStyle.FixedSingle;
            txtNroComprobante.Location = new Point(166, 66);
            txtNroComprobante.Name = "txtNroComprobante";
            txtNroComprobante.Size = new Size(228, 23);
            txtNroComprobante.TabIndex = 1;
            // 
            // btnCancelar
            // 
            btnCancelar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.Location = new Point(138, 137);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(109, 45);
            btnCancelar.TabIndex = 2;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBuscar.Location = new Point(321, 137);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(109, 45);
            btnBuscar.TabIndex = 3;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // FNroComprobanteParaCancelacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(562, 194);
            Controls.Add(btnBuscar);
            Controls.Add(btnCancelar);
            Controls.Add(txtNroComprobante);
            Controls.Add(lblTitulo);
            Name = "FNroComprobanteParaCancelacion";
            Text = "FCancelacionDevolucion";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private TextBox txtNroComprobante;
        private Button btnCancelar;
        private Button btnBuscar;
    }
}