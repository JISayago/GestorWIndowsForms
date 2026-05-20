using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.Sistema.Extras
{
    public static class NormalizadorTextoBusqueda
    {
            public static string NormalizarTextoParaBusqueda(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;

            texto = texto.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD);

            var sb = new StringBuilder();

            foreach (var c in texto)
            {
                var categoria = CharUnicodeInfo.GetUnicodeCategory(c);
                if (categoria != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
