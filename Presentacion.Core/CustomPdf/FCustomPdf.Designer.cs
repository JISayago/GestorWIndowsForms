namespace Presentacion.Core.CustomPdf
{
    partial class FCustomPdf
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
            lblTitulo = new Label();
            lblNombreNegocio = new Label();
            txtNombreNegocio = new TextBox();
            lblSubtitulo = new Label();
            txtSubtitulo = new TextBox();
            lblTextoPie = new Label();
            txtTextoPie = new TextBox();
            lblLogo = new Label();
            pbxLogo = new PictureBox();
            btnSeleccionarLogo = new Button();
            btnQuitarLogo = new Button();
            lblRutaLogo = new Label();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbxLogo).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(28, 68);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(280, 15);
            lblTitulo.TabIndex = 1;
            lblTitulo.Tag = "Titulo";
            lblTitulo.Text = "Datos que aparecen en los comprobantes PDF";
            // 
            // lblNombreNegocio
            // 
            lblNombreNegocio.AutoSize = true;
            lblNombreNegocio.Font = new Font("Segoe UI", 9.75F);
            lblNombreNegocio.Location = new Point(28, 108);
            lblNombreNegocio.Name = "lblNombreNegocio";
            lblNombreNegocio.Size = new Size(118, 17);
            lblNombreNegocio.TabIndex = 2;
            lblNombreNegocio.Text = "Nombre del negocio";
            // 
            // txtNombreNegocio
            // 
            txtNombreNegocio.Font = new Font("Segoe UI", 9.75F);
            txtNombreNegocio.Location = new Point(28, 128);
            txtNombreNegocio.MaxLength = 80;
            txtNombreNegocio.Name = "txtNombreNegocio";
            txtNombreNegocio.Size = new Size(430, 25);
            txtNombreNegocio.TabIndex = 3;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI", 9.75F);
            lblSubtitulo.Location = new Point(28, 168);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(221, 17);
            lblSubtitulo.TabIndex = 4;
            lblSubtitulo.Text = "Subtítulo (dirección, teléfono, CUIT)";
            // 
            // txtSubtitulo
            // 
            txtSubtitulo.Font = new Font("Segoe UI", 9.75F);
            txtSubtitulo.Location = new Point(28, 188);
            txtSubtitulo.MaxLength = 250;
            txtSubtitulo.Multiline = true;
            txtSubtitulo.Name = "txtSubtitulo";
            txtSubtitulo.Size = new Size(430, 62);
            txtSubtitulo.TabIndex = 5;
            // 
            // lblTextoPie
            // 
            lblTextoPie.AutoSize = true;
            lblTextoPie.Font = new Font("Segoe UI", 9.75F);
            lblTextoPie.Location = new Point(28, 266);
            lblTextoPie.Name = "lblTextoPie";
            lblTextoPie.Size = new Size(72, 17);
            lblTextoPie.TabIndex = 6;
            lblTextoPie.Text = "Texto de pie";
            // 
            // txtTextoPie
            // 
            txtTextoPie.Font = new Font("Segoe UI", 9.75F);
            txtTextoPie.Location = new Point(28, 286);
            txtTextoPie.MaxLength = 150;
            txtTextoPie.Name = "txtTextoPie";
            txtTextoPie.Size = new Size(430, 25);
            txtTextoPie.TabIndex = 7;
            // 
            // lblLogo
            // 
            lblLogo.AutoSize = true;
            lblLogo.Font = new Font("Segoe UI", 9.75F);
            lblLogo.Location = new Point(490, 108);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(36, 17);
            lblLogo.TabIndex = 8;
            lblLogo.Text = "Logo";
            // 
            // pbxLogo
            // 
            pbxLogo.BorderStyle = BorderStyle.FixedSingle;
            pbxLogo.Location = new Point(490, 128);
            pbxLogo.Name = "pbxLogo";
            pbxLogo.Size = new Size(220, 145);
            pbxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pbxLogo.TabIndex = 9;
            pbxLogo.TabStop = false;
            // 
            // btnSeleccionarLogo
            // 
            btnSeleccionarLogo.Location = new Point(490, 286);
            btnSeleccionarLogo.Name = "btnSeleccionarLogo";
            btnSeleccionarLogo.Size = new Size(108, 28);
            btnSeleccionarLogo.TabIndex = 10;
            btnSeleccionarLogo.Text = "Seleccionar";
            btnSeleccionarLogo.UseVisualStyleBackColor = true;
            btnSeleccionarLogo.Click += btnSeleccionarLogo_Click;
            // 
            // btnQuitarLogo
            // 
            btnQuitarLogo.Location = new Point(604, 286);
            btnQuitarLogo.Name = "btnQuitarLogo";
            btnQuitarLogo.Size = new Size(106, 28);
            btnQuitarLogo.TabIndex = 11;
            btnQuitarLogo.Text = "Quitar";
            btnQuitarLogo.UseVisualStyleBackColor = true;
            btnQuitarLogo.Click += btnQuitarLogo_Click;
            // 
            // lblRutaLogo
            // 
            lblRutaLogo.Font = new Font("Segoe UI", 8.25F);
            lblRutaLogo.Location = new Point(28, 330);
            lblRutaLogo.Name = "lblRutaLogo";
            lblRutaLogo.Size = new Size(682, 32);
            lblRutaLogo.TabIndex = 12;
            lblRutaLogo.Text = "Sin logo";
            // 
            // FCustomPdf
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(741, 390);
            Controls.Add(lblRutaLogo);
            Controls.Add(btnQuitarLogo);
            Controls.Add(btnSeleccionarLogo);
            Controls.Add(pbxLogo);
            Controls.Add(lblLogo);
            Controls.Add(txtTextoPie);
            Controls.Add(lblTextoPie);
            Controls.Add(txtSubtitulo);
            Controls.Add(lblSubtitulo);
            Controls.Add(txtNombreNegocio);
            Controls.Add(lblNombreNegocio);
            Controls.Add(lblTitulo);
            MaximumSize = new Size(757, 429);
            MinimumSize = new Size(757, 429);
            Name = "FCustomPdf";
            Text = "Personalizar comprobantes";
            Controls.SetChildIndex(lblTitulo, 0);
            Controls.SetChildIndex(lblNombreNegocio, 0);
            Controls.SetChildIndex(txtNombreNegocio, 0);
            Controls.SetChildIndex(lblSubtitulo, 0);
            Controls.SetChildIndex(txtSubtitulo, 0);
            Controls.SetChildIndex(lblTextoPie, 0);
            Controls.SetChildIndex(txtTextoPie, 0);
            Controls.SetChildIndex(lblLogo, 0);
            Controls.SetChildIndex(pbxLogo, 0);
            Controls.SetChildIndex(btnSeleccionarLogo, 0);
            Controls.SetChildIndex(btnQuitarLogo, 0);
            Controls.SetChildIndex(lblRutaLogo, 0);
            ((System.ComponentModel.ISupportInitialize)pbxLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblNombreNegocio;
        private TextBox txtNombreNegocio;
        private Label lblSubtitulo;
        private TextBox txtSubtitulo;
        private Label lblTextoPie;
        private TextBox txtTextoPie;
        private Label lblLogo;
        private PictureBox pbxLogo;
        private Button btnSeleccionarLogo;
        private Button btnQuitarLogo;
        private Label lblRutaLogo;
    }
}
