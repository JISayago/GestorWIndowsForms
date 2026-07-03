using Presentacion.FBase.Helpers;
using Servicios.LogicaNegocio.PantallaPrincipal;
using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Notificaciones
{
    public class PanelDatosTurno : UserControl
    {
        #region Configuración de Colores (Temas)

        // Modifica estos valores por defecto o cámbialos desde afuera antes de llamar a CargarResumenTurno
        public Color ColorFondoContenedor { get; set; } = TemaSistema.Fondo;
        public Color ColorTarjetaFondo { get; set; } = TemaSistema.Fondo;
        public Color ColorTextoPrincipal { get; set; } = TemaSistema.Texto;
        public Color ColorTextoSecundario { get; set; } = TemaSistema.TextoSecundario;
        public Color ColorTextoGrisClaro { get; set; } = Color.Gray;

        // Colores de acento o indicadores laterales
        public Color ColorIndicadorCaja { get; set; } = TemaSistema.Fondo;
        public Color ColorIndicadorSesion { get; set; } = TemaSistema.Fondo;
        public Color ColorBotonGuardarFondo { get; set; } = TemaSistema.Seleccion;
        public Color ColorBotonGuardarTexto { get; set; } = Color.Black;

        #endregion

        #region Campos y Propiedades

        private IPantallaPrincipalServicio _pantallaPrincipalServicio;
        private FlowLayoutPanel panelSuperior;
        private Panel pNotasContainer;
        private TextBox txtNotas;
        private Button btnGuardarNotas;
        private Label lblNotas;
        private System.Windows.Forms.Timer timerReloj;
        private DatosTurnoDTO _datosTurno;
        private string _textoNotasLimpio = string.Empty;

        private Label lblContenidoSesion;
        private Label lblContenidoCaja;

        // Estado de censura de dinero (Estilo Home Banking)
        private bool _informacionCensurada = false;

        #endregion

        #region Métodos Públicos (Interfaz de Control)

        public void CargarResumenTurno(Control contenedorPadre, DatosTurnoDTO datosTurno)
        {
            _pantallaPrincipalServicio = new PantallaPrincipalServicio();
            _datosTurno = datosTurno;

            contenedorPadre.BackColor = this.ColorFondoContenedor;
            contenedorPadre.Controls.Clear();

            // Configuración del FlowLayout Principal
            panelSuperior = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                BackColor = Color.Transparent
            };

            // Título de la Sección
            Label lblSeccion = new Label
            {
                Text = "RESUMEN DEL TURNO ACTUAL",
                ForeColor = this.ColorTextoPrincipal,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Margin = new Padding(0, 0, 0, 20),
                AutoSize = true
            };

            panelSuperior.Controls.Add(lblSeccion);
            panelSuperior.SetFlowBreak(lblSeccion, true);

            // 1. Tarjeta de Caja (Con lógica de censura y botón a la derecha)
            var tarjetaCaja = CrearTarjetaCaja(_datosTurno);
            panelSuperior.Controls.Add(tarjetaCaja);

            // 2. Tarjeta de Sesión
            var tarjetaSesion = CrearTarjeta(
                "SESIÓN ACTIVA",
                ObtenerTextoSesion(),
                this.ColorIndicadorSesion,
                420
            );

            // Extraer referencia al label de sesión para el timer
            lblContenidoSesion = tarjetaSesion.Controls.OfType<Label>().FirstOrDefault(l => l.Top == 40);
            panelSuperior.Controls.Add(tarjetaSesion);

            // Inicializar Reloj de Tiempo Transcurrido
            ConfigurarTimerReloj();

            // 3. Panel de Notas (Inferior)
            ConfigurarPanelNotas();

            contenedorPadre.Controls.Add(panelSuperior);
            contenedorPadre.Controls.Add(pNotasContainer);

            // Evento de Redimensionamiento
            contenedorPadre.Resize += (s, e) => AjustarLayout(contenedorPadre);
            AjustarLayout(contenedorPadre);
        }

        public void ActualizarSoloTextoCaja(DatosTurnoDTO nuevosDatos)
        {
            this._datosTurno = nuevosDatos;

            if (lblContenidoCaja != null && !lblContenidoCaja.IsDisposed)
            {
                lblContenidoCaja.Text = ObtenerTextoCaja(nuevosDatos);
            }
        }

        #endregion

        #region Fábrica de Controles (UI Factory)

        private Panel CrearTarjetaCaja(DatosTurnoDTO datosTurno)
        {
            var tarjeta = CrearTarjeta(
                "ESTADO DE CAJA",
                ObtenerTextoCaja(datosTurno),
                this.ColorIndicadorCaja,
                420
            );

            // Replace this incorrect object initializer usage inside CrearTarjetaCaja:

            Button btnCensura = new Button
            {
                Text = "*",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(30, 30),
                Location = new Point(tarjeta.Width - 35, 5),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                BackColor = TemaSistema.Seleccion,
                ForeColor = Color.Black,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
            };

            // Set FlatAppearance.BorderSize after construction:
            btnCensura.FlatAppearance.BorderSize = 1;
            btnCensura.FlatAppearance.BorderColor = Color.Black;

            btnCensura.Click += (s, e) =>
            {
                _informacionCensurada = !_informacionCensurada;
                btnCensura.Text = _informacionCensurada ? "👁" : "*";
                ActualizarSoloTextoCaja(_datosTurno);
            };

            tarjeta.Controls.Add(btnCensura);
            btnCensura.BringToFront();

            // Guardamos la referencia para actualizaciones futuras
            lblContenidoCaja = tarjeta.Controls.OfType<Label>().FirstOrDefault(l => l.Top == 40);

            return tarjeta;
        }

        private Panel CrearTarjeta(string titulo, string contenido, Color colorIndicador, int ancho)
        {
            // Panel Contenedor
            Panel p = new Panel
            {
                Width = ancho,
                Height = 130,
                BackColor = this.ColorTarjetaFondo,
                Margin = new Padding(0, 0, 20, 20)
            };

            // Indicador Lateral (Color)
            Panel indicador = new Panel
            {
                Dock = DockStyle.Left,
                Width = 12,
                BackColor = colorIndicador
            };

            // Label de Título (Superior)
            Label lblT = new Label
            {
                Text = titulo.ToUpper(),
                Top = 12,
                Left = 25,
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = this.ColorTextoGrisClaro
            };

            // Label de Contenido (Valores numéricos)
            Label lblC = new Label
            {
                Text = contenido,
                Top = 40,
                Left = 25,
                Width = ancho - 50,
                Height = 80,
                Font = new Font("Consolas", 13f, FontStyle.Bold),
                ForeColor = this.ColorTextoPrincipal
            };

            p.Controls.Add(lblC);
            p.Controls.Add(lblT);
            p.Controls.Add(indicador);

            return p;
        }

        private void ConfigurarPanelNotas()
        {
            pNotasContainer = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 260,
                BackColor = Color.Transparent
            };

            lblNotas = new Label
            {
                Text = "NOTAS PARA EL SIGUIENTE TURNO",
                ForeColor = this.ColorTextoSecundario,
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
                Text = string.IsNullOrWhiteSpace(_datosTurno.NotasTurno) ? "- " : _datosTurno.NotasTurno,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = this.ColorTarjetaFondo,
                ForeColor = this.ColorTextoPrincipal
            };

            // --- EVENTO DE FORMATEO EN VIVO ---
            txtNotas.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    string nuevaLinea = Environment.NewLine + "- ";
                    int seleccionIndex = txtNotas.SelectionStart;
                    txtNotas.Text = txtNotas.Text.Insert(seleccionIndex, nuevaLinea);
                    txtNotas.SelectionStart = seleccionIndex + nuevaLinea.Length;
                }
            };

            btnGuardarNotas = new Button
            {
                Text = "GUARDAR NOTAS",
                Width = 200,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = this.ColorBotonGuardarFondo,
                ForeColor = this.ColorBotonGuardarTexto,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            btnGuardarNotas.Click += (s, e) =>
            {
                var lineasValidas = txtNotas.Lines
                    .Select(l => l.Trim())
                    .Where(l => !string.IsNullOrWhiteSpace(l) && l != "-")
                    .Select(l => l.StartsWith("-") ? l : "- " + l)
                    .ToList();

                string textoLimpio = string.Join(Environment.NewLine, lineasValidas);

                txtNotas.Text = string.IsNullOrEmpty(textoLimpio) ? "- " : textoLimpio;
                _datosTurno.NotasTurno = textoLimpio;

                _pantallaPrincipalServicio.GuardarNotasRapidas(textoLimpio, _datosTurno.UsuarioLogeado);

                MessageBox.Show("Nota guardada.");
            };

            pNotasContainer.Controls.Add(lblNotas);
            pNotasContainer.Controls.Add(txtNotas);
            pNotasContainer.Controls.Add(btnGuardarNotas);

            var notasGuardadas = _pantallaPrincipalServicio.ObtenerNotasRapidas();
            if (notasGuardadas != null && !string.IsNullOrWhiteSpace(notasGuardadas.ToString()))
            {
                txtNotas.Text = notasGuardadas.ToString();
            }
        }

        #endregion

        #region Helpers de Formato y Lógica

        private void ConfigurarTimerReloj()
        {
            if (timerReloj != null) { timerReloj.Stop(); timerReloj.Dispose(); }

            timerReloj = new System.Windows.Forms.Timer { Interval = 1000 };
            timerReloj.Tick += (s, e) =>
            {
                if (lblContenidoSesion != null && !lblContenidoSesion.IsDisposed)
                    lblContenidoSesion.Text = ObtenerTextoSesion();
            };
            timerReloj.Start();
        }

        public string ObtenerTextoCaja(DatosTurnoDTO datos)
        {
            string mInicial = _informacionCensurada ? "****" : datos.MontoInicial.ToString("N2");
            string mIngresos = _informacionCensurada ? "****" : datos.Ingresos.ToString("N2");
            string mTotal = _informacionCensurada ? "****" : datos.TotalCaja.ToString("N2");

            return $"Monto Inicial:    $ {mInicial}\n" +
                   $"Total Ingresos:   $ {mIngresos}\n" +
                   $"TOTAL CAJA:       $ {mTotal}";
        }

        private string ObtenerTextoSesion()
        {
            if (_datosTurno == null) return "Cargando...";

            TimeSpan transcurrido = DateTime.Now - _datosTurno.HoraIngresoUsuario;
            string tiempoStr = $"{(int)transcurrido.TotalHours:00}h {transcurrido.Minutes:00}m {transcurrido.Seconds:00}s";

            return $"Usuario:        {_datosTurno.UsuarioLogeado}\n" +
                   $"Ingreso:        {_datosTurno.HoraIngresoUsuario:HH:mm:ss}\n" +
                   $"Transcurrido:   {tiempoStr}";
        }

        private void AjustarLayout(Control padre)
        {
            if (txtNotas == null) return;

            txtNotas.Width = padre.ClientSize.Width - 40;
            btnGuardarNotas.Top = txtNotas.Bottom + 10;
            btnGuardarNotas.Left = txtNotas.Left + txtNotas.Width - btnGuardarNotas.Width;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) { timerReloj?.Stop(); timerReloj?.Dispose(); }
            base.Dispose(disposing);
        }

        #endregion
    }
}