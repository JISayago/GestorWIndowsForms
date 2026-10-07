using Presentacion.FBase.Helpers;
using Servicios.LogicaNegocio.PantallaPrincipal;
using Servicios.LogicaNegocio.PantallaPrincipal.DTO;

namespace Presentacion.Notificaciones
{
    /// <summary>
    /// Panel de avisos de la pantalla principal: una franja de pestañas (una por tipo de aviso),
    /// la lista del tipo elegido (con scroll propio) y debajo el calendario de vencimientos.
    /// Con todas las pestañas plegadas, el calendario ocupa el espacio de la lista.
    /// </summary>
    public class PanelAvisosCalendario : Panel
    {
        private const int AltoListaMaximo = 300;
        private const int AltoListaMinimo = 120;
        private const int AltoCalendarioMinimo = 280;
        private const int Separacion = 8;

        private readonly IPantallaPrincipalServicio _servicio = new PantallaPrincipalServicio();
        private readonly ToolTip _toolTip = new ToolTip();
        private readonly Dictionary<TipoNotificacion, List<NotificacionDTO>> _avisos = new();

        private readonly TableLayoutPanel _layout;
        private readonly PestanasAvisos _pestanas;
        private readonly ListaAvisos _lista;
        private readonly CalendarioVencimientos _calendario;

        private TipoNotificacion? _seleccionada;
        private bool _primeraCarga = true;

        public PanelAvisosCalendario()
        {
            DoubleBuffered = true;
            BackColor = TemaSistema.Fondo;
            Padding = new Padding(14);

            _pestanas = new PestanasAvisos { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, Separacion) };
            _pestanas.Configurar(AvisosEstilo.Tipos);
            _pestanas.PestanaClick += Pestanas_PestanaClick;

