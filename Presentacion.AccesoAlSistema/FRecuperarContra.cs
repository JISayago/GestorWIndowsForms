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
    public partial class FRecuperarContra : Form
    {
        public string Nro { get; private set; }
        public string Usuario { get; private set; }
        private readonly IUsuarioServicio _usuarioServicio;
        public FRecuperarContra()
        {
            InitializeComponent();
            _usuarioServicio = new UsuarioServicio();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRecuperar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDniLegajo.Text))
            {
                MessageBox.Show("Ingrese un legajo o DNI.");
                return;
            }

            Nro = txtDniLegajo.Text;
            Usuario = txtUsuario.Text;
            if (string.IsNullOrWhiteSpace(Usuario))
            {
                MessageBox.Show("Ingrese un usuario.");
                return;
            }
            if (string.IsNullOrWhiteSpace(Nro))
            {
                MessageBox.Show("Ingrese un legajo o DNI.");
                return;
            }
            var respuesta = _usuarioServicio.DeshabilitarUsuarioYRecuperarContra(Usuario,Nro);
            if (!respuesta.Exitoso)
            {
                MessageBox.Show(respuesta.Mensaje);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
