using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.Sistema.Admin
{
    public enum EstadoEmpleado
    {
        Inhablitado = 0, //valor por defecto inicio usuario primer ingreso.
        Habilitado = 1, // Activo
        SinPass = 2, // olvido pass
       // Suspendido = 3, // Activo pero sin acceso momentaneo del sistema
    }
}
