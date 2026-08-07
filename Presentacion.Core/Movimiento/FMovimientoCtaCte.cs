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
        private Label lblTipoOperacion;
        private Label lblContacto;
        private Label lblAntiguedad;
        private DataGridView dgvHistorial;
        private Label lblSinHistorial;

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

            // Distingue de un vistazo si ESTE movimiento fue una carga de saldo (ingreso) o una
            // compra a cuenta (egreso) -> dato ya disponible en el Movimiento padre, sin necesitar
            // vincular la venta puntual (eso sí requeriría migración).
            lblTipoOperacion = new Label
            {
                Dock = DockStyle.Top,
                Height = 26,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

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
            // Teléfono/Email del cliente: ya existían en Persona, no se mostraban en este panel
            lblContacto = new Label { AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = TemaSistema.TextoSecundario, Margin = new Padding(0, 0, 0, 3) };
            lblEstado = new Label { AutoSize = true, Font = new Font("Segoe UI", 10), ForeColor = TemaSistema.TextoSecundario, Margin = new Padding(0, 3, 0, 3) };
            lblLimiteDeuda = new Label { AutoSize = true, Font = new Font("Segoe UI", 10), ForeColor = TemaSistema.TextoSecundario, Margin = new Padding(0, 3, 0, 3) };
            lblVencimiento = new Label { AutoSize = true, Font = new Font("Segoe UI", 10), ForeColor = TemaSistema.TextoSecundario, Margin = new Padding(0, 3, 0, 3) };
            // Antigüedad de la cuenta (FechaCreacion ya existía en la entidad, no se mostraba)
            lblAntiguedad = new Label { AutoSize = true, Font = new Font("Segoe UI", 10), ForeColor = TemaSistema.TextoSecundario, Margin = new Padding(0, 3, 0, 3) };

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
            pnlFicha.Controls.Add(lblContacto);
            pnlFicha.Controls.Add(lblEstado);
            pnlFicha.Controls.Add(lblLimiteDeuda);
            pnlFicha.Controls.Add(lblVencimiento);
            pnlFicha.Controls.Add(lblAntiguedad);
            pnlFicha.Controls.Add(lblTituloAutorizados);
            pnlFicha.Controls.Add(lblAutorizados);

            // --- HISTORIAL RECIENTE DE LA CUENTA ---
            // Da contexto temporal (movimientos previos de esta misma cuenta) sin necesitar
            // vincular la venta puntual que originó ESTE movimiento en particular.
            Panel pnlHistorial = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 15, 0, 0)
            };

            Label lblTituloHistorial = new Label
            {
                Text = "MOVIMIENTOS RECIENTES DE ESTA CUENTA",
                Dock = DockStyle.Top,
                Height = 24,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = TemaSistema.Texto
            };

            dgvHistorial = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoGenerateColumns = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                // Colores y sorteo por click los aplica AplicarTema (FBase), igual que el resto de grids.
            };

            lblSinHistorial = new Label
            {
                Text = "Esta cuenta no tiene otros movimientos registrados.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = TemaSistema.TextoSecundario,
                Visible = false
            };

            pnlHistorial.Controls.Add(lblSinHistorial);
            pnlHistorial.Controls.Add(dgvHistorial);
            pnlHistorial.Controls.Add(lblTituloHistorial);

            // El orden de agregado es vital para el Dock: Fill primero, después los Top
            // (el último agregado entre los "Top" queda más arriba de todos).
            this.Controls.Add(pnlHistorial);
            this.Controls.Add(pnlFicha);
            this.Controls.Add(lblTipoOperacion);
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

            bool esCarga = cc.TipoMovimientoPadre == (int)Servicios.Helpers.Movimiento.TipoMovimiento.Ingreso;
            lblTipoOperacion.Text = $"Este movimiento fue: {cc.TipoMovimientoPadreDescripcion}";
            lblTipoOperacion.ForeColor = esCarga ? Color.SeaGreen : Color.Firebrick;

            lblCliente.Text = string.IsNullOrWhiteSpace(cc.NumeroCliente)
                ? $"Cliente: {cc.NombreCliente} (ID: {cc.ClienteId})"
                : $"Cliente: {cc.NombreCliente}  —  N° Cliente: {cc.NumeroCliente}";

            var datosContacto = new System.Collections.Generic.List<string>();
            if (!string.IsNullOrWhiteSpace(cc.TelefonoCliente)) datosContacto.Add($"Tel: {cc.TelefonoCliente}");
            if (!string.IsNullOrWhiteSpace(cc.EmailCliente)) datosContacto.Add($"Email: {cc.EmailCliente}");
            lblContacto.Text = datosContacto.Any() ? string.Join("   ", datosContacto) : "Sin datos de contacto cargados";

            lblEstado.Text = $"Estado de Cuenta: {cc.EstadoDescripcionCtaCte}" + (cc.ConDeuda ? " (con deuda pendiente)" : "");

            string limite = cc.LimiteDeudaActivo ? $"{cc.LimiteDeuda:C}" : "Sin límite definido";
            lblLimiteDeuda.Text = $"Límite de Deuda: {limite}";

            lblVencimiento.Text = cc.FechaVencimiento.HasValue
                ? $"Vencimiento: {cc.FechaVencimiento.Value:dd/MM/yyyy}"
                : "Vencimiento: No definido";

            lblAntiguedad.Text = cc.FechaCreacion.HasValue
                ? $"Cuenta creada: {cc.FechaCreacion.Value:dd/MM/yyyy}"
                : "Fecha de creación no registrada";

            if (cc.DniAutorizados != null && cc.DniAutorizados.Any())
                lblAutorizados.Text = string.Join("  |  ", cc.DniAutorizados);
            else
                lblAutorizados.Text = "No hay DNI autorizados registrados.";

            bool tieneHistorial = cc.HistorialReciente != null && cc.HistorialReciente.Any();
            dgvHistorial.Visible = tieneHistorial;
            lblSinHistorial.Visible = !tieneHistorial;

            if (tieneHistorial)
            {
                dgvHistorial.DataSource = cc.HistorialReciente;
                ConfigurarColumnasHistorial(dgvHistorial);
            }
        }

        // Cura las columnas autogeneradas del historial: oculta el campo crudo TipoMovimiento
        // (ya viene resuelto en TipoMovimientoDescripcion) y formatea fecha/moneda.
        private void ConfigurarColumnasHistorial(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;

            void Ocultar(string nombre)
            {
                if (grilla.Columns.Contains(nombre)) grilla.Columns[nombre].Visible = false;
            }

            void Config(string nombre, string header, string formato = null, int fillWeight = 100)
            {
                if (!grilla.Columns.Contains(nombre)) return;
                var col = grilla.Columns[nombre];
                col.HeaderText = header;
                col.FillWeight = fillWeight;
                if (formato != null) col.DefaultCellStyle.Format = formato;
            }

            Ocultar("TipoMovimiento"); // se muestra la versión legible (TipoMovimientoDescripcion)

            Config("NumeroMovimiento", "N° Movimiento", fillWeight: 140);
            Config("FechaMovimiento", "Fecha", "dd/MM/yyyy HH:mm", fillWeight: 110);
            Config("TipoMovimientoDescripcion", "Tipo", fillWeight: 100);
            Config("Monto", "Monto", "C2", fillWeight: 90);
        }
    }
}