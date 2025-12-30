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
            SuspendLayout();
            // 
            // txtMontoApertura
            // 
            txtMontoApertura.Location = new Point(123, 68);
            txtMontoApertura.Name = "txtMontoApertura";
            txtMontoApertura.Size = new Size(100, 23);
            txtMontoApertura.TabIndex = 0;
            // 
            // lblMontoApertura
            // 
            lblMontoApertura.AutoSize = true;
            lblMontoApertura.Location = new Point(12, 71);
            lblMontoApertura.Name = "lblMontoApertura";
            lblMontoApertura.Size = new Size(92, 15);
            lblMontoApertura.TabIndex = 1;
            lblMontoApertura.Text = "Monto Apertura";
            // 
            // btnAbrirCaja
            // 
            btnAbrirCaja.Location = new Point(71, 128);
            btnAbrirCaja.Name = "btnAbrirCaja";
            btnAbrirCaja.Size = new Size(90, 23);
            btnAbrirCaja.TabIndex = 2;
            btnAbrirCaja.Text = "ABRIR CAJA";
            btnAbrirCaja.UseVisualStyleBackColor = true;
            btnAbrirCaja.Click += btnAbrirCaja_Click;
            // 
            // lblUsuarioLogeado
            // 
            lblUsuarioLogeado.AutoSize = true;
            lblUsuarioLogeado.Location = new Point(123, 27);
            lblUsuarioLogeado.Name = "lblUsuarioLogeado";
            lblUsuarioLogeado.Size = new Size(38, 15);
            lblUsuarioLogeado.TabIndex = 4;
            lblUsuarioLogeado.Text = "label2";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(8, 27);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(96, 15);
            lblUsuario.TabIndex = 3;
            lblUsuario.Text = "Usuario Logeado";
            // 
            // FCajaAbrir
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(245, 172);
            Controls.Add(lblUsuarioLogeado);
            Controls.Add(lblUsuario);
            Controls.Add(btnAbrirCaja);
            Controls.Add(lblMontoApertura);
            Controls.Add(txtMontoApertura);
            Name = "FCajaAbrir";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FCajaAbrir";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtMontoApertura;
        private Label lblMontoApertura;
        private Button btnAbrirCaja;
        private Label lblUsuarioLogeado;
        private Label lblUsuario;
    }
}