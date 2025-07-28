using Servicios.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Core.Venta
{
    public class Pago
    {
        public int Numero { get; set; }             
        public decimal Monto { get; set; }          
        public TipoDePago? TipoDePago { get; set; }  

    }
}
