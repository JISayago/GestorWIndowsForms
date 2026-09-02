using AccesoDatos;
using AccesoDatos.Config;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Cliente.CtaCte;
using Servicios.Helpers.DatosObligatorios;
using Servicios.Helpers.OpcionesPagos;
using Servicios.Helpers.Producto;
using Servicios.Helpers.VentaEnum;
using Servicios.LogicaNegocio.Venta;
using Servicios.LogicaNegocio.Venta.DTO;
using Servicios.LogicaNegocio.Venta.VentaLibre.DTO;

namespace Servicios.Tests;

/// <summary>
/// Fixture E2E contra SQL LocalDB: crea DB, migra y siembra datos mínimos por test.
/// </summary>
public sealed class VentaCtaCteE2EFixture : IDisposable
{
    public const string CadenaConexion =
        @"Server=(localdb)\MSSQLLocalDB;Database=GestorE2E_VentaCtaCte;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True";

    public long EmpleadoId { get; private set; }
    public long ClienteId { get; private set; }
    public long CuentaCorrienteId { get; private set; }
    public long ProductoId { get; private set; }
    public long CajaId { get; private set; }

    public decimal SaldoInicialCtaCte { get; } = 0m;
    public decimal StockInicialProducto { get; } = 100m;
    public decimal PrecioProducto { get; } = 1000m;

    public VentaCtaCteE2EFixture()
    {
        ConexionOLD.ConfigurarCadenaConexionParaTests(CadenaConexion);
        RecrearBaseYSeed();
    }

