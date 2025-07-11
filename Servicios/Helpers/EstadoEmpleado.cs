using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers
{
    public enum EstadoEmpleado
    {
        Inhablitado = 0, //valor por defecto
        Habilitado = 1, // Activo
        Bloqueado = 2, // No permitir acceso posiblemente por EstaEliminado True
        Suspendido = 3, // Activo pero sin acceso momentaneo del sistema
    }
}
