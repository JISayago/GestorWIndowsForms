using Servicios.LogicaNegocio.Gasto.DTO;
using System.Drawing;
using System.Windows.Forms;

namespace TuProyecto.Presentacion.Paneles
{
    public partial class PanelMovimientoGasto : UserControl
    {
        private Label lblNumeroGasto;
        private Label lblMontoTotal;
        private Label lblMontoPagado;
        private Label lblEmpleado;
        private Label lblFechas;
        private Label lblCategoriaEstado;
        private Label lblDetalle;

        public PanelMovimientoGasto()
        {
            CrearControlesVisuales();
        }

        private void CrearControlesVisuales()
        {
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(25);
            this.BackColor = Color.White;

            // --- HEADER (Tabla para evitar que se corte el monto) ---
            TableLayoutPanel tblGastoHeader = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 50,
                ColumnCount = 2
            };
            tblGastoHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
            tblGastoHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));

            lblNumeroGasto = new Label { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 13, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft };
            lblMontoTotal = new Label { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 13, FontStyle.Bold), ForeColor = Color.DarkRed, TextAlign = ContentAlignment.MiddleRight };

            tblGastoHeader.Controls.Add(lblNumeroGasto, 0, 0);
            tblGastoHeader.Controls.Add(lblMontoTotal, 1, 0);

            // --- CUADRO DE DETALLES (Ficha gris) ---
            Panel pnlFicha = new Panel
            {
                Dock = DockStyle.Top,
                Height = 320, // Aumentamos un poquito para que entre todo bien
                Margin = new Padding(0, 20, 0, 0),
                Padding = new Padding(20),
                BackColor = Color.FromArgb(252, 252, 252),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Inicialización de los labels que faltaban
            lblEmpleado = new Label { Location = new Point(20, 25), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            lblFechas = new Label { Location = new Point(20, 60), AutoSize = true, Font = new Font("Segoe UI", 10) };
            lblCategoriaEstado = new Label { Location = new Point(20, 95), AutoSize = true, Font = new Font("Segoe UI", 10) };

            lblMontoPagado = new Label
            {
                Location = new Point(20, 135),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40)
            };

            Label lblTituloDetalle = new Label
            {
                Text = "Detalles adicionales:",
                Location = new Point(20, 185),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Underline)
            };

            lblDetalle = new Label
            {
                Location = new Point(20, 210),
                Size = new Size(700, 80),
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.DimGray
            };

            // Agregamos los controles al panel de la ficha
            pnlFicha.Controls.AddRange(new Control[] {
                lblEmpleado,
                lblFechas,
                lblCategoriaEstado,
                lblMontoPagado,
                lblTituloDetalle,
                lblDetalle
            });

            // Agregamos todo al UserControl (El orden importa para el Dock)
            this.Controls.Add(pnlFicha);
            this.Controls.Add(tblGastoHeader);
        }

        public void CargarDatos(GastoDTO gasto)
        {
            if (gasto == null) return;

            lblNumeroGasto.Text = $"Gasto Asoc. N°: {gasto.NumeroGasto}";
            lblMontoTotal.Text = $"Total del Gasto: {gasto.MontoTotal:C}";

            lblEmpleado.Text = $"Empleado Asignado: {gasto.NombreEmpleado} (ID: {gasto.IdEmpleado})";
            lblFechas.Text = $"Fecha del Gasto: {gasto.FechaGasto:dd/MM/yyyy HH:mm}  |  Fecha de Registro: {gasto.FechaRegistro:dd/MM/yyyy HH:mm}";

            // Asumiendo que CategoriaGasto y EstadoGasto son Enums que luego mapearás a texto. 
            // Por ahora mostramos el número, pero puedes agregar tu lógica de Enum aquí.
            lblCategoriaEstado.Text = $"Categoría: {gasto.CategoriaGasto}  |  Estado: {gasto.EstadoGasto}";

            lblMontoPagado.Text = $"Monto Pagado: {gasto.MontoPagado:C}  |  Saldo Pendiente: {(gasto.MontoTotal - gasto.MontoPagado):C}";
            lblDetalle.Text = $"Detalles adicionales:\n{gasto.Detalle}";
        }
    }
}