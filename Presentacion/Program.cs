using AccesoDatos;
using AccesoDatos.Config;
using Presentacion.AccesoAlSistema;
using Presentacion.Constantes;
using Presentacion.Core.Categoria;
using Presentacion.Core.Empleado;
using Presentacion.Core.Empleado.Rol;
using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers;
using Servicios.Helpers.DatosObligatorios;
using Servicios.Seguridad;


namespace Presentacion
{
    internal static class Program
    {

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool estadoIniciado = false;
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

            // Preparar inicializador y contenedores para resultados
            var inicializadorDatosObligatorios = new InicializadorDatosObligatorios();
            List<string> mensajesOfertas = null;

            // Crear la pantalla de carga (no se muestra aún). El evento Shown arrancará la inicialización.
            var mensajeCarga = "Preparando todo lo necesario...";
            var minSeconds = 5; // <- cambiá a 10 si querés 10 segundos

            using (var pantallaCarga = new PantallaCargaEspera(Imagenes.GIFCarga, mensajeCarga))
            {
                pantallaCarga.Shown += async (s, e) =>
                {
                    Exception initEx = null;
                    try
                    {
                        var minimoDelay = Task.Delay(TimeSpan.FromSeconds(minSeconds));

                        // Ejecutar la inicialización en hilo de fondo
                        var initTask = Task.Run(() =>
                        {
                            inicializadorDatosObligatorios.InicializadorDatos();
                        });

                        // Esperar a que se cumplan ambas: inicialización y tiempo mínimo
                        await Task.WhenAll(initTask, minimoDelay);

                        // Recuperar resultados en hilo UI
                        mensajesOfertas = inicializadorDatosObligatorios.RetornarMensajeOfertasActivadasDesactivadasConflictos();
                        estadoIniciado = inicializadorDatosObligatorios.seCargo;
                    }
                    catch (Exception ex)
                    {
                        initEx = ex;
                        MessageBox.Show("Error al inicializar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        // Cerramos la pantalla de carga siempre (en el hilo UI)
                        pantallaCarga.Close();
                    }

                    // opcional: si querés loguear excepción aparte, lo podés hacer acá
                    if (initEx != null)
                    {
                        // ya mostramos MessageBox arriba; si necesitás más, lo agregás.
                    }
                };

                Application.Run(pantallaCarga);
            }

            // Aquí ya terminó la inicialización (o hubo un error)
            if (mensajesOfertas != null && mensajesOfertas.Count > 0)
            {
                MessageBox.Show(
                    "Se han realizado las siguientes modificaciones en las ofertas de descuento por conflictos:\n\n" +
                    string.Join("\n", mensajesOfertas),
                    "Información de ofertas",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            // Continuar con login y arranque normal
            var login = new LoginForm();
            login.ShowDialog();

            if (login._usuarioLogeado == null || string.IsNullOrEmpty(login._usuarioLogeado.Username))
            {
                MessageBox.Show("Error: usuario no válido.");
                return;
            }

            Application.ThreadException += (s, e) =>
            {
                MessageBox.Show($"Excepción no manejada (UI thread):\n{e.Exception}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                MessageBox.Show($"Excepción no manejada (otro thread):\n{ex?.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
