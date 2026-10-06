using Licencia.Constantes;
using Licencia.Modelos;
using Licencia.Servicios;
using Stockeate.Licensing;

namespace Presentacion
{
    public partial class FActualizacionLicencia : Form
    {
        private readonly LicenseInfo _licenciaTrial;

        public bool LicenciaActualizada { get; private set; }

        public FActualizacionLicencia(LicenseInfo licenciaTrial)
        {
            InitializeComponent();

            _licenciaTrial = licenciaTrial;

            MostrarInformacionTrial();
        }

        private void MostrarInformacionTrial()
        {
            lblFechaVencimiento.Text =
                _licenciaTrial.FechaVencimiento.HasValue
                    ? _licenciaTrial.FechaVencimiento.Value.ToString("dd/MM/yyyy")
                    : "Sin fecha de vencimiento";

            if (!_licenciaTrial.FechaVencimiento.HasValue)
            {
                lblTiempoRestante.Text =
                    "No se pudo determinar el tiempo restante.";
                return;
            }

            int diasRestantes =
                (_licenciaTrial.FechaVencimiento.Value.Date -
                 DateTime.Today).Days;

            if (diasRestantes > 0)
            {
                lblTiempoRestante.Text =
                    diasRestantes == 1
                        ? "Queda 1 día de prueba."
                        : $"Quedan {diasRestantes} días de prueba.";
            }
            else if (diasRestantes == 0)
            {
                lblTiempoRestante.Text =
                    "La prueba vence hoy.";
            }
            else
            {
                lblTiempoRestante.Text =
                    "La prueba ha vencido.";
            }
        }

        private void btnCargarLicencia_Click(object sender, EventArgs e)
        {
            using var dialogo = new OpenFileDialog();

            dialogo.Title =
                "Seleccionar licencia permanente";

            dialogo.Filter =
                "Licencia de Stockeate (*.json)|*.json|" +
                "Archivo JSON (*.json)|*.json|" +
                "Todos los archivos (*.*)|*.*";

            dialogo.FileName = "license.json";

            dialogo.CheckFileExists = true;

            dialogo.Multiselect = false;

            if (dialogo.ShowDialog(this) != DialogResult.OK)
                return;

            InstalarLicencia(dialogo.FileName);
        }

        private void InstalarLicencia(string rutaNuevaLicencia)
        {
            byte[] licenciaAnterior;

            try
            {
                if (!File.Exists(LicensePaths.License))
                {
                    MessageBox.Show(
                        "No existe la licencia actual de prueba.",
                        "Stockeate",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                licenciaAnterior =
                    File.ReadAllBytes(LicensePaths.License);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo leer la licencia actual.\r\n\r\n" +
                    ex.Message,
                    "Stockeate",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            try
            {
                // ====================================================
                // REEMPLAZAR LICENCIA ACTUAL
                // ====================================================

                File.Copy(
                    rutaNuevaLicencia,
                    LicensePaths.License,
                    true);


                // ====================================================
                // VALIDAR LA NUEVA LICENCIA
                // ====================================================

                var resultado =
                    StartupValidator.ValidarInicio();


                // ====================================================
                // SI NO ES VÁLIDA
                // ====================================================

                if (!resultado.Valida)
                {
                    RestaurarLicenciaAnterior(licenciaAnterior);

                    MessageBox.Show(
                        resultado.Mensaje,
                        "Licencia no válida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }


                // ====================================================
                // LEER LICENCIA YA VALIDADA
                // ====================================================

                var licenciaNueva =
                    new LicenseStorage().Leer();


                // ====================================================
                // COMPROBAR QUE SEA PERPETUA
                // ====================================================

                if (licenciaNueva.Tipo != LicenseType.Perpetua)
                {
                    RestaurarLicenciaAnterior(licenciaAnterior);

                    MessageBox.Show(
                        "El archivo seleccionado no corresponde a una " +
                        "licencia permanente.\r\n\r\n" +
                        "Debe seleccionar una licencia de tipo Perpetua.",
                        "Licencia incorrecta",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }


                // ====================================================
                // TODO CORRECTO
                // ====================================================

                LicenciaActualizada = true;

                MessageBox.Show(
                    "La licencia permanente fue instalada y validada " +
                    "correctamente.",
                    "Stockeate",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;

                Close();
            }
            catch (Exception ex)
            {
                try
                {
                    RestaurarLicenciaAnterior(licenciaAnterior);
                }
                catch
                {
                    // No ocultamos el error original.
                }

                MessageBox.Show(
                    "No se pudo instalar la nueva licencia.\r\n\r\n" +
                    ex.Message,
                    "Stockeate",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void RestaurarLicenciaAnterior(byte[] licenciaAnterior)
        {
            File.WriteAllBytes(
                LicensePaths.License,
                licenciaAnterior);
        }

        private void btnContinuarPrueba_Click(object sender, EventArgs e)
        {
            LicenciaActualizada = false;

            DialogResult = DialogResult.Cancel;

            Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            LicenciaActualizada = false;

            DialogResult = DialogResult.Cancel;

            Close();
        }
    }
}