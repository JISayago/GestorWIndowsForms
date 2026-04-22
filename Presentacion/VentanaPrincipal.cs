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
using ServicioAccesoSistema.AccesoSistema;
using Servicios.LogicaNegocio.PantallaPrincipal;
using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Producto.Lote;
using Servicios.LogicaNegocio.Sistema;
using Servicios.LogicaNegocio.Venta.Oferta;

namespace Presentacion
{
    public partial class VentanaPrincipal : FBase.FBase
    {
        private readonly UsuarioLogeado _usuarioLogeado;
        private readonly IOfertaServicio _ofertaServicio;
        private readonly IDetallesSistemaServicio _dellesSistema;
        private readonly IAccesoSistema _accesoSistema;
        private readonly IPantallaPrincipalServicio _pantallaPrincipalServicio;
        private readonly ILoteServicio _loteServicio;
        private DateTime _fechaActual = DateTime.Now;
        private DateTime _horaActual = DateTime.Now;

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

            // Correcting the issue with Font.Size property  
            var font = new Font(lblNombreUsuario.Font.FontFamily, 12F, FontStyle.Bold);
            lblNombreUsuario.Font = font;
            lblNombreUsuario.ForeColor = Color.DarkGreen;
            lblNombreUsuario.Text = _usuarioLogeado.Username.ToUpper();
        }

        private void VentanaPrincipal_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();
            MyTimer.Interval = 1000;
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();

            flowLayoutNotificaciones.SizeChanged += FlowLayoutNotificaciones_SizeChanged;

            crearNotificacionesLotes();
            crearNotificacionesPromociones();
            crearNotificacionesCuentaCorriente();

