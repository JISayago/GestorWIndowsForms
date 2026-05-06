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
            SuspendLayout();
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Location = new Point(38, 88);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(137, 15);
            lblCodigo.TabIndex = 0;
            lblCodigo.Text = "Código de Recuperación";
            // 
            // txtCodigoRecuperacion
            // 
            txtCodigoRecuperacion.Location = new Point(38, 106);
            txtCodigoRecuperacion.Name = "txtCodigoRecuperacion";
            txtCodigoRecuperacion.Size = new Size(249, 23);
            txtCodigoRecuperacion.TabIndex = 1;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(212, 154);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(75, 23);
            btnConfirmar.TabIndex = 2;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(38, 154);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(38, 44);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(249, 23);
            txtUsuario.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(38, 26);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 4;
            label1.Text = "Usuario";
            // 
            // FCodigoRecuperacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(342, 203);
            Controls.Add(txtUsuario);
            Controls.Add(label1);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmar);
            Controls.Add(txtCodigoRecuperacion);
            Controls.Add(lblCodigo);
            Name = "FCodigoRecuperacion";
            StartPosition = FormStartPosition.CenterParent;
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
    }
}