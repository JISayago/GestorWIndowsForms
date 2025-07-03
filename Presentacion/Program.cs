using AccesoDatos;
using AccesoDatos.Config;
using Presentacion.AccesoAlSistema;
using Presentacion.Core.Categoria;
using Presentacion.Core.Empleado;
using Presentacion.Core.Empleado.Rol;
using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
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

                if (login.PuedeAccederAlSistema)
                {
                    Application.Run(new FCategoriaConsulta());
                }
                else
                {
                    Application.Exit();
                }
            }     

        }
    }
}
