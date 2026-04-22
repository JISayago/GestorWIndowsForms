using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
using Servicios.Helpers.Sistema;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class NotificationGroupBox : GroupBox
{
    private Button btnToggle;
    private FlowLayoutPanel panelItems;
    private bool expanded = true;
    private string _tituloVisual = "";

    // ===========================================================================
    // CONFIGURACIÓN DE COLORES (Modificar aquí para Temas Claro/Oscuro)
    // ===========================================================================

    // --- Sección: Título del GroupBox (Badge Superior) ---
    private readonly Color COLOR_TITULO_FONDO = Color.FromArgb(50, 50, 50); // Fondo oscuro del badge
    private readonly Color COLOR_TITULO_TEXTO = Color.White;                // Texto blanco del badge

    // --- Sección: Ítems de Notificación ---
    private readonly Color COLOR_ITEM_FONDO_LEIDO = Color.FromArgb(242, 242, 242); // Gris tenue para leídos
    private readonly Color COLOR_ITEM_FONDO_NUEVO = Color.White;                    // Blanco para no leídos
    private readonly Color COLOR_ITEM_BORDE = Color.DarkGray;                      // Color de la línea del borde del ítem

    // --- Sección: Textos ---
    private readonly Color COLOR_TEXTO_PRINCIPAL = Color.FromArgb(40, 40, 40);   // Gris muy oscuro (Título ítem)
    private readonly Color COLOR_TEXTO_SECUNDARIO = Color.FromArgb(100, 100, 100); // Gris medio (Descripción)

    // --- Sección: Botón Expandir/Contraer ---
    private readonly Color COLOR_BTN_FONDO = Color.FromArgb(230, 230, 230); // Fondo del botón de la flecha
    private readonly Color COLOR_BTN_BORDE = Color.Black;                  // Borde del botón de la flecha
    private readonly Color COLOR_BTN_TEXTO = Color.Black;                  // Color de la flecha (▲/▼)

    // ===========================================================================

    public NotificationGroupBox()
    {
        // Espaciado interno para evitar que los ítems toquen el marco
        this.Padding = new Padding(12, 45, 12, 12);
        this.DoubleBuffered = true;
        InicializarComponentes();
    }

    private void InicializarComponentes()
    {
        this.Height = 50;
        this.Font = new Font("Segoe UI", 9);

        btnToggle = new Button();
        btnToggle.Text = "▲";
        btnToggle.Width = 30;
        btnToggle.Height = 25;

        // CAMBIO SOLICITADO: 
        // X: this.Width - 50 (se aleja del borde derecho)
        // Y: 12 (baja el botón para que no toque el borde superior)
        btnToggle.Location = new Point(this.Width - 50, 12);

        btnToggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnToggle.FlatStyle = FlatStyle.Flat;

        // Aplicamos los colores del botón desde la configuración
        btnToggle.BackColor = COLOR_BTN_FONDO;
        btnToggle.ForeColor = COLOR_BTN_TEXTO;
        btnToggle.FlatAppearance.BorderColor = COLOR_BTN_BORDE;

        btnToggle.Click += BtnToggle_Click;

        panelItems = new FlowLayoutPanel();
        panelItems.Dock = DockStyle.Top;
        panelItems.AutoSize = true;
        panelItems.FlowDirection = FlowDirection.TopDown;
        panelItems.WrapContents = false;
        panelItems.BorderStyle = BorderStyle.None;
        panelItems.BackColor = Color.Transparent; // El fondo salmón se ve a través

        this.Controls.Add(panelItems);
        this.Controls.Add(btnToggle);

        this.Resize += (s, e) =>
        {
            // Mantiene la nueva posición al redimensionar
            btnToggle.Location = new Point(this.Width - 50, 12);

            foreach (Control ctrl in panelItems.Controls)
            {
                ctrl.Width = panelItems.ClientSize.Width - 5;
                foreach (Control child in ctrl.Controls)
                {
                    if (child is Label lbl && lbl.ForeColor == COLOR_TEXTO_SECUNDARIO)
                    {
                        lbl.MaximumSize = new Size(ctrl.Width - 25, 0);
                    }
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

        using (Font fontTitulo = new Font("Segoe UI", 8.5f, FontStyle.Bold))
        {
            Size sizeTexto = TextRenderer.MeasureText(_tituloVisual, fontTitulo);
            Rectangle rectFondo = new Rectangle(10, 0, sizeTexto.Width + 20, 22);

            // Dibujo del Badge Superior
            using (SolidBrush brushFondo = new SolidBrush(COLOR_TITULO_FONDO))
            {
                g.FillRectangle(brushFondo, rectFondo);
            }

            TextRenderer.DrawText(g, _tituloVisual, fontTitulo, rectFondo,
                COLOR_TITULO_TEXTO, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }

    public void SetData(List<NotificacionPP> notificaciones, string tituloBase)
    {
        // Aseguramos que siempre sea visible
        this.Visible = true;

        int conteo = notificaciones?.Count ?? 0;
        _tituloVisual = $"{tituloBase} ({conteo})".ToUpper();
        this.Text = "";

        panelItems.Controls.Clear();

        if (conteo > 0)
        {
            // Si hay datos, los ordenamos y cargamos
            var notificacionesOrdenadas = notificaciones
                .OrderByDescending(n => n.NivelUrgencia)
                .ThenByDescending(n => n.FechaNotificacion)
                .ToList();

            foreach (var item in notificacionesOrdenadas)
            {
                panelItems.Controls.Add(CrearItem(item));
            }

            // Opcional: Podrías forzar que se abra si llegan notificaciones nuevas
            // expanded = true; 
        }
        else
        {
            // CAMBIO CLAVE: Si no hay notificaciones, forzamos el estado contraído
            expanded = false;
        }

        this.Invalidate();

        // AplicarEstado se encarga de ajustar el alto del GroupBox y el texto del botón (▲/▼)
        AplicarEstado();
    }

    private Control CrearItem(NotificacionPP item)
    {
        var panelItem = new Panel();
        panelItem.Width = panelItems.ClientSize.Width - 5;
        panelItem.Margin = new Padding(0, 0, 0, 8);
        panelItem.BorderStyle = BorderStyle.FixedSingle;
        panelItem.Cursor = Cursors.Hand;

        // Color inicial según estado (Leída o Nueva)
        panelItem.BackColor = item.Leida ? COLOR_ITEM_FONDO_LEIDO : COLOR_ITEM_FONDO_NUEVO;

        panelItem.Paint += (s, e) =>
        {
            Color colorUrgencia;
            switch (item.NivelUrgencia)
            {
                case (int)NivelUrgencia.Alta: colorUrgencia = Color.Crimson; break;
                case (int)NivelUrgencia.Media: colorUrgencia = Color.DarkOrange; break;
                case (int)NivelUrgencia.Baja: colorUrgencia = Color.ForestGreen; break;
                default: colorUrgencia = Color.Gray; break;
            }
            using (SolidBrush brush = new SolidBrush(colorUrgencia))
            {
                e.Graphics.FillRectangle(brush, 0, 0, 6, panelItem.Height);
            }
        };

        var lblTitulo = new Label();
        lblTitulo.Text = item.Titulo;
        lblTitulo.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
        lblTitulo.Top = 8;
        lblTitulo.Left = 12;
        lblTitulo.ForeColor = COLOR_TEXTO_PRINCIPAL;
        lblTitulo.AutoSize = true;

        var lblDescripcion = new Label();
        lblDescripcion.Text = item.Descripcion;
        lblDescripcion.Font = new Font("Segoe UI", 8.5f);
        lblDescripcion.Left = 12;
        lblDescripcion.Top = lblTitulo.Bottom + 4;
        lblDescripcion.ForeColor = COLOR_TEXTO_SECUNDARIO;
        lblDescripcion.AutoSize = true;
        lblDescripcion.MaximumSize = new Size(panelItem.Width - 20, 0);

        MouseEventHandler unifiedClickHandler = (s, e) =>
        {
            if (e.Button == MouseButtons.Left)
            {
                OnItemClick(item);
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (!item.Leida)
                {
                    item.Leida = true;
                    panelItem.BackColor = COLOR_ITEM_FONDO_LEIDO;
                }
            }
        };

        panelItem.MouseClick += unifiedClickHandler;
        lblTitulo.MouseClick += unifiedClickHandler;
        lblDescripcion.MouseClick += unifiedClickHandler;

        panelItem.Controls.Add(lblTitulo);
        panelItem.Controls.Add(lblDescripcion);

        panelItem.Height = lblDescripcion.Bottom + 12;

        return panelItem;
    }

    private void OnItemClick(NotificacionPP item)
    {
        MessageBox.Show($"Notificación:\n{item.Titulo}");
    }

    private void BtnToggle_Click(object sender, EventArgs e)
    {
        expanded = !expanded;
        AplicarEstado();
    }

    private void AplicarEstado()
    {
        panelItems.Visible = expanded;
        if (expanded)
        {
            btnToggle.Text = "▲";
            this.Height = panelItems.Bottom + this.Padding.Bottom;
        }
        else
        {
            btnToggle.Text = "▼";
            this.Height = 50;
        }
    }
}