using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Articulo.Categoria.DTO;
using Servicios.LogicaNegocio.Empleado.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Articulo.Categoria
{
    public class CategoriaServicio : ICategoriaServicio
    {
        public EstadoOperacion Eliminar(long categoriaId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var categoriaEliminar = context.Categorias.FirstOrDefault(x => x.CategoriaId == categoriaId);

            if (categoriaEliminar == null)
                throw new Exception("No se encontró la categoria.");

            // Eliminación lógica
            categoriaEliminar.EstaEliminado = true;

            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = $"La categoria {categoriaEliminar.Nombre} fue eliminada correctamente."
            };
        }

        public EstadoOperacion Insertar(CategoriaDTO categoriaDTO)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            if (context.Categorias.Any(p => p.Nombre == categoriaDTO.Nombre))
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una categoria con el mismo nombre"
                };

            var nuevaCategoria = new AccesoDatos.Entidades.Categoria
            {
                Nombre = categoriaDTO.Nombre, // o marcaDTO.Descripcion si ese es el campo real
            };

            context.Categorias.Add(nuevaCategoria);
            context.SaveChanges(); // Guarda en la DB

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Categoria creada correctamente.",
                EntidadId = nuevaCategoria.CategoriaId
            };
        }

        public EstadoOperacion Modificar(CategoriaDTO categoriaDTO)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var categoriaEditar = context.Categorias.FirstOrDefault(x => x.CategoriaId == categoriaDTO.Id);

            if (categoriaEditar == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Categoria no encontrada."
                };
            }

            bool categoriaDuplicada = context.Categorias
                .Any(p => p.Nombre == categoriaDTO.Nombre && p.Nombre != categoriaEditar.Nombre);

            if (categoriaDuplicada)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una categoria con le mismo nombre."
                };
            }

            // Modificar los campos
            categoriaEditar.Nombre = categoriaDTO.Nombre;

            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Categoria modificada correctamente.",
                EntidadId = categoriaEditar.CategoriaId
            };
        }
        public CategoriaDTO ObtenerPorId(long categoriaId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var categoria = context.Categorias.FirstOrDefault(x => x.CategoriaId == categoriaId);

            if (categoria == null)
                throw new Exception("No se encontró la marca.");
            
            return new CategoriaDTO
            {
                Id = categoria.CategoriaId,
                Nombre = categoria.Nombre
            };
        }
        public ResultadoPaginacion<CategoriaDTO> ObtenerCategorias(FiltroConsulta filtros)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            string collation = "Latin1_General_CI_AI";
            var query = context.Categorias
                .AsNoTracking()
                .AsQueryable();

            // 🔴 ELIMINADOS
            query = filtros.Bool1
                ? query.Where(x => x.EstaEliminado)
                : query.Where(x => !x.EstaEliminado);

            // 🔍 BUSQUEDA
            if (!string.IsNullOrWhiteSpace(filtros.TextoBuscar))
            {
                var texto = filtros.TextoBuscar.Trim();

                query = query.Where(x =>
                    EF.Functions.Collate(x.Nombre, collation)
                        .Contains(texto));
            }

            // 📊 TOTAL
            var total = query.Count();

            // 🔴 PAGINACION SEGURA
            var totalPaginas = (int)Math.Ceiling((double)total / filtros.PageSize);

            if (totalPaginas <= 0)
                totalPaginas = 1;

            if (filtros.Page > totalPaginas)
                filtros.Page = totalPaginas;

            if (filtros.Page < 1)
                filtros.Page = 1;

            // 🔽 ORDEN
            query = query.OrderBy(x => x.Nombre);

            // 📄 DATA
            var data = query
                .Skip((filtros.Page - 1) * filtros.PageSize)
                .Take(filtros.PageSize)
                .Select(x => new CategoriaDTO
                {
                    Id = x.CategoriaId,
                    Nombre = x.Nombre,
                    EstaEliminado = x.EstaEliminado
                })
                .ToList();

            return new ResultadoPaginacion<CategoriaDTO>
            {
                Items = data,
                TotalRegistros = total,
                Page = filtros.Page,
                PageSize = filtros.PageSize
            };
        }
    }
}
