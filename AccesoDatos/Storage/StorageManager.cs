using AccesoDatos.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Storage
{
    public static class StorageManager
    {
        private static ConfiguracionAlmacenamiento Config =>
            ConfigManager.Config.Almacenamiento;

        public static string RutaPrincipal =>
            Config.RutaPrincipal;

        public static string ObtenerRutaLogs()
        {
            return CrearCarpeta(Config.CarpetaLogs);
        }

        public static string ObtenerRutaBackups()
        {
            return CrearCarpeta(Config.CarpetaBackups);
        }

        public static string ObtenerRutaExportaciones()
        {
            return CrearCarpeta(Config.CarpetaExportaciones);
        }

        public static string ObtenerRutaIdentidadComprobantes()
        {
            return CrearCarpeta(Config.CarpetaComprobantes, "Identidad");
        }

        public static string ObtenerRutaComprobante(
            string modulo,
            string tipo,
            DateTime fecha)
        {
            return CrearCarpeta(
                Config.CarpetaComprobantes,
                modulo,
                tipo,
                fecha.Year.ToString(),
                fecha.Month.ToString("D2"));
        }

        private static string CrearCarpeta(params string[] partes)
        {
            var ruta = Path.Combine(
                new[] { RutaPrincipal }
                    .Concat(partes)
                    .ToArray());

            Directory.CreateDirectory(ruta);

            return ruta;
        }
    }
}
