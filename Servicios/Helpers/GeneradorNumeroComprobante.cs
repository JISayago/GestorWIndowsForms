using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers
{
    public class GeneradorNumeroComprobante
    {
        public static string Generar(
        string prefijo,
        DateTime fecha,
        int cantidadDelDia
    )
        {
            return $"{prefijo}-{fecha:yyyyMMdd}-{(cantidadDelDia + 1):D3}";
        }
    }
}
