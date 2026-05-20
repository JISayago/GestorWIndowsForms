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
            lblSaldoCaja = new Label();
            SuspendLayout();
            // 
            // btnConsultarMovimientos
            // 
            btnConsultarMovimientos.Location = new Point(63, 218);
            btnConsultarMovimientos.Name = "btnConsultarMovimientos";
            btnConsultarMovimientos.Size = new Size(218, 42);
            btnConsultarMovimientos.TabIndex = 7;
            btnConsultarMovimientos.Text = "Consultar Movimientos";
            btnConsultarMovimientos.UseVisualStyleBackColor = true;
            btnConsultarMovimientos.Click += btnConsultarMovimientos_Click;
            // 
            // btnConsultarCajas
            // 
            btnConsultarCajas.Location = new Point(63, 168);
            btnConsultarCajas.Name = "btnConsultarCajas";
            btnConsultarCajas.Size = new Size(218, 42);
            btnConsultarCajas.TabIndex = 6;
            btnConsultarCajas.Text = "Consulta Cajas";
            btnConsultarCajas.UseVisualStyleBackColor = true;
            btnConsultarCajas.Click += btnConsultarCajas_Click;
            // 
            // btnCerrarCaja
            // 
            btnCerrarCaja.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCerrarCaja.Location = new Point(175, 106);
            btnCerrarCaja.Name = "btnCerrarCaja";
            btnCerrarCaja.Size = new Size(106, 54);
            btnCerrarCaja.TabIndex = 5;
            btnCerrarCaja.Text = "Cerrar Caja";
            btnCerrarCaja.UseVisualStyleBackColor = true;
            btnCerrarCaja.Click += btnCerrarCaja_Click;
            // 
            // btnAbrirCaja
            // 
            btnAbrirCaja.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAbrirCaja.Location = new Point(63, 106);
            btnAbrirCaja.Name = "btnAbrirCaja";
            btnAbrirCaja.Size = new Size(106, 54);
            btnAbrirCaja.TabIndex = 4;
            btnAbrirCaja.Text = "Abrir Caja";
            btnAbrirCaja.UseVisualStyleBackColor = true;
            btnAbrirCaja.Click += btnAbrirCaja_Click;
            // 
            // lblEstadoCaja
            // 
            lblEstadoCaja.AutoSize = true;
            lblEstadoCaja.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEstadoCaja.Location = new Point(104, 9);
            lblEstadoCaja.Name = "lblEstadoCaja";
            lblEstadoCaja.Size = new Size(108, 25);
            lblEstadoCaja.TabIndex = 8;
            lblEstadoCaja.Text = "estadoCaja";
            // 
            // lblSaldoCaja
            // 
            lblSaldoCaja.AutoSize = true;
            lblSaldoCaja.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSaldoCaja.Location = new Point(117, 52);
            lblSaldoCaja.Name = "lblSaldoCaja";
            lblSaldoCaja.Size = new Size(77, 21);
            lblSaldoCaja.TabIndex = 9;
            lblSaldoCaja.Text = "saldocaja";
            // 
            // FCaja
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(344, 301);
            Controls.Add(lblSaldoCaja);
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
        private Label lblSaldoCaja;
    }
}