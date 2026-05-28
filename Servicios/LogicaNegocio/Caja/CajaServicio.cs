using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Caja.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Caja
{
    public class CajaServicio : ICajaServicio
    {
        public void AbrirCaja(decimal montoInicial, long empleadoId)
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            if (context.Cajas.Any(c => !c.EstaCerrada))
            {
                throw new InvalidOperationException("Ya hay una caja abierta. No se puede abrir otra caja hasta que la actual sea cerrada.");
            }

            var caja = new AccesoDatos.Entidades.Caja();

            caja.SaldoInicial = montoInicial;
            caja.SaldoActual = montoInicial;
            caja.FechaInicio = DateTime.Now;
            caja.EmpleadoApertura = empleadoId;
            caja.EstaCerrada = false;

            context.Cajas.Add(caja);
            context.SaveChanges();
        }

        public void CerrarCaja(long empleadoId)
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            //revisar ese llamado a db
            var caja = context.Cajas
                .Where(c => !c.EstaCerrada)
                .OrderByDescending(c => c.FechaInicio)
                .FirstOrDefault();

            if (caja != null)
            {
                caja.FechaFin = DateTime.Now;
                caja.EstaCerrada = true;
                caja.EmpleadoCierre = empleadoId; //asignar el empleado que cierra la caja
                caja.BalanceFinal = caja.SaldoActual;
                // Asignar otros valores finales como TotalIngresos, TotalEgresos, BalanceFinal si es necesario
                context.SaveChanges();
            }
        }

        public CajaDTO ObtenerCaja(long cajaId)
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            var caja = context.Cajas
                .Where(c => c.CajaId == cajaId)
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
                    EstaCerrada = c.EstaCerrada,
                })
                .FirstOrDefault();

            if (caja == null)
            {
                throw new InvalidOperationException("No se encontró la caja con el ID proporcionado.");
            }


            return caja;
        }

            public long? ObtenerIdCajaAbierta(GestorContextDB context = null)
        {
            if (context == null)
            {
                context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);
            }

            var caja = context.Cajas
                .Where(c => !c.EstaCerrada)
                .OrderByDescending(c => c.FechaInicio)
                .FirstOrDefault();
            if (caja == null)
            {
                return null;
            }
            return caja.CajaId;
        }
            public long? ObtenerIdDeEña(GestorContextDB context)
        {
            var caja = context.Cajas
                .Where(c => !c.EstaCerrada)
                .OrderByDescending(c => c.FechaInicio)
                .FirstOrDefault();
            if (caja == null)
            {
                return null;
            }
            return caja.CajaId;
        }

        public bool ObtenerEstadoCaja()
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            return context.Cajas
                .Any(c => !c.EstaCerrada);
        }

        public decimal ObtenerSaldoCaja()
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            var saldo = context.Cajas
                .Where(c => !c.EstaCerrada)
                .Select(c => c.SaldoActual)
                .FirstOrDefault();

            return saldo;
        }
        public CajaDTO EstadoInicioCaja()
        {
            var estado = ObtenerEstadoCaja();
            var saldo = ObtenerSaldoCaja(); 
            var caja = new CajaDTO
            {
                EstaCerrada = !estado, //si no hay caja abierta, entonces esta cerrada
                SaldoActual = saldo
            };
            return caja;
        }

        public void RegistrarTransaccion(GestorContextDB context, decimal monto, TipoMovimiento tipo,long cajaId)
        {
            var caja = context.Cajas
                .Where(c => !c.EstaCerrada && c.CajaId == cajaId)
                .OrderByDescending(c => c.FechaInicio)
                .FirstOrDefault();

            if (caja == null)
                throw new InvalidOperationException("No hay una caja abierta.");

            ActualizarSaldoCaja(caja, tipo, monto);

            //context.SaveChanges();
            context.Cajas.Update(caja);

        }
            
        public void ActualizarSaldoCaja(AccesoDatos.Entidades.Caja caja, TipoMovimiento tipo, decimal monto)
        {
            //TipoDeTransaccion podria es un enum en vez de string, fixear
            switch (tipo)
            {
                case TipoMovimiento.Ingreso:
                    caja.TotalIngresos += monto;
                    break;

                case TipoMovimiento.Egreso:
                    caja.TotalEgresos += monto;
                    break;

                default:
                    throw new ArgumentException("Tipo de movimiento inválido.");
            }
            caja.SaldoActual = caja.SaldoInicial + (caja.TotalIngresos - caja.TotalEgresos);
        }
        public ResultadoPaginacion<CajaDTO> ObtenerCajas(FiltroConsulta filtros)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.Cajas
                .AsNoTracking()
                .AsQueryable();

            // =========================================================
            // 📌 ESTADO (cbx1)
            // =========================================================

            switch (filtros.Filtro1?.ToString())
            {
                case "Abierta":
                    query = query.Where(c => !c.EstaCerrada);
                    break;

                case "Cerrada":
                    query = query.Where(c => c.EstaCerrada);
                    break;
            }

            // =========================================================
            // 📅 FECHAS (lógica completa)
            // =========================================================

            if (filtros.FechaDesde.HasValue || filtros.FechaHasta.HasValue)
            {
                // 👉 filtro manual del usuario
                if (filtros.FechaDesde.HasValue)
                {
                    query = query.Where(c =>
                        c.FechaInicio >= filtros.FechaDesde.Value);
                }

                if (filtros.FechaHasta.HasValue)
                {
                    var hasta = filtros.FechaHasta.Value.AddDays(1);

                    query = query.Where(c =>
                        c.FechaInicio < hasta);
                }
            }
            else if (!filtros.Bool2)
            {
                // 👉 default: último mes
                var desde = DateTime.Now.AddMonths(-1);

                query = query.Where(c =>
                    c.FechaInicio >= desde);
            }
            // 👉 si Bool2 = histórico → no filtro fechas

            // =========================================================
            // 📊 TOTAL
            // =========================================================

            var total = query.Count();

            // =========================================================
            // 🔴 PAGINACIÓN
            // =========================================================

            var totalPaginas = (int)Math.Ceiling((double)total / filtros.PageSize);

            if (totalPaginas <= 0)
                totalPaginas = 1;

            if (filtros.Page > totalPaginas)
                filtros.Page = totalPaginas;

            if (filtros.Page < 1)
                filtros.Page = 1;

            // =========================================================
            // 📌 ORDEN
            // =========================================================

            query = query.OrderByDescending(c => c.FechaInicio);

            // =========================================================
            // 📦 TRAER DATA BASE (SIN EMPLEADOS)
            // =========================================================

            var cajas = query
                .Skip((filtros.Page - 1) * filtros.PageSize)
                .Take(filtros.PageSize)
                .Select(c => new
                {
                    c.CajaId,
                    c.SaldoInicial,
                    c.SaldoActual,
                    c.FechaInicio,
                    c.FechaFin,
                    c.TotalIngresos,
                    c.TotalEgresos,
                    c.BalanceFinal,
                    c.EmpleadoApertura,
                    c.EmpleadoCierre,
                    c.EstaCerrada
                })
                .ToList();

            // =========================================================
            // 🔥 TRAER EMPLEADOS (UNA SOLA QUERY)
            // =========================================================

            var empleadosIds = cajas
                .SelectMany(c => new long?[] { c.EmpleadoApertura, c.EmpleadoCierre })
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .Distinct()
                .ToList();

            var empleados = context.Empleados
                .Where(e => empleadosIds.Contains(e.PersonaId))
                .Select(e => new
                {
                    e.PersonaId,
                    e.Username
                })
                .ToList()
                .ToDictionary(e => e.PersonaId, e => e.Username);

            // =========================================================
            // 🧠 MAPEAR A DTO
            // =========================================================

            var data = cajas.Select(c =>
            {
                empleados.TryGetValue(c.EmpleadoApertura, out var nombreApertura);

                string nombreCierre = null;

                if (c.EmpleadoCierre.HasValue)
                {
                    empleados.TryGetValue(c.EmpleadoCierre.Value, out nombreCierre);
                }

                return new CajaDTO
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

                    NombreEmpleadoApertura = nombreApertura ?? "",
                    NombreEmpleadoCierre = nombreCierre ?? "",

                    EstaCerrada = c.EstaCerrada
                };
            }).ToList();

            // =========================================================
            // 📄 RESULTADO
            // =========================================================

            return new ResultadoPaginacion<CajaDTO>
            {
                Items = data,
                TotalRegistros = total,
                Page = filtros.Page,
                PageSize = filtros.PageSize
            };
        }

        public List<CajaDTO> ObtenerUltimasXCajas(int cantidadDeCajas)
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            var cajas = context.Cajas
                .OrderByDescending(c => c.FechaInicio)
                .Take(cantidadDeCajas)
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
                    EstaCerrada = c.EstaCerrada,
                    //MovimientoIds = c.Movimientos
                    //    .Select(m => m.MovimientoId)
                    //    .ToList()
                })
                .OrderBy(c => c.FechaInicio)
                .ToList();


            return cajas;
        }

        public List<CajaDTO> ObtenerCajasUltimosXDias(int cantidadDeDias)
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            var cajas = context.Cajas.Where(c => c.FechaInicio >= DateTime.Now.AddDays(-cantidadDeDias))
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
                    EstaCerrada = c.EstaCerrada,
                    //MovimientoIds = c.Movimientos
                    //    .Select(m => m.MovimientoId)
                    //    .ToList()
                })
                .OrderBy(c => c.FechaInicio)
                .ToList();

            return cajas;
        }

        public List<CajaDTO> ObtenerLasCajasDeXAño(int AñoDeLasCajas)
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            var cajas = context.Cajas.Select(c => new CajaDTO
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
                EstaCerrada = c.EstaCerrada,
                //MovimientoIds = c.Movimientos
                //        .Select(m => m.MovimientoId)
                //        .ToList()
            }).Where(c => c.FechaInicio.Year == AñoDeLasCajas)
              .ToList();

            return cajas;
        }

        public List<CajaDTO> ObtenerCajasPorMesYAño(int mesDeLasCajas, int añoDeLasCajas)
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            var cajas = context.Cajas.Select(c => new CajaDTO
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
                EstaCerrada = c.EstaCerrada,
                //MovimientoIds = c.Movimientos
                //        .Select(m => m.MovimientoId)
                //        .ToList()
            }).Where(c => c.FechaInicio.Year == añoDeLasCajas && c.FechaInicio.Month == mesDeLasCajas)
              .ToList();

            return cajas;
        }

        //PODRIA HACER UNA FUNCION PARA BUSCAR FECHAS ESPECIFICAS,Y REUSAR ESA PARA LOS GRAFICOS FILTRADOS

        public CajaDTO ObtenerCajaAbierta(long? cajaId)
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);
            var caja = context.Cajas
                .Where(c => !c.EstaCerrada && c.CajaId == cajaId)
                .OrderByDescending(c => c.FechaInicio)
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
                    EstaCerrada = c.EstaCerrada,
                })
                .FirstOrDefault();
            return caja;
        }

        //Graficos (mover la funicones de grafios que estan arriba a abacjo)
        public List<int> ObtenerAniosConCajas()
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            return context.Cajas
                .Select(c => c.FechaInicio.Year)
                .Distinct()
                .OrderByDescending(y => y) // Trae del más reciente al más antiguo
                .ToList();
        }

        public List<int> ObtenerMesesConCajas(int anio)
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            return context.Cajas
                .Where(c => c.FechaInicio.Year == anio)
                .Select(c => c.FechaInicio.Month)
                .Distinct()
                .OrderBy(m => m) // De Enero a Diciembre
                .ToList();
        }
    }
}
