using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.Movimiento
{
    public enum TipoMovimiento
    {
        /*--------------------------------
         * Se utilzia para identificar si es un tipo de movimiento de ingreso o egreso, esto es importante para el manejo de caja y reportes financieros.
         * -------------------------*/
        Ingreso = 1,
        Egreso = 2,
        

    }
}
