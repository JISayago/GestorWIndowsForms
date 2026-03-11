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

            CargarInfo();
        }

        private void CargarInfo()
        {
            CargarInfo1();
            CargarInfo2();
            CargarInfo3();
            CargarInfo4();
        }

        private void CargarInfo4()
        {
            var detalleSistema = _dellesSistema.ObtenerInfoSistema();

            lblTituloSector4.Text = detalleSistema.Titulo;
            lblConenido4.Text = detalleSistema.TextoPrincipal;
            lblConenido42.Text = detalleSistema.TextoSecundario;

        }

        private void CargarInfo3()
        {
            var InfoOferta = new InfoPantallaPrincipal
            {
                Titulo = "INFO 3",
                TextoPrincipal = "Info 3",
                TextoSecundario = "Info 3",
                Tipo = TipoSectorPrincipal.Ofertas
            };
        }

        private void CargarInfo2()
        {
            var InfoOferta = new InfoPantallaPrincipal
            {
                Titulo = "INFO 2",
                TextoPrincipal = "Info 2",
                TextoSecundario = "Info 2",
                Tipo = TipoSectorPrincipal.Ofertas
            };
        }

        private void CargarInfo1()
        {
            var ofertaInfo = _ofertaServicio.ObtenerInfoOferta();

            lblTituloSector1.Text = ofertaInfo.Titulo;
            lblConenido1.Text = ofertaInfo.TextoPrincipal;
            lblConenido12.Text = ofertaInfo.TextoSecundario;
        }

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
    }
}
