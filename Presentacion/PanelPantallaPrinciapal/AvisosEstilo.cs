using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
using System.Drawing;

namespace Presentacion.Notificaciones
{
    /// <summary>
    /// Colores y nombres de cada tipo de aviso. El mismo color identifica al tipo en la pestaña
    /// y en los puntos del calendario. Son distintos del rojo/naranja/verde de la urgencia de cada aviso.
    /// </summary>
    public static class AvisosEstilo
    {
        public static readonly Color ColorLote = ColorTranslator.FromHtml("#8E5BE8");
        public static readonly Color ColorOferta = ColorTranslator.FromHtml("#2F80D1");
        public static readonly Color ColorOfertaStock = ColorTranslator.FromHtml("#14A398");
        public static readonly Color ColorCuentaCorriente = ColorTranslator.FromHtml("#E0A020");

        /// <summary>Orden de las pestañas.</summary>
        public static readonly TipoNotificacion[] Tipos =
        {
            TipoNotificacion.LoteVencido,
            TipoNotificacion.OfertaVencida,
            TipoNotificacion.OfertaBajoStock,
            TipoNotificacion.CuentaCorrienteVencida
        };

        public static Color Color(TipoNotificacion tipo) => tipo switch
        {
            TipoNotificacion.LoteVencido => ColorLote,
            TipoNotificacion.OfertaVencida => ColorOferta,
            TipoNotificacion.OfertaBajoStock => ColorOfertaStock,
            _ => ColorCuentaCorriente
        };

        public static Color Color(TipoVencimientoCalendario tipo) => tipo switch
        {
            TipoVencimientoCalendario.Lote => ColorLote,
            TipoVencimientoCalendario.Oferta => ColorOferta,
            _ => ColorCuentaCorriente
        };

        public static string NombreCorto(TipoNotificacion tipo) => tipo switch
        {
            TipoNotificacion.LoteVencido => "Lotes",
            TipoNotificacion.OfertaVencida => "Ofertas venc.",
            TipoNotificacion.OfertaBajoStock => "Oferta stock",
            _ => "Ctas. ctes."
        };

        public static string NombreCompleto(TipoNotificacion tipo) => tipo switch
        {
            TipoNotificacion.LoteVencido => "Lotes vencidos",
            TipoNotificacion.OfertaVencida => "Ofertas vencidas",
            TipoNotificacion.OfertaBajoStock => "Ofertas con bajo stock",
            _ => "Cuentas corrientes vencidas"
        };
    }
}
