using AccesoDatos;
using AccesoDatos.Config;
using Licencia.Servicios;
using Microsoft.Extensions.Options;
using Presentacion.AccesoAlSistema;
using Presentacion.Constantes;
using Presentacion.FBase.Helpers;
using Servicios.Helpers;
using Servicios.Helpers.DatosObligatorios;
using Servicios.Helpers.DatosObligatoriosParaInicioSistema;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Venta.DTO;
using Servicios.Seguridad;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Application.ThreadException += (s, e) =>
            {
                MessageBox.Show(
                    $"Excepción no manejada (UI thread):\n{e.Exception}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            };

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = e.ExceptionObject as Exception;

                MessageBox.Show(
                    $"Excepción no manejada (otro thread):\n{ex}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            };

            //---------------------------------------------
            // VALIDACIÓN DE LICENCIA
            //---------------------------------------------

            var resultadoLicencia = StartupValidator.ValidarInicio();

            if (!resultadoLicencia.Valida)
            {
                MessageBox.Show(
                    resultadoLicencia.Mensaje,
                    "Licencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            //---------------------------------------------
            // CONEXIÓN
            //---------------------------------------------

            if (!PruebaConexion.ProbarConexion(out string error))
            {
                MessageBox.Show(
                    $"No se pudo establecer conexión con la base de datos.\n\n{error}",
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            //---------------------------------------------
            // DATOS MÍNIMOS
            //---------------------------------------------

            var inicializadorBase = new InicializadorDatosObligatorios();
            inicializadorBase.InicializarBaseMinima();

            //---------------------------------------------
            // LOGIN
            //---------------------------------------------

            var login = new LoginForm();

            login.ShowDialog();

            if (!login.PuedeAccederAlSistema)
                return;

            if (login._usuarioLogeado == null ||
                string.IsNullOrWhiteSpace(login._usuarioLogeado.Username))
            {
                MessageBox.Show("Error: usuario no válido.");
                return;
            }

            //---------------------------------------------
            // DATOS DEL SISTEMA
            //---------------------------------------------

            new DatosSistema(
                login._usuarioLogeado.PersonaId,
                login._usuarioLogeado.Nombre,
                login._usuarioLogeado.Apellido);

            var personaId = DatosSistema.UsuarioId;
            var cajaId = DatosSistema.CajaId;

            //---------------------------------------------
            // CARGA DE DATOS
            //---------------------------------------------

            var inicializador =
                new InicializadorDatosObligatorios(personaId, cajaId);

            List<string> mensajesOfertas = null;
            ElementoDePanelesPantallaPrincipal datosPantalla = null;
            List<ProductoDTO> productos = null;
            List<VentaDTO> ventas = null;

            using (var pantallaCarga =
                   new PantallaCargaEspera("Preparando todo lo necesario..."))
            {
                pantallaCarga.Shown += async (s, e) =>
                {
                    try
                    {
                        var progreso = new Progress<(int, string)>(p =>
                        {
                            pantallaCarga.SetProgress(p.Item1);
                            pantallaCarga.SetMensaje(p.Item2);
                        });

                        await Task.Run(() =>
                        {
                            inicializador.InicializadorDatos(progreso);
                        });

                        mensajesOfertas = inicializador.mensajes;
                        datosPantalla = inicializador.DatosPantallaPrincipal;
                        productos = inicializador.Productos;
                        ventas = inicializador.Ventas;

                        pantallaCarga.SetProgress(100);
                        pantallaCarga.SetMensaje("Listo");

                        await Task.Delay(300);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    finally
                    {
                        pantallaCarga.Close();
                    }
                };

                Application.Run(pantallaCarga);
            }

            //---------------------------------------------
            // MENSAJES
            //---------------------------------------------

            if (mensajesOfertas != null &&
                mensajesOfertas.Count > 0)
            {
                MessageBox.Show(
                    "Se realizaron las siguientes modificaciones:\n\n" +
                    string.Join("\n", mensajesOfertas),
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            if (datosPantalla == null)
            {
                MessageBox.Show("No se pudieron cargar los datos.");
                return;
            }

            //---------------------------------------------
            // SISTEMA
            //---------------------------------------------

            Application.Run(
                new VentanaPrincipal(
                    login._usuarioLogeado,
                    datosPantalla,
                    productos,
                    ventas));
        }
    }
}