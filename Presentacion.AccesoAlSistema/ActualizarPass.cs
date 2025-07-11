using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.Empleado.DTO;
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
    public partial class ActualizarPass : Form
    {
        private readonly IUsuarioServicio _usuarioServicio;
        public UsuarioDTO _usuario;
        public ActualizarPass()
        {
            InitializeComponent();
        }
        public ActualizarPass(long EntidadId) : this()
        {
            _usuarioServicio = new UsuarioServicio();
            _usuario = _usuarioServicio.ObtenerUsuarioPorId(EntidadId);
            if (_usuario == null)
            {
                MessageBox.Show("Error al obtener el usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardarNuevaPass_Click(object sender, EventArgs e)
        {
            GuardarNuevaPass();
        }
        public void GuardarNuevaPass()
        {
            if (!string.IsNullOrEmpty(txtNuevaPass.Text) && !string.IsNullOrEmpty(txtConfirmarPass.Text))
            {
                if (txtNuevaPass.Text.ToLower() == txtConfirmarPass.Text.ToLower())
                {
                    var response = _usuarioServicio.ActualziarPassPrimerIngreso(_usuario.PersonaId, txtNuevaPass.Text);
                    if (!response.Exitoso)
                    {
                        MessageBox.Show(response.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Contraseña actualizada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Las contraseñas no coinciden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar una contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}