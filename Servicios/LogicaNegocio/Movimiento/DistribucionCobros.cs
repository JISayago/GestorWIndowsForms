using Servicios.Helpers.OpcionesPagos;

namespace Servicios.LogicaNegocio.Movimiento
{
    /// <summary>
    /// Reparte lo que entra o sale de la caja entre los tipos de pago de una venta (sin cuenta corriente).
    /// Función pura (sin base de datos): la suma del resultado es siempre igual a <c>montoCaja</c>.
    /// </summary>
    public static class DistribucionCobros
    {
        public static List<(TipoDePago Tipo, decimal Monto)> Distribuir(decimal montoCaja, IEnumerable<FormaPago>? pagos)
        {
            var resultado = new List<(TipoDePago Tipo, decimal Monto)>();

            montoCaja = Math.Abs(montoCaja);
            if (montoCaja == 0)
                return resultado;

            // Pagos que no son cuenta corriente, agrupados por tipo en el orden en que aparecen.
            var orden = new List<TipoDePago>();
            var acumulado = new Dictionary<TipoDePago, decimal>();

            foreach (var pago in pagos ?? Enumerable.Empty<FormaPago>())
            {
                if (!pago.TipoDePago.HasValue)
                    continue;

                var tipo = pago.TipoDePago.Value;
                if (tipo == TipoDePago.CtaCte)
                    continue;

                var monto = Math.Abs(pago.Monto);
                if (monto == 0)
                    continue;

                if (!acumulado.ContainsKey(tipo))
                {
                    acumulado[tipo] = 0;
                    orden.Add(tipo);
                }

                acumulado[tipo] += monto;
            }

            // Sin pagos informados: se asume efectivo.
            if (orden.Count == 0)
            {
                resultado.Add((TipoDePago.Efectivo, montoCaja));
                return resultado;
            }

            var pagado = acumulado.Values.Sum();

            if (pagado > montoCaja)
            {
                // Vuelto: se descuenta primero del efectivo y, si hiciera falta, del resto en orden.
                var exceso = pagado - montoCaja;

                foreach (var tipo in orden.OrderBy(t => t == TipoDePago.Efectivo ? 0 : 1))
                {
                    if (exceso <= 0)
                        break;

                    var quitar = Math.Min(acumulado[tipo], exceso);
                    acumulado[tipo] -= quitar;
                    exceso -= quitar;
                }
            }
            else if (pagado < montoCaja)
            {
                // Faltante: se suma al efectivo si existe, si no al primer tipo informado.
                var destino = orden.Contains(TipoDePago.Efectivo) ? TipoDePago.Efectivo : orden[0];
                acumulado[destino] += montoCaja - pagado;
            }

            foreach (var tipo in orden)
            {
                if (acumulado[tipo] > 0)
                    resultado.Add((tipo, acumulado[tipo]));
            }

            return resultado;
        }
    }
}
