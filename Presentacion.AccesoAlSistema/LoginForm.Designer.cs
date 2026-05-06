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
            lnklblCodigoRec = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // btnIngresar
            // 
            btnIngresar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnIngresar.Location = new Point(135, 144);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(113, 39);
            btnIngresar.TabIndex = 0;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.Location = new Point(297, 144);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(113, 39);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // txtUsuario
            // 
            txtUsuario.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsuario.Location = new Point(170, 31);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(297, 33);
            txtUsuario.TabIndex = 2;
            txtUsuario.Text = "Admin";
            // 
            // txtPass
            // 
            txtPass.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPass.Location = new Point(170, 82);
            txtPass.Name = "txtPass";
            txtPass.PasswordChar = '*';
            txtPass.Size = new Size(297, 33);
            txtPass.TabIndex = 3;
            txtPass.Text = "Admin123";
            txtPass.UseSystemPasswordChar = true;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsuario.Location = new Point(75, 43);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(81, 25);
            lblUsuario.TabIndex = 4;
            lblUsuario.Text = "Usuario";
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPass.Location = new Point(48, 88);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(113, 25);
            lblPass.TabIndex = 5;
            lblPass.Text = "Contraseña";
            lblPass.Click += lblPass_Click;
            // 
            // lnklblRecuperacionContra
            // 
            lnklblRecuperacionContra.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lnklblRecuperacionContra.AutoSize = true;
            lnklblRecuperacionContra.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnklblRecuperacionContra.Location = new Point(411, 207);
            lnklblRecuperacionContra.Name = "lnklblRecuperacionContra";
            lnklblRecuperacionContra.Size = new Size(121, 13);
            lnklblRecuperacionContra.TabIndex = 6;
            lnklblRecuperacionContra.TabStop = true;
            lnklblRecuperacionContra.Text = "Recuperar Contraseña";
            lnklblRecuperacionContra.LinkClicked += lnklblRecuperacionContra_LinkClicked;
            // 
            // lnklblCodigoRec
            // 
            lnklblCodigoRec.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lnklblCodigoRec.AutoSize = true;
            lnklblCodigoRec.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnklblCodigoRec.Location = new Point(12, 207);
            lnklblCodigoRec.Name = "lnklblCodigoRec";
            lnklblCodigoRec.Size = new Size(174, 13);
            lnklblCodigoRec.TabIndex = 7;
            lnklblCodigoRec.TabStop = true;
            lnklblCodigoRec.Text = "Ingresar código de recuperación";
            lnklblCodigoRec.LinkClicked += lnklblCodigoRec_LinkClicked;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(546, 231);
            Controls.Add(lnklblCodigoRec);
            Controls.Add(lnklblRecuperacionContra);
            Controls.Add(lblPass);
            Controls.Add(lblUsuario);
            Controls.Add(txtPass);
            Controls.Add(txtUsuario);
            Controls.Add(btnCancelar);
            Controls.Add(btnIngresar);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ingreso al Sistema";
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
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
        private LinkLabel lnklblCodigoRec;
    }
}