using AccesoDatos;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.VentaEnum;

namespace Servicios.Tests;

/// <summary>
/// Helpers de lectura directa a la base para las pruebas de caracterización de Movimientos (Etapa 0).
/// Solo lee (y cierra caja); no contiene lógica de negocio.
/// </summary>
internal static class MovimientosLector
{
    public static List<AccesoDatos.Entidades.Movimiento> Todos()
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);
        return context.Movimientos.AsNoTracking().OrderBy(m => m.MovimientoId).ToList();
    }

    public static List<AccesoDatos.Entidades.Movimiento> De(int tipoEntidad, long entidadId)
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);
        return context.Movimientos.AsNoTracking()
            .Where(m => m.TipoEntidad == tipoEntidad && m.EntidadId == entidadId)
            .OrderBy(m => m.MovimientoId)
            .ToList();
    }

    public static (decimal Ingresos, decimal Egresos, decimal Saldo) Caja(long cajaId)
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);
        var c = context.Cajas.AsNoTracking().First(x => x.CajaId == cajaId);
        return (c.TotalIngresos, c.TotalEgresos, c.SaldoActual);
    }

    public static void CerrarCaja(long cajaId)
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);
        var c = context.Cajas.First(x => x.CajaId == cajaId);
        c.EstaCerrada = true;
        c.FechaFin = DateTime.Now;
        context.SaveChanges();
    }

    public static long? IdVentaPorEstado(EstadoVenta estado)
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);
        return context.Ventas.AsNoTracking()
            .Where(v => v.Estado == (int)estado)
            .OrderByDescending(v => v.VentaId)
            .Select(v => (long?)v.VentaId)
            .FirstOrDefault();
    }

    public static long? IdVentaLibrePorEstado(EstadoVenta estado)
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);
        return context.VentasLibres.AsNoTracking()
            .Where(v => v.Estado == (int)estado)
            .OrderByDescending(v => v.VentaLibreId)
            .Select(v => (long?)v.VentaLibreId)
            .FirstOrDefault();
    }

    public static AccesoDatos.Entidades.Gasto LeerGasto(long gastoId)
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);
        return context.Gastos.AsNoTracking().First(g => g.GastoId == gastoId);
    }
}
