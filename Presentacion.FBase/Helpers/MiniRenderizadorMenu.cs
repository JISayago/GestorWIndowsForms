using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.FBase.Helpers
{
    public class MiniRenderizadorMenu : ToolStripProfessionalRenderer
    {
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            // Si el mouse está arriba del ítem (Hover)
            if (e.Item.Selected)
            {
                using var brush = new SolidBrush(TemaSistema.Seleccion);
                e.Graphics.FillRectangle(brush, 0, 0, e.Item.Width, e.Item.Height);
                e.Item.ForeColor = TemaSistema.Oscuro;
            }
            else
            {
                // Fondo normal cuando el mouse no está encima
                using var brush = new SolidBrush(TemaSistema.Oscuro);
                e.Graphics.FillRectangle(brush, 0, 0, e.Item.Width, e.Item.Height);
                e.Item.ForeColor = TemaSistema.Acento;
            }
        }
    }
}
