using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.Producto
{
    public enum EstadoProducto
    {
        Disponible = 1,
        Vencido = 2, //borrar no se usa en producto
        Discontinuado = 3,
        SinStock = 4
    }
}
