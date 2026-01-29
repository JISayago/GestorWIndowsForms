using AccesoDatos;
using AccesoDatos.Entidades;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Gasto.DTO;
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

        public EstadoOperacion NuevoGasto(GastoDTO gastoDto)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            // 1. Validaciones básicas
            if (gastoDto == null)
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Datos de gasto inválidos."
                };

            if (gastoDto.MontoTotal <= 0)
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "El monto del gasto debe ser mayor a cero."
                };

            // 2. Validar empleado (solo existencia)
            var existeEmpleado = context.Empleados.Any(e => e.PersonaId == gastoDto.IdEmpleado);
            if (!existeEmpleado)
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "El empleado indicado no existe."
                };

            // 3. (Opcional) validar número de gasto si querés evitar duplicados
            if (!string.IsNullOrEmpty(gastoDto.NumeroGasto) &&
                context.Gastos.Any(g => g.NumeroGasto == gastoDto.NumeroGasto))
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe un gasto con el mismo número."
                };
            }
            // 4. Crear entidad Gasto y generacion de numeracion
            var fechaGasto = gastoDto.FechaGasto.Date;

            var cantidadDelDia = context.Gastos
                .Count(g => g.FechaGasto.Date == fechaGasto);

            gastoDto.NumeroGasto = GeneradorNumeroComprobante.Generar("GAS",fechaGasto,cantidadDelDia );

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


            // 5. Reglas simples de consistencia
            if (gasto.MontoPagado > gasto.MontoTotal)
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "El monto pagado no puede ser mayor al monto total."
                };

            context.Gastos.Add(gasto);
            context.SaveChanges();

            // 6. Resultado
            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Gasto registrado correctamente.",
                EntidadId = gasto.GastoId
            };
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
                query = query.Where(g => g.EstadoGasto == estadoGasto.Value);

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
