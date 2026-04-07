using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.Movimiento
{
    public enum TipoEntidadMovimiento
    {
        /* -----------------------------
         * De este se basa la ENTIDAD MOVIMIENTO para nombrar en toda accion de guardado del movimiento en bbdd.
         * el nombre de las opciones del enum tienen que si o si  representar la entidad que se quiera representar(venr en carpeta de entidades por el nombre correcto a utilizarr.)
         * ------------------- 
         */
        Venta = 1,
        CuentaCorriente = 2,
        Producto = 3,
        Caja = 4,
        Gasto = 5,
        VentaLibre = 6,
    }
}
