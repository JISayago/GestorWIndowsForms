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

            contenedorPadre.BackColor = SystemColors.ButtonFace;
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

                // CAMBIO: De Color.White a un gris muy oscuro
                ForeColor = Color.FromArgb(40, 40, 40),

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
                Color.DodgerBlue,
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
                Color.SeaGreen,
                420
            );

            // Botón de Ocultar/Mostrar (Ubicado arriba a la derecha)
            Button btnCensura = new Button
            {
                Text = "*",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(30, 30),
                Location = new Point(tarjeta.Width - 35, 5),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                BackColor = Color.White,
                ForeColor = Color.Gray,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            btnCensura.FlatAppearance.BorderSize = 0;

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
                BackColor = Color.White,
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
                ForeColor = Color.Gray
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
                ForeColor = Color.FromArgb(40, 40, 40)
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

                // CAMBIO: De Color.LightGray a DimGray (gris medio-oscuro)
                ForeColor = Color.DimGray,

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
                // Al iniciar, si no hay nada, ponemos el primer guion
                Text = string.IsNullOrWhiteSpace(_datosTurno.NotasTurno) ? "- " : _datosTurno.NotasTurno,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            // --- EVENTO DE FORMATEO EN VIVO ---
            txtNotas.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    // Evitamos el sonido de "beep" de Windows al presionar Enter
                    e.SuppressKeyPress = true;

                    // Añadimos una nueva línea y el guion automáticamente
                    string nuevaLinea = Environment.NewLine + "- ";
                    int seleccionIndex = txtNotas.SelectionStart;

                    // Insertar el guion en la posición actual del cursor
                    txtNotas.Text = txtNotas.Text.Insert(seleccionIndex, nuevaLinea);

                    // Reposicionar el cursor al final del nuevo guion
                    txtNotas.SelectionStart = seleccionIndex + nuevaLinea.Length;
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
                // Limpiamos las líneas: quitamos espacios, y filtramos las que solo sean un "-" o estén vacías
                var lineasValidas = txtNotas.Lines
                    .Select(l => l.Trim())
                    .Where(l => !string.IsNullOrWhiteSpace(l) && l != "-")
                    .Select(l => l.StartsWith("-") ? l : "- " + l)
                    .ToList();

                string textoLimpio = string.Join(Environment.NewLine, lineasValidas);

                // Actualizamos la UI para que el usuario vea la "limpieza"
                txtNotas.Text = string.IsNullOrEmpty(textoLimpio) ? "- " : textoLimpio;
                _datosTurno.NotasTurno = textoLimpio;

                // Guardar en DB
                _pantallaPrincipalServicio.GuardarNotasRapidas(textoLimpio, _datosTurno.UsuarioLogeado);

                MessageBox.Show("Nota guardada.");
            };

            pNotasContainer.Controls.Add(lblNotas);
            pNotasContainer.Controls.Add(txtNotas);
            pNotasContainer.Controls.Add(btnGuardarNotas);

            var notasGuardadas = _pantallaPrincipalServicio.ObtenerNotasRapidas();
            if(notasGuardadas != null && !string.IsNullOrWhiteSpace(notasGuardadas.ToString()))
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
            // Lógica de censura de montos
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