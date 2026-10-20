using Licencia.Constantes;
using Licencia.Modelos;
using Licencia.Servicios;
using Presentacion.FBase;
using Servicios.AccesoSistema.Seguridad;

namespace Stockeate
{
    public partial class FActivacion : FBase
    {
        private readonly bool _primeraEjecucion;
        private readonly bool _esRenovacionTrial;

        private InstallationInfo? _instalacion;

        public bool LicenciaValida { get; private set; }


        public FActivacion(
            bool primeraEjecucion,
            bool esRenovacionTrial = false)
        {
            InitializeComponent();

            _primeraEjecucion = primeraEjecucion;
            _esRenovacionTrial = esRenovacionTrial;

            ConfigurarFormulario();
            CargarInstalacion();
        }


        // ============================================================
        // CONFIGURACIÓN
        // ============================================================

        private void ConfigurarFormulario()
        {
            StartPosition =
                FormStartPosition.CenterScreen;


            // ========================================================
            // PRIMERA EJECUCIÓN
            // ========================================================

            if (_primeraEjecucion)
            {
                Text =
                    "Stockeate - Primera activación";

                lblTitulo.Text =
                    "Activación de Stockeate";

                lblDescripcion.Text =
                    "Es el primer inicio de esta instalación.\r\n\r\n" +
                    "Stockeate generó una identificación única para " +
                    "esta computadora.\r\n\r\n" +
                    "Podés copiar el Installation ID o guardar el " +
                    "archivo de activación completo para enviarlo " +
                    "al proveedor.";

                btnGuardarArchivo.Visible = true;
                btnCargarLicencia.Visible = true;

                lblAyuda.Text =
                    "El archivo se guardará inicialmente en el Escritorio.";
            }


            // ========================================================
            // RENOVACIÓN DE TRIAL
            // ========================================================

            else if (_esRenovacionTrial)
            {
                Text =
                    "Stockeate - Activación";

                lblTitulo.Text =
                    "Activar licencia permanente";

                lblDescripcion.Text =
                    "Esta instalación ya está registrada.\r\n\r\n" +
                    "Podés volver a compartir el Installation ID " +
                    "o el archivo de activación si necesitás solicitar " +
                    "una nueva licencia.\r\n\r\n" +
                    "Cuando recibas la licencia permanente, cargala " +
                    "desde esta misma pantalla.";

                btnGuardarArchivo.Visible = true;
                btnCargarLicencia.Visible = true;

                lblAyuda.Text =
                    "Podés guardar nuevamente el archivo de activación " +
                    "o cargar directamente la licencia recibida.";
            }


            // ========================================================
            // ACTIVACIÓN NORMAL SIN LICENCIA
            // ========================================================

            else
            {
                Text =
                    "Stockeate - Activación";

                lblTitulo.Text =
                    "Activar Stockeate";

                lblDescripcion.Text =
                    "Esta instalación ya está registrada, pero todavía " +
                    "no tiene una licencia válida.\r\n\r\n" +
                    "Seleccioná el archivo de licencia que recibiste " +
                    "del proveedor.";

                btnGuardarArchivo.Visible = true;
                btnCargarLicencia.Visible = true;

                lblAyuda.Text =
                    "Podés compartir el Installation ID o guardar " +
                    "una copia del archivo de activación.";
            }

            lblEstado.Text = "";
        }


        // ============================================================
        // CARGAR INSTALLATION.JSON
        // ============================================================
        private void CargarInstalacion()
        {
            try
            {
                var manager =
                    new InstallationManager();

                // Crea installation.json si todavía no existe.
                _instalacion =
                    manager.Obtener();


                // ========================================================
                // INSTALLATION ID
                // ========================================================

                txtInstallationId.Text =
                    _instalacion.InstallationId.ToString("D");


                // ========================================================
                // HARDWARE FINGERPRINT
                //
                // DEBE SER EL MISMO QUE USA LicenseValidator
                // ========================================================

                txtHardwareFingerprint.Text =
                    HardwareFingerprint.Obtener();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo obtener la información de instalación.\r\n\r\n" +
                    ex.Message,
                    "Stockeate - Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                DialogResult =
                    DialogResult.Cancel;

                Close();
            }
        }


        // ============================================================
        // COPIAR INSTALLATION ID
        // ============================================================
        private void btnCopiarId_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                string datos =
                    "Installation ID: " +
                    txtInstallationId.Text +
                    Environment.NewLine +
                    Environment.NewLine +
                    "Hardware Fingerprint: " +
                    txtHardwareFingerprint.Text;

                Clipboard.SetText(datos);

                lblEstado.Text =
                    "Installation ID y Hardware Fingerprint copiados.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron copiar los datos.\r\n\r\n" +
                    ex.Message,
                    "Stockeate",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // ============================================================
        // GUARDAR COPIA DE INSTALLATION.JSON
        // ============================================================

