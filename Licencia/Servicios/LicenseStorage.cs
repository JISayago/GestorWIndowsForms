using Licencia.Constantes;
using Licencia.Modelos;
using Stockeate.Licensing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Licencia.Servicios
{
    public class LicenseStorage
    {
        private static JsonSerializerOptions CrearOpcionesJson()
        {
            return new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };
        }

        public LicenseInfo Leer()
        {
            string json =
                File.ReadAllText(
                    LicensePaths.License);

            return JsonSerializer.Deserialize<LicenseInfo>(
                json,
                CrearOpcionesJson())!;
        }

        public void Guardar(LicenseInfo licencia)
        {
            Directory.CreateDirectory(
                LicensePaths.ProgramData);

            string json =
                JsonSerializer.Serialize(
                    licencia,
                    CrearOpcionesJson());

            File.WriteAllText(
                LicensePaths.License,
                json);
        }

        public bool Existe()
        {
            return File.Exists(
                LicensePaths.License);
        }

        public void Eliminar()
        {
            if (Existe())
                File.Delete(
                    LicensePaths.License);
        }
    }
}