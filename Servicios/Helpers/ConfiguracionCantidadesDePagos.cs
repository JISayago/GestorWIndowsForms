using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers
{
    public class ConfiguracionCantidadesDePagos
    {
        public int[] listPagosCantidades { get; set; }
        public int Efectivo { get; set; }
        public int Credito { get; set; }
        public int CtaCte { get; set; }
        public int Transferencia { get; set; }
        public int Debito { get; set; }
        public int QR { get; set; }
        public int Cheque { get; set; }
        public int Otro { get; set; }
        public ConfiguracionCantidadesDePagos()
        {
            listPagosCantidades = new int[8] { 1, 2, 1, 1, 1, 1, 1, 1 };
            /*Efectivo = 1;
            Credito = 2;
            CtaCte = 1;
            Transferencia = 1;
            Debito = 1;
            QR = 1;
            Cheque = 1;
            Otro = 1;*/
        }

    }
}
