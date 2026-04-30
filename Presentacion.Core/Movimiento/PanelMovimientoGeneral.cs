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
            this.Padding = new Padding(20);
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.BorderStyle = BorderStyle.None;

            lblNumero = new Label { Location = new Point(20, 15), AutoSize = true, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            lblMonto = new Label { AutoSize = true, Font = new Font("Segoe UI", 15, FontStyle.Bold), ForeColor = Color.MidnightBlue };
            lblEstado = new Label { AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold) };
            lblFecha = new Label { Location = new Point(20, 50), AutoSize = true, Font = new Font("Segoe UI", 10) };
            lblTipoMovimiento = new Label { Location = new Point(20, 80), AutoSize = true, Font = new Font("Segoe UI", 9) };

            this.SizeChanged += (s, e) => {
                // Margen derecho deseado
                int margenDerecho = 25;

                // Posicionamos el Estado (Verde) primero a la derecha
                lblEstado.Location = new Point(this.Width - lblEstado.Width - margenDerecho, 18);

                // Posicionamos el Monto (Azul) a la izquierda del Estado
                // Le damos un espacio extra (por ejemplo 150px) para que no se choquen
                lblMonto.Location = new Point(lblEstado.Left - lblMonto.Width - 40, 15);
            };

            // Asegúrate de que lblEstado tenga el anclaje correcto para evitar saltos raros
            lblEstado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblMonto.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            this.Controls.AddRange(new Control[] { lblNumero, lblMonto, lblEstado, lblFecha, lblTipoMovimiento });
        }

        public void CargarDatos(MovimientoHelperDTO mov)
        {
            lblNumero.Text = $"Movimiento N°: {mov.NumeroMovimiento}";
            lblFecha.Text = $"Fecha: {mov.FechaMovimiento:dd/MM/yyyy HH:mm}";
            lblMonto.Text = mov.Monto.ToString();

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

            lblTipoMovimiento.Text = $"Tipo: {mov.TipoMovimiento} | Detalle: {mov.TipoMovimientoDetalle}";
        }
    }
}