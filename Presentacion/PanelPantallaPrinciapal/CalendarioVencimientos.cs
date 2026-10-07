using Presentacion.FBase.Helpers;
using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
using System.Globalization;

namespace Presentacion.Notificaciones
{
    /// <summary>Día del calendario clickeado (solo se produce en días con avisos pendientes).</summary>
    public class DiaCalendarioEventArgs : EventArgs
    {
        public DateTime Fecha { get; }
        /// <summary>Rectángulo de la celda en coordenadas del calendario.</summary>
        public Rectangle Celda { get; }

        public DiaCalendarioEventArgs(DateTime fecha, Rectangle celda)
        {
            Fecha = fecha;
            Celda = celda;
        }
    }

    /// <summary>
    /// Calendario mensual de solo lectura. Marca con un punto de color (uno por tipo) los días con
    /// avisos pendientes; los días ya pasados con avisos pendientes llevan el número en rojo.
    /// Las flechas del encabezado cambian de mes; un click en un día con avisos produce <see cref="DiaClick"/>.
    /// </summary>
    public class CalendarioVencimientos : Control
    {
        private const int AltoEncabezado = 42;
        private const int AltoDiasSemana = 28;
        private const int AltoLeyenda = 30;

        private static readonly CultureInfo Cultura = new CultureInfo("es-AR");
        private static readonly string[] DiasSemana = { "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb", "Dom" };
        private static readonly Color ColorLinea = ColorTranslator.FromHtml("#D8D3DE");
        private static readonly Color ColorFinDeSemana = ColorTranslator.FromHtml("#F1EEE7");

        private DateTime _mes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        private Dictionary<DateTime, HashSet<TipoVencimientoCalendario>> _porDia = new();
        private Rectangle _rectAnterior;
        private Rectangle _rectSiguiente;

        /// <summary>Se produce al cambiar el mes mostrado; hay que volver a cargar los vencimientos del rango visible.</summary>
        public event EventHandler? MesCambiado;

        /// <summary>Se produce al hacer click en un día que tiene avisos pendientes.</summary>
        public event EventHandler<DiaCalendarioEventArgs>? DiaClick;

        public CalendarioVencimientos()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            Font = new Font("Segoe UI", 9.5f);
            BackColor = TemaSistema.Alternado;
        }

        private int DesplazamientoPrimerDia => ((int)_mes.DayOfWeek + 6) % 7; // lunes = 0

        private int Semanas => (DesplazamientoPrimerDia + DateTime.DaysInMonth(_mes.Year, _mes.Month) + 6) / 7;

        /// <summary>Primer día de la grilla (incluye días del mes anterior).</summary>
        public DateTime PrimerDiaVisible => _mes.AddDays(-DesplazamientoPrimerDia);

        /// <summary>Último día de la grilla (incluye días del mes siguiente).</summary>
        public DateTime UltimoDiaVisible => PrimerDiaVisible.AddDays(Semanas * 7 - 1);

