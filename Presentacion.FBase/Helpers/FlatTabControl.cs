using Presentacion.FBase.Helpers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.Administracion
{
    /// <summary>
    /// TabControl plano sin bordes.
    /// Reemplazar el TabControl del diseñador por este control.
    /// </summary>
    public class FlatTabControl : TabControl
    {
        /// <summary>
        /// Permite cambiar el color del encabezado desde afuera.
        /// </summary>
        public Color HeaderBackColor { get; set; } = TemaSistema.FondoControl;

        public FlatTabControl()
        {
            // =====================================================
            // CONFIGURACIÓN VISUAL FIJA DEL CONTROL
            // =====================================================

            DrawMode = TabDrawMode.OwnerDrawFixed;
            Appearance = TabAppearance.FlatButtons;
            SizeMode = TabSizeMode.Fixed;

            DrawItem += FlatTabControl_DrawItem;
        }

        /// <summary>
        /// Expande la página interna para tapar el borde gris
        /// nativo del TabControl.
        /// </summary>
        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rect = base.DisplayRectangle;

                return new Rectangle(
                    rect.Left - 4,
                    rect.Top - 4,
                    rect.Width + 8,
                    rect.Height + 8);
            }
        }

        /// <summary>
        /// Dibuja cada pestaña.
        /// </summary>
        private void FlatTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            var tabPage = TabPages[e.Index];

            bool isSelected =
                (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            // =====================================================
            // FONDO DE LA PESTAÑA
            // =====================================================

            using var brush = new SolidBrush(
                isSelected
                    ? TemaSistema.Seleccion
                    : TemaSistema.Fondo);

            e.Graphics.FillRectangle(brush, e.Bounds);

            // =====================================================
            // TEXTO
            // =====================================================

            Color colorTexto =
                isSelected
                    ? Color.Black
                    : TemaSistema.Texto;

            TextRenderer.DrawText(
                e.Graphics,
                tabPage.Text,
                Font,
                e.Bounds,
                colorTexto,
                TextFormatFlags.HorizontalCenter |
                TextFormatFlags.VerticalCenter);
        }

        /// <summary>
        /// Pinta el espacio vacío a la derecha de las pestañas.
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // WM_PAINT
            if (m.Msg == 0x0F && TabCount > 0)
            {
                using Graphics g = Graphics.FromHwnd(Handle);

                Rectangle lastTabRect =
                    GetTabRect(TabCount - 1);

                Rectangle espacioVacio = new Rectangle(
                    lastTabRect.Right,
                    0,
                    Width - lastTabRect.Right,
                    ItemSize.Height + 4);

                using Brush brush =
                    new SolidBrush(HeaderBackColor);

                g.FillRectangle(brush, espacioVacio);
            }
        }

        /// <summary>
        /// Permite refrescar colores del tema.
        /// </summary>
        public void AplicarTema()
        {
            BackColor = TemaSistema.Fondo;
            ForeColor = TemaSistema.Texto;

            HeaderBackColor = TemaSistema.FondoControl;

            foreach (TabPage page in TabPages)
            {
                page.BackColor = TemaSistema.Fondo;
                page.ForeColor = TemaSistema.Texto;
            }

            Invalidate();
        }
    }
}