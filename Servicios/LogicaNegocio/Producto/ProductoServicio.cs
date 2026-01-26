using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Venta.DTO;
using Servicios.LogicaNegocio.Venta.Oferta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Producto
{
    public class ProductoServicio : IProductoServicio
    {
        public ProductosEnOfertaDescuentosDTO?  ControlarProductoEstaEnOfertaPorId(long productoId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            // Intentamos buscar primero la relación producto-en-oferta (puede ser null)
            var prodOfer = context.ProductosEnOfertasDescuentos
                .AsNoTracking()
                .Include(x => x.Producto)
                .Include(x => x.Oferta)
                .FirstOrDefault(x => x.ProductoId == productoId);

            // Si no hay relación, traigo el producto directamente (con sus navegaciones necesarias)
            var producto = prodOfer?.Producto
                           ?? context.Productos
                                .Include(x => x.Marca)
                                .Include(x => x.Rubro)
                                .Include(x => x.CategoriasProductos)
                                .AsNoTracking()
                                .FirstOrDefault(p => p.ProductoId == productoId);

            if (producto == null)
                return null; // No existe el producto

            // Solo busco la oferta si prodOfer existe y tiene un Id de oferta válido
            OfertaDescuento? oferta = null;

            if (prodOfer != null && prodOfer.OfertaId > 0) // ajuste según el tipo real de OfertaId
            {
                oferta = context.OfertasDescuentos
                    .Include(o => o.Marca)
                    .Include(o => o.Rubro)
                    .Include(o => o.Categoria)
                    .AsNoTracking()
                    .FirstOrDefault(o => o.OfertaDescuentoId == prodOfer.OfertaId);
            }

            // Aquí NO fallamos si oferta == null: devolvemos el DTO con Oferta = null
            var dto = new ProductosEnOfertaDescuentosDTO
            {
                // Si prodOfer es null usamos el id del producto; si existe, usamos los valores de prodOfer
                ProductoOfertaId = prodOfer?.ProductoId ?? producto.ProductoId,
                Cantidad = prodOfer?.Cantidad ?? 0m,
                PrecioEnOferta = prodOfer?.PrecioConDescuento ?? 0m,
                Oferta = oferta == null ? null : new OfertaDTO
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
                },
                Producto = new ProductoDTO
                {
                    ProductoId = producto.ProductoId,
                    IdMarca = producto.IdMarca,
                    IdRubro = producto.IdRubro,
                    Stock = producto.Stock,
                    PrecioCosto = producto.PrecioCosto,
                    PrecioVenta = producto.PrecioVenta,
                    Descripcion = producto.Descripcion,
                    EstaEliminado = producto.EstaEliminado,
                    Estado = producto.Estado,
                    Medida = producto.Medida,
                    UnidadMedida = producto.UnidadMedida,
                    Codigo = producto.Codigo,
                    CodigoBarra = producto.CodigoBarra,
                    IvaIncluidoPrecioFinal = producto.IvaIncluidoPrecioFinal,
                    MarcaNombre = producto.Marca?.Nombre,
                    RubroNombre = producto.Rubro?.Nombre,
                    EsFraccionable = producto.EsFraccionable,
                    CategoriaIds = producto.CategoriasProductos?.Select(c => c.IdCategoria).ToList() ?? new List<long>()
                }
            };

            return dto;
        }


        public void DescontarStockProductos(List<ItemVentaDTO> items, GestorContextDB context)
        {
            if (items == null || !items.Any())
                return;

            foreach (var item in items)
            {
                if (item.EsOferta)
                {
                   // DescontarStockOferta(item, context);
                }
                else
                {
                    DescontarStockProducto(item, context);
                }
            }
        }
     
        private void DescontarStockProducto(ItemVentaDTO item, GestorContextDB context)
        {
            var producto = context.Productos
                .FirstOrDefault(p => p.ProductoId == item.ItemId);

            if (producto == null)
                throw new Exception($"Producto no encontrado. {item.Descripcion}");

            if (producto.Stock < item.Cantidad)
                throw new Exception(
                    $"Stock insuficiente para {producto.Descripcion}. Stock actual: {producto.Stock}"
                );

            producto.Stock -= item.Cantidad;

            context.Productos.Update(producto); 
        }
        public void RestaurarStockProductos(List<ItemVentaDTO> items, GestorContextDB context)
        {
            if (items == null || !items.Any())
                return;

            foreach (var item in items)
            {
                if (item.EsOferta)
                {
                    // RestaurarStockOferta(item, context);
                }
                else
                {
                    RestaurarStockProducto(item, context);
                }
            }
        }
        private void RestaurarStockProducto(ItemVentaDTO item, GestorContextDB context)
        {
            var producto = context.Productos
                .FirstOrDefault(p => p.ProductoId == item.ItemId);

            if (producto == null)
                throw new Exception($"Producto no encontrado. {item.Descripcion}");

            producto.Stock += item.Cantidad;

            context.Productos.Update(producto);
        }

        /* private void DescontarStockOferta(ItemVentaDTO item, GestorContextDB context)
         {
             var oferta = context.OfertasDescuentos
                 .Include(o => o.Descripcion)
                 .FirstOrDefault(o => o.OfertaDescuentoId == item.ItemId);

             if (oferta == null)
                 throw new Exception($"Oferta no encontrada. ID {item.ItemId}");

             foreach (var detalle in oferta.Descripcion)
             {
                 var producto = context.Productos
                     .FirstOrDefault(p => p.ProductoId == detalle.);

                 if (producto == null)
                     throw new Exception($"Producto {detalle.ProductoId} de la oferta no existe");

                 var cantidadADescontar = detalle.Cantidad * item.Cantidad;

                 if (producto.Stock < cantidadADescontar)
                     throw new Exception(
                         $"Stock insuficiente para {producto.Descripcion} (oferta)"
                     );

                 producto.Stock -= cantidadADescontar;
             }
         }*/

        public EstadoOperacion Eliminar(long productoId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);
            var productoEliminar = context.Productos
                    .FirstOrDefault(x => x.ProductoId == productoId);

            if (productoEliminar == null) 
                throw new Exception($" No se encontro el Producto");

            productoEliminar.EstaEliminado = true;

            context.SaveChanges();
            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = $"El producto {productoEliminar.Descripcion} fue eliminado correctamente."
            };
        }

        public EstadoOperacion Insertar(ProductoDTO productoDto)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            if (context.Productos.Any(p => p.Descripcion == productoDto.Descripcion))
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe un producto con el mismo nombre."
                };

            var producto = new AccesoDatos.Entidades.Producto{
                Stock = productoDto.Stock,
                PrecioCosto = productoDto.PrecioCosto,
                PrecioVenta = productoDto.PrecioVenta,
                Descripcion = productoDto.Descripcion,
                EstaEliminado = false,
                Estado = productoDto.Estado,
                Medida = productoDto.Medida,
                UnidadMedida = productoDto.UnidadMedida,
                IdMarca = productoDto.IdMarca,
                IdRubro = productoDto.IdRubro,
                Codigo = productoDto.Codigo ?? "01",
                CodigoBarra = productoDto.CodigoBarra ?? "01",
                IvaIncluidoPrecioFinal = productoDto.IvaIncluidoPrecioFinal,
                EsFraccionable = productoDto.EsFraccionable,
                CategoriasProductos = productoDto.CategoriaIds
                    .Select(id => new CategoriaProducto
                    {
                        IdCategoria = id
                        // ProductoId se setea automáticamente si EF está trackeando al Producto 
                    })
                    .ToList()
            };

            context.Productos.Add(producto);
            context.SaveChanges();
            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Producto creado correctamente.",
                EntidadId = producto.ProductoId
            };
        }

        public EstadoOperacion Modificar(ProductoDTO productoDto, long? productoId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            var productoEditar = context.Productos
                .Include(x => x.Marca)
                .Include(x => x.CategoriasProductos)
                .FirstOrDefault(x => x.ProductoId == productoId);

            if (productoEditar == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Producto no encontrado."
                };
            }

            //bool productoDuplicado ??

            productoEditar.Stock = productoDto.Stock;
            productoEditar.PrecioCosto = productoDto.PrecioCosto;
            productoEditar.PrecioVenta = productoDto.PrecioVenta;
            productoEditar.Descripcion = productoDto.Descripcion;
            productoEditar.EstaEliminado = productoDto.EstaEliminado;
            productoEditar.Estado = productoDto.Estado;
            productoEditar.Medida = productoDto.Medida;
            productoEditar.UnidadMedida = productoDto.UnidadMedida;
            productoEditar.IdMarca = productoDto.IdMarca;
            productoEditar.IdRubro = productoDto.IdRubro;
            productoEditar.Codigo = productoDto.Codigo;
            productoEditar.CodigoBarra = productoDto.CodigoBarra;
            productoEditar.IvaIncluidoPrecioFinal = productoDto.IvaIncluidoPrecioFinal;
            productoEditar.EsFraccionable = productoDto.EsFraccionable;

            foreach (var categoriaId in productoDto.CategoriaIds)
            {
                productoEditar.CategoriasProductos.Add(new CategoriaProducto
                {
                    IdProducto = productoEditar.ProductoId,
                    IdCategoria = categoriaId
                });
            };

            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Producto modificado correctamente.",
                EntidadId = productoEditar.ProductoId
            };
        }

        public ProductoDTO ObtenerProductoPorId(long productoId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var producto = context.Productos
                 .AsNoTracking()
                 .Include(e => e.Marca)
                 .Include(e => e.CategoriasProductos)
                 .Where(e => e.ProductoId == productoId && !e.EstaEliminado)
                 .Select(e => new ProductoDTO
                 {
                     ProductoId = e.ProductoId,
                     IdMarca = e.IdMarca,
                     IdRubro = e.IdRubro,
                     MarcaNombre = e.Marca.Nombre,
                     RubroNombre = e.Rubro.Nombre,
                     Stock = Convert.ToDecimal(e.Stock),
                     PrecioCosto = e.PrecioCosto,
                     PrecioVenta = e.PrecioVenta,
                     Descripcion = e.Descripcion,
                     EstaEliminado = e.EstaEliminado,
                     Estado = e.Estado,
                     Medida = e.Medida,
                     UnidadMedida = e.UnidadMedida,
                     Codigo = e.Codigo,
                     CodigoBarra = e.CodigoBarra,
                     IvaIncluidoPrecioFinal = e.IvaIncluidoPrecioFinal,
                     EsFraccionable = e.EsFraccionable,
                     CategoriaIds = e.CategoriasProductos
                        .Select(cp => cp.IdCategoria)
                        .ToList()
                 })
                 .FirstOrDefault();

            return producto;
        }

        public IEnumerable<ProductoDTO> ObtenerProductos(string cadenabuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var producto = context.Productos
            .AsNoTracking()
            .Include(e => e.Marca)
            .Include(e => e.Rubro)
            .Include(e => e.CategoriasProductos)
            .Where(e => !e.EstaEliminado && (e.Descripcion.Contains(cadenabuscar) || e.Marca.Nombre.Contains(cadenabuscar)))
            .ToList()
            .Select(e => new ProductoDTO
            {
                ProductoId = e.ProductoId,
                IdMarca = e.IdMarca,
                IdRubro = e.IdRubro,
                MarcaNombre = e.Marca.Nombre,
                RubroNombre = e.Rubro.Nombre,
                Stock = Convert.ToDecimal(e.Stock),
                PrecioCosto = e.PrecioCosto,
                PrecioVenta = e.PrecioVenta,
                Descripcion = e.Descripcion,
                EstaEliminado = e.EstaEliminado,
                Estado = e.Estado,
                Medida = e.Medida,
                UnidadMedida = e.UnidadMedida,
                Codigo = e.Codigo,
                CodigoBarra = e.CodigoBarra,
                IvaIncluidoPrecioFinal = e.IvaIncluidoPrecioFinal,
                EsFraccionable = e.EsFraccionable,
                CategoriaIds = e.CategoriasProductos
                        .Select(cp => cp.IdCategoria)
                        .ToList()
            })
            .ToList();

            return producto;
        }

        public IEnumerable<ProductoDTO> ObtenerProductosEliminados(string cadenabuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var producto = context.Productos
            .AsNoTracking()
            .Include(e => e.Marca)
            .Include(e => e.CategoriasProductos)
            .Where(e => e.EstaEliminado && (e.Descripcion.Contains(cadenabuscar) || e.Marca.Nombre.Contains(cadenabuscar)))
            .ToList()
            .Select(e => new ProductoDTO
            {
                ProductoId = e.ProductoId,
                IdMarca = e.IdMarca,
                IdRubro = e.IdRubro,
                MarcaNombre = e.Marca.Nombre,
                RubroNombre = e.Rubro.Nombre,
                Stock = Convert.ToDecimal(e.Stock),
                PrecioCosto = e.PrecioCosto,
                PrecioVenta = e.PrecioVenta,
                Descripcion = e.Descripcion,
                EstaEliminado = e.EstaEliminado,
                Estado = e.Estado,
                Medida = e.Medida,
                UnidadMedida = e.UnidadMedida,
                Codigo = e.Codigo,
                CodigoBarra = e.CodigoBarra,
                IvaIncluidoPrecioFinal = e.IvaIncluidoPrecioFinal,
                EsFraccionable = e.EsFraccionable,
                CategoriaIds = e.CategoriasProductos
                        .Select(cp => cp.IdCategoria)
                        .ToList()
            })
            .ToList();

            return producto;
        }

        public IEnumerable<ProductoDTO> ObtenerProductosPorMarcaRubroCategoriaParaOferta(
     long? MarcaId = null, long? RubroId = null, long? CategoriaId = null)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            // Partimos de la entidad Productos (solo productos, sin considerar tablas de ofertas)
            var query = context.Productos
                .AsNoTracking()
                .Include(p => p.Marca)
                .Include(p => p.Rubro)
                .Include(p => p.CategoriasProductos)
                .Where(p => !p.EstaEliminado); // solo productos activos

            if (MarcaId.HasValue)
                query = query.Where(p => p.IdMarca == MarcaId.Value);

            if (RubroId.HasValue)
                query = query.Where(p => p.IdRubro == RubroId.Value);

            if (CategoriaId.HasValue)
                query = query.Where(p => p.CategoriasProductos.Any(cp => cp.IdCategoria == CategoriaId.Value));

            // Proyección directa a DTO
            var productos = query
                .Select(p => new ProductoDTO
                {
                    ProductoId = p.ProductoId,
                    IdMarca = p.IdMarca,
                    IdRubro = p.IdRubro,
                    Stock = p.Stock,
                    PrecioCosto = p.PrecioCosto,
                    PrecioVenta = p.PrecioVenta,
                    Descripcion = p.Descripcion,
                    EstaEliminado = p.EstaEliminado,
                    Estado = p.Estado,
                    Medida = p.Medida,
                    UnidadMedida = p.UnidadMedida,
                    Codigo = p.Codigo,
                    CodigoBarra = p.CodigoBarra,
                    IvaIncluidoPrecioFinal = p.IvaIncluidoPrecioFinal,
                    MarcaNombre = p.Marca != null ? p.Marca.Nombre : null,
                    RubroNombre = p.Rubro != null ? p.Rubro.Nombre : null,
                    EsFraccionable = p.EsFraccionable,
                    CategoriaIds = p.CategoriasProductos != null
                        ? p.CategoriasProductos.Select(cp => cp.IdCategoria).ToList()
                        : new List<long>()
                })
                .ToList();

            return productos;
        }

        
    }
}
