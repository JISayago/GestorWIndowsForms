using AccesoDatos;
using AccesoDatos.Config;
using AccesoDatos.Database;
using Licencia.Constantes;
using Licencia.Modelos;
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
using Stockeate;
using Stockeate.Licensing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
            // PRIMERA EJECUCIÓN
            //---------------------------------------------

            bool primeraEjecucion =
                !File.Exists(LicensePaths.Installation);


            //---------------------------------------------
            // VALIDACIÓN DE LICENCIA
            //---------------------------------------------

            var resultadoLicencia =
                StartupValidator.ValidarInicio();


            //---------------------------------------------
            // NO EXISTE LICENCIA O ES INVÁLIDA
            //---------------------------------------------

            if (!resultadoLicencia.Valida &&
                (resultadoLicencia.Estado == LicenseStatus.NoExiste ||
                 resultadoLicencia.Estado == LicenseStatus.Invalida))
            {
                using (var activacion =
                       new FActivacion(primeraEjecucion))
                {
                    var resultadoFormulario =
                        activacion.ShowDialog();

                    if (resultadoFormulario != DialogResult.OK ||
                        !activacion.LicenciaValida)
                    {
                        return;
                    }
                }

                //---------------------------------------------
                // VOLVER A VALIDAR
                //---------------------------------------------

                resultadoLicencia =
                    StartupValidator.ValidarInicio();
            }


            //---------------------------------------------
            // ¿HAY UNA LICENCIA TRIAL?
            //---------------------------------------------

            LicenseInfo? licenciaActual = null;

            if (File.Exists(LicensePaths.License))
            {
                try
                {
                    licenciaActual =
                        new LicenseStorage().Leer();
                }
                catch
                {
                    // La validación posterior se encargará del error.
                }
            }


            if (licenciaActual != null &&
                licenciaActual.Tipo == LicenseType.Trial)
            {
                bool trialVigente =
                    resultadoLicencia.Valida &&
                    resultadoLicencia.Estado == LicenseStatus.Valida;


                //---------------------------------------------
                // TRIAL VIGENTE
                //---------------------------------------------

                if (trialVigente)
                {
                    using var formularioTrial =
                        new FActualizacionLicencia(
                            licenciaActual);

                    formularioTrial.ShowDialog();

                    //-----------------------------------------
                    // SI CARGÓ UNA PERMANENTE
                    //-----------------------------------------

                    if (formularioTrial.LicenciaActualizada)
                    {
                        resultadoLicencia =
                            StartupValidator.ValidarInicio();

                        licenciaActual =
                            new LicenseStorage().Leer();
                    }
                }


                //---------------------------------------------
                // TRIAL VENCIDA
                //---------------------------------------------

                else if (resultadoLicencia.Estado == LicenseStatus.Vencida)
                {
                    using var formularioTrial =
                        new FActualizacionLicencia(
                            licenciaActual);

                    formularioTrial.ShowDialog();

                    //-----------------------------------------
                    // SI NO CARGÓ PERMANENTE, NO CONTINÚA
                    //-----------------------------------------

                    if (!formularioTrial.LicenciaActualizada)
                    {
                        MessageBox.Show(
                            "La licencia de prueba ha vencido.\r\n\r\n" +
                            "Para continuar utilizando Stockeate debe " +
                            "cargar una licencia permanente.",
                            "Stockeate",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }


                    //-----------------------------------------
                    // VOLVER A VALIDAR
                    //-----------------------------------------

                    resultadoLicencia =
                        StartupValidator.ValidarInicio();

                    licenciaActual =
                        new LicenseStorage().Leer();
                }
            }


            //---------------------------------------------
            // LICENCIA NO VÁLIDA
            //---------------------------------------------

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
            // DETERMINAR SI ES TRIAL
            //---------------------------------------------

            bool esTrial =
                licenciaActual != null &&
                licenciaActual.Tipo == LicenseType.Trial;

            Conexion.UsarTrial = esTrial;

            try { PreparadorBase.Migrar(); }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo preparar la base de datos:\n" + ex.Message, "Error");
                return;
            }

            if (!PruebaConexion.ProbarConexion(out string error2))
            {
                MessageBox.Show(
                    $"No se pudo establecer conexión con la base de datos.\n\n{error2}",
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            new InicializadorDatosObligatorios().InicializarBaseMinima();

            if (esTrial)
            {
                var estado = SeedTrial.Estado();
                if (estado != EstadoSeed.Completa)
                {
                    Exception? errorSeed = null;
                    using var pantalla = new PantallaCargaEspera("Preparando la versión de prueba (puede demorar unos minutos)...");
                    pantalla.Shown += async (s, e) =>
                    {
                        try
                        {
                            var runner = new SeedRunner(Path.Combine(AppContext.BaseDirectory, "Seed"));
                            var progreso = new Progress<SeedProgreso>(p =>
                            {
                                pantalla.SetProgress(p.Paso * 100 / p.Total);
                                pantalla.SetMensaje($"Cargando datos de ejemplo ({p.Paso}/{p.Total})");
                            });
                            await Task.Run(() => runner.EjecutarAsync(estado == EstadoSeed.Parcial, progreso));
                        }
                        catch (Exception ex) { errorSeed = ex; }
                        finally { pantalla.Close(); }
                    };
                    Application.Run(pantalla);

                    if (errorSeed != null)
                    {
                        MessageBox.Show("No se pudieron cargar los datos de prueba:\n" + errorSeed.Message, "Error");
                        return;
                    }
                }
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

            var inicializadorBase =
                new InicializadorDatosObligatorios();

            inicializadorBase.InicializarBaseMinima();


            //---------------------------------------------
            // LOGIN
            //---------------------------------------------

            var login = new LoginForm();

            login.ShowDialog();

            if (!login.PuedeAccederAlSistema)
                return;

            if (login._usuarioLogeado == null ||
                string.IsNullOrWhiteSpace(
                    login._usuarioLogeado.Username))
            {
                MessageBox.Show(
                    "Error: usuario no válido.");

                return;
            }


            //---------------------------------------------
            // DATOS DEL SISTEMA
            //---------------------------------------------

            new DatosSistema(
                login._usuarioLogeado.PersonaId,
                login._usuarioLogeado.Nombre,
                login._usuarioLogeado.Apellido);

            var personaId =
                DatosSistema.UsuarioId;

            var cajaId =
                DatosSistema.CajaId;


            //---------------------------------------------
            // CARGA DE DATOS
            //---------------------------------------------

            var inicializador =
                new InicializadorDatosObligatorios(
                    personaId,
                    cajaId);

            List<string> mensajesOfertas = null;
            ElementoDePanelesPantallaPrincipal datosPantalla = null;
            List<ProductoDTO> productos = null;
            List<VentaDTO> ventas = null;


            using (var pantallaCarga =
                   new PantallaCargaEspera(
                       "Preparando todo lo necesario..."))
            {
                pantallaCarga.Shown += async (s, e) =>
                {
                    try
                    {
                        var progreso =
                            new Progress<(int, string)>(
                                p =>
                                {
                                    pantallaCarga.SetProgress(
                                        p.Item1);

                                    pantallaCarga.SetMensaje(
                                        p.Item2);
                                });


                        await Task.Run(() =>
                        {
                            inicializador.InicializadorDatos(
                                progreso);
                        });


                        mensajesOfertas =
                            inicializador.mensajes;

                        datosPantalla =
                            inicializador.DatosPantallaPrincipal;

                        productos =
                            inicializador.Productos;

                        ventas =
                            inicializador.Ventas;


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
                    string.Join(
                        "\n",
                        mensajesOfertas),
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }



            if (datosPantalla == null)
            {
                MessageBox.Show(
                    "No se pudieron cargar los datos.");

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