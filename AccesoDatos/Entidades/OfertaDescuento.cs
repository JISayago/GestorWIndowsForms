using AccesoDatos.Entidades;
using System.ComponentModel.DataAnnotations;

public class OfertaDescuento
{
    [Key]
    public long OfertaDescuentoId { get; set; }

    public string Descripcion { get; set; }
    public string Codigo { get; set; }

    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }

    public bool EstaActiva { get; set; }

    public int TipoOferta { get; set; }

    public decimal? PorcentajeDescuento { get; set; }
    public decimal? PrecioFinal { get; set; }

    public ICollection<ProductosEnOfertaDescuentos> Productos { get; set; }
        = new List<ProductosEnOfertaDescuentos>();

    public ICollection<DetallesVenta> DetallesVentas { get; set; }
        = new List<DetallesVenta>();

    public ICollection<OfertaProductoEstadistica> Estadisticas { get; set; }
        = new List<OfertaProductoEstadistica>();
}