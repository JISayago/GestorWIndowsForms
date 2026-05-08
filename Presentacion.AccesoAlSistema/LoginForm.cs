using Presentacion.FBase;
using ServicioAccesoSistema.AccesoSistema;
using Servicios.Helpers.Sistema.Rol;
using Servicios.LogicaNegocio.Empleado;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.AccesoAlSistema
{
    public partial class LoginForm : FBase.FBase
    {
        private readonly IAccesoSistema _accesoSistema;
        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IUsuarioServicio _usuarioServicio;
        public UsuarioLogeado _usuarioLogeado { get; protected set; }
        public bool PuedeAccederAlSistema { get; protected set; }

        private string pass;
        private string username;


        public LoginForm() : this(new AccesoSistema(), new EmpleadoServicio(), new UsuarioServicio())
        {
            InitializeComponent();
        }
        public LoginForm(IAccesoSistema accesoSistema, IEmpleadoServicio empleadoServicio, IUsuarioServicio usuarioServicio)
        {
            _accesoSistema = accesoSistema;
            _empleadoServicio = empleadoServicio;
            _usuarioServicio = usuarioServicio;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            IngresarAlSistema();
        }

        private void IngresarAlSistema()
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Por favor el usuario y la contraseña son obligatorios",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var username = txtUsuario.Text.Trim();
            var pass = txtPass.Text;


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

                var permisos = _usuarioServicio.ObtenerPermisosPorUsuario((long)estadoUsuario.EntidadId);
            }
            var response = _accesoSistema.LogeoAlSistema(username, pass);

            if (!response.Exitoso)
            {
                MessageBox.Show(response.Mensaje,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var uLogeado = _empleadoServicio.ObtenerEmpleadoPorId((long)response.EntidadId);

            if (uLogeado.PersonaId != null)
            {
                var permisos = _usuarioServicio.ObtenerPermisosPorUsuario((long)response.EntidadId);
                var esSAdmin = _usuarioServicio.EsSuperAdmin((long)response.EntidadId);

                _usuarioLogeado = new UsuarioLogeado
                {
                    PersonaId = uLogeado.PersonaId,
                    Nombre = uLogeado.Nombre,
                    Apellido = uLogeado.Apellido,
                    Username = uLogeado.Username,
                    Permisos = permisos,
                    EsSuperAdmin = esSAdmin
                };

                AuthHelper.UsuarioActual = _usuarioLogeado;
                PuedeAccederAlSistema = true;
            }

            Close();
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

            if (Frec.recuperar && Frec.esAdmin)
            {
                var estadoUsuario = _accesoSistema.ValidarEstadoUsuario(Frec.Usuario);

                if (estadoUsuario.Exitoso)
                {
                    if (estadoUsuario.Mensaje.Contains("recuperación"))
                    {
                        MessageBox.Show(estadoUsuario.Mensaje,
                            "Recuperación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        var X = estadoUsuario.DatoExtra;
                        
                        var form = new FCodigoRecuperacion(estadoUsuario.DatoExtra);
                        form.ShowDialog();
                        return;
                    }

                }
            }
        }

        private void lnklblCodigoRec_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using var f = new FCodigoRecuperacion();
            f.ShowDialog();
        }
    }
}
