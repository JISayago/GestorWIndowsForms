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
using Servicios.LogicaNegocio.Producto.DTO;

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
                        ofertaId: ofertaIdGenerado,         // <-- pasar el id generado
                        ofertaDto: dto,
                        productosDto: dto.Productos,
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
                return new EstadoOperacion { Exitoso = false, Mensaje = "Error al crear la oferta: " + ex.Message };
            }
        }
        private EstadoOperacion CrearProductosEnOferta(
            GestorContextDB context,
            long ofertaId,
            OfertaDTO ofertaDto,
            ICollection<ProductoDTO> productosDto,
            decimal? cantidadLimiteDeStock = null
        )
        {
            if (productosDto == null || !productosDto.Any())
            {
                return new EstadoOperacion { Exitoso = true, Mensaje = "No hay productos para agregar." };
            }

            // Asegurar porcentaje (si es null lo tratamos como 0)
            var porcentaje = ofertaDto.PorcentajeDescuento ?? 0m;

            var entidadesHijas = productosDto.Select(p => new ProductosEnOfertaDescuentos
            {
                OfertaId = ofertaId,
                ProductoId = p.ProductoId,
                Cantidad = 0.0m,
                CantidadVendidaPorLimite = 0.0m,//agregart el limite segun corresponda
                PrecioOrginal = p.PrecioVenta,
                PrecioConDescuento = p.PrecioVenta * (1 - (porcentaje / 100m))
            }).ToList();

            context.Set<ProductosEnOfertaDescuentos>().AddRange(entidadesHijas);
            context.SaveChanges();

            return new EstadoOperacion { Exitoso = true, Mensaje = "Productos en oferta creados correctamente." };
        }
        public List<OfertaDTO> ObtenerOfertas(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var ofertas = context.OfertasDescuentos
                .Where(o => string.IsNullOrEmpty(cadenaBuscar) || o.Descripcion.Contains(cadenaBuscar) || o.Codigo.Contains(cadenaBuscar))
                .Include(o => o.Productos)
                .ToList()
                .Select(x => new OfertaDTO
                {
                    OfertaDescuentoId = x.OfertaDescuentoId,
                    Descripcion = x.Descripcion,
                    PrecioFinal = x.PrecioFinal,
                    PrecioOriginal = x.PrecioOriginal,
                    DescuentoTotalFinal = x.DescuentoTotalFinal,
                    PorcentajeDescuento = x.PorcentajeDescuento,
                    FechaInicio = x.FechaInicio,
                    FechaFin = x.FechaFin,
                    CantidadProductosDentroOferta = x.CantidadProductosDentroOferta,
                    EstaActiva = x.EstaActiva,
                    EsUnSoloProducto = x.EsUnSoloProducto,
                    Detalle = x.Detalle,
                    Codigo = x.Codigo,
                    esOfertaPorGrupo = x.esOfertaPorGrupo,
                    TieneLimiteDeStock = x.TieneLimiteDeStock,
                    CantidadLimiteDeStock = x.CantidadLimiteDeStock,
                    IdMarca = x.IdMarca,
                    IdRubro = x.IdRubro,
                    IdCategoria = x.IdCategoria,
                    GrupoNombre = x.GrupoNombre,
                    Productos = x.Productos.Select(p => new ProductoDTO
                    {
                        ProductoId = p.ProductoId,
                        PrecioVenta = p.PrecioOrginal
                    }).ToList()
                })
            .ToList();
            return ofertas;
        }

        public OfertaDTO ObtenerOfertaPorId(long idOFerta)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var oferta = context.OfertasDescuentos
                .Include(o => o.Productos)
                .Where(o => o.OfertaDescuentoId == idOFerta)
                .Select(x => new OfertaDTO
                {
                    OfertaDescuentoId = x.OfertaDescuentoId,
                    Descripcion = x.Descripcion,
                    PrecioFinal = x.PrecioFinal,
                    PrecioOriginal = x.PrecioOriginal,
                    DescuentoTotalFinal = x.DescuentoTotalFinal,
                    PorcentajeDescuento = x.PorcentajeDescuento,
                    FechaInicio = x.FechaInicio,
                    FechaFin = x.FechaFin,
                    CantidadProductosDentroOferta = x.CantidadProductosDentroOferta,
                    EstaActiva = x.EstaActiva,
                    EsUnSoloProducto = x.EsUnSoloProducto,
                    Detalle = x.Detalle,
                    Codigo = x.Codigo,
                    esOfertaPorGrupo = x.esOfertaPorGrupo,
                    TieneLimiteDeStock = x.TieneLimiteDeStock,
                    CantidadLimiteDeStock = x.CantidadLimiteDeStock,
                    IdMarca = x.IdMarca,
                    IdRubro = x.IdRubro,
                    IdCategoria = x.IdCategoria,
                    GrupoNombre = x.GrupoNombre,
                    Productos = x.Productos.Select(p => new ProductoDTO
                    {
                        ProductoId = p.ProductoId,
                        PrecioVenta = p.PrecioOrginal
                    }).ToList()
                }).FirstOrDefault();
            return oferta;
        }
    }
}
