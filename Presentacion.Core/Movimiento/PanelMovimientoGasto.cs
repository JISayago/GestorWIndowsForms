using Presentacion.FBase.Helpers;
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
            this.BackColor = TemaSistema.FondoControl;

            // --- HEADER (Tabla para evitar que se corte el monto) ---
            TableLayoutPanel tblGastoHeader = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 50,
                ColumnCount = 2,
                BackColor = Color.Transparent
            };
            tblGastoHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
            tblGastoHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));

            lblNumeroGasto = new Label { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 13, FontStyle.Bold), ForeColor = TemaSistema.Texto, TextAlign = ContentAlignment.MiddleLeft };
            // El color se fija en CargarDatos (egreso = Firebrick, mismo criterio que el resto de los paneles)
            lblMontoTotal = new Label { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 13, FontStyle.Bold), TextAlign = ContentAlignment.MiddleRight };

            tblGastoHeader.Controls.Add(lblNumeroGasto, 0, 0);
            tblGastoHeader.Controls.Add(lblMontoTotal, 1, 0);

            // --- CUADRO DE DETALLES (Ficha) ---
            // TableLayoutPanel de una columna en vez de Location fijos: cada fila crece con su
            // contenido (AutoSize), así un nombre largo o un detalle extenso no pisa la fila siguiente.
            TableLayoutPanel pnlFicha = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                Margin = new Padding(0, 20, 0, 0),
                Padding = new Padding(20),
                BackColor = TemaSistema.Alternado,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlFicha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            lblEmpleado = new Label { AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = TemaSistema.Texto, Margin = new Padding(0, 3, 0, 3) };
            lblFechas = new Label { AutoSize = true, Font = new Font("Segoe UI", 10), ForeColor = TemaSistema.TextoSecundario, Margin = new Padding(0, 3, 0, 3) };
            lblCategoriaEstado = new Label { AutoSize = true, Font = new Font("Segoe UI", 10), ForeColor = TemaSistema.TextoSecundario, Margin = new Padding(0, 3, 0, 3) };

            // Color dinámico en CargarDatos: rojo/ámbar si queda saldo pendiente, verde si está saldado.
            lblMontoPagado = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Margin = new Padding(0, 10, 0, 3)
            };

            Label lblTituloDetalle = new Label
            {
                Text = "Detalles adicionales:",
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Underline),
                ForeColor = TemaSistema.Texto,
                Margin = new Padding(0, 10, 0, 3)
            };

            lblDetalle = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(700, 0), // ancho fijo, alto libre: el texto envuelve en vez de cortarse
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = TemaSistema.TextoSecundario,
                Margin = new Padding(0, 0, 0, 5)
            };

            // Agregamos los controles al panel de la ficha, uno por fila
            pnlFicha.Controls.Add(lblEmpleado);
            pnlFicha.Controls.Add(lblFechas);
            pnlFicha.Controls.Add(lblCategoriaEstado);
            pnlFicha.Controls.Add(lblMontoPagado);
            pnlFicha.Controls.Add(lblTituloDetalle);
            pnlFicha.Controls.Add(lblDetalle);

            // Agregamos todo al UserControl (El orden importa para el Dock)
            this.Controls.Add(pnlFicha);
            this.Controls.Add(tblGastoHeader);

            this.SizeChanged += (s, e) =>
            {
                int anchoDisponible = System.Math.Max(this.ClientSize.Width - pnlFicha.Padding.Horizontal - 40, 100);
                lblDetalle.MaximumSize = new Size(anchoDisponible, 0);
            };
        }

        public void CargarDatos(GastoDTO gasto)
        {
            if (gasto == null) return;

            lblNumeroGasto.Text = $"Gasto Asoc. N°: {gasto.NumeroGasto}";
            // Un gasto es, por naturaleza, un egreso -> mismo rojo que usa CtaCte para saldo negativo
            // Math.Abs por si MontoTotal llegara negativo: el signo lo controla este código, no el formato :C
            lblMontoTotal.Text = $"- {System.Math.Abs(gasto.MontoTotal):C}";
            lblMontoTotal.ForeColor = Color.Firebrick;

            lblEmpleado.Text = $"Empleado Asignado: {gasto.NombreEmpleado} (ID: {gasto.IdEmpleado})";
            lblFechas.Text = $"Fecha del Gasto: {gasto.FechaGastoDescripcion}  |  Fecha de Registro: {gasto.FechaRegistro:dd/MM/yyyy HH:mm}";

            lblCategoriaEstado.Text = $"Categoría: {gasto.CategoriaGastoDescripcion}  |  Estado: {gasto.EstadoGastoDescripcion}";

            decimal saldoPendiente = gasto.MontoTotal - gasto.MontoPagado;
            lblMontoPagado.Text = $"Monto Pagado: {gasto.MontoPagado:C}  |  Saldo Pendiente: {saldoPendiente:C}";
            // Resalta si queda deuda del gasto, igual que el estado ya resalta Pendiente/Anulado en rojo
            lblMontoPagado.ForeColor = saldoPendiente > 0 ? Color.Firebrick : Color.SeaGreen;

            lblDetalle.Text = string.IsNullOrWhiteSpace(gasto.Detalle) ? "Sin detalles adicionales" : gasto.Detalle;
        }
    }
}