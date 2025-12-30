namespace Presentacion.Core.Caja
{
    partial class FCerrarCaja
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
            lblUsuarioLogeado = new Label();
            lblUsuario = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // lblUsuarioLogeado
            // 
            lblUsuarioLogeado.AutoSize = true;
            lblUsuarioLogeado.Location = new Point(127, 26);
            lblUsuarioLogeado.Name = "lblUsuarioLogeado";
            lblUsuarioLogeado.Size = new Size(38, 15);
            lblUsuarioLogeado.TabIndex = 6;
            lblUsuarioLogeado.Text = "label2";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(12, 26);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(96, 15);
            lblUsuario.TabIndex = 5;
            lblUsuario.Text = "Usuario Logeado";
            // 
            // button1
            // 
            button1.Location = new Point(70, 118);
            button1.Name = "button1";
            button1.Size = new Size(95, 23);
            button1.TabIndex = 7;
            button1.Text = "CERRAR CAJA";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FCerrarCaja
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(245, 172);
            Controls.Add(button1);
            Controls.Add(lblUsuarioLogeado);
            Controls.Add(lblUsuario);
            Name = "FCerrarCaja";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FCerrarCaja";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblUsuarioLogeado;
        private Label lblUsuario;
        private Button button1;
    }
}