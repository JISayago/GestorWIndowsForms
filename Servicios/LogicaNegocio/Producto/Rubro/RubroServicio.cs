using AccesoDatos;
using Servicios.Helpers;
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
