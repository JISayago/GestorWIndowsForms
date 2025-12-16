namespace Presentacion.Core.Caja
{
    partial class FCajaConsulta
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
            btnAbrirCaja = new Button();
            btnCerrarCaja = new Button();
            btnConsultarCajas = new Button();
            btnConsultarMovimientos = new Button();
            SuspendLayout();
            // 
            // btnAbrirCaja
            // 
            btnAbrirCaja.Location = new Point(47, 144);
            btnAbrirCaja.Name = "btnAbrirCaja";
            btnAbrirCaja.Size = new Size(95, 23);
            btnAbrirCaja.TabIndex = 0;
            btnAbrirCaja.Text = "Abrir Caja";
            btnAbrirCaja.UseVisualStyleBackColor = true;
            btnAbrirCaja.Click += btnAbrirCaja_Click;
            // 
            // btnCerrarCaja
            // 
            btnCerrarCaja.Location = new Point(145, 144);
            btnCerrarCaja.Name = "btnCerrarCaja";
            btnCerrarCaja.Size = new Size(95, 23);
            btnCerrarCaja.TabIndex = 1;
            btnCerrarCaja.Text = "Cerrar Caja";
            btnCerrarCaja.UseVisualStyleBackColor = true;
            // 
            // btnConsultarCajas
            // 
            btnConsultarCajas.Location = new Point(47, 173);
            btnConsultarCajas.Name = "btnConsultarCajas";
            btnConsultarCajas.Size = new Size(193, 24);
            btnConsultarCajas.TabIndex = 2;
            btnConsultarCajas.Text = "Consulta Cajas";
            btnConsultarCajas.UseVisualStyleBackColor = true;
            // 
            // btnConsultarMovimientos
            // 
            btnConsultarMovimientos.Location = new Point(47, 203);
            btnConsultarMovimientos.Name = "btnConsultarMovimientos";
            btnConsultarMovimientos.Size = new Size(193, 24);
            btnConsultarMovimientos.TabIndex = 3;
            btnConsultarMovimientos.Text = "Consultar Movimientos";
            btnConsultarMovimientos.UseVisualStyleBackColor = true;
            // 
            // FCajaConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(295, 269);
            Controls.Add(btnConsultarMovimientos);
            Controls.Add(btnConsultarCajas);
            Controls.Add(btnCerrarCaja);
            Controls.Add(btnAbrirCaja);
            Name = "FCajaConsulta";
            Text = "FCajaConsulta";
            ResumeLayout(false);
        }

        #endregion

        private Button btnAbrirCaja;
        private Button btnCerrarCaja;
        private Button btnConsultarCajas;
        private Button btnConsultarMovimientos;
    }
}