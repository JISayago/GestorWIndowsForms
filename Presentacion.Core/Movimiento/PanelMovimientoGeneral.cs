using Presentacion.FBase.Helpers;
using Servicios.Helpers.Movimiento;
using Servicios.LogicaNegocio.Movimiento.DTO;
using System.Windows.Forms;
using System.Drawing;

namespace TuProyecto.Presentacion.Paneles
{
    public partial class PanelMovimientoGeneral : UserControl
    {
        // Declaramos los controles visuales
        private Label lblNumero;
        private Label lblFecha;
        private Label lblMonto;
        private Label lblEstado;
        private Label lblTipoMovimiento;
        private Label lblReferencia;

        // Remove or comment out the call to InitializeComponent() in the constructor,
        // since this UserControl does not use a designer file and all controls are created manually.

        public PanelMovimientoGeneral()
        {
            //InitializeComponent(); // <-- Remove or comment out this line
            CrearControlesVisuales();
        }

        private void CrearControlesVisuales()
        {
            this.Height = 145;
            this.Padding = new Padding(20, 10, 20, 10);
            this.BackColor = TemaSistema.FondoControl;

            // Tabla invisible para organizar Izquierda y Derecha
            TableLayoutPanel tblHeader = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                BackColor = Color.Transparent
            };
            tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70f)); // Texto
            tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f)); // Monto/Estado

            // Contenedor Izquierdo
            Panel pnlIzquierdo = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            lblNumero = new Label { Text = "Movimiento N°:", Location = new Point(0, 5), AutoSize = true, Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = TemaSistema.Texto };
            lblFecha = new Label { Location = new Point(0, 40), AutoSize = true, Font = new Font("Segoe UI", 10), ForeColor = TemaSistema.TextoSecundario };
            lblTipoMovimiento = new Label { Location = new Point(0, 70), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = TemaSistema.TextoSecundario };
            // Referencia cruzada a la entidad asociada (Venta / Gasto / Cuenta Corriente), para ubicarla sin salir del formulario
            lblReferencia = new Label { Location = new Point(0, 95), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Italic), ForeColor = TemaSistema.TextoSecundario };
            pnlIzquierdo.Controls.AddRange(new Control[] { lblNumero, lblFecha, lblTipoMovimiento, lblReferencia });

            // Contenedor Derecho (Alineado a la derecha)
            FlowLayoutPanel pnlDerecho = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                RightToLeft = RightToLeft.Yes, // Esto empuja todo a la derecha
                BackColor = Color.Transparent
            };
            lblMonto = new Label { AutoSize = true, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = TemaSistema.Primario };
            // Solo se muestra cuando hay algo que advertir (ELIMINADO); mostrar "ACTIVO" todo el
            // tiempo era ruido visual redundante, ya que activo es el estado esperado por defecto.
            lblEstado = new Label { AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), Margin = new Padding(0, 5, 0, 0), Visible = false };
            pnlDerecho.Controls.AddRange(new Control[] { lblMonto, lblEstado });

            tblHeader.Controls.Add(pnlIzquierdo, 0, 0);
            tblHeader.Controls.Add(pnlDerecho, 1, 0);

            this.Controls.Add(tblHeader);
        }

        public void CargarDatos(MovimientoHelperDTO mov)
        {
            lblNumero.Text = $"Movimiento N°: {mov.NumeroMovimiento}";
            lblFecha.Text = $"Fecha: {mov.FechaMovimiento:dd/MM/yyyy HH:mm}";

            // Color por signo: mismo criterio que ya usa PanelMovimientoCuentaCorriente para el saldo,
            // así el monto se lee de un vistazo como ingreso o egreso.
            // Math.Abs: si Monto ya viene negativo en BD para egresos, formatear con :C directamente
            // agrega el signo negativo de nuevo según el patrón de la cultura (a veces DESPUÉS del
            // número, ej. "100,00-"), duplicando o corriendo el signo. Se normaliza a valor absoluto
            // y el signo +/- lo controla únicamente este código.
            bool esIngreso = mov.TipoMovimiento == (int)TipoMovimiento.Ingreso;
            string signo = esIngreso ? "+" : "-";
            lblMonto.Text = $"{signo} {System.Math.Abs(mov.Monto):C}";
            lblMonto.ForeColor = esIngreso ? Color.SeaGreen : Color.Firebrick;

            // Solo se muestra el badge de estado cuando el movimiento está eliminado (excepción a
            // señalar); en el caso normal (activo) se mantiene oculto para no repetir información
            // implícita y despejar el encabezado.
            if (mov.EstaEliminado)
            {
                lblEstado.Text = "ELIMINADO";
                lblEstado.ForeColor = Color.Red;
                lblEstado.Visible = true;
            }
            else
            {
                lblEstado.Visible = false;
            }

            lblTipoMovimiento.Text = $"Tipo: {mov.TipoMovimientoDescripcion} | Detalle: {mov.TipoMovimientoDetalleDescripcion}";

            lblReferencia.Text = mov.EntidadId.HasValue
                ? $"Referencia: Entidad #{mov.EntidadId.Value} ({(mov.TipoEntidad.HasValue ? ((TipoEntidadMovimiento)mov.TipoEntidad.Value).ToString() : "N/D")})"
                : "Sin entidad asociada";
        }
    }
}