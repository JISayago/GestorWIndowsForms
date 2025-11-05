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
                Cantidad = p.CantidadItemEnOferta.HasValue ? (decimal)p.CantidadItemEnOferta.Value : -1m,
                CantidadVendidaPorLimite = p.CantidadItemEnOferta.HasValue ? (decimal)ofertaDto.CantidadLimiteDeStock : -1m ,//agregart el limite segun corresponda
                PrecioOrginal = p.PrecioVenta,
                PrecioConDescuento = p.PrecioVenta * (1 - (porcentaje / 100m))
            }).ToList();

            context.Set<ProductosEnOfertaDescuentos>().AddRange(entidadesHijas);
            context.SaveChanges();

            return new EstadoOperacion { Exitoso = true, Mensaje = "Productos en oferta creados correctamente." };
        }
        public List<OfertaDTO> ObtenerOfertasActivas(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var ofertas = context.OfertasDescuentos
                .Where(o => (string.IsNullOrEmpty(cadenaBuscar) || o.Descripcion.Contains(cadenaBuscar) || o.Codigo.Contains(cadenaBuscar)) && o.EstaActiva == true)
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

        public bool ExisteOfertaPorCodigo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return false;

            codigo = codigo.Trim().ToLowerInvariant();

            using var context = new GestorContextDBFactory().CreateDbContext(null);
            return context.OfertasDescuentos
                          .Any(o => o.Codigo != null && o.Codigo.ToLower() == codigo);
        }

        public List<OfertaDTO> ObtenerOfertasInactivas(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var ofertas = context.OfertasDescuentos
                .Where(o => (string.IsNullOrEmpty(cadenaBuscar) || o.Descripcion.Contains(cadenaBuscar) || o.Codigo.Contains(cadenaBuscar))
                            && o.EstaActiva == false)
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

        public List<OfertaDTO> ObtenerOfertasActivasCompuestas(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var ofertas = context.OfertasDescuentos
                .Where(o => (string.IsNullOrEmpty(cadenaBuscar) || o.Descripcion.Contains(cadenaBuscar) || o.Codigo.Contains(cadenaBuscar))
                 && o.EstaActiva == true && !o.esOfertaPorGrupo)
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

        public List<OfertaDTO> ObtenerOfertasInactivasCompuesta(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var ofertas = context.OfertasDescuentos
                .Where(o => (string.IsNullOrEmpty(cadenaBuscar) || o.Descripcion.Contains(cadenaBuscar) || o.Codigo.Contains(cadenaBuscar))
                            && o.EstaActiva == false && !o.esOfertaPorGrupo)
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


        public List<InformacionExistenciaOfertaDescuentoProducto> ObtenerProductosEnOferta(List<ProductoDTO> productosDentroOferta)
        {
            if (productosDentroOferta == null || productosDentroOferta.Count == 0)
                return new List<InformacionExistenciaOfertaDescuentoProducto>();

            var productoIds = productosDentroOferta
                .Where(p => p != null)
                .Select(p => p.ProductoId)
                .Distinct()
                .ToList();

            if (productoIds.Count == 0) return new List<InformacionExistenciaOfertaDescuentoProducto>();

            using var context = new GestorContextDBFactory().CreateDbContext(null);
            //roto 
            // Traemos coincidencias y los datos de la oferta (navegación)
            var matches = context.ProductosEnOfertasDescuentos
                .Where(pe => productoIds.Contains(pe.ProductoId))
                .Include(pe => pe.Oferta) // si existe la navegación
                .AsNoTracking()
                .Select(pe => new InformacionExistenciaOfertaDescuentoProducto
                {
                    ProductoId = pe.ProductoId,
                    OfertaId = pe.OfertaId, // o pe.OfertaId si se llama distinto
                    OfertaCodigo = pe.Oferta != null ? pe.Oferta.Codigo : "",
                    OfertaActiva = pe.Oferta != null && pe.Oferta.EstaActiva,
                    cantidadProductoEnOferta = pe.Cantidad
                })
                // Evitamos duplicados exactos (si hay múltiples filas iguales)
                .AsEnumerable()
                .GroupBy(x => new { x.ProductoId, x.OfertaId })
                .Select(g => g.First())
                .ToList();

            return matches;
        }

        public List<OfertaDTO> ObtenerOfertasActivasInactivas(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.OfertasDescuentos
                .Where(o => string.IsNullOrEmpty(cadenaBuscar)
                            || o.Descripcion.Contains(cadenaBuscar)
                            || o.Codigo.Contains(cadenaBuscar))
                .Include(o => o.Productos)
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
                });

            var ofertas = query.ToList();

            var ahora = DateTime.Now;

            ofertas = ofertas
                .OrderBy(o => Math.Abs(((o.FechaFin ?? DateTime.MaxValue) - ahora).TotalSeconds))
                .ToList();

            return ofertas;
        }
        public OfertaDTO? ActivarDesactivar(long ofertaId)
        {
            try
            {
                using var context = new GestorContextDBFactory().CreateDbContext(null);
                using var transaction = context.Database.BeginTransaction();

                var oferta = context.OfertasDescuentos
                    .Include(o => o.Productos)
                    .FirstOrDefault(o => o.OfertaDescuentoId == ofertaId);

                if (oferta == null)
                    return null;

                // Si está activa, simplemente se desactiva
                if (oferta.EstaActiva)
                {
                    oferta.EstaActiva = false;
                    context.SaveChanges();
                    transaction.Commit();
                }
                else
                {
                    // Si se va a activar, buscar y desactivar otras ofertas que comparten productos
                    var productoIds = oferta.Productos.Select(p => p.ProductoId).ToList();

                    var ofertasEnConflicto = context.OfertasDescuentos
                        .Include(o => o.Productos)
                        .Where(o => o.EstaActiva && o.OfertaDescuentoId != ofertaId)
                        .Where(o => o.Productos.Any(p => productoIds.Contains(p.ProductoId)))
                        .ToList();

                    foreach (var conflictiva in ofertasEnConflicto)
                        conflictiva.EstaActiva = false;

                    // Activar la actual
                    oferta.EstaActiva = true;

                    context.SaveChanges();
                    transaction.Commit();
                }

                // Asegurarnos de tener el estado final cargado
                context.Entry(oferta).Collection(o => o.Productos).Load();
                context.Entry(oferta).Reload();

                // Mapear a DTO y devolver
                var dto = new OfertaDTO
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
                    Detalle = oferta.Detalle,
                    Codigo = oferta.Codigo,
                    esOfertaPorGrupo = oferta.esOfertaPorGrupo,
                    TieneLimiteDeStock = oferta.TieneLimiteDeStock,
                    CantidadLimiteDeStock = oferta.CantidadLimiteDeStock,
                    IdMarca = oferta.IdMarca,
                    IdRubro = oferta.IdRubro,
                    IdCategoria = oferta.IdCategoria,
                    GrupoNombre = oferta.GrupoNombre,
                    Productos = oferta.Productos?.Select(p => new ProductoDTO
                    {
                        ProductoId = p.ProductoId,
                        PrecioVenta = p.PrecioOrginal
                    }).ToList() ?? new List<ProductoDTO>()
                };

                return dto;
            }
            catch (Exception)
            {
                // opcional: loguear exception
                return null;
            }
        }
    }
}
