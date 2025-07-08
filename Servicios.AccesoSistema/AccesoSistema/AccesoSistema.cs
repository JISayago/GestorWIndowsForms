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
        public string error = "";
        public string usuarioLogeado = "-";
        public long ObtenerId(string nombreUsuario)
        {
            throw new NotImplementedException();

        }
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

        public string LogeoAlSistema(string username, string pass)
        {
            if (VerificarSiExisteUsuario(username))
            {
                if (VerificarSiUsuarioEstaBloqueado(username))
                {
                    error = "Usuario bloqueado. Por favor comuníquese con un Administrador.";
                    return error;
                }

                using var context = new GestorContextDBFactory().CreateDbContext(null);
                var empleado = context.Empleados
                    .FirstOrDefault(e => e.Username == username);

                if (empleado == null || !HashPass.VerifyPassword(pass, empleado.Pass))
                {
                    error = "El usuario y/o la contraseña son incorrectos";
                    return error;
                }

                // Login exitoso
                return usuarioLogeado;
            }

            error = "Usuario no encontrado. Por favor comuníquese con un Administrador.";
            return error;
        }

    }
}
