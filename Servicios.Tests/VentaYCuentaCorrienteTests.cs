using Servicios.Helpers.Cliente.CtaCte;
using Servicios.Helpers.OpcionesPagos;
using Servicios.LogicaNegocio.CuentaCorriente;
using Servicios.LogicaNegocio.Venta;
using CuentaEntity = AccesoDatos.Entidades.CuentaCorriente;

namespace Servicios.Tests;

public class VentaMontosHelperTests
{
    [Fact]
    public void SoloEfectivo_TodoVaACaja_CtaCteEnCero()
    {
        var pagos = new List<FormaPago>
        {
            new() { TipoDePago = TipoDePago.Efectivo, Monto = 1000 }
        };

        var (caja, cta) = VentaMontosHelper.Calcular(1000, pagos);

        Assert.Equal(1000, caja);
        Assert.Equal(0, cta);
    }

    [Fact]
    public void SoloCtaCte_TodoVaACtaCte_CajaEnCero()
    {
        var pagos = new List<FormaPago>
        {
            new() { TipoDePago = TipoDePago.CtaCte, Monto = 1000 }
        };

        var (caja, cta) = VentaMontosHelper.Calcular(1000, pagos);

        Assert.Equal(0, caja);
        Assert.Equal(1000, cta);
    }

    [Fact]
    public void Mixto_SeparaCorrectamente()
    {
        var pagos = new List<FormaPago>
        {
            new() { TipoDePago = TipoDePago.Efectivo, Monto = 600 },
            new() { TipoDePago = TipoDePago.CtaCte, Monto = 400 }
        };

        var (caja, cta) = VentaMontosHelper.Calcular(1000, pagos);

        Assert.Equal(600, caja);
        Assert.Equal(400, cta);
    }

    [Fact]
    public void SinPagos_TodoACaja()
    {
        var (caja, cta) = VentaMontosHelper.Calcular(500, null);

        Assert.Equal(500, caja);
        Assert.Equal(0, cta);
    }

    [Fact]
    public void CtaCteConMontoNegativo_UsaAbsoluto()
    {
        // Cancelación puede mandar montos negativos; el helper normaliza.
        var pagos = new List<FormaPago>
        {
            new() { TipoDePago = TipoDePago.CtaCte, Monto = -400 },
            new() { TipoDePago = TipoDePago.Efectivo, Monto = -600 }
        };

        var (caja, cta) = VentaMontosHelper.Calcular(1000, pagos);

        Assert.Equal(600, caja);
        Assert.Equal(400, cta);
    }

    [Fact]
    public void TransferenciaMasCtaCte_CajaEsTotalMenosCtaCte()
    {
        var pagos = new List<FormaPago>
        {
            new() { TipoDePago = TipoDePago.Transferencia, Monto = 700 },
            new() { TipoDePago = TipoDePago.CtaCte, Monto = 300 }
        };

        var (caja, cta) = VentaMontosHelper.Calcular(1000, pagos);

        Assert.Equal(700, caja);
        Assert.Equal(300, cta);
    }

    [Fact]
    public void CreditoDebitoYQr_SinCtaCte_TodoACaja()
    {
        var pagos = new List<FormaPago>
        {
            new() { TipoDePago = TipoDePago.Credito, Monto = 400 },
            new() { TipoDePago = TipoDePago.Debito, Monto = 300 },
            new() { TipoDePago = TipoDePago.QR, Monto = 300 }
        };

        var (caja, cta) = VentaMontosHelper.Calcular(1000, pagos);

        Assert.Equal(1000, caja);
        Assert.Equal(0, cta);
    }

    [Fact]
    public void EnumCtaCte_NoCoincideConPkTipicoDeSeed()
    {
        // Documenta el bug histórico: castear TipoPagoId (PK) a enum era incorrecto.
        // Tras seed: Transferencia suele ser Id=2 pero enum Transferencia=3, CtaCte enum=5.
        Assert.Equal(5, (int)TipoDePago.CtaCte);
        Assert.Equal(3, (int)TipoDePago.Transferencia);
        Assert.Equal(1, (int)TipoDePago.Efectivo);
        Assert.Equal(2, (int)TipoDePago.Credito);
    }
}

