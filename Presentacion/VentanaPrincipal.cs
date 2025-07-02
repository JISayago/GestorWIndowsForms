using Presentacion.Core.Marca;
using Presentacion.FormulariosBase.Helpers;

namespace Presentacion
{
    public partial class VentanaPrincipal : Form
    {

        public VentanaPrincipal()
        {
            InitializeComponent();

        }

        private void marcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fmarca = new FMarcaConsulta();
            fmarca.Show();
        }
    }
}
