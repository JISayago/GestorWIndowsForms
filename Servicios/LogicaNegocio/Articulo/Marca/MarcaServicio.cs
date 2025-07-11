using AccesoDatos;
using AccesoDatos.Entidades;
using Servicios.Helpers;
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

        public IEnumerable<MarcaDTO> ObtenerMarca(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.Marcas
                .Where(x => !x.EstaEliminado && x.Nombre.Contains(cadenaBuscar))
                .Select(x => new MarcaDTO
                {
                    Id = x.MarcaId,
                    Nombre = x.Nombre
                })
                .ToList();
        }

        public IEnumerable<MarcaDTO> ObtenerMarcaEliminada(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.Marcas
                .Where(x => x.EstaEliminado && x.Nombre.Contains(cadenaBuscar))
                .Select(x => new MarcaDTO
                {
                    Id = x.MarcaId,
                    Nombre = x.Nombre
                })
                .ToList();
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
    }
}
