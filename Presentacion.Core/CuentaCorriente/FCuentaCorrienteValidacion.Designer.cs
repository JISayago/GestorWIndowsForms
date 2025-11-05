namespace Presentacion.Core.CuentaCorriente
{
    partial class FCuentaCorrienteValidacion
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
            btnVerificar = new Button();
            lblDni = new Label();
            txtDni = new TextBox();
            lblCtaCte = new Label();
            lblSaldoDisponible = new Label();
            lblLimite = new Label();
            SuspendLayout();
            // 
            // btnVerificar
            // 
            btnVerificar.Location = new Point(287, 45);
            btnVerificar.Name = "btnVerificar";
            btnVerificar.Size = new Size(75, 23);
            btnVerificar.TabIndex = 0;
            btnVerificar.Text = "Verificar";
            btnVerificar.UseVisualStyleBackColor = true;
            btnVerificar.Click += btnVerificar_Click;
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Location = new Point(213, 19);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(27, 15);
            lblDni.TabIndex = 1;
            lblDni.Text = "DNI";
            // 
            // txtDni
            // 
            txtDni.Location = new Point(246, 16);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(166, 23);
            txtDni.TabIndex = 2;
            // 
            // lblCtaCte
            // 
            lblCtaCte.AutoSize = true;
            lblCtaCte.Location = new Point(45, 19);
            lblCtaCte.Name = "lblCtaCte";
            lblCtaCte.Size = new Size(97, 15);
            lblCtaCte.TabIndex = 3;
            lblCtaCte.Text = "Cuenta Corriente";
            // 
            // lblSaldoDisponible
            // 
            lblSaldoDisponible.AutoSize = true;
            lblSaldoDisponible.Location = new Point(45, 45);
            lblSaldoDisponible.Name = "lblSaldoDisponible";
            lblSaldoDisponible.Size = new Size(36, 15);
            lblSaldoDisponible.TabIndex = 4;
            lblSaldoDisponible.Text = "Saldo";
            // 
            // lblLimite
            // 
            lblLimite.AutoSize = true;
            lblLimite.Location = new Point(45, 69);
            lblLimite.Name = "lblLimite";
            lblLimite.Size = new Size(40, 15);
            lblLimite.TabIndex = 5;
            lblLimite.Text = "Limite";
            // 
            // FCuentaCorrienteValidacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(458, 102);
            Controls.Add(lblLimite);
            Controls.Add(lblSaldoDisponible);
            Controls.Add(lblCtaCte);
            Controls.Add(txtDni);
            Controls.Add(lblDni);
            Controls.Add(btnVerificar);
            Name = "FCuentaCorrienteValidacion";
            Text = "FCuentaCorrienteValidacion";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnVerificar;
        private Label lblDni;
        private TextBox txtDni;
        private Label lblCtaCte;
        private Label lblSaldoDisponible;
        private Label lblLimite;
    }
}