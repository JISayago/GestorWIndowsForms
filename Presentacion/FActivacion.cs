using Licencia.Constantes;
using Licencia.Modelos;
using Licencia.Servicios;
using Presentacion.FBase;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Stockeate
{
    public partial class FActivacion : FBase
    {
        private readonly bool _primeraEjecucion;

        private InstallationInfo? _instalacion;

        public bool LicenciaValida { get; private set; }

        public FActivacion(bool primeraEjecucion)
        {
            InitializeComponent();

            _primeraEjecucion =
                primeraEjecucion;

            ConfigurarFormulario();

            CargarInformacionInstalacion();
        }

        // ============================================================
        // CONFIGURACION INICIAL
        // ============================================================

        private void ConfigurarFormulario()
        {
            StartPosition =
                FormStartPosition.CenterScreen;

            // ========================================================
            // PRIMERA EJECUCION
            // ========================================================

            if (_primeraEjecucion)
            {
                Text =
                    "Stockeate - Primera activación";

                lblTitulo.Text =
                    "Activación de Stockeate";

                lblDescripcion.Text =
                    "Es el primer inicio de esta instalación.\r\n\r\n" +
                    "Stockeate generó una identificación para esta " +
                    "instalación y una identificación de hardware del equipo.\r\n\r\n" +
                    "Para solicitar la licencia, guardá el archivo de " +
                    "activación y envialo al proveedor de Stockeate.";

                btnGuardarArchivo.Visible =
                    true;

                btnCopiarId.Visible =
                    true;

                btnCargarLicencia.Visible =
                    false;

                btnCopiarId.Text =
                    "Copiar datos";

                lblAyuda.Text =
                    "Podés guardar el archivo de activación " +
                    "o copiar los datos mostrados.";

                // En la primera ejecución solamente necesitamos
                // generar y enviar la información de instalación.
            }

            // ========================================================
            // EJECUCIONES POSTERIORES
            // ========================================================

            else
            {
                Text =
                    "Stockeate - Activación";

                lblTitulo.Text =
                    "Activar Stockeate";

                lblDescripcion.Text =
                    "Esta instalación ya está registrada.\r\n\r\n" +
                    "Seleccioná el archivo de licencia que recibiste " +
                    "del proveedor de Stockeate.\r\n\r\n" +
                    "Stockeate verificará automáticamente la instalación, " +
                    "el hardware y la firma digital de la licencia.";

                btnGuardarArchivo.Visible =
                    false;

                btnCopiarId.Visible =
                    true;

                btnCargarLicencia.Visible =
                    true;

                btnCopiarId.Text =
                    "Copiar datos";

                lblAyuda.Text =
                    "Seleccioná el archivo license.json que recibiste.";
            }

            lblEstado.Text =
                "";
        }

        // ============================================================
        // OBTENER INSTALLATION.JSON
        // ============================================================

        private void CargarInformacionInstalacion()
        {
            try
            {
                var manager =
                    new InstallationManager();

                // Si no existe installation.json,
                // Obtener() lo crea automáticamente.

                _instalacion =
                    manager.Obtener();

                txtInstallationId.Text =
                    _instalacion.InstallationId
                        .ToString("D");

                txtHardwareFingerprint.Text =
                    _instalacion.HardwareFingerprint;

                // Ambos datos son informativos.
                // El usuario no debe modificarlos.

                txtInstallationId.ReadOnly =
                    true;

                txtHardwareFingerprint.ReadOnly =
                    true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo obtener la información de esta instalación.\r\n\r\n" +
                    ex.Message,
                    "Stockeate - Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                BeginInvoke(
                    new Action(
                        CerrarCancelado));
            }
        }

        // ============================================================
        // COPIAR DATOS DE INSTALACION
        // ============================================================

        private void btnCopiarId_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (_instalacion == null)
                    return;

                string datos =
                    "InstallationId: " +
                    _instalacion.InstallationId.ToString("D") +
                    Environment.NewLine +
                    Environment.NewLine +
                    "HardwareFingerprint: " +
                    _instalacion.HardwareFingerprint;

                Clipboard.SetText(
                    datos);

                lblEstado.Text =
                    "Datos de instalación copiados al portapapeles.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron copiar los datos de instalación.\r\n\r\n" +
                    ex.Message,
                    "Stockeate",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // ============================================================
        // GUARDAR COPIA DEL INSTALLATION.JSON
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

            dialogo.DefaultExt =
                "json";

            dialogo.AddExtension =
                true;

            dialogo.FileName =
                $"Stockeate_Installation_{_instalacion.InstallationId:N}.json";

            // ========================================================
            // DESTINO PREDETERMINADO
            // ESCRITORIO
            // SI NO EXISTE, DESCARGAS
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

            // ========================================================
            // CANCELAR
            // ========================================================

            if (dialogo.ShowDialog(this) !=
                DialogResult.OK)
            {
                return;
            }

            // ========================================================
            // COPIAR INSTALLATION.JSON
            // ========================================================

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
                    "Ahora podés enviarlo al proveedor de Stockeate " +
                    "para solicitar la licencia.",
                    "Stockeate - Activación",
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

            try
            {
                Directory.CreateDirectory(
                    LicensePaths.ProgramData);

                // ====================================================
                // GUARDAR COPIA DE LA LICENCIA ACTUAL
                // ====================================================

                byte[]? licenciaAnterior = null;

                if (File.Exists(
                        LicensePaths.License))
                {
                    licenciaAnterior =
                        File.ReadAllBytes(
                            LicensePaths.License);
                }

                // ====================================================
                // COPIAR NUEVA LICENCIA
                // ====================================================

                File.Copy(
                    dialogo.FileName,
                    LicensePaths.License,
                    true);

                // ====================================================
                // VALIDAR INMEDIATAMENTE
                // ====================================================

                var resultado =
                    StartupValidator.ValidarInicio();

                // ====================================================
                // LICENCIA CORRECTA
                // ====================================================

                if (resultado.Valida)
                {
                    LicenciaValida =
                        true;

                    lblEstado.Text =
                        "Licencia válida.";

                    MessageBox.Show(
                        "La licencia fue instalada y validada correctamente.",
                        "Stockeate - Activación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    DialogResult =
                        DialogResult.OK;

                    Close();

                    return;
                }

                // ====================================================
                // LICENCIA INVÁLIDA
                // ====================================================
                //
                // Restauramos la licencia anterior.
                //
                // Esto es especialmente importante ahora que la
                // licencia también valida el hardware.
                // ====================================================

                if (licenciaAnterior != null)
                {
                    File.WriteAllBytes(
                        LicensePaths.License,
                        licenciaAnterior);
                }
                else
                {
                    if (File.Exists(
                            LicensePaths.License))
                    {
                        File.Delete(
                            LicensePaths.License);
                    }
                }

                lblEstado.Text =
                    resultado.Mensaje;

                MessageBox.Show(
                    resultado.Mensaje,
                    "Stockeate - Licencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo instalar la licencia.\r\n\r\n" +
                    ex.Message,
                    "Stockeate - Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // CERRAR
        // ============================================================

        private void btnCerrar_Click(
            object sender,
            EventArgs e)
        {
            CerrarCancelado();
        }

        private void CerrarCancelado()
        {
            LicenciaValida =
                false;

            DialogResult =
                DialogResult.Cancel;

            Close();
        }
    }
}