using Servicios; // Asegurate de agregar esta referencia
using Servicios.Seguridad;
using System.Windows.Forms;

namespace Presentacion
{
    internal static class Program
    {

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {


            ApplicationConfiguration.Initialize();

            // Verificar conexión antes de iniciar la app
            if (!PruebaConexion.ProbarConexion())
            {
                MessageBox.Show(
                    "No se pudo establecer conexión con la base de datos.\nLa aplicación se cerrará.",
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            Application.Run(new Form1());
        }
       }
}
