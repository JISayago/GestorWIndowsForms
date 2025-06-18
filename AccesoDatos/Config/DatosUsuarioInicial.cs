using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Config
{
    public static class DatosUsuarioInicial
    { 
        public static void Inicializar(GestorContextDB context)
        {
            // Aplica migraciones pendientes
            context.Database.Migrate();

            // Si ya existe un usuario admin, no hacer nada
            if (context.Empleados.Any(e => e.Username == "admin"))
                return;

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
            context.SaveChanges(); // Esto genera el PersonaId

            // Crear empleado vinculado a esa persona
            var empleado = new Empleado
            {
                PersonaId = persona.PersonaId,
                Legajo = "ADM001",
                FechaIngreso = DateTime.Today,
                Estado = 1,
                Username = "admin",
                Pass = "admin123", // Reemplazá esto con un hash si lo vas a encriptar
                UsuarioEstaHabilitado = true
            };

            context.Empleados.Add(empleado);
            context.SaveChanges();
        }
    }
}
