using AccesoDatos;
using AccesoDatos.Config;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Empleado;
using Servicios.Helpers.Producto;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Empleado.DTO;
using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
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
                .Where(e => e.Persona != null)
                .AsQueryable();

            // =========================================================
            // 🔴 ELIMINADOS / TODOS
            // =========================================================

            if (filtros.Bool2)
            {
                // VER TODOS → no filtra eliminados
            }
            else if (filtros.Bool1)
            {
                // SOLO eliminados
                query = query.Where(e => e.Persona.EstaEliminado);
            }
            else
            {
                // NORMAL → solo no eliminados
                query = query.Where(e => !e.Persona.EstaEliminado);
            }

            // =========================================================
            // 🔍 BUSQUEDA
            // =========================================================

            if (!string.IsNullOrWhiteSpace(filtros.TextoBuscar))
            {
                var texto = filtros.TextoBuscar.Trim();

                switch (filtros.Filtro1?.ToString())
                {
                    case "ApyNom":

                        query = query.Where(e =>
                            e.Persona.Nombre.Contains(texto) ||
                            e.Persona.Apellido.Contains(texto));

                        break;

                    case "Legajo":

                        query = query.Where(e =>
                            e.Legajo.ToString().Contains(texto));

                        break;

                    case "Usuario":

                        query = query.Where(e =>
                            e.Username.Contains(texto));

                        break;

                    case "Dni":

                        query = query.Where(e =>
                            e.Persona.Dni.Contains(texto));

                        break;

                    default:

                        query = query.Where(e =>
                            e.Persona.Nombre.Contains(texto) ||
                            e.Persona.Apellido.Contains(texto));

                        break;
                }
            }

            // =========================================================
            // 📌 FILTRO ESTADO (cbx2)
            // =========================================================

            if (!filtros.Bool2) // si está en "ver todos", no filtrar estado
            {
                if (!string.IsNullOrWhiteSpace(filtros.Filtro2?.ToString()))
                {
                    if (int.TryParse(filtros.Filtro2.ToString(), out int estado))
                    {
                        switch ((EstadoEmpleado)estado)
                        {
                            case EstadoEmpleado.Habilitado:

                                query = query.Where(e =>
                                    e.Estado == (int)EstadoEmpleado.Habilitado);

                                break;

                            case EstadoEmpleado.Inhablitado:

                                query = query.Where(e =>
                                    e.Estado == (int)EstadoEmpleado.Inhablitado);

                                break;

                            case EstadoEmpleado.SinPass:

                                query = query.Where(e =>
                                    string.IsNullOrWhiteSpace(e.Pass));

                                break;
                        }
                    }
                }
            }

            // =========================================================
            // 📅 TIPO FECHA (cbx3)
            // =========================================================

            var tipoFecha = filtros.Filtro3?.ToString();

            // =========================================================
            // 📅 FILTRO FECHAS
            // =========================================================

            if (filtros.FechaDesde.HasValue || filtros.FechaHasta.HasValue)
            {
                if (tipoFecha == "Egreso")
                {
                    query = query.Where(e => e.FechaEgreso.HasValue);

                    if (filtros.FechaDesde.HasValue)
                    {
                        query = query.Where(e =>
                            e.FechaEgreso.Value >= filtros.FechaDesde.Value);
                    }

                    if (filtros.FechaHasta.HasValue)
                    {
                        var hasta = filtros.FechaHasta.Value.AddDays(1);

                        query = query.Where(e =>
                            e.FechaEgreso.Value < hasta);
                    }
                }
                else
                {
                    // DEFAULT = Ingreso

                    if (filtros.FechaDesde.HasValue)
                    {
                        query = query.Where(e =>
                            e.FechaIngreso >= filtros.FechaDesde.Value);
                    }

                    if (filtros.FechaHasta.HasValue)
                    {
                        var hasta = filtros.FechaHasta.Value.AddDays(1);

                        query = query.Where(e =>
                            e.FechaIngreso < hasta);
                    }
                }
            }

            // =========================================================
            // 📊 TOTAL
            // =========================================================

            var total = query.Count();

            // =========================================================
            // 📄 PAGINACION
            // =========================================================

            var totalPaginas =
                (int)Math.Ceiling((double)total / filtros.PageSize);

            if (totalPaginas <= 0)
                totalPaginas = 1;

            if (filtros.Page > totalPaginas)
                filtros.Page = totalPaginas;

            if (filtros.Page < 1)
                filtros.Page = 1;

            // =========================================================
            // 📌 ORDEN
            // =========================================================

            query = query
                .OrderBy(e => e.Persona.Apellido)
                .ThenBy(e => e.Persona.Nombre);

            // =========================================================
            // 📦 DATA
            // =========================================================

            var data = query
                .Skip((filtros.Page - 1) * filtros.PageSize)
                .Take(filtros.PageSize)
                .AsEnumerable()
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
                    EstadoDescripcion = ((EstadoEmpleado)e.Estado).ToString(),

                    Username = e.Username,
                    Pass = e.Pass,
                    UsuarioEstaHabilitado = e.UsuarioEstaHabilitado
                })
                .ToList();

            // =========================================================
            // RESULTADO
            // =========================================================

            return new ResultadoPaginacion<EmpleadoDTO>
            {
                Items = data,
                TotalRegistros = total,
                Page = filtros.Page,
                PageSize = filtros.PageSize
            };
        }

        public DatosTurnoDTO ObtenerDatosPanelPrincipal(long usuarioId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var datosTurno = context.Empleados
                .AsNoTracking()
                .Include(e => e.Sesiones)
                .Where(e => e.PersonaId == usuarioId)
                .Select(e => new DatosTurnoDTO
                {
                    UsuarioId = e.PersonaId,
                    UsuarioLogeado = $"{e.Username}",
                    HoraIngresoUsuario = e.Sesiones
                        .Where(s => s.FechaLogout == null)
                        .Select(s => s.FechaLogin)
                        .FirstOrDefault()
                }).FirstOrDefault();
        
            return datosTurno;
        }
    }
}