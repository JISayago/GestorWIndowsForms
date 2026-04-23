using Presentacion.FBase.Helpers;
using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Notificaciones
{
    public class PanelDatosTurno : UserControl
    {
        private FlowLayoutPanel panelSuperior;
        private Panel pNotasContainer;
        private TextBox txtNotas;
        private Button btnGuardarNotas;
        private Label lblNotas;

        public void CargarResumenTurno(Control contenedorPadre, DatosTurnoDTO datosTurno)
        {
            contenedorPadre.BackColor = Color.FromArgb(45, 45, 48);
            contenedorPadre.Controls.Clear();

            // =========================
            // 🔹 PANEL SUPERIOR
            // =========================
            panelSuperior = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                BackColor = Color.Transparent
            };

            Label lblSeccion = new Label
            {
                Text = "RESUMEN DEL TURNO ACTUAL",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Margin = new Padding(0, 0, 0, 20),
                AutoSize = true
            };

            panelSuperior.Controls.Add(lblSeccion);
            panelSuperior.SetFlowBreak(lblSeccion, true);

            //CARGAR LOS VALORES REALES DESDE EL SERVICIO
            panelSuperior.Controls.Add(CrearTarjeta
                (
                "ESTADO DE CAJA",
                $"Monto Inicial: $ 10.000,00\n" +
                "Ventas Efec.:   $ 45.200,50\n" +
                "TOTAL CAJA:     $ 55.200,50",
                Color.SeaGreen, 420)
                );

            panelSuperior.Controls.Add(CrearTarjeta
                (
                "SESIÓN ACTIVA",
                "Usuario:        ADMIN_Jose\n" +
                "Ingreso:        08:00 AM\n" +
                "Transcurrido:   08h 48m",
                Color.DodgerBlue, 420)
                );

            // =========================
            // 🔹 PANEL NOTAS
            // =========================
            pNotasContainer = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 260,
                BackColor = Color.Transparent
            };

            lblNotas = new Label
            {
                Text = "NOTAS PARA EL SIGUIENTE TURNO",
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Top = 10,
                Left = 20,
                AutoSize = true
            };

            txtNotas = new TextBox
            {
                Multiline = true,
                Height = 150,
                Top = 35,
                Left = 20,
                Font = new Font("Segoe UI", 11),
                ScrollBars = ScrollBars.Vertical,
                AcceptsReturn = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Text = "- Falta cambiar el rollo de la impresora.\r\n- Pedido de Juan Pérez pagado."
            };

            txtNotas.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    string nl = Environment.NewLine + "- ";
                    int pos = txtNotas.SelectionStart;
                    txtNotas.SelectedText = nl;
                    txtNotas.SelectionStart = pos + nl.Length;
                }
            };

            btnGuardarNotas = new Button
            {
                Text = "GUARDAR NOTAS",
                Width = 200,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            btnGuardarNotas.Click += (s, e) =>
            {
                var lineas = txtNotas.Lines
                    .Select(l => l.Trim())
                    .Where(l => !string.IsNullOrWhiteSpace(l) && l != "-")
                    .Select(l => l.StartsWith("-") ? l : "- " + l);

                txtNotas.Text = string.Join(Environment.NewLine, lineas);
                MessageBox.Show("Notas guardadas.");
                //GUARDA EN DB LA NOTA PARA QUE EL SIGUIENTE TURNO LA VEA
            };

            pNotasContainer.Controls.Add(lblNotas);
            pNotasContainer.Controls.Add(txtNotas);
            pNotasContainer.Controls.Add(btnGuardarNotas);

            // =========================
            // 🔹 AGREGAR AL FORM
            // =========================
            contenedorPadre.Controls.Add(panelSuperior);
            contenedorPadre.Controls.Add(pNotasContainer);

            contenedorPadre.Resize += (s, e) => AjustarLayout(contenedorPadre);
            AjustarLayout(contenedorPadre);
        }

        private void AjustarLayout(Control padre)
        {
            if (pNotasContainer == null) return;

            int ancho = padre.ClientSize.Width - 40;

            // Ajustar ancho del textbox respetando margen izquierdo
            txtNotas.Width = ancho;

            // Posicionar botón alineado al textbox
            btnGuardarNotas.Top = txtNotas.Bottom + 10;
            btnGuardarNotas.Left = txtNotas.Left + txtNotas.Width - btnGuardarNotas.Width;
        }

        private Panel CrearTarjeta(string titulo, string contenido, Color colorIndicador, int ancho)
        {
            Panel p = new Panel
            {
                Width = ancho,
                Height = 130,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 20, 20)
            };

            Panel indicador = new Panel
            {
                Dock = DockStyle.Left,
                Width = 12,
                BackColor = colorIndicador
            };

            Label lblT = new Label
            {
                Text = titulo.ToUpper(),
                Top = 12,
                Left = 25,
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.Gray
            };

            Label lblC = new Label
            {
                Text = contenido,
                Top = 40,
                Left = 25,
                Width = ancho - 50,
                Height = 80,
                Font = new Font("Consolas", 13f, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40)
            };

            p.Controls.AddRange(new Control[] { lblC, lblT, indicador });

            return p;
        }
    }
}