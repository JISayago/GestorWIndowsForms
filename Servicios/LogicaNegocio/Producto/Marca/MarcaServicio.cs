using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Articulo.Marca.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Articulo.Marca
{
    public class MarcaServicio : IMarcaServicio
    {
        public EstadoOperacion Eliminar(long marcaId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var marcaEliminar = context.Marcas.FirstOrDefault(x => x.MarcaId == marcaId);


            if (marcaEliminar == null)
                throw new Exception("No se encontró la marca.");

            // Eliminación lógica
            marcaEliminar.EstaEliminado = true;

            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = $"La marca {marcaEliminar.Nombre} fue eliminada correctamente."
            };
        }

        public EstadoOperacion Insertar(MarcaDTO marcaDTO)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            if (context.Marcas.Any(p => p.Nombre == marcaDTO.Nombre))
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una marca con el mismo nombre"
                };

            var nuevaMarca = new AccesoDatos.Entidades.Marca
            {
                Nombre = marcaDTO.Nombre, // o marcaDTO.Descripcion si ese es el campo real
            };

            context.Marcas.Add(nuevaMarca);
            context.SaveChanges(); // Guarda en la DB

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Marca creada correctamente.",
                EntidadId = nuevaMarca.MarcaId
            }; // Devuelve el ID generado
        }

        public EstadoOperacion Modificar(MarcaDTO marcaDTO)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var marcaEditar = context.Marcas.FirstOrDefault(x => x.MarcaId == marcaDTO.Id);

            if (marcaEditar == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Marca no encontrada."
                };
            }

            bool marcaDuplicada = context.Marcas
                .Any(p => p.Nombre == marcaDTO.Nombre && p.Nombre != marcaEditar.Nombre);

            if (marcaDuplicada)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una marca con le mismo nombre."
                };
            }

            // Modificar los campos
            marcaEditar.Nombre = marcaDTO.Nombre;

            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Marca modificada correctamente.",
                EntidadId = marcaEditar.MarcaId
            };
        }



        public MarcaDTO ObtenerPorId(long marca)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var marcaBusqueda = context.Marcas.FirstOrDefault(x => x.MarcaId == marca);

            if (marcaBusqueda == null)
                throw new Exception("No se encontró la marca.");

            return new MarcaDTO
            {
                Id = marcaBusqueda.MarcaId,
                Nombre = marcaBusqueda.Nombre
            };
        }

        public ResultadoPaginacion<MarcaDTO> ObtenerMarcas(FiltroConsulta filtros)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.Marcas
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

                switch (filtros.Filtro1?.ToString())
                {
                    case "Nombre":
                        query = query.Where(x =>
                            x.Nombre.Contains(texto));
                        break;


                    default:
                        query = query.Where(x =>
                            x.Nombre.Contains(texto) ||
                            x.Nombre.Contains(texto));
                        break;
                }
            }

            // 📊 TOTAL
            var total = query.Count();

            // 🔴 PAGINACION SEGURA
            var totalPaginas = (int)Math.Ceiling((double)total / filtros.PageSize);

            if (totalPaginas == 0)
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
                .Select(x => new MarcaDTO
                {
                    Id = x.MarcaId,
                    Nombre = x.Nombre,
                    EstaEliminado = x.EstaEliminado,
                })
                .ToList();

            return new ResultadoPaginacion<MarcaDTO>
            {
                Items = data,
                TotalRegistros = total,
                Page = filtros.Page,
                PageSize = filtros.PageSize
            };
        }
    }
}
