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
            pbxLogo = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbxLogo).BeginInit();
            SuspendLayout();
            // 
            // btnIngresar
            // 
            btnIngresar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnIngresar.Location = new Point(145, 269);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(113, 39);
            btnIngresar.TabIndex = 2;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.Location = new Point(307, 269);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(113, 39);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // txtUsuario
            // 
            txtUsuario.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsuario.Location = new Point(180, 156);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(297, 33);
            txtUsuario.TabIndex = 0;
            txtUsuario.Text = "admin";
            // 
            // txtPass
            // 
            txtPass.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPass.Location = new Point(180, 207);
            txtPass.Name = "txtPass";
            txtPass.PasswordChar = '*';
            txtPass.Size = new Size(297, 33);
            txtPass.TabIndex = 1;
            txtPass.Text = "Admin123";
            txtPass.UseSystemPasswordChar = true;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsuario.Location = new Point(85, 168);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(81, 25);
            lblUsuario.TabIndex = 4;
            lblUsuario.Text = "Usuario";
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPass.Location = new Point(58, 213);
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
            lnklblRecuperacionContra.Location = new Point(411, 325);
            lnklblRecuperacionContra.Name = "lnklblRecuperacionContra";
            lnklblRecuperacionContra.Size = new Size(121, 13);
            lnklblRecuperacionContra.TabIndex = 4;
            lnklblRecuperacionContra.TabStop = true;
            lnklblRecuperacionContra.Text = "Recuperar Contraseña";
            lnklblRecuperacionContra.LinkClicked += lnklblRecuperacionContra_LinkClicked;
            // 
            // lnklblCodigoRec
            // 
            lnklblCodigoRec.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lnklblCodigoRec.AutoSize = true;
            lnklblCodigoRec.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnklblCodigoRec.Location = new Point(12, 325);
            lnklblCodigoRec.Name = "lnklblCodigoRec";
            lnklblCodigoRec.Size = new Size(174, 13);
            lnklblCodigoRec.TabIndex = 5;
            lnklblCodigoRec.TabStop = true;
            lnklblCodigoRec.Text = "Ingresar código de recuperación";
            lnklblCodigoRec.LinkClicked += lnklblCodigoRec_LinkClicked;
            // 
            // pbxLogo
            // 
            pbxLogo.Location = new Point(38, 12);
            pbxLogo.Margin = new Padding(50, 3, 3, 3);
            pbxLogo.MaximumSize = new Size(472, 118);
            pbxLogo.MinimumSize = new Size(472, 118);
            pbxLogo.Name = "pbxLogo";
            pbxLogo.Size = new Size(472, 118);
            pbxLogo.TabIndex = 6;
            pbxLogo.TabStop = false;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(546, 349);
            Controls.Add(pbxLogo);
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
            Load += LoginForm_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbxLogo).EndInit();
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
        private PictureBox pbxLogo;
    }
}