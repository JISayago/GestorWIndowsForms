using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using Presentacion.FBase.Helpers;

namespace Presentacion.FBase
{
    // =====================================================================
    //  Tipos de mensaje (definen ícono, color y sonido)
    // =====================================================================
    public enum TipoMensaje
    {
        Informacion,
        Exito,
        Advertencia,
        Error,
        Pregunta
    }

    // =====================================================================
    //  Un botón configurable
    // =====================================================================
    public class BotonMensaje
    {
        public string Texto { get; set; }

        /// <summary>Valor que devuelve el mensaje si se toca este botón (cualquier objeto).</summary>
        public object Valor { get; set; }

        /// <summary>DialogResult equivalente (opcional). Útil para comparar como un MessageBox clásico.</summary>
        public DialogResult Resultado { get; set; }

        /// <summary>Botón con foco inicial y que responde al Enter.</summary>
        public bool EsPredeterminado { get; set; }

        /// <summary>Botón que responde a Esc (y a la X de la ventana).</summary>
        public bool EsCancelar { get; set; }

        /// <summary>Si se define, pisa el color del tema para este botón.</summary>
        public Color? ColorFondo { get; set; }
        public Color? ColorTexto { get; set; }

        /// <summary>Ancho fijo en px. 0 = automático.</summary>
        public int Ancho { get; set; }

        public BotonMensaje(string texto, object valor = null, DialogResult resultado = DialogResult.None)
        {
            Texto = texto;
            Valor = valor;
            Resultado = resultado;
        }
    }

    // =====================================================================
    //  Todas las opciones del mensaje
    // =====================================================================
    public class OpcionesMensaje
    {
        public string Titulo { get; set; }
        public string Mensaje { get; set; }

        /// <summary>Texto largo opcional (ej: ex.ToString()). Se muestra plegado con link "Ver detalles".</summary>
        public string Detalle { get; set; }

        public TipoMensaje Tipo { get; set; }
        public bool MostrarIcono { get; set; }

        /// <summary>Color del ícono. Si es null se usa el color del tipo.</summary>
        public Color? ColorAcento { get; set; }

        public List<BotonMensaje> Botones { get; private set; }

        /// <summary>Si tiene texto, aparece un checkbox (ej: "No volver a mostrar").</summary>
        public string TextoCasilla { get; set; }
        public bool CasillaMarcada { get; set; }

        /// <summary>Si es mayor a 0, se cierra solo activando el botón predeterminado.</summary>
        public int SegundosAutoCierre { get; set; }

        /// <summary>Pinta el botón predeterminado con Primario/Acento del tema.</summary>
        public bool DestacarPredeterminado { get; set; }

        public bool Sonido { get; set; }
        public bool PermitirCerrarConX { get; set; }
        public bool SiempreVisible { get; set; }

        public int AnchoMaximo { get; set; }
        public int AnchoMinimo { get; set; }

        /// <summary>Si es null se usa la fuente heredada de FBase.</summary>
        public Font Fuente { get; set; }
        public IWin32Window Owner { get; set; }

        public OpcionesMensaje()
        {
            Tipo = TipoMensaje.Informacion;
            MostrarIcono = true;
            Botones = new List<BotonMensaje>();
            DestacarPredeterminado = true;
            Sonido = true;
            PermitirCerrarConX = true;
            AnchoMaximo = 520;
            AnchoMinimo = 360;
        }

        public OpcionesMensaje AgregarBoton(string texto, object valor = null,
            DialogResult resultado = DialogResult.None,
            bool predeterminado = false, bool cancelar = false,
            Color? colorFondo = null, Color? colorTexto = null, int ancho = 0)
        {
            Botones.Add(new BotonMensaje(texto, valor, resultado)
            {
                EsPredeterminado = predeterminado,
                EsCancelar = cancelar,
                ColorFondo = colorFondo,
                ColorTexto = colorTexto,
                Ancho = ancho
            });
            return this;
        }
    }

    // =====================================================================
    //  Resultado
    // =====================================================================
    public class ResultadoMensaje
    {
        /// <summary>Botón tocado (null si se cerró con la X y no hay botón "cancelar").</summary>
        public BotonMensaje Boton { get; set; }

        public object Valor { get { return Boton != null ? Boton.Valor : null; } }

        public DialogResult Resultado
        {
            get { return Boton != null ? Boton.Resultado : DialogResult.Cancel; }
        }

        public string TextoBoton { get { return Boton != null ? Boton.Texto : null; } }

        public bool CasillaMarcada { get; set; }
        public bool AutoCerrado { get; set; }

