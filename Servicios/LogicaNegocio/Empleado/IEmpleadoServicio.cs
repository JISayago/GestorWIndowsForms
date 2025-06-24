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
        IEnumerable<EmpleadoDTO> ObtenerEmpleados();

        long Insertar(EmpleadoDTO empleadoDto);

        void Modificar(EmpleadoDTO empleadoDto);

        void Eliminar(long empleadoId);
    }
}
