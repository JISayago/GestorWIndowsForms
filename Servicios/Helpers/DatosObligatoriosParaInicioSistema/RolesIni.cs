using AccesoDatos;
using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.DatosObligatoriosParaInicioSistema
{
    public class RolesIni
    {
        public static void Inicializar(GestorContextDB context)
        {
            var rol = context.Roles
               .FirstOrDefault(r => r.CodigoRol == "SADMIN");

            if (rol == null)
            {
                rol = new Rol
                {
                    CodigoRol = "SADMIN",
                    Nombre = "Super Administrador"
                };

                context.Roles.Add(rol);
                context.SaveChanges();
            }

            var todosLosPermisos = context.Permisos.ToList();

            foreach (var permiso in todosLosPermisos)
            {
                bool existe = context.RolesPermisos.Any(rp =>
                    rp.IdRol == rol.RolId && rp.IdPermiso == permiso.PermisoId);

                if (!existe)
                {
                    context.RolesPermisos.Add(new RolPermiso
                    {
                        IdRol = rol.RolId,
                        IdPermiso = permiso.PermisoId
                    });
                }
            }

            context.SaveChanges();
        }
    }
}
