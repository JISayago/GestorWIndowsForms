namespace Presentacion.Core.Caja
{
    partial class FCaja
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
            btnConsultarMovimientos = new Button();
            btnConsultarCajas = new Button();
            btnCerrarCaja = new Button();
            btnAbrirCaja = new Button();
            lblEstadoCaja = new Label();
            SuspendLayout();
            // 
            // btnConsultarMovimientos
            // 
            btnConsultarMovimientos.Location = new Point(33, 133);
            btnConsultarMovimientos.Name = "btnConsultarMovimientos";
            btnConsultarMovimientos.Size = new Size(193, 24);
            btnConsultarMovimientos.TabIndex = 7;
            btnConsultarMovimientos.Text = "Consultar Movimientos";
            btnConsultarMovimientos.UseVisualStyleBackColor = true;
            // 
            // btnConsultarCajas
            // 
            btnConsultarCajas.Location = new Point(33, 103);
            btnConsultarCajas.Name = "btnConsultarCajas";
            btnConsultarCajas.Size = new Size(193, 24);
            btnConsultarCajas.TabIndex = 6;
            btnConsultarCajas.Text = "Consulta Cajas";
            btnConsultarCajas.UseVisualStyleBackColor = true;
            // 
            // btnCerrarCaja
            // 
            btnCerrarCaja.Location = new Point(131, 74);
            btnCerrarCaja.Name = "btnCerrarCaja";
            btnCerrarCaja.Size = new Size(95, 23);
            btnCerrarCaja.TabIndex = 5;
            btnCerrarCaja.Text = "Cerrar Caja";
            btnCerrarCaja.UseVisualStyleBackColor = true;
            btnCerrarCaja.Click += btnCerrarCaja_Click;
            // 
            // btnAbrirCaja
            // 
            btnAbrirCaja.Location = new Point(33, 74);
            btnAbrirCaja.Name = "btnAbrirCaja";
            btnAbrirCaja.Size = new Size(95, 23);
            btnAbrirCaja.TabIndex = 4;
            btnAbrirCaja.Text = "Abrir Caja";
            btnAbrirCaja.UseVisualStyleBackColor = true;
            btnAbrirCaja.Click += btnAbrirCaja_Click;
            // 
            // lblEstadoCaja
            // 
            lblEstadoCaja.AutoSize = true;
            lblEstadoCaja.Location = new Point(92, 37);
            lblEstadoCaja.Name = "lblEstadoCaja";
            lblEstadoCaja.Size = new Size(65, 15);
            lblEstadoCaja.TabIndex = 8;
            lblEstadoCaja.Text = "estadoCaja";
            // 
            // FCaja
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(263, 187);
            Controls.Add(lblEstadoCaja);
            Controls.Add(btnConsultarMovimientos);
            Controls.Add(btnConsultarCajas);
            Controls.Add(btnCerrarCaja);
            Controls.Add(btnAbrirCaja);
            Name = "FCaja";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FCaja";
            Load += FCaja_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnConsultarMovimientos;
        private Button btnConsultarCajas;
        private Button btnCerrarCaja;
        private Button btnAbrirCaja;
        private Label lblEstadoCaja;
    }
}