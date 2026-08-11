using AccesoDatos;
using Servicios.Helpers.Cliente.CtaCte;
using Servicios.Helpers.OpcionesPagos;
using Servicios.Helpers.VentaEnum;
using Servicios.LogicaNegocio.CuentaCorriente;
using Servicios.LogicaNegocio.Venta;
using Servicios.LogicaNegocio.Venta.VentaLibre;

namespace Servicios.Tests;

[Collection("VentaCtaCteE2E")]
public class VentaExtraE2ETests
{
    private readonly VentaCtaCteE2EFixture _fx;

    public VentaExtraE2ETests(VentaCtaCteE2EFixture fx) => _fx = fx;

    [Fact]
    public void Cancelar_VentaTransferenciaMasCtaCte_UsaNumeroReferenciaYRevierte()
    {
        // Seed: Transferencia no tiene PK == enum; cancelación debe mapear NumeroReferencia.
        _fx.RecrearBaseYSeed();
        var servicio = new VentaServicio(new PdfGeneratorNoOp());
        var dto = _fx.CrearVentaDto(
            1,
            (TipoDePago.Transferencia, 600),
            (TipoDePago.CtaCte, 400));

        var alta = servicio.NuevaVenta(dto);
        Assert.True(alta.Exitoso, alta.Mensaje);
        Assert.Equal(-400, _fx.LeerSaldoCtaCte());
        Assert.Equal(600, _fx.LeerSaldoCaja());

        var ventaId = _fx.UltimaVentaConfirmadaId();
        Assert.NotNull(ventaId);

        var baja = servicio.CancelacionVentaPorId(ventaId.Value);
        Assert.True(baja.Exitoso, baja.Mensaje);
        Assert.Equal(0, _fx.LeerSaldoCtaCte());
        Assert.Equal(0, _fx.LeerSaldoCaja());
        Assert.Equal(100, _fx.LeerStockProducto());
    }

    [Fact]
    public void Cancelar_VentaSoloEfectivo_RevierteCajaYStock()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaServicio(new PdfGeneratorNoOp());
        var dto = _fx.CrearVentaDto(1, (TipoDePago.Efectivo, 1000));

        Assert.True(servicio.NuevaVenta(dto).Exitoso);
        var id = _fx.UltimaVentaConfirmadaId();
        Assert.NotNull(id);

