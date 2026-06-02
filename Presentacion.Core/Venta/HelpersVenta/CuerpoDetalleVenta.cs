using Servicios.Helpers.OpcionesPagos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Core.Venta.HelpersVenta
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    namespace Servicios.Helpers.Venta
    {
        public class CuerpoDetalleVenta
        {
            public decimal TotalOriginal { get; set; }

            public decimal TotalFinal { get; set; }

            public decimal Descuento => TotalOriginal - TotalFinal;

            public List<FormaPago> tiposDePago { get; set; } = new();

            public bool pagoParcial { get; set; }

            public decimal saldoPendiente { get; set; }

            public bool ofertaIncluidas { get; set; }

            public string descripcionOferta { get; set; }

            public string CuerpoDelTextoTP()
            {
                var sb = new StringBuilder();

                sb.AppendLine("FORMAS DE PAGO");

                foreach (var pago in tiposDePago)
                {
                    sb.AppendLine(
                        $"{pago.TipoDePago}: {pago.Monto:C2}");
                }

                if (pagoParcial)
                {
                    sb.AppendLine(
                        $"Saldo pendiente: {saldoPendiente:C2}");
                }

                return sb.ToString();
            }

            public string CuerpoDelTextoFinal(string observaciones)
            {
                var sb = new StringBuilder();

                sb.AppendLine("===== RESUMEN DE VENTA =====");
                sb.AppendLine();

                sb.AppendLine($"Total Original: {TotalOriginal:C2}");

                if (Descuento > 0)
                {
                    sb.AppendLine($"Descuento Aplicado: {Descuento:C2}");
                }

                sb.AppendLine($"Total Final: {TotalFinal:C2}");

                sb.AppendLine();
                sb.AppendLine("FORMAS DE PAGO");

                if (tiposDePago != null && tiposDePago.Any())
                {
                    foreach (var pago in tiposDePago)
                    {
                        sb.Append($"- {pago.TipoDePago}: {pago.Monto:C2}");

                        if (!string.IsNullOrWhiteSpace(pago.DatosExtra))
                        {
                            sb.Append($" | {pago.DatosExtra}");
                        }

                        sb.AppendLine();
                    }
                }
                else
                {
                    sb.AppendLine("Sin pagos registrados");
                }

                if (saldoPendiente > 0)
                {
                    sb.AppendLine();
                    sb.AppendLine($"Saldo Pendiente: {saldoPendiente:C2}");
                }

                if (ofertaIncluidas &&
                    !string.IsNullOrWhiteSpace(descripcionOferta))
                {
                    sb.AppendLine();
                    sb.AppendLine("OFERTAS APLICADAS");

                    sb.AppendLine(descripcionOferta);
                }

                if (!string.IsNullOrWhiteSpace(observaciones))
                {
                    sb.AppendLine();
                    sb.AppendLine("OBSERVACIONES");

                    sb.AppendLine(observaciones.Trim());
                }

                var resultado = sb.ToString().Trim();

                return resultado.Length > 2000
                    ? resultado.Substring(0, 2000)
                    : resultado;
            }
        }
    }
}
