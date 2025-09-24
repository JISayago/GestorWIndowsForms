using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Producto.DTO;
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
                //IdRubro = productoDto.IdRubro,
                IdRubro = 1,
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

        public IEnumerable<ProductosEnOfertaDescuentosDTO> ObtenerProductosPorMarcaRubroCategoriaParaOferta(
          long? MarcaId = null, long? RubroId = null, long? CategoriaId = null)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.Productos
                .AsNoTracking()
                .Where(p => !p.EstaEliminado); 

            if (MarcaId.HasValue)
                query = query.Where(p => p.IdMarca == MarcaId.Value);

            if (RubroId.HasValue)
                query = query.Where(p => p.IdRubro == RubroId.Value);

            if (CategoriaId.HasValue)
                query = query.Where(p => p.CategoriasProductos.Any(cp => cp.IdCategoria == CategoriaId.Value));

            var productos = query
                .Select(p => new ProductosEnOfertaDescuentosDTO
                {
                    ProductoOfertaId = p.ProductoId,
                    Cantidad = p.Stock,
                    Codigo = p.Codigo,
                    CodigoBarra = p.CodigoBarra,
                    PrecioVenta = p.PrecioVenta,
                    PrecioCosto = p.PrecioCosto,
                    Descripcion = p.Descripcion
                })
                .ToList();

            return productos;
        }
    }
}
