using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace AccesoDatos.Config
{
    public class Conexion
    {
        public class ConexionInfo
        {
            public string Servidor { get; set; }
            public string BaseDeDatos { get; set; }
            public string Usuario { get; set; }
            public string Password { get; set; }
        }

        private static readonly object _sync = new();
        private static string? _cadenaConexionCache;

        /// <summary>
        /// Solo para tests de integración. Evita tocar el .enc de producción.
        /// </summary>
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

        public static string ObtenerCadenaConexion()
        {
            if (!string.IsNullOrEmpty(_cadenaConexionCache))
                return _cadenaConexionCache;

            lock (_sync)
            {
                if (!string.IsNullOrEmpty(_cadenaConexionCache))
                    return _cadenaConexionCache;

                _cadenaConexionCache = LeerYDesencriptarCadena();
                return _cadenaConexionCache;
            }
        }

        private static string LeerYDesencriptarCadena()
        {
            var ruta = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\AccesoDatos\Config\configc.json.enc"));

            var clave = "canto-lover-ncgm";
            var iv = "noveoporquenodot";

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(clave.PadRight(32));
            aes.IV = Encoding.UTF8.GetBytes(iv.PadRight(16));

            using var decryptor = aes.CreateDecryptor();
            using var fs = new FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var cs = new CryptoStream(fs, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            var json = sr.ReadToEnd();

            var datos = JsonSerializer.Deserialize<ConexionInfo>(json)
                ?? throw new InvalidOperationException("No se pudo deserializar la configuración de conexión.");

            return $"Server={datos.Servidor};Database={datos.BaseDeDatos};User Id={datos.Usuario};Password={datos.Password};TrustServerCertificate=True;";
        }
    }
}
