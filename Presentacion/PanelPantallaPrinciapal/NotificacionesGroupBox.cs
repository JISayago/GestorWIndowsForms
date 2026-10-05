using Presentacion.FBase.Helpers;
using Presentacion.Notificaciones;
using Servicios.Helpers.Sistema;
using Servicios.LogicaNegocio.PantallaPrincipal;
using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class NotificationGroupBox : GroupBox
{
    private FlowLayoutPanel panelItems;
    private bool expanded = false;
    private string _tituloVisual = "";
    private readonly IPantallaPrincipalServicio _pantallaPrincipalServicio;
    private readonly ToolTip _toolTip = new ToolTip();

    public event EventHandler NotificacionCambiada;

    public string TituloBase { get; private set; } = "";

    /// <summary>
    /// Tipo de aviso del grupo. Si está definido, el click izquierdo abre la consulta del objeto
    /// del aviso; si es null, el click izquierdo no hace nada. Se asigna antes de SetData.
    /// </summary>
    public TipoNotificacion? Tipo { get; set; }

    public bool Expanded
    {
        get => expanded;
        set
        {
            expanded = value;
            AplicarEstado();
        }
    }

    private readonly Color COLOR_TITULO_FONDO = TemaSistema.Oscuro;
    private readonly Color COLOR_TITULO_TEXTO = TemaSistema.Acento;
    private readonly Color COLOR_ITEM_FONDO_LEIDO = TemaSistema.FondoControl;
    private readonly Color COLOR_ITEM_FONDO_NUEVO = Color.White;
    private readonly Color COLOR_TEXTO_PRINCIPAL = TemaSistema.Texto;
    private readonly Color COLOR_TEXTO_SECUNDARIO = TemaSistema.TextoSecundario;

    public NotificationGroupBox()
    {
        this.Padding = new Padding(12, 45, 12, 12);
        this.DoubleBuffered = true;
        this.BackColor = TemaSistema.Fondo;
        this.ForeColor = TemaSistema.Texto;
        _pantallaPrincipalServicio = new PantallaPrincipalServicio();
        _toolTip.SetToolTip(this, "Click en el título: abrir o cerrar el grupo\nClick derecho en un aviso: marcarlo como leído");
        InicializarComponentes();
    }

    private void InicializarComponentes()
    {
        this.Height = 50;
        this.Font = new Font("Segoe UI", 9);

        panelItems = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BorderStyle = BorderStyle.None,
            BackColor = Color.Transparent
        };

        this.Controls.Add(panelItems);

        // Abrir/cerrar con click en la cabecera (reemplaza al botón de la flecha).
        this.MouseClick += GroupBox_MouseClick;
        this.MouseMove += GroupBox_MouseMove;

        this.Resize += (s, e) =>
        {
            foreach (Control ctrl in panelItems.Controls)
            {
                ctrl.Width = panelItems.ClientSize.Width - 5;
                foreach (Control child in ctrl.Controls)
                {
                    if (child is Label lbl && lbl.ForeColor == COLOR_TEXTO_SECUNDARIO)
                        lbl.MaximumSize = new Size(ctrl.Width - 25, 0);
                }
            }
            this.Invalidate();
        };
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (string.IsNullOrEmpty(_tituloVisual)) return;

        Graphics g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        using Font fontTitulo = new Font("Segoe UI", 8.5f, FontStyle.Bold);
        Size sizeTexto = TextRenderer.MeasureText(_tituloVisual, fontTitulo);
        Rectangle rectFondo = new Rectangle(10, 0, sizeTexto.Width + 20, 22);

        using (SolidBrush brushFondo = new SolidBrush(COLOR_TITULO_FONDO))
            g.FillRectangle(brushFondo, rectFondo);

        TextRenderer.DrawText(g, _tituloVisual, fontTitulo, rectFondo,
            COLOR_TITULO_TEXTO, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
    }

    public void SetData(List<NotificacionDTO> notificaciones, string tituloBase)
    {
        this.Visible = true;
        TituloBase = tituloBase;

        int conteo = notificaciones?.Count ?? 0;
        ActualizarTituloVisual(conteo);
        this.Text = "";
        panelItems.Controls.Clear();

        if (conteo > 0)
        {
            var ordenadas = notificaciones
                .OrderByDescending(n => n.NivelUrgencia)
                .ThenByDescending(n => n.FechaNotificacion)
                .ToList();

            foreach (var item in ordenadas)
                panelItems.Controls.Add(CrearItem(item));
        }
        else
        {
            expanded = false;
        }

        this.Invalidate();
        AplicarEstado();
    }

    private void ActualizarTituloVisual(int conteo)
    {
        _tituloVisual = $"{TituloBase} ({conteo})".ToUpper();
        this.Invalidate();
    }

    private Control CrearItem(NotificacionDTO item)
    {
        var panelItem = new Panel
        {
            Width = Math.Max(panelItems.ClientSize.Width - 5, 100),
            Margin = new Padding(0, 0, 0, 8),
            BorderStyle = BorderStyle.FixedSingle,
            Cursor = Cursors.Hand,
            BackColor = item.Leida ? COLOR_ITEM_FONDO_LEIDO : COLOR_ITEM_FONDO_NUEVO
        };

        string textoAyuda = Tipo.HasValue
            ? "Click izquierdo: abrir la consulta\nClick derecho: marcar como leído"
            : "Click derecho: marcar como leído";

        _toolTip.SetToolTip(panelItem, textoAyuda);

        panelItem.Paint += (s, e) =>
        {
            Color colorUrgencia = item.NivelUrgencia switch
            {
                (int)NivelUrgencia.Alta => Color.Crimson,
                (int)NivelUrgencia.Media => Color.DarkOrange,
                (int)NivelUrgencia.Baja => Color.ForestGreen,
                _ => Color.Gray
            };
            using SolidBrush brush = new SolidBrush(colorUrgencia);
            e.Graphics.FillRectangle(brush, 0, 0, 6, panelItem.Height);
        };

        var lblTitulo = new Label
        {
            Text = item.Titulo,
            Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
            Top = 8,
            Left = 12,
            ForeColor = COLOR_TEXTO_PRINCIPAL,
            AutoSize = true
        };

        var lblDescripcion = new Label
        {
            Text = item.Descripcion,
            Font = new Font("Segoe UI", 8.5f),
            Left = 12,
            Top = lblTitulo.Bottom + 4,
            ForeColor = COLOR_TEXTO_SECUNDARIO,
            AutoSize = true,
            MaximumSize = new Size(panelItem.Width - 20, 0)
        };

        MouseEventHandler unifiedClickHandler = (s, e) =>
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Tipo.HasValue)
                    NotificacionNavegador.Abrir(Tipo.Value, item, _pantallaPrincipalServicio);
                return;
            }

            if (e.Button != MouseButtons.Right || item.Leida)
                return;

            item.Leida = true;
            _pantallaPrincipalServicio.MarcarNotificacionComoLeida(item.NotificacionId);

            // La lista solo muestra no leídas: quitar el ítem del UI sin rebuild de los 4 grupos.
            panelItems.SuspendLayout();
            panelItems.Controls.Remove(panelItem);
            panelItem.Dispose();
            panelItems.ResumeLayout();

            int restante = panelItems.Controls.Count;
            ActualizarTituloVisual(restante);
            if (restante == 0)
                expanded = false;
            AplicarEstado();

            NotificacionCambiada?.Invoke(this, EventArgs.Empty);
        };

        panelItem.MouseClick += unifiedClickHandler;
        lblTitulo.MouseClick += unifiedClickHandler;
        lblDescripcion.MouseClick += unifiedClickHandler;
        _toolTip.SetToolTip(lblTitulo, textoAyuda);
        _toolTip.SetToolTip(lblDescripcion, textoAyuda);

        panelItem.Controls.Add(lblTitulo);
        panelItem.Controls.Add(lblDescripcion);
        panelItem.Height = lblDescripcion.Bottom + 12;
        return panelItem;
    }

    // Cerrado: toda el área del grupo (50 px) es cabecera y abre. Abierto: solo la franja del
    // título (el padding superior) cierra; el resto son los avisos, que tienen sus propios clicks.
    private bool EsZonaCabecera(Point p)
    {
        return !expanded || p.Y < this.Padding.Top;
    }

    private bool TieneAvisos => panelItems.Controls.Count > 0;

    private void GroupBox_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left || !TieneAvisos || !EsZonaCabecera(e.Location))
            return;

        expanded = !expanded;
        AplicarEstado();
    }

    private void GroupBox_MouseMove(object sender, MouseEventArgs e)
    {
        this.Cursor = TieneAvisos && EsZonaCabecera(e.Location)
            ? Cursors.Hand
            : Cursors.Default;
    }

    private void AplicarEstado()
    {
        panelItems.Visible = expanded;
        this.Height = expanded
            ? Math.Max(panelItems.Bottom + this.Padding.Bottom, 50)
            : 50;
    }
}
