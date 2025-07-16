using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Empleado.DTO;
using Servicios.LogicaNegocio.TipoPago.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.TipoPago
{
    public class TipoPagoServicio : ITipoPagoServicio
    {
        public EstadoOperacion Eliminar(long tipoPagoId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);
            var tipoPagoAEliminar = context.TiposPago
                    .FirstOrDefault(x => x.TipoPagoId == tipoPagoId);

            if (tipoPagoAEliminar == null || tipoPagoAEliminar.EstaEliminado) throw new Exception($" No se encontro el Tipo de Pago: {tipoPagoAEliminar.Nombre}");

            tipoPagoAEliminar.EstaEliminado = true;

            context.SaveChanges();
            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = $"El Tipo Pago {tipoPagoAEliminar.Nombre} con el codigo: ({tipoPagoAEliminar.Codigo}) fue eliminado correctamente."
            };
        }

        public EstadoOperacion Insertar(TipoPagoDTO tipoPagoDto)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            if (context.TiposPago.Any(p => p.Codigo == tipoPagoDto.Codigo))
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe un Tipo de Pago con el mismo Código."
                };

            var tipoPago = new AccesoDatos.Entidades.TipoPago
            {
                Nombre = tipoPagoDto.Nombre,
                Detalle = tipoPagoDto.Detalle,
                Codigo = tipoPagoDto.Codigo,
                EstaEliminado = false,
            };

            context.TiposPago.Add(tipoPago);
            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Tipo de Pago creado correctamente.",
                EntidadId = tipoPago.TipoPagoId
            };
        }

        public EstadoOperacion Modificar(TipoPagoDTO tipoPagoDto, long? tipoPagoId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            var tipoPagoEditar = context.TiposPago
                .FirstOrDefault(x => x.TipoPagoId == tipoPagoId);

            if (tipoPagoEditar == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Tipo de Pago no encontrado."
                };
            }

            bool codigoDuplicado = context.TiposPago
                .Any(tp => tp.Codigo == tipoPagoDto.Codigo && tp.TipoPagoId != tipoPagoEditar.TipoPagoId);

            if (codigoDuplicado)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe un Tipo de Pago con el mismo Código."
                };
            }

            tipoPagoEditar.Nombre = tipoPagoDto.Nombre;
            tipoPagoEditar.Codigo = tipoPagoDto.Codigo;
            tipoPagoEditar.Detalle = tipoPagoDto.Detalle;
            tipoPagoEditar.EstaEliminado = tipoPagoDto.EstaEliminado;
            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Tipo de Pago modificado correctamente.",
                EntidadId = tipoPagoDto.TipoPagoId
            };
        }

        public TipoPagoDTO ObtenerTipoPagoPorId(long tipoPagoId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var tipoPago = context.TiposPago
                 .AsNoTracking()
                 .Where(tp => tp.TipoPagoId == tipoPagoId && !tp.EstaEliminado)
                 .Select(tp => new TipoPagoDTO
                 {
                     TipoPagoId = tp.TipoPagoId,
                     Nombre = tp.Nombre,
                     Codigo = tp.Codigo,
                     Detalle= tp.Detalle,
                     EstaEliminado= tp.EstaEliminado,
                 })
                 .FirstOrDefault();
            return tipoPago;
        }

        public IEnumerable<TipoPagoDTO> ObtenerTipoPagos(string cadenabuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var tipoPagos = context.TiposPago
            .AsNoTracking()
            .Where(tp => !tp.EstaEliminado &&
                (tp.Nombre.Contains(cadenabuscar)
                || tp.Codigo.Contains(cadenabuscar)))
            .Select(x => new TipoPagoDTO
            {
                TipoPagoId = x.TipoPagoId,
                Nombre = x.Nombre,
                Codigo = x.Codigo,
                Detalle = x.Detalle,
            })
            .ToList();


            return tipoPagos;
        }

        public IEnumerable<TipoPagoDTO> ObtenerTipoPagosEliminados(string cadenabuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var tipoPagos = context.TiposPago
                .AsNoTracking()
                .Where(tp => tp.EstaEliminado &&
                               (tp.Nombre.Contains(cadenabuscar)
                               || tp.Codigo.Contains(cadenabuscar)))
               .Select(x => new TipoPagoDTO
               {
                   TipoPagoId = x.TipoPagoId,
                   Nombre = x.Nombre,
                   Codigo = x.Codigo,
                   Detalle = x.Detalle,
               })
                .ToList();

            return tipoPagos;
        }
    }
    
}