        public void SetVencimientos(IEnumerable<VencimientoCalendarioDTO>? vencimientos)
        {
            _porDia = new Dictionary<DateTime, HashSet<TipoVencimientoCalendario>>();
            if (vencimientos != null)
            {
                foreach (var v in vencimientos)
                {
                    DateTime dia = v.Fecha.Date;
                    if (!_porDia.TryGetValue(dia, out var tipos))
                        _porDia[dia] = tipos = new HashSet<TipoVencimientoCalendario>();
                    tipos.Add(v.Tipo);
                }
            }

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(BackColor);

            int w = ClientSize.Width;
            int h = ClientSize.Height;
            DateTime hoy = DateTime.Today;

            DibujarEncabezado(g, w);

            // Días de la semana
            var rectSemana = new Rectangle(0, AltoEncabezado, w, AltoDiasSemana);
            using (var b = new SolidBrush(TemaSistema.FondoControl))
                g.FillRectangle(b, rectSemana);
            float anchoCelda = w / 7f;
            for (int i = 0; i < 7; i++)
            {
                var r = new Rectangle((int)(i * anchoCelda), rectSemana.Y, (int)anchoCelda, rectSemana.Height);
                using var fuente = new Font("Segoe UI", 8.5f, FontStyle.Bold);
                TextRenderer.DrawText(g, DiasSemana[i], fuente, r, TemaSistema.TextoSecundario,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            using (var p = new Pen(TemaSistema.Borde))
                g.DrawLine(p, 0, rectSemana.Bottom - 1, w, rectSemana.Bottom - 1);

            // Grilla
            int yGrilla = rectSemana.Bottom;
            int altoGrilla = Math.Max(h - yGrilla - AltoLeyenda, 1);
            int semanas = Semanas;
            float altoCelda = altoGrilla / (float)semanas;
            DateTime dia = PrimerDiaVisible;

            using var fuenteNormal = new Font("Segoe UI", 9.5f);
            using var fuenteNegrita = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            using var penLinea = new Pen(ColorLinea);

            for (int fila = 0; fila < semanas; fila++)
            {
                for (int col = 0; col < 7; col++, dia = dia.AddDays(1))
                {
                    var celda = new RectangleF(col * anchoCelda, yGrilla + fila * altoCelda, anchoCelda, altoCelda);
                    var rc = Rectangle.Round(celda);

                    bool esHoy = dia == hoy;
                    bool otroMes = dia.Month != _mes.Month;
                    bool tieneAvisos = _porDia.TryGetValue(dia, out var tipos) && tipos.Count > 0;
                    bool vencidoPendiente = tieneAvisos && dia < hoy;

                    Color fondo = esHoy ? TemaSistema.Seleccion : (col >= 5 ? ColorFinDeSemana : BackColor);
                    using (var b = new SolidBrush(fondo))
                        g.FillRectangle(b, rc);

                    g.DrawLine(penLinea, rc.Right - 1, rc.Top, rc.Right - 1, rc.Bottom);
                    g.DrawLine(penLinea, rc.Left, rc.Bottom - 1, rc.Right, rc.Bottom - 1);

                    if (esHoy)
                    {
                        using var pen = new Pen(TemaSistema.Primario, 2);
                        g.DrawRectangle(pen, rc.X + 1, rc.Y + 1, rc.Width - 3, rc.Height - 3);
                    }

                    Color colorNumero = vencidoPendiente ? Color.Crimson
                        : otroMes ? TemaSistema.TextoSecundario
                        : TemaSistema.Texto;
                    Font fuenteDia = (esHoy || vencidoPendiente) ? fuenteNegrita : fuenteNormal;
                    TextRenderer.DrawText(g, dia.Day.ToString(), fuenteDia,
                        new Rectangle(rc.X + 6, rc.Y + 4, rc.Width - 8, 22), colorNumero,
                        TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.NoPadding);

                    if (tieneAvisos)
                    {
                        int x = rc.X + 8;
                        int y = rc.Bottom - 8 - 7;
                        foreach (var tipo in tipos!.OrderBy(t => t))
                        {
                            using var b = new SolidBrush(AvisosEstilo.Color(tipo));
                            g.FillEllipse(b, x, y, 7, 7);
                            x += 10;
                        }
                    }
                }
            }

            DibujarLeyenda(g, w, h);

            using (var p = new Pen(TemaSistema.Borde))
                g.DrawRectangle(p, 0, 0, w - 1, h - 1);
        }

        private void DibujarEncabezado(Graphics g, int w)
        {
            var rect = new Rectangle(0, 0, w, AltoEncabezado);
            using (var b = new SolidBrush(TemaSistema.Oscuro))
                g.FillRectangle(b, rect);

            string titulo = _mes.ToString("MMMM yyyy", Cultura).ToUpper(Cultura);
            using (var fuente = new Font("Segoe UI", 9.5f, FontStyle.Bold))
                TextRenderer.DrawText(g, titulo, fuente, rect, TemaSistema.Acento,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            _rectAnterior = new Rectangle(8, (AltoEncabezado - 32) / 2, 32, 32);
            _rectSiguiente = new Rectangle(w - 8 - 32, (AltoEncabezado - 32) / 2, 32, 32);

            using var pen = new Pen(TemaSistema.Acento, 2);
            int cy = AltoEncabezado / 2;
            int ax = _rectAnterior.X + 16;
            g.DrawLines(pen, new[] { new Point(ax + 3, cy - 5), new Point(ax - 3, cy), new Point(ax + 3, cy + 5) });
            int sx = _rectSiguiente.X + 16;
            g.DrawLines(pen, new[] { new Point(sx - 3, cy - 5), new Point(sx + 3, cy), new Point(sx - 3, cy + 5) });
        }

        private void DibujarLeyenda(Graphics g, int w, int h)
        {
            var rect = new Rectangle(0, h - AltoLeyenda, w, AltoLeyenda);
            using (var b = new SolidBrush(TemaSistema.FondoControl))
                g.FillRectangle(b, rect);
            using (var p = new Pen(TemaSistema.Borde))
                g.DrawLine(p, 0, rect.Y, w, rect.Y);

            using var fuente = new Font("Segoe UI", 8.5f);
            using var fuenteNegrita = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            int x = 12;
            int cy = rect.Y + rect.Height / 2;

            var items = new (string Texto, Color Color)[]
            {
                ("Lotes", AvisosEstilo.ColorLote),
                ("Ofertas", AvisosEstilo.ColorOferta),
                ("Ctas. ctes.", AvisosEstilo.ColorCuentaCorriente)
            };

            foreach (var (texto, color) in items)
            {
                using (var b = new SolidBrush(color))
                    g.FillEllipse(b, x, cy - 3, 7, 7);
                x += 12;
                Size medida = TextRenderer.MeasureText(g, texto, fuente, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding);
                TextRenderer.DrawText(g, texto, fuente, new Point(x, cy - medida.Height / 2),
                    TemaSistema.TextoSecundario, TextFormatFlags.NoPadding);
                x += medida.Width + 14;
            }

            // Referencia del rojo, alineada a la derecha
            const string explicacion = "vencido sin resolver";
            Size mExp = TextRenderer.MeasureText(g, explicacion, fuente, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding);
            Size mNum = TextRenderer.MeasureText(g, "12", fuenteNegrita, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding);
            int xExp = w - 12 - mExp.Width;
            int xNum = xExp - 5 - mNum.Width;
            if (xNum > x)
            {
                TextRenderer.DrawText(g, "12", fuenteNegrita, new Point(xNum, cy - mNum.Height / 2),
                    Color.Crimson, TextFormatFlags.NoPadding);
                TextRenderer.DrawText(g, explicacion, fuente, new Point(xExp, cy - mExp.Height / 2),
                    TemaSistema.TextoSecundario, TextFormatFlags.NoPadding);
            }
        }

        /// <summary>Celda de la grilla bajo el punto, si el punto cae dentro de la grilla.</summary>
        private (DateTime Fecha, Rectangle Celda)? CeldaEn(Point p)
        {
            int yGrilla = AltoEncabezado + AltoDiasSemana;
            int altoGrilla = Math.Max(ClientSize.Height - yGrilla - AltoLeyenda, 1);
            if (p.Y < yGrilla || p.Y >= yGrilla + altoGrilla || p.X < 0 || p.X >= ClientSize.Width)
                return null;

            int semanas = Semanas;
            float anchoCelda = ClientSize.Width / 7f;
            float altoCelda = altoGrilla / (float)semanas;
            int col = Math.Min((int)(p.X / anchoCelda), 6);
            int fila = Math.Min((int)((p.Y - yGrilla) / altoCelda), semanas - 1);

            DateTime fecha = PrimerDiaVisible.AddDays(fila * 7 + col);
            var celda = Rectangle.Round(new RectangleF(col * anchoCelda, yGrilla + fila * altoCelda, anchoCelda, altoCelda));
            return (fecha, celda);
        }

        private bool TieneAvisos(DateTime fecha) => _porDia.TryGetValue(fecha, out var tipos) && tipos.Count > 0;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            var celda = CeldaEn(e.Location);
            bool mano = _rectAnterior.Contains(e.Location)
                        || _rectSiguiente.Contains(e.Location)
                        || (celda.HasValue && TieneAvisos(celda.Value.Fecha));
            Cursor = mano ? Cursors.Hand : Cursors.Default;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button != MouseButtons.Left)
                return;

            if (_rectAnterior.Contains(e.Location))
            {
                CambiarMes(-1);
                return;
            }
            if (_rectSiguiente.Contains(e.Location))
            {
                CambiarMes(1);
                return;
            }

            var celda = CeldaEn(e.Location);
            if (celda.HasValue && TieneAvisos(celda.Value.Fecha))
                DiaClick?.Invoke(this, new DiaCalendarioEventArgs(celda.Value.Fecha, celda.Value.Celda));
        }

        private void CambiarMes(int delta)
        {
            _mes = _mes.AddMonths(delta);
            _porDia = new Dictionary<DateTime, HashSet<TipoVencimientoCalendario>>();
            Invalidate();
            MesCambiado?.Invoke(this, EventArgs.Empty);
        }
    }
}
