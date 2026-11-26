using Servicios.Helpers.Sistema;
using Servicios.LogicaNegocio.Configuracion;
using Servicios.LogicaNegocio.Configuracion.DTO;

namespace Presentacion.Core.Configuracion;

public sealed class SeccionNegocio : SeccionBase
{
    private const long MaxBytesLogo = 2 * 1024 * 1024;

    private readonly TextBox txtNombre = new() { MaxLength = 80, Location = new Point(0, 20), Size = new Size(430, 25) };
    private readonly TextBox txtSubtitulo = new() { MaxLength = 250, Multiline = true, Location = new Point(0, 80), Size = new Size(430, 62) };
    private readonly TextBox txtPie = new() { MaxLength = 150, Location = new Point(0, 178), Size = new Size(430, 25) };
    private readonly PictureBox pbxLogo = new() { BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.Zoom, Location = new Point(470, 20), Size = new Size(220, 145) };
    private readonly Button btnSeleccionar = new() { Text = "Seleccionar", Location = new Point(470, 178), Size = new Size(108, 28), UseVisualStyleBackColor = true };
    private readonly Button btnQuitar = new() { Text = "Quitar", Location = new Point(584, 178), Size = new Size(106, 28), UseVisualStyleBackColor = true };
    private readonly Label lblLogoInfo = new() { Location = new Point(470, 214), Size = new Size(220, 32), Font = new Font("Segoe UI", 8.25F) };

    private readonly IConfiguracionServicio _servicio = new ConfiguracionServicio();

    private byte[]? _logo;
    private bool _logoModificado;

    public override SeccionConfiguracion Clave => SeccionConfiguracion.Negocio;
    public override string Descripcion => "Datos que aparecen en los comprobantes PDF.";

    public SeccionNegocio()
    {
        btnSeleccionar.Click += SeleccionarLogo;
        btnQuitar.Click += QuitarLogo;
        Controls.AddRange(new Control[]
        {
            Etiqueta("Nombre del negocio", 0, 0), txtNombre,
            Etiqueta("Subtítulo (dirección, teléfono, CUIT)", 0, 60), txtSubtitulo,
            Etiqueta("Texto de pie", 0, 158), txtPie,
            Etiqueta("Logo", 470, 0), pbxLogo, btnSeleccionar, btnQuitar, lblLogoInfo
        });
    }

    public override void Cargar()
    {
        var d = _servicio.Obtener<DatosNegocioDTO>(ClaveConfiguracion.DatosNegocio);
        txtNombre.Text = d.NombreNegocio;
        txtSubtitulo.Text = d.Subtitulo;
        txtPie.Text = d.TextoPie;

        _logo = _servicio.ObtenerLogo();
        _logoModificado = false;
        MostrarLogo(_logo);
        lblLogoInfo.Text = _logo == null ? "Sin logo" : "Logo guardado";
    }

    public override string? Validar() =>
        string.IsNullOrWhiteSpace(txtNombre.Text) ? "El nombre del negocio es obligatorio." : null;

    public override void Guardar(LoteConfiguracion lote)
    {
        lote.Texto(ClaveConfiguracion.DatosNegocio, new DatosNegocioDTO
        {
            NombreNegocio = txtNombre.Text.Trim(),
            Subtitulo = txtSubtitulo.Text.Trim(),
            TextoPie = txtPie.Text.Trim()
        });

        if (_logoModificado)
            lote.Binario(ClaveConfiguracion.LogoNegocio, _logo);
    }

    public override void DespuesDeGuardar()
    {
        _logoModificado = false;
        lblLogoInfo.Text = _logo == null ? "Sin logo" : "Logo guardado";
    }

    private void SeleccionarLogo(object? sender, EventArgs e)
    {
        using var dlg = new OpenFileDialog
        {
            Title = "Seleccionar logo",
            Filter = "Imágenes|*.png;*.jpg;*.jpeg;*.bmp|Todos los archivos|*.*"
        };
        if (dlg.ShowDialog(FindForm()) != DialogResult.OK) return;

        try
        {
            var bytes = File.ReadAllBytes(dlg.FileName);
            if (bytes.Length > MaxBytesLogo)
            {
                MessageBox.Show("El logo no puede superar los 2 MB.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MostrarLogo(bytes); // lanza excepción si no es una imagen válida, antes de tocar el estado
            _logo = bytes;
            _logoModificado = true;
            MarcarModificada();
            lblLogoInfo.Text = Path.GetFileName(dlg.FileName) + " (pendiente de guardar)";
        }
        catch (Exception ex)
        {
            MessageBox.Show("No se pudo leer el logo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void QuitarLogo(object? sender, EventArgs e)
    {
        _logo = null;
        _logoModificado = true;
        MarcarModificada();
        MostrarLogo(null);
        lblLogoInfo.Text = "Sin logo (pendiente de guardar)";
    }

    private void MostrarLogo(byte[]? bytes)
    {
        Image? nueva = null;
        if (bytes != null)
        {
            using var ms = new MemoryStream(bytes);
            using var origen = Image.FromStream(ms);
            nueva = new Bitmap(origen); // copia: no depende del stream
        }
        var vieja = pbxLogo.Image;
        pbxLogo.Image = nueva;
        vieja?.Dispose();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) pbxLogo.Image?.Dispose();
        base.Dispose(disposing);
    }
}