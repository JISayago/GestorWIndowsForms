using Servicios.Helpers.OpcionesPagos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servicios.LogicaNegocio.Venta
{
    /// <summary>
    /// Calcula cuánto de una venta va a caja física vs cuenta corriente.
    /// Separado para poder verificarlo con tests sin DB.
    /// </summary>
    public static class VentaMontosHelper
    {
        public static (decimal MontoCaja, decimal MontoCtaCte) Calcular(
            decimal total,
            IEnumerable<FormaPago> tiposDePago)
        {
            decimal montoCaja = 0;
            decimal montoCtaCte = 0;

            var pagoCtaCte = tiposDePago?
                .FirstOrDefault(x =>
                    x.TipoDePago.HasValue &&
                    Convert.ToInt32(x.TipoDePago.Value) == (int)TipoDePago.CtaCte);

            if (pagoCtaCte != null)
            {
                montoCtaCte = Math.Abs(pagoCtaCte.Monto);
                montoCaja = Math.Abs(total) - montoCtaCte;
            }
            else
            {
                montoCaja = Math.Abs(total);
            }

            if (montoCaja < 0)
                montoCaja = 0;

            return (montoCaja, montoCtaCte);
        }
    }
}
