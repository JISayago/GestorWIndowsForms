using Servicios.LogicaNegocio.Venta.DTO;
using System.Windows.Forms;
using System.Drawing;

namespace TuProyecto.Presentacion.Paneles
{
    public partial class PanelMovimientoVenta : UserControl
    {
        private Label lblNumeroVenta;
        private Label lblTotal;
        private Label lblDescuento;
        private Label lblDetalle;
        private DataGridView dgvItems;

        public PanelMovimientoVenta()
        {
            CrearControlesVisuales();
        }

        private void CrearControlesVisuales()
        {
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(15);
            this.BackColor = Color.White;

            // --- CONTENEDOR DE TEXTOS (Para que no se pisen con la grilla) ---
            Panel pnlHeaderVenta = new Panel
            {
                Dock = DockStyle.Top,
                Height = 140, // Espacio suficiente para todos los labels
                BackColor = Color.White
            };

            lblNumeroVenta = new Label { Location = new Point(0, 5), AutoSize = true, Font = new Font("Segoe UI", 13, FontStyle.Bold) };
            lblTotal = new Label { AutoSize = true, Font = new Font("Segoe UI", 13, FontStyle.Bold), ForeColor = Color.DarkBlue, Anchor = AnchorStyles.Top | AnchorStyles.Right };
            lblDescuento = new Label { Location = new Point(0, 35), AutoSize = true, Font = new Font("Segoe UI", 10) };

            lblDetalle = new Label
            {
                Location = new Point(0, 60),
                Size = new Size(800, 75), // Tamaño fijo de área de texto
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(64, 64, 64)
            };

            this.SizeChanged += (s, e) => {
                lblTotal.Location = new Point(pnlHeaderVenta.Width - lblTotal.Width - 5, 5);
            };

            pnlHeaderVenta.Controls.AddRange(new Control[] { lblNumeroVenta, lblTotal, lblDescuento, lblDetalle });

            // --- GRILLA ---
            dgvItems = new DataGridView
            {
                Dock = DockStyle.Fill, // Ahora ocupa todo lo que SOBRA del panel
                BackgroundColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.Fixed3D,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells // Se ajusta al contenido
            };

            this.Controls.Add(dgvItems);
            this.Controls.Add(pnlHeaderVenta); // El orden de agregado es vital para el Dock
        }

        public void CargarDatos(VentaDTO venta)
        {
            if (venta == null) return;

            lblNumeroVenta.Text = $"Venta Asoc. N°: {venta.NumeroVenta}";
            lblTotal.Text = $"Total: {venta.Total:C}";
            lblDescuento.Text = $"Descuento: {venta.Descuento:C} (Total sin desc: {venta.TotalSinDescuento:C})";
            lblDetalle.Text = string.IsNullOrWhiteSpace(venta.Detalle) ? "Sin detalles extra" : $"Detalle: {venta.Detalle}";

            dgvItems.DataSource = venta.Items;
        }
    }
}