namespace Presentacion.Core.Venta
{
    partial class FSeleccionCantidadPagos
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
            lblSeleccionCantPagos = new Label();
            cbx1pago = new CheckBox();
            cbxMultiplesPagos = new CheckBox();
            btnContinuar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // lblSeleccionCantPagos
            // 
            lblSeleccionCantPagos.AutoSize = true;
            lblSeleccionCantPagos.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSeleccionCantPagos.Location = new Point(22, 9);
            lblSeleccionCantPagos.Name = "lblSeleccionCantPagos";
            lblSeleccionCantPagos.Size = new Size(260, 21);
            lblSeleccionCantPagos.TabIndex = 0;
            lblSeleccionCantPagos.Text = "Seleccionar la cantidad de pagos";
            // 
            // cbx1pago
            // 
            cbx1pago.AutoSize = true;
            cbx1pago.Location = new Point(32, 49);
            cbx1pago.Name = "cbx1pago";
            cbx1pago.Size = new Size(88, 19);
            cbx1pago.TabIndex = 1;
            cbx1pago.Text = "1 Solo Pago";
            cbx1pago.UseVisualStyleBackColor = true;
            cbx1pago.CheckedChanged += cbx1pago_CheckedChanged;
            // 
            // cbxMultiplesPagos
            // 
            cbxMultiplesPagos.AutoSize = true;
            cbxMultiplesPagos.Location = new Point(161, 49);
            cbxMultiplesPagos.Name = "cbxMultiplesPagos";
            cbxMultiplesPagos.Size = new Size(110, 19);
            cbxMultiplesPagos.TabIndex = 2;
            cbxMultiplesPagos.Text = "Multiples Pagos";
            cbxMultiplesPagos.UseVisualStyleBackColor = true;
            cbxMultiplesPagos.CheckedChanged += cbxMultiplesPagos_CheckedChanged;
            // 
            // btnContinuar
            // 
            btnContinuar.Location = new Point(70, 87);
            btnContinuar.Name = "btnContinuar";
            btnContinuar.Size = new Size(75, 23);
            btnContinuar.TabIndex = 3;
            btnContinuar.Text = "Continuar";
            btnContinuar.UseVisualStyleBackColor = true;
            btnContinuar.Click += btnContinuar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(161, 87);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FSeleccionCantidadPagos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(306, 122);
            Controls.Add(btnCancelar);
            Controls.Add(btnContinuar);
            Controls.Add(cbxMultiplesPagos);
            Controls.Add(cbx1pago);
            Controls.Add(lblSeleccionCantPagos);
            Name = "FSeleccionCantidadPagos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FSeleccionCantidadPagos";
            Load += FSeleccionCantidadPagos_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSeleccionCantPagos;
        private CheckBox cbx1pago;
        private CheckBox cbxMultiplesPagos;
        private Button btnContinuar;
        private Button btnCancelar;
    }
}