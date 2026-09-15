using Licencia.Criptografia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licencia.Servicios
{
    public class MachineInfoService
    {
        public string ObtenerNombreEquipo()
            => Environment.MachineName;

        public string ObtenerUsuario()
            => Environment.UserName;

        public string ObtenerSistemaOperativo()
            => Environment.OSVersion.VersionString;

        public string ObtenerVersionFramework()
            => Environment.Version.ToString();

        public string GenerarMachineFingerprint()
        {
            string datos =
                $"{Environment.MachineName}|{Environment.UserName}|{Environment.OSVersion.VersionString}";

            return HashService.CalcularHashBase64(datos);
        }
    }
}
