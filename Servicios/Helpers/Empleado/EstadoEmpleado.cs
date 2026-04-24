using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.Empleado
{
    public enum EstadoEmpleado
    {
        Inhablitado = 1, //valor por defecto inicio usuario primer ingreso.
        Habilitado = 2, // Activo
        SinPass = 3, // olvido pass
       // Suspendido = 3, // Activo pero sin acceso momentaneo del sistema
    }
}
