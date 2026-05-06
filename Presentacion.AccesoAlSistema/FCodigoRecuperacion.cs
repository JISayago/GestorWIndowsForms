using Servicios.LogicaNegocio.Empleado;
using System;
using System.Windows.Forms;

namespace Presentacion.AccesoAlSistema
{
    public partial class FCodigoRecuperacion : Form
    {
        private readonly IUsuarioServicio _usuarioServicio;

        public FCodigoRecuperacion()
        {
            InitializeComponent();
            _usuarioServicio = new UsuarioServicio();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            var username = txtUsuario.Text.Trim();
            var codigo = txtCodigoRecuperacion.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(codigo))
            {
                MessageBox.Show("Debe ingresar usuario y código.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var resp = _usuarioServicio.ValidarCodigoRecuperacion(username, codigo);

            if (!resp.Exitoso)
            {
                MessageBox.Show(resp.Mensaje,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(resp.Mensaje,
                "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var f = new ActualizarPass((long)resp.EntidadId);
            f.ShowDialog();

            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}