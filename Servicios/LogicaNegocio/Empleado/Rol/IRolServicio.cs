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
    public  interface IRolServicio
    {
        IEnumerable<RolDTO> ObtenerRoles(string cadenabuscar);
        IEnumerable<RolDTO> ObtenerRolesAsignadosAEmpleados(long empleadoId);
        IEnumerable<RolDTO> ObtenerRolesEliminados(string cadenabuscar);

        EstadoOperacion ActualizarRolesDeEmpleado(List<RolDTO> rolesAsignados, long empleadoId, DateTime fechaAsignacion);
        EstadoOperacion Insertar(RolDTO rolDto);
        RolDTO ObtenerRolPorId(long rolId);
        EstadoOperacion Modificar(RolDTO rolDto, long? rolId);
        EstadoOperacion Eliminar(long rolId);
    }
}
