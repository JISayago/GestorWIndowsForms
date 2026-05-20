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
            this.BackColor = Color.White;

            // --- HEADER ---
            TableLayoutPanel tblHeader = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 60,
                ColumnCount = 2
            };
            tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
            tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));

            lblNombreCuenta = new Label { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 13, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft };
            lblSaldoActual = new Label { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 14, FontStyle.Bold), TextAlign = ContentAlignment.MiddleRight };

            tblHeader.Controls.Add(lblNombreCuenta, 0, 0);
            tblHeader.Controls.Add(lblSaldoActual, 1, 0);

            // --- FICHA DE DATOS ---
            Panel pnlFicha = new Panel
            {
                Dock = DockStyle.Top,
                Height = 350,
                Margin = new Padding(0, 20, 0, 0),
                Padding = new Padding(25),
                BackColor = Color.FromArgb(250, 251, 252),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblCliente = new Label { Location = new Point(25, 25), AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold) };
            lblEstado = new Label { Location = new Point(25, 60), AutoSize = true, Font = new Font("Segoe UI", 10) };
            lblLimiteDeuda = new Label { Location = new Point(25, 95), AutoSize = true, Font = new Font("Segoe UI", 10) };
            lblVencimiento = new Label { Location = new Point(25, 130), AutoSize = true, Font = new Font("Segoe UI", 10) };

            Label lblTituloAutorizados = new Label
            {
                Text = "Personas Autorizadas (DNI):",
                Location = new Point(25, 180),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Underline | FontStyle.Bold)
            };

            lblAutorizados = new Label
            {
                Location = new Point(25, 205),
                Size = new Size(600, 100),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.DarkSlateGray
            };

            pnlFicha.Controls.AddRange(new Control[] {
                lblCliente, lblEstado, lblLimiteDeuda, lblVencimiento, lblTituloAutorizados, lblAutorizados
            });

            this.Controls.Add(pnlFicha);
            this.Controls.Add(tblHeader);
        }

        public void CargarDatos(CuentaCorrienteDTO cc)
        {
            if (cc == null) return;

            lblNombreCuenta.Text = $"💳 {cc.NombreCuentaCorriente}";

            // Lógica de color para el saldo
            lblSaldoActual.Text = $"Saldo Actual: {cc.Saldo:C}";
            lblSaldoActual.ForeColor = cc.Saldo < 0 ? Color.Firebrick : Color.SeaGreen;

            lblCliente.Text = $"👤 Cliente: {cc.NombreCliente} (ID: {cc.ClienteId})";
            lblEstado.Text = $"📌 Estado de Cuenta: {cc.EstadoCtaCte}";

            string limite = cc.LimiteDeudaActivo ? $"{cc.LimiteDeuda:C}" : "Sin límite definido";
            lblLimiteDeuda.Text = $"⚠️ Límite de Deuda: {limite}";

            lblVencimiento.Text = cc.FechaVencimiento.HasValue
                ? $"📅 Vencimiento: {cc.FechaVencimiento.Value:dd/MM/yyyy}"
                : "📅 Vencimiento: No definido";

            if (cc.DniAutorizados != null && cc.DniAutorizados.Any())
                lblAutorizados.Text = string.Join("  |  ", cc.DniAutorizados);
            else
                lblAutorizados.Text = "No hay DNI autorizados registrados.";
        }
    }
}