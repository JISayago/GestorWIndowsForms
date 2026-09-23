namespace Stockeate
{
    partial class FActivacion
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblInstallation;
        private System.Windows.Forms.TextBox txtInstallationId;
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblInstallation = new System.Windows.Forms.Label();
            this.txtInstallationId = new System.Windows.Forms.TextBox();
            this.btnCopiarId = new System.Windows.Forms.Button();
            this.btnGuardarArchivo = new System.Windows.Forms.Button();
            this.btnCargarLicencia = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // ========================================================
            // FORM
            // ========================================================

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize = new System.Drawing.Size(680, 430);

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedDialog;

            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.BackColor =
                System.Drawing.Color.White;

            this.Name = "FrmActivacion";

            this.Text = "Stockeate - Activación";


            // ========================================================
            // TITULO
            // ========================================================

            this.lblTitulo.AutoSize = false;

            this.lblTitulo.Location =
                new System.Drawing.Point(30, 25);

            this.lblTitulo.Size =
                new System.Drawing.Size(620, 45);

            this.lblTitulo.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    20F,
                    System.Drawing.FontStyle.Bold);

            this.lblTitulo.ForeColor =
                System.Drawing.Color.FromArgb(67, 20, 135);

            this.lblTitulo.Text =
                "Activación de Stockeate";

            this.lblTitulo.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;


            // ========================================================
            // DESCRIPCION
            // ========================================================

            this.lblDescripcion.AutoSize = false;

            this.lblDescripcion.Location =
                new System.Drawing.Point(30, 85);

            this.lblDescripcion.Size =
                new System.Drawing.Size(620, 100);

            this.lblDescripcion.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.lblDescripcion.ForeColor =
                System.Drawing.Color.FromArgb(45, 45, 45);

            this.lblDescripcion.Text =
                "";


            // ========================================================
            // LABEL INSTALLATION ID
            // ========================================================

            this.lblInstallation.AutoSize = false;

            this.lblInstallation.Location =
                new System.Drawing.Point(30, 200);

            this.lblInstallation.Size =
                new System.Drawing.Size(620, 25);

            this.lblInstallation.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.lblInstallation.ForeColor =
                System.Drawing.Color.FromArgb(35, 35, 35);

            this.lblInstallation.Text =
                "Installation ID:";


            // ========================================================
            // TEXTBOX INSTALLATION ID
            // ========================================================

            this.txtInstallationId.Location =
                new System.Drawing.Point(30, 230);

            this.txtInstallationId.Size =
                new System.Drawing.Size(500, 30);

            this.txtInstallationId.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.txtInstallationId.ReadOnly = true;

            this.txtInstallationId.BackColor =
                System.Drawing.Color.FromArgb(245, 245, 245);

            this.txtInstallationId.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;


            // ========================================================
            // COPIAR ID
            // ========================================================

            this.btnCopiarId.Location =
                new System.Drawing.Point(540, 229);

            this.btnCopiarId.Size =
                new System.Drawing.Size(110, 32);

            this.btnCopiarId.Text =
                "Copiar ID";

            this.btnCopiarId.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.btnCopiarId.BackColor =
                System.Drawing.Color.FromArgb(67, 20, 135);

            this.btnCopiarId.ForeColor =
                System.Drawing.Color.White;

            this.btnCopiarId.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnCopiarId.FlatAppearance.BorderSize = 0;

            this.btnCopiarId.UseVisualStyleBackColor = false;

            this.btnCopiarId.Click +=
                new System.EventHandler(
                    this.btnCopiarId_Click);


            // ========================================================
            // GUARDAR ARCHIVO
            // ========================================================

            this.btnGuardarArchivo.Location =
                new System.Drawing.Point(30, 285);

            this.btnGuardarArchivo.Size =
                new System.Drawing.Size(250, 42);

            this.btnGuardarArchivo.Text =
                "Guardar archivo de activación";

            this.btnGuardarArchivo.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F,
                    System.Drawing.FontStyle.Bold);

            this.btnGuardarArchivo.BackColor =
                System.Drawing.Color.FromArgb(67, 20, 135);

            this.btnGuardarArchivo.ForeColor =
                System.Drawing.Color.White;

            this.btnGuardarArchivo.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnGuardarArchivo.FlatAppearance.BorderSize = 0;

            this.btnGuardarArchivo.UseVisualStyleBackColor = false;

            this.btnGuardarArchivo.Click +=
                new System.EventHandler(
                    this.btnGuardarArchivo_Click);


            // ========================================================
            // CARGAR LICENCIA
            // ========================================================

            this.btnCargarLicencia.Location =
                new System.Drawing.Point(30, 285);

            this.btnCargarLicencia.Size =
                new System.Drawing.Size(250, 42);

            this.btnCargarLicencia.Text =
                "Cargar licencia";

            this.btnCargarLicencia.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F,
                    System.Drawing.FontStyle.Bold);

            this.btnCargarLicencia.BackColor =
                System.Drawing.Color.FromArgb(67, 20, 135);

            this.btnCargarLicencia.ForeColor =
                System.Drawing.Color.White;

            this.btnCargarLicencia.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnCargarLicencia.FlatAppearance.BorderSize = 0;

            this.btnCargarLicencia.UseVisualStyleBackColor = false;

            this.btnCargarLicencia.Click +=
                new System.EventHandler(
                    this.btnCargarLicencia_Click);


            // ========================================================
            // AYUDA
            // ========================================================

            this.lblAyuda.AutoSize = false;

            this.lblAyuda.Location =
                new System.Drawing.Point(30, 340);

            this.lblAyuda.Size =
                new System.Drawing.Size(620, 30);

            this.lblAyuda.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F);

            this.lblAyuda.ForeColor =
                System.Drawing.Color.FromArgb(90, 90, 90);

            this.lblAyuda.Text =
                "";


            // ========================================================
            // ESTADO
            // ========================================================

            this.lblEstado.AutoSize = false;

            this.lblEstado.Location =
                new System.Drawing.Point(30, 370);

            this.lblEstado.Size =
                new System.Drawing.Size(500, 25);

            this.lblEstado.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.lblEstado.ForeColor =
                System.Drawing.Color.FromArgb(67, 20, 135);

            this.lblEstado.Text =
                "";


            // ========================================================
            // CERRAR
            // ========================================================

            this.btnCerrar.Location =
                new System.Drawing.Point(540, 365);

            this.btnCerrar.Size =
                new System.Drawing.Size(110, 35);

            this.btnCerrar.Text =
                "Cerrar";

            this.btnCerrar.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F,
                    System.Drawing.FontStyle.Bold);

            this.btnCerrar.BackColor =
                System.Drawing.Color.FromArgb(70, 70, 70);

            this.btnCerrar.ForeColor =
                System.Drawing.Color.White;

            this.btnCerrar.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnCerrar.FlatAppearance.BorderSize = 0;

            this.btnCerrar.UseVisualStyleBackColor = false;

            this.btnCerrar.Click +=
                new System.EventHandler(
                    this.btnCerrar_Click);


            // ========================================================
            // CONTROLES
            // ========================================================

            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblInstallation);
            this.Controls.Add(this.txtInstallationId);
            this.Controls.Add(this.btnCopiarId);
            this.Controls.Add(this.btnGuardarArchivo);
            this.Controls.Add(this.btnCargarLicencia);
            this.Controls.Add(this.lblAyuda);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.btnCerrar);

            this.AcceptButton = this.btnCerrar;
            this.CancelButton = this.btnCerrar;

            this.ResumeLayout(false);
        }
    }
}
