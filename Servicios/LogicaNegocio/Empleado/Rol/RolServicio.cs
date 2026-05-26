using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Empleado.DTO;
using Servicios.LogicaNegocio.Empleado.Rol.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ResultadoPaginacion<RolDTO> ObtenerRoles(FiltroConsulta filtros)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            string collation = "Latin1_General_CI_AI";
            var query = context.Roles
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
                            x.Nombre != null &&
                            EF.Functions.Collate(x.Nombre, collation)
                                .Contains(texto));

                        break;

                    case "DetalleRol":

                        query = query.Where(x =>
                            x.DetalleRol != null &&
                            EF.Functions.Collate(x.DetalleRol, collation)
                                .Contains(texto));

                        break;

                    case "CodigoRol":

                        query = query.Where(x =>
                            x.CodigoRol != null &&
                            EF.Functions.Collate(x.CodigoRol, collation)
                                .Contains(texto));

                        break;

                    default:

                        query = query.Where(x =>
                            (x.Nombre != null &&
                             EF.Functions.Collate(x.Nombre, collation).Contains(texto))
                            ||
                            (x.DetalleRol != null &&
                             EF.Functions.Collate(x.DetalleRol, collation).Contains(texto))
                            ||
                            (x.CodigoRol != null &&
                             EF.Functions.Collate(x.CodigoRol, collation).Contains(texto)));

                        break;
                }
            }

            // 📊 TOTAL
            var total = query.Count();

            // 🔴 CONTROL PAGINACION
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
                .Select(x => new RolDTO
                {
                    RolId = x.RolId,

                    Nombre = x.Nombre,

                    CodigoRol = x.CodigoRol,

                    DetalleRol = x.DetalleRol
                })
                .ToList();

            return new ResultadoPaginacion<RolDTO>
            {
                Items = data,
                TotalRegistros = total,
                Page = filtros.Page,
                PageSize = filtros.PageSize
            };
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
