using AccesoDatos;
using Servicios.Helpers.Gasto;
using Servicios.Helpers.VentaEnum;
using Servicios.LogicaNegocio.Caja.DTO;
using Servicios.LogicaNegocio.Sistema.Administracion.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Sistema.Administracion
{
    public class AdministracionGraficosServicios
    {
        public GraficosAdministracionDTO ObtenerDatos(int año, int mes)
        {
            int mesAnterior = mes == 1 ? 12 : mes - 1;
            int anioMesAnterior = mes == 1 ? año - 1 : año;
            int anioAnterior = año - 1;

            var inicioMes = new DateTime(año, mes, 1);
            var finMes = inicioMes.AddMonths(1);
            var inicioMesAnterior = new DateTime(anioMesAnterior, mesAnterior, 1);
            var finMesAnterior = inicioMesAnterior.AddMonths(1);
            var inicioAnio = new DateTime(año, 1, 1);
            var finAnio = new DateTime(año + 1, 1, 1);
            var inicioAnioAnterior = new DateTime(anioAnterior, 1, 1);
            var finAnioAnterior = inicioAnio;

            // Cada Task usa su propio DbContext (EF no es thread-safe).
            var tCajas = Task.Run(() => ObtenerCajasPorRango(inicioMes, finMes));
            var tVentasMes = Task.Run(() => ObtenerVentasResumen(inicioMes, finMes));
            var tVentasMesAnt = Task.Run(() => ObtenerVentasAgregadasPorDia(inicioMesAnterior, finMesAnterior));
            var tVentasAnio = Task.Run(() => ObtenerVentasAgregadasPorMes(inicioAnio, finAnio));
            var tVentasAnioAnt = Task.Run(() => ObtenerVentasAgregadasPorMes(inicioAnioAnterior, finAnioAnterior));
            var tPagos = Task.Run(() => ObtenerPagosPorTipo(inicioMes, finMes));
            var tTop = Task.Run(() => ObtenerTopProductos(inicioMes, finMes, 10));
            var tGastos = Task.Run(() => ObtenerTotalGastosPagados(inicioMes, finMes));

            Task.WaitAll(tCajas, tVentasMes, tVentasMesAnt, tVentasAnio, tVentasAnioAnt, tPagos, tTop, tGastos);

            return new GraficosAdministracionDTO
            {
                CajasMes = tCajas.Result,
                VentasMes = tVentasMes.Result,
                VentasMesAnteriorPorDia = tVentasMesAnt.Result,
                VentasAnioPorMes = tVentasAnio.Result,
                VentasAnioAnteriorPorMes = tVentasAnioAnt.Result,
                PagosMes = tPagos.Result,
                TopProductosMes = tTop.Result,
                TotalGastosMes = tGastos.Result
            };
        }

        private static GestorContextDB CrearContexto()
            => new GestorContextDBFactory().CreateDbContext(null);

        private static List<CajaDTO> ObtenerCajasPorRango(DateTime inicio, DateTime fin)
        {
            using var context = CrearContexto();

            return context.Cajas
                .Where(c => c.FechaInicio >= inicio && c.FechaInicio < fin)
                .OrderBy(c => c.FechaInicio)
                .Select(c => new CajaDTO
                {
                    CajaId = c.CajaId,
                    SaldoInicial = c.SaldoInicial,
                    SaldoActual = c.SaldoActual,
                    FechaInicio = c.FechaInicio,
                    FechaFin = c.FechaFin,
                    TotalIngresos = c.TotalIngresos,
                    TotalEgresos = c.TotalEgresos,
                    BalanceFinal = c.BalanceFinal,
                    EmpleadoApertura = c.EmpleadoApertura,
                    EmpleadoCierre = c.EmpleadoCierre,
                    EstaCerrada = c.EstaCerrada
                })
                .ToList();
        }

        private static List<VentaResumenGraficoDTO> ObtenerVentasResumen(DateTime inicio, DateTime fin)
        {
            using var context = CrearContexto();
            int estadoConfirmada = (int)EstadoVenta.Confirmada;

            return context.Ventas
                .Where(v => v.Estado == estadoConfirmada
                    && v.FechaVenta >= inicio
                    && v.FechaVenta < fin)
                .Select(v => new VentaResumenGraficoDTO
                {
                    FechaVenta = v.FechaVenta,
                    Total = v.Total
                })
                .ToList();
        }

        private static List<VentaDiaAgregadoDTO> ObtenerVentasAgregadasPorDia(DateTime inicio, DateTime fin)
        {
            using var context = CrearContexto();
            int estadoConfirmada = (int)EstadoVenta.Confirmada;

            return context.Ventas
                .Where(v => v.Estado == estadoConfirmada
                    && v.FechaVenta >= inicio
                    && v.FechaVenta < fin)
                .GroupBy(v => v.FechaVenta.Day)
                .Select(g => new VentaDiaAgregadoDTO
                {
                    Dia = g.Key,
                    Total = g.Sum(x => x.Total),
                    Cantidad = g.Count()
                })
                .ToList();
        }

        private static List<VentaMesAgregadoDTO> ObtenerVentasAgregadasPorMes(DateTime inicio, DateTime fin)
        {
            using var context = CrearContexto();
            int estadoConfirmada = (int)EstadoVenta.Confirmada;

            return context.Ventas
                .Where(v => v.Estado == estadoConfirmada
                    && v.FechaVenta >= inicio
                    && v.FechaVenta < fin)
                .GroupBy(v => v.FechaVenta.Month)
                .Select(g => new VentaMesAgregadoDTO
                {
                    Mes = g.Key,
                    Total = g.Sum(x => x.Total),
                    Cantidad = g.Count()
                })
                .ToList();
        }

        private static List<PagoTipoResumenDTO> ObtenerPagosPorTipo(DateTime inicio, DateTime fin)
        {
            using var context = CrearContexto();
            int estadoConfirmada = (int)EstadoVenta.Confirmada;

            return context.VentaPagosDetalles
                .Where(p => p.IdVenta != null
                    && p.Venta.Estado == estadoConfirmada
                    && p.Venta.FechaVenta >= inicio
                    && p.Venta.FechaVenta < fin)
                .GroupBy(p => p.TipoPago.Nombre)
                .Select(g => new PagoTipoResumenDTO
                {
                    Nombre = g.Key ?? "Sin nombre",
                    Total = g.Sum(x => x.Monto)
                })
                .Where(x => x.Total > 0)
                .OrderByDescending(x => x.Total)
                .ToList();
        }

        private static List<ProductoTopDTO> ObtenerTopProductos(DateTime inicio, DateTime fin, int top)
        {
            using var context = CrearContexto();
            int estadoConfirmada = (int)EstadoVenta.Confirmada;

            // Agrupa por producto; el nombre sale de Descripcion del detalle (denormalizada).
            return context.DetallesVentas
                .Where(d => d.IdProducto != null
                    && d.Venta.Estado == estadoConfirmada
                    && d.Venta.FechaVenta >= inicio
                    && d.Venta.FechaVenta < fin)
                .GroupBy(d => d.IdProducto)
                .Select(g => new ProductoTopDTO
                {
                    Nombre = g.Max(x => x.Descripcion) ?? "Sin nombre",
                    Cantidad = g.Sum(x => x.Cantidad),
                    Total = g.Sum(x => x.Subtotal)
                })
                .OrderByDescending(x => x.Total)
                .Take(top)
                .ToList();
        }

        private static decimal ObtenerTotalGastosPagados(DateTime inicio, DateTime fin)
        {
            using var context = CrearContexto();
            int estadoPagado = (int)EstadoGasto.Pagado;

            return context.Gastos
                .Where(g => g.EstadoGasto == estadoPagado
                    && (g.FechaGasto ?? g.FechaRegistro) >= inicio
                    && (g.FechaGasto ?? g.FechaRegistro) < fin)
                .Sum(g => (decimal?)g.MontoPagado) ?? 0m;
        }
    }
}
