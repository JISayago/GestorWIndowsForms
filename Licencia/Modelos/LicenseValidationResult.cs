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

        public string Mensaje { get; set; } = "";
    }
}
