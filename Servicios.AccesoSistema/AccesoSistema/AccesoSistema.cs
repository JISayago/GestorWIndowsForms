using AccesoDatos;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers;
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
            if (empleado.Estado == 1) return false;
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

        public EstadoOperacion LogeoAlSistema(string username, string pass)
        {
            if (VerificarSiExisteUsuario(username))
            {
                if (VerificarSiUsuarioEstaBloqueado(username))
                {

                    return new EstadoOperacion {
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

                // Login exitoso
                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Ingreso Exitoso!",
                    EntidadId = empleado.PersonaId
                };
            }
            return new EstadoOperacion
            {
                Exitoso = false,
                Mensaje = "Usuario no encontrado. Por favor comuníquese con un Administrador."
            };

        }

        public EstadoOperacion PrimerIngreso(string nombreUsuario, string pass)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var empleado = context.Empleados
                .FirstOrDefault(e => e.Username == nombreUsuario);
            if (empleado == null || empleado.Estado == 1 || pass != "123456789")
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
    }
}
