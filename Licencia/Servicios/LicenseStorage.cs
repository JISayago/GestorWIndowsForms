using System.Text.Json;
using Licencia.Constantes;
using Licencia.Modelos;

namespace Licencia.Servicios
{
    public class LicenseStorage
    {
        public LicenseInfo Leer()
        {
            string json = File.ReadAllText(LicensePaths.License);

            return JsonSerializer.Deserialize<LicenseInfo>(json)!;
        }

        public void Guardar(LicenseInfo licencia)
        {
            Directory.CreateDirectory(LicensePaths.ProgramData);

            string json = JsonSerializer.Serialize(
                licencia,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(LicensePaths.License, json);
        }

        public bool Existe()
        {
            return File.Exists(LicensePaths.License);
        }

        public void Eliminar()
        {
            if (Existe())
                File.Delete(LicensePaths.License);
        }
    }
}