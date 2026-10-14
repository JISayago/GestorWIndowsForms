using Licencia.Constantes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Licencia.Servicios
{
    internal class ClockGuard
    {
        private static readonly byte[] Entropia =
            Encoding.UTF8.GetBytes("Stockeate.ClockGuard.v1");

        private static readonly TimeSpan Tolerancia =
            TimeSpan.FromHours(24);

        private static string Ruta =>
            Path.Combine(LicensePaths.ProgramData, "clock.dat");

        public bool RelojRetrocedido()
        {
            DateTime? ultima = Leer();

            if (ultima == null)
                return false;

            return DateTime.UtcNow < ultima.Value - Tolerancia;
        }

        public void Registrar()
        {
            try
            {
                DateTime ahora = DateTime.UtcNow;
                DateTime? ultima = Leer();

                if (ultima != null && ahora <= ultima.Value)
                    return;

                string texto = ahora.ToString("O", CultureInfo.InvariantCulture);

                byte[] cifrado = ProtectedData.Protect(
                    Encoding.UTF8.GetBytes(texto),
                    Entropia,
                    DataProtectionScope.LocalMachine);

                Directory.CreateDirectory(LicensePaths.ProgramData);
                File.WriteAllBytes(Ruta, cifrado);
            }
            catch
            {
                // Nunca debe impedir el uso del sistema.
            }
        }

        private static DateTime? Leer()
        {
            try
            {
                if (!File.Exists(Ruta))
                    return null;

                byte[] plano = ProtectedData.Unprotect(
                    File.ReadAllBytes(Ruta),
                    Entropia,
                    DataProtectionScope.LocalMachine);

                return DateTime.Parse(
                    Encoding.UTF8.GetString(plano),
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind);
            }
            catch
            {
                return null;
            }
        }
    }
}
