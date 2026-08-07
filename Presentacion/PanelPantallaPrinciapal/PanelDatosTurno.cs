using Presentacion.FBase.Helpers;
using Servicios.LogicaNegocio.PantallaPrincipal;
using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Notificaciones
{
    public class PanelDatosTurno : UserControl
    {
        #region Colores

        public Color ColorFondoContenedor { get; set; } = TemaSistema.Fondo;
        public Color ColorTarjetaFondo { get; set; } = TemaSistema.Alternado;
        public Color ColorTextoPrincipal { get; set; } = TemaSistema.Texto;
        public Color ColorTextoSecundario { get; set; } = TemaSistema.TextoSecundario;
        public Color ColorTextoGrisClaro { get; set; } = Color.Gray;
        public Color ColorIndicadorCaja { get; set; } = TemaSistema.Primario;
        public Color ColorIndicadorSesion { get; set; } = TemaSistema.Acento;
        public Color ColorBotonGuardarFondo { get; set; } = TemaSistema.Seleccion;
        public Color ColorBotonGuardarTexto { get; set; } = Color.Black;

        #endregion

        private IPantallaPrincipalServicio _pantallaPrincipalServicio;
        private TableLayoutPanel _layoutRoot;
        private Panel pNotasContainer;
        private TextBox txtNotas;
        private Button btnGuardarNotas;
        private Label lblNotas;
        private Label lblUltimaActualizacion;
        private Label lblEstadoCaja;
        private Label lblEstadoNotas;
        private Button btnCensura;
        private System.Windows.Forms.Timer timerReloj;
        private System.Windows.Forms.Timer timerAutosaveNotas;
        private DatosTurnoDTO _datosTurno;
        private Label lblContenidoSesion;
        private Label lblContenidoCaja;
        private EventHandler _resizeHandler;
        private readonly ToolTip _toolTip = new ToolTip();
        private bool _informacionCensurada = false;
        private bool _notasDirty = false;
        private bool _cargandoNotas = false;
        private string _ultimoTextoGuardado = "";

        public void CargarResumenTurno(Control contenedorPadre, DatosTurnoDTO datosTurno)
        {
            _pantallaPrincipalServicio = new PantallaPrincipalServicio();
            _datosTurno = datosTurno;

            if (_resizeHandler != null)
                contenedorPadre.Resize -= _resizeHandler;

            contenedorPadre.BackColor = ColorFondoContenedor;
            contenedorPadre.Controls.Clear();

            _layoutRoot = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new Padding(16, 12, 16, 8),
                BackColor = ColorFondoContenedor
            };
            _layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _layoutRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            _layoutRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 168F));
            _layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Fila 0: título + última actualización
            var header = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent
            };
            header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));

            var lblSeccion = new Label
            {
                Text = "Resumen del turno",
                ForeColor = ColorTextoPrincipal,
                Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            lblUltimaActualizacion = new Label
            {
                Text = "Última actualización: —",
                ForeColor = ColorTextoSecundario,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight
            };

            header.Controls.Add(lblSeccion, 0, 0);
            header.Controls.Add(lblUltimaActualizacion, 1, 0);
            _layoutRoot.Controls.Add(header, 0, 0);
            _layoutRoot.SetColumnSpan(header, 2);

            // Fila 1: caja | sesión
            var tarjetaCaja = CrearTarjetaCaja(_datosTurno);
            var tarjetaSesion = CrearTarjeta(
                "SESIÓN ACTIVA",
                ObtenerTextoSesion(),
                ColorIndicadorSesion);

            lblContenidoSesion = tarjetaSesion.Controls.OfType<Label>()
                .FirstOrDefault(l => l.Name == "lblContenido");

            _layoutRoot.Controls.Add(tarjetaCaja, 0, 1);
            _layoutRoot.Controls.Add(tarjetaSesion, 1, 1);

            ConfigurarTimerReloj();
            ConfigurarPanelNotas();
            _layoutRoot.Controls.Add(pNotasContainer, 0, 2);
            _layoutRoot.SetColumnSpan(pNotasContainer, 2);

            contenedorPadre.Controls.Add(_layoutRoot);

            _resizeHandler = (s, e) => AjustarLayout(contenedorPadre);
            contenedorPadre.Resize += _resizeHandler;
            AjustarLayout(contenedorPadre);

            MarcarUltimaActualizacion(DateTime.Now);
        }

        public void ActualizarSoloTextoCaja(DatosTurnoDTO nuevosDatos)
        {
            _datosTurno = nuevosDatos;

            if (lblContenidoCaja != null && !lblContenidoCaja.IsDisposed)
                lblContenidoCaja.Text = ObtenerTextoCaja(nuevosDatos);

            ActualizarBadgeEstadoCaja(nuevosDatos);
        }

        public void MarcarUltimaActualizacion(DateTime momento)
        {
            if (lblUltimaActualizacion == null || lblUltimaActualizacion.IsDisposed)
                return;

            lblUltimaActualizacion.Text = $"Última actualización: {momento:HH:mm:ss}";
        }

        private Panel CrearTarjetaCaja(DatosTurnoDTO datosTurno)
        {
            var tarjeta = CrearTarjeta(
                "ESTADO DE CAJA",
                ObtenerTextoCaja(datosTurno),
                ColorIndicadorCaja);

            lblEstadoCaja = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                Top = 10,
                Left = 150,
                Padding = new Padding(6, 2, 6, 2),
                Tag = "card-sin-tema"
            };
            ActualizarBadgeEstadoCaja(datosTurno);
            tarjeta.Controls.Add(lblEstadoCaja);
            lblEstadoCaja.BringToFront();

            btnCensura = new Button
            {
                Size = new Size(88, 28),
                Location = new Point(tarjeta.Width - 98, 8),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Tag = "card-sin-tema"
            };
            btnCensura.FlatAppearance.BorderSize = 1;
            btnCensura.FlatAppearance.BorderColor = Color.Black;
            ActualizarBotonCensura();
            _toolTip.SetToolTip(btnCensura, "Mostrar u ocultar montos de caja");

            btnCensura.Click += (s, e) =>
            {
                _informacionCensurada = !_informacionCensurada;
                ActualizarBotonCensura();
                ActualizarSoloTextoCaja(_datosTurno);
            };

            tarjeta.Controls.Add(btnCensura);
            btnCensura.BringToFront();

            lblContenidoCaja = tarjeta.Controls.OfType<Label>()
                .FirstOrDefault(l => l.Name == "lblContenido");

            return tarjeta;
        }

        private void ActualizarBotonCensura()
        {
            if (btnCensura == null || btnCensura.IsDisposed)
                return;

            if (_informacionCensurada)
            {
                btnCensura.Text = "Mostrar $";
                btnCensura.BackColor = TemaSistema.Primario;
                btnCensura.ForeColor = Color.White;
            }
            else
            {
                btnCensura.Text = "Ocultar $";
                btnCensura.BackColor = TemaSistema.Seleccion;
                btnCensura.ForeColor = Color.Black;
            }
        }

        private void ActualizarBadgeEstadoCaja(DatosTurnoDTO datos)
        {
            if (lblEstadoCaja == null || lblEstadoCaja.IsDisposed || datos == null)
                return;

            if (datos.CajaAbierta)
            {
                lblEstadoCaja.Text = "ABIERTA";
                lblEstadoCaja.BackColor = Color.FromArgb(46, 125, 50);
                lblEstadoCaja.ForeColor = Color.White;
            }
            else
            {
                lblEstadoCaja.Text = "CERRADA";
                lblEstadoCaja.BackColor = Color.FromArgb(198, 40, 40);
                lblEstadoCaja.ForeColor = Color.White;
            }
        }

        private Panel CrearTarjeta(string titulo, string contenido, Color colorIndicador)
        {
            Panel p = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 12, 0),
                BackColor = ColorTarjetaFondo,
                Tag = "card-sin-tema",
                Padding = new Padding(0)
            };

            Panel indicador = new Panel
            {
                Dock = DockStyle.Left,
                Width = 12,
                BackColor = colorIndicador,
                Tag = "card-sin-tema"
            };

            Label lblT = new Label
            {
                Text = titulo.ToUpper(),
                Top = 12,
                Left = 25,
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = ColorTextoGrisClaro,
                Name = "lblTitulo"
            };

            Label lblC = new Label
            {
                Name = "lblContenido",
                Text = contenido,
                Top = 42,
                Left = 25,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold),
                ForeColor = ColorTextoPrincipal
            };

            p.Controls.Add(lblC);
            p.Controls.Add(lblT);
            p.Controls.Add(indicador);

            p.Resize += (s, e) =>
            {
                lblC.Width = Math.Max(p.ClientSize.Width - 50, 80);
                lblC.Height = Math.Max(p.ClientSize.Height - 50, 60);
                if (btnCensura != null && btnCensura.Parent == p)
                    btnCensura.Location = new Point(p.Width - btnCensura.Width - 10, 8);
            };

            return p;
        }

        private void ConfigurarPanelNotas()
        {
            timerAutosaveNotas?.Stop();
            timerAutosaveNotas?.Dispose();
            timerAutosaveNotas = null;

            pNotasContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 8, 0, 0),
                BackColor = Color.Transparent
            };

            lblNotas = new Label
            {
                Text = "NOTAS PARA EL SIGUIENTE TURNO",
                ForeColor = ColorTextoSecundario,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Top = 4,
                Left = 0,
                AutoSize = true
            };

            lblEstadoNotas = new Label
            {
                Text = "",
                AutoSize = true,
                Top = 4,
                Left = 260,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = ColorTextoSecundario
            };

            txtNotas = new TextBox
            {
                Multiline = true,
                Height = 140,
                Top = 28,
                Left = 0,
                Font = new Font("Segoe UI", 11),
                ScrollBars = ScrollBars.Vertical,
                AcceptsReturn = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Text = string.IsNullOrWhiteSpace(_datosTurno.NotasTurno) ? "- " : _datosTurno.NotasTurno,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = ColorTarjetaFondo,
                ForeColor = ColorTextoPrincipal
            };

            txtNotas.KeyDown += (s, e) =>
            {
                if (e.Control && e.KeyCode == Keys.S)
                {
                    e.SuppressKeyPress = true;
                    GuardarNotas(silencioso: false);
                    return;
                }

                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    string nuevaLinea = Environment.NewLine + "- ";
                    int seleccionIndex = txtNotas.SelectionStart;
                    txtNotas.Text = txtNotas.Text.Insert(seleccionIndex, nuevaLinea);
                    txtNotas.SelectionStart = seleccionIndex + nuevaLinea.Length;
                }
            };

            txtNotas.TextChanged += (s, e) =>
            {
                if (_cargandoNotas)
                    return;

                string actual = NormalizarTextoNotas(txtNotas.Text);
                bool dirty = !string.Equals(actual, _ultimoTextoGuardado, StringComparison.Ordinal);
                MarcarNotasDirty(dirty);
                ReiniciarAutosaveNotas();
            };

            btnGuardarNotas = new Button
            {
                Text = "GUARDAR NOTAS",
                Width = 200,
                Height = 36,
                FlatStyle = FlatStyle.Flat,
                BackColor = ColorBotonGuardarFondo,
                ForeColor = ColorBotonGuardarTexto,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            _toolTip.SetToolTip(btnGuardarNotas, "Guardar ahora (Ctrl+S). También se guarda solo tras unos segundos sin escribir.");

            btnGuardarNotas.Click += (s, e) => GuardarNotas(silencioso: false);

            pNotasContainer.Controls.Add(lblNotas);
            pNotasContainer.Controls.Add(lblEstadoNotas);
            pNotasContainer.Controls.Add(txtNotas);
            pNotasContainer.Controls.Add(btnGuardarNotas);

            _cargandoNotas = true;
            var notasGuardadas = _pantallaPrincipalServicio.ObtenerNotasRapidas();
            if (notasGuardadas != null && !string.IsNullOrWhiteSpace(notasGuardadas.ToString()))
                txtNotas.Text = notasGuardadas.ToString();
            _ultimoTextoGuardado = NormalizarTextoNotas(txtNotas.Text);
            _datosTurno.NotasTurno = _ultimoTextoGuardado;
            _cargandoNotas = false;
            MarcarNotasDirty(false);
            MostrarEstadoNotas("Listo", ColorTextoSecundario);
        }

        private void ReiniciarAutosaveNotas()
        {
            if (timerAutosaveNotas == null)
            {
                timerAutosaveNotas = new System.Windows.Forms.Timer { Interval = 1600 };
                timerAutosaveNotas.Tick += (s, e) =>
                {
                    timerAutosaveNotas.Stop();
                    if (_notasDirty)
                        GuardarNotas(silencioso: true);
                };
            }

            timerAutosaveNotas.Stop();
            if (_notasDirty)
                timerAutosaveNotas.Start();
        }

        private void MarcarNotasDirty(bool dirty)
        {
            _notasDirty = dirty;
            if (dirty)
                MostrarEstadoNotas("Cambios sin guardar", Color.FromArgb(230, 126, 34));
        }

        private void MostrarEstadoNotas(string texto, Color color)
        {
            if (lblEstadoNotas == null || lblEstadoNotas.IsDisposed)
                return;

            lblEstadoNotas.Text = texto;
            lblEstadoNotas.ForeColor = color;
            if (lblNotas != null)
                lblEstadoNotas.Left = lblNotas.Right + 12;
        }

        private static string NormalizarTextoNotas(string texto)
        {
            var lineasValidas = (texto ?? string.Empty)
                .Replace("\r\n", "\n")
                .Split('\n')
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l) && l != "-")
                .Select(l => l.StartsWith("-") ? l : "- " + l)
                .ToList();

            return string.Join(Environment.NewLine, lineasValidas);
        }

        private void GuardarNotas(bool silencioso)
        {
            if (txtNotas == null || _pantallaPrincipalServicio == null || _datosTurno == null)
                return;

            string textoLimpio = NormalizarTextoNotas(txtNotas.Text);

            _cargandoNotas = true;
            int caret = txtNotas.SelectionStart;
            txtNotas.Text = string.IsNullOrEmpty(textoLimpio) ? "- " : textoLimpio;
            txtNotas.SelectionStart = Math.Min(caret, txtNotas.Text.Length);
            _cargandoNotas = false;

            if (string.Equals(textoLimpio, _ultimoTextoGuardado, StringComparison.Ordinal) && !_notasDirty)
            {
                MostrarEstadoNotas("Guardado ✓", Color.FromArgb(46, 125, 50));
                return;
            }

            try
            {
                _datosTurno.NotasTurno = textoLimpio;
                _pantallaPrincipalServicio.GuardarNotasRapidas(textoLimpio, _datosTurno.UsuarioLogeado);
                _ultimoTextoGuardado = textoLimpio;
                _notasDirty = false;
                MostrarEstadoNotas(silencioso ? "Autoguardado ✓" : "Guardado ✓", Color.FromArgb(46, 125, 50));
            }
            catch (Exception ex)
            {
                MarcarNotasDirty(true);
                MostrarEstadoNotas("Error al guardar", Color.FromArgb(198, 40, 40));
                if (!silencioso)
                    MessageBox.Show("No se pudieron guardar las notas:\n" + ex.Message, "Notas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ConfigurarTimerReloj()
        {
            if (timerReloj != null)
            {
                timerReloj.Stop();
                timerReloj.Dispose();
            }

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
            string mInicial = _informacionCensurada ? "••••••" : datos.MontoInicial.ToString("N2");
            string mIngresos = _informacionCensurada ? "••••••" : datos.Ingresos.ToString("N2");
            string mEgresos = _informacionCensurada ? "••••••" : datos.Egresos.ToString("N2");
            string mTotal = _informacionCensurada ? "••••••" : datos.TotalCaja.ToString("N2");

            return $"Monto inicial:   $ {mInicial}\n" +
                   $"Ingresos:        $ {mIngresos}\n" +
                   $"Egresos:         $ {mEgresos}\n" +
                   $"Total caja:      $ {mTotal}";
        }

        private string ObtenerTextoSesion()
        {
            if (_datosTurno == null) return "Cargando...";

            TimeSpan transcurrido = DateTime.Now - _datosTurno.HoraIngresoUsuario;
            string tiempoStr = $"{(int)transcurrido.TotalHours:00}h {transcurrido.Minutes:00}m {transcurrido.Seconds:00}s";

            return $"Usuario:       {_datosTurno.UsuarioLogeado}\n" +
                   $"Ingreso:       {_datosTurno.HoraIngresoUsuario:HH:mm:ss}\n" +
                   $"Transcurrido:  {tiempoStr}";
        }

        private void AjustarLayout(Control padre)
        {
            if (txtNotas == null || btnGuardarNotas == null || pNotasContainer == null)
                return;

            int ancho = Math.Max(pNotasContainer.ClientSize.Width, 100);
            txtNotas.Width = ancho;
            txtNotas.Height = Math.Max(pNotasContainer.ClientSize.Height - 80, 80);
            btnGuardarNotas.Top = txtNotas.Bottom + 8;
            btnGuardarNotas.Left = Math.Max(txtNotas.Left + txtNotas.Width - btnGuardarNotas.Width, 0);

            if (lblEstadoNotas != null && lblNotas != null)
                lblEstadoNotas.Left = lblNotas.Right + 12;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_notasDirty)
                {
                    try { GuardarNotas(silencioso: true); } catch { /* ignore on dispose */ }
                }

                timerReloj?.Stop();
                timerReloj?.Dispose();
                timerAutosaveNotas?.Stop();
                timerAutosaveNotas?.Dispose();
                _toolTip?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
