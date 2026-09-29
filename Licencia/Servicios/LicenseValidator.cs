using Licencia.Criptografia;
using Licencia.Modelos;
using Licencia.Servicios;
using Servicios.AccesoSistema.Seguridad;
using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Stockeate.Licensing
{
    public class LicenseValidator
    {
        private readonly LicenseStorage _storage = new();
        private readonly InstallationManager _installation = new();

        public LicenseValidationResult Validar(
            string publicKeyPem)
        {
            // ========================================================
            // 1. OBTENER / CREAR INSTALACION
            // ========================================================

            InstallationInfo instalacion;

            try
            {
                instalacion =
                    _installation.Obtener();
            }
            catch (Exception ex)
            {
                return new LicenseValidationResult
                {
                    Valida = false,
                    Estado = LicenseStatus.Invalida,
                    SoloConsulta = false,
                    Mensaje =
                        "No se pudo crear o leer la información " +
                        "de instalación.\r\n\r\n" +
                        ex.Message
                };
            }

            // ========================================================
            // 2. EXISTE LICENCIA
            // ========================================================

            if (!_storage.Existe())
            {
                return new LicenseValidationResult
                {
                    Valida = false,
                    Estado = LicenseStatus.NoExiste,
                    SoloConsulta = false,
                    Mensaje =
                        "No existe una licencia para esta instalación."
                };
            }

            // ========================================================
            // 3. LEER LICENCIA
            // ========================================================

            LicenseInfo licencia;

            try
            {
                licencia =
                    _storage.Leer();
            }
            catch (Exception ex)
            {
                return new LicenseValidationResult
                {
                    Valida = false,
                    Estado = LicenseStatus.Invalida,
                    SoloConsulta = false,
                    Mensaje =
                        "No se pudo leer el archivo de licencia.\r\n\r\n" +
                        ex.Message
                };
            }

            // ========================================================
            // 4. FIRMA
            // ========================================================

            string firmaOriginal =
                licencia.Firma;

            if (string.IsNullOrWhiteSpace(
                    firmaOriginal))
            {
                return new LicenseValidationResult
                {
                    Valida = false,
                    Estado = LicenseStatus.Invalida,
                    SoloConsulta = false,
                    Mensaje =
                        "La licencia no contiene una firma digital."
                };
            }

            if (string.IsNullOrWhiteSpace(
                    licencia.HardwareFingerprint))
            {
                return new LicenseValidationResult
                {
                    Valida = false,
                    Estado = LicenseStatus.Invalida,
                    SoloConsulta = false,
                    Mensaje =
                        "La licencia no contiene la identificación " +
                        "de hardware requerida.\r\n\r\n" +
                        "Debe solicitar una nueva licencia."
                };
            }

            string datos =
                CrearDatosCanonicos(licencia);

            bool firmaValida =
                RsaVerifier.VerificarFirma(
                    datos,
                    firmaOriginal,
                    publicKeyPem);

            if (!firmaValida)
            {
                return new LicenseValidationResult
                {
                    Valida = false,
                    Estado = LicenseStatus.Invalida,
                    SoloConsulta = false,
                    Mensaje =
                        "Firma digital inválida."
                };
            }

            // ========================================================
            // 5. INSTALLATION ID
            // ========================================================

            if (licencia.InstallationId !=
                instalacion.InstallationId)
            {
                return new LicenseValidationResult
                {
                    Valida = false,
                    Estado = LicenseStatus.Invalida,
                    SoloConsulta = false,
                    Mensaje =
                        "La licencia pertenece a otra instalación."
                };
            }

            // ========================================================
            // 6. HARDWARE
            // ========================================================

            string hardwareActual;

            try
            {
                hardwareActual =
                    HardwareFingerprint.Obtener();
            }
            catch (Exception ex)
            {
                return new LicenseValidationResult
                {
                    Valida = false,
                    Estado = LicenseStatus.Invalida,
                    SoloConsulta = false,
                    Mensaje =
                        "No se pudo obtener la identificación de hardware " +
                        "del equipo.\r\n\r\n" +
                        ex.Message
                };
            }

            // ========================================================
            // 7. COMPARAR HARDWARE
            // ========================================================

            if (!string.Equals(
                    licencia.HardwareFingerprint,
                    hardwareActual,
                    StringComparison.OrdinalIgnoreCase))
            {
                return new LicenseValidationResult
                {
                    Valida = false,
                    Estado = LicenseStatus.Invalida,
                    SoloConsulta = false,
                    Mensaje =
                        "La licencia pertenece a otro equipo.\r\n\r\n" +
                        "El sistema no fue bloqueado ni se eliminó " +
                        "ningún dato.\r\n\r\n" +
                        "Debe solicitar una nueva licencia para este equipo."
                };
            }

            // ========================================================
            // 8. FECHA DE INICIO
            // ========================================================

            if (licencia.FechaInicio.Date >
                DateTime.Today)
            {
                return new LicenseValidationResult
                {
                    Valida = false,
                    Estado = LicenseStatus.Invalida,
                    SoloConsulta = false,
                    Mensaje =
                        "La licencia aún no es válida."
                };
            }

            // ========================================================
            // 9. FECHA DE VENCIMIENTO
            // ========================================================

            if (licencia.FechaVencimiento.HasValue &&
                licencia.FechaVencimiento.Value.Date <
                DateTime.Today)
            {
                return new LicenseValidationResult
                {
                    Valida = false,
                    Estado = LicenseStatus.Vencida,
                    SoloConsulta = true,
                    Mensaje =
                        "La licencia ha vencido. " +
                        "El sistema funcionará en modo consulta."
                };
            }

            // ========================================================
            // 10. LICENCIA VALIDA
            // ========================================================

            return new LicenseValidationResult
            {
                Valida = true,
                Estado = LicenseStatus.Valida,
                SoloConsulta = false,
                Mensaje = "Licencia válida."
            };
        }

        // ============================================================
        // DATOS CANONICOS
        // ============================================================

        private static string CrearDatosCanonicos(
            LicenseInfo licencia)
        {
            var datos = new
            {
                Cliente =
                    licencia.Cliente ?? string.Empty,

                Empresa =
                    licencia.Empresa ?? string.Empty,

                InstallationId =
                    licencia.InstallationId.ToString("D"),

                HardwareFingerprint =
                    licencia.HardwareFingerprint
                        ?.Trim()
                        .ToUpperInvariant()
                        ?? string.Empty,

                Tipo =
                    licencia.Tipo.ToString(),

                FechaInicio =
                    licencia.FechaInicio.Date.ToString(
                        "yyyy-MM-dd",
                        CultureInfo.InvariantCulture),

                FechaVencimiento =
                    licencia.FechaVencimiento.HasValue
                        ? licencia.FechaVencimiento.Value.Date.ToString(
                            "yyyy-MM-dd",
                            CultureInfo.InvariantCulture)
                        : null
            };

            return JsonSerializer.Serialize(
                datos,
                new JsonSerializerOptions
                {
                    WriteIndented = false
                });
        }
    }
}