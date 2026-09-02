using AccesoDatos.Entidades;
using Servicios.Infraestructura;

namespace Servicios.Tests;

internal sealed class PdfGeneratorNoOp : IPdfGenerator
{
    public string GenerarVenta(Venta venta) => "noop-venta";
    public string GenerarVentaLibre(VentaLibre venta) => "noop-venta-libre";
    public string GenerarCancelacionVenta(Venta venta) => "noop-cancelacion";
    public string GenerarGasto(Gasto gasto) => "noop-gasto";
    public string GenerarGastoAnulado(Gasto gasto) => "noop-gasto-anulado";
}
