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

        // Remove or comment out the call to InitializeComponent() in the constructor,
        // since this UserControl does not use a designer file and all controls are created manually.

        public PanelMovimientoGeneral()
        {
            //InitializeComponent(); // <-- Remove or comment out this line
            CrearControlesVisuales();
        }

        private void CrearControlesVisuales()
        {
            this.Height = 120;
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
            pnlIzquierdo.Controls.AddRange(new Control[] { lblNumero, lblFecha, lblTipoMovimiento });

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
            lblEstado = new Label { AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), Margin = new Padding(0, 5, 0, 0) };
            pnlDerecho.Controls.AddRange(new Control[] { lblMonto, lblEstado });

            tblHeader.Controls.Add(pnlIzquierdo, 0, 0);
            tblHeader.Controls.Add(pnlDerecho, 1, 0);

            this.Controls.Add(tblHeader);
        }

        public void CargarDatos(MovimientoHelperDTO mov)
        {
            lblNumero.Text = $"Movimiento N°: {mov.NumeroMovimiento}";
            lblFecha.Text = $"Fecha: {mov.FechaMovimiento:dd/MM/yyyy HH:mm}";

            // Signo y color según Ingreso/Egreso (el monto en BD siempre viaja en positivo,
            // ver MovimientoService.cs: "Monto = Math.Abs(monto)").
            bool esIngreso = mov.TipoMovimiento == (int)TipoMovimiento.Ingreso;

            lblMonto.Text = (esIngreso ? "+ " : "- ") + mov.Monto.ToString("C2");
            lblMonto.ForeColor = esIngreso ? Color.SeaGreen : Color.Firebrick;

            if (mov.EstaEliminado)
            {
                lblEstado.Text = "ELIMINADO";
                lblEstado.ForeColor = Color.Red;
            }
            else
            {
                lblEstado.Text = "ACTIVO";
                lblEstado.ForeColor = Color.SeaGreen;
            }

            lblTipoMovimiento.Text = $"Tipo: {mov.TipoMovimientoDescripcion} | Detalle: {mov.TipoMovimientoDetalleDescripcion}";
        }
    }
}