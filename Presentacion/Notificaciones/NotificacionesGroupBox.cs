using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        this.Controls.Add(btnToggle);
        this.Controls.Add(panelItems);

        this.Resize += (s, e) =>
        {
            btnToggle.Left = this.Width - 40;
            panelItems.Width = this.Width - 20;

            // Recalcular ancho de items cuando cambia el tamaño
            foreach (Control ctrl in panelItems.Controls)
            {
                ctrl.Width = panelItems.Width - 5;
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

        foreach (var item in notificaciones)
        {
            panelItems.Controls.Add(CrearItem(item));
        }

        AplicarEstado();
    }

    private Control CrearItem(NotificacionPP item)
    {
        var panelItem = new Panel();
        panelItem.Width = panelItems.Width - 5;
        panelItem.Margin = new Padding(3);
        panelItem.BorderStyle = BorderStyle.FixedSingle;
        panelItem.Cursor = Cursors.Hand;

        panelItem.BackColor = item.Leida
            ? Color.FromArgb(240, 240, 240)
            : Color.FromArgb(255, 235, 235);

        // Título
        var lblTitulo = new Label();
        lblTitulo.Text = item.Titulo;
        lblTitulo.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        lblTitulo.Top = 5;
        lblTitulo.Left = 8;
        lblTitulo.AutoSize = true;

        // Fecha
        var lblFecha = new Label();
        lblFecha.Text = item.FechaNotificacion.ToString("dd/MM/yyyy");
        lblFecha.Font = new Font("Segoe UI", 7);
        lblFecha.Top = 5;
        lblFecha.Left = panelItem.Width - 80;
        lblFecha.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblFecha.ForeColor = Color.Gray;
        lblFecha.AutoSize = true;

        // Descripción (FIX DEFINITIVO)
        var lblDescripcion = new Label();
        lblDescripcion.Text = item.Descripcion;
        lblDescripcion.Font = new Font("Segoe UI", 8);
        lblDescripcion.Left = 8;
        lblDescripcion.Top = lblTitulo.Bottom + 3;
        lblDescripcion.ForeColor = Color.DimGray;

        int anchoDisponible = panelItems.Width - 25;

        lblDescripcion.AutoSize = true;
        lblDescripcion.MaximumSize = new Size(anchoDisponible, 0);

        // Click
        panelItem.Click += (s, e) => OnItemClick(item);
        lblTitulo.Click += (s, e) => OnItemClick(item);
        lblDescripcion.Click += (s, e) => OnItemClick(item);

        panelItem.Controls.Add(lblTitulo);
        panelItem.Controls.Add(lblDescripcion);
        panelItem.Controls.Add(lblFecha);

        // Altura dinámica DESPUÉS de agregar controles
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
            this.Height = panelItems.Bottom + 10;
        }
        else
        {
            btnToggle.Text = "▼";
            this.Height = 50;
        }
    }
}
