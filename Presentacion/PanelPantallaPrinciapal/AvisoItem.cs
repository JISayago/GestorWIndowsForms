using Presentacion.FBase.Helpers;
using Servicios.Helpers.Sistema;
using Servicios.LogicaNegocio.PantallaPrincipal.DTO;

namespace Presentacion.Notificaciones
{
    /// <summary>
    /// Tarjeta de un aviso: franja de urgencia a la izquierda, título y descripción.
    /// Click izquierdo: avisa para abrir la consulta. Click derecho: despliega un menú con
    /// "Marcar como leído", y recién al elegir esa opción se avisa (evita marcados accidentales).
    /// </summary>
    public class AvisoItem : Panel
    {
        private const int PaddingIzquierdo = 14;
        private const int PaddingDerecho = 10;

        private readonly Label _lblTitulo;
        private readonly Label _lblDescripcion;
        private readonly ContextMenuStrip _menu = new ContextMenuStrip();

        public NotificacionDTO Aviso { get; }

        /// <summary>Click izquierdo sobre la tarjeta o sus textos.</summary>
        public event EventHandler? AvisoClick;

        /// <summary>El usuario eligió "Marcar como leído" en el menú del click derecho.</summary>
        public event EventHandler? MarcarLeidoSolicitado;

        /// <summary>El mouse entró a la tarjeta; sirve para que la lista tome el foco y responda a la rueda.</summary>
        public event EventHandler? MouseEntro;

        public AvisoItem(NotificacionDTO aviso, string textoAyuda, ToolTip toolTip)
        {
            Aviso = aviso;

            Margin = new Padding(0, 0, 0, 8);
            BorderStyle = BorderStyle.FixedSingle;
            Cursor = Cursors.Hand;
            BackColor = aviso.Leida ? TemaSistema.FondoControl : Color.White;
            DoubleBuffered = true;

            _lblTitulo = new Label
            {
                Text = aviso.Titulo?.Trim(),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                ForeColor = TemaSistema.Texto,
                AutoSize = false,
                Left = PaddingIzquierdo,
                Top = 7
            };

            _lblDescripcion = new Label
            {
                Text = aviso.Descripcion,
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = TemaSistema.TextoSecundario,
                AutoSize = false,
                Left = PaddingIzquierdo
            };

            Controls.Add(_lblTitulo);
            Controls.Add(_lblDescripcion);

            var opcionLeido = new ToolStripMenuItem("Marcar como leído");
            opcionLeido.Click += (s, e) => MarcarLeidoSolicitado?.Invoke(this, EventArgs.Empty);
            _menu.Items.Add(opcionLeido);

            foreach (Control c in new Control[] { this, _lblTitulo, _lblDescripcion })
            {
                c.MouseClick += (s, e) =>
                {
                    if (e.Button == MouseButtons.Left)
                        AvisoClick?.Invoke(this, EventArgs.Empty);
                    else if (e.Button == MouseButtons.Right && !Aviso.Leida)
                        _menu.Show(c, e.Location);
                };
                c.MouseEnter += (s, e) => MouseEntro?.Invoke(this, EventArgs.Empty);
                toolTip.SetToolTip(c, textoAyuda);
            }
        }

        /// <summary>Fija el ancho, acomoda los textos (con salto de línea) y devuelve la altura resultante.</summary>
        public int AjustarAncho(int ancho)
        {
            Width = ancho;
            int anchoTexto = Math.Max(ancho - PaddingIzquierdo - PaddingDerecho, 60);

            _lblTitulo.Size = _lblTitulo.GetPreferredSize(new Size(anchoTexto, 0));
            _lblDescripcion.Top = _lblTitulo.Bottom + 3;
            _lblDescripcion.Size = _lblDescripcion.GetPreferredSize(new Size(anchoTexto, 0));

            Height = _lblDescripcion.Bottom + 9;
            return Height;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _menu.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Color urgencia = Aviso.NivelUrgencia switch
            {
                (int)NivelUrgencia.Alta => Color.Crimson,
                (int)NivelUrgencia.Media => Color.DarkOrange,
                (int)NivelUrgencia.Baja => Color.ForestGreen,
                _ => Color.Gray
            };

            using var brush = new SolidBrush(urgencia);
            e.Graphics.FillRectangle(brush, 0, 0, 6, Height);
        }
    }
}
