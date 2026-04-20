using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class NotificationGroupBox : GroupBox
{
    private Button btnToggle;

    // Modos de contenido
    private DataGridView grid;
    private FlowLayoutPanel panelItems;

    private bool expanded = true; // abierto por default
    private int maxFilasVisibles = 6;

    public bool UseTextMode { get; set; } = true; // true = lista texto, false = grid

    public NotificationGroupBox()
    {
        InicializarComponentes();
    }

    private void InicializarComponentes()
    {
        this.Height = 50;

        btnToggle = new Button();
        btnToggle.Text = "▲";
        btnToggle.Width = 30;
        btnToggle.Height = 25;
        btnToggle.Top = 15;
        btnToggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnToggle.Click += BtnToggle_Click;

        // GRID (modo tabla)
        grid = new DataGridView();
        grid.Top = 45;
        grid.Left = 10;
        grid.Width = this.Width - 20;
        grid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grid.Visible = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.RowHeadersVisible = false;
        grid.AllowUserToAddRows = false;

        // PANEL TEXTO (modo notificación)
        panelItems = new FlowLayoutPanel();
        panelItems.Top = 45;
        panelItems.Left = 10;
        panelItems.Width = this.Width - 20;
        panelItems.AutoSize = true;
        panelItems.FlowDirection = FlowDirection.TopDown;
        panelItems.WrapContents = false;
        panelItems.Visible = false;

        this.Controls.Add(btnToggle);
        this.Controls.Add(grid);
        this.Controls.Add(panelItems);

        this.Resize += NotificationGroupBox_Resize;
    }

    private void NotificationGroupBox_Resize(object sender, EventArgs e)
    {
        btnToggle.Left = this.Width - 40;
        grid.Width = this.Width - 20;
        panelItems.Width = this.Width - 20;
    }

    private void BtnToggle_Click(object sender, EventArgs e)
    {
        expanded = !expanded;
        AplicarEstado();
    }

    // ================= GRID =================
    public void SetDataGrid<T>(List<T> data, string tituloBase)
    {
        if (data == null || data.Count == 0)
        {
            this.Visible = false;
            return;
        }

        UseTextMode = false;

        this.Visible = true;
        this.Text = $"{tituloBase} ({data.Count})";

        grid.DataSource = data;

        AjustarAlturaGrid(data.Count);
        AplicarEstado();
    }

    private void AjustarAlturaGrid(int cantidadFilas)
    {
        int alturaFila = grid.RowTemplate.Height;
        int filasMostradas = Math.Min(cantidadFilas, maxFilasVisibles);

        int alturaGrid = (filasMostradas * alturaFila) + 30;
        grid.Height = alturaGrid;
    }

    // ================= TEXTO =================
    public void SetDataTexto(List<string> items, string tituloBase)
    {
        if (items == null || items.Count == 0)
        {
            this.Visible = false;
            return;
        }

        UseTextMode = true;

        this.Visible = true;
        this.Text = $"{tituloBase} ({items.Count})";

        panelItems.Controls.Clear();

        foreach (var item in items)
        {
            var label = new Label();
            label.Text = "• " + item;
            label.AutoSize = true;
            label.Margin = new Padding(3);

            panelItems.Controls.Add(label);
        }

        AplicarEstado();
    }

    // ================= ESTADO =================
    private void AplicarEstado()
    {
        grid.Visible = !UseTextMode && expanded;
        panelItems.Visible = UseTextMode && expanded;

        if (expanded)
        {
            btnToggle.Text = "▲";

            if (UseTextMode)
                this.Height = panelItems.Bottom + 10;
            else
                this.Height = grid.Top + grid.Height + 10;
        }
        else
        {
            btnToggle.Text = "▼";
            this.Height = 50;
        }
    }
}


// ================= USO =================
// TEXTO (recomendado para notificaciones)
// notif.SetDataTexto(listaStrings, "Productos vencidos");

// GRID (si necesitás tabla)
// notif.SetDataGrid(listaObjetos, "Productos vencidos");
