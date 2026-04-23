using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Notificaciones
{
    public class PanelConsultasRapidas : UserControl
    {
        // ===========================================================================
        // MÉTODO PRINCIPAL: Estructura de Tab 1 en 2 filas (Limpio)
        // ===========================================================================
        public void CargarConsultasRapidas(Control contenedorPadre)
        {
            // Usamos colores neutros de sistema
            contenedorPadre.BackColor = SystemColors.Control;

            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(10)
            };

            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            mainLayout.Controls.Add(CrearPanelVentas(), 0, 0);
            mainLayout.Controls.Add(CrearPanelProductos(), 0, 1);

            contenedorPadre.Controls.Add(mainLayout);
        }

        // ===========================================================================
        // SECCIÓN SUPERIOR: VENTAS
        // ===========================================================================
        private Panel CrearPanelVentas()
        {
            Panel p = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };

            Label lbl = new Label
            {
                Text = "Últimas Ventas",
                Dock = DockStyle.Top,
                Height = 25,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            DataGridView dgvVentas = ConfigurarGridSimple();
            dgvVentas.Columns.Add("Id", "Comprobante");
            dgvVentas.Columns.Add("Fecha", "Fecha/Hora");
            dgvVentas.Columns.Add("Cliente", "Cliente");
            dgvVentas.Columns.Add("Total", "Total $");

            p.Controls.Add(dgvVentas);
            p.Controls.Add(lbl);
            p.Controls.Add(CrearFooterSimple("Ventas"));

            return p;
        }

        // ===========================================================================
        // SECCIÓN INFERIOR: PRODUCTOS
        // ===========================================================================
        private Panel CrearPanelProductos()
        {
            Panel p = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };

            Panel pHeader = new Panel { Dock = DockStyle.Top, Height = 35 };

            Label lbl = new Label
            {
                Text = "Productos",
                Width = 100,
                Left = 0,
                Top = 5,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            TextBox txtBuscador = new TextBox
            {
                Width = 350,
                Left = 110,
                Top = 5,
                PlaceholderText = "Buscar producto..."
            };

            pHeader.Controls.Add(lbl);
            pHeader.Controls.Add(txtBuscador);

            DataGridView dgvProds = ConfigurarGridSimple();
            dgvProds.Columns.Add("Codigo", "Código");
            dgvProds.Columns.Add("Nombre", "Descripción");
            dgvProds.Columns.Add("Precio", "Precio $");
            dgvProds.Columns.Add("Stock", "Stock");

            p.Controls.Add(dgvProds);
            p.Controls.Add(pHeader);
            p.Controls.Add(CrearFooterSimple("Productos"));

            return p;
        }

        // ===========================================================================
        // GRID SIMPLE (Sin estilos forzados)
        // ===========================================================================
        private DataGridView ConfigurarGridSimple()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = SystemColors.Window,
                BorderStyle = BorderStyle.FixedSingle,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                ColumnHeadersHeight = 30
            };
        }

        // ===========================================================================
        // FOOTER SIMPLE
        // ===========================================================================
        private Panel CrearFooterSimple(string contexto)
        {
            Panel pNav = new Panel { Dock = DockStyle.Bottom, Height = 45, Padding = new Padding(0, 5, 0, 0) };

            Button btnPrev = new Button { Text = "Anterior", Width = 80, Dock = DockStyle.Left };
            Button btnNext = new Button { Text = "Siguiente", Width = 80, Dock = DockStyle.Left };

            Button btnVerMas = new Button
            {
                Text = $"Ver más {contexto}",
                Width = 150,
                Dock = DockStyle.Right,
                Cursor = Cursors.Hand
            };

            pNav.Controls.Add(btnNext);
            pNav.Controls.Add(btnPrev);
            pNav.Controls.Add(btnVerMas);

            return pNav;
        }
    }
}
