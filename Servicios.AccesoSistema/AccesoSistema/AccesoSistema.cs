using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Empleado;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.Extras;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAccesoSistema.AccesoSistema
{
    public class AccesoSistema : IAccesoSistema
    {
        public bool VerificarSiUsuarioEstaBloqueado(string username)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var empleado = context.Empleados
                .FirstOrDefault(e => e.Username == username);
            if (empleado == null) return true;
            if (empleado.Estado == (int)EstadoEmpleado.Habilitado) return false;
            return true;
        }

        public bool VerificarSiExisteUsuario(string username)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var empleado = context.Empleados
            .Include(e => e.Persona)
            .FirstOrDefault(e =>
            e.Username == username &&
            e.Persona != null &&
            !e.Persona.EstaEliminado);
            if (empleado == null) return false;
            return true;
        }

        public EstadoOperacion LogeoAlSistema(string username, string pass, long? sesionPrincipalId = null)
        {
            bool esSecundaria = sesionPrincipalId.HasValue;

            if (!VerificarSiExisteUsuario(username))
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Usuario no encontrado. Por favor comuníquese con un Administrador."
                };
            }

            if (VerificarSiUsuarioEstaBloqueado(username))
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Usuario bloqueado. Por favor comuníquese con un Administrador."
                };
            }

            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var empleado = context.Empleados
                .FirstOrDefault(e => e.Username == username);

            if (empleado == null || !HashPass.VerifyPassword(pass, empleado.Pass))
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "El usuario y/o la contraseña son incorrectos"
                };
            }

            // La secundaria solo es válida si la sesión principal que la lanzó sigue activa
            if (esSecundaria &&
                !context.Usuarios.Any(s => s.UsuarioSesionId == sesionPrincipalId.Value && s.Activa && !s.EsSecundaria))
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "La sesión principal ya no está activa. Cerrá la sesión secundaria y volvé a abrirla."
                };
            }

            // Cerrar sesiones activas previas del usuario (seguridad).
            // Una secundaria no cierra la principal del mismo usuario (ni al revés).
            var sesionesActivas = context.Usuarios
                .Where(s => s.UsuarioId == empleado.PersonaId && s.Activa && s.EsSecundaria == esSecundaria)
                .ToList();

            foreach (var sesion in sesionesActivas)
            {
                sesion.Activa = false;
                sesion.FechaLogout = DateTime.Now;
            }

            // Crear nueva sesión
            var nuevaSesion = new UsuarioSesion
            {
                UsuarioId = empleado.PersonaId,
                FechaLogin = DateTime.Now,
                Activa = true,
                EsSecundaria = esSecundaria,
                SesionPrincipalId = sesionPrincipalId
            };

            context.Usuarios.Add(nuevaSesion);

            context.SaveChanges();

            // Login exitoso
            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Ingreso Exitoso!",
                EntidadId = empleado.PersonaId,
                DatoExtra = nuevaSesion.UsuarioSesionId.ToString()
            };
        }


        public EstadoOperacion CerrarSesion(long usuarioId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var sesionActiva = context.Usuarios
                .FirstOrDefault(s => s.UsuarioId == usuarioId && s.Activa);

            if (sesionActiva == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "El usuario no tiene una sesión activa."
                };
            }

            sesionActiva.Activa = false;
            sesionActiva.FechaLogout = DateTime.Now;

            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Sesión cerrada correctamente."
            };
        }
        /// <summary>Id de la sesión principal activa del usuario (null si no hay).</summary>
        public long? ObtenerSesionActivaId(long usuarioId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.Usuarios
                .Where(s => s.UsuarioId == usuarioId && s.Activa && !s.EsSecundaria)
                .OrderByDescending(s => s.FechaLogin)
                .Select(s => (long?)s.UsuarioSesionId)
                .FirstOrDefault();
        }

        /// <summary>
        /// Cierra las sesiones secundarias colgadas de una sesión principal. La principal lo llama al volver:
        /// si la secundaria cayó sin cerrar sesión, queda "Activa" en la BD y esto la regulariza.
        /// </summary>
        public void CerrarSesionesSecundarias(long sesionPrincipalId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var secundarias = context.Usuarios
                .Where(s => s.SesionPrincipalId == sesionPrincipalId && s.Activa)
                .ToList();

            if (secundarias.Count == 0) return;

            foreach (var sesion in secundarias)
            {
                sesion.Activa = false;
                sesion.FechaLogout = DateTime.Now;
            }

            context.SaveChanges();
        }

        /// <summary>
        /// Devuelve la sesión principal indicada y todas las secundarias lanzadas desde ella.
        /// Orden: principal primero, después las secundarias activas y por último las cerradas (más recientes antes).
        /// </summary>
        public List<SesionInfoDTO> ObtenerSesionesDelTerminal(long sesionPrincipalId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.Usuarios
                .Where(s => s.UsuarioSesionId == sesionPrincipalId || s.SesionPrincipalId == sesionPrincipalId)
                .OrderBy(s => s.EsSecundaria)
                .ThenByDescending(s => s.Activa)
                .ThenByDescending(s => s.FechaLogin)
                .Take(20)
                .Select(s => new SesionInfoDTO
                {
                    UsuarioSesionId = s.UsuarioSesionId,
                    UsuarioId = s.UsuarioId,
                    Username = s.Usuario != null ? s.Usuario.Username : "",
                    FechaLogin = s.FechaLogin,
                    FechaLogout = s.FechaLogout,
                    Activa = s.Activa,
                    EsSecundaria = s.EsSecundaria,
                    SesionPrincipalId = s.SesionPrincipalId
                })
                .ToList();
        }

        public EstadoOperacion PrimerIngreso(string nombreUsuario, string pass)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var empleado = context.Empleados
                .FirstOrDefault(e => e.Username == nombreUsuario);
            if (empleado == null || empleado.Estado == (int)EstadoEmpleado.Habilitado || pass != "123456789")//cambiar esto por lo que corresponda por cofig primer ingreso.
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "El usuario no existe o no es un primer ingreso."
                };
            }
            else
            {
                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Primer ingreso exitoso. Por favor, cambie su contraseña.",
                    EntidadId = empleado.PersonaId
                };

            }
        }
        public EstadoOperacion ValidarEstadoUsuario(string username)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var _usuarioServicio = new UsuarioServicio();
            var empleado = context.Empleados
                .Include(e => e.EmpleadoRoles)
                    .ThenInclude(er => er.Rol)
                .FirstOrDefault(e => e.Username == username);

            if (empleado == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Usuario no encontrado"
                };
            }

            // 🔹 Verificar si es SADMIN
            bool esSAdmin = empleado.EmpleadoRoles
                .Any(er => er.Rol.CodigoRol == "SADMIN");

            // 🔹 Usuario activo → login normal
            if (empleado.Estado == (int)EstadoEmpleado.Habilitado)
            {
                return new EstadoOperacion
                {
                    Exitoso = false
                };
            }

            // 🔹 Caso especial: SADMIN en recuperación → generar código automático
            if (empleado.Estado == (int)EstadoEmpleado.SinPass && esSAdmin)
            {
                // 🔸 invalidar códigos anteriores
                var codigosActivos = context.CodigosRecuperacionPass
                    .Where(c => c.UsuarioAsignadoId == empleado.PersonaId && !c.EstaUsado)
                    .ToList();

                foreach (var c in codigosActivos)
                {
                    c.EstaUsado = true;
                }

                // 🔸 generar nuevo código
                string codigo = _usuarioServicio.GenerarCodigoRecuperacion();

                var nuevoCodigo = new CodigoRecuperacionPass
                {
                    UsuarioAsignadoId = empleado.PersonaId,
                    Codigo = codigo,
                    FechaCreacion = DateTime.Now,
                    FechaExpiracion = DateTime.Now.AddHours(24),
                    EstaUsado = false
                };

                context.CodigosRecuperacionPass.Add(nuevoCodigo);

                context.SaveChanges();

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = $"Código de recuperación: {codigo}. Válido por 24 horas .",
                    EntidadId = empleado.PersonaId,
                    DatoExtra = codigo.ToString(),
                };
            }

            // 🔹 Primer ingreso
            if (empleado.Estado == (int)EstadoEmpleado.Inhablitado)
            {
                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Primer ingreso. Debe crear una contraseña.",
                    EntidadId = empleado.PersonaId
                };
            }

            // 🔹 Recuperación normal
            if (empleado.Estado == (int)EstadoEmpleado.SinPass)
            {
                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Debe ingresar código de recuperación.",
                    EntidadId = empleado.PersonaId
                };
            }

            return new EstadoOperacion
            {
                Exitoso = false,
                Mensaje = "Estado de usuario no válido"
            };
        }
        //public EstadoOperacion ValidarEstadoUsuario(string username)
        //{
        //    using var context = new GestorContextDBFactory().CreateDbContext(null);

        //    var empleado = context.Empleados
        //        .FirstOrDefault(e => e.Username == username);

        //    if (empleado == null)
        //    {
        //        return new EstadoOperacion
        //        {
        //            Exitoso = false,
        //            Mensaje = "Usuario no encontrado"
        //        };
        //    }

        //    // 🔹 Usuario activo → login normal
        //    if (empleado.Estado == (int)EstadoEmpleado.Habilitado)
        //    {
        //        return new EstadoOperacion
        //        {
        //            Exitoso = false // sigue flujo normal de login
        //        };
        //    }

        //    // 🔹 Primer ingreso
        //    if (empleado.Estado == (int)EstadoEmpleado.Inhablitado)
        //    {
        //        return new EstadoOperacion
        //        {
        //            Exitoso = true,
        //            Mensaje = "Primer ingreso. Debe crear una contraseña.",
        //            EntidadId = empleado.PersonaId
        //        };
        //    }

        //    // 🔹 Recuperación
        //    if (empleado.Estado == (int)EstadoEmpleado.SinPass)
        //    {
        //        return new EstadoOperacion
        //        {
        //            Exitoso = true,
        //            Mensaje = "Debe ingresar código de recuperación.",
        //            EntidadId = empleado.PersonaId
        //        };
        //    }

        //    return new EstadoOperacion
        //    {
        //        Exitoso = false,
        //        Mensaje = "Estado de usuario no válido"
        //    };
        //}
    }
}