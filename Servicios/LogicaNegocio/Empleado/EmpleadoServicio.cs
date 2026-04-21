using AccesoDatos;
using AccesoDatos.Config;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Producto;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.Admin;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Empleado.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Servicios.LogicaNegocio.Empleado
{
    public class EmpleadoServicio : IEmpleadoServicio
    {

        
        public EstadoOperacion Eliminar(long empleadoId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);
            var empleadoEliminar = context.Empleados
                    .Include(e => e.Persona)
                    .FirstOrDefault(x => x.PersonaId == empleadoId);

                if (empleadoEliminar == null || empleadoEliminar.Persona.EstaEliminado) throw new Exception($" No se encontro el Empleado: {empleadoEliminar.Persona}");

                empleadoEliminar.Persona.EstaEliminado = true;

                context.SaveChanges();
            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = $"El empleado {empleadoEliminar.Persona.Nombre} {empleadoEliminar.Persona.Apellido} fue eliminado correctamente."
            };
        }

        public EstadoOperacion Insertar(EmpleadoDTO empleadoDto)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            if (context.Personas.Any(p => p.Dni == empleadoDto.Dni))
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una persona con el mismo DNI."
                };

            var persona = new Persona
            {
                Nombre = empleadoDto.Nombre,
                Apellido = empleadoDto.Apellido,
                Dni = empleadoDto.Dni,
                Cuil = empleadoDto.Cuil,
                Telefono = empleadoDto.Telefono,
                Telefono2 = empleadoDto.Telefono2,
                Email = empleadoDto.Email,
                Direccion = empleadoDto.Direccion,
                EstaEliminado = false,
                FechaNacimiento = empleadoDto.FechaNacimiento
            };

            context.Personas.Add(persona);
            context.SaveChanges(); 
                        
            var empleado = new AccesoDatos.Entidades.Empleado
            {
                PersonaId = persona.PersonaId,
                Legajo = empleadoDto.Legajo,
                FechaIngreso = empleadoDto.FechaIngreso,
                Estado = 0,
                Username = null,
                Pass = null,
                UsuarioEstaHabilitado = false
            };

            context.Empleados.Add(empleado);
            context.SaveChanges();
            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Empleado creado correctamente.",
                EntidadId = empleado.PersonaId
            };

        }

        public EstadoOperacion Modificar(EmpleadoDTO empleadoDto, long? empleadoId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            var empleadoEditar = context.Empleados
                .Include(e => e.Persona)
                .FirstOrDefault(x => x.PersonaId == empleadoId);

            if (empleadoEditar == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Empleado no encontrado."
                };
            }

            bool dniDuplicado = context.Personas
                .Any(p => p.Dni == empleadoDto.Dni && p.PersonaId != empleadoEditar.PersonaId);

            if (dniDuplicado)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una persona con el mismo DNI."
                };
            }

            var persona = empleadoEditar.Persona;
            persona.Nombre = empleadoDto.Nombre;
            persona.Apellido = empleadoDto.Apellido;
            persona.Dni = empleadoDto.Dni;
            persona.Cuil = empleadoDto.Cuil;
            persona.Telefono = empleadoDto.Telefono;
            persona.Telefono2 = empleadoDto.Telefono2;
            persona.Email = empleadoDto.Email;
            persona.Direccion = empleadoDto.Direccion;
            persona.EstaEliminado = empleadoDto.EstaEliminado;
            persona.FechaNacimiento = empleadoDto.FechaNacimiento;

            empleadoEditar.Legajo = empleadoDto.Legajo;
            empleadoEditar.FechaIngreso = empleadoDto.FechaIngreso;
            empleadoEditar.FechaEgreso = empleadoDto.FechaEgreso;
            empleadoEditar.UsuarioEstaHabilitado = empleadoDto.UsuarioEstaHabilitado;


            
            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Empleado modificado correctamente.",
                EntidadId = empleadoEditar.PersonaId
            };
        }

        public EmpleadoDTO ObtenerEmpleadoPorId(long personaId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var empleado = context.Empleados
                 .AsNoTracking()
                 .Include(e => e.Persona)
                 .Where(e => e.Persona != null && e.PersonaId == personaId && !e.Persona.EstaEliminado)
                 .Select(e => new EmpleadoDTO
                 {
                     PersonaId = e.PersonaId,
                     Nombre = e.Persona.Nombre,
                     Apellido = e.Persona.Apellido,
                     Dni = e.Persona.Dni,
                     Cuil = e.Persona.Cuil,
                     Telefono = e.Persona.Telefono,
                     Telefono2 = e.Persona.Telefono2,
                     Email = e.Persona.Email,
                     Direccion = e.Persona.Direccion,
                     FechaNacimiento = e.Persona.FechaNacimiento,
                     EstaEliminado = e.Persona.EstaEliminado,
                     Legajo = e.Legajo,
                     FechaIngreso = e.FechaIngreso,
                     FechaEgreso = e.FechaEgreso,
                     Estado = e.Estado,
                     Username = e.Username,
                     Pass = e.Pass,
                     UsuarioEstaHabilitado = e.UsuarioEstaHabilitado
                 })
                 .FirstOrDefault();
            return empleado;
        }

        public ResultadoPaginacion<EmpleadoDTO> ObtenerEmpleados(FiltroConsulta filtros)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.Empleados
                .AsNoTracking()
                .Include(e => e.Persona)
                .AsQueryable();

            // 🔴 Eliminados
            query = filtros.VerEliminados
                ? query.Where(e => e.Persona != null && e.Persona.EstaEliminado)
                : query.Where(e => e.Persona != null && !e.Persona.EstaEliminado);

            // 🔍 TEXTO
            if (!string.IsNullOrWhiteSpace(filtros.TextoBuscar))
            {
                switch (filtros.Extra?.ToString())
                {
                    case "Nombre":
                        query = query.Where(e => e.Persona.Nombre.Contains(filtros.TextoBuscar));
                        break;

                    case "Apellido":
                        query = query.Where(e => e.Persona.Apellido.Contains(filtros.TextoBuscar));
                        break;

                    case "Dni":
                        query = query.Where(e => e.Persona.Dni == filtros.TextoBuscar);
                        break;

                    case "Email":
                        query = query.Where(e => e.Persona.Email == filtros.TextoBuscar);
                        break;

                    case "Username":
                        query = query.Where(e => e.Username.Contains(filtros.TextoBuscar));
                        break;

                    default: // ApyNom
                        query = query.Where(e =>
                            e.Persona.Nombre.Contains(filtros.TextoBuscar) ||
                            e.Persona.Apellido.Contains(filtros.TextoBuscar));
                        break;
                }
            }

            // 📅 FECHAS (Ingreso / Egreso)
            var tipoFecha = (TipoFiltroFecha?)filtros.Extra2;

            if (tipoFecha.HasValue && tipoFecha != TipoFiltroFecha.Ninguno)
            {
                if (tipoFecha == TipoFiltroFecha.Alta) // ingreso
                {
                    if (filtros.FechaDesde.HasValue)
                        query = query.Where(e => e.FechaIngreso >= filtros.FechaDesde.Value);

                    if (filtros.FechaHasta.HasValue)
                        query = query.Where(e => e.FechaIngreso <= filtros.FechaHasta.Value);
                }

                //if (tipoFecha == TipoFiltroFecha.Baja) // egreso
                //{
                //    if (filtros.FechaDesde.HasValue)
                //        query = query.Where(e =>
                //            e.FechaEgreso.HasValue &&
                //            e.FechaEgreso.Value >= filtros.FechaDesde.Value);

                //    if (filtros.FechaHasta.HasValue)
                //        query = query.Where(e =>
                //            e.FechaEgreso.HasValue &&
                //            e.FechaEgreso.Value <= filtros.FechaHasta.Value);
                //}
            }

            // 🔢 TOTAL
            var total = query.Count();

            // 🔽 ORDEN
            query = query.OrderBy(e => e.Persona.Apellido);

            // 📄 PAGINACIÓN + PROYECCIÓN
            var data = query
                .Skip((filtros.Page - 1) * filtros.PageSize)
                .Take(filtros.PageSize)
                .Select(e => new
                {
                    e.PersonaId,
                    Nombre = e.Persona.Nombre,
                    Apellido = e.Persona.Apellido,
                    e.Persona.Dni,
                    e.Persona.Cuil,
                    e.Persona.Telefono,
                    e.Persona.Telefono2,
                    e.Persona.Email,
                    e.Persona.Direccion,
                    e.Persona.FechaNacimiento,
                    e.Persona.EstaEliminado,
                    e.Legajo,
                    e.FechaIngreso,
                    e.FechaEgreso,
                    e.Estado,
                    e.Username,
                    e.Pass,
                    e.UsuarioEstaHabilitado
                })
                .ToList()
                .Select(e => new EmpleadoDTO
                {
                    PersonaId = e.PersonaId,
                    Nombre = e.Nombre,
                    Apellido = e.Apellido,
                    Dni = e.Dni,
                    Cuil = e.Cuil,
                    Telefono = e.Telefono,
                    Telefono2 = e.Telefono2,
                    Email = e.Email,
                    Direccion = e.Direccion,
                    FechaNacimiento = e.FechaNacimiento,
                    EstaEliminado = e.EstaEliminado,
                    Legajo = e.Legajo,
                    FechaIngreso = e.FechaIngreso,
                    FechaEgreso = e.FechaEgreso,
                    Estado = e.Estado,
                    EstadoDescripcion = Enum.GetName(typeof(EstadoEmpleado), e.Estado) ?? "Desconocido",
                    Username = e.Username,
                    Pass = e.Pass,
                    UsuarioEstaHabilitado = e.UsuarioEstaHabilitado
                })
                .ToList();

            return new ResultadoPaginacion<EmpleadoDTO>
            {
                Items = data,
                TotalRegistros = total,
                Page = filtros.Page,
                PageSize = filtros.PageSize
            };
        }
    }
}
