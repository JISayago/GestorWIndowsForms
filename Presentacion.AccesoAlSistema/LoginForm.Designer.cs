namespace Presentacion.AccesoAlSistema
{
    partial class LoginForm
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
            btnIngresar = new Button();
            btnCancelar = new Button();
            txtUsuario = new TextBox();
            txtPass = new TextBox();
            lblUsuario = new Label();
            lblPass = new Label();
            lnklblRecuperacionContra = new LinkLabel();
            SuspendLayout();
            // 
            // btnIngresar
            // 
            btnIngresar.Location = new Point(71, 137);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(113, 39);
            btnIngresar.TabIndex = 0;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(237, 137);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(113, 39);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(42, 32);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(351, 23);
            txtUsuario.TabIndex = 2;
            txtUsuario.Text = "Admin";
            // 
            // txtPass
            // 
            txtPass.Location = new Point(42, 96);
            txtPass.Name = "txtPass";
            txtPass.PasswordChar = '*';
            txtPass.Size = new Size(351, 23);
            txtPass.TabIndex = 3;
            txtPass.Text = "Admin123";
            txtPass.UseSystemPasswordChar = true;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(43, 8);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(47, 15);
            lblUsuario.TabIndex = 4;
            lblUsuario.Text = "Usuario";
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Location = new Point(43, 78);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(67, 15);
            lblPass.TabIndex = 5;
            lblPass.Text = "Contraseña";
            lblPass.Click += lblPass_Click;
            // 
            // lnklblRecuperacionContra
            // 
            lnklblRecuperacionContra.AutoSize = true;
            lnklblRecuperacionContra.Location = new Point(149, 187);
            lnklblRecuperacionContra.Name = "lnklblRecuperacionContra";
            lnklblRecuperacionContra.Size = new Size(123, 15);
            lnklblRecuperacionContra.TabIndex = 6;
            lnklblRecuperacionContra.TabStop = true;
            lnklblRecuperacionContra.Text = "Recuperar Contraseña";
            lnklblRecuperacionContra.LinkClicked += lnklblRecuperacionContra_LinkClicked;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(422, 211);
            Controls.Add(lnklblRecuperacionContra);
            Controls.Add(lblPass);
            Controls.Add(lblUsuario);
            Controls.Add(txtPass);
            Controls.Add(txtUsuario);
            Controls.Add(btnCancelar);
            Controls.Add(btnIngresar);
            MaximizeBox = false;
            MaximumSize = new Size(438, 250);
            MinimizeBox = false;
            MinimumSize = new Size(438, 250);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ingreso al Sistema";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnIngresar;
        private Button btnCancelar;
        private TextBox txtUsuario;
        private TextBox txtPass;
        private Label lblUsuario;
        private Label lblPass;
        private LinkLabel lnklblRecuperacionContra;
    }
}