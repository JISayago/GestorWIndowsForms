using AccesoDatos;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Movimiento;
using Servicios.LogicaNegocio.Movimiento.DTO;

namespace Servicios.LogicaNegocio.Movimiento
{
    public class MovimientoConsultaServicio : IMovimientoConsultaServicio
    {
        private const string Collation = "Latin1_General_CI_AI";

        // =====================================================================
        // LIBRO DE CAJA
        // =====================================================================
        public ResultadoMovimientos ConsultarLibroCaja(FiltroMovimientos filtro)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.Movimientos
                .AsNoTracking()
                .Where(m => m.AfectaCaja);

            if (filtro.CajaId.HasValue)
                query = query.Where(m => m.CajaId == filtro.CajaId.Value);

            // Una caja ya es un período en sí misma: con caja elegida no se aplica la ventana de 2 meses.
            var resultado = Consultar(context, query, filtro, aplicarVentana: !filtro.CajaId.HasValue);

            if (filtro.CajaId.HasValue)
                AplicarSaldoCaja(context, resultado, filtro.CajaId.Value);

            return resultado;
        }

        // =====================================================================
        // CUENTA CORRIENTE
        // =====================================================================
        public ResultadoMovimientos ConsultarCuentaCorriente(FiltroMovimientos filtro)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.Movimientos
                .AsNoTracking()
                .Where(m => m.TipoEntidad == (int)TipoEntidadMovimiento.CuentaCorriente);

            if (filtro.EntidadId.HasValue)
                query = query.Where(m => m.EntidadId == filtro.EntidadId.Value);

            var resultado = Consultar(context, query, filtro, aplicarVentana: true);

            if (filtro.EntidadId.HasValue)
                AplicarSaldoCuentaCorriente(context, resultado, filtro.EntidadId.Value);

