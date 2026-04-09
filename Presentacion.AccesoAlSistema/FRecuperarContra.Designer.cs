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
            txtDniLegajo.Location = new Point(82, 110);
            txtDniLegajo.Name = "txtDniLegajo";
            txtDniLegajo.Size = new Size(241, 23);
            txtDniLegajo.TabIndex = 0;
            // 
            // lblDniLegajo
            // 
            lblDniLegajo.AutoSize = true;
            lblDniLegajo.Location = new Point(82, 92);
            lblDniLegajo.Name = "lblDniLegajo";
            lblDniLegajo.Size = new Size(71, 15);
            lblDniLegajo.TabIndex = 1;
            lblDniLegajo.Text = "Dni / Legajo";
            // 
            // btnRecuperar
            // 
            btnRecuperar.Location = new Point(94, 152);
            btnRecuperar.Name = "btnRecuperar";
            btnRecuperar.Size = new Size(75, 23);
            btnRecuperar.TabIndex = 2;
            btnRecuperar.Text = "Recuperar";
            btnRecuperar.UseVisualStyleBackColor = true;
            btnRecuperar.Click += btnRecuperar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(233, 152);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(82, 26);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(47, 15);
            lblUsuario.TabIndex = 5;
            lblUsuario.Text = "Usuario";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(82, 44);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(241, 23);
            txtUsuario.TabIndex = 4;
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
            Text = "FRecuperarContra";
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