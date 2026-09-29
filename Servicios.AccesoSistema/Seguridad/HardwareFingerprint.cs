using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.AccesoSistema.Seguridad
{
        public static class HardwareFingerprint
        {
            /// <summary>
            /// Obtiene el fingerprint de hardware de la máquina
            /// utilizando:
            ///
            /// UUID de Win32_ComputerSystemProduct
            /// +
            /// ProcessorId de Win32_Processor
            ///
            /// El resultado final es SHA-256 en hexadecimal.
            /// </summary>
            public static string Obtener()
            {
                string uuid = ObtenerUuid();
                string processorId = ObtenerProcessorId();

                uuid = Normalizar(uuid);
                processorId = Normalizar(processorId);

                string datos =
                    $"{uuid}-{processorId}";

                byte[] datosBytes =
                    Encoding.UTF8.GetBytes(datos);

                byte[] hash =
                    SHA256.HashData(datosBytes);

                return Convert.ToHexString(hash);
            }

            private static string ObtenerUuid()
            {
                using var searcher =
                    new ManagementObjectSearcher(
                        "SELECT UUID FROM Win32_ComputerSystemProduct");

                using var resultados = searcher.Get();

                foreach (ManagementObject item in resultados)
                {
                    string uuid =
                        item["UUID"]?.ToString();

                    if (!string.IsNullOrWhiteSpace(uuid))
                        return uuid;
                }

                throw new InvalidOperationException(
                    "No se pudo obtener el UUID de hardware del equipo.");
            }

            private static string ObtenerProcessorId()
            {
                using var searcher =
                    new ManagementObjectSearcher(
                        "SELECT ProcessorId FROM Win32_Processor");

                using var resultados = searcher.Get();

                foreach (ManagementObject item in resultados)
                {
                    string processorId =
                        item["ProcessorId"]?.ToString();

                    if (!string.IsNullOrWhiteSpace(processorId))
                        return processorId;
                }

                throw new InvalidOperationException(
                    "No se pudo obtener el identificador del procesador.");
            }

            private static string Normalizar(string valor)
            {
                return valor
                    .Trim()
                    .ToUpperInvariant();
            }
        }
}
