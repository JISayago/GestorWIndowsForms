using Servicios.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Core.Venta.HelpersVenta
{
    public class CuerpoDetalleVenta
    {
        public List<FormaPago> tiposDePago;
        public bool pagoParcial { get; set; }
        public decimal saldoPendiente { get; set; } = 0.00m;

        public bool ofertaIncluidas { get; set; }
        public string descripcionOferta { get; set; }

        public string CuerpoDelTextoTP()
        {
            var pagos = tiposDePago != null && tiposDePago.Any()
                ? string.Join(", ", tiposDePago.Select(p => $"{p.TipoDePago}: {p.Monto:C2}"))
                : "Sin pagos registrados";

            var texto = $"Pagos Realizados: {pagos}";

            if (pagoParcial)
                texto += $"{Environment.NewLine}Saldo Pendiente: {saldoPendiente:C2}";

            return texto;
        }

        public string CuerpoDelTextoFinal(string extra)
        {
            var pagos = $"Pagos Realizados: {string.Join(", ", tiposDePago.Select(p => $"{p.TipoDePago}: {p.Monto:C2}"))}";
            var saldo = $"Saldo Pendiente: {saldoPendiente:C2}";

            var ofertas = string.Empty;

            if (ofertaIncluidas && !string.IsNullOrWhiteSpace(descripcionOferta))
            {
                ofertas = $"{Environment.NewLine}Ofertas aplicadas: {descripcionOferta}";
            }

            var detalleExtra = $"{Environment.NewLine}Detalle adicional: {extra}";

            return $"{pagos}{Environment.NewLine}{saldo}{ofertas}{detalleExtra}";
        }
    }
}
