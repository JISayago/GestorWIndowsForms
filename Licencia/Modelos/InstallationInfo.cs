using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licencia.Modelos
{
    public class InstallationInfo
{
    public Guid InstallationId { get; set; }

    public DateTime FechaInstalacion { get; set; }

    public string VersionInstalada { get; set; } = "";

    public string NombreEquipo { get; set; } = "";
}
}
