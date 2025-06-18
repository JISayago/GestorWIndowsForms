using ServicioAccesoSistema.AccesoSistema;
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
        public bool PuedeAccederAlSistema { get; protected set; }
        
        private string msge;
        private string pass;
        private string username;


        public LoginForm() : this(new AccesoSistema())
        {
            InitializeComponent();
        }
        public LoginForm(IAccesoSistema accesoSistema)
        {
            _accesoSistema = accesoSistema;
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
                msge = _accesoSistema.LogeoAlSistema(username, pass);
                if (msge != "-")
                {
                    MessageBox.Show(msge, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Bienvenido/a", "Ingreso Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PuedeAccederAlSistema = true;
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
