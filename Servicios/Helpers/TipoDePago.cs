using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers
{
    public enum TipoDePago
    {
        Efectivo = 0,
        Transferencia = 1,
        Credito = 2, 
        Debito = 3, 
        CtaCte = 4,
        QR = 5,
        Cheque = 6,
        Otro = 7
    }
}
