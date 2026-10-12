namespace Stockeate
{
    partial class FActivacion
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDescripcion;

        private System.Windows.Forms.Label lblInstallation;
        private System.Windows.Forms.TextBox txtInstallationId;

        private System.Windows.Forms.Label lblHardwareFingerprint;
        private System.Windows.Forms.TextBox txtHardwareFingerprint;

        private System.Windows.Forms.Button btnCopiarId;
        private System.Windows.Forms.Button btnGuardarArchivo;
        private System.Windows.Forms.Button btnCargarLicencia;
        private System.Windows.Forms.Button btnCerrar;

        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.Label lblEstado;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FActivacion));
            lblTitulo = new Label();
            lblDescripcion = new Label();
            lblInstallation = new Label();
            txtInstallationId = new TextBox();
            lblHardwareFingerprint = new Label();
            txtHardwareFingerprint = new TextBox();
            btnCopiarId = new Button();
            btnGuardarArchivo = new Button();
            btnCargarLicencia = new Button();
            btnCerrar = new Button();
            lblAyuda = new Label();
            lblEstado = new Label();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(67, 20, 135);
            lblTitulo.Location = new Point(30, 25);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(620, 45);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Activación de Stockeate";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDescripcion
            // 
            lblDescripcion.Font = new Font("Segoe UI", 10F);
            lblDescripcion.ForeColor = Color.FromArgb(45, 45, 45);
            lblDescripcion.Location = new Point(30, 85);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(620, 90);
            lblDescripcion.TabIndex = 1;
            // 
            // lblInstallation
            // 
            lblInstallation.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblInstallation.ForeColor = Color.FromArgb(35, 35, 35);
            lblInstallation.Location = new Point(30, 185);
            lblInstallation.Name = "lblInstallation";
            lblInstallation.Size = new Size(620, 25);
            lblInstallation.TabIndex = 2;
            lblInstallation.Text = "Identificador de Instalación:";
            // 
            // txtInstallationId
            // 
            txtInstallationId.BackColor = Color.FromArgb(245, 245, 245);
            txtInstallationId.BorderStyle = BorderStyle.FixedSingle;
            txtInstallationId.Font = new Font("Segoe UI", 10F);
            txtInstallationId.Location = new Point(30, 215);
            txtInstallationId.Name = "txtInstallationId";
            txtInstallationId.ReadOnly = true;
            txtInstallationId.Size = new Size(500, 25);
            txtInstallationId.TabIndex = 0;
            // 
            // lblHardwareFingerprint
            // 
            lblHardwareFingerprint.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHardwareFingerprint.ForeColor = Color.FromArgb(35, 35, 35);
            lblHardwareFingerprint.Location = new Point(30, 260);
            lblHardwareFingerprint.Name = "lblHardwareFingerprint";
            lblHardwareFingerprint.Size = new Size(620, 25);
            lblHardwareFingerprint.TabIndex = 3;
            lblHardwareFingerprint.Text = "Dato de Pc:";
            // 
            // txtHardwareFingerprint
            // 
            txtHardwareFingerprint.BackColor = Color.FromArgb(245, 245, 245);
            txtHardwareFingerprint.BorderStyle = BorderStyle.FixedSingle;
            txtHardwareFingerprint.CharacterCasing = CharacterCasing.Upper;
            txtHardwareFingerprint.Font = new Font("Consolas", 9F);
            txtHardwareFingerprint.Location = new Point(30, 290);
            txtHardwareFingerprint.Name = "txtHardwareFingerprint";
            txtHardwareFingerprint.ReadOnly = true;
            txtHardwareFingerprint.Size = new Size(620, 22);
            txtHardwareFingerprint.TabIndex = 1;
            // 
            // btnCopiarId
            // 
            btnCopiarId.BackColor = Color.FromArgb(67, 20, 135);
            btnCopiarId.FlatAppearance.BorderSize = 0;
            btnCopiarId.FlatStyle = FlatStyle.Flat;
            btnCopiarId.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCopiarId.ForeColor = Color.White;
            btnCopiarId.Location = new Point(540, 214);
            btnCopiarId.Name = "btnCopiarId";
            btnCopiarId.Size = new Size(110, 32);
            btnCopiarId.TabIndex = 2;
            btnCopiarId.Text = "Copiar datos";
            btnCopiarId.UseVisualStyleBackColor = false;
            btnCopiarId.Click += btnCopiarId_Click;
            // 
            // btnGuardarArchivo
            // 
            btnGuardarArchivo.BackColor = Color.FromArgb(67, 20, 135);
            btnGuardarArchivo.FlatAppearance.BorderSize = 0;
            btnGuardarArchivo.FlatStyle = FlatStyle.Flat;
            btnGuardarArchivo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnGuardarArchivo.ForeColor = Color.White;
            btnGuardarArchivo.Location = new Point(30, 345);
            btnGuardarArchivo.Name = "btnGuardarArchivo";
            btnGuardarArchivo.Size = new Size(250, 42);
            btnGuardarArchivo.TabIndex = 3;
            btnGuardarArchivo.Text = "Guardar archivo de activación";
            btnGuardarArchivo.UseVisualStyleBackColor = false;
            btnGuardarArchivo.Click += btnGuardarArchivo_Click;
            // 
            // btnCargarLicencia
            // 
            btnCargarLicencia.BackColor = Color.FromArgb(67, 20, 135);
            btnCargarLicencia.FlatAppearance.BorderSize = 0;
            btnCargarLicencia.FlatStyle = FlatStyle.Flat;
            btnCargarLicencia.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCargarLicencia.ForeColor = Color.White;
            btnCargarLicencia.Location = new Point(290, 345);
            btnCargarLicencia.Name = "btnCargarLicencia";
            btnCargarLicencia.Size = new Size(250, 42);
            btnCargarLicencia.TabIndex = 4;
            btnCargarLicencia.Text = "Cargar licencia";
            btnCargarLicencia.UseVisualStyleBackColor = false;
            btnCargarLicencia.Click += btnCargarLicencia_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.FromArgb(70, 70, 70);
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Location = new Point(540, 425);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(110, 35);
            btnCerrar.TabIndex = 5;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // lblAyuda
            // 
            lblAyuda.Font = new Font("Segoe UI", 9F);
            lblAyuda.ForeColor = Color.FromArgb(90, 90, 90);
            lblAyuda.Location = new Point(30, 400);
            lblAyuda.Name = "lblAyuda";
            lblAyuda.Size = new Size(620, 30);
            lblAyuda.TabIndex = 5;
            // 
            // lblEstado
            // 
            lblEstado.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEstado.ForeColor = Color.FromArgb(67, 20, 135);
            lblEstado.Location = new Point(30, 430);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(500, 25);
            lblEstado.TabIndex = 6;
            // 
            // FActivacion
            // 
            AcceptButton = btnCerrar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnCerrar;
            ClientSize = new Size(680, 500);
            Controls.Add(lblTitulo);
            Controls.Add(lblDescripcion);
            Controls.Add(lblInstallation);
            Controls.Add(txtInstallationId);
            Controls.Add(lblHardwareFingerprint);
            Controls.Add(txtHardwareFingerprint);
            Controls.Add(btnCopiarId);
            Controls.Add(btnGuardarArchivo);
            Controls.Add(btnCargarLicencia);
            Controls.Add(lblAyuda);
            Controls.Add(lblEstado);
            Controls.Add(btnCerrar);
            ForeColor = Color.FromArgb(31, 26, 43);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FActivacion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Stockeate - Activación";
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}