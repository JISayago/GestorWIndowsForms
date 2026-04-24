using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.Sistema
{
    public enum NivelUrgencia
    {
        //Pensado para las notificaciones, para que el sistema sepa cuales mostrar primero o cuales destacar mas.

        //la "urgencia" depende de cada TIPO de notificacion, por ejemplo una notificacion de producto vencido
        //es mas o menos urgente segun la cantidad de dias que falten para el vencimiento, o si ya esta vencido,
        //entonces seria alta urgencia.

        Baja = 1, //VENCE EN LOS PROXIMOS 4-7 DIAS
        Media = 2, //VENCE EN LOS PROXIMOS 1-3 DIAS
        Alta = 3 //VENCE HOY 
    }
}
