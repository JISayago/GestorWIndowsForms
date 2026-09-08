using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licencia.Modelos
{
    public class LicenseInfo
    {
        public string Cliente { get; set; } = "";

        public string Empresa { get; set; } = "";

        public Guid InstallationId { get; set; }

        public LicenseType Tipo { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        public string VersionMinima { get; set; } = "";

        public string VersionMaxima { get; set; } = "";

        public List<LicenseModule> Modulos { get; set; } = new();

        public string Firma { get; set; } = "";
    }
}
