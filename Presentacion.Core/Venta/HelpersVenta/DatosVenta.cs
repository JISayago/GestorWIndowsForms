using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Core.Venta.HelpersVenta
{
    public class DatosVenta
    {
        public decimal Total { get; set; }
        public bool IncluirCtaCte { get; set; }

        public bool DescuentoEfectivo { get; set; }
    }
}
