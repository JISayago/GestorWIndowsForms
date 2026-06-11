using AccesoDatos;
using AccesoDatos.Config;
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

            // 🔥 HANDLERS GLOBALES (ANTES DE TODO)
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

            // 🔥 VALIDAR CONEXIÓN
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

            // 🔥 DATOS MÍNIMOS (roles, permisos, admin)
            var inicializadorBase = new InicializadorDatosObligatorios();
            inicializadorBase.InicializarBaseMinima();

            // 🔥 LOGIN
            var login = new LoginForm();
            login.ShowDialog(); 
            if (!login.PuedeAccederAlSistema) return; 
            if (login._usuarioLogeado == null || string.IsNullOrEmpty(login._usuarioLogeado.Username)) 
            { MessageBox.Show("Error: usuario no válido."); return; } 
            
            // 🔥 SETEAR DATOS DEL SISTEMA
            new DatosSistema( login._usuarioLogeado.PersonaId, login._usuarioLogeado.Nombre, login._usuarioLogeado.Apellido );
            var personaId = DatosSistema.UsuarioId; var cajaId = DatosSistema.CajaId; 
            // 🔥 INICIALIZADOR COMPLETO
            var inicializador = new InicializadorDatosObligatorios(personaId, cajaId); 
            List<string> mensajesOfertas = null;
            ElementoDePanelesPantallaPrincipal datosPantalla = null; 
            List<ProductoDTO> productos = null; List<VentaDTO> ventas = null;
            var mensajeCarga = "Preparando todo lo necesario..."; 
            // 🔥 PANTALLA DE CARGA
            using (var pantallaCarga = new PantallaCargaEspera(mensajeCarga)) 
            { pantallaCarga.Shown += async (s, e) => 
            { 
                try {
                    var progreso = new Progress<(int progreso, string mensaje)>(p => { pantallaCarga.SetProgress(p.progreso); pantallaCarga.SetMensaje(p.mensaje); });
                    await Task.Run(() => { inicializador.InicializadorDatos(progreso); }); 
                    // 🔥 DATOS YA CARGADOS
                    mensajesOfertas = inicializador.mensajes;
                    datosPantalla = inicializador.DatosPantallaPrincipal;
                    productos = inicializador.Productos; 
                    ventas = inicializador.Ventas; 
                    pantallaCarga.SetProgress(100);
                    pantallaCarga.SetMensaje("Listo");
                    await Task.Delay(300); } 
                catch (Exception ex) 
                {
                    MessageBox.Show( "Error al inicializar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                } 
                finally { pantallaCarga.Close(); 
                }
            };
                Application.Run(pantallaCarga);
            }

            // 🔥 MENSAJES POST CARGA
            if (mensajesOfertas != null && mensajesOfertas.Count > 0)
            {
                MessageBox.Show(
                    "Se han realizado las siguientes modificaciones en las ofertas:\n\n" +
                    string.Join("\n", mensajesOfertas),
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            // 🔥 VALIDACIÓN FINAL
            if (datosPantalla == null)
            {
                MessageBox.Show("No se pudieron cargar los datos iniciales.", "Error");
                return;
            }

            // 🔥 ARRANQUE REAL DE LA APP
            Application.Run(
                new VentanaPrincipal(
                    login._usuarioLogeado,
                    datosPantalla,
                    productos,
                    ventas
                )
            );
        }
    }
}