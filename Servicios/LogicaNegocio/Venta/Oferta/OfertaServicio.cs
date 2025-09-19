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
        /*  public EstadoOperacion Insertar(OfertaDTO dto)
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
                  IdMarca = dto.IdMarca,
                  IdRubro = dto.IdRubro,
                  IdCategoria = dto.IdCategoria,
                  GrupoNombre = dto.GrupoNombre,
              };

              context.OfertasDescuentos.Add(entidad);
              context.SaveChanges();

              return new EstadoOperacion
              {
                  Exitoso = true,
                  Mensaje = "Oferta creada correctamente.",
                  EntidadId = entidad.OfertaDescuentoId
              };
          }*/
        public EstadoOperacion Insertar(OfertaDTO dto)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            using var transaction = context.Database.BeginTransaction();

            try
            {
                if (context.OfertasDescuentos.Any(o => o.Descripcion == dto.Descripcion))
                {
                    return new EstadoOperacion { Exitoso = false, Mensaje = "Ya existe una oferta con la misma descripción." };
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
                    Detalle = dto.Detalle ?? string.Empty,
                    Codigo = dto.Codigo,
                    esOfertaPorGrupo = dto.esOfertaPorGrupo,
                    TieneLimiteDeStock = dto.TieneLimiteDeStock,
                    CantidadLimiteDeStock = dto.CantidadLimiteDeStock,
                    IdMarca = dto.IdMarca,
                    IdRubro = dto.IdRubro,
                    IdCategoria = dto.IdCategoria,
                    GrupoNombre = dto.GrupoNombre
                };

                context.OfertasDescuentos.Add(entidad);
                context.SaveChanges();

                var ofertaIdGenerado = entidad.OfertaDescuentoId;

                if (dto.Productos != null && dto.Productos.Any())
                {
                    var resultadoProductos = CrearProductosEnOferta(
                        context,
                        ofertaId: ofertaIdGenerado,
                        dto.Productos,
                        cantidadLimiteDeStock: dto.CantidadLimiteDeStock
                    );

                    if (!resultadoProductos.Exitoso)
                    {
                        transaction.Rollback();
                        return resultadoProductos; 
                    }
                }

                transaction.Commit();

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Oferta creada correctamente.",
                    EntidadId = ofertaIdGenerado
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new EstadoOperacion { Exitoso = false, Mensaje = "Error al crear la oferta:  aqui" + ex.Message };
            }
        }

        private EstadoOperacion CrearProductosEnOferta(
            GestorContextDB context,
            long ofertaId,
            ICollection<ProductosEnOfertaDescuentosDTO> productosDto,
            decimal? cantidadLimiteDeStock = null
        )
        {
            if (productosDto == null || !productosDto.Any())
            {
                return new EstadoOperacion { Exitoso = true, Mensaje = "No hay productos para agregar." };
            }

            var entidadesHijas = productosDto.Select(p => new ProductosEnOfertaDescuentos
            {
                OfertaId = ofertaId,
                ProductoId = p.ProductoOfertaId,
                Cantidad = p.Cantidad,
                CantidadVendidaPorLimite = 0.0m, 
                PrecioOrginal = p.PrecioVenta,
                PrecioConDescuento = 0.0m
            }).ToList();

            // Insertar las filas hijas
            context.Set<ProductosEnOfertaDescuentos>().AddRange(entidadesHijas);
            context.SaveChanges();

            return new EstadoOperacion { Exitoso = true, Mensaje = "Productos en oferta creados correctamente." };
        }

    }
}
