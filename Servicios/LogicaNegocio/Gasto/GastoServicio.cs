using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Gasto;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.OpcionesPagos;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.Extras;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.Infraestructura;
using Servicios.LogicaNegocio.Gasto.DTO;
using Servicios.LogicaNegocio.Movimiento;
using Servicios.LogicaNegocio.Producto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Gasto
{
    public class GastoServicio : IGastoServicio
    {
        private readonly IPdfGenerator _pdf;

        public GastoServicio()
        {
            _pdf = new PdfGenerator();
        }
        private void GeneracionComprobanteGasto(GestorContextDB context, AccesoDatos.Entidades.Gasto gasto)
        {
            var gastoCompleto = context.Gastos
                .Include(g => g.Empleado)
                    .ThenInclude(e => e.Persona)
                .Include(g => g.VentaPagoDetalles)
                    .ThenInclude(p => p.TipoPago)
                .First(g => g.GastoId == gasto.GastoId);

            // 🔥 lógica de decisión
            if (gastoCompleto.EstadoGasto == (int)EstadoGasto.Pagado)
            {
                _pdf.GenerarGasto(gastoCompleto);
            }
            else if (gastoCompleto.EstadoGasto == (int)EstadoGasto.Anulado && gastoCompleto.MontoPagado > 0)
            {
                _pdf.GenerarGastoAnulado(gastoCompleto);
            }
        }
        private static EstadoOperacion Error(string mensaje) =>
            new EstadoOperacion { Exitoso = false, Mensaje = mensaje };

        /// <summary>
        /// Paga un gasto dentro de la transacción del llamador: valida origen y tipo de pago, descuenta la caja si el
        /// origen es Caja (bloquea si supera el efectivo disponible), registra el movimiento y el tipo de pago.
        /// Devuelve null si salió bien, o el error a informar.
        /// </summary>
        private EstadoOperacion? RegistrarPago(GestorContextDB context, AccesoDatos.Entidades.Gasto gasto, int? origen, int? tipoPago, string? extra)
        {
            if (!origen.HasValue || !Enum.IsDefined(typeof(OrigenPagoGasto), origen.Value))
                return Error("Debe indicar el origen del pago (Caja u Otro).");

            var origenPago = (OrigenPagoGasto)origen.Value;
            var movimientoServicio = new MovimientoServicio();
            var cajaServicio = new Caja.CajaServicio();

            TipoDePago tipo;
            long? cajaId = null;

            if (origenPago == OrigenPagoGasto.Caja)
            {
                // Plata de la caja: siempre efectivo.
                tipo = TipoDePago.Efectivo;

                cajaId = cajaServicio.ObtenerIdDeUltimaCajaAbierta(context);
                if (!cajaId.HasValue)
                    return Error("Debe haber una caja abierta para pagar un gasto con dinero de la caja.");

                var disponible = movimientoServicio.ObtenerEfectivoDisponible(context, cajaId.Value);
                if (gasto.MontoTotal > disponible)
                    return Error($"El efectivo disponible en la caja ({disponible:C}) no alcanza para pagar este gasto ({gasto.MontoTotal:C}).");
            }
            else
            {
                if (!tipoPago.HasValue || !Enum.IsDefined(typeof(TipoDePago), tipoPago.Value))
                    return Error("Debe indicar el tipo de pago.");

                tipo = (TipoDePago)tipoPago.Value;

                if (tipo == TipoDePago.CtaCte)
                    return Error("La cuenta corriente no se puede usar para pagar gastos.");
            }

            gasto.OrigenPago = (int)origenPago;
            gasto.MontoPagado = gasto.MontoTotal;
            gasto.EstadoGasto = (int)EstadoGasto.Pagado;

            var movimiento = movimientoServicio.RegistrarPagoGasto(
                context,
                gasto.GastoId,
                origenPago == OrigenPagoGasto.Caja,
                cajaId,
                tipo,
                gasto.MontoPagado);

            if (movimiento == null)
                return Error("El monto del gasto debe ser mayor a cero.");

            if (origenPago == OrigenPagoGasto.Caja)
                cajaServicio.RegistrarTransaccion(context, gasto.MontoPagado, TipoMovimiento.Egreso, cajaId!.Value);

            context.VentaPagosDetalles.Add(new VentaPagoDetalle
            {
                IdGasto = gasto.GastoId,
                IdTipoPago = movimiento.TipoPagoId!.Value,
                Monto = gasto.MontoPagado,
                ExtraDescripcionPago = string.IsNullOrWhiteSpace(extra) ? "Sin especificar" : extra.Trim()
            });

            return null;
        }

        public EstadoOperacion AnularGasto(long gastoId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var gasto = context.Gastos.FirstOrDefault(g => g.GastoId == gastoId);

                if (gasto == null)
                    return Error("El gasto no existe.");

                if (gasto.EstadoGasto == (int)EstadoGasto.Anulado)
                    return Error("El gasto ya se encuentra anulado.");

                var estabaPagado = gasto.EstadoGasto == (int)EstadoGasto.Pagado || gasto.MontoPagado > 0;

                if (estabaPagado)
                {
                    // Un gasto pagado se anula con una reversa del pago original.
                    var original = context.Movimientos.FirstOrDefault(m =>
                        m.TipoEntidad == (int)TipoEntidadMovimiento.Gasto &&
                        m.EntidadId == gastoId &&
                        m.Operacion == (int)Operacion.PagoGasto &&
                        m.MovimientoOrigenId == null);

                    if (original != null)
                    {
                        var cajaServicio = new Caja.CajaServicio();
                        long? cajaId = null;

                        if (original.AfectaCaja)
                        {
                            // El dinero vuelve a la caja abierta actual.
                            cajaId = cajaServicio.ObtenerIdDeUltimaCajaAbierta(context);
                            if (!cajaId.HasValue)
                                return Error("Debe haber una caja abierta para anular un gasto pagado con dinero de la caja.");
                        }

                        new MovimientoServicio().RegistrarReversa(
                            context,
                            original,
                            TipoEntidadMovimiento.Gasto,
                            gastoId,
                            cajaId,
                            "REV-GASTO");

                        if (original.AfectaCaja)
                            cajaServicio.RegistrarTransaccion(context, original.Monto, TipoMovimiento.Ingreso, cajaId!.Value);
                    }
                }

                gasto.EstadoGasto = (int)EstadoGasto.Anulado;

                context.SaveChanges();
                transaction.Commit();

                if (estabaPagado)
                {
                    try
                    {
                        GeneracionComprobanteGasto(context, gasto);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Error generando PDF gasto anulado: " + ex.Message);
                    }
                }

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Gasto anulado correctamente.",
                    EntidadId = gasto.GastoId
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                return Error($"Error al anular gasto: {ex.Message}");
            }
        }

        public EstadoOperacion ConfirmarPago(long gastoId, GastoDTO datosPago)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            using var transaction = context.Database.BeginTransaction();

            try
            {
                if (datosPago == null)
                    return Error("Datos de pago inválidos.");

                var gasto = context.Gastos.FirstOrDefault(g => g.GastoId == gastoId);

                if (gasto == null)
                    return Error("El gasto no existe.");

                if (gasto.EstadoGasto == (int)EstadoGasto.Pagado)
                    return Error("El gasto ya se encuentra pagado.");

                if (gasto.EstadoGasto == (int)EstadoGasto.Anulado)
                    return Error("No se puede pagar un gasto anulado.");

                if (!datosPago.FechaGasto.HasValue)
                    return Error("Debe ingresar la fecha del pago.");

                gasto.FechaGasto = datosPago.FechaGasto.Value.Date;

                var errorPago = RegistrarPago(context, gasto, datosPago.OrigenPago, datosPago.TipoPago, datosPago.ExtraDescripcionPago);
                if (errorPago != null)
                    return errorPago;

                context.SaveChanges();
                transaction.Commit();

                try
                {
                    GeneracionComprobanteGasto(context, gasto);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error generando PDF gasto pagado: " + ex.Message);
                }

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

                return Error($"Error al pagar gasto: {ex.Message}");
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
                    var errorPago = RegistrarPago(context, gasto, gastoDto.OrigenPago, gastoDto.TipoPago, gastoDto.ExtraDescripcionPago);
                    if (errorPago != null)
                        return errorPago;
                }

                context.SaveChanges();

                // =========================================================
                // ✅ 7. COMMIT
                // =========================================================

                transaction.Commit();

                try
                {
                    GeneracionComprobanteGasto(context, gasto);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error generando PDF gasto: " + ex.Message);
                }

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
                    Detalle = g.Detalle,
                    OrigenPago = g.OrigenPago
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
            // 📅 DETECCIÓN DEL FILTRO MANUAL DE FECHAS
            // =========================================================

            bool usaFechas =
                filtros.FechaDesde.HasValue ||
                filtros.FechaHasta.HasValue;

            bool hayFiltroManual =
                usaFechas &&
                filtros.Filtro3 != null &&
                int.TryParse(filtros.Filtro3.ToString(), out _);

            // =========================================================
            // CORE: ANULADOS + HISTORICO
            // =========================================================

            int estadoFiltro = 0;

            bool tieneFiltroEstado =
                filtros.Filtro2 != null &&
                int.TryParse(filtros.Filtro2.ToString(), out estadoFiltro);

            if (filtros.Bool2)
            {
                // HISTORICO
                // Se mantiene el comportamiento actual.
                // No se excluyen los anulados en esta rama.
            }
            else if (filtros.Bool1)
            {
                // SOLO ANULADOS
                query = query.Where(g =>
                    g.EstadoGasto == (int)EstadoGasto.Anulado);
            }
            else if (!tieneFiltroEstado)
            {
                // =====================================================
                // SIN ESTADO SELECCIONADO
                // =====================================================

                // Excluir anulados.
                query = query.Where(g =>
                    g.EstadoGasto != (int)EstadoGasto.Anulado);

                // Aplicar el filtro predeterminado del último mes
                // únicamente cuando NO hay un filtro manual de fechas.
                if (!hayFiltroManual)
                {
                    var desde = DateTime.Now.AddMonths(-1);

                    query = query.Where(g =>
                        g.EstadoGasto == (int)EstadoGasto.Pendiente
                        ||
                        (
                            g.FechaGasto.HasValue &&
                            g.FechaGasto.Value >= desde
                        ));
                }
            }

            // =========================================================
            // BÚSQUEDA
            // =========================================================

            if (!string.IsNullOrWhiteSpace(filtros.TextoBuscar))
            {
                var texto = filtros.TextoBuscar.Trim();

                switch (filtros.Filtro1?.ToString())
                {
                    case "NumeroGasto":

                        query = query.Where(g =>
                            g.NumeroGasto != null &&
                            EF.Functions.Collate(
                                g.NumeroGasto,
                                collation
                            ).Contains(texto));

                        break;

                    case "NombreEmpleado":

                        query = query.Where(g =>
                            g.Empleado != null &&
                            g.Empleado.Persona != null &&
                            (
                                (
                                    g.Empleado.Persona.Nombre != null &&
                                    EF.Functions.Collate(
                                        g.Empleado.Persona.Nombre,
                                        collation
                                    ).Contains(texto)
                                )
                                ||
                                (
                                    g.Empleado.Persona.Apellido != null &&
                                    EF.Functions.Collate(
                                        g.Empleado.Persona.Apellido,
                                        collation
                                    ).Contains(texto)
                                )
                            ));

                        break;

                    case "CategoriaGasto":

                        var textoBusqueda = texto.ToLower();

                        var categorias = Enum
                            .GetValues(typeof(CategoriaGasto))
                            .Cast<CategoriaGasto>()
                            .Where(c =>
                                c.ToString()
                                    .ToLower()
                                    .Contains(textoBusqueda))
                            .Select(c => (int)c)
                            .ToList();

                        if (categorias.Any())
                        {
                            query = query.Where(g =>
                                categorias.Contains(g.CategoriaGasto));
                        }
                        else
                        {
                            query = query.Where(g => false);
                        }

                        break;

                    default:

                        query = query.Where(g =>
                            (
                                g.NumeroGasto != null &&
                                EF.Functions.Collate(
                                    g.NumeroGasto,
                                    collation
                                ).Contains(texto)
                            )
                            ||
                            (
                                g.Detalle != null &&
                                EF.Functions.Collate(
                                    g.Detalle,
                                    collation
                                ).Contains(texto)
                            ));

                        break;
                }
            }

            // =========================================================
            // FILTRO ESTADO (cbx2)
            // =========================================================

            if (tieneFiltroEstado)
            {
                query = query.Where(g =>
                    g.EstadoGasto == estadoFiltro);
            }

            // =========================================================
            // FILTRO FECHAS
            // =========================================================

            if (hayFiltroManual)
            {
                int tipoFecha = int.Parse(filtros.Filtro3.ToString());

                switch ((TipoFiltroFechaGasto)tipoFecha)
                {
                    case TipoFiltroFechaGasto.FechaGasto:

                        if (filtros.FechaDesde.HasValue)
                        {
                            query = query.Where(g =>
                                g.FechaGasto.HasValue &&
                                g.FechaGasto.Value >= filtros.FechaDesde.Value);
                        }

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
                        {
                            query = query.Where(g =>
                                g.FechaRegistro >= filtros.FechaDesde.Value);
                        }

                        if (filtros.FechaHasta.HasValue)
                        {
                            var hasta = filtros.FechaHasta.Value.AddDays(1);

                            query = query.Where(g =>
                                g.FechaRegistro < hasta);
                        }

                        break;
                }
            }
            else if (filtros.Bool2)
            {
                // HISTORICO = últimos 6 meses,
                // siempre que no haya un filtro manual de fechas.

                var desde = DateTime.Now.AddMonths(-6);

                query = query.Where(g =>
                    (
                        g.FechaGasto.HasValue &&
                        g.FechaGasto.Value >= desde
                    )
                    ||
                    g.FechaRegistro >= desde);
            }

            // =========================================================
            // TOTAL
            // =========================================================

            var total = query.Count();

            // =========================================================
            // PAGINACIÓN
            // =========================================================

            var totalPaginas =
                (int)Math.Ceiling((double)total / filtros.PageSize);

            if (totalPaginas <= 0)
                totalPaginas = 1;

            if (filtros.Page > totalPaginas)
                filtros.Page = totalPaginas;

            if (filtros.Page < 1)
                filtros.Page = 1;

            // =========================================================
            // ORDEN
            // =========================================================

            query = query
                .OrderBy(g =>
                    g.EstadoGasto == (int)EstadoGasto.Pagado)
                .ThenByDescending(g =>
                    g.FechaGasto ?? g.FechaRegistro)
                .ThenByDescending(g =>
                    g.FechaRegistro);

            // =========================================================
            // DATA
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
            // RESULTADO
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
