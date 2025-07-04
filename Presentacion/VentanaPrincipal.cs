using Presentacion.AccesoAlSistema;
using Presentacion.Core.Categoria;
using Presentacion.Core.Empleado;
using Presentacion.Core.Empleado.Rol;
using ServicioAccesoSistema.AccesoSistema;

namespace Presentacion
{
    public partial class VentanaPrincipal : Form
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
            var fmarca = new FCategoriaConsulta();
            fmarca.Show();
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
    }
}
