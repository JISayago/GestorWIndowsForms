using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Gasto;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.Extras;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Gasto.DTO;
using Servicios.LogicaNegocio.Movimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Gasto
{
    public class GastoServicio : IGastoServicio
    {
        public EstadoOperacion AnularGasto(long gastoId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var gasto = context.Gastos.FirstOrDefault(g => g.GastoId == gastoId);

            if (gasto == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "El gasto no existe."
                };
            }

            if (gasto.EstadoGasto == 3)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "El gasto ya se encuentra anulado."
                };
            }

            gasto.EstadoGasto = 3; // ANULADO

            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Gasto anulado correctamente.",
                EntidadId = gasto.GastoId
            };
        }

        public EstadoOperacion ConfirmarPago(long gastoId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var gasto = context.Gastos.FirstOrDefault(g => g.GastoId == gastoId);

                if (gasto == null)
                {
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "El gasto no existe."
                    };
                }

                // ya pagado
                if (gasto.EstadoGasto == (int)EstadoGasto.Pagado)
                {
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "El gasto ya se encuentra pagado."
                    };
                }

                // anulado
                if (gasto.EstadoGasto == (int)EstadoGasto.Anulado)
                {
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "No se puede pagar un gasto anulado."
                    };
                }

                // 🔥 actualizar montos por consistencia
                gasto.MontoPagado = gasto.MontoTotal;
                gasto.EstadoGasto = (int)EstadoGasto.Pagado;

                context.SaveChanges(); // guardo cambio de estado

                // 🔥 crear movimiento
                var movimientoServicio = new MovimientoServicio();

                movimientoServicio.CrearMovimientoGasto(
                    gasto.GastoId,
                    gasto.MontoPagado,
                    TipoMovimientoDetalle.Servicios,
                    context
                );

                context.SaveChanges();

                transaction.Commit();

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Gasto marcado como pagado correctamente.",
                    EntidadId = gasto.GastoId
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = $"Error al pagar gasto: {ex.Message}"
                };
            }
        }

        public EstadoOperacion NuevoGasto(GastoDTO gastoDto)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            using var transaction = context.Database.BeginTransaction();

            try
            {
                // 1. Validaciones
                if (gastoDto == null)
                    return new EstadoOperacion { Exitoso = false, Mensaje = "Datos de gasto inválidos." };

                if (gastoDto.MontoTotal <= 0)
                    return new EstadoOperacion { Exitoso = false, Mensaje = "El monto del gasto debe ser mayor a cero." };

                var existeEmpleado = context.Empleados.Any(e => e.PersonaId == gastoDto.IdEmpleado);
                if (!existeEmpleado)
                    return new EstadoOperacion { Exitoso = false, Mensaje = "El empleado indicado no existe." };

                if (!string.IsNullOrEmpty(gastoDto.NumeroGasto) &&
                    context.Gastos.Any(g => g.NumeroGasto == gastoDto.NumeroGasto))
                {
                    return new EstadoOperacion { Exitoso = false, Mensaje = "Ya existe un gasto con el mismo número." };
                }

                // =========================================================
                // 🔥 2. LÓGICA DE FECHA
                // =========================================================

                DateTime? fechaGasto = null;

                if (gastoDto.EstadoGasto == (int)EstadoGasto.Pagado)
                {
                    if (!gastoDto.FechaGasto.HasValue)
                        return new EstadoOperacion
                        {
                            Exitoso = false,
                            Mensaje = "Debe ingresar una fecha para un gasto pagado."
                        };

                    fechaGasto = gastoDto.FechaGasto.Value.Date;
                }

                // =========================================================
                // 🔢 3. GENERAR NÚMERO (CLAVE)
                // =========================================================

                // 👉 SI NO HAY fecha (pendiente), usamos FechaRegistro
                var fechaBase = fechaGasto ?? DateTime.Today;

                var cantidadDelDia = context.Gastos
                    .Count(g =>
                        (g.FechaGasto ?? g.FechaRegistro).Date == fechaBase.Date
                    );

                gastoDto.NumeroGasto = GeneradorNumeroComprobante
                    .Generar("GAS", fechaBase, cantidadDelDia);

                // =========================================================
                // 🧾 4. CREAR ENTIDAD
                // =========================================================

                var montoPagado = gastoDto.EstadoGasto == (int)EstadoGasto.Pagado
                    ? gastoDto.MontoTotal
                    : 0;

                var gasto = new AccesoDatos.Entidades.Gasto
                {
                    NumeroGasto = gastoDto.NumeroGasto,
                    IdEmpleado = gastoDto.IdEmpleado,
                    CategoriaGasto = gastoDto.CategoriaGasto,
                    FechaGasto = fechaGasto, // 🔥 ahora sí nullable real
                    FechaRegistro = DateTime.Now,
                    MontoTotal = gastoDto.MontoTotal,
                    MontoPagado = montoPagado,
                    EstadoGasto = gastoDto.EstadoGasto,
                    Detalle = gastoDto.Detalle
                };

                // Validación coherente
                if (gasto.MontoPagado > gasto.MontoTotal)
                {
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "El monto pagado no puede ser mayor al monto total."
                    };
                }

                // =========================================================
                // 💾 5. GUARDAR
                // =========================================================

                context.Gastos.Add(gasto);
                context.SaveChanges();

                // =========================================================
                // 💰 6. MOVIMIENTO (SOLO SI PAGADO)
                // =========================================================

                if (gasto.EstadoGasto == (int)EstadoGasto.Pagado)
                {
                    var movimientoServicio = new MovimientoServicio();

                    movimientoServicio.CrearMovimientoGasto(
                        gasto.GastoId,
                        gasto.MontoPagado,
                        TipoMovimientoDetalle.Servicios,
                        context
                    );
                }

                context.SaveChanges();

                // =========================================================
                // ✅ 7. COMMIT
                // =========================================================

                transaction.Commit();

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Gasto registrado correctamente.",
                    EntidadId = gasto.GastoId
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = $"Error al registrar gasto: {ex.Message}"
                };
            }
        }
        public GastoDTO ObtenerGastoPorId(long gastoId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var gasto = context.Gastos
                .Where(g => g.GastoId == gastoId)
                .Select(g => new GastoDTO
                {
                    GastoId = g.GastoId,
                    NumeroGasto = g.NumeroGasto,
                    IdEmpleado = g.IdEmpleado,
                    NombreEmpleado = g.Empleado.Persona.Nombre + " " + g.Empleado.Persona.Apellido,
                    CategoriaGasto = g.CategoriaGasto,
                    FechaGasto = g.FechaGasto,
                    FechaRegistro = g.FechaRegistro,
                    MontoTotal = g.MontoTotal,
                    MontoPagado = g.MontoPagado,
                    EstadoGasto = g.EstadoGasto,
                    Detalle = g.Detalle
                })
                .FirstOrDefault();

            return gasto;
        }


        //public List<GastoDTO> ObtenerGastos(int? estadoGasto = null)
        //{
        //    using var context = new GestorContextDBFactory().CreateDbContext(null);

        //    var query = context.Gastos.AsQueryable();

        //    if (estadoGasto.HasValue)
        //    {
        //        query = query.Where(g => g.EstadoGasto == estadoGasto.Value);
        //    }
        //    else
        //    {
        //        query = query.Where(g => g.EstadoGasto > 0);
        //    }

        //        var gastos = query
        //            .OrderByDescending(g => g.FechaGasto)
        //            .Select(g => new GastoDTO
        //            {
        //                GastoId = g.GastoId,
        //                NumeroGasto = g.NumeroGasto,
        //                IdEmpleado = g.IdEmpleado,
        //                NombreEmpleado = g.Empleado.Persona.Nombre + " " + g.Empleado.Persona.Apellido,
        //                CategoriaGasto = g.CategoriaGasto,
        //                FechaGasto = g.FechaGasto,
        //                FechaRegistro = g.FechaRegistro,
        //                MontoTotal = g.MontoTotal,
        //                MontoPagado = g.MontoPagado,
        //                EstadoGasto = g.EstadoGasto,
        //                Detalle = g.Detalle
        //            })
        //            .ToList();

        //    return gastos;
        //}

        public ResultadoPaginacion<GastoDTO> ObtenerGastos(FiltroConsulta filtros)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            string collation = "Latin1_General_CI_AI";

            var query = context.Gastos
                .AsNoTracking()
                .Include(g => g.Empleado)
                    .ThenInclude(e => e.Persona)
                .AsQueryable();

            // =========================================================
            // 🧠 CORE: ANULADOS + HISTORICO
            // =========================================================

            if (filtros.Bool2)
            {
                // 👉 HISTÓRICO → no filtramos nada
            }
            else if (filtros.Bool1)
            {
                // 👉 SOLO ANULADOS
                query = query.Where(g => g.EstadoGasto == (int)EstadoGasto.Anulado);
            }
            else
            {
                // 👉 DEFAULT → NO anulados + lógica nueva
                var desde = DateTime.Now.AddMonths(-1);

                query = query.Where(g =>
                    g.EstadoGasto != (int)EstadoGasto.Anulado &&
                    (
                        g.EstadoGasto == (int)EstadoGasto.Pendiente
                        || (g.FechaGasto.HasValue && g.FechaGasto.Value >= desde)
                    ));
            }

            // =========================================================
            // 🔍 BUSQUEDA
            // =========================================================

            if (!string.IsNullOrWhiteSpace(filtros.TextoBuscar))
            {
                var texto = filtros.TextoBuscar.Trim();

                switch (filtros.Filtro1?.ToString())
                {
                    case "NumeroGasto":
                        query = query.Where(g =>
                            g.NumeroGasto != null &&
                            EF.Functions.Collate(g.NumeroGasto, collation).Contains(texto));
                        break;

                    case "NombreEmpleado":
                        query = query.Where(g =>
                            g.Empleado != null &&
                            g.Empleado.Persona != null &&
                            (
                                (g.Empleado.Persona.Nombre != null &&
                                 EF.Functions.Collate(g.Empleado.Persona.Nombre, collation).Contains(texto))
                                ||
                                (g.Empleado.Persona.Apellido != null &&
                                 EF.Functions.Collate(g.Empleado.Persona.Apellido, collation).Contains(texto))
                            ));
                        break;

                    case "CategoriaGasto":
                        var textoBusqueda = texto.ToLower();

                        var categorias = Enum
                            .GetValues(typeof(CategoriaGasto))
                            .Cast<CategoriaGasto>()
                            .Where(c => c.ToString().ToLower().Contains(textoBusqueda))
                            .Select(c => (int)c)
                            .ToList();

                        if (categorias.Any())
                            query = query.Where(g => categorias.Contains(g.CategoriaGasto));
                        else
                            query = query.Where(g => false);

                        break;

                    default:
                        query = query.Where(g =>
                            (g.NumeroGasto != null &&
                             EF.Functions.Collate(g.NumeroGasto, collation).Contains(texto))
                            ||
                            (g.Detalle != null &&
                             EF.Functions.Collate(g.Detalle, collation).Contains(texto)));
                        break;
                }
            }

            // =========================================================
            // 🔴 FILTRO ESTADO (cbx2)
            // =========================================================

            if (filtros.Filtro2 != null &&
                int.TryParse(filtros.Filtro2.ToString(), out var estado))
            {
                query = query.Where(g => g.EstadoGasto == estado);
            }

            // =========================================================
            // 📅 FILTRO FECHA (cbx3)
            // =========================================================

            bool usaFechas = filtros.FechaDesde.HasValue || filtros.FechaHasta.HasValue;

            if (usaFechas && filtros.Filtro3 != null &&
                int.TryParse(filtros.Filtro3.ToString(), out var tipoFecha))
            {
                switch ((TipoFiltroFechaGasto)tipoFecha)
                {
                    case TipoFiltroFechaGasto.FechaGasto:

                        if (filtros.FechaDesde.HasValue)
                            query = query.Where(g =>
                                g.FechaGasto.HasValue &&
                                g.FechaGasto.Value >= filtros.FechaDesde.Value);

                        if (filtros.FechaHasta.HasValue)
                        {
                            var hasta = filtros.FechaHasta.Value.AddDays(1);
                            query = query.Where(g =>
                                g.FechaGasto.HasValue &&
                                g.FechaGasto.Value < hasta);
                        }

                        break;

                    case TipoFiltroFechaGasto.FechaRegistro:

                        if (filtros.FechaDesde.HasValue)
                            query = query.Where(g => g.FechaRegistro >= filtros.FechaDesde.Value);

                        if (filtros.FechaHasta.HasValue)
                        {
                            var hasta = filtros.FechaHasta.Value.AddDays(1);
                            query = query.Where(g => g.FechaRegistro < hasta);
                        }

                        break;
                }
            }

            // =========================================================
            // 📊 TOTAL
            // =========================================================

            var total = query.Count();

            // =========================================================
            // 🔴 PAGINACION
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

            query = query
                .OrderBy(g => g.EstadoGasto == (int)EstadoGasto.Pagado) // pendientes primero
                .ThenByDescending(g => g.FechaGasto ?? g.FechaRegistro)
                .ThenByDescending(g => g.FechaRegistro);

            // =========================================================
            // 📄 DATA
            // =========================================================

            var data = query
                .Skip((filtros.Page - 1) * filtros.PageSize)
                .Take(filtros.PageSize)
                .Select(g => new GastoDTO
                {
                    GastoId = g.GastoId,
                    NumeroGasto = g.NumeroGasto,

                    IdEmpleado = g.IdEmpleado,

                    NombreEmpleado =
                        g.Empleado.Persona.Nombre + " " +
                        g.Empleado.Persona.Apellido,

                    CategoriaGasto = g.CategoriaGasto,

                    FechaGasto = g.FechaGasto,
                    FechaRegistro = g.FechaRegistro,

                    MontoTotal = g.MontoTotal,
                    MontoPagado = g.MontoPagado,

                    EstadoGasto = g.EstadoGasto,

                    Detalle = g.Detalle
                })
                .ToList();

            // =========================================================
            // 📄 RESULTADO
            // =========================================================

            return new ResultadoPaginacion<GastoDTO>
            {
                Items = data,
                TotalRegistros = total,
                Page = filtros.Page,
                PageSize = filtros.PageSize
            };
        }

    }
}