        public T ObtenerValor<T>(T porDefecto = default(T))
        {
            object v = Valor;
            if (v is T) return (T)v;
            return porDefecto;
        }
    }

    // =====================================================================
    //  El formulario: hereda de FBase (tema, ícono de la app, manejo de Esc/Enter)
    // =====================================================================
    [DesignerCategory("Code")]
    public class FMensaje : FBase
    {
        private readonly OpcionesMensaje _op;
        private readonly Dictionary<Button, BotonMensaje> _mapa = new Dictionary<Button, BotonMensaje>();

        private FlowLayoutPanel _barra;
        private Button _btnCancelar;
        private BotonMensaje _seleccionado;
        private bool _autoCerrado;
        private CheckBox _chk;
        private TextBox _txtDetalle;
        private LinkLabel _lnkDetalle;

        private System.Windows.Forms.Timer _timer;
        private int _restante;
        private Button _btnAuto;
        private string _textoBtnAuto;

        public FMensaje(OpcionesMensaje op) : base()
        {
            if (op == null) throw new ArgumentNullException("op");
            _op = op;

            if (_op.Botones.Count == 0)
                _op.AgregarBoton("Aceptar", true, DialogResult.OK, true, true);

            // Si nadie marcó predeterminado, el primero lo es
            if (!_op.Botones.Any(b => b.EsPredeterminado))
                _op.Botones[0].EsPredeterminado = true;

            // Si hay un solo botón, también cancela
            if (_op.Botones.Count == 1) _op.Botones[0].EsCancelar = true;

            if (_op.Fuente != null) Font = _op.Fuente;

            Text = string.IsNullOrEmpty(_op.Titulo) ? "Stockeate" : _op.Titulo;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            ControlBox = _op.PermitirCerrarConX;
            TopMost = _op.SiempreVisible;

            // FBase deja el form en 800x450: lo achicamos para que el AutoSize arranque desde poco
            ClientSize = new Size(_op.AnchoMinimo, 100);
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            MinimumSize = new Size(_op.AnchoMinimo, 0);

            ConstruirUI();
        }

        // ------------------------------------------------------------------
        private void ConstruirUI()
        {
            var raiz = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                Margin = Padding.Empty
            };
            raiz.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            // ---- Cuerpo: ícono + texto ----
            int anchoIcono = _op.MostrarIcono ? 48 : 0;
            int margenTexto = _op.MostrarIcono ? 16 : 0;
            int anchoTexto = _op.AnchoMaximo - 40 - anchoIcono - margenTexto;

