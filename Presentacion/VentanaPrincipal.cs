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

        private void btnVenta_Click(object sender, EventArgs e)
        {
             var FVenta = new FVenta(_usuarioLogeado.PersonaId);
            FVenta.Show();
        }


    }
}
