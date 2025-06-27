using AccesoDatos.Config;
using AccesoDatos;
using Servicios.LogicaNegocio.Empleado.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Entidades;
using Servicios.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Servicios.LogicaNegocio.Empleado
{
    public class EmpleadoServicio : IEmpleadoServicio
    {
        public void Eliminar(long empleadoId)
        {
            throw new NotImplementedException();
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
                Estado = 1,
                Username = "Usuario",
                Pass = "usuario2", // Reemplazá esto con un hash si lo vas a encriptar
                UsuarioEstaHabilitado = false
            };

            context.Empleados.Add(empleado);
            context.SaveChanges();
            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Empleado creado correctamente.",
                EmpleadoId = empleado.PersonaId
            };

        }

        public void Modificar(EmpleadoDTO empleadoDto)
        {
            throw new NotImplementedException();
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

        public IEnumerable<EmpleadoDTO> ObtenerEmpleados(string cadenabuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var empleados = context.Empleados
                .AsNoTracking()
                .Include(e => e.Persona)
                  .Where(e => e.Persona != null && !e.Persona.EstaEliminado && (e.Persona.Nombre.Contains(cadenabuscar)
                                || e.Persona.Apellido.Contains(cadenabuscar)
                                || e.Persona.Dni == (cadenabuscar)
                                || e.Persona.Email == (cadenabuscar)))
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
                .ToList();

            return empleados;
        }

        public IEnumerable<EmpleadoDTO> ObtenerEmpleadosEliminados(string cadenabuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var empleados = context.Empleados
                .AsNoTracking()
                .Include(e => e.Persona)
                .Where(e => e.Persona != null && e.Persona.EstaEliminado && (e.Persona.Nombre.Contains(cadenabuscar)
                                || e.Persona.Apellido.Contains(cadenabuscar)
                                || e.Persona.Dni == (cadenabuscar)
                                || e.Persona.Email == (cadenabuscar)))
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
                .ToList();

            return empleados;
        }
    }
}
