using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers
{
    public  class FormaPago
    {
        public int Numero { get; set; }
        public decimal Monto { get; set; }
        public TipoDePago? TipoDePago { get; set; }
    }
}
