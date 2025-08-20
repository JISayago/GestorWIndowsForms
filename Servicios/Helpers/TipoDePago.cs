using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers
{
    public enum TipoDePago
    {
        Efectivo = 1,
        Transferencia = 2,
        Credito = 3, 
        Debito = 4, 
        CtaCte = 5,
        QR = 6,
        Cheque = 7,
        Otro = 8
    }
}
