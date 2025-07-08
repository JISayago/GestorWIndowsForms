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
        public EstadoOperacion ActualizarRolesDeEmpleado(List<RolDTO> rolesAsignados, long empleadoId, DateTime fechaAsignacion)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            try
            {
                // 1. Obtener los roles actualmente asignados al empleado
                var rolesActualesDelEmpleado = context.EmpleadoRoles
                    .Where(er => er.IdEmpleado == empleadoId)
                    .ToList();

                var listadoNuevosRoles = rolesAsignados.Select(r => r.RolId).ToHashSet();
                var listadosRolesPreviamenteAsignados = rolesActualesDelEmpleado.Select(er => er.IdRol).ToHashSet();

                // 2. Determinar los roles a quitar (estaban antes pero no están en la nueva lista)
                var rolesAEliminar = rolesActualesDelEmpleado
                    .Where(er => !listadoNuevosRoles.Contains(er.IdRol))
                    .ToList();

                // 3. Determinar los roles a agregar (están en la nueva lista pero no estaban antes)
                var rolesAAgregar = rolesAsignados
                    .Where(r => !listadosRolesPreviamenteAsignados.Contains(r.RolId))
                    .Select(r => new EmpleadoRol
                    {
                        IdEmpleado = empleadoId,
                        IdRol = r.RolId,
                        FechaAsignacion = fechaAsignacion
                    })
                    .ToList();

                // 4. Ejecutar los cambios
                context.EmpleadoRoles.RemoveRange(rolesAEliminar);
                context.EmpleadoRoles.AddRange(rolesAAgregar);

                context.SaveChanges();

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = $"Se actualizaron los roles del empleado. Asignados: {rolesAAgregar.Count}, Eliminados: {rolesAEliminar.Count}."
                };
            }
            catch (Exception ex)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = $"Error al actualizar roles: {ex.Message}"
                };
            }
        }


        public EstadoOperacion Eliminar(long rolId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);
            var rolEliminar = context.Roles
                    .FirstOrDefault(x => x.RolId == rolId);

            if (rolEliminar == null || rolEliminar.EstaEliminado) throw new Exception($" No se encontro el Rol: {rolEliminar.Nombre}");

            rolEliminar.EstaEliminado = true;

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
                .Where(e => e.Nombre.Contains(cadenabuscar) && !e.EstaEliminado)
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

        public IEnumerable<RolDTO> ObtenerRolesAsignadosAEmpleados(long empleadoId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var rolesAsignados = context.EmpleadoRoles
                .AsNoTracking()
                .Include(ra => ra.Rol)
                .Where(ra => ra.IdEmpleado == empleadoId  && !ra.Rol.EstaEliminado)
                .Select(ra => new RolDTO
                {
                    RolId = ra.IdRol,
                    Nombre = ra.Rol.Nombre,
                    CodigoRol = ra.Rol.CodigoRol,
                    DetalleRol = ra.Rol.DetalleRol,

                })
                .ToList();

            return rolesAsignados;
        }

        public IEnumerable<RolDTO> ObtenerRolesEliminados(string cadenabuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var roles = context.Roles
                .AsNoTracking()
                .Where(e => e.Nombre.Contains(cadenabuscar) && e.EstaEliminado)
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

        public EstadoOperacion QuitarRolesAEmpleado(List<RolDTO> rolesQuitados, long empleadoId)
        {
            throw new NotImplementedException();
        }
    }
}