        private void btnGuardarArchivo_Click(
            object sender,
            EventArgs e)
        {
            if (_instalacion == null)
                return;

            using var dialogo =
                new SaveFileDialog();

            dialogo.Title =
                "Guardar archivo de activación";

            dialogo.Filter =
                "Archivo de activación de Stockeate (*.json)|*.json|" +
                "Archivo JSON (*.json)|*.json";

            dialogo.DefaultExt = "json";
            dialogo.AddExtension = true;

            dialogo.FileName =
                $"Stockeate_Installation_" +
                $"{_instalacion.InstallationId:N}.json";


            // ========================================================
            // ESCRITORIO
            // ========================================================

            string escritorio =
                Environment.GetFolderPath(
                    Environment.SpecialFolder.DesktopDirectory);

            string descargas =
                Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.UserProfile),
                    "Downloads");


            if (Directory.Exists(escritorio))
            {
                dialogo.InitialDirectory =
                    escritorio;
            }
            else if (Directory.Exists(descargas))
            {
                dialogo.InitialDirectory =
                    descargas;
            }


            if (dialogo.ShowDialog(this) !=
                DialogResult.OK)
            {
                return;
            }


            try
            {
                File.Copy(
                    LicensePaths.Installation,
                    dialogo.FileName,
                    true);

                lblEstado.Text =
                    "Archivo de activación guardado correctamente.";

                MessageBox.Show(
                    "El archivo de activación se guardó correctamente.\r\n\r\n" +
                    "Podés enviarlo al proveedor de Stockeate.",
                    "Stockeate",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo guardar el archivo de activación.\r\n\r\n" +
                    ex.Message,
                    "Stockeate - Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // CARGAR LICENSE.JSON
        // ============================================================

        private void btnCargarLicencia_Click(
            object sender,
            EventArgs e)
        {
            using var dialogo =
                new OpenFileDialog();

            dialogo.Title =
                "Seleccionar licencia de Stockeate";

            dialogo.Filter =
                "Licencia de Stockeate (*.json)|*.json|" +
                "Archivo JSON (*.json)|*.json|" +
                "Todos los archivos (*.*)|*.*";

            dialogo.FileName =
                "license.json";

            dialogo.CheckFileExists =
                true;

            dialogo.Multiselect =
                false;


            if (dialogo.ShowDialog(this) !=
                DialogResult.OK)
            {
                return;
            }


            InstalarLicencia(
                dialogo.FileName);
        }


        // ============================================================
        // INSTALAR LICENCIA
        // ============================================================

        private void InstalarLicencia(
            string rutaNuevaLicencia)
        {
            byte[]? licenciaAnterior = null;

            try
            {
                // ====================================================
                // GUARDAR LICENCIA ACTUAL
                // ====================================================

                if (File.Exists(LicensePaths.License))
                {
                    licenciaAnterior =
                        File.ReadAllBytes(
                            LicensePaths.License);
                }


                // ====================================================
                // COPIAR NUEVA LICENCIA
                // ====================================================

                Directory.CreateDirectory(
                    LicensePaths.ProgramData);

                File.Copy(
                    rutaNuevaLicencia,
                    LicensePaths.License,
                    true);


                // ====================================================
                // VALIDAR
                // ====================================================

                var resultado =
                    StartupValidator.ValidarInicio();


                if (!resultado.Valida)
                {
                    RestaurarLicenciaAnterior(
                        licenciaAnterior);

                    MessageBox.Show(
                        resultado.Mensaje,
                        "Licencia no válida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }


                // ====================================================
                // LEER LICENCIA NUEVA
                // ====================================================

                var nuevaLicencia =
                    new LicenseStorage().Leer();


                // ====================================================
                // SI VENIMOS DE TRIAL,
                // LA NUEVA TIENE QUE SER PERPETUA
                // ====================================================

                if (_esRenovacionTrial &&
                    nuevaLicencia.Tipo !=
                    LicenseType.Perpetua)
                {
                    RestaurarLicenciaAnterior(
                        licenciaAnterior);

                    MessageBox.Show(
                        "La licencia seleccionada no es una " +
                        "licencia permanente.\r\n\r\n" +
                        "Seleccioná una licencia de tipo Perpetua.",
                        "Licencia incorrecta",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }


                // ====================================================
                // ÉXITO
                // ====================================================

                LicenciaValida =
                    true;

                lblEstado.Text =
                    "Licencia válida.";

                MessageBox.Show(
                    "La licencia fue instalada y validada correctamente.",
                    "Stockeate",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult =
                    DialogResult.OK;

                Close();
            }
            catch (Exception ex)
            {
                try
                {
                    RestaurarLicenciaAnterior(
                        licenciaAnterior);
                }
                catch
                {
                }

                MessageBox.Show(
                    "No se pudo instalar la licencia.\r\n\r\n" +
                    ex.Message,
                    "Stockeate - Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // RESTAURAR LICENCIA ANTERIOR
        // ============================================================

        private void RestaurarLicenciaAnterior(
            byte[]? licenciaAnterior)
        {
            if (licenciaAnterior != null)
            {
                File.WriteAllBytes(
                    LicensePaths.License,
                    licenciaAnterior);

                return;
            }

            if (File.Exists(LicensePaths.License))
            {
                File.Delete(
                    LicensePaths.License);
            }
        }


        // ============================================================
        // CERRAR
        // ============================================================

        private void btnCerrar_Click(
            object sender,
            EventArgs e)
        {
            LicenciaValida =
                false;

            DialogResult =
                DialogResult.Cancel;

            Close();
        }
    }
}