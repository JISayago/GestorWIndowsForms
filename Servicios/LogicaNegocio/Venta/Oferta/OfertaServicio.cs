using AccesoDatos;
using AccesoDatos.Entidades;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Venta.Oferta.DTO;
using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Servicios.LogicaNegocio.Venta.Oferta
{
        public class OfertaServicio : IOfertaServicio
        {
            public EstadoOperacion Insertar(OfertaDTO dto)
            {
                using var context = new GestorContextDBFactory().CreateDbContext(null);

                // Validar duplicados
                if (context.OfertasDescuentos.Any(o => o.Descripcion == dto.Descripcion))
                {
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "Ya existe una oferta con la misma descripción."
                    };
                }

                var entidad = new OfertaDescuento
                {
                    Descripcion = dto.Descripcion,
                    PrecioFinal = dto.PrecioFinal,
                    PrecioOriginal = dto.PrecioOriginal,
                    DescuentoTotalFinal = dto.DescuentoTotalFinal,
                    PorcentajeDescuento = dto.PorcentajeDescuento,
                    FechaInicio = dto.FechaInicio,
                    FechaFin = dto.FechaFin,
                    CantidadProductosDentroOferta = dto.CantidadProductosDentroOferta,
                    EstaActiva = dto.EstaActiva,
                    EsUnSoloProducto = dto.EsUnSoloProducto,
                    Productos = dto.Productos.ToList()
                };

                context.OfertasDescuentos.Add(entidad);
                context.SaveChanges();

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Oferta creada correctamente.",
                    EntidadId = entidad.OfertaDescuentoId
                };
            }

            public EstadoOperacion Modificar(long ofertaId, OfertaDTO dto)
            {
                using var context = new GestorContextDBFactory().CreateDbContext(null);

                var oferta = context.OfertasDescuentos
                    .Include(o => o.Productos)
                    .FirstOrDefault(o => o.OfertaDescuentoId == ofertaId);

                if (oferta == null)
                {
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "La oferta no fue encontrada."
                    };
                }

                oferta.Descripcion = dto.Descripcion;
                oferta.PrecioFinal = dto.PrecioFinal;
                oferta.PrecioOriginal = dto.PrecioOriginal;
                oferta.DescuentoTotalFinal = dto.DescuentoTotalFinal;
                oferta.PorcentajeDescuento = dto.PorcentajeDescuento;
                oferta.FechaInicio = dto.FechaInicio;
                oferta.FechaFin = dto.FechaFin;
                oferta.CantidadProductosDentroOferta = dto.CantidadProductosDentroOferta;
                oferta.EstaActiva = dto.EstaActiva;
                oferta.EsUnSoloProducto = dto.EsUnSoloProducto;

                // Actualizar productos en la oferta
                oferta.Productos.Clear();
                foreach (var prod in dto.Productos)
                {
                    oferta.Productos.Add(prod);
                }

                context.SaveChanges();

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Oferta modificada correctamente.",
                    EntidadId = oferta.OfertaDescuentoId
                };
            }

            public EstadoOperacion Eliminar(long ofertaId)
            {
                using var context = new GestorContextDBFactory().CreateDbContext(null);

                var oferta = context.OfertasDescuentos.FirstOrDefault(o => o.OfertaDescuentoId == ofertaId);
                if (oferta == null)
                    throw new Exception("No se encontró la oferta.");

                context.SaveChanges();

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = $"La oferta {oferta.Descripcion} fue eliminada correctamente."
                };
            }

            public OfertaDTO ObtenerPorId(long ofertaId)
            {
                using var context = new GestorContextDBFactory().CreateDbContext(null);

                var oferta = context.OfertasDescuentos
                    .AsNoTracking()
                    .Include(o => o.Productos)
                    .FirstOrDefault(o => o.OfertaDescuentoId == ofertaId);

                if (oferta == null) return null;

                return new OfertaDTO
                {
                    OfertaDescuentoId = oferta.OfertaDescuentoId,
                    Descripcion = oferta.Descripcion,
                    PrecioFinal = oferta.PrecioFinal,
                    PrecioOriginal = oferta.PrecioOriginal,
                    DescuentoTotalFinal = oferta.DescuentoTotalFinal,
                    PorcentajeDescuento = oferta.PorcentajeDescuento,
                    FechaInicio = oferta.FechaInicio,
                    FechaFin = oferta.FechaFin,
                    CantidadProductosDentroOferta = oferta.CantidadProductosDentroOferta,
                    EstaActiva = oferta.EstaActiva,
                    EsUnSoloProducto = oferta.EsUnSoloProducto,
                    Productos = oferta.Productos.ToList()
                };
            }

            public IEnumerable<OfertaDTO> ObtenerTodas(string cadenaBuscar)
            {
                using var context = new GestorContextDBFactory().CreateDbContext(null);

                var query = context.OfertasDescuentos
                    .AsNoTracking()
                    .Include(o => o.Productos)
                    .Where(o => (string.IsNullOrEmpty(cadenaBuscar) ||
                                 o.Descripcion.Contains(cadenaBuscar)));

                return query.Select(o => new OfertaDTO
                {
                    OfertaDescuentoId = o.OfertaDescuentoId,
                    Descripcion = o.Descripcion,
                    PrecioFinal = o.PrecioFinal,
                    PrecioOriginal = o.PrecioOriginal,
                    DescuentoTotalFinal = o.DescuentoTotalFinal,
                    PorcentajeDescuento = o.PorcentajeDescuento,
                    FechaInicio = o.FechaInicio,
                    FechaFin = o.FechaFin,
                    CantidadProductosDentroOferta = o.CantidadProductosDentroOferta,
                    EstaActiva = o.EstaActiva,
                    EsUnSoloProducto = o.EsUnSoloProducto,
                    Productos = o.Productos.ToList()
                }).ToList();
            }
        }
}