    public void RecrearBaseYSeed()
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);
        context.Database.EnsureDeleted();
        // EnsureCreated evita migraciones rotas del historial (ej. Lotes duplicado).
        context.Database.EnsureCreated();

        TipoDePagoInicial.Inicializar(context);

        var marca = new Marca { Nombre = "Marca E2E", EstaEliminado = false };
        var rubro = new Rubro { Nombre = "Rubro E2E", EstaEliminado = false };
        context.Marcas.Add(marca);
        context.Rubros.Add(rubro);
        context.SaveChanges();

        var producto = new Producto
        {
            IdMarca = marca.MarcaId,
            IdRubro = rubro.RubroId,
            ControlPorLote = false,
            Codigo = "E2E-001",
            CodigoBarra = "7790000000001",
            Stock = StockInicialProducto,
            EsFraccionable = false,
            PrecioCosto = 500,
            PrecioVenta = PrecioProducto,
            IvaIncluidoPrecioFinal = true,
            Descripcion = "Producto E2E",
            EstaEliminado = false,
            Estado = (int)EstadoProducto.Disponible,
            Medida = "1",
            UnidadMedida = "u",
            TieneVencimiento = false
        };
        context.Productos.Add(producto);

        var personaEmp = new Persona
        {
            Nombre = "Empleado",
            Apellido = "E2E",
            Dni = "30000001",
            Cuil = "20300000011",
            Telefono = "111",
            EstaEliminado = false
        };
        context.Personas.Add(personaEmp);
        context.SaveChanges();

        var empleado = new Empleado
        {
            PersonaId = personaEmp.PersonaId,
            Legajo = "E2E-1",
            FechaIngreso = DateTime.Today,
            Estado = 1,
            Username = "e2e",
            Pass = "x",
            UsuarioEstaHabilitado = true
        };
        context.Empleados.Add(empleado);

        var personaCli = new Persona
        {
            Nombre = "Cliente",
            Apellido = "E2E",
            Dni = "40000001",
            Cuil = "20400000011",
            Telefono = "222",
            EstaEliminado = false
        };
        context.Personas.Add(personaCli);
        context.SaveChanges();

        var cliente = new Cliente
        {
            PersonaId = personaCli.PersonaId,
            NumeroCliente = "C-E2E-1",
            FechaAlta = DateTime.Today,
            Estado = 1,
            CuentaCorrienteId = null
        };
        context.Cliente.Add(cliente);
        context.SaveChanges();

        var cta = new AccesoDatos.Entidades.CuentaCorriente
        {
            ClienteId = cliente.PersonaId,
            NombreCuentaCorriente = "CC E2E",
            Saldo = SaldoInicialCtaCte,
            LimiteDeuda = 50000,
            LimiteDeudaActivo = true,
            EstaEliminado = false,
            FechaCreacion = DateTime.Now,
            FechaActivacion = DateTime.Now,
            FechaVencimiento = DateTime.Now.AddMonths(3),
            TipoVencimiento = (int)TipoVencimientoCuentaCorriente.Manual,
            CantidadMesesVencimiento = 3,
            EstadoCuentaCorriente = (int)EstadoCuentaCorriente.Activa,
            ConDeuda = false,
            CuentaCorrienteAutorizado = new List<CuentaCorrienteAutorizado>
            {
                new() { Dni = personaCli.Dni }
            }
        };
        context.CuentaCorriente.Add(cta);
        context.SaveChanges();

        cliente.CuentaCorrienteId = cta.CuentaCorrienteId;
        context.SaveChanges();

        var caja = new AccesoDatos.Entidades.Caja
        {
            SaldoInicial = 0,
            SaldoActual = 0,
            FechaInicio = DateTime.Now,
            EmpleadoApertura = empleado.PersonaId,
            EstaCerrada = false,
            TotalIngresos = 0,
            TotalEgresos = 0
        };
        context.Cajas.Add(caja);
        context.SaveChanges();

        EmpleadoId = empleado.PersonaId;
        ClienteId = cliente.PersonaId;
        CuentaCorrienteId = cta.CuentaCorrienteId;
        ProductoId = producto.ProductoId;
        CajaId = caja.CajaId;
    }

    public void ResetSaldosYStock(decimal? stock = null, decimal? saldoCta = null)
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);

        var producto = context.Productos.First(p => p.ProductoId == ProductoId);
        producto.Stock = stock ?? StockInicialProducto;
        producto.Estado = (int)EstadoProducto.Disponible;

        var cta = context.CuentaCorriente.First(c => c.CuentaCorrienteId == CuentaCorrienteId);
        cta.Saldo = saldoCta ?? SaldoInicialCtaCte;
        cta.ConDeuda = cta.Saldo < 0;
        cta.EstadoCuentaCorriente = (int)EstadoCuentaCorriente.Activa;
        cta.LimiteDeudaActivo = true;
        cta.LimiteDeuda = 50000;
        cta.FechaVencimiento = DateTime.Now.AddMonths(3);

        var caja = context.Cajas.First(c => c.CajaId == CajaId);
        if (caja.EstaCerrada)
        {
            caja.EstaCerrada = false;
            caja.FechaFin = null;
        }
        caja.SaldoActual = 0;
        caja.TotalIngresos = 0;
        caja.TotalEgresos = 0;

        context.SaveChanges();
    }

    public void ConfigurarCuenta(
        decimal? saldo = null,
        int? estado = null,
        bool? limiteActivo = null,
        decimal? limite = null,
        DateTime? vencimiento = null)
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);
        var cta = context.CuentaCorriente.First(c => c.CuentaCorrienteId == CuentaCorrienteId);

        if (saldo.HasValue)
        {
            cta.Saldo = saldo.Value;
            cta.ConDeuda = cta.Saldo < 0;
        }

        if (estado.HasValue)
            cta.EstadoCuentaCorriente = estado.Value;

        if (limiteActivo.HasValue)
            cta.LimiteDeudaActivo = limiteActivo.Value;

        if (limite.HasValue)
            cta.LimiteDeuda = limite.Value;

        if (vencimiento.HasValue)
            cta.FechaVencimiento = vencimiento.Value;

        context.SaveChanges();
    }

    public (int Estado, bool ConDeuda, decimal Saldo) LeerEstadoCtaCte()
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);
        var cta = context.CuentaCorriente.AsNoTracking()
            .First(c => c.CuentaCorrienteId == CuentaCorrienteId);
        return (cta.EstadoCuentaCorriente, cta.ConDeuda, cta.Saldo);
    }

    public (decimal MontoPagado, decimal MontoAdeudado)? LeerMontosUltimaVenta()
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);
        var v = context.Ventas.AsNoTracking()
            .Where(x => x.Estado == (int)EstadoVenta.Confirmada)
            .OrderByDescending(x => x.VentaId)
            .Select(x => new { x.MontoPagado, x.MontoAdeudado })
            .FirstOrDefault();
        return v == null ? null : (v.MontoPagado, v.MontoAdeudado);
    }

    public int ContarVentasConfirmadas()
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);
        return context.Ventas.Count(v => v.Estado == (int)EstadoVenta.Confirmada);
    }

    public VentaDTO CrearVentaDto(
        decimal cantidad,
        params (TipoDePago tipo, decimal monto)[] pagos)
    {
        var total = PrecioProducto * cantidad;
        return new VentaDTO
        {
            IdEmpleado = EmpleadoId,
            IdVendedor = EmpleadoId,
            IdCliente = ClienteId,
            FechaVenta = DateTime.Now,
            Total = total,
            TotalSinDescuento = total,
            Descuento = 0,
            Estado = (int)EstadoVenta.Confirmada,
            Detalle = "Venta E2E",
            Items = new List<ItemVentaDTO>
            {
                new()
                {
                    ItemId = ProductoId,
                    Cantidad = cantidad,
                    PrecioVenta = PrecioProducto,
                    PrecioOferta = PrecioProducto,
                    Descripcion = "Producto E2E",
                    Medida = "1",
                    UnidadMedida = "u",
                    Stock = StockInicialProducto,
                    EsOferta = false
                }
            },
            TiposDePagoSeleccionado = pagos
                .Select(p => new FormaPago { TipoDePago = p.tipo, Monto = p.monto })
                .ToList()
        };
    }

    public decimal LeerSaldoCtaCte()
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);
        return context.CuentaCorriente.AsNoTracking()
            .Where(c => c.CuentaCorrienteId == CuentaCorrienteId)
            .Select(c => c.Saldo)
            .First();
    }

    public decimal LeerStockProducto()
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);
        return context.Productos.AsNoTracking()
            .Where(p => p.ProductoId == ProductoId)
            .Select(p => p.Stock)
            .First();
    }

    public decimal LeerSaldoCaja()
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);
        return context.Cajas.AsNoTracking()
            .Where(c => c.CajaId == CajaId)
            .Select(c => c.SaldoActual)
            .First();
    }

    public long? UltimaVentaConfirmadaId()
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);
        return context.Ventas.AsNoTracking()
            .Where(v => v.Estado == (int)EstadoVenta.Confirmada)
            .OrderByDescending(v => v.VentaId)
            .Select(v => (long?)v.VentaId)
            .FirstOrDefault();
    }

    public VentaLibreDTO CrearVentaLibreDto(
        decimal total,
        params (TipoDePago tipo, decimal monto)[] pagos)
    {
        return new VentaLibreDTO
        {
            IdEmpleado = EmpleadoId,
            IdVendedor = EmpleadoId,
            IdCliente = ClienteId,
            FechaVenta = DateTime.Now,
            Total = total,
            Estado = (int)EstadoVenta.Confirmada,
            Detalle = "Venta libre E2E",
            TiposDePagoSeleccionado = pagos
                .Select(p => new FormaPago { TipoDePago = p.tipo, Monto = p.monto })
                .ToList()
        };
    }

    public long? UltimaVentaLibreConfirmadaId()
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);
        return context.VentasLibres.AsNoTracking()
            .Where(v => v.Estado == (int)EstadoVenta.Confirmada)
            .OrderByDescending(v => v.VentaLibreId)
            .Select(v => (long?)v.VentaLibreId)
            .FirstOrDefault();
    }

    public (decimal MontoPagado, decimal MontoAdeudado)? LeerMontosUltimaVentaLibre()
    {
        using var context = new GestorContextDBFactory().CreateDbContext(null);
        var v = context.VentasLibres.AsNoTracking()
            .Where(x => x.Estado == (int)EstadoVenta.Confirmada)
            .OrderByDescending(x => x.VentaLibreId)
            .Select(x => new { x.MontoPagado, x.MontoAdeudado })
            .FirstOrDefault();
        return v == null ? null : (v.MontoPagado, v.MontoAdeudado);
    }

    public void Dispose()
    {
        try
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            context.Database.EnsureDeleted();
        }
        catch
        {
            // ignore cleanup errors
        }
        finally
        {
            ConexionOLD.LimpiarCacheCadenaConexion();
        }
    }
}

[CollectionDefinition("VentaCtaCteE2E")]
public class VentaCtaCteE2ECollection : ICollectionFixture<VentaCtaCteE2EFixture>
{
}
