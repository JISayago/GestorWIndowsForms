using Presentacion.FBase.Helpers;
using System.Drawing.Drawing2D;

namespace Presentacion.Notificaciones
{
    /// <summary>
    /// Franja de pestañas de los avisos: una por tipo, con punto de color, nombre corto y cantidad.
    /// La pestaña activa se resalta; un click sobre la activa pide plegarla.
    /// </summary>
    public class PestanasAvisos : Control
    {
        private const int Separacion = 4;

        private readonly List<TipoNotificacion> _tipos = new();
        private readonly Dictionary<TipoNotificacion, int> _cantidades = new();
        private readonly ToolTip _toolTip = new ToolTip();
        private TipoNotificacion? _seleccionada;
        private int _hover = -1;

        /// <summary>Se produce al hacer click izquierdo sobre una pestaña (tenga o no avisos).</summary>
        public event EventHandler<TipoNotificacion>? PestanaClick;

        public PestanasAvisos()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            Height = 40;
            Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            BackColor = TemaSistema.Fondo;
        }

        public void Configurar(IEnumerable<TipoNotificacion> tipos)
        {
            _tipos.Clear();
            _tipos.AddRange(tipos);
            Invalidate();
        }

        public void SetCantidad(TipoNotificacion tipo, int cantidad)
        {
            _cantidades[tipo] = cantidad;
            Invalidate();
        }

        public int Cantidad(TipoNotificacion tipo) => _cantidades.TryGetValue(tipo, out int c) ? c : 0;

        public TipoNotificacion? Seleccionada
        {
            get => _seleccionada;
            set
            {
                _seleccionada = value;
                Invalidate();
            }
        }

        private Rectangle RectanguloDe(int indice)
        {
            int n = Math.Max(_tipos.Count, 1);
            int ancho = (ClientSize.Width - Separacion * (n - 1)) / n;
            return new Rectangle(indice * (ancho + Separacion), 0, ancho, ClientSize.Height);
        }

        private int IndiceEn(Point p)
        {
            for (int i = 0; i < _tipos.Count; i++)
                if (RectanguloDe(i).Contains(p))
                    return i;
            return -1;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent?.BackColor ?? BackColor);

            for (int i = 0; i < _tipos.Count; i++)
            {
                TipoNotificacion tipo = _tipos[i];
                Rectangle r = RectanguloDe(i);
                bool activa = _seleccionada == tipo;
                int cantidad = Cantidad(tipo);
                bool vacia = cantidad == 0;

                Color fondo = activa ? TemaSistema.Primario : TemaSistema.Oscuro;
                if (!activa && i == _hover && !vacia)
                    fondo = ControlPaint.Light(TemaSistema.Oscuro, 0.15f);

                using (var b = new SolidBrush(fondo))
                    g.FillRectangle(b, r);

                if (activa)
                {
                    using var b = new SolidBrush(TemaSistema.Acento);
                    g.FillRectangle(b, r.X, r.Bottom - 3, r.Width, 3);
                }

                Color colorTexto = vacia && !activa ? Color.FromArgb(140, TemaSistema.Acento) : TemaSistema.Acento;
                Color colorPunto = AvisosEstilo.Color(tipo);
                if (vacia && !activa)
                    colorPunto = Color.FromArgb(110, colorPunto);

                string texto = $"{AvisosEstilo.NombreCorto(tipo)} ({cantidad})";
                int dot = 9, gapDot = 7, flecha = activa ? 16 : 0;
                Size medida = TextRenderer.MeasureText(g, texto, Font, new Size(int.MaxValue, int.MaxValue),
                    TextFormatFlags.NoPadding);

                int anchoDisponible = r.Width - 12;
                int anchoTexto = Math.Min(medida.Width, anchoDisponible - dot - gapDot - flecha);
                int anchoTotal = dot + gapDot + anchoTexto + flecha;
                int x = r.X + (r.Width - anchoTotal) / 2;
                int yCentro = r.Y + (r.Height - 3) / 2;

                using (var b = new SolidBrush(colorPunto))
                    g.FillEllipse(b, x, yCentro - dot / 2, dot, dot);

                var rectTexto = new Rectangle(x + dot + gapDot, r.Y, Math.Max(anchoTexto, 1), r.Height - 3);
                TextRenderer.DrawText(g, texto, Font, rectTexto, colorTexto,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding
                    | TextFormatFlags.Left);

                if (activa)
                {
                    int fx = rectTexto.Right + 8;
                    Point[] triangulo =
                    {
                        new Point(fx, yCentro + 2),
                        new Point(fx + 8, yCentro + 2),
                        new Point(fx + 4, yCentro - 3)
                    };
                    using var b = new SolidBrush(TemaSistema.Acento);
                    g.FillPolygon(b, triangulo);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int idx = IndiceEn(e.Location);
            if (idx != _hover)
            {
                _hover = idx;
                _toolTip.SetToolTip(this, idx >= 0 ? TextoAyuda(_tipos[idx]) : "");
                Invalidate();
            }

            Cursor = idx >= 0 && (Cantidad(_tipos[idx]) > 0 || _seleccionada == _tipos[idx])
                ? Cursors.Hand
                : Cursors.Default;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _hover = -1;
            Cursor = Cursors.Default;
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button != MouseButtons.Left)
                return;

            int idx = IndiceEn(e.Location);
            if (idx >= 0)
                PestanaClick?.Invoke(this, _tipos[idx]);
        }

        private string TextoAyuda(TipoNotificacion tipo)
        {
            string nombre = AvisosEstilo.NombreCompleto(tipo);
            if (_seleccionada == tipo)
                return nombre + "\nClick para plegar la lista";
            return Cantidad(tipo) > 0
                ? nombre + "\nClick para ver los avisos"
                : nombre + "\nSin avisos pendientes";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _toolTip.Dispose();
            base.Dispose(disposing);
        }
    }
}
