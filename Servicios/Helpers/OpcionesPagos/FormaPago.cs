using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.OpcionesPagos
{
    public  class FormaPago
    {
        public int Numero { get; set; }
        public decimal Monto { get; set; }
        public TipoDePago? TipoDePago { get; set; }

        public string DatosExtra { get; set; } // Para información adicional como DNI o nombre del cliente en pagos.
    }
}
