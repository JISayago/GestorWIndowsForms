using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stockeate
{

    /// <summary>
    /// Pantalla completa que tapa y bloquea a la ventana principal mientras la sesión secundaria está abierta.
    /// No se puede cerrar a mano: se cierra sola cuando el proceso secundario termina (también si se cae).
    /// Se muestra con ShowDialog(owner), así que es modal respecto de toda la ventana principal.
    /// </summary>
    internal sealed partial class FSesionSecundariaEnCurso : Form
    {
        // Ajustes del logo: marca de agua clara y de tamaño moderado (no el logo completo a todo color).
        private const float OpacidadLogo = 0.35f;   // 0 = invisible, 1 = color pleno
        private const int AnchoLogo = 810;
        private const int AltoLogo = 350;

        private volatile bool _puedeCerrar;

        /// <summary>Solo para el diseñador de Visual Studio.</summary>
        public FSesionSecundariaEnCurso()
        {
            InitializeComponent();
        }

        public FSesionSecundariaEnCurso(Process proceso) : this()
        {
            // Exited se dispara en un hilo del pool: se vuelve al hilo de la UI con BeginInvoke.
            proceso.EnableRaisingEvents = true;
            proceso.Exited += (_, _) => Liberar();

            // Si el proceso ya terminó antes de mostrarse el modal, no hay quien dispare Exited.
            Shown += (_, _) =>
            {
                if (proceso.HasExited) Liberar();
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            // Colores del sistema
            var fondo = TemaSistema.Oscuro;
            BackColor = fondo;
            tlpCentro.BackColor = fondo;
            lblTitulo.ForeColor = TemaSistema.Acento;
            lblDetalle.ForeColor = Color.WhiteSmoke;

            // Logo compuesto (el mismo de las consultas), aclarado y más chico
            var logo = Presentacion.Constantes.Imagenes.ImgLogoCompuesto;
            if (logo != null)
            {
                pbxLogo.Size = new Size(AnchoLogo, AltoLogo);
                pbxLogo.Image = Aclarar(logo, OpacidadLogo);   // copia propia: el original es compartido
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Solo se libera la copia aclarada; ImgLogoCompuesto es estático y lo usan otras pantallas.
            var copia = pbxLogo.Image;
            pbxLogo.Image = null;
            copia?.Dispose();

            base.OnFormClosed(e);
        }

        private static Bitmap Aclarar(Image original, float opacidad)
        {
            var resultado = new Bitmap(original.Width, original.Height);

            using var g = Graphics.FromImage(resultado);
            using var atributos = new ImageAttributes();

            var matriz = new ColorMatrix { Matrix33 = opacidad };   // canal alfa
            atributos.SetColorMatrix(matriz, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            g.DrawImage(
                original,
                new Rectangle(0, 0, resultado.Width, resultado.Height),
                0, 0, original.Width, original.Height,
                GraphicsUnit.Pixel,
                atributos);

            return resultado;
        }

        private void Liberar()
        {
            _puedeCerrar = true;

            try
            {
                if (IsHandleCreated && !IsDisposed)
                    BeginInvoke(new Action(Close));
            }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Bloquea Alt+F4 y cualquier cierre manual mientras la secundaria siga viva.
            if (!_puedeCerrar)
                e.Cancel = true;

            base.OnFormClosing(e);
        }
    }
}
