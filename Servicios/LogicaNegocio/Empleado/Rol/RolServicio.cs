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
                    Mensaje = "Ya existe una Rol con el mismo Codigo."
                };

            var rol = new AccesoDatos.Entidades.Rol
            {
                Nombre = rolDto.Nombre,
                /*DetalleRol = rolDto.DetalleRol,
                CodigoRol = rolDto.CodigoRol,*/
                DetalleRol = 1,
                CodigoRol = 1,
               
            };

            context.Roles.Add(rol);
            context.SaveChanges();
            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Rol creado correctamente.",
                EntidadId = rol.RolId
            };
        }
    }
}
