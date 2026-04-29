using AccesoDatos;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Producto.Rubro.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Producto.Rubro
{
    public class RubroServicio : IRubroServicio
    {
        public EstadoOperacion Eliminar(long rubroId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var rubroEliminar = context.Rubros.FirstOrDefault(x => x.RubroId == rubroId);


            if (rubroEliminar == null)
                throw new Exception("No se encontró la rubro.");

            // Eliminación lógica
            rubroEliminar.EstaEliminado = true;

            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = $"La rubro {rubroEliminar.Nombre} fue eliminada correctamente."
            };
        }

        public EstadoOperacion Insertar(RubroDTO rubroDTO)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            if (context.Rubros.Any(p => p.Nombre == rubroDTO.Nombre))
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una rubro con el mismo nombre"
                };

            var nuevaRubro = new AccesoDatos.Entidades.Rubro
            {
                Nombre = rubroDTO.Nombre, // o rubroDTO.Descripcion si ese es el campo real
            };

            context.Rubros.Add(nuevaRubro);
            context.SaveChanges(); // Guarda en la DB

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Rubro creada correctamente.",
                EntidadId = nuevaRubro.RubroId
            }; // Devuelve el ID generado
        }

        public EstadoOperacion Modificar(RubroDTO rubroDTO)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var rubroEditar = context.Rubros.FirstOrDefault(x => x.RubroId == rubroDTO.Id);

            if (rubroEditar == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Rubro no encontrada."
                };
            }

            bool rubroDuplicada = context.Rubros
                .Any(p => p.Nombre == rubroDTO.Nombre && p.Nombre != rubroEditar.Nombre);

            if (rubroDuplicada)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una rubro con le mismo nombre."
                };
            }

            // Modificar los campos
            rubroEditar.Nombre = rubroDTO.Nombre;

            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Rubro modificada correctamente.",
                EntidadId = rubroEditar.RubroId
            };
        }

        public IEnumerable<RubroDTO> ObtenerRubro(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.Rubros
                .Where(x => !x.EstaEliminado && x.Nombre.Contains(cadenaBuscar))
                .Select(x => new RubroDTO
                {
                    Id = x.RubroId,
                    Nombre = x.Nombre
                })
                .ToList();
        }

        public ResultadoPaginacion<RubroDTO> ObtenerRubroPaginado(FiltroConsulta filtros)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            // 1. Creamos la consulta base (IQueryable) sin ejecutarla aún
            var query = context.Rubros
                .AsNoTracking()
                .Where(x => !x.EstaEliminado)
                .AsQueryable();

            // 2. Aplicamos filtros de búsqueda si existen
            if (!string.IsNullOrWhiteSpace(filtros.TextoBuscar))
            {
                query = query.Where(x => x.Nombre.Contains(filtros.TextoBuscar));
            }

            // 3. Contamos el total de registros antes de paginar
            var total = query.Count();

            // 4. Lógica de PAGINACIÓN SEGURA (Cálculo de páginas y límites)
            var totalPaginas = (int)Math.Ceiling((double)total / filtros.PageSize);
            if (totalPaginas == 0) totalPaginas = 1;

            if (filtros.Page > totalPaginas)
                filtros.Page = totalPaginas;

            if (filtros.Page < 1)
                filtros.Page = 1;

            // 5. Ejecutamos la consulta con Skip y Take
            var data = query
                .OrderBy(x => x.Nombre) // Es importante ordenar para que la paginación sea consistente
                .Skip((filtros.Page - 1) * filtros.PageSize)
                .Take(filtros.PageSize)
                .Select(x => new RubroDTO
                {
                    Id = x.RubroId,
                    Nombre = x.Nombre
                })
                .ToList();

            // 6. Retornamos el objeto con la estructura de paginación
            return new ResultadoPaginacion<RubroDTO>
            {
                Items = data,
                TotalRegistros = total,
                Page = filtros.Page,
                PageSize = filtros.PageSize
            };
        }

        public IEnumerable<RubroDTO> ObtenerRubroEliminado(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.Rubros
                .Where(x => x.EstaEliminado && x.Nombre.Contains(cadenaBuscar))
                .Select(x => new RubroDTO
                {
                    Id = x.RubroId,
                    Nombre = x.Nombre
                })
                .ToList();
        }

        public RubroDTO ObtenerPorId(long rubro)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var rubroBusqueda = context.Rubros.FirstOrDefault(x => x.RubroId == rubro);

            if (rubroBusqueda == null)
                throw new Exception("No se encontró la rubro.");

            return new RubroDTO
            {
                Id = rubroBusqueda.RubroId,
                Nombre = rubroBusqueda.Nombre
            };
        }
    }
}
