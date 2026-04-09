using ServicioAccesoSistema.AccesoSistema;
using Servicios.LogicaNegocio.Empleado;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.AccesoAlSistema
{
    public partial class LoginForm : Form
    {
        private readonly IAccesoSistema _accesoSistema;
        private readonly IEmpleadoServicio _empleadoServicio;
        public UsuarioLogeado _usuarioLogeado { get; protected set; }
        public bool PuedeAccederAlSistema { get; protected set; }

        private string pass;
        private string username;


        public LoginForm() : this(new AccesoSistema(), new EmpleadoServicio())
        {
            InitializeComponent();
        }
        public LoginForm(IAccesoSistema accesoSistema, IEmpleadoServicio empleadoServicio)
        {
            _accesoSistema = accesoSistema;
            _empleadoServicio = empleadoServicio;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            IngresarAlSistema();
        }

        private void IngresarAlSistema()
        {
            if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtPass.Text))
            {
                MessageBox.Show("Por favor el usuario y la contraseña son obligatorios",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            username = txtUsuario.Text;
            pass = txtPass.Text;

            // 🔹 1. Validar estado del usuario
            var estadoUsuario = _accesoSistema.ValidarEstadoUsuario(username);

            if (estadoUsuario.Exitoso)
            {
                // 🔸 Primer ingreso
                if (estadoUsuario.Mensaje.Contains("Primer ingreso"))
                {
                    MessageBox.Show(estadoUsuario.Mensaje,
                        "Acción requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    var form = new ActualizarPass((long)estadoUsuario.EntidadId);
                    form.ShowDialog();
                    return;
                }

                // 🔸 Recuperación de contraseña
                if (estadoUsuario.Mensaje.Contains("5 minutos"))
                {
                    MessageBox.Show(estadoUsuario.Mensaje,
                        "Recuperación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    var form = new FCodigoRecuperacion((long)estadoUsuario.EntidadId);
                    form.ShowDialog();
                    return;
                }
            }

            // 🔹 2. Login normal
            var response = _accesoSistema.LogeoAlSistema(username, pass);

            if (!response.Exitoso)
            {
                MessageBox.Show(response.Mensaje,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 🔹 3. Obtener usuario logueado
            var uLogeado = _empleadoServicio.ObtenerEmpleadoPorId((long)response.EntidadId);

            if (uLogeado.PersonaId != null)
            {
                _usuarioLogeado = new UsuarioLogeado
                {
                    PersonaId = uLogeado.PersonaId,
                    Nombre = uLogeado.Nombre,
                    Apellido = uLogeado.Apellido,
                    Username = uLogeado.Username
                };

                MessageBox.Show(response.Mensaje,
                    "Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PuedeAccederAlSistema = true;
            }

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblPass_Click(object sender, EventArgs e)
        {

        }

        private void lnklblRecuperacionContra_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var Frec = new FRecuperarContra();
            Frec.ShowDialog();
        }
    }
}
