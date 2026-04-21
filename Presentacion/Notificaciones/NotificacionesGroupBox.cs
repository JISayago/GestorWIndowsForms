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

    public NotificationGroupBox()
    {
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
        btnToggle.Top = 15;
        btnToggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnToggle.FlatStyle = FlatStyle.Flat;
        btnToggle.Click += BtnToggle_Click;

        panelItems = new FlowLayoutPanel();
        panelItems.Top = 45;
        panelItems.Left = 10;
        panelItems.Width = this.Width - 20;
        panelItems.AutoSize = true;
        panelItems.FlowDirection = FlowDirection.TopDown;
        panelItems.WrapContents = false;

        // FIX: Eliminamos el marco gris oscuro
        panelItems.BorderStyle = BorderStyle.None;

        panelItems.Padding = new Padding(0);

        this.Controls.Add(btnToggle);
        this.Controls.Add(panelItems);

        this.Resize += (s, e) =>
        {
            btnToggle.Left = this.Width - 40;
            panelItems.Width = this.Width - 20;

            foreach (Control ctrl in panelItems.Controls)
            {
                ctrl.Width = panelItems.Width - 10;
            }
        };
    }

    private void BtnToggle_Click(object sender, EventArgs e)
    {
        expanded = !expanded;
        AplicarEstado();
    }

    public void SetData(List<NotificacionPP> notificaciones, string tituloBase)
    {
        if (notificaciones == null || notificaciones.Count == 0)
        {
            this.Visible = false;
            return;
        }

        this.Visible = true;
        this.Text = $"{tituloBase} ({notificaciones.Count})";

        panelItems.Controls.Clear();

        var notificacionesOrdenadas = notificaciones
            .OrderByDescending(n => n.NivelUrgencia)
            .ThenByDescending(n => n.FechaNotificacion)
            .ToList();

        foreach (var item in notificacionesOrdenadas)
        {
            panelItems.Controls.Add(CrearItem(item));
        }

        AplicarEstado();
    }

    private Control CrearItem(NotificacionPP item)
    {
        var panelItem = new Panel();
        panelItem.Width = panelItems.Width - 10;
        panelItem.Margin = new Padding(0, 0, 0, 8);
        panelItem.BorderStyle = BorderStyle.FixedSingle;
        panelItem.Cursor = Cursors.Hand;

        panelItem.BackColor = item.Leida
            ? Color.FromArgb(240, 240, 240)
            : Color.FromArgb(255, 235, 235);

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

        var lblFecha = new Label();
        lblFecha.Text = item.FechaNotificacion.ToString("dd/MM/yyyy");
        lblFecha.Font = new Font("Segoe UI", 7);
        lblFecha.AutoSize = true;
        lblFecha.ForeColor = Color.Gray;
        lblFecha.Top = 5;
        lblFecha.Left = panelItem.Width - 75;
        lblFecha.Anchor = AnchorStyles.Top | AnchorStyles.Right;

        var lblTitulo = new Label();
        lblTitulo.Text = item.Titulo;
        lblTitulo.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        lblTitulo.Top = 5;
        lblTitulo.Left = 14;
        lblTitulo.AutoSize = true;

        var lblDescripcion = new Label();
        lblDescripcion.Text = item.Descripcion;
        lblDescripcion.Font = new Font("Segoe UI", 8);
        lblDescripcion.Left = 14;
        lblDescripcion.Top = lblTitulo.Bottom + 3;
        lblDescripcion.ForeColor = Color.DimGray;
        lblDescripcion.AutoSize = true;
        lblDescripcion.MaximumSize = new Size(panelItem.Width - 25, 0);

        Action<object, EventArgs> clickHandler = (s, e) => OnItemClick(item);
        panelItem.Click += new EventHandler(clickHandler);
        lblTitulo.Click += new EventHandler(clickHandler);
        lblDescripcion.Click += new EventHandler(clickHandler);
        lblFecha.Click += new EventHandler(clickHandler);

        panelItem.Controls.Add(lblFecha);
        panelItem.Controls.Add(lblTitulo);
        panelItem.Controls.Add(lblDescripcion);

        panelItem.Height = lblDescripcion.Bottom + 10;

        return panelItem;
    }

    private void OnItemClick(NotificacionPP item)
    {
        MessageBox.Show($"Notificación:\n{item.Titulo}");
    }

    private void AplicarEstado()
    {
        panelItems.Visible = expanded;

        if (expanded)
        {
            btnToggle.Text = "▲";
            this.Height = panelItems.Bottom + 15;
        }
        else
        {
            btnToggle.Text = "▼";
            this.Height = 50;
        }
    }
}