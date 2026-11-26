using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.FBase.Helpers
{
    /// <summary>
    /// Estilo de los menús desplegables (ContextMenuStrip) del sistema: mismo look que la barra de menú
    /// (fondo oscuro, texto crema, hover lila), sin la columna lateral de íconos de Windows.
    /// Uso: <c>EstiloMenuContextual.Aplicar(menu);</c> después de cargar los ítems.
    /// </summary>
    public static class EstiloMenuContextual
    {
        public static void Aplicar(ContextMenuStrip menu)
        {
            menu.Renderer = new RenderizadorMenuContextual();
            menu.ShowImageMargin = false;
            menu.BackColor = TemaSistema.Oscuro;
            menu.ForeColor = TemaSistema.Acento;
            menu.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
            menu.Padding = new Padding(0, 4, 0, 4);

            foreach (ToolStripItem item in menu.Items)
            {
                if (item is ToolStripMenuItem)
                {
                    item.Padding = new Padding(14, 8, 14, 8);
                    item.ForeColor = TemaSistema.Acento;
                }
            }
        }
    }

    public class RenderizadorMenuContextual : ToolStripProfessionalRenderer
    {
        public RenderizadorMenuContextual()
        {
            RoundedEdges = false;
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            using var pincel = new SolidBrush(TemaSistema.Oscuro);
            e.Graphics.FillRectangle(pincel, e.AffectedBounds);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            using var lapiz = new Pen(TemaSistema.Primario);
            var borde = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
            e.Graphics.DrawRectangle(lapiz, borde);
        }

        // Sin la columna lateral gris/degradada que dibuja Windows para los íconos.
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            var color = e.Item.Selected && e.Item.Enabled ? TemaSistema.Seleccion : TemaSistema.Oscuro;

            using var pincel = new SolidBrush(color);
            // Se deja 1px de borde para no pisar la línea del contorno del menú.
            e.Graphics.FillRectangle(pincel, 1, 0, e.Item.Width - 2, e.Item.Height);
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = !e.Item.Enabled ? TemaSistema.TextoSecundario
                : e.Item.Selected ? TemaSistema.Oscuro
                : TemaSistema.Acento;

            base.OnRenderItemText(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            int y = e.Item.Height / 2;

            using var lapiz = new Pen(Color.FromArgb(90, TemaSistema.Acento));
            e.Graphics.DrawLine(lapiz, 12, y, e.Item.Width - 12, y);
        }
    }
}
