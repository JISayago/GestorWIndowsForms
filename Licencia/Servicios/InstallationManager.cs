using System.Text.Json;
using Licencia.Constantes;
using Licencia.Modelos;

namespace Licencia.Servicios
{
    public class InstallationManager
    {
        public InstallationInfo Obtener()
        {
            if (!File.Exists(LicensePaths.Installation))
                return Crear();

            string json = File.ReadAllText(LicensePaths.Installation);

            return JsonSerializer.Deserialize<InstallationInfo>(json)!;
        }

        public InstallationInfo Crear()
        {
            var info = new InstallationInfo
            {
                InstallationId = Guid.NewGuid(),
                FechaInstalacion = DateTime.Now,
                VersionInstalada = "1.0.0",
                NombreEquipo = Environment.MachineName
            };

            Guardar(info);

            return info;
        }

        public void Guardar(InstallationInfo info)
        {
            Directory.CreateDirectory(LicensePaths.ProgramData);

            string json = JsonSerializer.Serialize(info,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(LicensePaths.Installation, json);
        }
    }
}