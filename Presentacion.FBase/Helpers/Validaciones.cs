using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.FBase.Helpers
{
    /// Regla: devuelve null si el control es válido, o el mensaje de error si no lo es.
    public delegate string ReglaValidacion(Control control, string nombreCampo);

    public static class Validaciones
    {
        private static string Texto(Control c) => c.Text?.Trim() ?? string.Empty;

        // ---------- Reglas directas (se pasan como método) ----------

        public static string Obligatorio(Control c, string nombre)
        {
            switch (c)
            {
                case ComboBox cmb:
                    bool vacio = cmb.DropDownStyle == ComboBoxStyle.DropDownList
                        ? cmb.SelectedIndex < 0
                        : string.IsNullOrWhiteSpace(cmb.Text);
                    return vacio ? $"Seleccione una opción en {nombre}." : null;

                case DateTimePicker dtp:
                    return dtp.ShowCheckBox && !dtp.Checked ? $"Seleccione {nombre}." : null;

                default:
                    return string.IsNullOrWhiteSpace(c.Text)
                        ? $"El campo {nombre} no puede quedar vacío."
                        : null;
            }
        }

        public static string SoloNumeros(Control c, string nombre)
        {
            var t = Texto(c);
            return t.Length == 0 || t.All(ch => ch >= '0' && ch <= '9')
                ? null
                : $"{nombre} solo puede contener números.";
        }

        public static string Email(Control c, string nombre)
        {
            var t = Texto(c);
            if (t.Length == 0) return null;
            try { if (new MailAddress(t).Address == t) return null; } catch { }
            return $"{nombre} no tiene un formato de email válido.";
        }

        public static string MayorACero(Control c, string nombre)
            => c is NumericUpDown nud && nud.Value <= 0 ? $"{nombre} debe ser mayor a cero." : null;

        /// Para combos cuyo item 0 es "Seleccione..." (placeholder)
        public static string ComboSinPlaceholder(Control c, string nombre)
            => c is ComboBox cmb && cmb.SelectedIndex <= 0 ? $"Seleccione una opción válida en {nombre}." : null;

        public static string FechaNoFutura(Control c, string nombre)
            => c is DateTimePicker dtp && dtp.Value.Date > DateTime.Today
                ? $"{nombre} no puede ser una fecha futura." : null;

        // ---------- Reglas con parámetros (fábricas) ----------

        public static ReglaValidacion Longitud(int min, int max) => (c, nombre) =>
        {
            var t = Texto(c);
            if (t.Length == 0 || (t.Length >= min && t.Length <= max)) return null;
            return min == max
                ? $"{nombre} debe tener {min} caracteres."
                : $"{nombre} debe tener entre {min} y {max} caracteres.";
        };
        // Acepta enteros y decimales, con coma o punto como separador (ej. 12, 12,50, 12.50)
        public static string Decimal(Control c, string nombre)
        {
            var t = Texto(c);
            if (t.Length == 0) return null;
            return TryParseDecimal(t, out _)
                ? null
                : $"{nombre} debe ser un número válido (ej. 1234,50).";
        }

        public static string DecimalMayorACero(Control c, string nombre)
        {
            var t = Texto(c);
            if (t.Length == 0) return null;
            if (!TryParseDecimal(t, out var valor))
                return $"{nombre} debe ser un número válido (ej. 1234,50).";
            return valor > 0 ? null : $"{nombre} debe ser mayor a cero.";
        }

        // Fábrica: limita la cantidad de decimales (ej. 2 para importes)
        public static ReglaValidacion DecimalConMaxDecimales(int maxDecimales) => (c, nombre) =>
        {
            var t = Texto(c);
            if (t.Length == 0) return null;
            if (!TryParseDecimal(t, out _))
                return $"{nombre} debe ser un número válido (ej. 1234,50).";

            int pos = t.IndexOfAny(new[] { ',', '.' });
            int cantDecimales = pos < 0 ? 0 : t.Length - pos - 1;
            return cantDecimales <= maxDecimales
                ? null
                : $"{nombre} admite como máximo {maxDecimales} decimales.";
        };

        // Fábrica: valida un rango (ej. descuento entre 0 y 100)
        public static ReglaValidacion DecimalEntre(decimal min, decimal max) => (c, nombre) =>
        {
            var t = Texto(c);
            if (t.Length == 0) return null;
            if (!TryParseDecimal(t, out var valor))
                return $"{nombre} debe ser un número válido (ej. 1234,50).";
            return valor >= min && valor <= max
                ? null
                : $"{nombre} debe estar entre {min} y {max}.";
        };

        // Parser tolerante: acepta coma o punto como separador decimal
        // y NO acepta separadores de miles (para evitar ambigüedad con "1.234").
        public static bool TryParseDecimal(string texto, out decimal valor)
        {
            valor = 0;
            texto = texto?.Trim();
            if (string.IsNullOrEmpty(texto)) return false;

            // Un solo separador permitido
            if (texto.Count(ch => ch == ',' || ch == '.') > 1) return false;

            texto = texto.Replace(',', '.');
            return decimal.TryParse(texto, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture, out valor);
        }
    }
}
