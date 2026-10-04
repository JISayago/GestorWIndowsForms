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
        /// <summary>Si no es null, tiene prioridad sobre la base del archivo de config. Solo en memoria.</summary>
        public static string? BaseForzada { get; set; }

        public static string ObtenerCadenaConexion(string? baseDatos = null)
        {
            var db = ConfigManager.Config.BaseDatos;
            var nombre = baseDatos ?? BaseForzada ?? db.BaseDeDatos;

            if (db.IntegratedSecurity)
                return $"Server={db.Servidor};Database={nombre};Integrated Security=True;TrustServerCertificate=True;";

            return $"Server={db.Servidor};Database={nombre};User Id={db.Usuario};Password={db.Password};TrustServerCertificate=True;";
        }
    }

}
