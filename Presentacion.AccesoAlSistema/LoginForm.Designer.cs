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
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbxLogo).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // btnIngresar
            // 
            btnIngresar.Anchor = AnchorStyles.None;
            btnIngresar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnIngresar.Location = new Point(113, 14);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(113, 39);
            btnIngresar.TabIndex = 2;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.None;
            btnCancelar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.Location = new Point(258, 14);
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
            txtUsuario.Location = new Point(136, 19);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(264, 33);
            txtUsuario.TabIndex = 0;
            txtUsuario.Text = "admin";
            // 
            // txtPass
            // 
            txtPass.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPass.Location = new Point(136, 59);
            txtPass.Name = "txtPass";
            txtPass.PasswordChar = '*';
            txtPass.Size = new Size(264, 33);
            txtPass.TabIndex = 1;
            txtPass.Text = "Admin123";
            txtPass.UseSystemPasswordChar = true;
            // 
            // lblUsuario
            // 
            lblUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsuario.Location = new Point(11, 16);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(81, 40);
            lblUsuario.TabIndex = 4;
            lblUsuario.Text = "Usuario";
            lblUsuario.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPass
            // 
            lblPass.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lblPass.AutoSize = true;
            lblPass.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPass.Location = new Point(11, 56);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(113, 41);
            lblPass.TabIndex = 5;
            lblPass.Text = "Contraseña";
            lblPass.TextAlign = ContentAlignment.MiddleCenter;
            lblPass.Click += lblPass_Click;
            // 
            // lnklblRecuperacionContra
            // 
            lnklblRecuperacionContra.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lnklblRecuperacionContra.AutoSize = true;
            lnklblRecuperacionContra.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnklblRecuperacionContra.Location = new Point(361, 1);
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
            lnklblCodigoRec.Location = new Point(3, 1);
            lnklblCodigoRec.Name = "lnklblCodigoRec";
            lnklblCodigoRec.Size = new Size(174, 13);
            lnklblCodigoRec.TabIndex = 5;
            lnklblCodigoRec.TabStop = true;
            lnklblCodigoRec.Text = "Ingresar código de recuperación";
            lnklblCodigoRec.LinkClicked += lnklblCodigoRec_LinkClicked;
            // 
            // pbxLogo
            // 
            pbxLogo.Anchor = AnchorStyles.None;
            pbxLogo.Location = new Point(37, 23);
            pbxLogo.Name = "pbxLogo";
            pbxLogo.Padding = new Padding(8);
            pbxLogo.Size = new Size(416, 102);
            pbxLogo.TabIndex = 6;
            pbxLogo.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 0, 3);
            tableLayoutPanel1.Controls.Add(pbxLogo, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 2);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 42.1052628F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 31.5789471F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 21.0526314F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.263158F));
            tableLayoutPanel1.Size = new Size(491, 354);
            tableLayoutPanel1.TabIndex = 7;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(lnklblCodigoRec, 0, 0);
            tableLayoutPanel4.Controls.Add(lnklblRecuperacionContra, 1, 0);
            tableLayoutPanel4.Location = new Point(3, 337);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(485, 14);
            tableLayoutPanel4.TabIndex = 8;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 4;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel3.Controls.Add(btnCancelar, 2, 0);
            tableLayoutPanel3.Controls.Add(btnIngresar, 1, 0);
            tableLayoutPanel3.Location = new Point(3, 263);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(485, 68);
            tableLayoutPanel3.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31.17506F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68.82494F));
            tableLayoutPanel2.Controls.Add(lblPass, 0, 2);
            tableLayoutPanel2.Controls.Add(txtPass, 1, 2);
            tableLayoutPanel2.Controls.Add(txtUsuario, 1, 1);
            tableLayoutPanel2.Controls.Add(lblUsuario, 0, 1);
            tableLayoutPanel2.Location = new Point(37, 152);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.Padding = new Padding(8);
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            tableLayoutPanel2.Size = new Size(417, 105);
            tableLayoutPanel2.TabIndex = 8;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(515, 376);
            Controls.Add(tableLayoutPanel1);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ingreso al Sistema";
            Load += LoginForm_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbxLogo).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
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
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel4;
    }
}