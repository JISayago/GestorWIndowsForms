using Servicios.Helpers.Sistema;
using System;
using System.Collections.Generic;

namespace Servicios.LogicaNegocio.Configuracion.DTO
{
    /// <summary>
    /// Formato de un código: [Prefijo][Sep][Año][Sep][PuntoVenta][Sep][Número con ceros].
    /// Ej.: V-2026-0001-00000125. El contador (el número) NO vive acá: lo sigue manejando
    /// cada módulo; esto solo define cómo se arma el código.
    /// </summary>
    public class NomenclaturaCodigoDTO
    {
        public string Prefijo { get; set; } = "";
        public string Separador { get; set; } = "-";
        public int Digitos { get; set; } = 8;
        public bool IncluirAnio { get; set; }
        public bool IncluirPuntoVenta { get; set; }
        public int PuntoVenta { get; set; } = 1;

        public static NomenclaturaCodigoDTO PorDefecto(TipoCodigo tipo)
        {
            string prefijo = tipo == TipoCodigo.Venta ? "V" : tipo == TipoCodigo.Movimiento ? "M" : "G";
            return new NomenclaturaCodigoDTO { Prefijo = prefijo };
        }

        public string Formatear(long numero, DateTime fecha)
        {
            var partes = new List<string>();
            if (!string.IsNullOrWhiteSpace(Prefijo)) partes.Add(Prefijo.Trim());
            if (IncluirAnio) partes.Add(fecha.Year.ToString());
            if (IncluirPuntoVenta) partes.Add(PuntoVenta.ToString().PadLeft(4, '0'));
            partes.Add(numero.ToString().PadLeft(Math.Max(1, Digitos), '0'));
            return string.Join(Separador ?? "", partes);
        }
    }
}