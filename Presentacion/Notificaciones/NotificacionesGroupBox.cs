using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class NotificationGroupBox : GroupBox
{
    private Button btnToggle;
    private DataGridView grid;

    private bool expanded = false;
    private int maxFilasVisibles = 6;

    public NotificationGroupBox()
    {
        InicializarComponentes();
    }

    private void InicializarComponentes()
    {
        this.Height = 50;

        btnToggle = new Button();
        btnToggle.Text = "▼";
        btnToggle.Width = 30;
        btnToggle.Height = 25;
        btnToggle.Top = 15;
        btnToggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnToggle.Click += BtnToggle_Click;

        grid = new DataGridView();
        grid.Top = 45;
        grid.Left = 10;
        grid.Width = this.Width - 20;
        grid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grid.Visible = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.RowHeadersVisible = false;
        grid.AllowUserToAddRows = false;

        this.Controls.Add(btnToggle);
        this.Controls.Add(grid);

        this.Resize += NotificationGroupBox_Resize;
    }

    private void NotificationGroupBox_Resize(object sender, EventArgs e)
    {
        btnToggle.Left = this.Width - 40;
        grid.Width = this.Width - 20;
    }

    private void BtnToggle_Click(object sender, EventArgs e)
    {
        expanded = !expanded;
        AplicarEstado();
    }

    public void SetData<T>(List<T> data, string tituloBase)
    {
        if (data == null || data.Count == 0)
        {
            this.Visible = false;
            return;
        }

        this.Visible = true;
        this.Text = $"{tituloBase} ({data.Count})";

        grid.DataSource = data;

        AjustarAltura(data.Count);
        AplicarEstado();
    }

    private void AjustarAltura(int cantidadFilas)
    {
        int alturaFila = grid.RowTemplate.Height;
        int filasMostradas = Math.Min(cantidadFilas, maxFilasVisibles);

        int alturaGrid = (filasMostradas * alturaFila) + 30;
        grid.Height = alturaGrid;
    }

    private void AplicarEstado()
    {
        grid.Visible = expanded;

        if (expanded)
        {
            btnToggle.Text = "▲";
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
// En tu Form:

// var notif = new NotificationGroupBox();
// notif.Width = flowLayoutPanel1.Width - 25;
// flowLayoutPanel1.Controls.Add(notif);
// notif.SetData(lotesVencidos, "Productos vencidos");
