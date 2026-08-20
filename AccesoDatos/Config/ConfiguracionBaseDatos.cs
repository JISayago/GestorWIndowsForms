using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Config
{
    public class ConfiguracionBaseDatos
    {
        public string Servidor { get; set; } = "";

        public string BaseDeDatos { get; set; } = "";

        public string Usuario { get; set; } = "";

        public string Password { get; set; } = "";

        public bool IntegratedSecurity { get; set; }
    }
}
