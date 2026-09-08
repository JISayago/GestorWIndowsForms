using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licencia.Constantes
{
    public static class LicensePaths
    {
        public static string ProgramData =>
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "Stockiate");

        public static string Config =>
            Path.Combine(ProgramData, "config.json");

        public static string License =>
            Path.Combine(ProgramData, "license.json");

        public static string Installation =>
            Path.Combine(ProgramData, "installation.json");

        public static string Logs =>
            Path.Combine(ProgramData, "Logs");

        public static string Backups =>
            Path.Combine(ProgramData, "Backups");

        public static string Exportaciones =>
            Path.Combine(ProgramData, "Exportaciones");

        public static string Comprobantes =>
            Path.Combine(ProgramData, "ComprobantesPdf");
    }
}
