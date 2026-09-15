using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Config
{
    public class ConfiguracionAlmacenamiento
    {
        public string RutaPrincipal { get; set; } = "";

        public string CarpetaComprobantes { get; set; } = "ComprobantesPdf";

        public string CarpetaBackups { get; set; } = "Backups";

        public string CarpetaLogs { get; set; } = "Logs";

        public string CarpetaExportaciones { get; set; } = "Exportaciones";
    }
}
