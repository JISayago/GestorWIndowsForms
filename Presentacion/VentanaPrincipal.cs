using Presentacion.AccesoAlSistema;
using Presentacion.Core.Administracion;
using Presentacion.Core.Articulo.Marca;
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

namespace Presentacion
{
    public partial class VentanaPrincipal : FBase.FBase
    {
        private readonly UsuarioLogeado _usuarioLogeado;
        private DateTime _fechaActual = DateTime.Now;
        private DateTime _horaActual = DateTime.Now;

        public VentanaPrincipal() : this(new UsuarioLogeado())
        {
        }

        public VentanaPrincipal(UsuarioLogeado usuarioLogeado)
        {
            _usuarioLogeado = usuarioLogeado;
            InitializeComponent();

            // Correcting the issue with Font.Size property  
            var font = new Font(lblNombreUsuario.Font.FontFamily, 12F, FontStyle.Bold);
            lblNombreUsuario.Font = font;
            lblNombreUsuario.ForeColor = Color.DarkGreen;
            lblNombreUsuario.Text = _usuarioLogeado.Username.ToUpper();
        }

        private void categoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var fCategoria = new FCategoriaConsulta();

            fCategoria.Show();
        }

        private void empleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fEmpleado = new FEmpleadoConsulta();
            fEmpleado.Show();

        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FRoles = new FRolConsulta();
            FRoles.Show();
        }

        private void marcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fmarca = new FMarcaConsulta();

            fmarca.Show();
        }

        private void articuloToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var fProducto = new FProductoConsulta();

            fProducto.Show();
        }
        private void tipoPagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fTipoPago = new FTipoPagoConsulta();
            fTipoPago.Show();
        }

        private void rubroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fRubro = new FRubroConsulta();
            fRubro.Show();
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            var FVenta = new FVenta(_usuarioLogeado.PersonaId);
            FVenta.Show();
        }

        private void cuentaCorrienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FCuentaCorriente = new FCuentaCorrienteConsulta();
            FCuentaCorriente.Show();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FCliente = new FClienteConsulta();
            FCliente.Show();
        }

        private void ofertasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*var FOferta = new FOfertaConsulta();
            FOferta.Show();*/
            var FOferta = new FSeleccionTipoOferta();
            FOferta.Show();
        }

        private void cuentaCorrienteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var FCuentaCorriente = new FCuentaCorrienteConsulta();
            FCuentaCorriente.Show();
        }

        private void clienteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var FCliente = new FClienteConsulta();
            FCliente.Show();
        }

        private void activarDesactivarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FActDesac = new FOfertaConsulta(true, "a");
            FActDesac.Show();
        }

        private void movimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FMovimiento = new FMovimientoConsulta();
            FMovimiento.Show();
        }

        private void VentanaPrincipal_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();
            MyTimer.Interval = 1000;
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();
        }
        private void MyTimer_Tick(object sender, EventArgs e)
        {
            lblFechaValor.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblHoraValor.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void btnPanelAdmin_Click(object sender, EventArgs e)
        {
            var administracion = new FAdministracion();
            administracion.Show();
        }
    }
}
