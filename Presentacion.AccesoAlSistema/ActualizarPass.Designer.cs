namespace Presentacion.AccesoAlSistema
{
    partial class ActualizarPass
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
            lblConfirmPass = new Label();
            lblNuevaPass = new Label();
            txtConfirmarPass = new TextBox();
            txtNuevaPass = new TextBox();
            btnCancelar = new Button();
            btnGuardarNuevaPass = new Button();
            SuspendLayout();
            // 
            // lblConfirmPass
            // 
            lblConfirmPass.AutoSize = true;
            lblConfirmPass.Location = new Point(37, 95);
            lblConfirmPass.Name = "lblConfirmPass";
            lblConfirmPass.Size = new Size(124, 15);
            lblConfirmPass.TabIndex = 11;
            lblConfirmPass.Text = "Confirmar Contraseña";
            // 
            // lblNuevaPass
            // 
            lblNuevaPass.AutoSize = true;
            lblNuevaPass.Location = new Point(37, 25);
            lblNuevaPass.Name = "lblNuevaPass";
            lblNuevaPass.Size = new Size(104, 15);
            lblNuevaPass.TabIndex = 10;
            lblNuevaPass.Text = "Nueva Contraseña";
            // 
            // txtConfirmarPass
            // 
            txtConfirmarPass.Location = new Point(36, 113);
            txtConfirmarPass.Name = "txtConfirmarPass";
            txtConfirmarPass.PasswordChar = '*';
            txtConfirmarPass.Size = new Size(351, 23);
            txtConfirmarPass.TabIndex = 9;
            txtConfirmarPass.UseSystemPasswordChar = true;
            // 
            // txtNuevaPass
            // 
            txtNuevaPass.Location = new Point(36, 49);
            txtNuevaPass.Name = "txtNuevaPass";
            txtNuevaPass.PasswordChar = '*';
            txtNuevaPass.Size = new Size(351, 23);
            txtNuevaPass.TabIndex = 8;
            txtNuevaPass.UseSystemPasswordChar = true;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(226, 158);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(113, 39);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnGuardarNuevaPass
            // 
            btnGuardarNuevaPass.Location = new Point(60, 158);
            btnGuardarNuevaPass.Name = "btnGuardarNuevaPass";
            btnGuardarNuevaPass.Size = new Size(113, 39);
            btnGuardarNuevaPass.TabIndex = 6;
            btnGuardarNuevaPass.Text = "Guardar";
            btnGuardarNuevaPass.UseVisualStyleBackColor = true;
            btnGuardarNuevaPass.Click += btnGuardarNuevaPass_Click;
            // 
            // ActualizarPass
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(408, 213);
            Controls.Add(lblConfirmPass);
            Controls.Add(lblNuevaPass);
            Controls.Add(txtConfirmarPass);
            Controls.Add(txtNuevaPass);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardarNuevaPass);
            MaximumSize = new Size(424, 252);
            MinimumSize = new Size(424, 252);
            Name = "ActualizarPass";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ActualizarPass";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblConfirmPass;
        private Label lblNuevaPass;
        private TextBox txtConfirmarPass;
        private TextBox txtNuevaPass;
        private Button btnCancelar;
        private Button btnGuardarNuevaPass;
    }
}