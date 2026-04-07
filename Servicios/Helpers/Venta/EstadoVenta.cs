using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.VentaEnum
{
    public enum EstadoVenta
    {
        Confirmada = 0,//venta confirmada
        Cancelada = 10, // original cancelada
        CancelacionVenta = 99,// movimiento de la cancelacion (venta formato cancelada)

    }
}
