using AccesoDatos.Entidades;
using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios.Helpers.Sistema.Extras;

namespace Servicios.Helpers.DatosObligatorios
{
    public class UsuarioInicial
    {
        public static void Inicializar(GestorContextDB context)
        {
            // Buscar usuario admin existente
            var empleado = context.Empleados
                .FirstOrDefault(e => e.Username == "admin");

            if (empleado == null)
            {
                // Crear persona
                var persona = new Persona
                {
                    Nombre = "Administrador",
                    Apellido = "DelSistema",
                    Dni = "99999999",
                    Cuil = "20-99999999-9",
                    Telefono = "0000000000",
                    Email = "admin@sistema.com",
                    Direccion = "Oficina Central",
                    EstaEliminado = false,
                    FechaNacimiento = DateTime.Today.AddYears(-30)
                };

                context.Personas.Add(persona);
                context.SaveChanges();

                // Crear empleado
                empleado = new AccesoDatos.Entidades.Empleado
                {
                    PersonaId = persona.PersonaId,
                    Legajo = "ADM001",
                    FechaIngreso = DateTime.Today,
                    Estado = 2,
                    Username = "admin",
                    Pass = HashPass.HashPassword("Admin123"),
                    UsuarioEstaHabilitado = true
                };

                context.Empleados.Add(empleado);
                context.SaveChanges();
            }

            // 🔥 ASEGURAR ROL SADMIN

            var rol = context.Roles.FirstOrDefault(r => r.CodigoRol == "SADMIN");

            if (rol == null)
            {
                // esto en teoría ya lo creaste antes, pero por seguridad:
                rol = new Rol
                {
                    CodigoRol = "SADMIN",
                    Nombre = "Super Administrador"
                };

                context.Roles.Add(rol);
                context.SaveChanges();
            }

            // Verificar si ya tiene el rol
            bool tieneRol = context.EmpleadoRoles.Any(ur =>
                ur.IdEmpleado == empleado.PersonaId && ur.IdRol == rol.RolId);

            if (!tieneRol)
            {
                context.EmpleadoRoles.Add(new EmpleadoRol
                {
                    IdEmpleado = empleado.PersonaId,
                    IdRol = rol.RolId,
                    FechaAsignacion = DateTime.Now
                });

                context.SaveChanges();
            }
        }
    }
}
