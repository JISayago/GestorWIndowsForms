using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.Helpers.VentaEnum;
using Servicios.LogicaNegocio.Articulo.Marca.DTO;
using Servicios.LogicaNegocio.Cliente.DTO;
using Servicios.LogicaNegocio.CuentaCorriente.DTO;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.Empleado.DTO;
using Servicios.LogicaNegocio.Gasto.DTO;
using Servicios.LogicaNegocio.Movimiento.DTO;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Producto.Rubro.DTO;
using Servicios.LogicaNegocio.Venta;
using Servicios.LogicaNegocio.Venta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Servicios.LogicaNegocio.Movimiento
{
    public class MovimientoServicio : IMovimientoServicio
    {
        // =====================================================================
        // ALTA DE MOVIMIENTOS (punto único)
        // Reglas: monto siempre positivo, número único, CajaId solo si AfectaCaja,
        // MovimientoOrigenId solo en reversas. No abre transacciones ni guarda:
        // el que llama es dueño del contexto, de la transacción y de SaveChanges.
        // =====================================================================

        public AccesoDatos.Entidades.Movimiento? Registrar(GestorContextDB context, NuevoMovimiento datos)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (datos == null) throw new ArgumentNullException(nameof(datos));

            if (string.IsNullOrWhiteSpace(datos.Prefijo))
                throw new ArgumentException("El movimiento necesita un prefijo para generar su número.", nameof(datos));

            if (datos.EntidadId <= 0)
                throw new Exception("El movimiento no puede crearse porque la entidad de origen no tiene un ID válido.");

            // Un movimiento de monto cero no aporta nada al libro: no se crea.
            var monto = Math.Abs(datos.Monto);
            if (monto == 0)
                return null;

            if (datos.AfectaCaja && !datos.CajaId.HasValue)
                throw new Exception("Un movimiento que afecta la caja debe indicar la caja.");

            if (!datos.AfectaCaja && datos.CajaId.HasValue)
                throw new Exception("Un movimiento que no afecta la caja no debe indicar una caja.");

            if (datos.MovimientoOrigenId.HasValue)
                ValidarReversa(context, datos);

            var movimiento = new AccesoDatos.Entidades.Movimiento
            {
                NumeroMovimiento = GenerarNumero(context, datos.Prefijo),
                TipoMovimiento = (int)datos.Tipo,
                TipoMovimientoDetalle = (int)datos.Detalle,
                Monto = monto,
                FechaMovimiento = DateTime.Now,
                EstaEliminado = false,
                TipoEntidad = (int)datos.Entidad,
                EntidadId = datos.EntidadId,
                AfectaCaja = datos.AfectaCaja,
                CajaId = datos.CajaId,
                TipoPagoId = datos.TipoPagoId,
                MovimientoOrigenId = datos.MovimientoOrigenId
            };

            context.Movimientos.Add(movimiento);
            return movimiento;
        }

        private static void ValidarReversa(GestorContextDB context, NuevoMovimiento datos)
        {
            var origenId = datos.MovimientoOrigenId!.Value;

            var original = context.Movimientos.FirstOrDefault(m => m.MovimientoId == origenId);

            if (original == null)
                throw new Exception("El movimiento que se quiere revertir no existe.");

            if (original.MovimientoOrigenId.HasValue)
                throw new Exception("No se puede revertir un movimiento que ya es una reversa.");

            if (original.TipoMovimiento == (int)datos.Tipo)
                throw new Exception("Una reversa debe ser del tipo contrario al movimiento original.");

            var yaRevertido =
                context.Movimientos.Any(m => m.MovimientoOrigenId == origenId) ||
                context.ChangeTracker.Entries<AccesoDatos.Entidades.Movimiento>()
                    .Any(e => e.State == EntityState.Added && e.Entity.MovimientoOrigenId == origenId);

            if (yaRevertido)
                throw new Exception("El movimiento ya fue revertido.");
        }

        /// <summary>
        /// Número único: PREFIJO-yyyyMMdd-000001. Cuenta lo ya guardado del día y lo agregado
        /// al contexto sin guardar, así varios movimientos de una misma operación no se pisan.
        /// </summary>
        private static string GenerarNumero(GestorContextDB context, string prefijo)
        {
            var baseNumero = $"{prefijo}-{DateTime.Now:yyyyMMdd}-";

            var guardados = context.Movimientos
                .Count(m => m.NumeroMovimiento.StartsWith(baseNumero));

            var pendientes = context.ChangeTracker.Entries<AccesoDatos.Entidades.Movimiento>()
                .Count(e => e.State == EntityState.Added &&
                            e.Entity.NumeroMovimiento.StartsWith(baseNumero, StringComparison.Ordinal));

            return $"{baseNumero}{guardados + pendientes + 1:D6}";
        }

        // =====================================================================
        // CREADORES ACTUALES (se reemplazan por métodos por caso en la Etapa 3).
        // Mismo comportamiento de hoy, pero pasando por Registrar.
        // =====================================================================

        public void CrearMovimientoVenta(
            long ventaId,
            decimal monto,
            int estado,
            TipoMovimientoDetalle detalleTipo,
            TipoEntidadMovimiento tipoEntidad,
            GestorContextDB context)
        {
            if (ventaId <= 0)
                throw new Exception("El movimiento no puede crearse porque la venta no tiene un ID válido.");

            var esEgreso = estado == (int)EstadoVenta.CancelacionVenta;

            string prefijoEntidad;

            switch (tipoEntidad)
            {
                case TipoEntidadMovimiento.Venta:
                    prefijoEntidad = "VENTA";
                    break;

                case TipoEntidadMovimiento.VentaLibre:
                    prefijoEntidad = "VENTALIBRE";
                    break;

                default:
                    throw new Exception("Tipo de entidad no válido para movimientos de venta.");
            }

            Registrar(context, new NuevoMovimiento
            {
                Prefijo = $"{(esEgreso ? "CAN" : "MOV")}-{prefijoEntidad}",
                Tipo = esEgreso ? TipoMovimiento.Egreso : TipoMovimiento.Ingreso,
                Detalle = detalleTipo,
                Entidad = tipoEntidad,
                EntidadId = ventaId,
                Monto = monto
            });
        }

        public void CrearMovimientoCtaCte(decimal total, long cajaId, long cuentaCorrienteId, TipoMovimientoDetalle detalleTipo, bool esPago, GestorContextDB context)
        {
            // cajaId no se usa todavía: los movimientos de cuenta corriente no afectan la caja.
            // Ingreso = pago/crédito de cuenta corriente, Egreso = compra con cuenta corriente.
            Registrar(context, new NuevoMovimiento
            {
                Prefijo = "MOV-CTACTE",
                Tipo = esPago ? TipoMovimiento.Ingreso : TipoMovimiento.Egreso,
                Detalle = detalleTipo,
                Entidad = TipoEntidadMovimiento.CuentaCorriente,
                EntidadId = cuentaCorrienteId,
                Monto = total
            });
        }

        public void CrearMovimientoGasto(
            long gastoId,
            decimal monto,
            TipoMovimientoDetalle detalleTipo,
            GestorContextDB context)
        {
            if (gastoId <= 0)
                throw new Exception("El movimiento no puede crearse porque el gasto no tiene un ID válido.");

            Registrar(context, new NuevoMovimiento
            {
                Prefijo = "MOV-GASTO",
                Tipo = TipoMovimiento.Egreso,
                Detalle = detalleTipo,
                Entidad = TipoEntidadMovimiento.Gasto,
                EntidadId = gastoId,
                Monto = monto
            });
        }

        public ResultadoPaginacion<MovimientoDTO> ObtenerMovimientos(FiltroConsulta filtros)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            string collation = "Latin1_General_CI_AI";

            var query = context.Movimientos
                .AsNoTracking()
                .AsQueryable();

            // =========================================================
            // 📅 DETECCIÓN DEL FILTRO MANUAL DE FECHAS
            // =========================================================

            bool usaFechas =
                filtros.FechaDesde.HasValue ||
                filtros.FechaHasta.HasValue;

            bool hayFiltroManual =
                usaFechas &&
                filtros.Filtro3?.ToString() == "FECHA";

            // =========================================================
            // CORE: ELIMINADOS + HISTORICO
            // =========================================================

            if (filtros.Bool2)
            {
                // HISTORICO
                // Trae eliminados y no eliminados.
                // La ventana de 6 meses se aplica más abajo (sección FECHAS),
                // siempre que no haya un filtro manual de fechas.
            }
            else if (filtros.Bool1)
            {
                // SOLO ELIMINADOS
                // Igual que "solo anulados" en gastos: sin ventana de fecha,
                // se ven todos los eliminados salvo que haya filtro manual.
                query = query.Where(x =>
                    x.EstaEliminado);
            }
            else
            {
                // =====================================================
                // NORMAL
                // =====================================================

                // Excluir eliminados.
                query = query.Where(x =>
                    !x.EstaEliminado);

                // Ventana predeterminada de 2 meses
                // únicamente cuando NO hay un filtro manual de fechas.
                if (!hayFiltroManual)
                {
                    var desde = DateTime.Now.AddMonths(-2);

                    query = query.Where(x =>
                        x.FechaMovimiento >= desde);
                }
            }

            // =========================================================
            // BÚSQUEDA (sin distinguir mayúsculas ni acentos)
            // =========================================================

            if (!string.IsNullOrWhiteSpace(filtros.TextoBuscar))
            {
                var texto = filtros.TextoBuscar.Trim();

                query = query.Where(x =>
                    x.NumeroMovimiento != null &&
                    EF.Functions.Collate(
                        x.NumeroMovimiento,
                        collation
                    ).Contains(texto));
            }

            // =========================================================
            // TIPO MOVIMIENTO / DETALLE (cbx2)
            // =========================================================

            var filtroTipo = filtros.Filtro2?.ToString();

            if (!string.IsNullOrWhiteSpace(filtroTipo))
            {
                if (filtroTipo.StartsWith("TM_") &&
                    int.TryParse(filtroTipo.Substring(3), out var tipo))
                {
                    query = query.Where(x =>
                        x.TipoMovimiento == tipo);
                }
                else if (filtroTipo.StartsWith("TMD_") &&
                         int.TryParse(filtroTipo.Substring(4), out var detalle))
                {
                    query = query.Where(x =>
                        x.TipoMovimientoDetalle == detalle);
                }
            }

            // =========================================================
            // FILTRO FECHAS
            // =========================================================

            if (hayFiltroManual)
            {
                if (filtros.FechaDesde.HasValue)
                {
                    var desde = filtros.FechaDesde.Value.Date;

                    query = query.Where(x =>
                        x.FechaMovimiento >= desde);
                }

                if (filtros.FechaHasta.HasValue)
                {
                    var hasta = filtros.FechaHasta.Value.Date.AddDays(1);

                    query = query.Where(x =>
                        x.FechaMovimiento < hasta);
                }
            }
            else if (filtros.Bool2)
            {
                // HISTORICO = últimos 6 meses,
                // siempre que no haya un filtro manual de fechas.

                var desde = DateTime.Now.AddMonths(-6);

                query = query.Where(x =>
                    x.FechaMovimiento >= desde);
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

            // El desempate por id mantiene estable la paginación
            // cuando hay varios movimientos con la misma fecha.
            query = query
                .OrderByDescending(x => x.FechaMovimiento)
                .ThenByDescending(x => x.MovimientoId);

            // =========================================================
            // DATA
            // =========================================================

            var data = query
                .Skip((filtros.Page - 1) * filtros.PageSize)
                .Take(filtros.PageSize)
                .Select(x => new MovimientoDTO
                {
                    MovimientoId = x.MovimientoId,

                    NumeroMovimiento = x.NumeroMovimiento,

                    TipoMovimiento = x.TipoMovimiento,

                    TipoMovimientoDetalle = x.TipoMovimientoDetalle,

                    Monto = x.Monto,

                    FechaMovimiento = x.FechaMovimiento,

                    EstaEliminado = x.EstaEliminado,

                    EntidadId = x.EntidadId,

                    TipoEntidad = x.TipoEntidad
                })
                .ToList();

            // =========================================================
            // RESULTADO
            // =========================================================

            return new ResultadoPaginacion<MovimientoDTO>
            {
                Items = data,
                TotalRegistros = total,
                Page = filtros.Page,
                PageSize = filtros.PageSize
            };
        }
        public MovimientoHelperDTO ObtenerDatosParaMovimientoConsulta(long movimientoId)
        {
            // Usamos 'using' para asegurar que la conexión se libere correctamente
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var movimiento = context.Movimientos
                .AsNoTracking()
                .Where(m => m.MovimientoId == movimientoId)
                .Select(m => new MovimientoHelperDTO
                {
                    // --- 1. PROPIEDADES BASE DEL MOVIMIENTO ---
                    MovimientoId = m.MovimientoId,
                    NumeroMovimiento = m.NumeroMovimiento,
                    Monto = m.Monto,
                    TipoMovimiento = m.TipoMovimiento,
                    TipoMovimientoDetalle = m.TipoMovimientoDetalle,
                    FechaMovimiento = m.FechaMovimiento,
                    EstaEliminado = m.EstaEliminado,
                    EntidadId = m.EntidadId,
                    TipoEntidad = m.TipoEntidad,

                    // --- 2. MAPEO CONDICIONAL PARA VENTA ---
                    Venta = m.TipoEntidad == (int)TipoEntidadMovimiento.Venta && m.EntidadId.HasValue
                        ? context.Ventas
                            .Where(v => v.VentaId == m.EntidadId.Value)
                            .Select(v => new VentaDTO
                            {
                                VentaId = v.VentaId,
                                NumeroVenta = v.NumeroVenta,
                                FechaVenta = v.FechaVenta,
                                Total = v.Total,
                                TotalSinDescuento = v.TotalSinDescuento,
                                Descuento = v.Descuento,
                                IdCliente = v.IdCliente,
                                Estado = v.Estado,
                                Detalle = v.Detalle,
                                Items = v.DetallesVentas.Select(i => new ItemVentaDTO
                                {
                                    ItemId = i.DetalleVentaId,
                                    Cantidad = i.Cantidad,
                                    PrecioVenta = i.PrecioUnitarioOriginal,
                                    PrecioOferta = i.PrecioUnitarioFinal,
                                    Descripcion = i.Descripcion,
                                    EsOferta = i.EsOferta,
                                    //EsOfertaPorGrupo = i.EsOfertaPorGrupo
                                }).ToList()
                            }).FirstOrDefault()
                        : null,

                    // --- 3. MAPEO CONDICIONAL PARA GASTO ---
                    Gasto = m.TipoEntidad == (int)TipoEntidadMovimiento.Gasto && m.EntidadId.HasValue
                        ? context.Gastos
                            .Where(g => g.GastoId == m.EntidadId.Value)
                            .Select(g => new GastoDTO
                            {
                                GastoId = g.GastoId,
                                NumeroGasto = g.NumeroGasto,
                                IdEmpleado = g.IdEmpleado,
                                // Concatenación segura para EF Core
                                NombreEmpleado = g.Empleado.Persona.Nombre + " " + g.Empleado.Persona.Apellido ?? "NO NAME",
                                CategoriaGasto = g.CategoriaGasto,
                                FechaGasto = g.FechaGasto,
                                FechaRegistro = g.FechaRegistro,
                                MontoTotal = g.MontoTotal,
                                MontoPagado = g.MontoPagado,
                                EstadoGasto = g.EstadoGasto,
                                Detalle = g.Detalle ?? "Sin Detalle"
                            }).FirstOrDefault()
                        : null,

                    // --- 4. MAPEO CONDICIONAL PARA CUENTA CORRIENTE ---
                    CuentaCorriente = m.TipoEntidad == (int)TipoEntidadMovimiento.CuentaCorriente && m.EntidadId.HasValue
                    ? context.CuentaCorriente
                        .Where(cc => cc.CuentaCorrienteId == m.EntidadId.Value)
                        .Select(cc => new CuentaCorrienteDTO
                        {
                        CuentaCorrienteId = cc.CuentaCorrienteId,
                        NombreCuentaCorriente = cc.NombreCuentaCorriente,
                        Saldo = cc.Saldo,
                        LimiteDeuda = cc.LimiteDeuda,
                        EstaEliminado = cc.EstaEliminado,
                        LimiteDeudaActivo = cc.LimiteDeudaActivo,
                        FechaVencimiento = cc.FechaVencimiento,
                        FechaCreacion = cc.FechaCreacion,
                        FechaActivacion = cc.FechaActivacion,
                        ConDeuda = cc.ConDeuda,
                        EstadoCtaCte = cc.EstadoCuentaCorriente,
                        ClienteId = cc.ClienteId,
                        // Navegación hacia el nombre del cliente
                        NombreCliente = cc.Cliente.Persona.Nombre + " " + cc.Cliente.Persona.Apellido,
                        // Datos de contacto ya existentes en Persona/Cliente, sin mapear hasta ahora
                        NumeroCliente = cc.Cliente.NumeroCliente,
                        TelefonoCliente = cc.Cliente.Persona.Telefono,
                        EmailCliente = cc.Cliente.Persona.Email,
                        // Mapeo de DNI autorizados (asumiendo relación o lista)
                        DniAutorizados = cc.CuentaCorrienteAutorizado.Select(a => a.Dni.ToString()).ToList(),

                        // Tipo del movimiento padre (Ingreso/Egreso), para distinguir "Carga de
                        // saldo" de "Compra a cuenta" sin necesitar vincular la venta puntual.
                        TipoMovimientoPadre = m.TipoMovimiento,

                        // Últimos 5 movimientos de esta misma cuenta (sin el actual), para dar
                        // contexto temporal. Consulta correlacionada, no requiere nueva columna.
                        HistorialReciente = context.Movimientos
                            .Where(mh => mh.TipoEntidad == (int)TipoEntidadMovimiento.CuentaCorriente
                                      && mh.EntidadId == cc.CuentaCorrienteId
                                      && !mh.EstaEliminado
                                      && mh.MovimientoId != m.MovimientoId)
                            .OrderByDescending(mh => mh.FechaMovimiento)
                            .Take(5)
                            .Select(mh => new MovimientoResumenCtaCteDTO
                            {
                                NumeroMovimiento = mh.NumeroMovimiento,
                                FechaMovimiento = mh.FechaMovimiento,
                                Monto = mh.Monto,
                                TipoMovimiento = mh.TipoMovimiento
                            }).ToList()
                        }).FirstOrDefault()
                       : null

                })
                .FirstOrDefault();

            return movimiento;
        }
    }
}
