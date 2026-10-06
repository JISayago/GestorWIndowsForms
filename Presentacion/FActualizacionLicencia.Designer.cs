namespace Presentacion
{
    partial class FActualizacionLicencia
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitulo;
        private Label lblDescripcion;
        private Label lblFecha;
        private Label lblFechaVencimiento;
        private Label lblTiempoRestante;
        private Button btnCargarLicencia;
        private Button btnContinuarPrueba;
        private Button btnCerrar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblDescripcion = new Label();
            lblFecha = new Label();
            lblFechaVencimiento = new Label();
            lblTiempoRestante = new Label();
            btnCargarLicencia = new Button();
            btnContinuarPrueba = new Button();
            btnCerrar = new Button();

            SuspendLayout();

            // ========================================================
            // FORM
            // ========================================================

            AutoScaleDimensions =
                new SizeF(7F, 15F);

            AutoScaleMode =
                AutoScaleMode.Font;

            ClientSize =
                new Size(650, 360);

            FormBorderStyle =
                FormBorderStyle.FixedDialog;

            MaximizeBox = false;
            MinimizeBox = false;

            StartPosition =
                FormStartPosition.CenterScreen;

            Text =
                "Stockeate - Licencia de prueba";

            // ========================================================
            // TITULO
            // ========================================================

            lblTitulo.AutoSize = false;

            lblTitulo.Location =
                new Point(30, 25);

            lblTitulo.Size =
                new Size(590, 40);

            lblTitulo.Font =
                new Font(
                    "Segoe UI",
                    18F,
                    FontStyle.Bold);

            lblTitulo.Text =
                "Licencia de prueba";

            // ========================================================
            // DESCRIPCION
            // ========================================================

            lblDescripcion.AutoSize = false;

            lblDescripcion.Location =
                new Point(30, 75);

            lblDescripcion.Size =
                new Size(590, 70);

            lblDescripcion.Font =
                new Font(
                    "Segoe UI",
                    10F);

            lblDescripcion.Text =
                "Actualmente estás utilizando la versión de prueba " +
                "de Stockeate.\r\n\r\n" +
                "Podés continuar utilizándola o cargar tu licencia " +
                "permanente.";

            // ========================================================
            // FECHA
            // ========================================================

            lblFecha.AutoSize = true;

            lblFecha.Location =
                new Point(30, 165);

            lblFecha.Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold);

            lblFecha.Text =
                "Vencimiento:";

            // ========================================================
            // FECHA VENCIMIENTO
            // ========================================================

            lblFechaVencimiento.AutoSize = true;

            lblFechaVencimiento.Location =
                new Point(145, 165);

            lblFechaVencimiento.Font =
                new Font(
                    "Segoe UI",
                    10F);

            lblFechaVencimiento.Text =
                "-";

            // ========================================================
            // TIEMPO RESTANTE
            // ========================================================

            lblTiempoRestante.AutoSize = false;

            lblTiempoRestante.Location =
                new Point(30, 195);

            lblTiempoRestante.Size =
                new Size(590, 35);

            lblTiempoRestante.Font =
                new Font(
                    "Segoe UI",
                    11F,
                    FontStyle.Bold);

            lblTiempoRestante.Text =
                "-";

            // ========================================================
            // CARGAR LICENCIA
            // ========================================================

            btnCargarLicencia.Location =
                new Point(30, 250);

            btnCargarLicencia.Size =
                new Size(250, 42);

            btnCargarLicencia.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            btnCargarLicencia.Text =
                "Cargar licencia permanente";

            btnCargarLicencia.Click +=
                btnCargarLicencia_Click;

            // ========================================================
            // CONTINUAR PRUEBA
            // ========================================================

            btnContinuarPrueba.Location =
                new Point(290, 250);

            btnContinuarPrueba.Size =
                new Size(160, 42);

            btnContinuarPrueba.Font =
                new Font(
                    "Segoe UI",
                    9F);

            btnContinuarPrueba.Text =
                "Continuar con prueba";

            btnContinuarPrueba.Click +=
                btnContinuarPrueba_Click;

            // ========================================================
            // CERRAR
            // ========================================================

            btnCerrar.Location =
                new Point(490, 250);

            btnCerrar.Size =
                new Size(130, 42);

            btnCerrar.Font =
                new Font(
                    "Segoe UI",
                    9F);

            btnCerrar.Text =
                "Cerrar";

            btnCerrar.Click +=
                btnCerrar_Click;

            // ========================================================
            // CONTROLES
            // ========================================================

            Controls.Add(lblTitulo);
            Controls.Add(lblDescripcion);
            Controls.Add(lblFecha);
            Controls.Add(lblFechaVencimiento);
            Controls.Add(lblTiempoRestante);
            Controls.Add(btnCargarLicencia);
            Controls.Add(btnContinuarPrueba);
            Controls.Add(btnCerrar);

            ResumeLayout(false);
        }
    }
}