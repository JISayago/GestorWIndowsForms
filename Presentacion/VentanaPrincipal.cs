using AccesoDatos.Entidades;
using Presentacion.AccesoAlSistema;
using Presentacion.Core.Administracion;
using Presentacion.Core.Articulo.Marca;
using Presentacion.Core.Caja;
using Presentacion.Core.Categoria;
using Presentacion.Core.Cliente;
using Presentacion.Core.CuentaCorriente;
using Presentacion.Core.Empleado;
using Presentacion.Core.Empleado.Rol;
using Presentacion.Core.Movimiento;
using Presentacion.Core.Oferta;
using Presentacion.Core.Producto;
using Presentacion.Core.Producto.Rubro;
using Presentacion.Core.TipoPago;
using Presentacion.Core.Venta;
using Presentacion.FBase.Helpers;
using Presentacion.Notificaciones;
using ServicioAccesoSistema.AccesoSistema;
using Servicios.Helpers.DatosObligatoriosParaInicioSistema;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.Rol;
using Servicios.LogicaNegocio.Caja.DTO;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.PantallaPrincipal;
using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Producto.Lote;
using Servicios.LogicaNegocio.Sistema;
using Servicios.LogicaNegocio.Venta.DTO;
using Servicios.LogicaNegocio.Venta.Oferta;
using Stockeate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class VentanaPrincipal : FBase.FBase
    {
        #region Campos y Propiedades

        private readonly UsuarioLogeado _usuarioLogeado;
        private readonly IOfertaServicio _ofertaServicio;
        private readonly IDetallesSistemaServicio _dellesSistema;
        private readonly IAccesoSistema _accesoSistema;
        private readonly IPantallaPrincipalServicio _pantallaPrincipalServicio;
        private readonly ILoteServicio _loteServicio;
        private DateTime _fechaActual = DateTime.Now;
        private DateTime _horaActual = DateTime.Now;
        private PanelDatosTurno _panelDatosTurno;
        private PanelConsultasRapidas _panelConsultasRapidas;
        private DatosTurnoDTO _ultimoEstadoTurno;

        private readonly ElementoDePanelesPantallaPrincipal _datosIniciales;
        private readonly List<ProductoDTO> _productosIniciales;
        private readonly List<VentaDTO> _ventasIniciales;

        #endregion

        #region Constructores

        public VentanaPrincipal(UsuarioLogeado usuarioLogeado, ElementoDePanelesPantallaPrincipal datosIniciales, List<ProductoDTO> productosIniciales, List<VentaDTO> ventasIniciales)
        {
            InitializeComponent();
            DibujarBotones();

            _ofertaServicio = new OfertaServicio();
            _dellesSistema = new DetallesSistemaServicio();
            _accesoSistema = new AccesoSistema();
            _pantallaPrincipalServicio = new PantallaPrincipalServicio();
            _loteServicio = new LoteServicio();

            _usuarioLogeado = usuarioLogeado;

            // 🔥 NUEVO
            _datosIniciales = datosIniciales;
            _productosIniciales = productosIniciales ?? new List<ProductoDTO>();
            _ventasIniciales = ventasIniciales ?? new List<VentaDTO>();

            this.Bounds = Screen.PrimaryScreen.WorkingArea;

            // Formato de nombre de usuario (junto a "Usuario Logeado:")
            var font = new Font("Segoe UI", 13.75F, FontStyle.Bold);
            lblNombreUsuario.Font = font;
            lblNombreUsuario.ForeColor = TemaSistema.Primario;
            lblNombreUsuario.Text = _usuarioLogeado.Username.ToUpper();
            lblNombreUsuario.AutoSize = true;
            lblNombreUsuario.Dock = DockStyle.None;
            lblNombreUsuario.TextAlign = ContentAlignment.MiddleLeft;

            if (SesionSecundaria.EsSecundaria)
            {
                //lblNombreUsuario.Text += "- Secundaria";
                lblUsuario.Text = "Usuario secundario:";
                lblNombreUsuario.ForeColor = Color.Firebrick;
                btnCambioUsuario.Visible = false;            // las sesiones no se anidan
                btnCerarSesion.Text = "Cerrar Secundaria";
            }


            //VENTANA FULLSCREEN SIN BORDES NI BOTONES DE CIERRE
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;
            //this.ShowIcon = false;
            //this.ShowInTaskbar = false; // opcional
        }

        #endregion

        #region Inicialización y Carga (Load)

        private void VentanaPrincipal_Load(object sender, EventArgs e)
        {
            UnificarFondosPantallaPrincipal();

            // 1. Configurar Timers
            InicializarTimers();

            // 2. Suscribir eventos de Layout
            tlpPanelBaseTabControlYNotis.Layout += (_, __) => AlinearNotificacionesConUltimasVentas();

            // 3. Inicializar Panel de Turno y Datos
            _panelDatosTurno = new PanelDatosTurno();
            crearPanelDatosAdicionales();

            // 4. Cargar Notificaciones
            panelAvisos.Recargar();

            // 5. Alinear top de notificaciones con la grilla de Últimas Ventas
            BeginInvoke(new Action(AlinearNotificacionesConUltimasVentas));
        }

        private void UnificarFondosPantallaPrincipal()
        {
            var fondo = TemaSistema.Fondo;
            const int padH = 20;

            // Mismo margen horizontal header / consultas+notis / botones
            tableLayoutPanel2.Padding = new Padding(padH, 0, padH, 0);
            tlpPanelBaseTabControlYNotis.Padding = new Padding(padH, 0, padH, 0);
            PnlBotones.Padding = new Padding(padH, 0, padH, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tblUsuario.Dock = DockStyle.Fill;
            //flpDatosFechaHora.Dock = DockStyle.Right;
            //flpDatosFechaHora.AutoSize = true;
            //flpDatosFechaHora.WrapContents = false;
            //flpDatosFechaHora.FlowDirection = FlowDirection.RightToLeft;

            tlpBaseInfo1.BackColor = fondo;
            tableLayoutPanel2.BackColor = fondo;
            tblUsuario.BackColor = fondo;
            flpUsuarioLogeado.BackColor = fondo;
            tlpPanelBaseTabControlYNotis.BackColor = fondo;
            tlpNotificaciones0.BackColor = fondo;
            PnlBotones.BackColor = fondo;
            flpDatosFechaHora.BackColor = fondo;
            flowLayoutPanel3.BackColor = fondo;

            tcIzquierda.BackColor = fondo;
            tcIzquierda.HeaderBackColor = fondo;
            tcIzquierda.AplicarTema();
            tabPage1.UseVisualStyleBackColor = false;
            tabPage1.BackColor = fondo;
            tabPage1.Text = "Acceso Rápido";
            tabPage2.UseVisualStyleBackColor = false;
            tabPage2.BackColor = fondo;
        }

        private void InicializarTimers()
        {
            // Timer Hora Sistema (Reloj superior)
            System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();
            MyTimer.Interval = 1000;
            MyTimer.Tick += MyTimer_Tick;
            MyTimer.Start();

            // Timer Datos de Caja (Sincronización cada 15s)
            System.Windows.Forms.Timer MyTimerDatos = new System.Windows.Forms.Timer();
            MyTimerDatos.Interval = 15000;
            MyTimerDatos.Tick += MyTimerDatos_Tick;
            MyTimerDatos.Start();

            // Timer Avisos (recarga del panel de notificaciones y calendario cada 1 hora)
            System.Windows.Forms.Timer timerAvisos = new System.Windows.Forms.Timer();
            timerAvisos.Interval = 60 * 60 * 1000;
            timerAvisos.Tick += TimerAvisos_Tick;
            timerAvisos.Start();
        }

        private void TimerAvisos_Tick(object? sender, EventArgs e)
        {
            // Solo si la ventana principal está a la vista y activa (no minimizada ni con otra ventana,
            // consulta o popup encima), para no interrumpir lo que el usuario esté haciendo.
            if (!Visible || WindowState == FormWindowState.Minimized || ActiveForm != this)
                return;

            try
            {
                panelAvisos.Recargar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"No se pudo recargar el panel de avisos: {ex.Message}");
            }
        }

        #endregion

        #region Lógica de Actualización y Datos (Business Logic)

        private void ActualizarInformacionTurno()
        {
            var datosNuevos = _pantallaPrincipalServicio.ObtenerActualizarDatosCaja(DatosSistema.CajaId, _ultimoEstadoTurno);

            // Siempre marcar el poll del timer (aunque no haya cambios de montos).
            _panelDatosTurno?.MarcarUltimaActualizacion(DateTime.Now);

            if (datosNuevos == null || datosNuevos.Equals(_ultimoEstadoTurno))
                return;

            _ultimoEstadoTurno = datosNuevos;
            _panelDatosTurno.ActualizarSoloTextoCaja(datosNuevos);
        }

        //private void crearPanelDatosAdicionales()
        //{

        //    //// 1. Inicializas la interfaz
        //    //var panelConsultas = new PanelConsultasRapidas();
        //    //panelConsultas.CargarConsultasRapidas(tabPage1);

        //    //// 2. Obtienes los datos de tus servicios (simulado)
        //    //var ultimasVentas = _ventaServicio.ObtenerUltimas(10);
        //    //var stockProductos = _productoServicio.ObtenerTodos();

        //    //// 3. Llenas las tablas
        //    //panelConsultas.ActualizarTablaVentas(ultimasVentas);
        //    //panelConsultas.ActualizarTablaProductos(stockProductos);

        //    //////////////////////////////////
        //    //var panelConsultasRapidas = new PanelConsultasRapidas();

        //    //// Carga inicial de datos
        //    //var datosTurno = _pantallaPrincipalServicio.ObtenerDatosTurno(DatosSistema.CajaId, DatosSistema.UsuarioId);
        //    //_ultimoEstadoTurno = datosTurno;

        //    //// Renderizar en tabPage2 usando la instancia global
        //    //_panelDatosTurno.CargarResumenTurno(tabPage2, datosTurno);

        //    //// Renderizar consultas en tabPage1
        //    //panelConsultasRapidas.CargarConsultasRapidas(tabPage1);
        //}
        private void crearPanelDatosAdicionales()
        {
            _panelConsultasRapidas = new PanelConsultasRapidas();

            // Turno: snapshot del arranque
            if (_datosIniciales?.DatosTurno != null)
            {
                _ultimoEstadoTurno = _datosIniciales.DatosTurno;
                _panelDatosTurno.CargarResumenTurno(tabPage2, _datosIniciales.DatosTurno);
            }

            // UI de grillas sin pegarle otra vez a BD; datos del precarga
            tabPage1.Controls.Clear();
            _panelConsultasRapidas.CargarConsultasRapidas(tabPage1, cargarDatosDesdeBd: false);
            _panelConsultasRapidas.AplicarDatosIniciales(_productosIniciales, _ventasIniciales);
        }

        #endregion

        #region Eventos de Timer e Interfaz

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            lblFechaValor.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblHoraValor.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void MyTimerDatos_Tick(object sender, EventArgs e)
        {
            ActualizarInformacionTurno();
        }

        /// <summary>
        /// Baja el bloque de notificaciones para que su contenido arranque
        /// a la misma altura que la grilla de Últimas Ventas.
        /// </summary>
        private bool _alineandoNotificaciones;

        private void AlinearNotificacionesConUltimasVentas()
        {
            if (_alineandoNotificaciones)
                return;

            var gridVentas = _panelConsultasRapidas?.GridUltimasVentas;
            if (gridVentas == null || !gridVentas.IsHandleCreated || !panelAvisos.IsHandleCreated)
                return;
            if (gridVentas.Height <= 0 || !gridVentas.Visible)
                return;

            try
            {
                _alineandoNotificaciones = true;

                int yGrid = gridVentas.PointToScreen(Point.Empty).Y;
                int yFlow = panelAvisos.PointToScreen(Point.Empty).Y;
                int diff = yGrid - yFlow;

                if (Math.Abs(diff) < 2)
                    return;

                var pad = tlpNotificaciones0.Padding;
                int nuevoTop = Math.Max(0, pad.Top + diff);
                if (nuevoTop == pad.Top)
                    return;

                tlpNotificaciones0.Padding = new Padding(pad.Left, nuevoTop, pad.Right, pad.Bottom);
            }
            finally
            {
                _alineandoNotificaciones = false;
            }
        }

        #endregion

        #region Navegación y Botones

        private void btnPanelAdmin_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.Tiene("Admin.Acceso"))
            {
                Informar("Acceso disponible sólo para Administradores", "Acceso denegado");
                //MessageBox.Show("Acceso disponible sólo para Administradores");
                return;
            }
            var administracion = new FAdministracion(_usuarioLogeado.PersonaId);
            administracion.Show();
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.Tiene("Ventas.Crear"))
            {
                Informar("No Cuenta con las credenciales necesarias para realizar ventas", "Acceso denegado");
                //MessageBox.Show("No Cuenta con las credenciales necesarias para ver los movimientos de la caja");
                return;
            }
            var FVenta = new FVenta(_usuarioLogeado.PersonaId);
            FVenta.Show();
        }

        private void btnCaja_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.Tiene("Caja.Ver"))
            {
                Informar("No Cuenta con las credenciales necesarias para acceder a la caja", "Acceso denegado");
                //MessageBox.Show("No Cuenta con las credenciales necesarias para acceder a la caja");
                return;
            }
            var fCaja = new FCaja();
            fCaja.Show();
        }

        private void btnContraVenta_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.Tiene("Ventas.Contrasiento"))
            {
                Informar("No Cuenta con las credenciales necesarias para ver los movimientos de la caja", "Acceso denegado");
                //MessageBox.Show("No Cuenta con las credenciales necesarias para ver los movimientos de la caja");
                return;
            }
            var NroCompr = new FNroComprobanteParaCancelacion(_usuarioLogeado.PersonaId);
            NroCompr.Show();
        }

        private void llbCerrarSesion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var respuesta = _accesoSistema.CerrarSesion(_usuarioLogeado.PersonaId);
            if (!respuesta.Exitoso)
            {
                MostrarError(respuesta.Mensaje);
                //MessageBox.Show(respuesta.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Exito(respuesta.Mensaje, "Información");
            //MessageBox.Show(respuesta.Mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SalirDeSesion();
        }

        #endregion

        #region Mocks de Datos (Pruebas)

        public List<NotificacionDTO> mockDatosNotificaciones(string tipoDato)
        {
            var listaLotesNotificar = new List<NotificacionDTO>
            {
                new NotificacionDTO { NotificacionId = 1, Titulo = "Producto: Leche Entera (URGENTE)", Descripcion = "Lote: L-001 venció hace 2 días (20/04/2026).", FechaNotificacion = DateTime.Now.AddMinutes(-60), Leida = false, NivelUrgencia = 1 },
                new NotificacionDTO { NotificacionId = 2, Titulo = "Producto: Yogurt Frutilla (PRÓXIMO)", Descripcion = "Lote: L-002 vence en 2 días (24/04/2026).", FechaNotificacion = DateTime.Now.AddMinutes(-30), Leida = false, NivelUrgencia = 2 },
                new NotificacionDTO { NotificacionId = 3, Titulo = "Producto: Arroz (AL DÍA)", Descripcion = "Lote: L-003 vence en 15 días (07/05/2026).", FechaNotificacion = DateTime.Now, Leida = false, NivelUrgencia = 3 },
                new NotificacionDTO { NotificacionId = 4, Titulo = "Producto: Queso Crema", Descripcion = "Lote: L-009 VENCIDO. Retirar de góndola inmediatamente.", FechaNotificacion = DateTime.Now.AddDays(-1), Leida = false, NivelUrgencia = 1 },
                new NotificacionDTO { NotificacionId = 5, Titulo = "Producto: Manteca 200g", Descripcion = "Lote: L-102 vence mañana. Considerar liquidación.", FechaNotificacion = DateTime.Now.AddHours(-5), Leida = true, NivelUrgencia = 2 },
                new NotificacionDTO { NotificacionId = 6, Titulo = "Producto: Fideos Tallarín", Descripcion = "Lote: L-505. Vencimiento lejano (Diciembre 2026).", FechaNotificacion = DateTime.Now.AddDays(-2), Leida = true, NivelUrgencia = 3 }
            };

            var listaOfertasVencidas = new List<NotificacionDTO>
            {
                new NotificacionDTO { NotificacionId = 101, Titulo = "Oferta Vencida (ALTA)", Descripcion = "La oferta Verano 2026 ya expiró.", FechaNotificacion = DateTime.Now.AddDays(-1), Leida = false, NivelUrgencia = 1 },
                new NotificacionDTO { NotificacionId = 102, Titulo = "Oferta por Vencer (MEDIA)", Descripcion = "La oferta Fin de Semana vence mañana.", FechaNotificacion = DateTime.Now, Leida = false, NivelUrgencia = 2 },
                new NotificacionDTO { NotificacionId = 103, Titulo = "Oferta Vigente (BAJA)", Descripcion = "La oferta Mensual vence en 20 días.", FechaNotificacion = DateTime.Now.AddHours(-2), Leida = true, NivelUrgencia = 3 },
                new NotificacionDTO { NotificacionId = 104, Titulo = "Liquidación de Stock: Bebidas", Descripcion = "Finalizó el tiempo de la promo 4x3 en gaseosas.", FechaNotificacion = DateTime.Now.AddDays(-3), Leida = false, NivelUrgencia = 1 },
                new NotificacionDTO { NotificacionId = 105, Titulo = "Descuento Empleados", Descripcion = "Actualización de cupos para el mes de Mayo.", FechaNotificacion = DateTime.Now, Leida = false, NivelUrgencia = 3 }
            };

            var listaCuentasCorrientes = new List<NotificacionDTO>
            {
                new NotificacionDTO { NotificacionId = 501, Titulo = "Cta Cte: Distribuidora X", Descripcion = "Saldo muy atrasado. Vencimiento: 15/04/2026.", FechaNotificacion = DateTime.Now.AddDays(-5), Leida = false, NivelUrgencia = 1 },
                new NotificacionDTO { NotificacionId = 502, Titulo = "Cta Cte: Almacén Y", Descripcion = "Factura por vencer hoy (22/04/2026).", FechaNotificacion = DateTime.Now, Leida = false, NivelUrgencia = 2 },
                new NotificacionDTO { NotificacionId = 503, Titulo = "Cta Cte: Kiosco Z", Descripcion = "Vencimiento programado para la próxima semana.", FechaNotificacion = DateTime.Now.AddDays(-1), Leida = false, NivelUrgencia = 3 },
                new NotificacionDTO { NotificacionId = 504, Titulo = "Cta Cte: Supermercado 'El Sol'", Descripcion = "Deuda prejudicial. El cliente ha superado el límite de crédito permitido.", FechaNotificacion = DateTime.Now.AddDays(-7), Leida = false, NivelUrgencia = 1 },
                new NotificacionDTO { NotificacionId = 505, Titulo = "Cta Cte: Carnicería Don Pepe", Descripcion = "Pago parcial recibido. Resta abonar el 50% de la factura actual.", FechaNotificacion = DateTime.Now.AddHours(-10), Leida = true, NivelUrgencia = 2 },
                new NotificacionDTO { NotificacionId = 506, Titulo = "Cta Cte: Minimarket Centro", Descripcion = "Aviso de cortesía: Su factura vence en 10 días.", FechaNotificacion = DateTime.Now.AddDays(-2), Leida = false, NivelUrgencia = 3 }
            };

            return tipoDato switch
            {
                "Lote" => listaLotesNotificar,
                "Oferta" => listaOfertasVencidas,
                "CtaCte" => listaCuentasCorrientes,
                _ => new List<NotificacionDTO>()
            };
        }

        #endregion

        private void tlpNotificaciones0_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DibujarBotones()
        {
            //========================= //
            //BOTON PANEL ADMINISTRACION//
            //========================= //

            btnPanelAdmin.Image = Constantes.Imagenes.ImgAdministracion;

            // Alineamos la imagen arriba al centro
            btnPanelAdmin.ImageAlign = ContentAlignment.TopCenter;
            btnPanelAdmin.TextImageRelation = TextImageRelation.ImageAboveText;

            // Le damos un padding superior para que el ícono no pegue contra el techo del botón
            btnPanelAdmin.Padding = new Padding(0, 10, 0, 0);

            //========================= //
            //BOTON CAJA                //
            //========================= //

            btnCaja.Image = Constantes.Imagenes.ImgCaja;
            // Alineamos la imagen arriba al centro
            btnCaja.ImageAlign = ContentAlignment.TopCenter;
            btnCaja.TextImageRelation = TextImageRelation.ImageAboveText;

            // Le damos un padding superior para que el ícono no pegue contra el techo del botón
            btnCaja.Padding = new Padding(0, 10, 0, 0);

            //========================= //
            //BOTON CANCELACION          //
            //========================= //

            btnContraVenta.Image = Constantes.Imagenes.ImgCancelacion;
            // Alineamos la imagen arriba al centro
            btnContraVenta.ImageAlign = ContentAlignment.TopCenter;
            btnContraVenta.TextImageRelation = TextImageRelation.ImageAboveText;
            // Le damos un padding superior para que el ícono no pegue contra el techo del botón
            btnContraVenta.Padding = new Padding(0, 10, 0, 0);

            //========================= //
            //BOTON VENTA               //
            //========================= //

            btnVenta.Image = Constantes.Imagenes.ImgVenta;
            // Alineamos la imagen arriba al centro
            btnVenta.ImageAlign = ContentAlignment.TopCenter;
            btnVenta.TextImageRelation = TextImageRelation.ImageAboveText;
            // Le damos un padding superior para que el ícono no pegue contra el techo del botón
            btnVenta.Padding = new Padding(0, 10, 0, 0);

        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);

        private const int WM_SETREDRAW = 0x000B;
        private const uint RDW_INVALIDATE = 0x0001, RDW_ERASE = 0x0004, RDW_ALLCHILDREN = 0x0080, RDW_UPDATENOW = 0x0100;

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            // Se congela el dibujo solo del contenido (no de la ventana completa, para no tocar la barra
            // de tareas de Windows) mientras se recarga; al terminar se repinta una sola vez.
            SendMessage(panel1.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
            try
            {
                // 1) Notificaciones: regenerar + pintar (único lugar donde conviene rebuild completo)
                panelAvisos.Recargar();

                // 2) Turno fresco desde BD
                var datosTurno = _pantallaPrincipalServicio.ObtenerDatosTurno(DatosSistema.CajaId, DatosSistema.UsuarioId);
                if (datosTurno != null)
                {
                    _ultimoEstadoTurno = datosTurno;
                    _panelDatosTurno.CargarResumenTurno(tabPage2, datosTurno);
                    _panelDatosTurno.MarcarUltimaActualizacion(DateTime.Now);
                }

                // 3) Grillas desde BD (no reusar snapshot del arranque)
                if (_panelConsultasRapidas == null || tabPage1.Controls.Count == 0)
                {
                    tabPage1.Controls.Clear();
                    _panelConsultasRapidas = new PanelConsultasRapidas();
                    _panelConsultasRapidas.CargarConsultasRapidas(tabPage1, cargarDatosDesdeBd: true);
                }
                else
                {
                    _panelConsultasRapidas.RefrescarTodoDesdeBd();
                }
            }
            finally
            {
                SendMessage(panel1.Handle, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
                RedrawWindow(panel1.Handle, IntPtr.Zero, IntPtr.Zero, RDW_INVALIDATE | RDW_ERASE | RDW_ALLCHILDREN | RDW_UPDATENOW);
                Cursor = Cursors.Default;
            }
        }

        private void flowHeaderUsuario_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Escape cierra la ventana: se pasa por OnFormClosing, que pregunta antes de salir.
        /// </summary>
        protected override void EjecutarEscape()
        {
            Close();
        }

        /// <summary>
        /// Cerrar la ventana principal (Escape, Alt+F4 o cualquier cierre iniciado por el usuario) ya no
        /// sale del sistema sin avisar: pregunta y, si el usuario acepta, hace lo mismo que el botón
        /// "Cerrar Sesión". Los cierres que provoca el propio sistema (Application.Restart, apagado de
        /// Windows) no se interceptan.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                var result = Confirmar(
                  SesionSecundaria.EsSecundaria
                    ? "¿Querés cerrar la sesión secundaria y volver a la principal?"
                    : "¿Querés cerrar la sesión y salir del sistema?",
                    "Cerrar sesión",
                 si: "Aceptar",
                 no: "Cancelar");

                //var respuesta = MessageBox.Show(
                //    "¿Querés cerrar la sesión y salir del sistema?",
                //    "Cerrar sesión",
                //    MessageBoxButtons.YesNo,
                //    MessageBoxIcon.Question,
                //    MessageBoxDefaultButton.Button2);

                if (result)
                    btnCerarSesion_Click(this, EventArgs.Empty);

                return;
            }

            base.OnFormClosing(e);
        }

        private void btnCerarSesion_Click(object sender, EventArgs e)
        {
            var respuesta = _accesoSistema.CerrarSesion(_usuarioLogeado.PersonaId);
            if (!respuesta.Exitoso)
            {
                MostrarError(respuesta.Mensaje);
                //MessageBox.Show(respuesta.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Exito(respuesta.Mensaje, "Exito");
            //MessageBox.Show(respuesta.Mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SalirDeSesion();
        }

        /// <summary>
        /// Abre una sesión secundaria: otra copia de Stockeate (otro proceso) donde otro usuario, ej. un admin,
        /// hace un cambio o una venta puntual. Esta ventana queda bloqueada hasta que la secundaria termina.
        /// </summary>
        private void btnCambioUsuario_Click(object sender, EventArgs e)
        {
            var sesionId = _accesoSistema.ObtenerSesionActivaId(_usuarioLogeado.PersonaId);
            if (sesionId == null)
            {
                MostrarError("No se encontró tu sesión activa. Cerrá sesión y volvé a ingresar.");
                return;
            }

            Process proceso;
            try
            {
                proceso = Process.Start(SesionSecundaria.CrearInicio(sesionId.Value, _usuarioLogeado.Username));
            }
            catch (Exception ex)
            {
                MostrarError("No se pudo abrir la sesión secundaria:\n" + ex.Message);
                return;
            }

            if (proceso == null)
            {
                MostrarError("No se pudo abrir la sesión secundaria.");
                return;
            }

            using (proceso)
            using (var bloqueo = new FSesionSecundariaEnCurso(proceso))
            {
                bloqueo.ShowDialog(this);   // modal: nada de esta ventana responde hasta que la secundaria termina
            }

            VolverDeSesionSecundaria(sesionId.Value);
        }

        private void VolverDeSesionSecundaria(long sesionPrincipalId)
        {
            // 1) Si la secundaria cayó sin cerrar sesión, queda "Activa" en la BD: se regulariza.
            _accesoSistema.CerrarSesionesSecundarias(sesionPrincipalId);

            // 2) La secundaria pudo abrir/cerrar la caja: se relee (el ctor sin parámetros no toca el usuario).
            new DatosSistema();

            // 3) El admin pudo cambiarle roles, permisos o datos a este usuario, o bloquearlo/eliminarlo.
            if (!RecargarUsuarioLogeado())
            {
                Informar("Tu usuario fue modificado o deshabilitado durante la sesión secundaria. Se cerrará tu sesión.", "Acceso");
                _accesoSistema.CerrarSesion(_usuarioLogeado.PersonaId);
                SalirDeSesion();
                return;
            }

            // 4) Turno, avisos y grillas frescos desde la BD.
            btnRefresh_Click(this, EventArgs.Empty);
        }

        /// <summary>
        /// Vuelve a leer de la BD al usuario logueado y actualiza el MISMO objeto (_usuarioLogeado), que es el que
        /// consulta AuthHelper. Los permisos se evalúan al hacer clic en cada botón (AuthHelper.Tiene), así que
        /// los cambios rigen desde el próximo clic. Devuelve false si el usuario ya no puede operar.
        /// </summary>
        private bool RecargarUsuarioLogeado()
        {
            var empleado = new EmpleadoServicio().ObtenerEmpleadoPorId(_usuarioLogeado.PersonaId);
            if (empleado == null || empleado.PersonaId == null)
                return false;                                   // eliminado

            // Se valida con el username NUEVO: si el admin lo renombró, el viejo ya no existe.
            if (_accesoSistema.VerificarSiUsuarioEstaBloqueado(empleado.Username))
                return false;                                   // bloqueado / deshabilitado

            var usuarioServicio = new UsuarioServicio();
            _usuarioLogeado.Nombre = empleado.Nombre;
            _usuarioLogeado.Apellido = empleado.Apellido;
            _usuarioLogeado.Username = empleado.Username;
            _usuarioLogeado.Permisos = usuarioServicio.ObtenerPermisosPorUsuario(_usuarioLogeado.PersonaId);
            _usuarioLogeado.EsSuperAdmin = usuarioServicio.EsSuperAdmin(_usuarioLogeado.PersonaId);

            // Se reasigna por si AuthHelper arma algún caché de permisos al setear UsuarioActual.
            AuthHelper.UsuarioActual = _usuarioLogeado;

            lblNombreUsuario.Text = _usuarioLogeado.Username.ToUpper();
            return true;
        }
        private static void SalirDeSesion()
        {
            // La secundaria simplemente se cierra.
            if (SesionSecundaria.EsSecundaria)
            {
                Application.Exit();
                return;
            }

            // La principal se reinicia desde el comienzo.
            Program.ReiniciarAplicacion();
        }
        //private static void SalirDeSesion()
        //{
        //    // Application.Restart() relanza el exe con los mismos argumentos: en la secundaria
        //    // reabriría otra secundaria, por eso ahí solo se sale.
        //    if (SesionSecundaria.EsSecundaria)
        //        Application.Exit();
        //    else
        //        Application.Restart();
        //}
    }
}