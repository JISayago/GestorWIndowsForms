namespace Stockeate
{
    partial class FSesionSecundariaEnCurso
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
            tlpCentro = new TableLayoutPanel();
            pbxLogo = new PictureBox();
            lblTitulo = new Label();
            lblDetalle = new Label();
            tlpCentro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxLogo).BeginInit();
            SuspendLayout();
            // 
            // tlpCentro
            // 
            tlpCentro.ColumnCount = 1;
            tlpCentro.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpCentro.Controls.Add(pbxLogo, 0, 0);
            tlpCentro.Controls.Add(lblTitulo, 0, 1);
            tlpCentro.Controls.Add(lblDetalle, 0, 2);
            tlpCentro.Dock = DockStyle.Fill;
            tlpCentro.Location = new Point(0, 0);
            tlpCentro.Name = "tlpCentro";
            tlpCentro.RowCount = 4;
            tlpCentro.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpCentro.RowStyles.Add(new RowStyle());
            tlpCentro.RowStyles.Add(new RowStyle());
            tlpCentro.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpCentro.Size = new Size(800, 450);
            tlpCentro.TabIndex = 0;
            // 
            // pbxLogo
            // 
            pbxLogo.Anchor = AnchorStyles.Bottom;
            pbxLogo.BackColor = Color.Transparent;
            pbxLogo.Location = new Point(95, 4);
            pbxLogo.Margin = new Padding(3, 3, 3, 24);
            pbxLogo.Name = "pbxLogo";
            pbxLogo.Size = new Size(610, 152);
            pbxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pbxLogo.TabIndex = 0;
            pbxLogo.TabStop = false;
            // 
            // lblTitulo
            // 
            lblTitulo.Anchor = AnchorStyles.None;
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(156, 180);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(488, 50);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Sesión secundaria en curso";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDetalle
            // 
            lblDetalle.Anchor = AnchorStyles.None;
            lblDetalle.AutoSize = true;
            lblDetalle.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDetalle.Location = new Point(65, 238);
            lblDetalle.Margin = new Padding(3, 8, 3, 0);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(670, 32);
            lblDetalle.TabIndex = 2;
            lblDetalle.Text = "Esta ventana se habilita cuando la sesión secundaria se cierre.";
            lblDetalle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FSesionSecundariaEnCurso
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            ControlBox = false;
            Controls.Add(tlpCentro);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FSesionSecundariaEnCurso";
            ShowInTaskbar = false;
            Text = "Sesión secundaria en curso";
            WindowState = FormWindowState.Maximized;
            tlpCentro.ResumeLayout(false);
            tlpCentro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbxLogo).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpCentro;
        private System.Windows.Forms.PictureBox pbxLogo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDetalle;
    }
}