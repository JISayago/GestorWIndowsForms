using AccesoDatos;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Servicios.LogicaNegocio.Empleado
{
    public class UsuarioServicio : IUsuarioServicio
    {
        public EstadoOperacion CrearUsuario(string nombre, string apellido, long empleadoId)
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
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = "Empleado no encontrado."
                    };
                }

                // Verificar si ya tiene usuario
                if (!string.IsNullOrEmpty(empleado.Username))
                {
                    return new EstadoOperacion
                    {
                        Exitoso = false,
                        Mensaje = $"El empleado {empleado.Persona.Nombre} {empleado.Persona.Apellido} ya tiene un usuario asignado: {empleado.Username}"
                    };
                }

                // Asignar y guardar
                empleado.Username = usuarioNuevo;
                context.SaveChanges();

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = $"Usuario creado correctamente: {usuarioNuevo}",
                    EntidadId = empleado.PersonaId
                };
            }
            catch (Exception ex)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = $"Error al crear el usuario: {ex.Message}"
                };
            }
        }

        public bool ExisteUsuario(string nombreUsuario)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            try
            {
                return context.Empleados.Any(u => u.Username.Equals(nombreUsuario, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar usuario existente", ex);
            }
        }
    }
}
