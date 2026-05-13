using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.Producto
{
    public enum EstadoProducto
    {
        Normal = 1,
        Vencido = 2, //este no tiene sentido
        Discontinuado = 3,
        SinStock = 4
    }
}
