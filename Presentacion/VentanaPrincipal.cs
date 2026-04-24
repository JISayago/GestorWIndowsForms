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
using Servicios.LogicaNegocio.Caja.DTO;
using Servicios.LogicaNegocio.PantallaPrincipal;
using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Producto.Lote;
using Servicios.LogicaNegocio.Sistema;
using Servicios.LogicaNegocio.Venta.Oferta;
using System;
using System.Collections.Generic;
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
        private DatosTurnoDTO _ultimoEstadoTurno;

        #endregion

        #region Constructores

        public VentanaPrincipal() : this(new UsuarioLogeado())
        {
        }

        public VentanaPrincipal(UsuarioLogeado usuarioLogeado)
        {
            InitializeComponent();
            _ofertaServicio = new OfertaServicio();
            _dellesSistema = new DetallesSistemaServicio();
            _accesoSistema = new AccesoSistema();
            _pantallaPrincipalServicio = new PantallaPrincipalServicio();
            _loteServicio = new LoteServicio();
            _usuarioLogeado = usuarioLogeado;

            this.Bounds = Screen.PrimaryScreen.WorkingArea;

            // Formato de nombre de usuario
            var font = new Font(lblNombreUsuario.Font.FontFamily, 12F, FontStyle.Bold);
            lblNombreUsuario.Font = font;
            lblNombreUsuario.ForeColor = Color.DarkGreen;
            lblNombreUsuario.Text = _usuarioLogeado.Username.ToUpper();
        }

        #endregion

        #region Inicialización y Carga (Load)

        private void VentanaPrincipal_Load(object sender, EventArgs e)
        {
            // 1. Configurar Timers
            InicializarTimers();

            // 2. Suscribir eventos de Layout
            flowLayoutNotificaciones.SizeChanged += FlowLayoutNotificaciones_SizeChanged;

            // 3. Inicializar Panel de Turno y Datos
            _panelDatosTurno = new PanelDatosTurno();
            crearPanelDatosAdicionales();

            // 4. Cargar Notificaciones
            crearNotificacionesLotes();
            crearNotificacionesPromociones();
            crearNotificacionesCuentaCorriente();
        }

        private void InicializarTimers()
        {
            // Timer Hora Sistema (Reloj superior)
            System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();
            MyTimer.Interval = 1000;
            MyTimer.Tick += MyTimer_Tick;
            MyTimer.Start();

            // Timer Datos de Caja (Sincronización cada 20s)
            System.Windows.Forms.Timer MyTimerDatos = new System.Windows.Forms.Timer();
            MyTimerDatos.Interval = 20000;
            MyTimerDatos.Tick += MyTimerDatos_Tick;
            MyTimerDatos.Start();
        }

        #endregion

        #region Lógica de Actualización y Datos (Business Logic)

        private void ActualizarInformacionTurno()
        {
            // 1. Obtener datos actualizados del servicio
            var datosNuevos = _pantallaPrincipalServicio.ObtenerActualizarDatosCaja(DatosSistema.CajaId, _ultimoEstadoTurno);

            // 2. Validar cambios (Equals compara los valores del record)
            if (datosNuevos == null || datosNuevos.Equals(_ultimoEstadoTurno))
            {
                return;
            }

            // 3. Si hay cambios, actualizar caché y UI silenciosamente (sin flashear)
            _ultimoEstadoTurno = datosNuevos;
            _panelDatosTurno.ActualizarSoloTextoCaja(datosNuevos);

            Console.WriteLine("UI Actualizada: Se detectaron cambios en el turno.");
        }

        private void crearPanelDatosAdicionales()
        {
            var panelConsultasRapidas = new PanelConsultasRapidas();

            // Carga inicial de datos
            var datosTurno = _pantallaPrincipalServicio.ObtenerDatosTurno(DatosSistema.CajaId, DatosSistema.UsuarioId);
            _ultimoEstadoTurno = datosTurno;

            // Renderizar en tabPage2 usando la instancia global
            _panelDatosTurno.CargarResumenTurno(tabPage2, datosTurno);

            // Renderizar consultas en tabPage1
            panelConsultasRapidas.CargarConsultasRapidas(tabPage1);
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

        private void FlowLayoutNotificaciones_SizeChanged(object sender, EventArgs e)
        {
            flowLayoutNotificaciones.SuspendLayout();
            flowLayoutNotificaciones.AutoScroll = false;
            flowLayoutNotificaciones.HorizontalScroll.Maximum = 0;
            flowLayoutNotificaciones.HorizontalScroll.Visible = false;

            int anchoSeguro = flowLayoutNotificaciones.ClientSize.Width - 35;

            foreach (Control control in flowLayoutNotificaciones.Controls)
            {
                control.Width = anchoSeguro;
                control.Margin = new Padding(control.Margin.Left, control.Margin.Top, 0, control.Margin.Bottom);
            }

            flowLayoutNotificaciones.AutoScroll = true;
            flowLayoutNotificaciones.ResumeLayout();
            flowLayoutNotificaciones.Refresh();
        }

        #endregion

        #region Navegación y Botones

        private void btnPanelAdmin_Click(object sender, EventArgs e)
        {
            var administracion = new FAdministracion(_usuarioLogeado.PersonaId);
            administracion.Show();
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            var FVenta = new FVenta(_usuarioLogeado.PersonaId);
            FVenta.Show();
        }

        private void btnCaja_Click(object sender, EventArgs e)
        {
            var fCaja = new FCaja();
            fCaja.Show();
        }

        private void btnContraVenta_Click(object sender, EventArgs e)
        {
            var NroCompr = new FNroComprobanteParaCancelacion(_usuarioLogeado.PersonaId);
            NroCompr.Show();
        }

        private void llbCerrarSesion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var respuesta = _accesoSistema.CerrarSesion(_usuarioLogeado.PersonaId);
            if (!respuesta.Exitoso)
            {
                MessageBox.Show(respuesta.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(respuesta.Mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        #endregion

        #region Generación de Notificaciones

        private void crearNotificacionesLotes()
        {
            var notiProdVencidos = new NotificationGroupBox();
            notiProdVencidos.Width = flowLayoutNotificaciones.Width - 25;
            flowLayoutNotificaciones.Controls.Add(notiProdVencidos);

            var listaLotesNotificar = mockDatosNotificaciones("Lote");
            notiProdVencidos.SetData(listaLotesNotificar, "Lotes Vencidos");
        }

        private void crearNotificacionesPromociones()
        {
            var notifOferVencidos = new NotificationGroupBox();
            notifOferVencidos.Width = flowLayoutNotificaciones.Width - 25;
            flowLayoutNotificaciones.Controls.Add(notifOferVencidos);

            var listaOfertasVencidas = mockDatosNotificaciones("Oferta");
            notifOferVencidos.SetData(listaOfertasVencidas, "Ofertas Vencidas");
        }

        private void crearNotificacionesCuentaCorriente()
        {
            var notifCuentasCorrientesVencidas = new NotificationGroupBox();
            notifCuentasCorrientesVencidas.Width = flowLayoutNotificaciones.Width - 25;
            flowLayoutNotificaciones.Controls.Add(notifCuentasCorrientesVencidas);

            var listaCuentasCorrientes = mockDatosNotificaciones("CtaCte");
            notifCuentasCorrientesVencidas.SetData(listaCuentasCorrientes, "Cuentas Corrientes Vencidas");
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
    }
}