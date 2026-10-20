namespace Presentacion.Notificaciones
{
    /// <summary>
    /// Lista vertical de tarjetas de avisos con scroll propio. Acomoda las tarjetas a mano:
    /// ocupan todo el ancho y solo se reserva lugar para la barra de scroll cuando hace falta,
    /// de modo que la barra nunca tapa el borde de las tarjetas.
    /// </summary>
    public class ListaAvisosPanel : Panel
    {
        private readonly List<AvisoItem> _items = new();
        private bool _distribuyendo;

        public ListaAvisosPanel()
        {
            SetStyle(ControlStyles.Selectable, true);   // puede tomar el foco y recibir la rueda del mouse
            TabStop = false;
            AutoScroll = true;
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            SizeChanged += (s, e) => Distribuir();
        }

        public IReadOnlyList<AvisoItem> Items => _items;

        public void SetItems(IEnumerable<AvisoItem> items)
        {
            SuspendLayout();
            foreach (var viejo in _items)
            {
                Controls.Remove(viejo);
                viejo.Dispose();
            }
            _items.Clear();

            foreach (var item in items)
            {
                _items.Add(item);
                Controls.Add(item);
            }

            ResumeLayout(false);
            Distribuir();
        }

        /// <summary>Altura total que ocuparían las tarjetas con el ancho dado (sin dejarlas colocadas).</summary>
        public int AlturaContenido(int ancho) => Colocar(ancho);

        public void Distribuir()
        {
            if (_distribuyendo)
                return;

            _distribuyendo = true;
            try
            {
                SuspendLayout();
                AutoScrollPosition = Point.Empty;

                if (_items.Count == 0 || Width <= 0)
                {
                    AutoScrollMinSize = Size.Empty;
                    return;
                }

                int ancho = Width;
                int total = Colocar(ancho);
                if (total > Height)
                {
                    ancho = Width - SystemInformation.VerticalScrollBarWidth;
                    total = Colocar(ancho);
                }

                AutoScrollMinSize = new Size(0, total);
            }
            finally
            {
                ResumeLayout(true);
                _distribuyendo = false;
            }
        }

        private int Colocar(int ancho)
        {
            int y = 0;
            foreach (var item in _items)
            {
                item.AjustarAncho(Math.Max(ancho - 2, 60));
                item.Location = new Point(0, y);
                y += item.Height + item.Margin.Bottom;
            }
            return y;
        }
    }
}
