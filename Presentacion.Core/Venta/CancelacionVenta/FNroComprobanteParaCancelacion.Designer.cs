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
            txtNroComprobante = new TextBox();
            btnCancelar = new Button();
            btnBuscar = new Button();
            dtpFecha = new DateTimePicker();
            lblFIltrarFecha = new Label();
            cbxSeleccionNroComprobante = new CheckBox();
            SuspendLayout();
            // 
            // txtNroComprobante
            // 
            txtNroComprobante.BorderStyle = BorderStyle.FixedSingle;
            txtNroComprobante.Location = new Point(301, 73);
            txtNroComprobante.Name = "txtNroComprobante";
            txtNroComprobante.Size = new Size(200, 23);
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
            // dtpFecha
            // 
            dtpFecha.Location = new Point(301, 28);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(200, 23);
            dtpFecha.TabIndex = 4;
            // 
            // lblFIltrarFecha
            // 
            lblFIltrarFecha.AutoSize = true;
            lblFIltrarFecha.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFIltrarFecha.Location = new Point(12, 28);
            lblFIltrarFecha.Name = "lblFIltrarFecha";
            lblFIltrarFecha.Size = new Size(283, 20);
            lblFIltrarFecha.TabIndex = 5;
            lblFIltrarFecha.Text = "Seleccione la fecha para buscar la venta";
            // 
            // cbxSeleccionNroComprobante
            // 
            cbxSeleccionNroComprobante.AutoSize = true;
            cbxSeleccionNroComprobante.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbxSeleccionNroComprobante.Location = new Point(60, 72);
            cbxSeleccionNroComprobante.Name = "cbxSeleccionNroComprobante";
            cbxSeleccionNroComprobante.Size = new Size(219, 21);
            cbxSeleccionNroComprobante.TabIndex = 6;
            cbxSeleccionNroComprobante.Text = "¿Buscar por Nro Comprobante?";
            cbxSeleccionNroComprobante.UseVisualStyleBackColor = true;
            cbxSeleccionNroComprobante.CheckedChanged += cbxSeleccionNroComprobante_CheckedChanged;
            // 
            // FNroComprobanteParaCancelacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(562, 194);
            Controls.Add(cbxSeleccionNroComprobante);
            Controls.Add(lblFIltrarFecha);
            Controls.Add(dtpFecha);
            Controls.Add(btnBuscar);
            Controls.Add(btnCancelar);
            Controls.Add(txtNroComprobante);
            Name = "FNroComprobanteParaCancelacion";
            Text = "FCancelacionDevolucion";
            Load += FNroComprobanteParaCancelacion_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtNroComprobante;
        private Button btnCancelar;
        private Button btnBuscar;
        private DateTimePicker dtpFecha;
        private Label lblFIltrarFecha;
        private CheckBox cbxSeleccionNroComprobante;
    }
}