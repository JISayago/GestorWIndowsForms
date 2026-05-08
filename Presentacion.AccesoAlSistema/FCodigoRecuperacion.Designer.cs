namespace Presentacion.AccesoAlSistema
{
    partial class FCodigoRecuperacion
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
            lblCodigo = new Label();
            txtCodigoRecuperacion = new TextBox();
            btnConfirmar = new Button();
            btnCancelar = new Button();
            txtUsuario = new TextBox();
            label1 = new Label();
            lblTitulo = new Label();
            SuspendLayout();
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCodigo.Location = new Point(76, 137);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(77, 25);
            lblCodigo.TabIndex = 0;
            lblCodigo.Text = "Código";
            // 
            // txtCodigoRecuperacion
            // 
            txtCodigoRecuperacion.Font = new Font("Segoe UI", 14.25F);
            txtCodigoRecuperacion.Location = new Point(180, 129);
            txtCodigoRecuperacion.Name = "txtCodigoRecuperacion";
            txtCodigoRecuperacion.Size = new Size(249, 33);
            txtCodigoRecuperacion.TabIndex = 1;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConfirmar.Location = new Point(76, 190);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(113, 39);
            btnConfirmar.TabIndex = 2;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.Location = new Point(307, 190);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(122, 39);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // txtUsuario
            // 
            txtUsuario.Font = new Font("Segoe UI", 14.25F);
            txtUsuario.Location = new Point(180, 69);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(249, 33);
            txtUsuario.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(76, 77);
            label1.Name = "label1";
            label1.Size = new Size(81, 25);
            label1.TabIndex = 4;
            label1.Text = "Usuario";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(130, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(252, 30);
            lblTitulo.TabIndex = 6;
            lblTitulo.Text = "Código de Recuperación";
            // 
            // FCodigoRecuperacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(519, 257);
            Controls.Add(lblTitulo);
            Controls.Add(txtUsuario);
            Controls.Add(label1);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmar);
            Controls.Add(txtCodigoRecuperacion);
            Controls.Add(lblCodigo);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FCodigoRecuperacion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FCodigoRecuperacion";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCodigo;
        private TextBox txtCodigoRecuperacion;
        private Button btnConfirmar;
        private Button btnCancelar;
        private TextBox txtUsuario;
        private Label label1;
        private Label lblTitulo;
    }
}