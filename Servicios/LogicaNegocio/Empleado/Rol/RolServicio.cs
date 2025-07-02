using AccesoDatos.Entidades;
using AccesoDatos;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Empleado.DTO;
using Servicios.LogicaNegocio.Empleado.Rol.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Empleado.Rol
{
    public class RolServicio : IRolServicio
    {
        public EstadoOperacion Insertar(RolDTO rolDto)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            if (context.Roles.Any(r => r.CodigoRol == rolDto.CodigoRol))
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una persona con el mismo DNI."
                };

            var rol = new AccesoDatos.Entidades.Rol
            {
                Nombre = rolDto.Nombre,
                DetalleRol = rolDto.DetalleRol,
                CodigoRol = rolDto.CodigoRol,
               
            };

            context.Roles.Add(rol);
            context.SaveChanges();
            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Empleado creado correctamente.",
                EntidadId = rol.RolId
            };
        }
    }
}
