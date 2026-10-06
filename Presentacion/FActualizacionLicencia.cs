using System;
using System.Windows.Forms;
using Stockeate.Licensing;

namespace Presentacion
{
    public partial class FActualizacionLicencia : FBase.FBase
    {
        private readonly LicenseInfo _licenciaTrial;
        private readonly bool _licenciaVencida;

        public bool DeseaCargarLicencia { get; private set; }


        // ============================================================
        // CONSTRUCTOR
        // ============================================================

        public FActualizacionLicencia(
            LicenseInfo licenciaTrial,
            bool licenciaVencida = false)
        {
            InitializeComponent();

            _licenciaTrial = licenciaTrial;
            _licenciaVencida = licenciaVencida;

            DeseaCargarLicencia = false;

            MostrarInformacionTrial();
        }


        // ============================================================
        // MOSTRAR INFORMACIÓN DE LA TRIAL
        // ============================================================

        private void MostrarInformacionTrial()
        {
            // --------------------------------------------------------
            // FECHA DE VENCIMIENTO
            // --------------------------------------------------------

            if (_licenciaTrial.FechaVencimiento.HasValue)
            {
                lblFechaVencimiento.Text =
                    _licenciaTrial.FechaVencimiento.Value
                        .ToString("dd/MM/yyyy");
            }
            else
            {
                lblFechaVencimiento.Text =
                    "Sin fecha de vencimiento";
            }


            // --------------------------------------------------------
            // LICENCIA SIN FECHA
            // --------------------------------------------------------

            if (!_licenciaTrial.FechaVencimiento.HasValue)
            {
                lblTiempoRestante.Text =
                    "No se pudo determinar el tiempo restante.";

                return;
            }


            // --------------------------------------------------------
            // CALCULAR DÍAS
            // --------------------------------------------------------

            int diasRestantes =
                (
                    _licenciaTrial.FechaVencimiento.Value.Date -
                    DateTime.Today
                ).Days;


            // --------------------------------------------------------
            // VENCIDA
            // --------------------------------------------------------

            if (_licenciaVencida || diasRestantes < 0)
            {
                lblTiempoRestante.Text =
                    "La licencia de prueba ha vencido.";

                return;
            }


            // --------------------------------------------------------
            // VENCE HOY
            // --------------------------------------------------------

            if (diasRestantes == 0)
            {
                lblTiempoRestante.Text =
                    "La licencia de prueba vence hoy.";

                return;
            }


            // --------------------------------------------------------
            // UN DÍA
            // --------------------------------------------------------

            if (diasRestantes == 1)
            {
                lblTiempoRestante.Text =
                    "Queda 1 día de prueba.";

                return;
            }


            // --------------------------------------------------------
            // VARIOS DÍAS
            // --------------------------------------------------------

            lblTiempoRestante.Text =
                $"Quedan {diasRestantes} días de prueba.";
        }


        // ============================================================
        // CARGAR LICENCIA PERMANENTE
        // ============================================================

        private void btnCargarLicencia_Click(
            object sender,
            EventArgs e)
        {
            DeseaCargarLicencia = true;

            DialogResult =
                DialogResult.OK;

            Close();
        }


        // ============================================================
        // CONTINUAR CON TRIAL
        // ============================================================

        private void btnContinuarPrueba_Click(
            object sender,
            EventArgs e)
        {
            DeseaCargarLicencia = false;

            DialogResult =
                DialogResult.Cancel;

            Close();
        }


        // ============================================================
        // CERRAR
        // ============================================================

        private void btnCerrar_Click(
            object sender,
            EventArgs e)
        {
            DeseaCargarLicencia = false;

            DialogResult =
                DialogResult.Cancel;

            Close();
        }
    }
}