using Servicios.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Core.Venta
{
    public class CuerpoDetalleVenta
    {
        public List<FormaPago> tiposDePago;
        public bool pagoParcial { get; set; }
        public decimal saldoPendiente { get; set; } = 0.00m;


    public string CuerpoDelTextoTP()
        {
            return $"Pagos Realizados: {string.Join(", ", tiposDePago.Select(p => $"{p.TipoDePago}: {p.Monto:C2}"))} "+
                   $"  Saldo Pendiente: {saldoPendiente:C2}";

        }
        public string CuerpoDelTextoFinal(string extra)
        {
            string cuerpo = $"Pagos Realizados: {string.Join(", ", tiposDePago.Select(p => $"{p.TipoDePago}: {p.Monto:C2}"))}" +
                            $"{Environment.NewLine}Saldo Pendiente: {saldoPendiente:C2}\n" +
                            $"{Environment.NewLine}Detalle adicional: {extra}";

            return cuerpo;
        }
    }
}
