using Servicios.Helpers.OpcionesPagos;
using Servicios.LogicaNegocio.Venta.VentaLibre;

namespace Servicios.Tests;

[Collection("VentaCtaCteE2E")]
public class VentaLibreCtaCteE2ETests
{
    private readonly VentaCtaCteE2EFixture _fx;

    public VentaLibreCtaCteE2ETests(VentaCtaCteE2EFixture fx)
    {
        _fx = fx;
    }

    [Fact]
    public void VentaLibre_100pct_Efectivo_NoTocaCtaCte()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaLibreServicio(new PdfGeneratorNoOp());
        var dto = _fx.CrearVentaLibreDto(1000, (TipoDePago.Efectivo, 1000));

        var resultado = servicio.NuevaVentaLibre(dto);

        Assert.True(resultado.Exitoso, resultado.Mensaje);
        Assert.Equal(0, _fx.LeerSaldoCtaCte());
        Assert.Equal(1000, _fx.LeerSaldoCaja());

        var montos = _fx.LeerMontosUltimaVentaLibre();
        Assert.NotNull(montos);
        Assert.Equal(1000, montos.Value.MontoPagado);
        Assert.Equal(0, montos.Value.MontoAdeudado);
    }

    [Fact]
    public void VentaLibre_Mixta_SeparaCajaYCtaCte_SinDobleCargoCaja()
    {
        // Bug histórico: UI sumaba todos los pagos a caja Y además cobraba CtaCte aparte.
        _fx.RecrearBaseYSeed();
        var servicio = new VentaLibreServicio(new PdfGeneratorNoOp());
        var dto = _fx.CrearVentaLibreDto(
            1000,
            (TipoDePago.Efectivo, 600),
            (TipoDePago.CtaCte, 400));

        var resultado = servicio.NuevaVentaLibre(dto);

        Assert.True(resultado.Exitoso, resultado.Mensaje);
        Assert.Equal(-400, _fx.LeerSaldoCtaCte());
        Assert.Equal(600, _fx.LeerSaldoCaja()); // no 1000

        var montos = _fx.LeerMontosUltimaVentaLibre();
        Assert.NotNull(montos);
        Assert.Equal(600, montos.Value.MontoPagado);
        Assert.Equal(400, montos.Value.MontoAdeudado);
    }

    [Fact]
    public void VentaLibre_SoloCtaCte_CajaEnCero()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaLibreServicio(new PdfGeneratorNoOp());
        var dto = _fx.CrearVentaLibreDto(1000, (TipoDePago.CtaCte, 1000));

        var resultado = servicio.NuevaVentaLibre(dto);

        Assert.True(resultado.Exitoso, resultado.Mensaje);
        Assert.Equal(-1000, _fx.LeerSaldoCtaCte());
        Assert.Equal(0, _fx.LeerSaldoCaja());
    }

    [Fact]
    public void Anular_VentaLibreConCtaCte_DevuelveCredito()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaLibreServicio(new PdfGeneratorNoOp());
        var dto = _fx.CrearVentaLibreDto(
            1000,
            (TipoDePago.Efectivo, 600),
            (TipoDePago.CtaCte, 400));

        var alta = servicio.NuevaVentaLibre(dto);
        Assert.True(alta.Exitoso, alta.Mensaje);
        Assert.Equal(-400, _fx.LeerSaldoCtaCte());

        var id = _fx.UltimaVentaLibreConfirmadaId();
        Assert.NotNull(id);

        var baja = servicio.AnularVentaLibre(id.Value);
        Assert.True(baja.Exitoso, baja.Mensaje);

        Assert.Equal(0, _fx.LeerSaldoCtaCte());
        Assert.Equal(0, _fx.LeerSaldoCaja()); // 600 in - 600 out
    }
}
