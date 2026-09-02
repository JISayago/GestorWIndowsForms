using Servicios.Helpers.OpcionesPagos;
using Servicios.Helpers.VentaEnum;
using Servicios.LogicaNegocio.Venta;

namespace Servicios.Tests;

[Collection("VentaCtaCteE2E")]
public class VentaCtaCteE2ETests
{
    private readonly VentaCtaCteE2EFixture _fx;

    public VentaCtaCteE2ETests(VentaCtaCteE2EFixture fx)
    {
        _fx = fx;
    }

    [Fact]
    public void Venta_100pct_Efectivo_NoTocaCtaCte()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaServicio(new PdfGeneratorNoOp());
        var dto = _fx.CrearVentaDto(1, (TipoDePago.Efectivo, 1000));

        var resultado = servicio.NuevaVenta(dto);

        Assert.True(resultado.Exitoso, resultado.Mensaje);
        Assert.Equal(0, _fx.LeerSaldoCtaCte());
        Assert.Equal(99, _fx.LeerStockProducto());
        Assert.Equal(1000, _fx.LeerSaldoCaja());
    }

    [Fact]
    public void Venta_Mixta_SeparaCajaYCtaCte()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaServicio(new PdfGeneratorNoOp());
        var dto = _fx.CrearVentaDto(
            1,
            (TipoDePago.Efectivo, 600),
            (TipoDePago.CtaCte, 400));

        var resultado = servicio.NuevaVenta(dto);

        Assert.True(resultado.Exitoso, resultado.Mensaje);
        Assert.Equal(-400, _fx.LeerSaldoCtaCte()); // deuda
        Assert.Equal(600, _fx.LeerSaldoCaja());
        Assert.Equal(99, _fx.LeerStockProducto());
    }

    [Fact]
    public void Cancelar_VentaConCtaCte_DevuelveCreditoCtaCte()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaServicio(new PdfGeneratorNoOp());
        var dto = _fx.CrearVentaDto(
            1,
            (TipoDePago.Efectivo, 600),
            (TipoDePago.CtaCte, 400));

        var alta = servicio.NuevaVenta(dto);
        Assert.True(alta.Exitoso, alta.Mensaje);
        Assert.Equal(-400, _fx.LeerSaldoCtaCte());

        var ventaId = _fx.UltimaVentaConfirmadaId();
        Assert.NotNull(ventaId);

        var baja = servicio.CancelacionVentaPorId(ventaId.Value);
        Assert.True(baja.Exitoso, baja.Mensaje);

        Assert.Equal(0, _fx.LeerSaldoCtaCte());
        Assert.Equal(100, _fx.LeerStockProducto());
    }

    [Fact]
    public void StockInsuficiente_HaceRollback_IncluyendoCtaCte()
    {
        _fx.RecrearBaseYSeed();
        _fx.ResetSaldosYStock(stock: 0, saldoCta: 0);

        var servicio = new VentaServicio(new PdfGeneratorNoOp());
        var dto = _fx.CrearVentaDto(
            1,
            (TipoDePago.CtaCte, 1000));

        var resultado = servicio.NuevaVenta(dto);

        Assert.False(resultado.Exitoso);
        Assert.Contains("Stock insuficiente", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
        Assert.Equal(0, _fx.LeerSaldoCtaCte()); // no quedó el cargo
        Assert.Equal(0, _fx.LeerStockProducto());
        Assert.Equal(0, _fx.LeerSaldoCaja());
    }

    [Fact]
    public void Venta_SuperaLimiteDeuda_NoImpactaCajaNiCtaCte()
    {
        _fx.RecrearBaseYSeed();
        _fx.ResetSaldosYStock(stock: 10, saldoCta: 0);

        using (var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null))
        {
            var cta = context.CuentaCorriente.First(c => c.CuentaCorrienteId == _fx.CuentaCorrienteId);
            cta.LimiteDeudaActivo = true;
            cta.LimiteDeuda = 100; // permite solo 100 de deuda
            context.SaveChanges();
        }

        var servicio = new VentaServicio(new PdfGeneratorNoOp());
        var dto = _fx.CrearVentaDto(1, (TipoDePago.CtaCte, 1000));

        var resultado = servicio.NuevaVenta(dto);

        Assert.False(resultado.Exitoso);
        Assert.Equal(0, _fx.LeerSaldoCtaCte());
        Assert.Equal(10, _fx.LeerStockProducto());
        Assert.Equal(0, _fx.LeerSaldoCaja());
    }
}
