using AccesoDatos;
using AccesoDatos.Entidades;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.Admin;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.Helpers.Venta.Oferta;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Venta.DTO;
using Servicios.LogicaNegocio.Venta.Oferta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (context.OfertasDescuentos.Any(x =>
                    x.Descripcion == dto.Descripcion))
                {
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "Ya existe una oferta con la misma descripción."
                    };
                }

                if (dto.Productos == null || !dto.Productos.Any())
                {
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "La oferta debe contener al menos un producto."
                    };
                }

                if (string.IsNullOrWhiteSpace(dto.Codigo) ||
                    dto.Codigo.Trim() == "*")
                {
                    //dto.Codigo = GenerarCodigoOferta(context, dto);
                    dto.Codigo = "x";
                }

                var oferta = new OfertaDescuento
                {
                    Descripcion = dto.Descripcion,
                    Codigo = dto.Codigo,
                    FechaInicio = dto.FechaInicio,
                    FechaFin = dto.FechaFin,
                    EstaActiva = dto.EstaActiva,
                    TipoOferta = dto.TipoOferta,
                    PorcentajeDescuento = dto.PorcentajeDescuento,
                    PrecioFinal = dto.PrecioFinal
                };

                context.OfertasDescuentos.Add(oferta);

                context.SaveChanges();

                var productosOferta = dto.Productos
       .Select(x =>
       {
           decimal? precioOferta = x.PrecioOfertaBase;

           // Si no vino un precio específico, lo calculo
           if (!precioOferta.HasValue && dto.PorcentajeDescuento.HasValue)
           {
               precioOferta = x.PrecioVentaBase -
                              (x.PrecioVentaBase * dto.PorcentajeDescuento.Value / 100m);
           }

           return new ProductosEnOfertaDescuentos
           {
               OfertaDescuentoId = oferta.OfertaDescuentoId,
               ProductoId = x.ProductoId,

               CantidadRequerida = x.CantidadRequerida,

               PrecioVentaBase = x.PrecioVentaBase,
               PrecioCostoBase = x.PrecioCostoBase,

               PrecioOfertaBase = precioOferta,

               LimiteVentaProducto = x.LimiteVentaProducto
           };
       })
       .ToList();

                context.ProductosEnOfertasDescuentos.AddRange(productosOferta);

                context.SaveChanges();

                transaction.Commit();

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Oferta creada correctamente.",
                    EntidadId = oferta.OfertaDescuentoId
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = $"Error al crear la oferta. {ex.Message}"
                };
            }
        }
      

        //private EstadoOperacion CrearProductosEnOferta(GestorContextDB context,long ofertaId,OfertaDTO ofertaDto,ICollection<ProductoDTO> productosDto,decimal? cantidadLimiteDeStock = null)
        //{
        //    if (productosDto == null || !productosDto.Any())
        //    {
        //        return new EstadoOperacion { Exitoso = true, Mensaje = "No hay productos para agregar." };
        //    }

        //    var porcentaje = ofertaDto.PorcentajeDescuento ?? 0m;

        //    var entidadesHijas = productosDto.Select(p => new ProductosEnOfertaDescuentos
        //    {
        //        OfertaId = ofertaId,
        //        ProductoId = p.ProductoId,
        //        Cantidad = p.CantidadItemEnOferta.HasValue ? (decimal)p.CantidadItemEnOferta.Value : -1m,
        //        CantidadVendidaPorLimite = p.CantidadItemEnOferta.HasValue
        //            ? (decimal)ofertaDto.CantidadLimiteDeStock
        //            : -1m,
        //        PrecioOrginal = p.PrecioVenta,
        //        PrecioConDescuento = p.PrecioVenta * (1 - (porcentaje / 100m))
        //    }).ToList();

        //    context.Set<ProductosEnOfertaDescuentos>().AddRange(entidadesHijas);

        //    // 🔹 ACTUALIZAR ESTADO DE LOS PRODUCTOS A 2
        //    var productosIds = productosDto.Select(p => p.ProductoId).ToList();

        //    var productos = context.Productos
        //        .Where(p => productosIds.Contains(p.ProductoId))
        //        .ToList();

        //    foreach (var producto in productos)
        //    {
        //        producto.Estado = 2;
        //    }

        //    context.SaveChanges();

        //    return new EstadoOperacion
        //    {
        //        Exitoso = true,
        //        Mensaje = "Productos en oferta creados correctamente."
        //    };
        //}


        //public OfertaDTO ObtenerOfertaPorId(long idOFerta)
        //{
        //    using var context = new GestorContextDBFactory().CreateDbContext(null);
        //    var oferta = context.OfertasDescuentos
        //        .Include(o => o.Productos)
        //        .Where(o => o.OfertaDescuentoId == idOFerta)
        //        .Select(x => new OfertaDTO
        //        {
        //            OfertaDescuentoId = x.OfertaDescuentoId,
        //            Descripcion = x.Descripcion,
        //            PrecioFinal = x.PrecioFinal,
        //            PrecioOriginal = x.PrecioOriginal,
        //            DescuentoTotalFinal = x.DescuentoTotalFinal,
        //            PorcentajeDescuento = x.PorcentajeDescuento,
        //            FechaInicio = x.FechaInicio,
        //            FechaFin = x.FechaFin,
        //            CantidadProductosDentroOferta = x.CantidadProductosDentroOferta,
        //            EstaActiva = x.EstaActiva,
        //            EsUnSoloProducto = x.EsUnSoloProducto,
        //            Detalle = x.Detalle,
        //            Codigo = x.Codigo,
        //            esOfertaPorGrupo = x.esOfertaPorGrupo,
        //            TieneLimiteDeStock = x.TieneLimiteDeStock,
        //            CantidadLimiteDeStock = x.CantidadLimiteDeStock,
        //            IdMarca = x.IdMarca,
        //            IdRubro = x.IdRubro,
        //            IdCategoria = x.IdCategoria,
        //            GrupoNombre = x.GrupoNombre,
        //            Productos = x.Productos.Select(p => new ProductoDTO
        //            {
        //                ProductoId = p.ProductoId,
        //                PrecioVenta = p.PrecioOrginal
        //            }).ToList()
        //        }).FirstOrDefault();
        //    return oferta;
        //}

        public bool ExisteOfertaPorCodigo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return false;

            codigo = codigo.Trim().ToLowerInvariant();

            using var context = new GestorContextDBFactory().CreateDbContext(null);
            return context.OfertasDescuentos
                          .Any(o => o.Codigo != null && o.Codigo.ToLower() == codigo);
        }

        //public List<OfertaDTO> ObtenerOfertasInactivas(string cadenaBuscar)
        //{
        //    using var context = new GestorContextDBFactory().CreateDbContext(null);
        //    var ofertas = context.OfertasDescuentos
        //        .Where(o => (string.IsNullOrEmpty(cadenaBuscar) || o.Descripcion.Contains(cadenaBuscar) || o.Codigo.Contains(cadenaBuscar))
        //                    && o.EstaActiva == false)
        //        .Include(o => o.Productos)
        //        .ToList()
        //        .Select(x => new OfertaDTO
        //        {
        //            OfertaDescuentoId = x.OfertaDescuentoId,
        //            Descripcion = x.Descripcion,
        //            PrecioFinal = x.PrecioFinal,
        //            PrecioOriginal = x.PrecioOriginal,
        //            DescuentoTotalFinal = x.DescuentoTotalFinal,
        //            PorcentajeDescuento = x.PorcentajeDescuento,
        //            FechaInicio = x.FechaInicio,
        //            FechaFin = x.FechaFin,
        //            CantidadProductosDentroOferta = x.CantidadProductosDentroOferta,
        //            EstaActiva = x.EstaActiva,
        //            EsUnSoloProducto = x.EsUnSoloProducto,
        //            Detalle = x.Detalle,
        //            Codigo = x.Codigo,
        //            esOfertaPorGrupo = x.esOfertaPorGrupo,
        //            TieneLimiteDeStock = x.TieneLimiteDeStock,
        //            CantidadLimiteDeStock = x.CantidadLimiteDeStock,
        //            IdMarca = x.IdMarca,
        //            IdRubro = x.IdRubro,
        //            IdCategoria = x.IdCategoria,
        //            GrupoNombre = x.GrupoNombre,
        //            Productos = x.Productos.Select(p => new ProductoDTO
        //            {
        //                ProductoId = p.ProductoId,
        //                PrecioVenta = p.PrecioOrginal
        //            }).ToList()
        //        })
        //    .ToList();

        //    return ofertas;
        //}
        //public List<OfertaDTO> ObtenerOfertasActivas(string cadenaBuscar)
        //{
        //    using var context = new GestorContextDBFactory().CreateDbContext(null);
        //    var ofertas = context.OfertasDescuentos
        //        .Where(o => (string.IsNullOrEmpty(cadenaBuscar) || o.Descripcion.Contains(cadenaBuscar) || o.Codigo.Contains(cadenaBuscar)) && o.EstaActiva == true)
        //        .Include(o => o.Productos)
        //        .ToList()
        //        .Select(x => new OfertaDTO
        //        {
        //            OfertaDescuentoId = x.OfertaDescuentoId,
        //            Descripcion = x.Descripcion,
        //            PrecioFinal = x.PrecioFinal,
        //            PrecioOriginal = x.PrecioOriginal,
        //            DescuentoTotalFinal = x.DescuentoTotalFinal,
        //            PorcentajeDescuento = x.PorcentajeDescuento,
        //            FechaInicio = x.FechaInicio,
        //            FechaFin = x.FechaFin,
        //            CantidadProductosDentroOferta = x.CantidadProductosDentroOferta,
        //            EstaActiva = x.EstaActiva,
        //            EsUnSoloProducto = x.EsUnSoloProducto,
        //            Detalle = x.Detalle,
        //            Codigo = x.Codigo,
        //            esOfertaPorGrupo = x.esOfertaPorGrupo,
        //            TieneLimiteDeStock = x.TieneLimiteDeStock,
        //            CantidadLimiteDeStock = x.CantidadLimiteDeStock,
        //            IdMarca = x.IdMarca,
        //            IdRubro = x.IdRubro,
        //            IdCategoria = x.IdCategoria,
        //            GrupoNombre = x.GrupoNombre,
        //            Productos = x.Productos.Select(p => new ProductoDTO
        //            {
        //                ProductoId = p.ProductoId,
        //                PrecioVenta = p.PrecioOrginal
        //            }).ToList()
        //        })
        //    .ToList();
        //    return ofertas;
        //}


        public ResultadoPaginacion<OfertaDTO> ObtenerOfertas(FiltroConsulta filtros, bool vieneDeVenta = false)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            const string collation = "Latin1_General_CI_AI";


            var query = context.OfertasDescuentos
                .AsNoTracking()
                .Include(x => x.Productos)
                    .ThenInclude(x => x.Producto)
                .AsQueryable();

            // =========================================================
            // OFERTAS PARA VENTA
            // =========================================================

            if (vieneDeVenta)
            {
                query = query.Where(x =>
                    x.EstaActiva &&
                    (
                        x.TipoOferta == (int)TipoOferta.Combo ||
                        x.TipoOferta == (int)TipoOferta.DosPorUno
                    ));
            }
            else
            {
                // Bool1 = mostrar también las inactivas
                if (!filtros.Bool1)
                {
                    query = query.Where(x => x.EstaActiva);
                }
            }

            // =========================================================
            // BUSQUEDA
            // =========================================================

            if (!string.IsNullOrWhiteSpace(filtros.TextoBuscar))
            {
                var texto = filtros.TextoBuscar.Trim();

                switch (filtros.Filtro1?.ToString())
                {
                    case "Codigo":

                        query = query.Where(x =>
                            x.Codigo != null &&
                            EF.Functions.Collate(x.Codigo, collation)
                                .Contains(texto));

                        break;

                    case "Descripcion":

                        query = query.Where(x =>
                            x.Descripcion != null &&
                            EF.Functions.Collate(x.Descripcion, collation)
                                .Contains(texto));

                        break;

                    case "Producto":

                        query = query.Where(x =>
                           x.Productos.Any(p =>
                               (p.Producto.Descripcion != null &&
                                EF.Functions.Collate(p.Producto.Descripcion, collation).Contains(texto))
                               ||
                               (p.Producto.Codigo != null &&
                                EF.Functions.Collate(p.Producto.Codigo, collation).Contains(texto))
                           ));

                        break;

                    default:

                        query = query.Where(x =>

                            (x.Codigo != null &&
                             EF.Functions.Collate(x.Codigo, collation)
                                .Contains(texto))

                            ||

                            (x.Descripcion != null &&
                             EF.Functions.Collate(x.Descripcion, collation)
                                .Contains(texto))

                            ||

                            x.Productos.Any(p =>

                                (p.Producto.Descripcion != null &&
                                 EF.Functions.Collate(p.Producto.Descripcion, collation)
                                    .Contains(texto))
                            
                                ||
                            
                                (p.Producto.Codigo != null &&
                                 EF.Functions.Collate(p.Producto.Codigo, collation)
                                    .Contains(texto))
                            ));

                        break;
                }
            }

            // =========================================================
            // TIPO OFERTA
            // =========================================================

            if (!string.IsNullOrWhiteSpace(filtros.Filtro2?.ToString()))
            {
                if (int.TryParse(filtros.Filtro2.ToString(), out var tipo))
                {
                    query = query.Where(x => x.TipoOferta == tipo);
                }
            }

            // =========================================================
            // FECHAS
            // =========================================================

            var filtroFecha = filtros.Filtro3?.ToString();

            bool hayFiltroFechaManual =
                (filtroFecha == "FechaInicio" || filtroFecha == "FechaFin") &&
                (
                    filtros.FechaDesde.HasValue ||
                    filtros.FechaHasta.HasValue
                );

            if (hayFiltroFechaManual)
            {
                if (filtroFecha == "FechaInicio")
                {
                    if (filtros.FechaDesde.HasValue)
                    {
                        query = query.Where(x =>
                            x.FechaInicio >= filtros.FechaDesde.Value);
                    }

                    if (filtros.FechaHasta.HasValue)
                    {
                        var hasta = filtros.FechaHasta.Value.AddDays(1);

                        query = query.Where(x =>
                            x.FechaInicio < hasta);
                    }
                }
                else if (filtroFecha == "FechaFin")
                {
                    if (filtros.FechaDesde.HasValue)
                    {
                        query = query.Where(x =>
                            x.FechaFin >= filtros.FechaDesde.Value);
                    }

                    if (filtros.FechaHasta.HasValue)
                    {
                        var hasta = filtros.FechaHasta.Value.AddDays(1);

                        query = query.Where(x =>
                            x.FechaFin < hasta);
                    }
                }
            }
            else
            {
                // Normal = últimos 2 meses
                // Histórico = últimos 6 meses

                var fechaLimite = filtros.Bool2
                    ? DateTime.Now.AddMonths(-6)
                    : DateTime.Now.AddMonths(-2);

                query = query.Where(x => x.FechaFin >= fechaLimite);
            }

            // =========================================================
            // TOTAL
            // =========================================================

            var total = query.Count();

            // =========================================================
            // PAGINACION
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

            if (filtroFecha == "FechaFin")
            {
                query = query.OrderByDescending(x => x.FechaFin);
            }
            else
            {
                query = query.OrderByDescending(x => x.FechaInicio);
            }

            // =========================================================
            // DATA
            // =========================================================

            var data = query
                .Skip((filtros.Page - 1) * filtros.PageSize)
                .Take(filtros.PageSize)
                .Select(x => new OfertaDTO
                {
                    OfertaDescuentoId = x.OfertaDescuentoId,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,

                    FechaInicio = x.FechaInicio,
                    FechaFin = x.FechaFin,

                    EstaActiva = x.EstaActiva,
                    TipoOferta = x.TipoOferta,

                    PorcentajeDescuento = x.PorcentajeDescuento,
                    PrecioFinal = x.PrecioFinal,

                    Productos = x.Productos.Select(p => new ProductosEnOfertaDescuentosDTO
                    {
                        ProductoId = p.ProductoId,
                        CantidadRequerida = p.CantidadRequerida,
                        PrecioCostoBase = p.PrecioCostoBase,
                        PrecioVentaBase = p.PrecioVentaBase,
                        PrecioOfertaBase = p.PrecioOfertaBase,
                        LimiteVentaProducto = p.LimiteVentaProducto,

                        Producto = new ProductoDTO
                        {
                            ProductoId = p.Producto.ProductoId,
                            Codigo = p.Producto.Codigo,
                            Descripcion = p.Producto.Descripcion,
                            PrecioCosto = p.Producto.PrecioCosto,
                            PrecioVenta = p.Producto.PrecioVenta,
                            Stock = p.Producto.Stock
                        }

                    }).ToList()
                })
                .ToList();

            return new ResultadoPaginacion<OfertaDTO>
            {
                Items = data,
                TotalRegistros = total,
                Page = filtros.Page,
                PageSize = filtros.PageSize
            };
        }
        //public List<OfertaDTO> ObtenerOfertasActivasCompuestas(string cadenaBuscar, string columna, DateTime? fechaDesde, DateTime? fechaHasta)
        //{
        //    using var context = new GestorContextDBFactory().CreateDbContext(null);

        //    var query = context.OfertasDescuentos
        //        .AsNoTracking()
        //        .Where(o => !o.esOfertaPorGrupo && o.EstaActiva);

        //    // BUSCADOR DINAMICO
        //    if (!string.IsNullOrWhiteSpace(cadenaBuscar))
        //    {
        //        cadenaBuscar = cadenaBuscar.ToLower();

        //        switch (columna)
        //        {
        //            case "Codigo":
        //                query = query.Where(o => o.Codigo.ToLower().Contains(cadenaBuscar));
        //                break;

        //            case "Detalle":
        //                query = query.Where(o => o.Detalle.ToLower().Contains(cadenaBuscar));
        //                break;

        //            default:
        //                query = query.Where(o => o.Descripcion.ToLower().Contains(cadenaBuscar));
        //                break;
        //        }
        //    }

        //    // FILTRO FECHAS
        //    if (fechaDesde.HasValue)
        //        query = query.Where(o => o.FechaInicio >= fechaDesde.Value);

        //    if (fechaHasta.HasValue)
        //        query = query.Where(o => o.FechaFin <= fechaHasta.Value);

        //    var ofertas = query
        //        .Include(o => o.Productos)
        //        .Select(x => new OfertaDTO
        //        {
        //            OfertaDescuentoId = x.OfertaDescuentoId,
        //            Descripcion = x.Descripcion,
        //            PrecioFinal = x.PrecioFinal,
        //            PrecioOriginal = x.PrecioOriginal,
        //            DescuentoTotalFinal = x.DescuentoTotalFinal,
        //            PorcentajeDescuento = x.PorcentajeDescuento,
        //            FechaInicio = x.FechaInicio,
        //            FechaFin = x.FechaFin,
        //            CantidadProductosDentroOferta = x.CantidadProductosDentroOferta,
        //            EstaActiva = x.EstaActiva,
        //            EsUnSoloProducto = x.EsUnSoloProducto,
        //            Detalle = x.Detalle,
        //            Codigo = x.Codigo,
        //            esOfertaPorGrupo = x.esOfertaPorGrupo,
        //            TieneLimiteDeStock = x.TieneLimiteDeStock,
        //            CantidadLimiteDeStock = x.CantidadLimiteDeStock,
        //            IdMarca = x.IdMarca,
        //            IdRubro = x.IdRubro,
        //            IdCategoria = x.IdCategoria,
        //            GrupoNombre = x.GrupoNombre,
        //            Productos = x.Productos.Select(p => new ProductoDTO
        //            {
        //                ProductoId = p.ProductoId,
        //                PrecioVenta = p.PrecioOrginal
        //            }).ToList()
        //        })
        //        .ToList();

        //    return ofertas;
        //}

        //public List<OfertaDTO> ObtenerOfertasInactivasCompuesta(string cadenaBuscar, string columna, DateTime? fechaDesde, DateTime? fechaHasta)
        //{
        //    using var context = new GestorContextDBFactory().CreateDbContext(null);

        //    var query = context.OfertasDescuentos
        //        .AsNoTracking()
        //        .Where(o => !o.esOfertaPorGrupo && !o.EstaActiva);

        //    if (!string.IsNullOrWhiteSpace(cadenaBuscar))
        //    {
        //        cadenaBuscar = cadenaBuscar.ToLower();

        //        switch (columna)
        //        {
        //            case "Codigo":
        //                query = query.Where(o => o.Codigo.ToLower().Contains(cadenaBuscar));
        //                break;

        //            case "Detalle":
        //                query = query.Where(o => o.Detalle.ToLower().Contains(cadenaBuscar));
        //                break;

        //            default:
        //                query = query.Where(o => o.Descripcion.ToLower().Contains(cadenaBuscar));
        //                break;
        //        }
        //    }

        //    if (fechaDesde.HasValue)
        //        query = query.Where(o => o.FechaInicio >= fechaDesde.Value);

        //    if (fechaHasta.HasValue)
        //        query = query.Where(o => o.FechaFin <= fechaHasta.Value);

        //    var ofertas = query
        //        .Include(o => o.Productos)
        //        .Select(x => new OfertaDTO
        //        {
        //            OfertaDescuentoId = x.OfertaDescuentoId,
        //            Descripcion = x.Descripcion,
        //            PrecioFinal = x.PrecioFinal,
        //            PrecioOriginal = x.PrecioOriginal,
        //            DescuentoTotalFinal = x.DescuentoTotalFinal,
        //            PorcentajeDescuento = x.PorcentajeDescuento,
        //            FechaInicio = x.FechaInicio,
        //            FechaFin = x.FechaFin,
        //            CantidadProductosDentroOferta = x.CantidadProductosDentroOferta,
        //            EstaActiva = x.EstaActiva,
        //            EsUnSoloProducto = x.EsUnSoloProducto,
        //            Detalle = x.Detalle,
        //            Codigo = x.Codigo,
        //            esOfertaPorGrupo = x.esOfertaPorGrupo,
        //            TieneLimiteDeStock = x.TieneLimiteDeStock,
        //            CantidadLimiteDeStock = x.CantidadLimiteDeStock,
        //            IdMarca = x.IdMarca,
        //            IdRubro = x.IdRubro,
        //            IdCategoria = x.IdCategoria,
        //            GrupoNombre = x.GrupoNombre,
        //            Productos = x.Productos.Select(p => new ProductoDTO
        //            {
        //                ProductoId = p.ProductoId,
        //                PrecioVenta = p.PrecioOrginal
        //            }).ToList()
        //        })
        //        .ToList();

        //    return ofertas;
        //}

        //public List<OfertaDTO> ObtenerOfertasActivasInactivas(string cadenaBuscar, string columna, DateTime? fechaDesde, DateTime? fechaHasta)
        //{
        //    using var context = new GestorContextDBFactory().CreateDbContext(null);
        //    var ahora = DateTime.Now;

        //    var query = context.OfertasDescuentos
        //        .AsNoTracking()
        //        .Where(o => !o.esOfertaPorGrupo);

        //    if (!string.IsNullOrWhiteSpace(cadenaBuscar))
        //    {
        //        cadenaBuscar = cadenaBuscar.ToLower();

        //        switch (columna)
        //        {
        //            case "Codigo":
        //                query = query.Where(o => o.Codigo.ToLower().Contains(cadenaBuscar));
        //                break;

        //            case "Detalle":
        //                query = query.Where(o => o.Detalle.ToLower().Contains(cadenaBuscar));
        //                break;

        //            default:
        //                query = query.Where(o => o.Descripcion.ToLower().Contains(cadenaBuscar));
        //                break;
        //        }
        //    }

        //    if (fechaDesde.HasValue)
        //        query = query.Where(o => o.FechaInicio >= fechaDesde.Value);

        //    if (fechaHasta.HasValue)
        //        query = query.Where(o => o.FechaFin <= fechaHasta.Value);

        //    var ofertas = query
        //        .Include(o => o.Productos)
        //        .Select(x => new OfertaDTO
        //        {
        //            OfertaDescuentoId = x.OfertaDescuentoId,
        //            Descripcion = x.Descripcion,
        //            PrecioFinal = x.PrecioFinal,
        //            PrecioOriginal = x.PrecioOriginal,
        //            DescuentoTotalFinal = x.DescuentoTotalFinal,
        //            PorcentajeDescuento = x.PorcentajeDescuento,
        //            FechaInicio = x.FechaInicio,
        //            FechaFin = x.FechaFin,
        //            CantidadProductosDentroOferta = x.CantidadProductosDentroOferta,
        //            EstaActiva = x.EstaActiva,
        //            EsUnSoloProducto = x.EsUnSoloProducto,
        //            Detalle = x.Detalle,
        //            Codigo = x.Codigo,
        //            esOfertaPorGrupo = x.esOfertaPorGrupo,
        //            TieneLimiteDeStock = x.TieneLimiteDeStock,
        //            CantidadLimiteDeStock = x.CantidadLimiteDeStock,
        //            IdMarca = x.IdMarca,
        //            IdRubro = x.IdRubro,
        //            IdCategoria = x.IdCategoria,
        //            GrupoNombre = x.GrupoNombre,
        //            Productos = x.Productos.Select(p => new ProductoDTO
        //            {
        //                ProductoId = p.ProductoId,
        //                PrecioVenta = p.PrecioOrginal
        //            }).ToList()
        //        })
        //        .ToList();

        //    // ORDENAR POR FECHA FIN MAS CERCANA
        //    ofertas = ofertas
        //        .OrderBy(o => Math.Abs(((o.FechaFin ?? DateTime.MaxValue) - ahora).TotalSeconds))
        //        .ToList();

        //    return ofertas;
        //}

        //public OfertaDTO? ActivarDesactivar(long ofertaId)
        //{
        //    try
        //    {
        //        using var context = new GestorContextDBFactory().CreateDbContext(null);
        //        using var transaction = context.Database.BeginTransaction();

        //        var oferta = context.OfertasDescuentos
        //            .Include(o => o.Productos)
        //            .FirstOrDefault(o => o.OfertaDescuentoId == ofertaId);

        //        if (oferta == null)
        //            return null;

        //        // Si está activa, simplemente se desactiva
        //        if (oferta.EstaActiva)
        //        {
        //            oferta.EstaActiva = false;
        //            context.SaveChanges();
        //            transaction.Commit();
        //        }
        //        else
        //        {
        //            // Si se va a activar, buscar y desactivar otras ofertas que comparten productos
        //            var productoIds = oferta.Productos.Select(p => p.ProductoId).ToList();

        //            var ofertasEnConflicto = context.OfertasDescuentos
        //                .Include(o => o.Productos)
        //                .Where(o => o.EstaActiva && o.OfertaDescuentoId != ofertaId)
        //                .Where(o => o.Productos.Any(p => productoIds.Contains(p.ProductoId)))
        //                .ToList();

        //            foreach (var conflictiva in ofertasEnConflicto)
        //                conflictiva.EstaActiva = false;

        //            // Activar la actual
        //            oferta.EstaActiva = true;

        //            context.SaveChanges();
        //            transaction.Commit();
        //        }

        //        // Asegurarnos de tener el estado final cargado
        //        context.Entry(oferta).Collection(o => o.Productos).Load();
        //        context.Entry(oferta).Reload();

        //        // Mapear a DTO y devolver
        //        var dto = new OfertaDTO
        //        {
        //            OfertaDescuentoId = oferta.OfertaDescuentoId,
        //            Descripcion = oferta.Descripcion,
        //            PrecioFinal = oferta.PrecioFinal,
        //            PrecioOriginal = oferta.PrecioOriginal,
        //            DescuentoTotalFinal = oferta.DescuentoTotalFinal,
        //            PorcentajeDescuento = oferta.PorcentajeDescuento,
        //            FechaInicio = oferta.FechaInicio,
        //            FechaFin = oferta.FechaFin,
        //            CantidadProductosDentroOferta = oferta.CantidadProductosDentroOferta,
        //            EstaActiva = oferta.EstaActiva,
        //            EsUnSoloProducto = oferta.EsUnSoloProducto,
        //            Detalle = oferta.Detalle,
        //            Codigo = oferta.Codigo,
        //            esOfertaPorGrupo = oferta.esOfertaPorGrupo,
        //            TieneLimiteDeStock = oferta.TieneLimiteDeStock,
        //            CantidadLimiteDeStock = oferta.CantidadLimiteDeStock,
        //            IdMarca = oferta.IdMarca,
        //            IdRubro = oferta.IdRubro,
        //            IdCategoria = oferta.IdCategoria,
        //            GrupoNombre = oferta.GrupoNombre,
        //            Productos = oferta.Productos?.Select(p => new ProductoDTO
        //            {
        //                ProductoId = p.ProductoId,
        //                PrecioVenta = p.PrecioOrginal
        //            }).ToList() ?? new List<ProductoDTO>()
        //        };

        //        return dto;
        //    }
        //    catch (Exception)
        //    {
        //        // opcional: loguear exception
        //        return null;
        //    }
        //}
        public ItemVentaDTO? ObtenerItemVentaOferta(long ofertaId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var oferta = context.OfertasDescuentos
                .AsNoTracking()
                .Where(x =>
                    x.OfertaDescuentoId == ofertaId &&
                    x.EstaActiva)
                .Select(x => new
                {
                    x.OfertaDescuentoId,
                    x.Descripcion,
                    x.TipoOferta,
                    x.PrecioFinal,
                    x.PorcentajeDescuento,
                    x.Codigo,

                    Productos = x.Productos.Select(p => new
                    {
                        p.CantidadRequerida,
                        p.PrecioVentaBase
                    })
                })
                .FirstOrDefault();

            if (oferta == null)
                return null;

            decimal precioOriginal = oferta.Productos
                .Sum(x => x.PrecioVentaBase * x.CantidadRequerida);

            decimal precioOferta = oferta.PrecioFinal ?? precioOriginal;

            return new ItemVentaDTO
            {
                ItemId = oferta.OfertaDescuentoId,

                Descripcion = oferta.Descripcion,
                CodigoOferta = oferta.Codigo ?? "Sin Oferta",
                PrecioVenta = precioOriginal,

                PrecioOferta = precioOferta,

                Medida = string.Empty,
                UnidadMedida = string.Empty,

                EsOferta = true,

                TipoOferta = oferta.TipoOferta,

                Cantidad = 0
            };
        }
        //public OfertaDTO ObtenerOfertaActivaPorId(long idOferta)
        //{
        //    using var context = new GestorContextDBFactory().CreateDbContext(null);

        //    return context.OfertasDescuentos
        //        .AsNoTracking()
        //        .Where(x =>
        //            x.OfertaDescuentoId == idOferta &&
        //            x.EstaActiva)
        //        .Select(x => new OfertaDTO
        //        {
        //            OfertaDescuentoId = x.OfertaDescuentoId,
        //            Codigo = x.Codigo,
        //            Descripcion = x.Descripcion,

        //            FechaInicio = x.FechaInicio,
        //            FechaFin = x.FechaFin,

        //            EstaActiva = x.EstaActiva,

        //            TipoOferta = x.TipoOferta,

        //            PorcentajeDescuento = x.PorcentajeDescuento,
        //            PrecioFinal = x.PrecioFinal,

        //            Productos = x.Productos
        //                .Select(p => new ProductosEnOfertaDescuentosDTO
        //                {
        //                    ProductoId = p.ProductoId,

        //                    CantidadRequerida = p.CantidadRequerida,

        //                    PrecioVentaBase = p.PrecioVentaBase,
        //                    PrecioCostoBase = p.PrecioCostoBase,

        //                    PrecioOfertaBase = p.PrecioOfertaBase,

        //                    LimiteVentaProducto = p.LimiteVentaProducto
        //                })
        //                .ToList()
        //        })
        //        .FirstOrDefault();
        //}

        //public InfoOfertaDTO ObtenerInfoOferta()
        //{
        //    using var context = new GestorContextDBFactory().CreateDbContext(null);

        //    var hoy = DateTime.Now.Date;

        //    var ofertas = context.OfertasDescuentos
        //        .AsNoTracking()
        //        .Select(x => new OfertaDTO
        //        {
        //            OfertaDescuentoId = x.OfertaDescuentoId,
        //            Descripcion = x.Descripcion,
        //            Codigo = x.Codigo,
        //            FechaInicio = x.FechaInicio,
        //            FechaFin = x.FechaFin,
        //            EstaActiva = x.EstaActiva,
        //            esOfertaPorGrupo = x.esOfertaPorGrupo,
        //            IdMarca = x.IdMarca,
        //            IdRubro = x.IdRubro,
        //            IdCategoria = x.IdCategoria
        //        })
        //        .ToList();

        //    var ofertasDentroDeFecha = ofertas
        //        .Where(o =>
        //            o.FechaInicio.Date <= hoy &&
        //            (o.FechaFin == null || o.FechaFin.Value.Date >= hoy))
        //        .ToList();

        //    var ofertasActivas = ofertasDentroDeFecha
        //        .Where(o => o.EstaActiva)
        //        .OrderBy(o => o.FechaFin ?? DateTime.MaxValue)
        //        .ToList();

        //    var ofertasInactivas = ofertasDentroDeFecha
        //        .Where(o => !o.EstaActiva)
        //        .ToList();

        //    var codigosActivos = ofertasActivas
        //        .Select(o => o.Codigo)
        //        .Where(c => !string.IsNullOrWhiteSpace(c))
        //        .ToList();

        //    var codigosInactivos = ofertasInactivas
        //        .Select(o => o.Codigo)
        //        .Where(c => !string.IsNullOrWhiteSpace(c))
        //        .ToList();

        //    var ofertasGrupoActivas = ofertasActivas
        //        .Where(o => o.esOfertaPorGrupo)
        //        .ToList();

        //    var grupoMarca = ofertasGrupoActivas.Count(o => o.IdMarca != null);
        //    var grupoCategoria = ofertasGrupoActivas.Count(o => o.IdCategoria != null);
        //    var grupoRubro = ofertasGrupoActivas.Count(o => o.IdRubro != null);

        //    var proximasAVencer = ofertasActivas
        //        .Take(3)
        //        .Select(o => $"{o.Codigo} (vence {o.FechaFin:dd/MM/yyyy})")
        //        .ToList();

        //    // TEXTO PRINCIPAL
        //    var lineasPrincipal = new List<string>();

        //    lineasPrincipal.Add($"Ofertas activas: {ofertasActivas.Count}");

        //    foreach (var codigo in codigosActivos)
        //        lineasPrincipal.Add($"   • {codigo}");

        //    lineasPrincipal.Add($"Ofertas inactivas: {ofertasInactivas.Count}");

        //    foreach (var codigo in codigosInactivos)
        //        lineasPrincipal.Add($"   • {codigo}");

        //    var textoPrincipal = string.Join(Environment.NewLine, lineasPrincipal);

        //    // TEXTO SECUNDARIO
        //    var lineasSecundario = new List<string>();

        //    if (proximasAVencer.Any())
        //    {
        //        lineasSecundario.Add("• Próximas a vencer:");
        //        foreach (var p in proximasAVencer)
        //            lineasSecundario.Add($"   • {p}");
        //    }

        //    if (ofertasGrupoActivas.Any())
        //    {
        //        var partesGrupo = new List<string>();

        //        if (grupoMarca > 0)
        //            partesGrupo.Add($"Marca: {grupoMarca}");

        //        if (grupoCategoria > 0)
        //            partesGrupo.Add($"Categoría: {grupoCategoria}");

        //        if (grupoRubro > 0)
        //            partesGrupo.Add($"Rubro: {grupoRubro}");

        //        lineasSecundario.Add($"• Grupos activos ({string.Join(", ", partesGrupo)})");
        //    }

        //    var textoSecundario = string.Join(Environment.NewLine, lineasSecundario);

        //    return new InfoOfertaDTO
        //    {
        //        Titulo = "Estado de Ofertas",
        //        TextoPrincipal = textoPrincipal,
        //        TextoSecundario = textoSecundario,
        //        Tipo = 1
        //    };
        //}
        //public List<InformacionExistenciaOfertaDescuentoProducto> ObtenerProductosEnOferta(List<ProductoDTO> productosDentroOferta)
        //{
        //    if (productosDentroOferta == null || productosDentroOferta.Count == 0)
        //        return new List<InformacionExistenciaOfertaDescuentoProducto>();

        //    var productoIds = productosDentroOferta
        //        .Where(p => p != null)
        //        .Select(p => p.ProductoId)
        //        .Distinct()
        //        .ToList();

        //    if (productoIds.Count == 0) return new List<InformacionExistenciaOfertaDescuentoProducto>();

        //    using var context = new GestorContextDBFactory().CreateDbContext(null);
        //    //roto 
        //    // Traemos coincidencias y los datos de la oferta (navegación)
        //    var matches = context.ProductosEnOfertasDescuentos
        //        .Where(pe => productoIds.Contains(pe.ProductoId))
        //        .Include(pe => pe.Oferta) // si existe la navegación
        //        .AsNoTracking()
        //        .Select(pe => new InformacionExistenciaOfertaDescuentoProducto
        //        {
        //            ProductoId = pe.ProductoId,
        //            OfertaId = pe.OfertaId, // o pe.OfertaId si se llama distinto
        //            OfertaCodigo = pe.Oferta != null ? pe.Oferta.Codigo : "",
        //            OfertaActiva = pe.Oferta != null && pe.Oferta.EstaActiva,
        //            cantidadProductoEnOferta = pe.Cantidad
        //        })
        //        // Evitamos duplicados exactos (si hay múltiples filas iguales)
        //        .AsEnumerable()
        //        .GroupBy(x => new { x.ProductoId, x.OfertaId })
        //        .Select(g => g.First())
        //        .ToList();

        //    return matches;
        //}

        //public List<OfertaDTO> ObtenerOfertasVencidas(int diasHaciaAtras)
        //{
        //    using var context = new GestorContextDBFactory().CreateDbContext(null);

        //    DateTime fechaLimite = DateTime.Now.AddDays(-diasHaciaAtras);


        //    var ofertasVencidas = context.OfertasDescuentos
        //        .Where(x => x.FechaFin < fechaLimite && x.EstaActiva)//DEBERIA SER SOLO LAS ACTIVAS
        //        .Select(o => new OfertaDTO
        //        {
        //            OfertaDescuentoId = o.OfertaDescuentoId,
        //            Descripcion = o.Descripcion,
        //            Codigo = o.Codigo,
        //            FechaInicio = o.FechaInicio,
        //            FechaFin = o.FechaFin,
        //            EstaActiva = o.EstaActiva,
        //            esOfertaPorGrupo = o.esOfertaPorGrupo,
        //            IdMarca = o.IdMarca,
        //            IdRubro = o.IdRubro,
        //            IdCategoria = o.IdCategoria,
        //            GrupoNombre = o.GrupoNombre
        //        }).ToList();

        //    return ofertasVencidas;
        //}
    }
}
