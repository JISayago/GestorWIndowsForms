using Presentacion.FBase.Helpers;
using Servicios.LogicaNegocio.Venta.DTO;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace TuProyecto.Presentacion.Paneles
{
    public partial class PanelMovimientoVenta : UserControl
    {
        private Label lblNumeroVenta;
        private Label lblTotal;
        private Label lblEstado;
        private Label lblDescuento;
        private TextBox txtDetalle;
        private DataGridView dgvItems;
        private Label lblSinItems;

        public PanelMovimientoVenta()
        {
            CrearControlesVisuales();
        }

        private void CrearControlesVisuales()
        {
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(15);
            this.BackColor = TemaSistema.FondoControl;

            // --- CONTENEDOR DE TEXTOS (Para que no se pisen con la grilla) ---
            Panel pnlHeaderVenta = new Panel
            {
                Dock = DockStyle.Top,
                Height = 165, // Espacio suficiente para todos los labels + textbox de detalle
                BackColor = TemaSistema.FondoControl
            };

            lblNumeroVenta = new Label { Location = new Point(0, 5), AutoSize = true, Font = new Font("Segoe UI", 13, FontStyle.Bold), ForeColor = TemaSistema.Texto };
            // El color se fija en CargarDatos según signo del movimiento (Ingreso = venta cobrada)
            lblTotal = new Label { AutoSize = true, Font = new Font("Segoe UI", 13, FontStyle.Bold), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            lblEstado = new Label { Location = new Point(0, 35), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            lblDescuento = new Label { Location = new Point(0, 60), AutoSize = true, Font = new Font("Segoe UI", 10), ForeColor = TemaSistema.TextoSecundario };

            // TextBox multilínea de solo lectura con scroll vertical: si el detalle no entra en el
            // área visible, se puede scrollear en vez de cortarse (antes era un Label con AutoEllipsis).
            // Colores de TemaSistema para que siga viéndose integrado al panel, con un borde fino
            // (FixedSingle) que ayuda a distinguir visualmente que es un área scrolleable.
            Label lblTituloDetalle = new Label
            {
                Text = "Detalle:",
                Location = new Point(0, 85),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Underline),
                ForeColor = TemaSistema.Texto
            };

            txtDetalle = new TextBox
            {
                Location = new Point(0, 105),
                Size = new Size(800, 50),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BorderStyle = BorderStyle.FixedSingle,
                TabStop = false,
                BackColor = TemaSistema.FondoControl,
                ForeColor = TemaSistema.TextoSecundario,
                Font = new Font("Segoe UI", 9),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            this.SizeChanged += (s, e) => {
                lblTotal.Location = new Point(pnlHeaderVenta.Width - lblTotal.Width - 5, 5);
                txtDetalle.Width = System.Math.Max(pnlHeaderVenta.Width - 10, 100);
            };

            pnlHeaderVenta.Controls.AddRange(new Control[] { lblNumeroVenta, lblTotal, lblEstado, lblDescuento, lblTituloDetalle, txtDetalle });

            // --- GRILLA ---
            dgvItems = new DataGridView
            {
                Dock = DockStyle.Fill, // Ahora ocupa todo lo que SOBRA del panel
                BackgroundColor = TemaSistema.FondoControl,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells // Se ajusta al contenido
            };

            // Aviso para cuando la venta no tiene ítems cargados, en vez de mostrar una grilla en
            // blanco sin explicación. Se alterna con dgvItems según haya datos o no.
            lblSinItems = new Label
            {
                Text = "Sin ítems registrados en esta venta.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = TemaSistema.TextoSecundario,
                Visible = false
            };

            this.Controls.Add(lblSinItems);
            this.Controls.Add(dgvItems);
            this.Controls.Add(pnlHeaderVenta); // El orden de agregado es vital para el Dock
        }

        public void CargarDatos(VentaDTO venta)
        {
            if (venta == null) return;

            lblNumeroVenta.Text = $"Venta Asoc. N°: {venta.NumeroVenta}";
            // Una venta es, por naturaleza, un ingreso -> mismo verde que usa CtaCte para saldo positivo
            // Math.Abs por si Total llegara negativo: el signo lo controla este código, no el formato :C
            lblTotal.Text = $"+ {System.Math.Abs(venta.Total):C}";
            lblTotal.ForeColor = Color.SeaGreen;

            lblEstado.Text = $"Estado: {venta.EstadoDescripcion}";
            lblEstado.ForeColor = venta.Estado == (int)Servicios.Helpers.VentaEnum.EstadoVenta.Confirmada
                ? Color.SeaGreen
                : Color.Firebrick;

            lblDescuento.Text = $"Descuento: {venta.Descuento:C} (Total sin desc: {venta.TotalSinDescuento:C})";
            txtDetalle.Text = string.IsNullOrWhiteSpace(venta.Detalle) ? "Sin detalles extra" : venta.Detalle;

            bool tieneItems = venta.Items != null && venta.Items.Any();
            dgvItems.Visible = tieneItems;
            lblSinItems.Visible = !tieneItems;

            if (tieneItems)
            {
                dgvItems.DataSource = venta.Items;
                ConfigurarColumnasItems(dgvItems);
            }
        }

        // Mismo criterio de curado de columnas que FMovimientoConsulta.ResetearGrilla:
        // ocultar IDs/códigos técnicos, formatear moneda, headers en español.
        private void ConfigurarColumnasItems(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0)
                return;

            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            void Ocultar(string nombre)
            {
                if (grilla.Columns.Contains(nombre))
                    grilla.Columns[nombre].Visible = false;
            }

            void Config(string nombre, string header, string formato = null, int fillWeight = 100)
            {
                if (!grilla.Columns.Contains(nombre)) return;
                var col = grilla.Columns[nombre];
                col.HeaderText = header;
                col.FillWeight = fillWeight;
                if (formato != null)
                    col.DefaultCellStyle.Format = formato;
            }

            // Técnicos / de soporte a Subtotal, sin valor para el usuario final
            Ocultar("ItemId");
            Ocultar("Stock");
            Ocultar("CodigoOferta");
            Ocultar("TipoOferta");
            Ocultar("PrecioOferta"); // se refleja en Subtotal; mostrarlo aparte confunde si EsOferta es false
            Ocultar("Medida");       // se combina con UnidadMedida en un único encabezado más claro

            Config("Descripcion", "Producto", fillWeight: 260);
            Config("Cantidad", "Cant.", "N2", fillWeight: 70);
            Config("UnidadMedida", "Unidad", fillWeight: 90);
            Config("PrecioVenta", "Precio Unit.", "C2", fillWeight: 120);
            Config("Subtotal", "Subtotal", "C2", fillWeight: 130);

            // EsOferta se autogenera como checkbox (bool) - se conserva, es la forma más clara
            // de marcar visualmente qué ítems tuvieron oferta aplicada.
            Config("EsOferta", "Oferta", fillWeight: 70);
        }
    }
}