        var baja = servicio.CancelacionVentaPorId(id.Value);
        Assert.True(baja.Exitoso, baja.Mensaje);
        Assert.Equal(0, _fx.LeerSaldoCaja());
        Assert.Equal(0, _fx.LeerSaldoCtaCte());
        Assert.Equal(100, _fx.LeerStockProducto());
    }

    [Fact]
    public void DosVentasCtaCte_AcumulanDeudaYDescuentanStock()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaServicio(new PdfGeneratorNoOp());

        var v1 = servicio.NuevaVenta(_fx.CrearVentaDto(1, (TipoDePago.CtaCte, 1000)));
        Assert.True(v1.Exitoso, v1.Mensaje);

        var v2 = servicio.NuevaVenta(_fx.CrearVentaDto(1, (TipoDePago.CtaCte, 1000)));
        Assert.True(v2.Exitoso, v2.Mensaje);

        Assert.Equal(-2000, _fx.LeerSaldoCtaCte());
        Assert.Equal(98, _fx.LeerStockProducto());
        Assert.Equal(0, _fx.LeerSaldoCaja());
        Assert.Equal(2, _fx.ContarVentasConfirmadas());
    }

    [Fact]
    public void SegundaVentaCtaCte_SuperaLimite_NoImpacta()
    {
        _fx.RecrearBaseYSeed();
        _fx.ConfigurarCuenta(limiteActivo: true, limite: 1500);

        var servicio = new VentaServicio(new PdfGeneratorNoOp());
        Assert.True(servicio.NuevaVenta(_fx.CrearVentaDto(1, (TipoDePago.CtaCte, 1000))).Exitoso);
        Assert.Equal(-1000, _fx.LeerSaldoCtaCte());

        var segunda = servicio.NuevaVenta(_fx.CrearVentaDto(1, (TipoDePago.CtaCte, 1000)));
        Assert.False(segunda.Exitoso);
        Assert.Equal(-1000, _fx.LeerSaldoCtaCte());
        Assert.Equal(99, _fx.LeerStockProducto());
        Assert.Equal(1, _fx.ContarVentasConfirmadas());
    }

    [Fact]
    public void Venta_CuentaSuspendida_NoImpacta()
    {
        _fx.RecrearBaseYSeed();
        _fx.ConfigurarCuenta(estado: (int)EstadoCuentaCorriente.Suspendida);

        var servicio = new VentaServicio(new PdfGeneratorNoOp());
        var resultado = servicio.NuevaVenta(_fx.CrearVentaDto(1, (TipoDePago.CtaCte, 1000)));

        Assert.False(resultado.Exitoso);
        Assert.Equal(0, _fx.LeerSaldoCtaCte());
        Assert.Equal(100, _fx.LeerStockProducto());
        Assert.Equal(0, _fx.LeerSaldoCaja());
    }

    [Fact]
    public void Venta_CuentaCerrada_NoImpacta()
    {
        _fx.RecrearBaseYSeed();
        _fx.ConfigurarCuenta(estado: (int)EstadoCuentaCorriente.Cerrada);

        var servicio = new VentaServicio(new PdfGeneratorNoOp());
        var resultado = servicio.NuevaVenta(_fx.CrearVentaDto(1, (TipoDePago.CtaCte, 1000)));

        Assert.False(resultado.Exitoso);
        Assert.Equal(0, _fx.LeerSaldoCtaCte());
        Assert.Equal(100, _fx.LeerStockProducto());
    }

    [Fact]
    public void Venta_CuentaVencidaSinSaldo_NoPuedeEndeudarse()
    {
        _fx.RecrearBaseYSeed();
        _fx.ConfigurarCuenta(saldo: 0, vencimiento: DateTime.Now.AddDays(-1));

        var servicio = new VentaServicio(new PdfGeneratorNoOp());
        var resultado = servicio.NuevaVenta(_fx.CrearVentaDto(1, (TipoDePago.CtaCte, 1000)));

        Assert.False(resultado.Exitoso);
        Assert.Equal(0, _fx.LeerSaldoCtaCte());
        Assert.Equal(100, _fx.LeerStockProducto());
    }

    [Fact]
    public void Venta_SinClienteConCtaCte_Falla()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaServicio(new PdfGeneratorNoOp());
        var dto = _fx.CrearVentaDto(1, (TipoDePago.CtaCte, 1000));
        dto.IdCliente = null;

        var resultado = servicio.NuevaVenta(dto);

        Assert.False(resultado.Exitoso);
        Assert.Equal(0, _fx.LeerSaldoCtaCte());
        Assert.Equal(100, _fx.LeerStockProducto());
    }

    [Fact]
    public void Venta_SoloTransferencia_CajaYStockOk_CtaCteIntacta()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaServicio(new PdfGeneratorNoOp());
        var resultado = servicio.NuevaVenta(_fx.CrearVentaDto(1, (TipoDePago.Transferencia, 1000)));

        Assert.True(resultado.Exitoso, resultado.Mensaje);
        Assert.Equal(1000, _fx.LeerSaldoCaja());
        Assert.Equal(0, _fx.LeerSaldoCtaCte());
        Assert.Equal(99, _fx.LeerStockProducto());

        var montos = _fx.LeerMontosUltimaVenta();
        Assert.NotNull(montos);
        Assert.Equal(1000, montos.Value.MontoPagado);
        Assert.Equal(0, montos.Value.MontoAdeudado);
    }

    [Fact]
    public void Venta_CreditoMasCtaCte_SeparaMontosEnCabecera()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaServicio(new PdfGeneratorNoOp());
        var resultado = servicio.NuevaVenta(_fx.CrearVentaDto(
            1,
            (TipoDePago.Credito, 250),
            (TipoDePago.CtaCte, 750)));

        Assert.True(resultado.Exitoso, resultado.Mensaje);
        Assert.Equal(250, _fx.LeerSaldoCaja());
        Assert.Equal(-750, _fx.LeerSaldoCtaCte());

        var montos = _fx.LeerMontosUltimaVenta();
        Assert.NotNull(montos);
        Assert.Equal(250, montos.Value.MontoPagado);
        Assert.Equal(750, montos.Value.MontoAdeudado);
    }
}

[Collection("VentaCtaCteE2E")]
public class CuentaCorrienteOperacionesE2ETests
{
    private readonly VentaCtaCteE2EFixture _fx;

    public CuentaCorrienteOperacionesE2ETests(VentaCtaCteE2EFixture fx) => _fx = fx;

