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
    public partial class FCodigoRecuperacion : Form
    {
        private long _usuarioId;
        private readonly IUsuarioServicio _usuarioServicio;
        public FCodigoRecuperacion(long usuarioId)
        {
            InitializeComponent();
            _usuarioId = usuarioId;
            _usuarioServicio = new UsuarioServicio();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            var resp = _usuarioServicio.ValidarCodigoRecuperacion(_usuarioId, txtCodigoRecuperacion.Text);
            if (resp.Exitoso)
            {
                MessageBox.Show(resp.Mensaje);
                var f = new ActualizarPass(_usuarioId);
                f.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show(resp.Mensaje);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
