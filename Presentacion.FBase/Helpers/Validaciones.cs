using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Mail;

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
    }
}
