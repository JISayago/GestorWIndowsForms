using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.Sistema
{
    /// <summary>
    /// Sesión secundaria de Stockeate: una segunda copia de la aplicación (otro proceso), lanzada desde la
    /// principal, donde otro usuario (ej. un admin) hace un cambio o una venta puntual.
    /// Al ser otro proceso, DatosSistema y AuthHelper (estáticos) quedan aislados de la sesión principal.
    /// </summary>
    public static class SesionSecundaria
    {
        public const string Argumento = "--secundaria";
        private const string ArgPadre = "--padre=";
        private const string ArgSesion = "--sesion=";
        private const string ArgUsuario = "--usuario=";

        /// <summary>True si este proceso es la sesión secundaria.</summary>
        public static bool EsSecundaria { get; private set; }

        /// <summary>UsuarioSesionId de la sesión principal que lanzó esta secundaria.</summary>
        public static long SesionPrincipalId { get; private set; }

        /// <summary>Username de la sesión principal (la secundaria no puede ingresar con el mismo).</summary>
        public static string UsernamePrincipal { get; private set; } = "";

        /// <summary>Lo usa la principal para lanzar la secundaria.</summary>
        public static ProcessStartInfo CrearInicio(long sesionPrincipalId, string usernamePrincipal)
        {
            var psi = new ProcessStartInfo(Environment.ProcessPath!)
            {
                UseShellExecute = false
            };

            psi.ArgumentList.Add(Argumento);
            psi.ArgumentList.Add(ArgPadre + Environment.ProcessId);
            psi.ArgumentList.Add(ArgSesion + sesionPrincipalId);
            psi.ArgumentList.Add(ArgUsuario + usernamePrincipal);
            return psi;
        }

        /// <summary>
        /// Lo llama Program.Main en el proceso secundario. Valida los argumentos y que la principal esté viva;
        /// si la principal se cae, la secundaria se cierra sola.
        /// </summary>
        public static bool Inicializar(string[] args)
        {
            if (!TryLeerLong(args, ArgPadre, out long padrePid) ||
                !TryLeerLong(args, ArgSesion, out long sesionId))
                return Rechazar();

            string username = LeerTexto(args, ArgUsuario);
            if (string.IsNullOrWhiteSpace(username))
                return Rechazar();

            try
            {
                var padre = Process.GetProcessById((int)padrePid);

                using (var actual = Process.GetCurrentProcess())
                {
                    if (padre.HasExited ||
                        !string.Equals(padre.ProcessName, actual.ProcessName, StringComparison.OrdinalIgnoreCase))
                        return Rechazar();
                }

                // Sin 'using': el objeto tiene que seguir vivo para que dispare Exited.
                padre.EnableRaisingEvents = true;
                padre.Exited += (_, _) => Environment.Exit(0);

                if (padre.HasExited)    // se cayó entre la validación y la suscripción
                    return Rechazar();
            }
            catch (ArgumentException)   // el proceso padre no existe
            {
                return Rechazar();
            }
            catch (InvalidOperationException)
            {
                return Rechazar();
            }

            EsSecundaria = true;
            SesionPrincipalId = sesionId;
            UsernamePrincipal = username.Trim();
            return true;
        }

        private static bool Rechazar()
        {
            //MessageBox.Show(
            //    "La sesión secundaria solo puede iniciarse desde Stockeate (botón \"Cambio Usuario\").",
            //    "Stockeate",
            //    MessageBoxButtons.OK,
            //    MessageBoxIcon.Information);

            return false;
        }

        private static string LeerTexto(string[] args, string prefijo)
        {
            foreach (var a in args)
                if (a.StartsWith(prefijo, StringComparison.OrdinalIgnoreCase))
                    return a.Substring(prefijo.Length);

            return "";
        }

        private static bool TryLeerLong(string[] args, string prefijo, out long valor)
            => long.TryParse(LeerTexto(args, prefijo), out valor);
    }
}
