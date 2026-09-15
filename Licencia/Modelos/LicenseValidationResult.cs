using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licencia.Modelos
{
    public class LicenseValidationResult
    {
        public bool Valida { get; set; }

        public bool SoloConsulta { get; set; }

        public string Mensaje { get; set; } = "";

        public LicenseStatus Estado { get; set; }
    }
}