    [Fact]
    public void CargarSaldo_PagaDeuda_ActualizaEstadoYCaja()
    {
        _fx.RecrearBaseYSeed();
        var venta = new VentaServicio(new PdfGeneratorNoOp());
        Assert.True(venta.NuevaVenta(_fx.CrearVentaDto(1, (TipoDePago.CtaCte, 1000))).Exitoso);
        Assert.Equal(-1000, _fx.LeerSaldoCtaCte());

        var cta = new CuentaCorrienteServicio();
        var pago = cta.CargarSaldoCuentaCorriente(_fx.CuentaCorrienteId, 0);

        Assert.True(pago.Exitoso, pago.Mensaje);

        var estado = _fx.LeerEstadoCtaCte();
        Assert.Equal(0, estado.Saldo);
        Assert.False(estado.ConDeuda);
        Assert.Equal((int)EstadoCuentaCorriente.Activa, estado.Estado);
        Assert.Equal(1000, _fx.LeerSaldoCaja()); // ingreso por carga de saldo
    }

    [Fact]
    public void PuedeComprar_CuentaActivaDentroDeLimite_Ok()
    {
        _fx.RecrearBaseYSeed();
        _fx.ConfigurarCuenta(saldo: 0, limiteActivo: true, limite: 5000);

        var resultado = new CuentaCorrienteServicio()
            .PuedeComprar(_fx.CuentaCorrienteId, 1000);

        Assert.True(resultado.Exitoso, resultado.Mensaje);
    }

    [Fact]
    public void PuedeComprar_Suspendida_Rechaza()
    {
        _fx.RecrearBaseYSeed();
        _fx.ConfigurarCuenta(estado: (int)EstadoCuentaCorriente.Suspendida);

        var resultado = new CuentaCorrienteServicio()
            .PuedeComprar(_fx.CuentaCorrienteId, 100);

        Assert.False(resultado.Exitoso);
        Assert.Contains("cerrada o suspendida", resultado.Mensaje, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void PuedeComprar_SinPermisoDeuda_RechazaSiQuedaNegativo()
    {
        _fx.RecrearBaseYSeed();
        _fx.ConfigurarCuenta(saldo: 50, limiteActivo: false);

        var resultado = new CuentaCorrienteServicio()
            .PuedeComprar(_fx.CuentaCorrienteId, 100);

        Assert.False(resultado.Exitoso);
    }
}

[Collection("VentaCtaCteE2E")]
public class VentaLibreExtraE2ETests
{
    private readonly VentaCtaCteE2EFixture _fx;

    public VentaLibreExtraE2ETests(VentaCtaCteE2EFixture fx) => _fx = fx;

    [Fact]
    public void VentaLibre_SuperaLimiteDeuda_NoImpacta()
    {
        _fx.RecrearBaseYSeed();
        _fx.ConfigurarCuenta(limiteActivo: true, limite: 100);

        var servicio = new VentaLibreServicio(new PdfGeneratorNoOp());
        var resultado = servicio.NuevaVentaLibre(
            _fx.CrearVentaLibreDto(1000, (TipoDePago.CtaCte, 1000)));

        Assert.False(resultado.Exitoso);
        Assert.Equal(0, _fx.LeerSaldoCtaCte());
        Assert.Equal(0, _fx.LeerSaldoCaja());
    }

    [Fact]
    public void VentaLibre_SinClienteConCtaCte_Falla()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaLibreServicio(new PdfGeneratorNoOp());
        var dto = _fx.CrearVentaLibreDto(1000, (TipoDePago.CtaCte, 1000));
        dto.IdCliente = null;

        var resultado = servicio.NuevaVentaLibre(dto);

        Assert.False(resultado.Exitoso);
        Assert.Equal(0, _fx.LeerSaldoCtaCte());
        Assert.Equal(0, _fx.LeerSaldoCaja());
    }

    [Fact]
    public void VentaLibre_TransferenciaMasCtaCte_YAnular_Revierte()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaLibreServicio(new PdfGeneratorNoOp());
        var alta = servicio.NuevaVentaLibre(_fx.CrearVentaLibreDto(
            1000,
            (TipoDePago.Transferencia, 550),
            (TipoDePago.CtaCte, 450)));

        Assert.True(alta.Exitoso, alta.Mensaje);
        Assert.Equal(-450, _fx.LeerSaldoCtaCte());
        Assert.Equal(550, _fx.LeerSaldoCaja());

        var id = _fx.UltimaVentaLibreConfirmadaId();
        Assert.NotNull(id);

        var baja = servicio.AnularVentaLibre(id.Value);
        Assert.True(baja.Exitoso, baja.Mensaje);
        Assert.Equal(0, _fx.LeerSaldoCtaCte());
        Assert.Equal(0, _fx.LeerSaldoCaja());
    }
}
