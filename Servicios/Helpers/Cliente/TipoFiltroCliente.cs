using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.Cliente
{
    public enum TipoFiltroCliente
    {
        FechaAlta = 1,
        FechaBaja = 2,
        ConCtaCte = 3,
        SinCtaCte = 4,
        Inhabilitado = 5,
        Activo = 6, 
        Baja = 7
    }
}