            var cuerpo = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = _op.MostrarIcono ? 2 : 1,
                RowCount = 1,
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 20, 20, 16),
                Margin = Padding.Empty
            };

            if (_op.MostrarIcono)
            {
                cuerpo.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                var icono = new IconoMensaje(_op.Tipo, _op.ColorAcento ?? ColorDeTipo(_op.Tipo))
                {
                    Margin = Padding.Empty,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left
                };
                cuerpo.Controls.Add(icono, 0, 0);
            }
            cuerpo.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            var lbl = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(anchoTexto, 0),
                Text = _op.Mensaje ?? string.Empty,
                Margin = new Padding(margenTexto, 0, 0, 0),
                Anchor = AnchorStyles.Left,
                ForeColor = TemaSistema.Texto,
                Tag = "NoModificarConBase"
            };
            cuerpo.Controls.Add(lbl, _op.MostrarIcono ? 1 : 0, 0);
            raiz.Controls.Add(cuerpo);

            // ---- Detalle plegable ----
            if (!string.IsNullOrEmpty(_op.Detalle))
            {
                _lnkDetalle = new LinkLabel
                {
                    AutoSize = true,
                    Text = "Ver detalles \u25BE",
                    LinkColor = TemaSistema.Primario,
                    ActiveLinkColor = TemaSistema.Oscuro,
                    VisitedLinkColor = TemaSistema.Primario,
                    Margin = new Padding(20, 0, 20, 10)
                };
                _lnkDetalle.LinkClicked += delegate { AlternarDetalle(); };
                raiz.Controls.Add(_lnkDetalle);

                _txtDetalle = new TextBox
                {
                    Multiline = true,
                    ReadOnly = true,
                    ScrollBars = ScrollBars.Both,
                    WordWrap = false,
                    Text = _op.Detalle,
                    Height = 110,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(20, 0, 20, 12),
                    Visible = false,
                    Font = new Font("Consolas", 8.5f)
                };
                raiz.Controls.Add(_txtDetalle);
            }

            // ---- Casilla ----
            if (!string.IsNullOrEmpty(_op.TextoCasilla))
            {
                _chk = new CheckBox
                {
                    AutoSize = true,
                    Text = _op.TextoCasilla,
                    Checked = _op.CasillaMarcada,
                    Margin = new Padding(20, 0, 20, 14)
                };
                raiz.Controls.Add(_chk);
            }

            // ---- Barra de botones ----
            _barra = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Padding = new Padding(12, 13, 12, 12),
                Margin = Padding.Empty
            };
            // Línea fina arriba, como separador
            _barra.Paint += delegate (object s, PaintEventArgs e)
            {
                using (var pen = new Pen(TemaSistema.Borde))
                    e.Graphics.DrawLine(pen, 0, 0, _barra.Width, 0);
            };

            // RightToLeft: el primero que se agrega queda a la derecha.
            // Recorremos al revés para que el primero de la lista quede a la izquierda.
            for (int i = _op.Botones.Count - 1; i >= 0; i--)
            {
                BotonMensaje bm = _op.Botones[i];
                Button b = CrearBoton(bm);
                _mapa[b] = bm;
                _barra.Controls.Add(b);

                if (bm.EsPredeterminado) AcceptButton = b;
                if (bm.EsCancelar) _btnCancelar = b;
            }
            raiz.Controls.Add(_barra);
            Controls.Add(raiz);

            // Foco inicial
            var acc = AcceptButton as Button;
            if (acc != null) ActiveControl = acc;

            // Auto cierre
            if (_op.SegundosAutoCierre > 0 && acc != null)
            {
                _btnAuto = acc;
                _textoBtnAuto = _btnAuto.Text;
                _restante = _op.SegundosAutoCierre;
                _btnAuto.Text = _textoBtnAuto + " (" + _restante + ")";
                _timer = new System.Windows.Forms.Timer { Interval = 1000 };
                _timer.Tick += TimerTick;
            }
        }

        private Button CrearBoton(BotonMensaje bm)
        {
            var b = new Button
            {
                Text = bm.Texto,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MinimumSize = new Size(bm.Ancho > 0 ? bm.Ancho : 88, 32),
                Padding = new Padding(10, 0, 10, 0),
                Margin = new Padding(6, 0, 0, 0),
                // Siempre un DialogResult válido: el botón cierra el form solo
                DialogResult = bm.Resultado == DialogResult.None ? DialogResult.OK : bm.Resultado
            };

            if (bm.Ancho > 0)
            {
                b.AutoSize = false;
                b.Size = new Size(bm.Ancho, 32);
            }

            b.Click += delegate { _seleccionado = bm; };
            return b;
        }

        // ------------------------------------------------------------------
        //  TEMA: FBase.OnLoad llama a AplicarTema(this) y pisa los colores de
        //  todos los botones. Lo enganchamos para aplicar después los nuestros.
        // ------------------------------------------------------------------
        protected override void AplicarTema(Control parent)
        {
            base.AplicarTema(parent);

            // AplicarTema es recursivo: solo actuamos en la llamada raíz
            if (!ReferenceEquals(parent, this)) return;

            if (_barra != null) _barra.BackColor = TemaSistema.FondoControl;

            foreach (KeyValuePair<Button, BotonMensaje> kv in _mapa)
            {
                Button b = kv.Key;
                BotonMensaje bm = kv.Value;

                if (bm.ColorFondo.HasValue || bm.ColorTexto.HasValue)
                {
                    if (bm.ColorFondo.HasValue) b.BackColor = bm.ColorFondo.Value;
                    if (bm.ColorTexto.HasValue) b.ForeColor = bm.ColorTexto.Value;
                }
                else if (bm.EsPredeterminado && _op.DestacarPredeterminado)
                {
                    b.BackColor = TemaSistema.Primario;
                    b.ForeColor = TemaSistema.Acento;
                    b.FlatAppearance.BorderColor = TemaSistema.Oscuro;
                }
            }
        }

        // ------------------------------------------------------------------
        //  FBase intercepta Esc y Enter en ProcessCmdKey, así que los
        //  resolvemos con sus hooks virtuales.
        // ------------------------------------------------------------------
        protected override void EjecutarEscape()
        {
            if (_btnCancelar != null) _btnCancelar.PerformClick();
            else if (_op.PermitirCerrarConX) Close();
        }

        protected override void EjecutarEnter()
        {
            var foco = ActiveControl as Button;
            if (foco != null && foco.Enabled) { foco.PerformClick(); return; }

            var acc = AcceptButton as Button;
            if (acc != null && acc.Enabled) acc.PerformClick();
        }

        // Ctrl+C copia el mensaje como el MessageBox de Windows
        // (si hay texto seleccionado en el detalle, deja el copiar normal)
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.C))
            {
                var tb = ActiveControl as TextBox;
                if (tb != null && tb.SelectionLength > 0)
                    return base.ProcessCmdKey(ref msg, keyData);

                try
                {
                    string txt = Text + Environment.NewLine + Environment.NewLine + _op.Mensaje;
                    if (!string.IsNullOrEmpty(_op.Detalle))
                        txt += Environment.NewLine + Environment.NewLine + _op.Detalle;
                    Clipboard.SetText(txt);
                }
                catch { }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // ------------------------------------------------------------------
        private void AlternarDetalle()
        {
            _txtDetalle.Visible = !_txtDetalle.Visible;
            _lnkDetalle.Text = _txtDetalle.Visible ? "Ocultar detalles \u25B4" : "Ver detalles \u25BE";
            BeginInvoke((Action)Recentrar);
        }

        private void Recentrar()
        {
            if (Owner != null) CenterToParent();
            else CenterToScreen();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            _restante--;
            if (_restante <= 0)
            {
                _timer.Stop();
                _autoCerrado = true;
                _btnAuto.PerformClick();
                return;
            }
            _btnAuto.Text = _textoBtnAuto + " (" + _restante + ")";
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Recentrar(); // el AutoSize cambió el tamaño después del centrado inicial
            if (_op.Sonido) ReproducirSonido();
            if (_timer != null) _timer.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Cerró con la X: lo tratamos como el botón "cancelar" si existe
            if (_seleccionado == null)
                _seleccionado = _op.Botones.FirstOrDefault(b => b.EsCancelar);

            if (_timer != null) _timer.Stop();
            base.OnFormClosing(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _timer != null) _timer.Dispose();
            base.Dispose(disposing);
        }

        private void ReproducirSonido()
        {
            try
            {
                switch (_op.Tipo)
                {
                    case TipoMensaje.Error: SystemSounds.Hand.Play(); break;
                    case TipoMensaje.Advertencia: SystemSounds.Exclamation.Play(); break;
                    case TipoMensaje.Pregunta: SystemSounds.Question.Play(); break;
                    default: SystemSounds.Asterisk.Play(); break;
                }
            }
            catch { }
        }

        public ResultadoMensaje ObtenerResultado()
        {
            return new ResultadoMensaje
            {
                Boton = _seleccionado,
                CasillaMarcada = _chk != null && _chk.Checked,
                AutoCerrado = _autoCerrado
            };
        }

        internal static Color ColorDeTipo(TipoMensaje t)
        {
            switch (t)
            {
                case TipoMensaje.Exito: return Color.FromArgb(46, 160, 67);
                case TipoMensaje.Advertencia: return Color.FromArgb(235, 160, 0);
                case TipoMensaje.Error: return Color.Firebrick;   // mismo que ColorError de FBase
                default: return TemaSistema.Primario;             // Informacion y Pregunta
            }
        }
    }

    // =====================================================================
    //  Ícono dibujado con GDI+ (se ve nítido en cualquier DPI)
    // =====================================================================
    internal class IconoMensaje : Control
    {
        private readonly TipoMensaje _tipo;
        private readonly Color _color;

        public IconoMensaje(TipoMensaje tipo, Color color)
        {
            _tipo = tipo;
            _color = color;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            Size = new Size(48, 48);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Fondo = el del contenedor (que ya recibió el tema)
            g.Clear(Parent != null ? Parent.BackColor : TemaSistema.Fondo);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            using (var br = new SolidBrush(_color))
                g.FillEllipse(br, 1, 1, Width - 3, Height - 3);

            float cx = Width / 2f;
            using (var pen = new Pen(Color.White, 4f) { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round })
            using (var br = new SolidBrush(Color.White))
            {
                switch (_tipo)
                {
                    case TipoMensaje.Exito:
                        g.DrawLines(pen, new[] { new PointF(14, 25), new PointF(21, 32), new PointF(34, 17) });
                        break;

                    case TipoMensaje.Error:
                        g.DrawLine(pen, 16, 16, 32, 32);
                        g.DrawLine(pen, 32, 16, 16, 32);
                        break;

                    case TipoMensaje.Advertencia:
                        g.DrawLine(pen, cx, 12, cx, 27);
                        g.FillEllipse(br, cx - 2.5f, 31.5f, 5, 5);
                        break;

                    case TipoMensaje.Pregunta:
                        using (var f = new Font("Segoe UI", 24f, FontStyle.Bold, GraphicsUnit.Pixel))
                        using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                            g.DrawString("?", f, br, new RectangleF(0, 0, Width, Height - 2), sf);
                        break;

                    default: // Informacion
                        g.FillEllipse(br, cx - 2.5f, 11, 5, 5);
                        g.DrawLine(pen, cx, 21, cx, 35);
                        break;
                }
            }
        }
    }

    // =====================================================================
    //  API estática: lo que se usa en todo el sistema
    // =====================================================================
    public static class Mensajes
    {
        /// <summary>Título por defecto de todas las ventanas.</summary>
        public static string TituloPredeterminado = "Stockeate";

        // ---------- Genérico: full configurable ----------
        public static ResultadoMensaje Mostrar(OpcionesMensaje op, IWin32Window owner = null)
        {
            if (owner != null) op.Owner = owner;
            if (string.IsNullOrEmpty(op.Titulo)) op.Titulo = TituloPredeterminado;

            using (var f = new FMensaje(op))
            {
                f.ShowDialog(op.Owner);
                return f.ObtenerResultado();
            }
        }

        // ---------- Atajos sin valor de retorno ----------
        public static void Informacion(string mensaje, string titulo = null, string detalle = null, IWin32Window owner = null)
        {
            Simple(TipoMensaje.Informacion, mensaje, titulo, detalle, owner);
        }

        public static void Exito(string mensaje, string titulo = null, IWin32Window owner = null)
        {
            Simple(TipoMensaje.Exito, mensaje, titulo, null, owner);
        }

        public static void Advertencia(string mensaje, string titulo = null, string detalle = null, IWin32Window owner = null)
        {
            Simple(TipoMensaje.Advertencia, mensaje, titulo, detalle, owner);
        }

        public static void Error(string mensaje, string titulo = null, string detalle = null, IWin32Window owner = null)
        {
            Simple(TipoMensaje.Error, mensaje, titulo, detalle, owner);
        }

        /// <summary>Error a partir de una excepción: el stack queda en "Ver detalles".</summary>
        public static void Error(Exception ex, string mensaje = null, string titulo = null, IWin32Window owner = null)
        {
            Simple(TipoMensaje.Error, mensaje ?? ex.Message, titulo, ex.ToString(), owner);
        }

        private static void Simple(TipoMensaje tipo, string mensaje, string titulo, string detalle, IWin32Window owner)
        {
            var op = new OpcionesMensaje { Tipo = tipo, Mensaje = mensaje, Titulo = titulo, Detalle = detalle };
            op.AgregarBoton("Aceptar", true, DialogResult.OK, true, true);
            Mostrar(op, owner);
        }

        // ---------- Atajos con valor de retorno ----------

        /// <summary>Sí/No. Devuelve true si eligió el primer botón.</summary>
        public static bool Confirmar(string mensaje, string titulo = null,
            string textoSi = "Sí", string textoNo = "No",
            TipoMensaje tipo = TipoMensaje.Pregunta, bool noPorDefecto = false,
            IWin32Window owner = null)
        {
            var op = new OpcionesMensaje { Tipo = tipo, Mensaje = mensaje, Titulo = titulo };
            op.AgregarBoton(textoSi, true, DialogResult.Yes, !noPorDefecto, false);
            op.AgregarBoton(textoNo, false, DialogResult.No, noPorDefecto, true);
            return Mostrar(op, owner).ObtenerValor<bool>(false);
        }

        /// <summary>Sí / No / Cancelar clásico.</summary>
        public static DialogResult SiNoCancelar(string mensaje, string titulo = null,
            TipoMensaje tipo = TipoMensaje.Pregunta, IWin32Window owner = null)
        {
            var op = new OpcionesMensaje { Tipo = tipo, Mensaje = mensaje, Titulo = titulo };
            op.AgregarBoton("Sí", null, DialogResult.Yes, true);
            op.AgregarBoton("No", null, DialogResult.No);
            op.AgregarBoton("Cancelar", null, DialogResult.Cancel, false, true);
            return Mostrar(op, owner).Resultado;
        }

        /// <summary>Muestra N botones y devuelve el resultado con el Valor del elegido.</summary>
        public static ResultadoMensaje Elegir(string mensaje, string titulo, TipoMensaje tipo,
            params BotonMensaje[] botones)
        {
            var op = new OpcionesMensaje { Tipo = tipo, Mensaje = mensaje, Titulo = titulo };
            op.Botones.AddRange(botones);
            return Mostrar(op);
        }
    }
}
