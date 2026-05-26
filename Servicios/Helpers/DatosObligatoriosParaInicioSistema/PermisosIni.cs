using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.DatosObligatoriosParaInicioSistema
{
    public class PermisosIni
    {
        public static void Inicializar(GestorContextDB context)
        {
            foreach (var def in StaticPermisosCuerpo.Lista)
            {
                var permiso = context.Permisos
                    .FirstOrDefault(p => p.Codigo == def.Codigo);
                         
                if (permiso == null)
                {
                    context.Permisos.Add(new Permiso
                    {
                       Codigo = def.Codigo,
                       Descripcion = def.Descripcion
                    });
                }
                else
                {
                permiso.Descripcion = def.Descripcion;
                }
            }
                 
             context.SaveChanges();
        }
    }
}
