using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.Helpers.VentaEnum;
using Servicios.LogicaNegocio.Articulo.Marca.DTO;
using Servicios.LogicaNegocio.Cliente.DTO;
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

            try
            {
                // 🔥 1. Tipo de movimiento (Ingreso / Egreso)
                var esEgreso = estado == (int)EstadoVenta.CancelacionVenta;

                var tipoMovimiento = esEgreso
                    ? TipoMovimiento.Egreso
                    : TipoMovimiento.Ingreso;

                // 🔥 2. Prefijo de operación
                var prefijoOperacion = esEgreso ? "CAN" : "MOV";

                // 🔥 3. Prefijo de entidad
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

                // 🔥 4. Número de movimiento
                var numeroMovimiento = $"{prefijoOperacion}-{prefijoEntidad}-{ventaId}-{DateTime.Now:yyyyMMddHHmmss}";

                // 🔥 5. Crear movimiento
                var movimiento = new AccesoDatos.Entidades.Movimiento
                {
                    NumeroMovimiento = numeroMovimiento,
                    EntidadId = ventaId,
                    TipoEntidad = (int)tipoEntidad,
                    TipoMovimiento = (int)tipoMovimiento,
                    TipoMovimientoDetalle = (int)detalleTipo,
                    Monto = Math.Abs(monto), // siempre positivo
                    FechaMovimiento = DateTime.Now,
                    EstaEliminado = false
                };

                context.Movimientos.Add(movimiento);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear movimiento de venta: {ex}");
                throw;
            }
        }

        public void CrearMovimientoCtaCte(decimal total, long cajaId, long cuentaCorrienteId, TipoMovimientoDetalle detalleTipo, GestorContextDB context = null)
        {
            //el return del id lo puse para caja pero al fnal no se usa, podria no devolver nada

            bool crearContextoLocal = (context == null);

            if (crearContextoLocal)
                context = new GestorContextDBFactory().CreateDbContext(null);

            try
            {
                var movimiento = new AccesoDatos.Entidades.Movimiento
                {
                    NumeroMovimiento = $"MOV{total}CTACTE",
                    TipoMovimiento = (int)TipoMovimiento.Ingreso,// tiene los dos ingeso y egreso?
                    TipoMovimientoDetalle = (int)detalleTipo,
                    Monto = total,
                    FechaMovimiento = DateTime.Now,
                    EstaEliminado = false,
                    EntidadId = cuentaCorrienteId,
                    TipoEntidad = (int)TipoEntidadMovimiento.CuentaCorriente
                };

                context.Movimientos.Add(movimiento);

                //Si el contexto es local, guardamos los cambios directamente
                if (crearContextoLocal)
                    context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear movimiento: {ex.Message}");
                throw; // Re-lanzamos para que el servicio que lo llame lo maneje
            }
            finally
            {
                if (crearContextoLocal)
                    context.Dispose();
            }
        }


        public MovimientoDTO ObtenerMovimientoPorId(long movimientoId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var movimiento = context.Movimientos
                .FirstOrDefault(m => m.MovimientoId == movimientoId && !m.EstaEliminado);

            if (movimiento == null)
                return null;

            return new MovimientoDTO
            {
                MovimientoId = movimiento.MovimientoId,
                NumeroMovimiento = movimiento.NumeroMovimiento,
                EntidadId = movimiento.EntidadId,
                TipoEntidad = movimiento.TipoEntidad,
                TipoMovimiento = movimiento.TipoMovimiento,
                TipoMovimientoDetalle = movimiento.TipoMovimientoDetalle,
                Monto = movimiento.Monto,
                FechaMovimiento = movimiento.FechaMovimiento,
                EstaEliminado = movimiento.EstaEliminado
            };
        }
        public void CrearMovimientoGasto(
    long gastoId,
    decimal monto,
    TipoMovimientoDetalle detalleTipo,
    GestorContextDB context)
        {
            if (gastoId <= 0)
                throw new Exception("El movimiento no puede crearse porque el gasto no tiene un ID válido.");

            try
            {
                var numeroMovimiento = $"MOV-GASTO-{gastoId}-{DateTime.Now:yyyyMMddHHmmss}";

                var movimiento = new AccesoDatos.Entidades.Movimiento
                {
                    NumeroMovimiento = numeroMovimiento,
                    EntidadId = gastoId,
                    TipoEntidad = (int)TipoEntidadMovimiento.Gasto,
                    TipoMovimiento = (int)TipoMovimiento.Egreso, // 🔴 importante
                    TipoMovimientoDetalle = (int)detalleTipo,
                    Monto = monto,
                    FechaMovimiento = DateTime.Now,
                    EstaEliminado = false
                };

                context.Movimientos.Add(movimiento);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear movimiento de gasto: {ex}");
                throw;
            }
        }
        public ResultadoPaginacion<MovimientoDTO> ObtenerMovimientos(FiltroConsulta filtros)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.Movimientos
                .AsNoTracking()
                .AsQueryable();

            // 🔴 Eliminados
            query = filtros.VerEliminados
                ? query.Where(x => x.EstaEliminado)
                : query.Where(x => !x.EstaEliminado);

            // 🔍 TEXTO
            if (!string.IsNullOrWhiteSpace(filtros.TextoBuscar))
            {
                var texto = filtros.TextoBuscar;

                switch (filtros.Extra?.ToString())
                {
                    case "NumeroMovimiento":
                        query = query.Where(x => x.NumeroMovimiento.Contains(texto));
                        break;

                    default:
                        query = query.Where(x => x.NumeroMovimiento.Contains(texto));
                        break;
                }
            }

            // 🔴 EXTRA2 → puede ser FECHA o TIPO MOVIMIENTO
            TipoMovimiento? tipoMovimiento = null;
            bool filtrarPorFechaMovimiento = false;

            if (filtros.Extra2 != null)
            {
                var valor = filtros.Extra2.ToString();

                // 📅 filtro fecha
                if (valor == "FM")
                {
                    filtrarPorFechaMovimiento = true;
                }

                // 🔢 tipo movimiento
                if (int.TryParse(valor, out var tipo))
                {
                    if (Enum.IsDefined(typeof(TipoMovimiento), tipo))
                        tipoMovimiento = (TipoMovimiento)tipo;
                }
            }

            // 📅 FILTRO FECHA (solo si eligió "Fecha Movimiento")
            if (filtrarPorFechaMovimiento)
            {
                if (filtros.FechaDesde.HasValue)
                    query = query.Where(x => x.FechaMovimiento >= filtros.FechaDesde.Value);

                if (filtros.FechaHasta.HasValue)
                {
                    var hastaReal = filtros.FechaHasta.Value.AddDays(1);
                    query = query.Where(x => x.FechaMovimiento < hastaReal);
                }
            }

            // 🔴 FILTRO TIPO (Ingresos / Egresos)
            if (tipoMovimiento.HasValue)
            {
                query = query.Where(x => x.TipoMovimiento == (int)tipoMovimiento.Value);
            }

            // 📊 TOTAL
            var total = query.Count();

            // 🔴 CONTROL PAGINACION
            var totalPaginas = (int)Math.Ceiling((double)total / filtros.PageSize);
            if (totalPaginas == 0) totalPaginas = 1;

            if (filtros.Page > totalPaginas)
                filtros.Page = totalPaginas;

            if (filtros.Page < 1)
                filtros.Page = 1;

            // 📌 ORDEN (correcto para movimientos)
            query = query.OrderByDescending(x => x.FechaMovimiento);

            // 📄 DATA
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

            return new ResultadoPaginacion<MovimientoDTO>
            {
                Items = data,
                TotalRegistros = total,
                Page = filtros.Page,
                PageSize = filtros.PageSize
            };
        }
        public MovimientoHelperDTO ObtenerDatosParaMovimientoConsultaVenta(long movimientoId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            var movimientoService = new MovimientoServicio();

            var movimiento = context.Movimientos
            .AsNoTracking()
            .Where(m => m.MovimientoId == movimientoId)
            .Select(m => new MovimientoHelperDTO
            {
                MovimientoId = m.MovimientoId,
                NumeroMovimiento = m.NumeroMovimiento,
                Monto = m.Monto,
                TipoMovimiento = m.TipoMovimiento,
                TipoMovimientoDetalle = m.TipoMovimientoDetalle,
                FechaMovimiento = m.FechaMovimiento,
                EstaEliminado = m.EstaEliminado,
                EntidadId = m.EntidadId,
                TipoEntidad = m.TipoEntidad,

                Venta = m.TipoEntidad == (int)TipoEntidadMovimiento.Venta
                    ? context.Ventas
                        .Where(v => v.VentaId == m.EntidadId)
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
                                // Mapeamos los precios según tus entidades de DetallesVenta
                                PrecioVenta = i.PrecioUnitarioOriginal,
                                PrecioOferta = i.PrecioUnitarioFinal,
                                Descripcion = i.Descripcion,
                                EsOferta = i.EsOferta,
                                EsOfertaPorGrupo = i.EsOfertaPorGrupo
                                // Nota: Si 'Medida' o 'UnidadMedida' están en Producto, 
                                // deberías acceder via i.Producto.Medida si tienes el Include o la relación.
                            }).ToList()
                        }).FirstOrDefault()
                    : null
            })
            .FirstOrDefault();

            return movimiento;
        }

        public MovimientoHelperDTO ObtenerDatosParaMovimientoConsultaGasto(long movimientoId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var movimiento = context.Movimientos
                .AsNoTracking()
                .Where(m => m.MovimientoId == movimientoId)
                .Select(m => new MovimientoHelperDTO
                {
                    MovimientoId = m.MovimientoId,
                    NumeroMovimiento = m.NumeroMovimiento,
                    Monto = m.Monto,
                    TipoMovimiento = m.TipoMovimiento,
                    TipoMovimientoDetalle = m.TipoMovimientoDetalle,
                    FechaMovimiento = m.FechaMovimiento,
                    EstaEliminado = m.EstaEliminado,
                    EntidadId = m.EntidadId,
                    TipoEntidad = m.TipoEntidad,

                    // Mapeo dinámico según el DTO proporcionado
                    Gasto = m.TipoEntidad == (int)TipoEntidadMovimiento.Gasto && m.EntidadId.HasValue
                        ? context.Gastos
                            .Where(g => g.GastoId == m.EntidadId.Value)
                            .Select(g => new GastoDTO
                            {
                                GastoId = g.GastoId,
                                NumeroGasto = g.NumeroGasto, // Nueva propiedad
                                IdEmpleado = g.IdEmpleado,
                                NombreEmpleado = $"{g.Empleado.Persona.Nombre} {g.Empleado.Persona.Apellido}" ?? "NO NAME",
                                CategoriaGasto = g.CategoriaGasto,
                                FechaGasto = g.FechaGasto,
                                FechaRegistro = g.FechaRegistro,
                                MontoTotal = g.MontoTotal, // Cambio de g.mon a g.MontoTotal
                                MontoPagado = g.MontoPagado,
                                EstadoGasto = g.EstadoGasto,
                                Detalle = g.Detalle ?? "Sin Detalle"
                            }).FirstOrDefault()
                        : null
                })
                .FirstOrDefault();

            return movimiento;
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
                                    EsOfertaPorGrupo = i.EsOfertaPorGrupo
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
                        : null
                })
                .FirstOrDefault();

            return movimiento;
        }
    }
}