            //CargarInfo();
        }
        private void FlowLayoutNotificaciones_SizeChanged(object sender, EventArgs e)
        {
            flowLayoutNotificaciones.SuspendLayout();

            // 1. Desactivamos el scroll horizontal explícitamente antes de recalcular
            flowLayoutNotificaciones.AutoScroll = false;
            flowLayoutNotificaciones.HorizontalScroll.Maximum = 0;
            flowLayoutNotificaciones.HorizontalScroll.Visible = false;

            // 2. Calculamos el ancho restando un margen mayor (35px) 
            // Esto asegura espacio para la barra vertical sin empujar el borde derecho.
            int anchoSeguro = flowLayoutNotificaciones.ClientSize.Width - 35;

            foreach (Control control in flowLayoutNotificaciones.Controls)
            {
                // Forzamos el ancho y eliminamos márgenes laterales que puedan estorbar
                control.Width = anchoSeguro;
                control.Margin = new Padding(control.Margin.Left, control.Margin.Top, 0, control.Margin.Bottom);
            }

            // 3. Reactivamos el scroll general y refrescamos
            flowLayoutNotificaciones.AutoScroll = true;
            flowLayoutNotificaciones.ResumeLayout();
            flowLayoutNotificaciones.Refresh();
        }

        //private void CargarInfo()
        //{
        //    CargarInfo1();
        //    CargarInfo2();
        //    CargarInfo3();
        //    CargarInfo4();
        //}

        //private void CargarInfo4()
        //{
        //    var detalleSistema = _dellesSistema.ObtenerInfoSistema();

        //    lblTituloSector4.Text = detalleSistema.Titulo;
        //    lblConenido4.Text = detalleSistema.TextoPrincipal;
        //    lblConenido42.Text = detalleSistema.TextoSecundario;

        //}

        //private void CargarInfo3()
        //{
        //    var InfoOferta = new InfoPantallaPrincipal
        //    {
        //        Titulo = "INFO 3",
        //        TextoPrincipal = "Info 3",
        //        TextoSecundario = "Info 3",
        //        Tipo = TipoSectorPrincipal.Ofertas
        //    };
        //}

        //private void CargarInfo2()
        //{
        //    var InfoOferta = new InfoPantallaPrincipal
        //    {
        //        Titulo = "INFO 2",
        //        TextoPrincipal = "Info 2",
        //        TextoSecundario = "Info 2",
        //        Tipo = TipoSectorPrincipal.Ofertas
        //    };
        //}

        //private void CargarInfo1()
        //{
        //    var ofertaInfo = _ofertaServicio.ObtenerInfoOferta();

        //    lblTituloSector1.Text = ofertaInfo.Titulo;
        //    lblConenido1.Text = ofertaInfo.TextoPrincipal;
        //    lblConenido12.Text = ofertaInfo.TextoSecundario;
        //}

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            lblFechaValor.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblHoraValor.Text = DateTime.Now.ToString("HH:mm:ss");
        }

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
            else
            {
                MessageBox.Show(respuesta.Mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();

        }

        private void crearNotificacionesLotes()
        {
            var listaLotesNotificar= _pantallaPrincipalServicio.notifiacionesProductosVencidos(0);

            var notiProdVencidos = new NotificationGroupBox();
            notiProdVencidos.Width = flowLayoutNotificaciones.Width - 25;

            flowLayoutNotificaciones.Controls.Add(notiProdVencidos);

            //Datos dummys, borrar al implementar el servicio
            listaLotesNotificar = mockDatosNotificaciones("Lote");

            notiProdVencidos.SetData(listaLotesNotificar, "Lotes Vencidos");
        }

        private void crearNotificacionesPromociones()
        {
            var listaOfertasVencidas = _pantallaPrincipalServicio.notifiacionesOfertasVencidas(7);

            var notifOferVencidos = new NotificationGroupBox();
            notifOferVencidos.Width = flowLayoutNotificaciones.Width - 25;

            flowLayoutNotificaciones.Controls.Add(notifOferVencidos);

            //Datos dummys, borrar al implementar el servicio
            listaOfertasVencidas = mockDatosNotificaciones("Oferta");

            notifOferVencidos.SetData(listaOfertasVencidas, "Ofertas Vencidas");
            //notifOferVencidos.SetDataGrid(listaOfertasVencidas, "Ofertas Vencidas"); SE MUESTRAN LOS DATOS EN UN DATAGRID

        }

        private void crearNotificacionesCuentaCorriente()
        {
            var listaCuentasCorrientes = _pantallaPrincipalServicio.notifiacionesCtaCteVencidas(0);

            var notifCuentasCorrientesVencidas = new NotificationGroupBox();
            notifCuentasCorrientesVencidas.Width = flowLayoutNotificaciones.Width - 25;

            flowLayoutNotificaciones.Controls.Add(notifCuentasCorrientesVencidas);

            //Datos dummys, borrar al implementar el servicio
            listaCuentasCorrientes = mockDatosNotificaciones("CtaCte");

            notifCuentasCorrientesVencidas.SetData(listaCuentasCorrientes, "Cuentas Corrientes Vencidas");
        }

        public List<NotificacionPP> mockDatosNotificaciones(string tipoDato)
        {
            var listaLotesNotificar = new List<NotificacionPP>
            {
                new NotificacionPP { NotificacionId = 1, Titulo = "Producto: Leche Entera (URGENTE)", Descripcion = "Lote: L-001 venció hace 2 días (20/04/2026).", FechaNotificacion = DateTime.Now.AddMinutes(-60), Leida = false, NivelUrgencia = 1 },
                new NotificacionPP { NotificacionId = 2, Titulo = "Producto: Yogurt Frutilla (PRÓXIMO)", Descripcion = "Lote: L-002 vence en 2 días (24/04/2026).", FechaNotificacion = DateTime.Now.AddMinutes(-30), Leida = false, NivelUrgencia = 2 },
                new NotificacionPP { NotificacionId = 3, Titulo = "Producto: Arroz (AL DÍA)", Descripcion = "Lote: L-003 vence en 15 días (07/05/2026).", FechaNotificacion = DateTime.Now, Leida = false, NivelUrgencia = 3 },
                new NotificacionPP { NotificacionId = 4, Titulo = "Producto: Queso Crema", Descripcion = "Lote: L-009 VENCIDO. Retirar de góndola inmediatamente.", FechaNotificacion = DateTime.Now.AddDays(-1), Leida = false, NivelUrgencia = 1 },
                new NotificacionPP { NotificacionId = 5, Titulo = "Producto: Manteca 200g", Descripcion = "Lote: L-102 vence mañana. Considerar liquidación.", FechaNotificacion = DateTime.Now.AddHours(-5), Leida = true, NivelUrgencia = 2 },
                new NotificacionPP { NotificacionId = 6, Titulo = "Producto: Fideos Tallarín", Descripcion = "Lote: L-505. Vencimiento lejano (Diciembre 2026).", FechaNotificacion = DateTime.Now.AddDays(-2), Leida = true, NivelUrgencia = 3 }
            };

                    var listaOfertasVencidas = new List<NotificacionPP>
            {
                new NotificacionPP { NotificacionId = 101, Titulo = "Oferta Vencida (ALTA)", Descripcion = "La oferta Verano 2026 ya expiró.", FechaNotificacion = DateTime.Now.AddDays(-1), Leida = false, NivelUrgencia = 1 },
                new NotificacionPP { NotificacionId = 102, Titulo = "Oferta por Vencer (MEDIA)", Descripcion = "La oferta Fin de Semana vence mañana.", FechaNotificacion = DateTime.Now, Leida = false, NivelUrgencia = 2 },
                new NotificacionPP { NotificacionId = 103, Titulo = "Oferta Vigente (BAJA)", Descripcion = "La oferta Mensual vence en 20 días.", FechaNotificacion = DateTime.Now.AddHours(-2), Leida = true, NivelUrgencia = 3 },
                new NotificacionPP { NotificacionId = 104, Titulo = "Liquidación de Stock: Bebidas", Descripcion = "Finalizó el tiempo de la promo 4x3 en gaseosas.", FechaNotificacion = DateTime.Now.AddDays(-3), Leida = false, NivelUrgencia = 1 },
                new NotificacionPP { NotificacionId = 105, Titulo = "Descuento Empleados", Descripcion = "Actualización de cupos para el mes de Mayo.", FechaNotificacion = DateTime.Now, Leida = false, NivelUrgencia = 3 }
            };

                    var listaCuentasCorrientes = new List<NotificacionPP>
            {
                new NotificacionPP { NotificacionId = 501, Titulo = "Cta Cte: Distribuidora X", Descripcion = "Saldo muy atrasado. Vencimiento: 15/04/2026.", FechaNotificacion = DateTime.Now.AddDays(-5), Leida = false, NivelUrgencia = 1 },
                new NotificacionPP { NotificacionId = 502, Titulo = "Cta Cte: Almacén Y", Descripcion = "Factura por vencer hoy (22/04/2026).", FechaNotificacion = DateTime.Now, Leida = false, NivelUrgencia = 2 },
                new NotificacionPP { NotificacionId = 503, Titulo = "Cta Cte: Kiosco Z", Descripcion = "Vencimiento programado para la próxima semana.", FechaNotificacion = DateTime.Now.AddDays(-1), Leida = false, NivelUrgencia = 3 },
                new NotificacionPP { NotificacionId = 504, Titulo = "Cta Cte: Supermercado 'El Sol'", Descripcion = "Deuda prejudicial. El cliente ha superado el límite de crédito permitido.", FechaNotificacion = DateTime.Now.AddDays(-7), Leida = false, NivelUrgencia = 1 },
                new NotificacionPP { NotificacionId = 505, Titulo = "Cta Cte: Carnicería Don Pepe", Descripcion = "Pago parcial recibido. Resta abonar el 50% de la factura actual.", FechaNotificacion = DateTime.Now.AddHours(-10), Leida = true, NivelUrgencia = 2 },
                new NotificacionPP { NotificacionId = 506, Titulo = "Cta Cte: Minimarket Centro", Descripcion = "Aviso de cortesía: Su factura vence en 10 días.", FechaNotificacion = DateTime.Now.AddDays(-2), Leida = false, NivelUrgencia = 3 }
            };

            return tipoDato switch
            {
                "Lote" => listaLotesNotificar,
                "Oferta" => listaOfertasVencidas,
                "CtaCte" => listaCuentasCorrientes,
                _ => new List<NotificacionPP>()
            };
        }
    }
}
