using Presentacion.Core.Categoria;

namespace Presentacion
{
    public partial class VentanaPrincipal : Form
    {

        public VentanaPrincipal()
        {
            InitializeComponent();

        }

        private void categoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fmarca = new FCategoriaConsulta();

            fmarca.Show();
        }
    }
}
