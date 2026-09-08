using System.Text.Json;
using Licencia.Criptografia;
using Licencia.Modelos;

namespace Licencia.Servicios
{
    public class LicenseValidator
    {
        private readonly LicenseStorage _storage = new();
        private readonly InstallationManager _installation = new();

        public LicenseValidationResult Validar(string publicKeyPem)
        {
            if (!_storage.Existe())
            {
                return new LicenseValidationResult
                {
                    Valida = false,
                    Mensaje = "No existe una licencia."
                };
            }

            LicenseInfo licencia = _storage.Leer();

            InstallationInfo instalacion = _installation.Obtener();

            if (licencia.InstallationId != instalacion.InstallationId)
            {
                return new LicenseValidationResult
                {
                    Valida = false,
                    Mensaje = "La licencia pertenece a otra instalación."
                };
            }

            if (licencia.FechaInicio > DateTime.Today)
            {
                return new LicenseValidationResult
                {
                    Valida = false,
                    Mensaje = "La licencia aún no es válida."
                };
            }

            if (licencia.FechaVencimiento.HasValue &&
                licencia.FechaVencimiento.Value.Date < DateTime.Today)
            {
                return new LicenseValidationResult
                {
                    Valida = false,
                    Mensaje = "La licencia ha vencido."
                };
            }

            string firma = licencia.Firma;

            licencia.Firma = "";

            string datos = JsonSerializer.Serialize(licencia);

            bool ok = RsaVerifier.VerificarFirma(
                datos,
                firma,
                publicKeyPem);

            if (!ok)
            {
                return new LicenseValidationResult
                {
                    Valida = false,
                    Mensaje = "Firma digital inválida."
                };
            }

            licencia.Firma = firma;

            return new LicenseValidationResult
            {
                Valida = true,
                Mensaje = "Licencia válida."
            };
        }
    }
}