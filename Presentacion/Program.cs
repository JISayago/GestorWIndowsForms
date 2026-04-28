using AccesoDatos;
using AccesoDatos.Config;
using Presentacion.AccesoAlSistema;
using Presentacion.Constantes;
using Presentacion.Core.Categoria;
using Presentacion.Core.Empleado;
using Presentacion.Core.Empleado.Rol;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers;
using Servicios.Helpers.DatosObligatorios;
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
            bool estadoIniciado = false;
            ApplicationConfiguration.Initialize();

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

            var inicializadorDatosObligatorios = new InicializadorDatosObligatorios();
            List<string> mensajesOfertas = null;

            var mensajeCarga = "Preparando todo lo necesario...";

            using (var pantallaCarga = new PantallaCargaEspera(mensajeCarga))
            {
                pantallaCarga.Shown += async (s, e) =>
                {
                    Exception initEx = null;

                    try
                    {
                        var progreso = new Progress<(int progreso, string mensaje)>(p =>
                        {
                            pantallaCarga.SetProgress(p.progreso);
                            pantallaCarga.SetMensaje(p.mensaje);
                        });

                        await Task.Run(() =>
                        {
                            inicializadorDatosObligatorios.InicializadorDatos(progreso);
                        });

                        mensajesOfertas = inicializadorDatosObligatorios.mensajes;
                        estadoIniciado = inicializadorDatosObligatorios.seCargo;

                        pantallaCarga.SetProgress(100);
                        pantallaCarga.SetMensaje("Listo");

                        await Task.Delay(300);
                    }
                    catch (Exception ex)
                    {
                        initEx = ex;
                        MessageBox.Show(
                            "Error al inicializar datos: " + ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                    finally
                    {
                        pantallaCarga.Close();
                    }
                };

                Application.Run(pantallaCarga);
            }

            var login = new LoginForm();
            login.ShowDialog();

            if (login._usuarioLogeado == null || string.IsNullOrEmpty(login._usuarioLogeado.Username))
            {
                MessageBox.Show("Error: usuario no válido.");
                return;
            }

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

            Application.ThreadException += (s, e) =>
            {
                MessageBox.Show(
                    $"Excepción no manejada (UI thread):\n{e.Exception}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            };

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                MessageBox.Show(
                    $"Excepción no manejada (otro thread):\n{ex?.ToString()}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            };

            if (login.PuedeAccederAlSistema)
            {
                new DatosSistema(login._usuarioLogeado.PersonaId, login._usuarioLogeado.Nombre, login._usuarioLogeado.Apellido);
                Application.Run(new VentanaPrincipal(login._usuarioLogeado));
            }
            else
            {
                Application.Exit();
            }
        }
    }
}