using Presentacion.FBase.Helpers;
using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
using System.Globalization;

namespace Presentacion.Notificaciones
{
    /// <summary>
    /// Ventanita que se despliega al hacer click en un día del calendario y lista los avisos
    /// pendientes que vencen ese día. Mismos clicks que la lista: izquierdo abre la consulta,
    /// derecho marca como leído. Se cierra al hacer click fuera.
    /// </summary>
    public class DiaAvisosPopup : ToolStripDropDown
    {
        private const int AnchoLogico = 440;
        private const int AltoListaMaximoLogico = 340;
        private const int AltoEncabezadoLogico = 30;
        private const int MargenLogico = 8;

        private static readonly CultureInfo Cultura = new CultureInfo("es-AR");

        public event EventHandler<AvisoDelDiaDTO>? AbrirAviso;
        public event EventHandler<AvisoDelDiaDTO>? MarcarLeido;

        public DiaAvisosPopup(DateTime fecha, IReadOnlyList<AvisoDelDiaDTO> avisos, ToolTip toolTip)
        {
            AutoClose = true;
            DropShadowEnabled = false;
            Padding = Padding.Empty;

            using var medidor = new Panel();
            int dpi(int v) => medidor.LogicalToDeviceUnits(v);

            int ancho = dpi(AnchoLogico);
            int margen = dpi(MargenLogico);
            int altoEncabezado = dpi(AltoEncabezadoLogico);

            var lista = new ListaAvisosPanel { Dock = DockStyle.Fill };
            string ayuda = "Click izquierdo: abrir la consulta\nClick derecho: marcar como leído";
            var items = new List<AvisoItem>();
            foreach (var dto in avisos)
            {
                var item = new AvisoItem(dto.Aviso, ayuda, toolTip);
                var captura = dto;
                item.AvisoClick += (s, e) =>
                {
                    Close();
                    AbrirAviso?.Invoke(this, captura);
                };
                item.MarcarLeidoSolicitado += (s, e) =>
                {
                    Close();
                    MarcarLeido?.Invoke(this, captura);
                };
                item.MouseEntro += (s, e) => lista.Focus();
                items.Add(item);
            }
            lista.SetItems(items);

            int anchoLista = ancho - margen * 2 - 2;
            int altoContenido = lista.AlturaContenido(anchoLista);
            int altoLista = Math.Min(altoContenido, dpi(AltoListaMaximoLogico));

            var encabezado = new Label
            {
                Dock = DockStyle.Top,
                Height = altoEncabezado,
                BackColor = TemaSistema.Oscuro,
                ForeColor = TemaSistema.Acento,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(margen, 0, 0, 0),
                Text = $"{TextoFecha(fecha)}  ·  {avisos.Count} {(avisos.Count == 1 ? "aviso" : "avisos")}"
            };

            var cuerpo = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(margen),
                BackColor = TemaSistema.Fondo
            };
            cuerpo.Controls.Add(lista);

            var raiz = new Panel
            {
                Size = new Size(ancho, altoEncabezado + altoLista + margen * 2 + 2),
                BackColor = TemaSistema.Fondo
            };
            raiz.Controls.Add(cuerpo);
            raiz.Controls.Add(encabezado);
            raiz.Paint += (s, e) =>
            {
                using var pen = new Pen(TemaSistema.Borde);
                e.Graphics.DrawRectangle(pen, 0, 0, raiz.Width - 1, raiz.Height - 1);
            };

            var host = new ToolStripControlHost(raiz)
            {
                Margin = Padding.Empty,
                Padding = Padding.Empty,
                AutoSize = false,
                Size = raiz.Size
            };
            Items.Add(host);
        }

        private static string TextoFecha(DateTime fecha)
        {
            string texto = fecha.ToString("dddd d 'de' MMMM", Cultura);
            return char.ToUpper(texto[0], Cultura) + texto.Substring(1);
        }
    }
}
