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
            this.Padding = new Padding(20);
            this.BackColor = Color.White;

            // Encabezado
            lblNumeroGasto = new Label { Location = new Point(20, 20), AutoSize = true, Font = new Font("Segoe UI", 14, FontStyle.Bold) };

            lblMontoTotal = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.DarkRed, // Rojo oscuro para los gastos
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            // Contenedor interno para organizar mejor los datos sin importar el tamaño de la ventana
            Panel pnlDatos = new Panel
            {
                Location = new Point(20, 70),
                Size = new Size(800, 300),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(248, 249, 250),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblEmpleado = new Label { Location = new Point(15, 15), AutoSize = true, Font = new Font("Segoe UI", 11) };
            lblFechas = new Label { Location = new Point(15, 55), AutoSize = true, Font = new Font("Segoe UI", 11) };
            lblCategoriaEstado = new Label { Location = new Point(15, 95), AutoSize = true, Font = new Font("Segoe UI", 11) };
            lblMontoPagado = new Label { Location = new Point(15, 135), AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold) };

            lblDetalle = new Label
            {
                Location = new Point(15, 185),
                Size = new Size(760, 100),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(64, 64, 64)
            };

            // Evento para reposicionar el monto a la derecha
            this.SizeChanged += (s, e) => {
                lblMontoTotal.Location = new Point(this.Width - lblMontoTotal.Width - 25, 20);
                pnlDatos.Width = this.Width - 40; // Mantiene el margen
                lblDetalle.Width = pnlDatos.Width - 30; // Ajusta el texto del detalle
            };

            pnlDatos.Controls.AddRange(new Control[] { lblEmpleado, lblFechas, lblCategoriaEstado, lblMontoPagado, lblDetalle });

            this.Controls.Add(lblNumeroGasto);
            this.Controls.Add(lblMontoTotal);
            this.Controls.Add(pnlDatos);
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