using Presentacion.FBase.Helpers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.Administracion
{
    public class FlatTabControl : TabControl
    {
        // Esta propiedad te permite elegir el color de fondo de la barra superior desde afuera
        public Color HeaderBackColor { get; set; } = TemaSistema.Fondo;

        // TRUCO 1: Expande la página interna para "comerse" y tapar el borde gris perimetral nativo de Windows
        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rect = base.DisplayRectangle;
                return new Rectangle(rect.Left - 4, rect.Top - 4, rect.Width + 8, rect.Height + 8);
            }
        }

        // TRUCO 2: Intercepta el dibujado de Windows para pintar el espacio vacío a la derecha de los botones
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // 0x0F es el mensaje WM_PAINT (cuando Windows ya terminó de renderizar el control)
            if (m.Msg == 0x0F && this.TabCount > 0)
            {
                using (Graphics g = Graphics.FromHwnd(this.Handle))
                {
                    // 1. Conseguimos el rectángulo del último botón de pestaña
                    Rectangle lastTabRect = this.GetTabRect(this.TabCount - 1);

                    // 2. Definimos el área vacía desde el final de ese botón hasta el borde derecho del control
                    Rectangle espacioVacio = new Rectangle(
                        lastTabRect.Right,
                        0,
                        this.Width - lastTabRect.Right,
                        this.ItemSize.Height + 4 // Margen pequeño para cubrir bien el alto de la barra
                    );

                    // 3. Pintamos ese espacio con el color personalizado de tu fondo
                    using (Brush brush = new SolidBrush(HeaderBackColor))
                    {
                        g.FillRectangle(brush, espacioVacio);
                    }
                }
            }
        }
    }
}