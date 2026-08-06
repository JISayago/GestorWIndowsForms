namespace Presentacion.Core.Caja
{
    partial class FCajaAbrir
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
            txtMontoApertura = new TextBox();
            lblMontoApertura = new Label();
            btnAbrirCaja = new Button();
            lblUsuarioLogeado = new Label();
            lblUsuario = new Label();
            lblConfirmacion = new Label();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // txtMontoApertura
            // 
            txtMontoApertura.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtMontoApertura.Location = new Point(142, 116);
            txtMontoApertura.Name = "txtMontoApertura";
            txtMontoApertura.Size = new Size(100, 25);
            txtMontoApertura.TabIndex = 0;
            txtMontoApertura.TextChanged += txtMontoApertura_TextChanged;
            txtMontoApertura.KeyPress += txtMontoApertura_KeyPress;
            // 
            // lblMontoApertura
            // 
            lblMontoApertura.AutoSize = true;
            lblMontoApertura.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMontoApertura.Location = new Point(29, 119);
            lblMontoApertura.Name = "lblMontoApertura";
            lblMontoApertura.Size = new Size(110, 17);
            lblMontoApertura.TabIndex = 1;
            lblMontoApertura.Text = "Monto Apertura:";
            // 
            // btnAbrirCaja
            // 
            btnAbrirCaja.Location = new Point(98, 150);
            btnAbrirCaja.Name = "btnAbrirCaja";
            btnAbrirCaja.Size = new Size(95, 23);
            btnAbrirCaja.TabIndex = 2;
            btnAbrirCaja.Text = "ABRIR CAJA";
            btnAbrirCaja.UseVisualStyleBackColor = true;
            btnAbrirCaja.Click += btnAbrirCaja_Click;
            // 
            // lblUsuarioLogeado
            // 
            lblUsuarioLogeado.AutoSize = true;
            lblUsuarioLogeado.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblUsuarioLogeado.ForeColor = Color.Green;
            lblUsuarioLogeado.Location = new Point(22, 33);
            lblUsuarioLogeado.Name = "lblUsuarioLogeado";
            lblUsuarioLogeado.Size = new Size(54, 21);
            lblUsuarioLogeado.TabIndex = 4;
            lblUsuarioLogeado.Text = "label2";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblUsuario.Location = new Point(22, 9);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(134, 21);
            lblUsuario.TabIndex = 3;
            lblUsuario.Text = "Usuario logeado:";
            // 
            // lblConfirmacion
            // 
            lblConfirmacion.AutoSize = true;
            lblConfirmacion.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblConfirmacion.Location = new Point(12, 86);
            lblConfirmacion.Name = "lblConfirmacion";
            lblConfirmacion.Size = new Size(51, 21);
            lblConfirmacion.TabIndex = 5;
            lblConfirmacion.Text = "label1";
            // 
            // FCajaAbrir
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(297, 185);
            Controls.Add(lblConfirmacion);
            Controls.Add(lblUsuarioLogeado);
            Controls.Add(lblUsuario);
            Controls.Add(btnAbrirCaja);
            Controls.Add(lblMontoApertura);
            Controls.Add(txtMontoApertura);
            ForeColor = Color.FromArgb(31, 26, 43);
            Name = "FCajaAbrir";
            Text = "Caja";
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtMontoApertura;
        private Label lblMontoApertura;
        private Button btnAbrirCaja;
        private Label lblUsuarioLogeado;
        private Label lblUsuario;
        private Label lblConfirmacion;
    }
}