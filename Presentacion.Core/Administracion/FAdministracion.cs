using Presentacion.Core.Articulo.Marca;
using Presentacion.Core.Categoria;
using Presentacion.Core.Cliente;
using Presentacion.Core.CustomPdf;
using Presentacion.Core.Empleado;
using Presentacion.Core.Empleado.Rol;
using Presentacion.Core.Herramientas;
using Presentacion.Core.Movimiento;
using Presentacion.Core.Oferta;
using Presentacion.Core.Producto;
using Presentacion.Core.Producto.Lote;
using Presentacion.Core.Producto.Rubro;
using Presentacion.Core.TipoPago;
using Presentacion.Core.Venta;
using Presentacion.Core.Administracion.Configuracion;
using Presentacion.FBase.Helpers;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Presentacion.Core.Administracion.GraficosEstadisticas;

namespace Presentacion.Core.Administracion
{
    public partial class FAdministracion : FBase.FBase
    {
        private readonly long _logeadoId;
        private FConfiguracion _vistaConfiguracion;


        private FConfiguracion? _formularioConfiguracion;

      


        public FAdministracion(long logeadoId)
        {
            InitializeComponent();
            DibujarBotones();

            _logeadoId = logeadoId;

            // Ventana maximizada, sin bordes ni botones de cierre.
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            MaximizeBox = false;
            MinimizeBox = false;
            ControlBox = false;
        }

        private void FAdministracion_Load(object sender, EventArgs e)
        {
            IntegrarConfiguracion();
            MostrarConfiguracion();
        }

        // Navegación de los módulos administrativos.
        private void btnVolver_Click(object sender, EventArgs e)
        {
            if (_vistaConfiguracion != null && !_vistaConfiguracion.IsDisposed &&
                !_vistaConfiguracion.PuedeCerrar())
                return;

            Close();
        }
        private void sTOCKToolStripMenuItem_Click(object sender, EventArgs e) => new FProductoConsulta().Show();
        private void mARCASToolStripMenuItem_Click(object sender, EventArgs e) => new FMarcaConsulta(false).Show();
        private void cATEGORIASToolStripMenuItem_Click(object sender, EventArgs e) => new FCategoriaConsulta(false).Show();
        private void rUBROSToolStripMenuItem_Click(object sender, EventArgs e) => new FRubroConsulta(false).Show();
        private void lISTADOEMPLEADOSToolStripMenuItem_Click(object sender, EventArgs e) => new FEmpleadoConsulta(_logeadoId).Show();
        private void lISTADOCLIENTESToolStripMenuItem_Click(object sender, EventArgs e) => new FClienteConsulta().Show();
        private void lISTADOOFERTASToolStripMenuItem_Click(object sender, EventArgs e) => new FOfertaConsulta().Show();
        private void lOTESToolStripMenuItem_Click(object sender, EventArgs e) => new FLoteConsulta().Show();
        private void btnMovimientos_Click(object sender, EventArgs e) => new FMovimientoConsulta().Show();
        private void tIPOPAGOToolStripMenuItem_Click(object sender, EventArgs e) => new FTipoPagoConsulta().Show();
        private void rOLESToolStripMenuItem_Click(object sender, EventArgs e) => new FRolConsulta().Show();
        private void nUEVAOFERTAToolStripMenuItem_Click(object sender, EventArgs e) => new FOfertaABM().ShowDialog();
        private void btnGasto_Click(object sender, EventArgs e) => new Gasto.FGastoConsulta(_logeadoId).Show();
        private void historialToolStripMenuItem_Click(object sender, EventArgs e) => new FVentaConsulta().Show();
        private void historialVentasLibresToolStripMenuItem_Click(object sender, EventArgs e) => new FVentaLibreConsulta().Show();
        private void nuevaVentaLibreToolStripMenuItem_Click(object sender, EventArgs e) => new FVentaLibre(_logeadoId).Show();

        private void MostrarConfiguracion()
        {
            if (_formularioConfiguracion == null ||
                _formularioConfiguracion.IsDisposed)
            {
                _formularioConfiguracion = new FConfiguracion();

                _formularioConfiguracion.PrepararComoContenidoEmbebido();

                _formularioConfiguracion.TopLevel = false;
                _formularioConfiguracion.Dock = DockStyle.Fill;
                _formularioConfiguracion.Margin = Padding.Empty;
            }

            if (_formularioConfiguracion.Parent != pnlConfiguracion)
            {
                pnlConfiguracion.Controls.Clear();
                pnlConfiguracion.Controls.Add(_formularioConfiguracion);
            }

            _formularioConfiguracion.Show();
            _formularioConfiguracion.BringToFront();
        }

