using AccesoDatos.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Database
{
    public static class Conexion
    {
        /// <summary>true = usar la base Trial. Se fija en Program.Main según la licencia.</summary>
        public static bool UsarTrial { get; set; }

        public static string ObtenerCadenaConexion(string? baseDatos = null)
        {
            var cfg = ConfigManager.Config;

            // Instalaciones viejas pueden no tener la sección BaseDatosTrial
            bool hayConfigTrial = cfg.BaseDatosTrial != null;

            var db = (UsarTrial && hayConfigTrial) ? cfg.BaseDatosTrial : cfg.BaseDatos;

            var nombre = baseDatos
                ?? ((UsarTrial && !hayConfigTrial) ? "StockeateTrial" : db.BaseDeDatos);

            if (db.IntegratedSecurity)
                return $"Server={db.Servidor};Database={nombre};Integrated Security=True;TrustServerCertificate=True;";

            return $"Server={db.Servidor};Database={nombre};User Id={db.Usuario};Password={db.Password};TrustServerCertificate=True;";
        }



        /// <summary>
        /// Solo para tests de integración. Evita tocar el .enc de producción.
        /// </summary>

        private static readonly object _sync = new();
        private static string? _cadenaConexionCache;

        public static void ConfigurarCadenaConexionParaTests(string cadenaConexion)
        {
            if (string.IsNullOrWhiteSpace(cadenaConexion))
                throw new ArgumentException("La cadena de conexión de test no puede estar vacía.", nameof(cadenaConexion));

            lock (_sync)
            {
                _cadenaConexionCache = cadenaConexion;
            }
        }

        public static void LimpiarCacheCadenaConexion()
        {
            lock (_sync)
            {
                _cadenaConexionCache = null;
            }
        }
    }
}
