using Presentacion.FBase.Helpers;
using Servicios.LogicaNegocio.CuentaCorriente.DTO;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace TuProyecto.Presentacion.Paneles
{
    public partial class PanelMovimientoCuentaCorriente : UserControl
    {
        private Label lblNombreCuenta;
        private Label lblSaldoActual;
        private Label lblCliente;
        private Label lblLimiteDeuda;
        private Label lblVencimiento;
        private Label lblAutorizados;
        private Label lblEstado;

        public PanelMovimientoCuentaCorriente()
        {
            CrearControlesVisuales();
        }

        private void CrearControlesVisuales()
        {
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(25);
            this.BackColor = TemaSistema.FondoControl;

            // --- HEADER ---
            TableLayoutPanel tblHeader = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 60,
                ColumnCount = 2,
                BackColor = Color.Transparent
            };
            tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
            tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));

            lblNombreCuenta = new Label { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 13, FontStyle.Bold), ForeColor = TemaSistema.Texto, TextAlign = ContentAlignment.MiddleLeft };
            lblSaldoActual = new Label { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 14, FontStyle.Bold), TextAlign = ContentAlignment.MiddleRight };

            tblHeader.Controls.Add(lblNombreCuenta, 0, 0);
            tblHeader.Controls.Add(lblSaldoActual, 1, 0);

            // --- FICHA DE DATOS ---
            // TableLayoutPanel de una columna en vez de Location fijos: cada fila crece con su
            // contenido (AutoSize), así un nombre de cliente largo o muchos DNIs autorizados
            // no se recortan ni pisan la fila siguiente.
            TableLayoutPanel pnlFicha = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                Margin = new Padding(0, 20, 0, 0),
                Padding = new Padding(25),
                BackColor = TemaSistema.Alternado,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlFicha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            lblCliente = new Label { AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = TemaSistema.Texto, Margin = new Padding(0, 3, 0, 3) };
            lblEstado = new Label { AutoSize = true, Font = new Font("Segoe UI", 10), ForeColor = TemaSistema.TextoSecundario, Margin = new Padding(0, 3, 0, 3) };
            lblLimiteDeuda = new Label { AutoSize = true, Font = new Font("Segoe UI", 10), ForeColor = TemaSistema.TextoSecundario, Margin = new Padding(0, 3, 0, 3) };
            lblVencimiento = new Label { AutoSize = true, Font = new Font("Segoe UI", 10), ForeColor = TemaSistema.TextoSecundario, Margin = new Padding(0, 3, 0, 3) };

            Label lblTituloAutorizados = new Label
            {
                Text = "Personas Autorizadas (DNI):",
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Underline | FontStyle.Bold),
                ForeColor = TemaSistema.Texto,
                Margin = new Padding(0, 10, 0, 3)
            };

            lblAutorizados = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(600, 0), // ancho fijo, alto libre: envuelve en vez de cortar la lista
                Font = new Font("Segoe UI", 10),
                ForeColor = TemaSistema.TextoSecundario,
                Margin = new Padding(0, 0, 0, 5)
            };

            pnlFicha.Controls.Add(lblCliente);
            pnlFicha.Controls.Add(lblEstado);
            pnlFicha.Controls.Add(lblLimiteDeuda);
            pnlFicha.Controls.Add(lblVencimiento);
            pnlFicha.Controls.Add(lblTituloAutorizados);
            pnlFicha.Controls.Add(lblAutorizados);

            this.Controls.Add(pnlFicha);
            this.Controls.Add(tblHeader);

            this.SizeChanged += (s, e) =>
            {
                int anchoDisponible = System.Math.Max(this.ClientSize.Width - pnlFicha.Padding.Horizontal - 40, 100);
                lblAutorizados.MaximumSize = new Size(anchoDisponible, 0);
            };
        }

        public void CargarDatos(CuentaCorrienteDTO cc)
        {
            if (cc == null) return;

            lblNombreCuenta.Text = cc.NombreCuentaCorriente;

            // Lógica de color para el saldo
            lblSaldoActual.Text = $"Saldo Actual: {cc.Saldo:C}";
            lblSaldoActual.ForeColor = cc.Saldo < 0 ? Color.Firebrick : Color.SeaGreen;

            lblCliente.Text = $"Cliente: {cc.NombreCliente} (ID: {cc.ClienteId})";
            lblEstado.Text = $"Estado de Cuenta: {cc.EstadoDescripcionCtaCte}";

            string limite = cc.LimiteDeudaActivo ? $"{cc.LimiteDeuda:C}" : "Sin límite definido";
            lblLimiteDeuda.Text = $"Límite de Deuda: {limite}";

            lblVencimiento.Text = cc.FechaVencimiento.HasValue
                ? $"Vencimiento: {cc.FechaVencimiento.Value:dd/MM/yyyy}"
                : "Vencimiento: No definido";

            if (cc.DniAutorizados != null && cc.DniAutorizados.Any())
                lblAutorizados.Text = string.Join("  |  ", cc.DniAutorizados);
            else
                lblAutorizados.Text = "No hay DNI autorizados registrados.";
        }
    }
}