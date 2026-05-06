namespace Presentacion.AccesoAlSistema
{
    partial class FRecuperarContra
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
            txtDniLegajo = new TextBox();
            lblDniLegajo = new Label();
            btnRecuperar = new Button();
            btnCancelar = new Button();
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            SuspendLayout();
            // 
            // txtDniLegajo
            // 
            txtDniLegajo.Font = new Font("Segoe UI", 12F);
            txtDniLegajo.Location = new Point(121, 95);
            txtDniLegajo.Name = "txtDniLegajo";
            txtDniLegajo.Size = new Size(241, 29);
            txtDniLegajo.TabIndex = 4;
            // 
            // lblDniLegajo
            // 
            lblDniLegajo.AutoSize = true;
            lblDniLegajo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDniLegajo.Location = new Point(12, 98);
            lblDniLegajo.Name = "lblDniLegajo";
            lblDniLegajo.Size = new Size(103, 21);
            lblDniLegajo.TabIndex = 1;
            lblDniLegajo.Text = "Dni / Legajo";
            // 
            // btnRecuperar
            // 
            btnRecuperar.Location = new Point(82, 143);
            btnRecuperar.Name = "btnRecuperar";
            btnRecuperar.Size = new Size(84, 40);
            btnRecuperar.TabIndex = 2;
            btnRecuperar.Text = "Recuperar";
            btnRecuperar.UseVisualStyleBackColor = true;
            btnRecuperar.Click += btnRecuperar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(256, 143);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(81, 40);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblUsuario.Location = new Point(46, 53);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(69, 21);
            lblUsuario.TabIndex = 5;
            lblUsuario.Text = "Usuario";
            // 
            // txtUsuario
            // 
            txtUsuario.Font = new Font("Segoe UI", 12F);
            txtUsuario.Location = new Point(121, 50);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(241, 29);
            txtUsuario.TabIndex = 0;
            // 
            // FRecuperarContra
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(428, 204);
            Controls.Add(lblUsuario);
            Controls.Add(txtUsuario);
            Controls.Add(btnCancelar);
            Controls.Add(btnRecuperar);
            Controls.Add(lblDniLegajo);
            Controls.Add(txtDniLegajo);
            Name = "FRecuperarContra";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FRecuperarContra";
            Load += FRecuperarContra_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtDniLegajo;
        private Label lblDniLegajo;
        private Button btnRecuperar;
        private Button btnCancelar;
        private Label lblUsuario;
        private TextBox txtUsuario;
    }
}