            return resultado;
        }

        // =====================================================================
        // GASTOS
        // =====================================================================
        public ResultadoMovimientos ConsultarGastos(FiltroMovimientos filtro)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.Movimientos
                .AsNoTracking()
                .Where(m => m.TipoEntidad == (int)TipoEntidadMovimiento.Gasto);

            if (filtro.EntidadId.HasValue)
                query = query.Where(m => m.EntidadId == filtro.EntidadId.Value);

            if (filtro.AfectaCaja.HasValue)
                query = query.Where(m => m.AfectaCaja == filtro.AfectaCaja.Value);

            return Consultar(context, query, filtro, aplicarVentana: !filtro.EntidadId.HasValue);
        }

        // =====================================================================
        // OPCIONES PARA LOS COMBOS DE FILTRO
        // =====================================================================
        public List<OpcionConsulta> ObtenerCajasParaFiltro()
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.Cajas
                .AsNoTracking()
                .OrderByDescending(c => c.CajaId)
                .Take(60)
                .Select(c => new { c.CajaId, c.FechaInicio, c.EstaCerrada })
                .ToList()
                .Select(c => new OpcionConsulta
                {
                    Id = c.CajaId,
                    Texto = $"Caja N° {c.CajaId} · {c.FechaInicio:dd/MM/yyyy HH:mm} · {(c.EstaCerrada ? "Cerrada" : "Abierta")}"
                })
                .ToList();
        }

        public List<OpcionConsulta> ObtenerCuentasCorrientesParaFiltro()
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.CuentaCorriente
                .AsNoTracking()
                .Where(c => !c.EstaEliminado)
                .Select(c => new
                {
                    c.CuentaCorrienteId,
                    Cliente = c.Cliente.Persona.Nombre + " " + c.Cliente.Persona.Apellido
                })
                .OrderBy(c => c.Cliente)
                .ToList()
                .Select(c => new OpcionConsulta { Id = c.CuentaCorrienteId, Texto = c.Cliente })
                .ToList();
        }

        // =====================================================================
        // NÚCLEO COMÚN
        // =====================================================================
        private static ResultadoMovimientos Consultar(
            GestorContextDB context,
            IQueryable<AccesoDatos.Entidades.Movimiento> query,
            FiltroMovimientos f,
            bool aplicarVentana)
        {
            if (!string.IsNullOrWhiteSpace(f.TextoBuscar))
            {
                var texto = f.TextoBuscar.Trim();

                query = query.Where(m =>
                    EF.Functions.Collate(m.NumeroMovimiento, Collation).Contains(texto));
            }

            if (f.Tipo.HasValue)
                query = query.Where(m => m.TipoMovimiento == f.Tipo.Value);

            if (f.Operacion.HasValue)
                query = query.Where(m => m.Operacion == f.Operacion.Value);

            if (f.TipoPago.HasValue)
                query = query.Where(m => m.TipoPago != null && m.TipoPago.NumeroReferencia == f.TipoPago.Value);

            if (f.Desde.HasValue || f.Hasta.HasValue)
            {
                if (f.Desde.HasValue)
                {
                    var desde = f.Desde.Value.Date;
                    query = query.Where(m => m.FechaMovimiento >= desde);
                }

                if (f.Hasta.HasValue)
                {
                    var hasta = f.Hasta.Value.Date.AddDays(1);
                    query = query.Where(m => m.FechaMovimiento < hasta);
                }
            }
            else if (aplicarVentana && !f.SinLimiteFecha)
            {
                var desde = DateTime.Now.AddMonths(-2);
                query = query.Where(m => m.FechaMovimiento >= desde);
            }

            var ingreso = (int)TipoMovimiento.Ingreso;
            var egreso = (int)TipoMovimiento.Egreso;

            var totalIngresos = query.Where(m => m.TipoMovimiento == ingreso).Sum(m => (decimal?)m.Monto) ?? 0m;
            var totalEgresos = query.Where(m => m.TipoMovimiento == egreso).Sum(m => (decimal?)m.Monto) ?? 0m;

            var total = query.Count();

            var pageSize = f.PageSize <= 0 ? 16 : f.PageSize;
            var totalPaginas = Math.Max(1, (int)Math.Ceiling((double)total / pageSize));
            var page = Math.Min(Math.Max(1, f.Page), totalPaginas);

            var items = query
                .OrderByDescending(m => m.FechaMovimiento)
                .ThenByDescending(m => m.MovimientoId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new MovimientoListadoDTO
                {
                    MovimientoId = m.MovimientoId,
                    NumeroMovimiento = m.NumeroMovimiento,
                    FechaMovimiento = m.FechaMovimiento,
                    TipoMovimiento = m.TipoMovimiento,
                    Operacion = m.Operacion,
                    Monto = m.Monto,
                    TipoPago = m.TipoPago != null ? m.TipoPago.Nombre : string.Empty,
                    AfectaCaja = m.AfectaCaja,
                    CajaId = m.CajaId,
                    TipoEntidad = m.TipoEntidad,
                    EntidadId = m.EntidadId,
                    MovimientoOrigenId = m.MovimientoOrigenId
                })
                .ToList();

            AplicarReferencias(context, items);

            f.Page = page;

            return new ResultadoMovimientos
            {
                Items = items,
                TotalRegistros = total,
                Page = page,
                PageSize = pageSize,
                TotalIngresos = totalIngresos,
                TotalEgresos = totalEgresos
            };
        }

        /// <summary>Completa el texto legible de la entidad (N° de venta, de gasto, cuenta...) con una consulta por tipo.</summary>
        private static void AplicarReferencias(GestorContextDB context, List<MovimientoListadoDTO> items)
        {
            List<long> Ids(TipoEntidadMovimiento tipo) => items
                .Where(i => i.TipoEntidad == (int)tipo && i.EntidadId.HasValue)
                .Select(i => i.EntidadId!.Value)
                .Distinct()
                .ToList();

            var ventaIds = Ids(TipoEntidadMovimiento.Venta);
            var ventas = ventaIds.Count == 0
                ? new Dictionary<long, string>()
                : context.Ventas.AsNoTracking()
                    .Where(v => ventaIds.Contains(v.VentaId))
                    .Select(v => new { v.VentaId, v.NumeroVenta })
                    .ToDictionary(x => x.VentaId, x => x.NumeroVenta);

            var libreIds = Ids(TipoEntidadMovimiento.VentaLibre);
            var libres = libreIds.Count == 0
                ? new Dictionary<long, string>()
                : context.VentasLibres.AsNoTracking()
                    .Where(v => libreIds.Contains(v.VentaLibreId))
                    .Select(v => new { v.VentaLibreId, v.NumeroVenta })
                    .ToDictionary(x => x.VentaLibreId, x => x.NumeroVenta);

            var gastoIds = Ids(TipoEntidadMovimiento.Gasto);
            var gastos = gastoIds.Count == 0
                ? new Dictionary<long, string>()
                : context.Gastos.AsNoTracking()
                    .Where(g => gastoIds.Contains(g.GastoId))
                    .Select(g => new { g.GastoId, g.NumeroGasto })
                    .ToDictionary(x => x.GastoId, x => x.NumeroGasto);

            var cuentaIds = Ids(TipoEntidadMovimiento.CuentaCorriente);
            var cuentas = cuentaIds.Count == 0
                ? new Dictionary<long, string>()
                : context.CuentaCorriente.AsNoTracking()
                    .Where(c => cuentaIds.Contains(c.CuentaCorrienteId))
                    .Select(c => new
                    {
                        c.CuentaCorrienteId,
                        Cliente = c.Cliente.Persona.Nombre + " " + c.Cliente.Persona.Apellido
                    })
                    .ToDictionary(x => x.CuentaCorrienteId, x => x.Cliente);

            foreach (var item in items)
            {
                if (!item.EntidadId.HasValue)
                    continue;

                var id = item.EntidadId.Value;

                item.Referencia = item.TipoEntidad switch
                {
                    (int)TipoEntidadMovimiento.Venta => ventas.TryGetValue(id, out var v) ? $"Venta {v}" : "Venta",
                    (int)TipoEntidadMovimiento.VentaLibre => libres.TryGetValue(id, out var l) ? $"Venta libre {l}" : "Venta libre",
                    (int)TipoEntidadMovimiento.Gasto => gastos.TryGetValue(id, out var g) ? $"Gasto {g}" : "Gasto",
                    (int)TipoEntidadMovimiento.CuentaCorriente => cuentas.TryGetValue(id, out var c) ? $"Cta. cte. {c}" : "Cuenta corriente",
                    _ => string.Empty
                };
            }
        }

        /// <summary>Saldo de la caja después de cada movimiento: saldo inicial + ingresos - egresos, en orden cronológico.</summary>
        private static void AplicarSaldoCaja(GestorContextDB context, ResultadoMovimientos resultado, long cajaId)
        {
            var saldoInicial = context.Cajas
                .Where(c => c.CajaId == cajaId)
                .Select(c => c.SaldoInicial)
                .FirstOrDefault();

            var todos = context.Movimientos
                .AsNoTracking()
                .Where(m => m.AfectaCaja && m.CajaId == cajaId)
                .OrderBy(m => m.FechaMovimiento)
                .ThenBy(m => m.MovimientoId)
                .Select(m => new { m.MovimientoId, m.TipoMovimiento, m.Monto })
                .ToList();

            var saldos = new Dictionary<long, decimal>();
            var acumulado = saldoInicial;

            foreach (var m in todos)
            {
                acumulado += m.TipoMovimiento == (int)TipoMovimiento.Ingreso ? m.Monto : -m.Monto;
                saldos[m.MovimientoId] = acumulado;
            }

            foreach (var item in resultado.Items)
                item.Saldo = saldos.TryGetValue(item.MovimientoId, out var s) ? s : null;

            resultado.TieneSaldo = true;
        }

        /// <summary>
        /// Saldo de la cuenta después de cada movimiento, anclado al saldo actual de la cuenta:
        /// se recorre hacia atrás restando lo que pasó después. Ingreso suma (carga, crédito), Egreso resta (cargo).
        /// </summary>
        private static void AplicarSaldoCuentaCorriente(GestorContextDB context, ResultadoMovimientos resultado, long cuentaId)
        {
            var saldoActual = context.CuentaCorriente
                .Where(c => c.CuentaCorrienteId == cuentaId)
                .Select(c => c.Saldo)
                .FirstOrDefault();

            var todos = context.Movimientos
                .AsNoTracking()
                .Where(m => m.TipoEntidad == (int)TipoEntidadMovimiento.CuentaCorriente && m.EntidadId == cuentaId)
                .OrderByDescending(m => m.FechaMovimiento)
                .ThenByDescending(m => m.MovimientoId)
                .Select(m => new { m.MovimientoId, m.TipoMovimiento, m.Monto })
                .ToList();

            var saldos = new Dictionary<long, decimal>();
            var acumulado = saldoActual;

            foreach (var m in todos)
            {
                saldos[m.MovimientoId] = acumulado;
                acumulado -= m.TipoMovimiento == (int)TipoMovimiento.Ingreso ? m.Monto : -m.Monto;
            }

            foreach (var item in resultado.Items)
                item.Saldo = saldos.TryGetValue(item.MovimientoId, out var s) ? s : null;

            resultado.TieneSaldo = true;
        }
    }
}
