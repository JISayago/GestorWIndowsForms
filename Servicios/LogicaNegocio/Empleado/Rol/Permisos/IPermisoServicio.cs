using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Empleado.Rol.Tareas.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Empleado.Rol.Tareas
{
    public interface IPermisoServicio
    {
        List <PermisoDTO> ObtenerPermisos(FiltroConsulta filtro);
        IEnumerable<PermisoDTO> ObtenerPermisosAsignadosARol(long rolId);
        EstadoOperacion ActualizarPermisosDeRol(List<PermisoDTO> permisos, long rolId);
    }
}
