using System;
using System.IO;
using System.Text.Json;

namespace AccesoDatos.Config
{
    public class ConfigManager
    {
        private static ConfiguracionSistema? _config;

        public static ConfiguracionSistema Config
        {
            get
            {
                if (_config == null)
                    Cargar();

                return _config!;
            }
        }

        private static string ObtenerRutaConfig()
        {
            return Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
    "Stockeate",
    "config.json");
        }

        public static void Guardar()
        {
            var ruta = ObtenerRutaConfig();

            Directory.CreateDirectory(Path.GetDirectoryName(ruta)!);

            var json = JsonSerializer.Serialize(
                _config,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(ruta, json);
        }

        public static void Recargar()
        {
            Cargar();
        }

        private static void Cargar()
        {
            var ruta = ObtenerRutaConfig();

            if (!File.Exists(ruta))
                throw new FileNotFoundException(
                    "No se encontró el archivo de configuración.",
                    ruta);

            var json = File.ReadAllText(ruta);

            _config = JsonSerializer.Deserialize<ConfiguracionSistema>(json)
                ?? throw new Exception("No se pudo leer la configuración.");
        }
    }
}