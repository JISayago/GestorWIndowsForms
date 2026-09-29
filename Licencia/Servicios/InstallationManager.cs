using System.Text.Json;
using Licencia.Constantes;
using Licencia.Modelos;
using Servicios.AccesoSistema.Seguridad;

namespace Licencia.Servicios
{
    public class InstallationManager
    {
        public InstallationInfo Obtener()
        {
            if (!File.Exists(LicensePaths.Installation))
                return Crear();

            string json =
                File.ReadAllText(
                    LicensePaths.Installation);

            InstallationInfo? info =
                JsonSerializer.Deserialize<InstallationInfo>(
                    json);

            if (info == null)
            {
                throw new InvalidOperationException(
                    "El archivo de instalación no contiene información válida.");
            }

            if (info.InstallationId == Guid.Empty)
            {
                throw new InvalidOperationException(
                    "El InstallationId no es válido.");
            }

            // ========================================================
            // MIGRACIÓN DE INSTALACIONES ANTIGUAS
            // ========================================================
            //
            // Si estamos trabajando con una instalación de desarrollo
            // que todavía no posee HardwareFingerprint, lo generamos
            // UNA SOLA VEZ y lo guardamos.
            //
            // IMPORTANTE:
            // Si ya existe un fingerprint, NO se reemplaza.
            // ========================================================

            if (string.IsNullOrWhiteSpace(
                    info.HardwareFingerprint))
            {
                info.HardwareFingerprint =
                    HardwareFingerprint.Obtener();
                Guardar(info);
            }

            return info;
        }

        public InstallationInfo Crear()
        {
            string hardwareFingerprint =
                HardwareFingerprint.Obtener();

            var info = new InstallationInfo
            {
                InstallationId =
                    Guid.NewGuid(),

                FechaInstalacion =
                    DateTime.Now,

                VersionInstalada =
                    "1.0.0",

                NombreEquipo =
                    Environment.MachineName,

                HardwareFingerprint =
                    hardwareFingerprint
            };

            Guardar(info);

            return info;
        }

        public void Guardar(InstallationInfo info)
        {
            Directory.CreateDirectory(
                LicensePaths.ProgramData);

            string json =
                JsonSerializer.Serialize(
                    info,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

            File.WriteAllText(
                LicensePaths.Installation,
                json);
        }
    }
}