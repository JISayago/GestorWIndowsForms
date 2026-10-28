using Servicios.Helpers.Gasto;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.OpcionesPagos;
using Servicios.Helpers.VentaEnum;
using Servicios.LogicaNegocio.CuentaCorriente;
using Servicios.LogicaNegocio.Gasto;
using Servicios.LogicaNegocio.Gasto.DTO;
using Servicios.LogicaNegocio.Venta;
using Servicios.LogicaNegocio.Venta.VentaLibre;

namespace Servicios.Tests;

/// <summary>
/// ETAPA 0 – Red de seguridad. Fija el comportamiento ACTUAL de Movimientos y Caja.
/// Algunos tests documentan comportamientos que el refactor va a cambiar a propósito
/// (marcados con "HOY:"); cuando se cambien, el test correspondiente se actualiza en la etapa que lo cambia.
/// </summary>
[Collection("VentaCtaCteE2E")]
public class MovimientosCaracterizacionTests
{
    private readonly VentaCtaCteE2EFixture _fx;

    public MovimientosCaracterizacionTests(VentaCtaCteE2EFixture fx) => _fx = fx;

    private static int I(TipoMovimiento t) => (int)t;
    private static int I(TipoMovimientoDetalle t) => (int)t;
    private static int I(TipoEntidadMovimiento t) => (int)t;

    private GastoDTO CrearGastoDto(decimal monto, EstadoGasto estado) => new()
    {
        IdEmpleado = _fx.EmpleadoId,
        CategoriaGasto = (int)CategoriaGasto.Alquiler,
        Detalle = "Gasto E2E",
        MontoTotal = monto,
        FechaGasto = estado == EstadoGasto.Pagado ? DateTime.Today : null,
        EstadoGasto = (int)estado
    };

    private long NuevoGastoId(decimal monto, EstadoGasto estado)
    {
        var r = new GastoServicio().NuevoGasto(CrearGastoDto(monto, estado));
        Assert.True(r.Exitoso, r.Mensaje);
        Assert.NotNull(r.EntidadId);
        return r.EntidadId!.Value;
    }

    // ------------------------------------------------------------------ VENTA

    [Fact]
    public void Venta_Efectivo_UnMovimientoIngresoYCajaSube()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaServicio(new PdfGeneratorNoOp());

        Assert.True(servicio.NuevaVenta(_fx.CrearVentaDto(1, (TipoDePago.Efectivo, 1000))).Exitoso);
        var ventaId = _fx.UltimaVentaConfirmadaId()!.Value;

        var movs = MovimientosLector.Todos();
        var m = Assert.Single(movs);
        Assert.Equal(I(TipoMovimiento.Ingreso), m.TipoMovimiento);
        Assert.Equal(I(TipoMovimientoDetalle.Venta), m.TipoMovimientoDetalle);
        Assert.Equal(I(TipoEntidadMovimiento.Venta), m.TipoEntidad);
        Assert.Equal(ventaId, m.EntidadId);
        Assert.Equal(1000, m.Monto);
        Assert.False(m.EstaEliminado);
        Assert.StartsWith($"MOV-VENTA-{ventaId}-", m.NumeroMovimiento);

