using Licencia.Criptografia;
using Licencia.Modelos;
using Licencia.Servicios;
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
            // 4. INSTALLATION ID
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
            // 5. FECHA DE INICIO
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
            // 6. FECHA DE VENCIMIENTO
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
            // 7. FIRMA
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
            // 8. LICENCIA VALIDA
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