using AccesoDatos.Config;

namespace Presentacion.FBase.Helpers
{
    public enum TipoTema { Original, Clasico }

    public static class TemaSistema
    {
        private sealed record Paleta(
            Color Primario, Color Oscuro, Color Acento, Color IntermedioVioleta,
            Color Fondo, Color FondoControl, Color Borde,
            Color Texto, Color TextoSecundario, Color Seleccion, Color Alternado);

        // Original: exactamente los valores que ya tenías.
        // (Alternativas que tenías comentadas: Fondo #F3F4F6 / FondoControl #FFFFFF / Borde #D1D5DB
        //  y Fondo #F8F5EF / FondoControl #FFFDF8 / Borde #DDD6C8)
        private static readonly Paleta Original = new(
            Primario: ColorTranslator.FromHtml("#431487"),
            Oscuro: ColorTranslator.FromHtml("#020014"),
            Acento: ColorTranslator.FromHtml("#F4E2B6"),
            IntermedioVioleta: ColorTranslator.FromHtml("#906EC3"),
            Fondo: ColorTranslator.FromHtml("#EAEAEA"),
            FondoControl: ColorTranslator.FromHtml("#ECECEC"),
            Borde: ColorTranslator.FromHtml("#A0A0A0"),
            Texto: ColorTranslator.FromHtml("#1F1A2B"),
            TextoSecundario: ColorTranslator.FromHtml("#6E687A"),
            Seleccion: ColorTranslator.FromHtml("#DCC7FF"),
            Alternado: ColorTranslator.FromHtml("#FAF7F1"));

        // Clásico: los tonos violetas pasan a grises neutros. Fondo/FondoControl/Borde ya eran grises
        // y el Acento (crema) no es violeta, así que quedan igual. Ajustá los grises a gusto.
        private static readonly Paleta Clasico = new(
            Primario: ColorTranslator.FromHtml("#3C3C3C"),
            Oscuro: ColorTranslator.FromHtml("#0A0A0A"),
            Acento: ColorTranslator.FromHtml("#F4E2B6"),
            IntermedioVioleta: ColorTranslator.FromHtml("#8C8C8C"),
            Fondo: ColorTranslator.FromHtml("#EAEAEA"),
            FondoControl: ColorTranslator.FromHtml("#ECECEC"),
            Borde: ColorTranslator.FromHtml("#A0A0A0"),
            Texto: ColorTranslator.FromHtml("#1F1F1F"),
            TextoSecundario: ColorTranslator.FromHtml("#6B6B6B"),
            Seleccion: ColorTranslator.FromHtml("#D6D6D6"),
            Alternado: ColorTranslator.FromHtml("#F5F5F5"));

        public static TipoTema Actual { get; private set; } = TipoTema.Original;

        /// <summary>Los formularios base pueden suscribirse para re-estilarse sin reiniciar.</summary>
        public static event EventHandler? TemaCambiado;

        // Mismos nombres que antes, para que todo el código existente siga compilando.
        public static Color Primario { get; private set; }
        public static Color Oscuro { get; private set; }
        public static Color Acento { get; private set; }
        public static Color IntermedioVioleta { get; private set; }
        public static Color Fondo { get; private set; }
        public static Color FondoControl { get; private set; }
        public static Color Borde { get; private set; }
        public static Color Texto { get; private set; }
        public static Color TextoSecundario { get; private set; }
        public static Color Seleccion { get; private set; }
        public static Color Alternado { get; private set; }

        static TemaSistema() => Asignar(Original);

        /// <summary>Llamar al iniciar la app (lee Apariencia.Tema del config.json).</summary>
        public static void CargarDesdeConfig() =>
            Aplicar(Enum.TryParse(ConfigManager.Config.Apariencia.Tema, true, out TipoTema t) ? t : TipoTema.Original);

        public static void Aplicar(TipoTema tema)
        {
            Asignar(tema == TipoTema.Clasico ? Clasico : Original);
            Actual = tema;
            TemaCambiado?.Invoke(null, EventArgs.Empty);
        }

        /// <summary>Colores representativos de un tema, para la vista previa en Configuración.</summary>
        public static IEnumerable<(string Nombre, Color Color)> Muestras(TipoTema tema)
        {
            var p = tema == TipoTema.Clasico ? Clasico : Original;
            yield return ("Primario", p.Primario);
            yield return ("Oscuro", p.Oscuro);
            yield return ("Intermedio", p.IntermedioVioleta);
            yield return ("Acento", p.Acento);
            yield return ("Fondo", p.Fondo);
            yield return ("Borde", p.Borde);
            yield return ("Texto", p.Texto);
            yield return ("Selección", p.Seleccion);
        }

        private static void Asignar(Paleta p)
        {
            Primario = p.Primario;
            Oscuro = p.Oscuro;
            Acento = p.Acento;
            IntermedioVioleta = p.IntermedioVioleta;
            Fondo = p.Fondo;
            FondoControl = p.FondoControl;
            Borde = p.Borde;
            Texto = p.Texto;
            TextoSecundario = p.TextoSecundario;
            Seleccion = p.Seleccion;
            Alternado = p.Alternado;
        }
    }
}