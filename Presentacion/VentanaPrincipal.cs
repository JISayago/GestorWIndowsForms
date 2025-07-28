using Presentacion.AccesoAlSistema;
using Presentacion.Core.Articulo.Marca;
using Presentacion.Core.Categoria;
using Presentacion.Core.Empleado;
using Presentacion.Core.Empleado.Rol;
using Presentacion.Core.TipoPago;
using Presentacion.Core.Venta;
using ServicioAccesoSistema.AccesoSistema;

namespace Presentacion
{
    public partial class VentanaPrincipal : FBase.FBase
    {
        private readonly UsuarioLogeado _usuarioLogeado;

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

        private void tipoPagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fTipoPago = new FTipoPagoConsulta();
            fTipoPago.Show();
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            var FVenta = new FVenta(_usuarioLogeado.PersonaId);
            FVenta.Show();
        }
    }
}