public class CuentaCorrienteEstadoTests
{
    private static CuentaEntity NuevaCuenta(
        decimal saldo,
        bool limiteActivo = false,
        decimal limite = 0,
        DateTime? vencimiento = null,
        int tipoVencimiento = (int)TipoVencimientoCuentaCorriente.Manual,
        int meses = 1,
        int estado = (int)EstadoCuentaCorriente.Activa)
    {
        return new CuentaEntity
        {
            Saldo = saldo,
            LimiteDeudaActivo = limiteActivo,
            LimiteDeuda = limite,
            FechaVencimiento = vencimiento,
            TipoVencimiento = tipoVencimiento,
            CantidadMesesVencimiento = meses,
            EstadoCuentaCorriente = estado
        };
    }

    [Fact]
    public void CargarSaldoQueSacaDeDeuda_ActualizaConDeuda()
    {
        var cuenta = NuevaCuenta(saldo: -100);
        cuenta.Saldo = 50; // simula CargarSaldo

        CuentaCorrienteServicio.VerificarYActualizarEstadoInstancia(cuenta);

        Assert.False(cuenta.ConDeuda);
        Assert.Equal((int)EstadoCuentaCorriente.Activa, cuenta.EstadoCuentaCorriente);
    }

    [Fact]
    public void VencimientoManualPasado_Suspende()
    {
        var cuenta = NuevaCuenta(
            saldo: 0,
            vencimiento: DateTime.Now.AddDays(-1),
            tipoVencimiento: (int)TipoVencimientoCuentaCorriente.Manual);

        CuentaCorrienteServicio.VerificarYActualizarEstadoInstancia(cuenta);

        Assert.Equal((int)EstadoCuentaCorriente.Suspendida, cuenta.EstadoCuentaCorriente);
    }

    [Fact]
    public void VencimientoAutomaticoSinDeuda_RuedaFecha()
    {
        var cuenta = NuevaCuenta(
            saldo: 10,
            vencimiento: DateTime.Now.AddDays(-40),
            tipoVencimiento: (int)TipoVencimientoCuentaCorriente.Automatico,
            meses: 1);

        var vencimientoAntes = cuenta.FechaVencimiento;

        CuentaCorrienteServicio.VerificarYActualizarEstadoInstancia(cuenta);

        Assert.Equal((int)EstadoCuentaCorriente.Activa, cuenta.EstadoCuentaCorriente);
        Assert.True(cuenta.FechaVencimiento > DateTime.Now);
        Assert.True(cuenta.FechaVencimiento > vencimientoAntes);
    }

    [Fact]
    public void VencimientoAutomaticoConMesesCero_NoLoop_UsaAlMenosUno()
    {
        var cuenta = NuevaCuenta(
            saldo: 0,
            vencimiento: DateTime.Now.AddDays(-10),
            tipoVencimiento: (int)TipoVencimientoCuentaCorriente.Automatico,
            meses: 0);

        CuentaCorrienteServicio.VerificarYActualizarEstadoInstancia(cuenta);

        Assert.Equal((int)EstadoCuentaCorriente.Activa, cuenta.EstadoCuentaCorriente);
        Assert.True(cuenta.FechaVencimiento > DateTime.Now);
    }

    [Fact]
    public void SuperaLimiteDeuda_Suspende()
    {
        var cuenta = NuevaCuenta(
            saldo: -500,
            limiteActivo: true,
            limite: 400);

        CuentaCorrienteServicio.VerificarYActualizarEstadoInstancia(cuenta);

        Assert.True(cuenta.ConDeuda);
        Assert.Equal((int)EstadoCuentaCorriente.Suspendida, cuenta.EstadoCuentaCorriente);
    }

    [Fact]
    public void Cerrada_NoCambiaEstado()
    {
        var cuenta = NuevaCuenta(
            saldo: -999,
            limiteActivo: true,
            limite: 1,
            vencimiento: DateTime.Now.AddDays(-1),
            estado: (int)EstadoCuentaCorriente.Cerrada);

        CuentaCorrienteServicio.VerificarYActualizarEstadoInstancia(cuenta);

        Assert.Equal((int)EstadoCuentaCorriente.Cerrada, cuenta.EstadoCuentaCorriente);
        Assert.True(cuenta.ConDeuda);
    }
}
