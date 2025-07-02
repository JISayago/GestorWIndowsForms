using AccesoDatos;
using AccesoDatos.Entidades;
using Servicios.Marca.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Marca
{
    public class MarcaServicio : IMarcaServicio
    {
        public void Eliminar(long marcaId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var marca = context.Marcas.FirstOrDefault(x => x.MarcaId == marcaId);

            if (marca == null)
                throw new Exception("No se encontró la marca.");

            // Eliminación lógica
            //marca.EstaEliminado = true;

            context.SaveChanges();
        }

        public long Insertar(MarcaDTO marcaDTO)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var nuevaMarca = new AccesoDatos.Entidades.Marca
            {
                Nombre = marcaDTO.Nombre, // o marcaDTO.Descripcion si ese es el campo real
            };

            context.Marcas.Add(nuevaMarca);
            context.SaveChanges(); // Guarda en la DB

            return nuevaMarca.MarcaId; // Devuelve el ID generado
        }

        public void Modificar(MarcaDTO marcaDTO)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var marca = context.Marcas.FirstOrDefault(x => x.MarcaId == marcaDTO.Id);

            if (marca == null)
                throw new Exception("No se encontró la marca.");

            // Modificar los campos
            marca.Nombre = marcaDTO.Nombre;

            context.SaveChanges();
        }

        public IEnumerable<MarcaDTO> ObtenerMarca(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.Marcas
                .Where(x => /*!x.EstaEliminado &&*/ x.Nombre.Contains(cadenaBuscar))
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
                .Where(x => /*x.EstaEliminado &&*/ x.Nombre.Contains(cadenaBuscar))
                .Select(x => new MarcaDTO
                {
                    Id = x.MarcaId,
                    Nombre = x.Nombre
                })
                .ToList();
        }

        public MarcaDTO ObtenerPorId(long marcaId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var marca = context.Marcas.FirstOrDefault(x => x.MarcaId == marcaId);

            /*if (marca == null)
                throw new Exception("No se encontró la marca.");
            */
            return new MarcaDTO
            {
                Id = marca.MarcaId,
                Nombre = marca.Nombre
            };
        }
    }
}
