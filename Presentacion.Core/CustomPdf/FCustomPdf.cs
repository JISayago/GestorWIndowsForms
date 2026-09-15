using AccesoDatos.Config;
using AccesoDatos.Storage;
using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;

namespace Presentacion.Core.CustomPdf
{
    public partial class FCustomPdf : FBaseABM
    {
        // Ruta del logo que ya está guardado en la configuración al abrir el formulario.
        private string _rutaLogoOriginal = "";

        // Copia TEMPORAL del logo recién elegido por el usuario, todavía no confirmado.
        private string? _rutaLogoTemporal;

        // true si el usuario apretó "Quitar" y todavía no guardó los cambios.
        private bool _logoMarcadoParaEliminar;

        public FCustomPdf() : base(TipoOperacion.Modificar, 0)
        {
            InitializeComponent();
            btnLimpiar.Visible = false;
            AgregarControlesObligatorios(txtNombreNegocio, "Nombre del negocio");
            FormClosed += (_, _) =>
            {
                LiberarImagen();
                EliminarLogoTemporal();
            };
        }

        public override void FBaseABM_Load(object sender, EventArgs e)
        {
            base.FBaseABM_Load(sender, e);
        }

        public override void CargarDatos(long? entidadId)
        {
            var cfg = ConfigManager.Config.Comprobantes;

            txtNombreNegocio.Text = cfg.NombreNegocio ?? "";
            txtSubtitulo.Text = cfg.Subtitulo ?? "";
            txtTextoPie.Text = cfg.TextoPie ?? "";

            _rutaLogoOriginal = cfg.RutaLogo ?? "";
            _rutaLogoTemporal = null;
            _logoMarcadoParaEliminar = false;

            MostrarImagenPrevia(_rutaLogoOriginal);
            lblRutaLogo.Text = string.IsNullOrWhiteSpace(_rutaLogoOriginal) ? "Sin logo" : _rutaLogoOriginal;
        }

        public override void EjecutarComando()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(
                    "El nombre del negocio es obligatorio.",
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var cfg = ConfigManager.Config.Comprobantes;
            cfg.NombreNegocio = txtNombreNegocio.Text.Trim();
            cfg.Subtitulo = txtSubtitulo.Text.Trim();
            cfg.TextoPie = txtTextoPie.Text.Trim();

            try
            {
                // Recién ACÁ, al confirmar, se toca el archivo final de identidad de comprobantes.
                cfg.RutaLogo = ConfirmarLogo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo guardar el logo: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            ConfigManager.Guardar();
            RealizoAlgunaOperacion = true;

            MessageBox.Show(
                "Los datos del comprobante se guardaron correctamente.",
                "Atención",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnSeleccionarLogo_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Seleccionar logo",
                Filter = "Imágenes|*.png;*.jpg;*.jpeg;*.bmp|Todos los archivos|*.*"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                // Copia a una ruta TEMPORAL solo para previsualizar.
                // El archivo final (y el logo.* anterior) no se tocan hasta confirmar.
                var rutaTemp = CopiarLogoATemporal(dialog.FileName);

                EliminarLogoTemporal();
                _rutaLogoTemporal = rutaTemp;
                _logoMarcadoParaEliminar = false;

                MostrarImagenPrevia(rutaTemp);
                lblRutaLogo.Text = dialog.FileName + "  (pendiente de guardar)";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo copiar el logo: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnQuitarLogo_Click(object sender, EventArgs e)
        {
            LiberarImagen();
            EliminarLogoTemporal();
            _rutaLogoTemporal = null;
            _logoMarcadoParaEliminar = true;
            lblRutaLogo.Text = "Sin logo (pendiente de guardar)";
        }

        /// <summary>
        /// Aplica al almacenamiento definitivo lo que el usuario decidió sobre el logo
        /// (nuevo logo elegido, logo quitado, o sin cambios) y devuelve la ruta que
        /// debe quedar guardada en la configuración. Solo se llama al confirmar (Guardar).
        /// </summary>
        private string ConfirmarLogo()
        {
            if (_logoMarcadoParaEliminar)
            {
                BorrarLogosExistentes(StorageManager.ObtenerRutaIdentidadComprobantes());
                return "";
            }

            if (_rutaLogoTemporal != null)
            {
                var destinoDir = StorageManager.ObtenerRutaIdentidadComprobantes();
                BorrarLogosExistentes(destinoDir);

                var destino = Path.Combine(destinoDir, "logo" + Path.GetExtension(_rutaLogoTemporal));
                File.Copy(_rutaLogoTemporal, destino, true);

                EliminarLogoTemporal();
                _rutaLogoTemporal = null;

                return destino;
            }

            // No se tocó el logo: se conserva el que ya estaba guardado antes de abrir el formulario.
            return _rutaLogoOriginal;
        }

        private static void BorrarLogosExistentes(string destinoDir)
        {
            foreach (var existente in Directory.GetFiles(destinoDir, "logo.*"))
            {
                try
                {
                    File.Delete(existente);
                }
                catch
                {
                    // Si el archivo anterior quedó bloqueado, se pisa con otro nombre de extensión.
                }
            }
        }

        private static string CopiarLogoATemporal(string rutaOrigen)
        {
            var destino = Path.Combine(
                Path.GetTempPath(),
                "Stockeate_Logo_" + Guid.NewGuid().ToString("N") + Path.GetExtension(rutaOrigen));

            File.Copy(rutaOrigen, destino, true);
            return destino;
        }

        private void EliminarLogoTemporal()
        {
            if (_rutaLogoTemporal == null)
                return;

            try
            {
                File.Delete(_rutaLogoTemporal);
            }
            catch
            {
                // Best effort: si no se puede borrar el temporal, lo termina limpiando el SO.
            }
        }

        private void MostrarImagenPrevia(string? ruta)
        {
            LiberarImagen();

            if (string.IsNullOrWhiteSpace(ruta) || !File.Exists(ruta))
                return;

            pbxLogo.Image = CargarImagenSinBloquear(ruta);
        }

        private static Image CargarImagenSinBloquear(string ruta)
        {
            using var original = Image.FromFile(ruta);
            return new Bitmap(original);
        }

        private void LiberarImagen()
        {
            if (pbxLogo.Image == null)
                return;

            var imagen = pbxLogo.Image;
            pbxLogo.Image = null;
            imagen.Dispose();
        }
    }
}