            _lista = new ListaAvisos
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, Separacion),
                Visible = false
            };
            _lista.SizeChanged += (s, e) => AjustarAnchosItems();

            _calendario = new CalendarioVencimientos { Dock = DockStyle.Fill, Margin = Padding.Empty };
            _calendario.MesCambiado += (s, e) => RefrescarCalendario();

            _layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.Transparent
            };
            _layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F + Separacion));
            _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 0F));
            _layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _layout.Controls.Add(_pestanas, 0, 0);
            _layout.Controls.Add(_lista, 0, 1);
            _layout.Controls.Add(_calendario, 0, 2);

            Controls.Add(_layout);

            Resize += (s, e) => AjustarAlturaLista();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using var pen = new Pen(TemaSistema.Borde);
            e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
        }

        /// <summary>
        /// Genera los avisos pendientes del sistema, los trae de la base y actualiza pestañas, lista y calendario.
        /// Conserva la pestaña abierta si sigue teniendo avisos.
        /// </summary>
        public void Recargar()
        {
            _servicio.NotifiacionesProductosVencidos();
            _servicio.NotificacionesOfertasVencidas();
            _servicio.NotificacionesOfertasBajoStock();
            _servicio.NotificacionesCtaCteVencidas();

            _avisos[TipoNotificacion.LoteVencido] = Copiar(_servicio.ObtenerNotificacionesProdutosVencidos());
            _avisos[TipoNotificacion.OfertaVencida] = Copiar(_servicio.ObtenerNotificacionesOfertasVencidas());
            _avisos[TipoNotificacion.OfertaBajoStock] = Copiar(_servicio.ObtenerNotificacionesOfertasBajoStock());
            _avisos[TipoNotificacion.CuentaCorrienteVencida] = Copiar(_servicio.ObtenerNotificacionesCtaCteVencidas());

            if (_primeraCarga)
            {
                // Al abrir la pantalla queda elegida la primera pestaña que tenga avisos.
                _seleccionada = AvisosEstilo.Tipos.Cast<TipoNotificacion?>()
                    .FirstOrDefault(t => Cantidad(t!.Value) > 0);
                _primeraCarga = false;
            }
            else if (_seleccionada.HasValue && Cantidad(_seleccionada.Value) == 0)
            {
                _seleccionada = null;
            }

            AplicarVista();
            RefrescarCalendario();
        }

        private static List<NotificacionDTO> Copiar(List<NotificacionDTO>? origen)
        {
            return (origen ?? new List<NotificacionDTO>())
                .OrderByDescending(n => n.NivelUrgencia)
                .ThenByDescending(n => n.FechaNotificacion)
                .ToList();
        }

        private int Cantidad(TipoNotificacion tipo)
            => _avisos.TryGetValue(tipo, out var lista) ? lista.Count : 0;

        private void Pestanas_PestanaClick(object? sender, TipoNotificacion tipo)
        {
            if (_seleccionada == tipo)
                _seleccionada = null;          // click sobre la activa: pliega la lista
            else if (Cantidad(tipo) > 0)
                _seleccionada = tipo;          // las pestañas sin avisos no se abren
            else
                return;

            AplicarVista();
        }

        /// <summary>Refleja el estado actual en pestañas, alto de la lista y contenido de la lista.</summary>
        private void AplicarVista()
        {
            foreach (var tipo in AvisosEstilo.Tipos)
                _pestanas.SetCantidad(tipo, Cantidad(tipo));
            _pestanas.Seleccionada = _seleccionada;

            _layout.SuspendLayout();
            if (_seleccionada.HasValue)
            {
                _lista.Visible = true;
                _layout.RowStyles[1].Height = AlturaLista() + Separacion;
                LlenarLista(_seleccionada.Value);
            }
            else
            {
                _lista.Visible = false;
                _layout.RowStyles[1].Height = 0F;
            }
            _layout.ResumeLayout(true);

            AjustarAnchosItems();
        }

        /// <summary>Al cambiar el tamaño del panel solo se reacomoda el alto de la lista, sin reconstruir las tarjetas.</summary>
        private void AjustarAlturaLista()
        {
            if (!_seleccionada.HasValue)
                return;

            _layout.RowStyles[1].Height = AlturaLista() + Separacion;
            AjustarAnchosItems();
        }

        private int AlturaLista()
        {
            int dpi(int v) => this.LogicalToDeviceUnits(v);
            int ocupado = Padding.Vertical + dpi(40) + dpi(Separacion) * 2 + dpi(AltoCalendarioMinimo);
            int disponible = Height - ocupado;
            return Math.Max(dpi(AltoListaMinimo), Math.Min(dpi(AltoListaMaximo), disponible));
        }

        private void LlenarLista(TipoNotificacion tipo)
        {
            _lista.SuspendLayout();
            foreach (Control c in _lista.Controls.Cast<Control>().ToList())
            {
                _lista.Controls.Remove(c);
                c.Dispose();
            }

            string ayuda = "Click izquierdo: abrir la consulta\nClick derecho: marcar como leído";
            foreach (var aviso in _avisos[tipo])
            {
                var item = new AvisoItem(aviso, ayuda, _toolTip);
                item.AvisoClick += (s, e) => Item_AvisoClick(tipo, item, e);
                item.MouseEntro += (s, e) => _lista.Focus();
                _lista.Controls.Add(item);
            }
            _lista.ResumeLayout(true);
            _lista.AutoScrollPosition = new Point(0, 0);
        }

        private void Item_AvisoClick(TipoNotificacion tipo, AvisoItem item, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NotificacionNavegador.Abrir(tipo, item.Aviso, _servicio);
                return;
            }

            if (e.Button != MouseButtons.Right || item.Aviso.Leida)
                return;

            item.Aviso.Leida = true;
            _servicio.MarcarNotificacionComoLeida(item.Aviso.NotificacionId);
            _avisos[tipo].Remove(item.Aviso);

            if (_avisos[tipo].Count == 0)
                _seleccionada = null;

            AplicarVista();
            RefrescarCalendario();   // el día deja de marcarse (y de estar en rojo) si ya no tiene avisos pendientes
        }

        /// <summary>Calcula el ancho de las tarjetas reservando el lugar de la barra de scroll solo si hace falta.</summary>
        private void AjustarAnchosItems()
        {
            if (!_lista.Visible || _lista.Controls.Count == 0)
                return;

            var items = _lista.Controls.OfType<AvisoItem>().ToList();
            int anchoCompleto = Math.Max(_lista.Width, 100);
            int barra = SystemInformation.VerticalScrollBarWidth;

            int Medir(int ancho) => items.Sum(i => i.AjustarAncho(ancho) + i.Margin.Vertical);

            int total = Medir(anchoCompleto);
            if (total > _lista.Height)
                Medir(anchoCompleto - barra - 2);
        }

        private void RefrescarCalendario()
        {
            try
            {
                var vencimientos = _servicio.ObtenerVencimientosCalendario(
                    _calendario.PrimerDiaVisible, _calendario.UltimoDiaVisible);
                _calendario.SetVencimientos(vencimientos);
            }
            catch (Exception ex)
            {
                // El calendario es informativo: si falla la consulta no debe impedir usar la pantalla principal.
                System.Diagnostics.Debug.WriteLine($"No se pudieron cargar los vencimientos del calendario: {ex.Message}");
                _calendario.SetVencimientos(null);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _toolTip.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>Lista vertical con scroll propio que puede tomar el foco, para responder a la rueda del mouse.</summary>
        private sealed class ListaAvisos : FlowLayoutPanel
        {
            public ListaAvisos()
            {
                SetStyle(ControlStyles.Selectable, true);
                TabStop = false;
                AutoScroll = true;
                FlowDirection = FlowDirection.TopDown;
                WrapContents = false;
                BackColor = Color.Transparent;
            }
        }
    }
}
