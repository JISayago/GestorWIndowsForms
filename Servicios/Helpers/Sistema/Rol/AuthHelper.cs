using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar la directiva using para el espacio de nombres Presentacion
using Presentacion.AccesoAlSistema;

namespace Servicios.Helpers.Sistema.Rol
{
    public static class AuthHelper
    {
        public static UsuarioLogeado UsuarioActual { get; set; }

        public static bool Tiene(string permiso)
        {
            if (UsuarioActual == null)
                return false;

            if (UsuarioActual.EsSuperAdmin)
                return true;

            return UsuarioActual.Permisos.Contains(permiso);
        }
    }
}
