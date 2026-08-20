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
        public static string ObtenerCadenaConexion()
        {
            var db = ConfigManager.Config.BaseDatos;

            if (db.IntegratedSecurity)
            {
                return
                    $"Server={db.Servidor};" +
                    $"Database={db.BaseDeDatos};" +
                    $"Integrated Security=True;" +
                    $"TrustServerCertificate=True;";
            }

            return
                $"Server={db.Servidor};" +
                $"Database={db.BaseDeDatos};" +
                $"User Id={db.Usuario};" +
                $"Password={db.Password};" +
                $"TrustServerCertificate=True;";
        }
    }
    
}
