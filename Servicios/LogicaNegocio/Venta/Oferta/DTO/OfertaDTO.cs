using Servicios.LogicaNegocio.Venta.Oferta.DTO;

public class OfertaDTO
{
    public long OfertaDescuentoId { get; set; }

    public string Descripcion { get; set; }

    public string Codigo { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public bool EstaActiva { get; set; }

    public string DescripcionEstado =>
        EstaActiva
            ? "Activa"
            : "Inactiva";

    public int TipoOferta { get; set; }

    public string DescripcionTipoOferta =>
        TipoOferta switch
        {
            (int)Servicios.Helpers.Venta.Oferta.TipoOferta.Producto => "Producto",
            (int)Servicios.Helpers.Venta.Oferta.TipoOferta.Combo => "Combo",
            (int)Servicios.Helpers.Venta.Oferta.TipoOferta.DosPorUno => "2x1",
            (int)Servicios.Helpers.Venta.Oferta.TipoOferta.Grupo => "Grupo",
            _ => "Desconocido"
        };

    public decimal? PorcentajeDescuento { get; set; }

    public decimal? PrecioFinal { get; set; }

    public ICollection<ProductosEnOfertaDescuentosDTO> Productos { get; set; }
        = new List<ProductosEnOfertaDescuentosDTO>();
}