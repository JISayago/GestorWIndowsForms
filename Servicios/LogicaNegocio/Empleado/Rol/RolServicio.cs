using AccesoDatos.Entidades;
using AccesoDatos;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Empleado.DTO;
using Servicios.LogicaNegocio.Empleado.Rol.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Servicios.LogicaNegocio.Empleado.Rol
{
    public class RolServicio : IRolServicio
    {
        public EstadoOperacion Eliminar(long rolId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);
            var rolEliminar = context.Roles
                    .FirstOrDefault(x => x.RolId == rolId);

            if (rolEliminar == null /*|| rolEliminar.EstaEliminado*/) throw new Exception($" No se encontro el Rol: {rolEliminar.Nombre}");

            //rolEliminar.EstaEliminado = true;

            context.SaveChanges();
            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = $"El Rol {rolEliminar.Nombre} con el n° de referencia:({rolEliminar.CodigoRol}), fue eliminado correctamente."
            };
        }

        public EstadoOperacion Insertar(RolDTO rolDto)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            if (context.Roles.Any(r => r.CodigoRol == rolDto.CodigoRol))
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una Rol con el mismo Codigo."
                };

            var rol = new AccesoDatos.Entidades.Rol
            {
                Nombre = rolDto.Nombre,
                DetalleRol = rolDto.DetalleRol,
                CodigoRol = rolDto.CodigoRol,
               
            };

            context.Roles.Add(rol);
            context.SaveChanges();
            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Rol creado correctamente.",
                EntidadId = rol.RolId
            };
        }

        public EstadoOperacion Modificar(RolDTO rolDto, long? rolId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            var rolEditar = context.Roles
                .FirstOrDefault(x => x.RolId == rolId);

            if (rolId == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Rol no encontrado."
                };
            }
           
            rolEditar.Nombre = rolDto.Nombre;
            rolEditar.CodigoRol = rolDto.CodigoRol;
            rolEditar.DetalleRol = rolDto.DetalleRol;

            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Rol modificado correctamente.",
                EntidadId = rolEditar.RolId
            };
        }

        public IEnumerable<RolDTO> ObtenerRoles(string cadenabuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var roles = context.Roles
                .AsNoTracking()
                .Where(e => e.Nombre.Contains(cadenabuscar)) // || e.CodigoRol.Contains(cadenabuscar) !e.EstaEliminado
                .Select(e => new RolDTO
                {
                    RolId = e.RolId,
                    Nombre = e.Nombre,
                    CodigoRol = e.CodigoRol,
                    DetalleRol = e.DetalleRol,
                    
                })
                .ToList();

            return roles;
        }

        public IEnumerable<RolDTO> ObtenerRolesEliminados(string cadenabuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var roles = context.Roles
                .AsNoTracking()
                .Where(e => e.Nombre.Contains(cadenabuscar)) // || e.CodigoRol.Contains(cadenabuscar) e.EstaEliminado)
                .Select(e => new RolDTO
                {
                    RolId = e.RolId,
                    Nombre = e.Nombre,
                    CodigoRol = e.CodigoRol,
                    DetalleRol = e.DetalleRol,

                })
                .ToList();

            return roles;
        }

        public RolDTO ObtenerRolPorId(long rolId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var rol = context.Roles
                 .AsNoTracking()
                 .Where(e => e.RolId == rolId /*&& !e.EstaEliminado*/)
                 .Select(e => new RolDTO
                 {
                     RolId = e.RolId,
                     Nombre = e.Nombre,
                     CodigoRol = e.CodigoRol,
                     DetalleRol = e.DetalleRol,
                 })
                 .FirstOrDefault();
            return rol;
        }
    }
}
