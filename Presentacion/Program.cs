using AccesoDatos;
using AccesoDatos.Config;
using Presentacion.AccesoAlSistema;
using Presentacion.Core.Categoria;
using Presentacion.Core.Empleado;
using Presentacion.Core.Empleado.Rol;
using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.AccesoSistema;
using Servicios.Seguridad;


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
            else
            {
                var context = new GestorContextDBFactory().CreateDbContext(null);
                DatosUsuarioInicial.Inicializar(context);

                var login = new LoginForm();
                login.ShowDialog();
                if (login._usuarioLogeado == null || string.IsNullOrEmpty(login._usuarioLogeado.Username))
                {
                    MessageBox.Show("Error: usuario no válido.");
                    return;
                }

                Application.ThreadException += (s, e) =>
                {
                    MessageBox.Show($"Excepción no manejada (UI thread):\n{e.Exception.ToString()}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                };

                AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                {
                    var ex = e.ExceptionObject as Exception;
                    MessageBox.Show($"Excepción no manejada (otro thread):\n{ex?.ToString()}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                };

                if (login.PuedeAccederAlSistema)
                {
                    Application.Run(new VentanaPrincipal(login._usuarioLogeado));

                }
                else
                {
                    Application.Exit();
                }
            }     

        }
    }
}
