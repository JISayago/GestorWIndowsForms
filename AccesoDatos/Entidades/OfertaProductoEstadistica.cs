using AccesoDatos.Entidades;

public class OfertaProductoEstadistica
{
    public long OfertaDescuentoId { get; set; }

    public long ProductoId { get; set; }

    public decimal CantidadVendida { get; set; }

    public decimal TotalCostoAcumulado { get; set; }

    public decimal TotalVentaAcumulado { get; set; }

    public decimal TotalOfertaAcumulado { get; set; }

    public DateTime? FechaUltimaVenta { get; set; }

    public OfertaDescuento OfertaDescuento { get; set; }

    public Producto Producto { get; set; }
}