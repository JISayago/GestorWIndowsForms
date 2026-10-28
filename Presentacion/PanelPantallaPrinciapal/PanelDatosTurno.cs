using Presentacion.FBase.Helpers;
using ServicioAccesoSistema.AccesoSistema;
using Servicios.Helpers.Sistema;
using Servicios.LogicaNegocio.Empleado;
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
        #region Colores

        public Color ColorFondoContenedor { get; set; } = TemaSistema.Fondo;
        public Color ColorTarjetaFondo { get; set; } = TemaSistema.Alternado;
        public Color ColorTextoPrincipal { get; set; } = TemaSistema.Texto;
        public Color ColorTextoSecundario { get; set; } = TemaSistema.TextoSecundario;
        public Color ColorTextoGrisClaro { get; set; } = Color.Gray;
        public Color ColorIndicadorCaja { get; set; } = TemaSistema.Primario;
        public Color ColorIndicadorSesion { get; set; } = TemaSistema.Acento;
        public Color ColorIndicadorSesionSecundaria { get; set; } = TemaSistema.Seleccion;
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
        // Sesiones del terminal: la principal y la secundaria (si hay)
        private readonly IAccesoSistema _accesoSistema = new AccesoSistema();
        private Panel _tarjetaSesionPrincipal;
        private Panel _tarjetaSesionSecundaria;
        private SesionInfoDTO _sesionPrincipal;
        private SesionInfoDTO _sesionSecundaria;
        private readonly List<BloqueSesion> _bloquesSesion = new List<BloqueSesion>();

        private sealed class BloqueSesion
        {
            public SesionInfoDTO Sesion;
            public Label Contenido;
        }
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
                ColumnCount = 3,
                RowCount = 3,
                Padding = new Padding(16, 12, 16, 8),
                BackColor = ColorFondoContenedor
            };
            _layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));
            _layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            _layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            _layoutRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            _layoutRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 188F));
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
            _layoutRoot.SetColumnSpan(header, 3);

            // Fila 1: caja | sesión principal | sesión secundaria (esta última solo si hay una activa)
            var tarjetaCaja = CrearTarjetaCaja(_datosTurno);
            _layoutRoot.Controls.Add(tarjetaCaja, 0, 1);

            CargarSesiones();
            ReemplazarTarjetasSesion();   // card principal (col 1) y, solo si hay una activa, card secundaria (col 2)

            ConfigurarTimerReloj();
            ConfigurarPanelNotas();
            _layoutRoot.Controls.Add(pNotasContainer, 0, 2);
            _layoutRoot.SetColumnSpan(pNotasContainer, 3);

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
            timerReloj.Tick += (s, e) => ActualizarTextosSesiones();
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

        #region Sesiones (principal / secundaria)

        private const string TextoDefinicionSesiones =
            "PRINCIPAL: la sesión con la que se abrió Stockeate en este equipo.\n" +
            "SECUNDARIA: sesión temporal de otro usuario (ej. un administrador), abierta desde la principal " +
            "con «Cambio Usuario».\n" +
            "Mientras la secundaria está abierta, la principal queda bloqueada.";

        /// <summary>
        /// Vuelve a leer las sesiones de la BD y redibuja solo las dos cards de sesión.
        /// </summary>
        public void ActualizarSesiones()
        {
            if (_layoutRoot == null || _layoutRoot.IsDisposed)
                return;

            CargarSesiones();

            _layoutRoot.SuspendLayout();
            try
            {
                ReemplazarTarjetasSesion();
            }
            finally
            {
                _layoutRoot.ResumeLayout(true);
            }
        }

        private void ReemplazarTarjetasSesion()
        {
            _bloquesSesion.Clear();

            foreach (var vieja in new[] { _tarjetaSesionPrincipal, _tarjetaSesionSecundaria })
            {
                if (vieja == null) continue;
                _layoutRoot.Controls.Remove(vieja);
                vieja.Dispose();
            }

            bool haySecundaria = HaySecundariaActiva;

            _tarjetaSesionPrincipal = CrearTarjetaSesion(_sesionPrincipal, esSecundaria: false);
            _layoutRoot.Controls.Add(_tarjetaSesionPrincipal, 1, 1);

            // La card de la secundaria existe solo mientras haya una sesión secundaria activa.
            _tarjetaSesionSecundaria = null;
            if (haySecundaria)
            {
                _tarjetaSesionSecundaria = CrearTarjetaSesion(_sesionSecundaria, esSecundaria: true);
                _layoutRoot.Controls.Add(_tarjetaSesionSecundaria, 2, 1);
            }

            AplicarColumnasSesiones(haySecundaria);
        }

        private bool HaySecundariaActiva => _sesionSecundaria != null && _sesionSecundaria.Activa;

        /// <summary>
        /// Con secundaria: caja | principal | secundaria (36/32/32). Sin secundaria: caja | principal (50/50),
        /// y la tercera columna queda en 0 % para no ocupar lugar.
        /// </summary>
        private void AplicarColumnasSesiones(bool haySecundaria)
        {
            var columnas = _layoutRoot.ColumnStyles;

            columnas[0].SizeType = SizeType.Percent;
            columnas[1].SizeType = SizeType.Percent;
            columnas[2].SizeType = SizeType.Percent;

            columnas[0].Width = haySecundaria ? 36F : 50F;
            columnas[1].Width = haySecundaria ? 32F : 50F;
            columnas[2].Width = haySecundaria ? 32F : 0F;
        }

        /// <summary>
        /// Una card por sesión, con el formato de siempre (Usuario / Ingreso / Transcurrido), más un badge de
        /// estado (como ABIERTA/CERRADA en la caja), la marca "ESTA VENTANA" y una línea que explica qué es.
        /// </summary>
        private Panel CrearTarjetaSesion(SesionInfoDTO sesion, bool esSecundaria)
        {
            bool existe = sesion != null;

            string texto = existe
                ? ObtenerTextoSesion(sesion)
                : "No se pudo leer la sesión principal.";

            var tarjeta = CrearTarjeta(
                esSecundaria ? "SESIÓN SECUNDARIA" : "SESIÓN PRINCIPAL",
                texto,
                esSecundaria ? ColorIndicadorSesionSecundaria : ColorIndicadorSesion);

            var lblTitulo = tarjeta.Controls.OfType<Label>().First(l => l.Name == "lblTitulo");
            var lblContenido = tarjeta.Controls.OfType<Label>().First(l => l.Name == "lblContenido");
            if (!existe)
                lblContenido.ForeColor = ColorTextoSecundario;

            _toolTip.SetToolTip(lblTitulo, TextoDefinicionSesiones);

            // Badge de estado, a la derecha del título
            var estado = ObtenerEstadoSesion(sesion, esSecundaria);
            var lblEstado = new Label
            {
                Text = estado.Texto,
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                BackColor = estado.Fondo,
                ForeColor = Color.White,
                Top = 10,
                Left = 25 + lblTitulo.PreferredWidth + 12,
                Padding = new Padding(6, 2, 6, 2),
                Tag = "card-sin-tema"
            };
            tarjeta.Controls.Add(lblEstado);
            lblEstado.BringToFront();

            // Marca de cuál de las dos sesiones es la de esta ventana
            //if (EsSesionPropia(sesion))
            //{
            //    var lblVentana = new Label
            //    {
            //        Text = "● ESTA VENTANA",
            //        AutoSize = true,
            //        Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
            //        ForeColor = TemaSistema.Primario,
            //        Top = 12,
            //        Left = lblEstado.Left + lblEstado.PreferredWidth + 10,
            //        Tag = "card-sin-tema"
            //    };
            //    tarjeta.Controls.Add(lblVentana);
            //    lblVentana.BringToFront();
            //}

            // Línea al pie que explica qué es esta sesión
            //var lblAyuda = new Label
            //{
            //    Text = ObtenerTextoAyudaSesion(sesion, esSecundaria),
            //    AutoSize = false,
            //    Left = 25,
            //    Height = 34,
            //    Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
            //    ForeColor = ColorTextoSecundario,
            //    Tag = "card-sin-tema"
            //};
            //_toolTip.SetToolTip(lblAyuda, TextoDefinicionSesiones);
            //tarjeta.Controls.Add(lblAyuda);
            //lblAyuda.BringToFront();

            // Se suscribe después del handler de CrearTarjeta, así que este ajuste es el que queda
            //tarjeta.Resize += (s, e) =>
            //{
            //    lblAyuda.Width = Math.Max(tarjeta.ClientSize.Width - 25 - 12, 80);
            //    lblAyuda.Top = Math.Max(tarjeta.ClientSize.Height - lblAyuda.Height - 6, 0);
            //    lblContenido.Height = Math.Max(tarjeta.ClientSize.Height - 50 - lblAyuda.Height - 4, 40);
            //};

            _bloquesSesion.Add(new BloqueSesion { Sesion = sesion, Contenido = lblContenido });
            return tarjeta;
        }

        private (string Texto, Color Fondo) ObtenerEstadoSesion(SesionInfoDTO sesion, bool esSecundaria)
        {
            if (sesion == null)
                return ("SIN DATOS", Color.Gray);

            if (!sesion.Activa)
                return ("CERRADA", Color.Gray);

            // La principal queda bloqueada mientras haya una secundaria abierta.
            if (!esSecundaria && _sesionSecundaria != null && _sesionSecundaria.Activa)
                return ("PAUSADA", Color.FromArgb(230, 126, 34));

            return ("ACTIVA", Color.FromArgb(46, 125, 50));
        }

        private string ObtenerTextoAyudaSesion(SesionInfoDTO sesion, bool esSecundaria)
        {
            if (!esSecundaria)
            {
                if (_sesionSecundaria != null && _sesionSecundaria.Activa)
                    return SesionSecundaria.EsSecundaria
                        ? "Sesión con la que se abrió el sistema. Queda bloqueada hasta que cierres esta sesión secundaria."
                        : "Sesión con la que se abrió el sistema. Queda bloqueada hasta que se cierre la sesión secundaria.";

                return "Sesión con la que se abrió el sistema en este equipo.";
            }

            return "Sesión temporal de otro usuario, abierta desde la principal. Al cerrarla se vuelve a la principal.";
        }

        /// <summary>
        /// Lee de UsuarioSesion la sesión principal del terminal y la secundaria asociada. Se parte siempre de la
        /// sesión principal: en la ventana principal es la propia (se busca por usuario) y en la secundaria llega
        /// por argumento (SesionSecundaria.SesionPrincipalId). Así no se mezclan sesiones de otros equipos.
        /// </summary>
        private void CargarSesiones()
        {
            _sesionPrincipal = null;
            _sesionSecundaria = null;

            try
            {
                long? principalId = SesionSecundaria.EsSecundaria
                    ? SesionSecundaria.SesionPrincipalId
                    : _accesoSistema.ObtenerSesionActivaId(DatosSistema.UsuarioId);

                if (principalId.HasValue)
                {
                    var sesiones = _accesoSistema.ObtenerSesionesDelTerminal(principalId.Value);
                    CompletarNombres(sesiones);

                    _sesionPrincipal = sesiones.FirstOrDefault(s => !s.EsSecundaria);

                    // Solo interesa la secundaria que está abierta: si no hay ninguna, no hay card de secundaria.
                    _sesionSecundaria = sesiones
                        .Where(s => s.EsSecundaria && s.Activa)
                        .OrderByDescending(s => s.FechaLogin)
                        .FirstOrDefault();
                }
            }
            catch
            {
                // Si falla la consulta, abajo se arma la sesión de esta ventana con los datos que ya trae el turno.
            }

            // Respaldo: la sesión de esta ventana siempre se muestra.
            if (_datosTurno != null)
            {
                if (SesionSecundaria.EsSecundaria && !EsSesionPropia(_sesionSecundaria))
                    _sesionSecundaria = SesionDesdeTurno(esSecundaria: true);
                else if (!SesionSecundaria.EsSecundaria && _sesionPrincipal == null)
                    _sesionPrincipal = SesionDesdeTurno(esSecundaria: false);
            }
        }

        private SesionInfoDTO SesionDesdeTurno(bool esSecundaria)
        {
            return new SesionInfoDTO
            {
                UsuarioId = DatosSistema.UsuarioId,
                NombreCompleto = Convert.ToString(_datosTurno.UsuarioLogeado) ?? "",
                FechaLogin = _datosTurno.HoraIngresoUsuario,
                Activa = true,
                EsSecundaria = esSecundaria
            };
        }

        private static void CompletarNombres(List<SesionInfoDTO> sesiones)
        {
            var empleadoServicio = new EmpleadoServicio();
            var nombres = new Dictionary<long, string>();

            foreach (var sesion in sesiones)
            {
                if (!nombres.TryGetValue(sesion.UsuarioId, out var nombre))
                {
                    try
                    {
                        var empleado = empleadoServicio.ObtenerEmpleadoPorId(sesion.UsuarioId);
                        nombre = empleado != null ? $"{empleado.Nombre} {empleado.Apellido}".Trim() : "";
                    }
                    catch
                    {
                        nombre = "";
                    }

                    nombres[sesion.UsuarioId] = nombre;
                }

                sesion.NombreCompleto = nombre;
            }
        }

        private string ObtenerTextoSesion(SesionInfoDTO sesion)
        {
            if (sesion == null)
                return "Cargando...";

            string usuario = NombreMostrar(sesion);

            if (sesion.Activa)
            {
                return $"Usuario:       {usuario}\n" +
                       $"Ingreso:       {FormatoHora(sesion.FechaLogin)}\n" +
                       $"Transcurrido:  {FormatoDuracion(DateTime.Now - sesion.FechaLogin)}";
            }

            DateTime fin = sesion.FechaLogout ?? sesion.FechaLogin;
            return $"Usuario:       {usuario}\n" +
                   $"Ingreso:       {FormatoHora(sesion.FechaLogin)}\n" +
                   $"Egreso:         {FormatoHora(fin)} ({FormatoDuracion(fin - sesion.FechaLogin)})";
        }

        /// <summary>Solo las sesiones activas cambian con el tiempo: se refrescan con el timer de 1 segundo.</summary>
        private void ActualizarTextosSesiones()
        {
            foreach (var bloque in _bloquesSesion)
            {
                if (bloque.Sesion != null && bloque.Sesion.Activa &&
                    bloque.Contenido != null && !bloque.Contenido.IsDisposed)
                {
                    bloque.Contenido.Text = ObtenerTextoSesion(bloque.Sesion);
                }
            }
        }

        /// <summary>True si la sesión es la de esta ventana (mismo usuario y mismo tipo: principal/secundaria).</summary>
        private static bool EsSesionPropia(SesionInfoDTO sesion)
        {
            return sesion != null
                && sesion.EsSecundaria == SesionSecundaria.EsSecundaria
                && sesion.UsuarioId == DatosSistema.UsuarioId;
        }

        private static string NombreMostrar(SesionInfoDTO sesion)
        {
            if (sesion == null) return "";

            bool tieneNombre = !string.IsNullOrWhiteSpace(sesion.NombreCompleto);
            bool tieneUsername = !string.IsNullOrWhiteSpace(sesion.Username);

            if (tieneNombre && tieneUsername &&
                !string.Equals(sesion.NombreCompleto, sesion.Username, StringComparison.OrdinalIgnoreCase))
                return $"{sesion.NombreCompleto} ({sesion.Username})";

            return tieneNombre ? sesion.NombreCompleto : (sesion.Username ?? "");
        }

        private static string FormatoHora(DateTime momento)
        {
            return momento.Date == DateTime.Today
                ? momento.ToString("HH:mm:ss")
                : momento.ToString("dd/MM HH:mm:ss");
        }

        private static string FormatoDuracion(TimeSpan duracion)
        {
            if (duracion < TimeSpan.Zero) duracion = TimeSpan.Zero;
            return $"{(int)duracion.TotalHours:00}h {duracion.Minutes:00}m {duracion.Seconds:00}s";
        }

        #endregion

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