using Servicios.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Oferta
{
    public partial class FSeleccionTipoOferta : Form
    {
        public FSeleccionTipoOferta()
        {
            InitializeComponent();

        }

        private void btnOfertaPorGrupo_Click(object sender, EventArgs e)
        {
            AbrirFormularioOferta(TipoOferta.Grupo);
        }

        private void btnOfertaCompuesta_Click(object sender, EventArgs e)
        {
            AbrirFormularioOferta(TipoOferta.Compuesta);
        }

        public void AbrirFormularioOferta(TipoOferta tipoOferta)
        {
            var fNuevaOferta = new FOfertaABM(tipoOferta);
            fNuevaOferta.ShowDialog();
            this.Close();
        }
    }
}
