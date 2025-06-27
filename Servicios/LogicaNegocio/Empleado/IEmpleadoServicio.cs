using Servicios.Helpers;
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
        IEnumerable<EmpleadoDTO> ObtenerEmpleados(string cadenabuscar);
        IEnumerable<EmpleadoDTO> ObtenerEmpleadosEliminados(string cadenabuscar);

        EstadoOperacion Insertar(EmpleadoDTO empleadoDto);
        EmpleadoDTO ObtenerEmpleadoPorId(long personaId);
        void Modificar(EmpleadoDTO empleadoDto);

        void Eliminar(long empleadoId);
    }
}
