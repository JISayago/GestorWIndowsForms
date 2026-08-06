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
            lblConfirmPass.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblConfirmPass.Location = new Point(22, 86);
            lblConfirmPass.Name = "lblConfirmPass";
            lblConfirmPass.Size = new Size(177, 21);
            lblConfirmPass.TabIndex = 11;
            lblConfirmPass.Text = "Confirmar Contraseña";
            // 
            // lblNuevaPass
            // 
            lblNuevaPass.AutoSize = true;
            lblNuevaPass.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNuevaPass.Location = new Point(48, 34);
            lblNuevaPass.Name = "lblNuevaPass";
            lblNuevaPass.Size = new Size(150, 21);
            lblNuevaPass.TabIndex = 10;
            lblNuevaPass.Text = "Nueva Contraseña";
            // 
            // txtConfirmarPass
            // 
            txtConfirmarPass.Font = new Font("Segoe UI", 12F);
            txtConfirmarPass.Location = new Point(214, 78);
            txtConfirmarPass.Name = "txtConfirmarPass";
            txtConfirmarPass.PasswordChar = '*';
            txtConfirmarPass.Size = new Size(315, 29);
            txtConfirmarPass.TabIndex = 9;
            txtConfirmarPass.UseSystemPasswordChar = true;
            // 
            // txtNuevaPass
            // 
            txtNuevaPass.Font = new Font("Segoe UI", 12F);
            txtNuevaPass.Location = new Point(214, 31);
            txtNuevaPass.Name = "txtNuevaPass";
            txtNuevaPass.PasswordChar = '*';
            txtNuevaPass.Size = new Size(315, 29);
            txtNuevaPass.TabIndex = 8;
            txtNuevaPass.UseSystemPasswordChar = true;
            // 
            // btnCancelar
            // 
            btnCancelar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnCancelar.Location = new Point(286, 135);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(113, 39);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnGuardarNuevaPass
            // 
            btnGuardarNuevaPass.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnGuardarNuevaPass.Location = new Point(120, 135);
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
            ClientSize = new Size(560, 198);
            Controls.Add(lblConfirmPass);
            Controls.Add(lblNuevaPass);
            Controls.Add(txtConfirmarPass);
            Controls.Add(txtNuevaPass);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardarNuevaPass);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ActualizarPass";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Actualizar Contraseña";
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