using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        public static string ObtenerCadenaConexion()
        {
            var ruta = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\AccesoDatos\Config\configc.json.enc"));

            var clave = "canto-lover-ncgm";
            var iv = "noveoporquenodot";

            using var aes = Aes.Create();
            aes.Key = System.Text.Encoding.UTF8.GetBytes(clave.PadRight(32));
            aes.IV = System.Text.Encoding.UTF8.GetBytes(iv.PadRight(16));

            using var decryptor = aes.CreateDecryptor();
            using var fs = new FileStream(ruta, FileMode.Open);
            using var cs = new CryptoStream(fs, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            var json = sr.ReadToEnd();

            var datos = JsonSerializer.Deserialize<ConexionInfo>(json);
          
            return $"Server={datos.Servidor};Database={datos.BaseDeDatos};User Id={datos.Usuario};Password={datos.Password};TrustServerCertificate=True;";
        }
       
    }
}
