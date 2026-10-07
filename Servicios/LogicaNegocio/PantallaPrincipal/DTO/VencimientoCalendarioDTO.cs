using System;

namespace Servicios.LogicaNegocio.PantallaPrincipal.DTO
{
    /// <summary>Tipo de aviso con fecha de vencimiento que el calendario de la pantalla principal puede mostrar.</summary>
    public enum TipoVencimientoCalendario
    {
        Lote,
        Oferta,
        CuentaCorriente
    }

    /// <summary>Un aviso pendiente (no leído) con fecha de vencimiento, para marcar el día en el calendario.</summary>
    public class VencimientoCalendarioDTO
    {
        public DateTime Fecha { get; set; }
        public TipoVencimientoCalendario Tipo { get; set; }
    }

    /// <summary>Un aviso pendiente con su tipo, para listar los vencimientos de un día del calendario.</summary>
    public class AvisoDelDiaDTO
    {
        public TipoVencimientoCalendario Tipo { get; set; }
        public NotificacionDTO Aviso { get; set; }
    }
}
