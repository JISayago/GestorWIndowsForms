using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
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
        ResultadoPaginacion<RolDTO> ObtenerRoles(FiltroConsulta filtros);
        IEnumerable<RolDTO> ObtenerRolesEliminados(string cadenabuscar);

        EstadoOperacion ActualizarRolesDeEmpleado(List<RolDTO> rolesAsignados, long empleadoId, DateTime fechaAsignacion);
        EstadoOperacion Insertar(RolDTO rolDto);
        RolDTO ObtenerRolPorId(long rolId);
        EstadoOperacion Modificar(RolDTO rolDto, long? rolId);
        EstadoOperacion Eliminar(long rolId);
    }
}
