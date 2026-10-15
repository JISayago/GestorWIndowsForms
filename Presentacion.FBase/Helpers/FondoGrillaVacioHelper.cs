using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

//namespace Presentacion.FBase.Helpers
//{
//    /// <summary>
//    /// Pinta el "fondo vacío" de un DataGridView: el área libre debajo de las filas (o toda la
//    /// grilla si no tiene datos) con un color suave, una banda superior de separación y el
//    /// logo del sistema como marca de agua. Es el mismo aspecto que tienen las consultas
//    /// (FBaseConsulta), centralizado para poder aplicarlo a cualquier otra grilla.
//    /// </summary>
//    public static class FondoGrillaVacioHelper
//    {
//        private static readonly Color ColorFondoVacio = Color.FromArgb(236, 230, 245);

//        /// <summary>
//        /// Habilita el fondo vacío en la grilla. Es seguro llamarlo más de una vez sobre la
//        /// misma grilla: no duplica la suscripción al evento Paint.
//        /// </summary>
//        public static void Aplicar(DataGridView dgv)
//        {
//            if (dgv == null)
//                return;

//            dgv.Paint -= Dgv_Paint;
//            dgv.Paint += Dgv_Paint;
//        }

//        private static void Dgv_Paint(object sender, PaintEventArgs e)
//        {
//            var dgv = sender as DataGridView;
//            if (dgv == null || dgv.IsDisposed)
//                return;

//            int top = dgv.ColumnHeadersVisible ? dgv.ColumnHeadersHeight : 0;
//            if (dgv.Rows.Count > 0)
//            {
//                try
//                {
//                    var last = dgv.GetRowDisplayRectangle(dgv.Rows.Count - 1, true);
//                    if (last.Height > 0)
//                        top = Math.Max(top, last.Bottom);
//                }
//                catch
//                {
//                    // Ignorar si la fila aún no está medida.
//                }
//            }

//            if (top >= dgv.ClientSize.Height - 8)
//                return;

//            var empty = Rectangle.FromLTRB(0, top, dgv.ClientSize.Width, dgv.ClientSize.Height);

//            using (var fill = new SolidBrush(ColorFondoVacio))
//                e.Graphics.FillRectangle(fill, empty);

//            // Banda superior suave para separar filas del vacío.
//            using (var accent = new SolidBrush(Color.FromArgb(55, TemaSistema.Seleccion)))
//                e.Graphics.FillRectangle(accent, empty.Left, empty.Top, empty.Width, Math.Min(6, empty.Height));

//            var logo = Constantes.Imagenes.ImgLogoCompuesto;
//            if (logo == null || empty.Height < 60 || empty.Width < 80)
//                return;

//            int maxW = Math.Min(300, empty.Width * 2 / 5);
//            int maxH = Math.Min(170, empty.Height - 24);
//            if (maxW < 48 || maxH < 48)
//                return;

//            float scale = Math.Min((float)maxW / logo.Width, (float)maxH / logo.Height);
//            int w = Math.Max(1, (int)(logo.Width * scale));
//            int h = Math.Max(1, (int)(logo.Height * scale));
//            int x = empty.Left + (empty.Width - w) / 2;
//            int y = empty.Top + (empty.Height - h) / 2;

//            var matrix = new ColorMatrix { Matrix33 = 0.11f };
//            using var attrs = new ImageAttributes();
//            attrs.SetColorMatrix(matrix);
//            e.Graphics.DrawImage(
//                logo,
//                new Rectangle(x, y, w, h),
//                0, 0, logo.Width, logo.Height,
//                GraphicsUnit.Pixel,
//                attrs);
//        }
//    }
//}
namespace Presentacion.FBase.Helpers
{
    /// <summary>
    /// Pinta el "fondo vacío" de un DataGridView: el área libre debajo de las filas (o toda la
    /// grilla si no tiene datos) con un color suave, una banda superior de separación y el
    /// logo del sistema como marca de agua. Es el mismo aspecto que tienen las consultas
    /// (FBaseConsulta), centralizado para poder aplicarlo a cualquier otra grilla.
    /// </summary>
    public static class FondoGrillaVacioHelper
    {
        private static readonly Color ColorFondoVacio = Color.FromArgb(236, 230, 245);

        /// <summary>
        /// Habilita el fondo vacío en la grilla. Es seguro llamarlo más de una vez sobre la
        /// misma grilla: no duplica la suscripción al evento Paint.
        /// </summary>
        public static void Aplicar(DataGridView dgv)
        {
            if (dgv == null)
                return;

            dgv.Paint -= Dgv_Paint;
            dgv.Paint += Dgv_Paint;
        }

        private static void Dgv_Paint(object sender, PaintEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv == null || dgv.IsDisposed)
                return;

            int top = dgv.ColumnHeadersVisible ? dgv.ColumnHeadersHeight : 0;

            // Última fila VISIBLE (ignora filas ocultas, a diferencia de Rows.Count - 1).
            int ultimaFila = dgv.Rows.GetLastRow(DataGridViewElementStates.Visible);

            if (ultimaFila >= 0)
            {
                Rectangle rect;
                try
                {
                    rect = dgv.GetRowDisplayRectangle(ultimaFila, true);
                }
                catch
                {
                    // La fila aún no está medida: se repinta después, no dibujamos nada ahora.
                    return;
                }

                // CLAVE: si la última fila no está en pantalla (hay más filas que alto de
                // grilla, o está scrolleada) el rectángulo viene con alto 0. Antes en ese caso
                // se asumía "no hay filas" y se pintaba el fondo sobre TODAS las filas
                // visibles. Si la última fila no se ve, no hay área vacía: no se pinta nada.
                if (rect.Height <= 0)
                    return;

                top = Math.Max(top, rect.Bottom);
            }

            if (top >= dgv.ClientSize.Height - 8)
                return;

            var empty = Rectangle.FromLTRB(0, top, dgv.ClientSize.Width, dgv.ClientSize.Height);

            using (var fill = new SolidBrush(ColorFondoVacio))
                e.Graphics.FillRectangle(fill, empty);

            // Banda superior suave para separar filas del vacío.
            using (var accent = new SolidBrush(Color.FromArgb(55, TemaSistema.Seleccion)))
                e.Graphics.FillRectangle(accent, empty.Left, empty.Top, empty.Width, Math.Min(6, empty.Height));

            var logo = Constantes.Imagenes.ImgLogoCompuesto;
            if (logo == null || empty.Height < 60 || empty.Width < 80)
                return;

            // El logo se adapta al área vacía disponible (no a la grilla entera), dejando margen.
            int maxW = Math.Min(300, empty.Width * 2 / 5);
            int maxH = Math.Min(170, empty.Height - 24);
            if (maxW < 48 || maxH < 48)
                return;

            float scale = Math.Min((float)maxW / logo.Width, (float)maxH / logo.Height);
            int w = Math.Max(1, (int)(logo.Width * scale));
            int h = Math.Max(1, (int)(logo.Height * scale));
            int x = empty.Left + (empty.Width - w) / 2;
            int y = empty.Top + (empty.Height - h) / 2;

            var matrix = new ColorMatrix { Matrix33 = 0.11f };
            using var attrs = new ImageAttributes();
            attrs.SetColorMatrix(matrix);
            e.Graphics.DrawImage(
                logo,
                new Rectangle(x, y, w, h),
                0, 0, logo.Width, logo.Height,
                GraphicsUnit.Pixel,
                attrs);
        }
    }
}
