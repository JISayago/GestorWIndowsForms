using Servicios.LogicaNegocio.Empleado;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.AccesoAlSistema
{
    public partial class FRecuperarContra : FBase.FBase
    {
        public bool recuperar { get; private set; }
        public bool esAdmin { get; private set; }
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
            Nro = txtDniLegajo.Text;
            Usuario = txtUsuario.Text;
            if (string.IsNullOrWhiteSpace(Usuario))
            {
             Informar("Ingrese un usuario.");
                //MessageBox.Show("Ingrese un usuario.");
                return;
            }
            if (string.IsNullOrWhiteSpace(Nro))
            {
                Informar("Ingrese un legajo o DNI.");
                MessageBox.Show("Ingrese un legajo o DNI.");
                return;
            }
            var respuesta = _usuarioServicio.DeshabilitarUsuarioYRecuperarContra(Usuario, Nro);
            Informar(respuesta.Mensaje,"Atención");
            //MessageBox.Show(respuesta.Mensaje);
            recuperar = respuesta.Exitoso;
            esAdmin = respuesta.EntidadId != 0;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FRecuperarContra_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();
        }
    }
}
