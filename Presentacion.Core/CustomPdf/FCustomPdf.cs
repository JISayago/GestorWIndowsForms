using AccesoDatos.Config;
using AccesoDatos.Storage;
using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;

namespace Presentacion.Core.CustomPdf
{
    public partial class FCustomPdf : FBaseABM
    {
        private string _rutaLogo = "";

        public FCustomPdf() : base(TipoOperacion.Modificar, 0)
        {
            InitializeComponent();
            btnLimpiar.Visible = false;
            AgregarControlesObligatorios(txtNombreNegocio, "Nombre del negocio");
            FormClosed += (_, _) => LiberarImagen();
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
            MostrarLogo(cfg.RutaLogo);
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
            cfg.RutaLogo = _rutaLogo;

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
                var rutaCopia = CopiarLogo(dialog.FileName);
                MostrarLogo(rutaCopia);
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
            _rutaLogo = "";
            lblRutaLogo.Text = "Sin logo";
        }

        private string CopiarLogo(string rutaOrigen)
        {
            LiberarImagen();

            var destinoDir = StorageManager.ObtenerRutaIdentidadComprobantes();

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

            var destino = Path.Combine(destinoDir, "logo" + Path.GetExtension(rutaOrigen));
            File.Copy(rutaOrigen, destino, true);
            return destino;
        }

        private void MostrarLogo(string? ruta)
        {
            LiberarImagen();

            if (string.IsNullOrWhiteSpace(ruta) || !File.Exists(ruta))
            {
                _rutaLogo = "";
                lblRutaLogo.Text = "Sin logo";
                return;
            }

            pbxLogo.Image = CargarImagenSinBloquear(ruta);
            _rutaLogo = ruta;
            lblRutaLogo.Text = ruta;
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
