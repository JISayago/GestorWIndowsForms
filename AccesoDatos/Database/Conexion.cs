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
    }

}
