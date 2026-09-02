using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Config
{
    public class ConfiguracionSistema
    {

        public ConfiguracionBaseDatos BaseDatos { get; set; } = new();

        public ConfiguracionAlmacenamiento Almacenamiento { get; set; } = new();

        public ConfiguracionLicencia Licencia { get; set; } = new();
    }
}
