using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.Admin;
using Servicios.Helpers.Sistema.Extras;
using Servicios.LogicaNegocio.Empleado.DTO;
using Servicios.LogicaNegocio.Empleado.Rol.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Empleado
{
    public class UsuarioServicio : IUsuarioServicio
    {
        public EstadoOperacion ActualziarPassPrimerIngreso(long usuarioId, string pass)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            var empleado = context.Empleados
             .Include(e => e.Persona)
                 .FirstOrDefault(x => x.PersonaId == usuarioId);

            if (empleado == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Error al encontrar el empelado",
                };
            }

            empleado.Pass = HashPass.HashPassword(pass);
            empleado.Estado = 1;
            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Usuario creado con exito!",
                EntidadId = empleado.PersonaId
            };

        }

        public EstadoOperacion CrearUsuario(UsuarioDTO usuario)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            var empleado = context.Empleados
             .Include(e => e.Persona)
                 .FirstOrDefault(x => x.PersonaId == usuario.PersonaId);

            if (empleado == null)
            {

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Error al encontrar el empelado",
                    EntidadId = empleado.PersonaId
                };
            }

            // Verificar si ya tiene usuario
            if (!string.IsNullOrEmpty(empleado.Username))
            {
                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = $"El empleado {empleado.Persona.Nombre} {empleado.Persona.Apellido} ya tiene un usuario asignado: {empleado.Username}",
                    EntidadId = empleado.PersonaId
                };
            }
            empleado.Username = usuario.Username;
            empleado.Pass = HashPass.HashPassword(usuario.Pass);
            empleado.Estado = usuario.Estado;
            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Usuario creado con exito!",
                EntidadId = empleado.PersonaId
            };


        }

        public bool ExisteUsuario(string nombreUsuario)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            try
            {
                string nombreUsuarioLimpio = nombreUsuario.Trim().ToLower();
                return context.Empleados.Any(u => u.Username.ToLower() == nombreUsuarioLimpio);
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public UsuarioDTO GeneracionNombreUsuario(string nombre, string apellido, long empleadoId)
        {
            int contadorLetras = 1;
            int digito = 1;
            string nombreLimpio = nombre.Trim().ToLower();
            string apellidoLimpio = apellido.Trim().ToLower();
            string usuarioNuevo = $"{nombreLimpio.Substring(0, contadorLetras)}{apellidoLimpio}";

            var context = new GestorContextDBFactory().CreateDbContext(null);

            try
            {
                // Generar nombre único
                while (context.Empleados.Any(x => x.Username.ToLower() == usuarioNuevo))
                {
                    if (contadorLetras < nombreLimpio.Length)
                    {
                        contadorLetras++;
                        usuarioNuevo = $"{nombreLimpio.Substring(0, contadorLetras)}{apellidoLimpio}";
                    }
                    else
                    {
                        usuarioNuevo = $"{nombreLimpio}{apellidoLimpio}{digito}";
                        digito++;
                    }
                }

                // Buscar al empleado
                var empleado = context.Empleados
                .Include(e => e.Persona)
                    .FirstOrDefault(x => x.PersonaId == empleadoId);

                if (empleado == null)
                {
                    return new UsuarioDTO
                    {
                        PersonaId = 0,
                        Username = "Error al encontrar el empelado",
                        Pass = "",
                        Estado = 0
                    };

                }

                // Verificar si ya tiene usuario
                if (!string.IsNullOrEmpty(empleado.Username))
                {
                    return new UsuarioDTO
                    {
                        PersonaId = 0,
                        Username = $"El empleado {empleado.Persona.Nombre} {empleado.Persona.Apellido} ya tiene un usuario asignado: {empleado.Username}",
                        Pass = "",
                        Estado = 0
                    };
                }

                // Asignar y guardar
                empleado.Username = usuarioNuevo;
                empleado.Pass = HashPass.HashPassword("123456789");
                empleado.Estado = 1; // Activo
                //context.SaveChanges();

                return new UsuarioDTO
                {
                    PersonaId = empleado.PersonaId,
                    Username = empleado.Username,
                    Pass = empleado.Pass,
                    Estado = empleado.Estado
                };
            }
            catch (Exception ex)
            {
                return new UsuarioDTO
                {
                    PersonaId = 0,
                    Username = "Error al asignar el nombre de usuario.",
                    Pass = "",
                    Estado = 0
                };
            }
        }

        public UsuarioDTO ObtenerUsuarioPorId(long usuarioId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            var usuario = context.Empleados
                .Include(e => e.Persona)
                .FirstOrDefault(x => x.PersonaId == usuarioId);
            if (usuario != null)
            {
                return new UsuarioDTO
                {
                    PersonaId = usuario.PersonaId,
                    Username = usuario.Username,
                    Pass = usuario.Pass,
                    Estado = usuario.Estado
                };
            }
            else
            {
                return new UsuarioDTO
                {
                    PersonaId = 0,
                    Username = "Usuario no encontrado",
                    Pass = "",
                    Estado = 0
                };
            }
        }

        public EstadoOperacion DeshabilitarUsuarioYRecuperarContra(string user,string nro)
        {
            using (var context = new GestorContextDBFactory().CreateDbContext(null))
            {
                var usuario = context.Empleados
                    .Include(e => e.Persona)
                    .Include(e => e.EmpleadoRoles)
                        .ThenInclude(er => er.Rol)
                    .FirstOrDefault(x => (x.Persona.Dni == nro || x.Legajo == nro) && x.Username == user);

                if (usuario == null)
                {
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "Usuario no encontrado",
                        EntidadId = null
                    };
                }

                // Roles
                var roles = usuario.EmpleadoRoles
                    .Select(er => er.Rol)
                    .ToList();

                // Validación de rol
                bool tienePermiso = roles.Any(r => r.CodigoRol == "SADMIN"); // mejor por ID

                if (!tienePermiso)
                {
                    
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = $"Hola {usuario.Username}!. Se notificó a un administrador para la recuperacion de contraseña.",
                        EntidadId = usuario.PersonaId
                    };
                }

                // Deshabilitar
                usuario.Estado = (int)EstadoEmpleado.SinPass;
                usuario.UsuarioEstaHabilitado = false;

                context.SaveChanges();

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Usuario deshabilitado correctamente",
                    EntidadId = usuario.PersonaId
                };
            }
        }
        public ICollection<long> ObtenerEmpleadosSinPassIDs()
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);
            var usuariosInhabilitadosIDs = context.Empleados
                .Where(e => !e.UsuarioEstaHabilitado && (e.Estado == (int)EstadoEmpleado.SinPass))
                .Select(e => e.PersonaId)
                .ToList();
            return usuariosInhabilitadosIDs;
        }

        public EstadoOperacion ResetearContra(long adminId, long usuarioDesbloquearId)
        {
            using (var context = new GestorContextDBFactory().CreateDbContext(null))
            {
                var admin = context.Empleados
                    .Include(e => e.EmpleadoRoles)
                        .ThenInclude(er => er.Rol)
                    .FirstOrDefault(x => x.PersonaId == adminId);

                if (admin == null)
                {
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "Administrador no encontrado"
                    };
                }

                bool esSAdmin = admin.EmpleadoRoles
                    .Any(er => er.Rol.CodigoRol == "SADMIN");

                if (!esSAdmin)
                {
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "No tiene permisos para resetear contraseñas",
                        EntidadId = adminId
                    };
                }

                var usuario = context.Empleados
                    .FirstOrDefault(x => x.PersonaId == usuarioDesbloquearId
                                      && x.Estado == (int)EstadoEmpleado.SinPass);

                if (usuario == null)
                {
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "Usuario a resetear no encontrado"
                    };
                }

                string codigo = GenerarCodigoRecuperacion();

                usuario.Estado = (int)EstadoEmpleado.Inhablitado;

                // usuario.CodigoRecuperacion = codigo;

                context.SaveChanges();

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = $"Código de recuperación para el usuario {usuario.Username}: {codigo}. Válido por 5 minutos.",
                    EntidadId = usuario.PersonaId
                };
            }
        }
        private string GenerarCodigoRecuperacion(int longitud = 5)
        {
            var random = new Random();
            return random.Next((int)Math.Pow(10, longitud - 1), (int)Math.Pow(10, longitud)).ToString();
        }
    }
}
