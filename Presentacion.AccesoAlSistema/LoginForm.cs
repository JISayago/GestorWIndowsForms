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
            if (!string.IsNullOrEmpty(txtPass.Text) && !string.IsNullOrEmpty(txtUsuario.Text))
            {
                username = txtUsuario.Text;
                pass = txtPass.Text;
                var response = _accesoSistema.LogeoAlSistema(username, pass);
                if (!response.Exitoso)
                {
                    MessageBox.Show(response.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var uLogeado = _empleadoServicio.ObtenerEmpleadoPorId((long)response.EntidadId);
                    if (uLogeado.PersonaId != null) { 
                    _usuarioLogeado = new UsuarioLogeado
                    {
                        PersonaId = uLogeado.PersonaId,
                        Nombre = uLogeado.Nombre,
                        Apellido = uLogeado.Apellido,
                        Username = uLogeado.Username,
                    };
                    MessageBox.Show(response.Mensaje, "Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PuedeAccederAlSistema = true;
                }
                    this.Close();
    }
            }
            else
            {
                MessageBox.Show("Por favor el usuario y la contraseña son Obligatrorios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
