using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Empleado.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Empleado
{
    public interface IEmpleadoServicio
    {
        ResultadoPaginacion<EmpleadoDTO> ObtenerEmpleados(FiltroConsulta filtros);
        EstadoOperacion Insertar(EmpleadoDTO empleadoDto);
        EmpleadoDTO ObtenerEmpleadoPorId(long personaId);
        EstadoOperacion Modificar(EmpleadoDTO empleadoDto, long? empleadoId);
        EstadoOperacion Eliminar(long empleadoId);
        
    }
}