        /// <summary>
        /// Abre el visor de comprobantes PDF almacenados localmente.
        /// </summary>
        private void btnComprobantes_Click(object sender, EventArgs e)
        {
            string programDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string carpeta = Path.Combine(programDataPath, "Stockeate", "ComprobantesPdf");

            if (!Directory.Exists(carpeta))
            {
                Informar("La carpeta de comprobantes todavía no existe.", "Comprobantes");
                return;
            }

            using OpenFileDialog dialog = new OpenFileDialog
            {
                InitialDirectory = carpeta,
                Filter = "Archivos PDF (*.pdf)|*.pdf",
                Title = "Seleccionar comprobante"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using FVisorPDF visor = new FVisorPDF(dialog.FileName);
                visor.ShowDialog();
            }
        }

        private void DibujarBotones()
        {
            ConfigurarBoton(btnVolver, Constantes.Imagenes.ImgVolver);
            ConfigurarBoton(btnGasto, Constantes.Imagenes.ImgGasto);
            ConfigurarBoton(btnMovimientos, Constantes.Imagenes.ImgMovimiento);
            ConfigurarBoton(btnComprobantes, Constantes.Imagenes.ImgComprobante);
            ConfigurarBoton(btnEstadisticas, Constantes.Imagenes.ImgEstadisticas);
        }

        private static void ConfigurarBoton(Button boton, Image imagen)
        {
            const int anchoImagen = 24;
            const int altoImagen = 24;
            const int margenImagenTexto = 6;

            var imagenConMargen = new Bitmap(
                anchoImagen,
                altoImagen + margenImagenTexto,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(imagenConMargen))
            {
                g.Clear(Color.Transparent);

                // Ajustar la imagen manteniendo sus proporciones.
                float escala = Math.Min(
                    (float)anchoImagen / imagen.Width,
                    (float)altoImagen / imagen.Height);

                int ancho = (int)(imagen.Width * escala);
                int alto = (int)(imagen.Height * escala);

                int x = (anchoImagen - ancho) / 2;
                int y = (altoImagen - alto) / 2;

                g.DrawImage(imagen, x, y, ancho, alto);
            }

            boton.Image = imagenConMargen;
            boton.ImageAlign = ContentAlignment.TopCenter;
            boton.TextImageRelation = TextImageRelation.ImageAboveText;
            boton.Padding = new Padding(0, 10, 0, 0);

            boton.Disposed += (s, e) => imagenConMargen.Dispose();
        }

        private void modificarPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CustomPdf.FCustomPdf().Show();
        }

        private void acccesoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Se mantiene esta opción como ventana independiente del menú.
            new FConfiguracion().Show();
        }

        /// <summary>
        /// Aloja FConfiguracion dentro del panel central de administración.
        /// </summary>
        private void IntegrarConfiguracion()
        {
            if (_vistaConfiguracion != null && !_vistaConfiguracion.IsDisposed)
            {
                if (_vistaConfiguracion.Parent != pnlConfiguracion)
                {
                    _vistaConfiguracion.Parent?.Controls.Remove(_vistaConfiguracion);
                    pnlConfiguracion.Controls.Add(_vistaConfiguracion);
                }

                _vistaConfiguracion.BringToFront();
                return;
            }

            pnlConfiguracion.SuspendLayout();
            try
            {
                pnlConfiguracion.Controls.Clear();
                pnlConfiguracion.BackColor = TemaSistema.Fondo;

                _vistaConfiguracion = new FConfiguracion();

                _vistaConfiguracion.PrepararComoContenidoEmbebido();

                // Reafirmar el modo embebido antes de agregarlo al panel.
                _vistaConfiguracion.TopLevel = false;
                _vistaConfiguracion.FormBorderStyle = FormBorderStyle.None;
                _vistaConfiguracion.WindowState = FormWindowState.Normal;
                _vistaConfiguracion.ShowInTaskbar = false;
                _vistaConfiguracion.Dock = DockStyle.Fill;

                pnlConfiguracion.Controls.Add(_vistaConfiguracion);

                _vistaConfiguracion.Show();
                _vistaConfiguracion.BringToFront();
            }
            finally
            {
                pnlConfiguracion.ResumeLayout(true);
            }
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            var FEstadisticas = new FEstadisticas();
            FEstadisticas.Show();

        }
    }
}
