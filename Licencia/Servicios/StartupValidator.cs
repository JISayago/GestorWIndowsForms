
using Licencia.Constantes;
using Licencia.Modelos;
using Stockeate.Licensing;

namespace Licencia.Servicios
{
    public static class StartupValidator
    {
        public static LicenseValidationResult ValidarInicio()
        {
            var validator = new LicenseValidator();

            return validator.Validar(PublicKey.Pem);
        }
    }
}

