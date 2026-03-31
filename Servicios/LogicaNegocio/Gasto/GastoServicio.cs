using AccesoDatos;
using AccesoDatos.Entidades;
using Servicios.Helpers;
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

                // 2. Generar número
                var fechaGasto = gastoDto.FechaGasto.Date;

                var cantidadDelDia = context.Gastos
                    .Count(g => g.FechaGasto.Date == fechaGasto);

                gastoDto.NumeroGasto = GeneradorNumeroComprobante
                    .Generar("GAS", fechaGasto, cantidadDelDia);

                // 3. Crear entidad
                var gasto = new AccesoDatos.Entidades.Gasto
                {
                    NumeroGasto = gastoDto.NumeroGasto,
                    IdEmpleado = gastoDto.IdEmpleado,
                    CategoriaGasto = gastoDto.CategoriaGasto,
                    FechaGasto = fechaGasto,
                    FechaRegistro = DateTime.Now,
                    MontoTotal = gastoDto.MontoTotal,
                    MontoPagado = gastoDto.MontoPagado > 0
                        ? gastoDto.MontoPagado
                        : gastoDto.MontoTotal,
                    EstadoGasto = gastoDto.EstadoGasto,
                    Detalle = gastoDto.Detalle
                };

                if (gasto.MontoPagado > gasto.MontoTotal)
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "El monto pagado no puede ser mayor al monto total."
                    };

                // 4. Guardar gasto (para obtener ID)
                context.Gastos.Add(gasto);
                context.SaveChanges();

                // 🔥 ACÁ YA TENÉS gasto.GastoId

                // 5. Crear movimiento (MISMO CONTEXTO)
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

                // 6. Guardar movimiento
                context.SaveChanges();

                // 7. Commit
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


        public List<GastoDTO> ObtenerGastos(int? estadoGasto = null)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.Gastos.AsQueryable();

            if (estadoGasto.HasValue)
            {
                query = query.Where(g => g.EstadoGasto == estadoGasto.Value);
            }
            else
            {
                query = query.Where(g => g.EstadoGasto > 0);
            }

                var gastos = query
                    .OrderByDescending(g => g.FechaGasto)
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
                    .ToList();

            return gastos;
        }

        public List<GastoDTO> ObtenerGastosFiltrados(
      string textoBuscar = null,
      int? estadoGasto = null,
      DateTime? fechaDesde = null,
      DateTime? fechaHasta = null)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.Gastos.AsQueryable();

            // 🔍 filtro por texto
            if (!string.IsNullOrWhiteSpace(textoBuscar))
            {
                query = query.Where(g =>
                    g.Detalle.Contains(textoBuscar) ||
                    g.NumeroGasto.Contains(textoBuscar) ||
                    g.Empleado.Persona.Nombre.Contains(textoBuscar) ||
                    g.Empleado.Persona.Apellido.Contains(textoBuscar)
                );
            }

            // 🔥 filtro por estado (enum → int)
            if (estadoGasto.HasValue)
            {
                query = query.Where(g => g.EstadoGasto == estadoGasto.Value);
            }
            else
            {
                // comportamiento por defecto (como ya tenías)
                query = query.Where(g => g.EstadoGasto > 0);
            }

            // 📅 filtro por fechas
            if (fechaDesde.HasValue)
            {
                query = query.Where(g => g.FechaGasto >= fechaDesde.Value);
            }

            if (fechaHasta.HasValue)
            {
                var hastaReal = fechaHasta.Value.AddDays(1);
                query = query.Where(g => g.FechaGasto < hastaReal);
            }

            // 📊 resultado
            var gastos = query
                .OrderByDescending(g => g.FechaGasto)
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
                .ToList();

            return gastos;
        }

    }
}
