using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers
{
    public enum TipoMovimientoDetalle
    {
        Cancelacion = 1,
        CuentaCorriente = 2,
        Stock = 3,
        Venta = 4,
        Compra = 5,
        Servicios = 6, // ver por el tema de los tipos de gasto mirarlo con el abm de gasto en las opciones de categoria de movimiento
        VentaLibre = 7,
    }
}