        var caja = MovimientosLector.Caja(_fx.CajaId);
        Assert.Equal(1000, caja.Ingresos);
        Assert.Equal(0, caja.Egresos);
        Assert.Equal(1000, caja.Saldo);
    }

    [Fact]
    public void Venta_MixtaEfectivoYCtaCte_IngresoPorTotalMasEgresoCtaCte()
    {
        // HOY: el movimiento de venta registra el TOTAL (1000) aunque solo 600 entró a caja,
        // y la parte cta cte genera un Egreso de 400 para "compensar".
        _fx.RecrearBaseYSeed();
        var servicio = new VentaServicio(new PdfGeneratorNoOp());

        Assert.True(servicio.NuevaVenta(_fx.CrearVentaDto(1,
            (TipoDePago.Efectivo, 600), (TipoDePago.CtaCte, 400))).Exitoso);
        var ventaId = _fx.UltimaVentaConfirmadaId()!.Value;

        var venta = Assert.Single(MovimientosLector.De(I(TipoEntidadMovimiento.Venta), ventaId));
        Assert.Equal(I(TipoMovimiento.Ingreso), venta.TipoMovimiento);
        Assert.Equal(1000, venta.Monto);

        var cta = Assert.Single(MovimientosLector.De(I(TipoEntidadMovimiento.CuentaCorriente), _fx.CuentaCorrienteId));
        Assert.Equal(I(TipoMovimiento.Egreso), cta.TipoMovimiento);
        Assert.Equal(I(TipoMovimientoDetalle.CuentaCorriente), cta.TipoMovimientoDetalle);
        Assert.Equal(400, cta.Monto);
        Assert.StartsWith("MOV", cta.NumeroMovimiento);
        Assert.EndsWith("CTACTE", cta.NumeroMovimiento);

        Assert.Equal(2, MovimientosLector.Todos().Count);

        var caja = MovimientosLector.Caja(_fx.CajaId);
        Assert.Equal(600, caja.Ingresos);
        Assert.Equal(0, caja.Egresos);
    }

    [Fact]
    public void Venta_100CtaCte_MovimientosPeroCajaIntacta()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaServicio(new PdfGeneratorNoOp());

        Assert.True(servicio.NuevaVenta(_fx.CrearVentaDto(1, (TipoDePago.CtaCte, 1000))).Exitoso);
        var ventaId = _fx.UltimaVentaConfirmadaId()!.Value;

        var venta = Assert.Single(MovimientosLector.De(I(TipoEntidadMovimiento.Venta), ventaId));
        Assert.Equal(I(TipoMovimiento.Ingreso), venta.TipoMovimiento);
        Assert.Equal(1000, venta.Monto);

        var cta = Assert.Single(MovimientosLector.De(I(TipoEntidadMovimiento.CuentaCorriente), _fx.CuentaCorrienteId));
        Assert.Equal(I(TipoMovimiento.Egreso), cta.TipoMovimiento);
        Assert.Equal(1000, cta.Monto);

        var caja = MovimientosLector.Caja(_fx.CajaId);
        Assert.Equal(0, caja.Ingresos);
        Assert.Equal(0, caja.Egresos);
    }

    [Fact]
    public void Venta_Fallida_NoDejaMovimientos()
    {
        _fx.RecrearBaseYSeed();
        _fx.ConfigurarCuenta(estado: (int)Servicios.Helpers.Cliente.CtaCte.EstadoCuentaCorriente.Suspendida);
        var servicio = new VentaServicio(new PdfGeneratorNoOp());

        Assert.False(servicio.NuevaVenta(_fx.CrearVentaDto(1, (TipoDePago.CtaCte, 1000))).Exitoso);

        Assert.Empty(MovimientosLector.Todos());
    }

    // ------------------------------------------------------- CANCELACIÓN VENTA

    [Fact]
    public void CancelarVenta_Efectivo_EgresoCancelacionPorTotal()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaServicio(new PdfGeneratorNoOp());
        Assert.True(servicio.NuevaVenta(_fx.CrearVentaDto(1, (TipoDePago.Efectivo, 1000))).Exitoso);
        var originalId = _fx.UltimaVentaConfirmadaId()!.Value;

        var baja = servicio.CancelacionVentaPorId(originalId);
        Assert.True(baja.Exitoso, baja.Mensaje);
        var contraId = MovimientosLector.IdVentaPorEstado(EstadoVenta.CancelacionVenta);
        Assert.NotNull(contraId);
        Assert.NotEqual(originalId, contraId!.Value);

        var movs = MovimientosLector.Todos();
        Assert.Equal(2, movs.Count);

        var can = Assert.Single(MovimientosLector.De(I(TipoEntidadMovimiento.Venta), contraId.Value));
        Assert.Equal(I(TipoMovimiento.Egreso), can.TipoMovimiento);
        Assert.Equal(I(TipoMovimientoDetalle.Cancelacion), can.TipoMovimientoDetalle);
        Assert.Equal(1000, can.Monto);
        Assert.StartsWith($"CAN-VENTA-{contraId.Value}-", can.NumeroMovimiento);

        var caja = MovimientosLector.Caja(_fx.CajaId);
        Assert.Equal(1000, caja.Ingresos);
        Assert.Equal(1000, caja.Egresos);
        Assert.Equal(0, caja.Saldo);
    }

    [Fact]
    public void CancelarVenta_Mixta_CuatroMovimientosYNetoCero()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaServicio(new PdfGeneratorNoOp());
        Assert.True(servicio.NuevaVenta(_fx.CrearVentaDto(1,
            (TipoDePago.Efectivo, 600), (TipoDePago.CtaCte, 400))).Exitoso);
        var originalId = _fx.UltimaVentaConfirmadaId()!.Value;

        var baja = servicio.CancelacionVentaPorId(originalId);
        Assert.True(baja.Exitoso, baja.Mensaje);

        var movs = MovimientosLector.Todos();
        Assert.Equal(4, movs.Count);

        var ctaMovs = MovimientosLector.De(I(TipoEntidadMovimiento.CuentaCorriente), _fx.CuentaCorrienteId);
        Assert.Equal(2, ctaMovs.Count);
        Assert.Equal(I(TipoMovimiento.Egreso), ctaMovs[0].TipoMovimiento);   // compra
        Assert.Equal(I(TipoMovimiento.Ingreso), ctaMovs[1].TipoMovimiento);  // devolución
        Assert.Equal(400, ctaMovs[1].Monto);

        // HOY: ingresos y egresos de movimientos coinciden (neto 0).
        decimal Neto(IEnumerable<AccesoDatos.Entidades.Movimiento> l) =>
            l.Sum(x => x.TipoMovimiento == I(TipoMovimiento.Ingreso) ? x.Monto : -x.Monto);
        Assert.Equal(0, Neto(movs));

        Assert.Equal(0, _fx.LeerSaldoCtaCte());
        var caja = MovimientosLector.Caja(_fx.CajaId);
        Assert.Equal(600, caja.Ingresos);
        Assert.Equal(600, caja.Egresos);
    }

    [Fact]
    public void CancelarVenta_100CtaCte_SinCajaAbierta_HOY_Falla()
    {
        // HOY: cancelar exige caja abierta aunque no haya efectivo que devolver. (Cambia en Etapa 4.)
        _fx.RecrearBaseYSeed();
        var servicio = new VentaServicio(new PdfGeneratorNoOp());
        Assert.True(servicio.NuevaVenta(_fx.CrearVentaDto(1, (TipoDePago.CtaCte, 1000))).Exitoso);
        var originalId = _fx.UltimaVentaConfirmadaId()!.Value;
        var antes = MovimientosLector.Todos().Count;

        MovimientosLector.CerrarCaja(_fx.CajaId);

        var baja = servicio.CancelacionVentaPorId(originalId);

        Assert.False(baja.Exitoso);
        Assert.Equal(antes, MovimientosLector.Todos().Count);
        Assert.Equal(-1000, _fx.LeerSaldoCtaCte());
        Assert.Equal(99, _fx.LeerStockProducto());
    }

    [Fact]
    public void CancelarVenta_DosVeces_SegundaFalla()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaServicio(new PdfGeneratorNoOp());
        Assert.True(servicio.NuevaVenta(_fx.CrearVentaDto(1, (TipoDePago.Efectivo, 1000))).Exitoso);
        var id = _fx.UltimaVentaConfirmadaId()!.Value;

        Assert.True(servicio.CancelacionVentaPorId(id).Exitoso);
        var cantidad = MovimientosLector.Todos().Count;

        Assert.False(servicio.CancelacionVentaPorId(id).Exitoso);
        Assert.Equal(cantidad, MovimientosLector.Todos().Count);
    }

    // ------------------------------------------------------------ VENTA LIBRE

    [Fact]
    public void VentaLibre_Efectivo_MovimientoConDetalleVentaLibre()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaLibreServicio(new PdfGeneratorNoOp());

        Assert.True(servicio.NuevaVentaLibre(_fx.CrearVentaLibreDto(1000, (TipoDePago.Efectivo, 1000))).Exitoso);
        var id = _fx.UltimaVentaLibreConfirmadaId()!.Value;

        var m = Assert.Single(MovimientosLector.Todos());
        Assert.Equal(I(TipoMovimiento.Ingreso), m.TipoMovimiento);
        Assert.Equal(I(TipoMovimientoDetalle.VentaLibre), m.TipoMovimientoDetalle);
        Assert.Equal(I(TipoEntidadMovimiento.VentaLibre), m.TipoEntidad);
        Assert.Equal(id, m.EntidadId);
        Assert.Equal(1000, m.Monto);
        Assert.StartsWith($"MOV-VENTALIBRE-{id}-", m.NumeroMovimiento);
        Assert.Equal(1000, MovimientosLector.Caja(_fx.CajaId).Ingresos);
    }

    [Fact]
    public void VentaLibre_MixtaYAnulada_CuatroMovimientos()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaLibreServicio(new PdfGeneratorNoOp());
        Assert.True(servicio.NuevaVentaLibre(_fx.CrearVentaLibreDto(1000,
            (TipoDePago.Efectivo, 600), (TipoDePago.CtaCte, 400))).Exitoso);
        var id = _fx.UltimaVentaLibreConfirmadaId()!.Value;

        var baja = servicio.AnularVentaLibre(id);
        Assert.True(baja.Exitoso, baja.Mensaje);
        var contraId = MovimientosLector.IdVentaLibrePorEstado(EstadoVenta.CancelacionVenta)!.Value;

        Assert.Equal(4, MovimientosLector.Todos().Count);

        var can = Assert.Single(MovimientosLector.De(I(TipoEntidadMovimiento.VentaLibre), contraId));
        Assert.Equal(I(TipoMovimiento.Egreso), can.TipoMovimiento);
        Assert.Equal(I(TipoMovimientoDetalle.Cancelacion), can.TipoMovimientoDetalle);
        Assert.Equal(1000, can.Monto);
        Assert.StartsWith($"CAN-VENTALIBRE-{contraId}-", can.NumeroMovimiento);

        var ctaMovs = MovimientosLector.De(I(TipoEntidadMovimiento.CuentaCorriente), _fx.CuentaCorrienteId);
        Assert.Equal(2, ctaMovs.Count);
        Assert.Equal(I(TipoMovimiento.Ingreso), ctaMovs[1].TipoMovimiento);

        var caja = MovimientosLector.Caja(_fx.CajaId);
        Assert.Equal(600, caja.Ingresos);
        Assert.Equal(600, caja.Egresos);
    }

    [Fact]
    public void VentaLibre_AnularDosVeces_SegundaFalla()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new VentaLibreServicio(new PdfGeneratorNoOp());
        Assert.True(servicio.NuevaVentaLibre(_fx.CrearVentaLibreDto(1000, (TipoDePago.Efectivo, 1000))).Exitoso);
        var id = _fx.UltimaVentaLibreConfirmadaId()!.Value;

        Assert.True(servicio.AnularVentaLibre(id).Exitoso);
        Assert.False(servicio.AnularVentaLibre(id).Exitoso);
    }

    // ------------------------------------------------------------------ GASTO

    [Fact]
    public void Gasto_Pagado_HOY_MovimientoEgresoServicios_SinTocarCaja()
    {
        // HOY: el detalle es siempre Servicios y la caja no se toca. (Cambia en Etapas 3/4.)
        _fx.RecrearBaseYSeed();

        var gastoId = NuevoGastoId(300, EstadoGasto.Pagado);

        var m = Assert.Single(MovimientosLector.Todos());
        Assert.Equal(I(TipoMovimiento.Egreso), m.TipoMovimiento);
        Assert.Equal(I(TipoMovimientoDetalle.Servicios), m.TipoMovimientoDetalle);
        Assert.Equal(I(TipoEntidadMovimiento.Gasto), m.TipoEntidad);
        Assert.Equal(gastoId, m.EntidadId);
        Assert.Equal(300, m.Monto);
        Assert.StartsWith($"MOV-GASTO-{gastoId}-", m.NumeroMovimiento);

        var g = MovimientosLector.LeerGasto(gastoId);
        Assert.Equal((int)EstadoGasto.Pagado, g.EstadoGasto);
        Assert.Equal(300, g.MontoPagado);
        Assert.Equal(DateTime.Today, g.FechaGasto!.Value);

        var caja = MovimientosLector.Caja(_fx.CajaId);
        Assert.Equal(0, caja.Ingresos);
        Assert.Equal(0, caja.Egresos);
        Assert.Equal(0, caja.Saldo);
    }

    [Fact]
    public void Gasto_Pendiente_SinMovimiento()
    {
        _fx.RecrearBaseYSeed();

        var gastoId = NuevoGastoId(300, EstadoGasto.Pendiente);

        Assert.Empty(MovimientosLector.Todos());
        var g = MovimientosLector.LeerGasto(gastoId);
        Assert.Equal((int)EstadoGasto.Pendiente, g.EstadoGasto);
        Assert.Equal(0, g.MontoPagado);
        Assert.Null(g.FechaGasto);
    }

    [Fact]
    public void Gasto_ConfirmarPago_HOY_CreaMovimientoYNoSeteaFecha()
    {
        // HOY: ConfirmarPago no asigna FechaGasto ni toca caja. (Se corrige en Etapa 3.)
        _fx.RecrearBaseYSeed();
        var servicio = new GastoServicio();
        var gastoId = NuevoGastoId(300, EstadoGasto.Pendiente);

        var r = servicio.ConfirmarPago(gastoId);
        Assert.True(r.Exitoso, r.Mensaje);

        var m = Assert.Single(MovimientosLector.Todos());
        Assert.Equal(I(TipoMovimiento.Egreso), m.TipoMovimiento);
        Assert.Equal(I(TipoMovimientoDetalle.Servicios), m.TipoMovimientoDetalle);
        Assert.Equal(300, m.Monto);

        var g = MovimientosLector.LeerGasto(gastoId);
        Assert.Equal((int)EstadoGasto.Pagado, g.EstadoGasto);
        Assert.Equal(300, g.MontoPagado);
        Assert.Null(g.FechaGasto);

        Assert.Equal(0, MovimientosLector.Caja(_fx.CajaId).Egresos);

        // Segundo intento: ya está pagado.
        Assert.False(servicio.ConfirmarPago(gastoId).Exitoso);
        Assert.Single(MovimientosLector.Todos());
    }

    [Fact]
    public void Gasto_Anular_HOY_NoCreaContramovimientoNiTocaCaja()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new GastoServicio();
        var gastoId = NuevoGastoId(300, EstadoGasto.Pagado);

        var r = servicio.AnularGasto(gastoId);
        Assert.True(r.Exitoso, r.Mensaje);

        var g = MovimientosLector.LeerGasto(gastoId);
        Assert.Equal((int)EstadoGasto.Anulado, g.EstadoGasto);
        Assert.Single(MovimientosLector.Todos());
        Assert.Equal(0, MovimientosLector.Caja(_fx.CajaId).Ingresos);
    }

    [Fact]
    public void Gasto_Anular_HOY_PuedeAnularseDosVeces_BugDeComparacion()
    {
        // HOY (bug): AnularGasto compara contra 3 en vez de EstadoGasto.Anulado (23),
        // por lo que no detecta el gasto ya anulado. Al corregirse este test debe invertirse.
        _fx.RecrearBaseYSeed();
        var servicio = new GastoServicio();
        var gastoId = NuevoGastoId(300, EstadoGasto.Pagado);

        Assert.True(servicio.AnularGasto(gastoId).Exitoso);
        Assert.True(servicio.AnularGasto(gastoId).Exitoso);
    }

    [Fact]
    public void Gasto_Invalido_NoCreaNada()
    {
        _fx.RecrearBaseYSeed();
        var dto = CrearGastoDto(0, EstadoGasto.Pagado);

        Assert.False(new GastoServicio().NuevoGasto(dto).Exitoso);
        Assert.Empty(MovimientosLector.Todos());
    }

    // -------------------------------------------------- CARGA DE SALDO CTA CTE

    [Fact]
    public void CargaSaldo_HOY_IngresoDeCajaYMovimientoCtaCte()
    {
        _fx.RecrearBaseYSeed();
        var servicio = new CuentaCorrienteServicio();

        var r = servicio.CargarSaldoCuentaCorriente(_fx.CuentaCorrienteId, 500);
        Assert.True(r.Exitoso, r.Mensaje);

        Assert.Equal(500, _fx.LeerSaldoCtaCte());

        var m = Assert.Single(MovimientosLector.Todos());
        Assert.Equal(I(TipoMovimiento.Ingreso), m.TipoMovimiento);
        Assert.Equal(I(TipoMovimientoDetalle.CuentaCorriente), m.TipoMovimientoDetalle);
        Assert.Equal(I(TipoEntidadMovimiento.CuentaCorriente), m.TipoEntidad);
        Assert.Equal(_fx.CuentaCorrienteId, m.EntidadId);
        Assert.Equal(500, m.Monto);
        Assert.StartsWith($"MOV-CTACTE-{_fx.CuentaCorrienteId}-", m.NumeroMovimiento);

        var caja = MovimientosLector.Caja(_fx.CajaId);
        Assert.Equal(500, caja.Ingresos);
        Assert.Equal(500, caja.Saldo);
    }

    [Fact]
    public void CargaSaldo_MismoSaldo_NoHaceNada()
    {
        _fx.RecrearBaseYSeed();

        var r = new CuentaCorrienteServicio().CargarSaldoCuentaCorriente(_fx.CuentaCorrienteId, 0);

        Assert.False(r.Exitoso);
        Assert.Empty(MovimientosLector.Todos());
    }

    [Fact]
    public void CargaSaldo_SinCajaAbierta_NoImpacta()
    {
        _fx.RecrearBaseYSeed();
        MovimientosLector.CerrarCaja(_fx.CajaId);

        var r = new CuentaCorrienteServicio().CargarSaldoCuentaCorriente(_fx.CuentaCorrienteId, 500);

        Assert.False(r.Exitoso);
        Assert.Equal(0, _fx.LeerSaldoCtaCte());
        Assert.Empty(MovimientosLector.Todos());
    }

    // ---------------------------------------------------------- INVARIANTES

    [Fact]
    public void NumerosDeMovimiento_HOY_NoSonUnicosSiSeRepitenMontosCtaCte()
    {
        // HOY: el número de movimiento de cta cte es MOV{monto}CTACTE: dos compras iguales repiten número.
        // (La Etapa 1 agrega índice único y la Etapa 2 cambia la generación.)
        _fx.RecrearBaseYSeed();
        var servicio = new VentaServicio(new PdfGeneratorNoOp());

        Assert.True(servicio.NuevaVenta(_fx.CrearVentaDto(1, (TipoDePago.CtaCte, 1000))).Exitoso);
        Assert.True(servicio.NuevaVenta(_fx.CrearVentaDto(1, (TipoDePago.CtaCte, 1000))).Exitoso);

        var cta = MovimientosLector.De(I(TipoEntidadMovimiento.CuentaCorriente), _fx.CuentaCorrienteId);
        Assert.Equal(2, cta.Count);
        Assert.Equal(cta[0].NumeroMovimiento, cta[1].NumeroMovimiento);
    }

    [Fact]
    public void Montos_DeMovimientos_SiempreNoNegativos()
    {
        _fx.RecrearBaseYSeed();
        var venta = new VentaServicio(new PdfGeneratorNoOp());
        Assert.True(venta.NuevaVenta(_fx.CrearVentaDto(1,
            (TipoDePago.Efectivo, 600), (TipoDePago.CtaCte, 400))).Exitoso);
        Assert.True(venta.CancelacionVentaPorId(_fx.UltimaVentaConfirmadaId()!.Value).Exitoso);
        NuevoGastoId(100, EstadoGasto.Pagado);

        Assert.All(MovimientosLector.Todos(), m => Assert.True(m.Monto > 0));
    }
}
