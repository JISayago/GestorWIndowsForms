using Presentacion.Core.Marca;
using Presentacion.FormulariosBase.Helpers;

namespace Presentacion
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fmarca = new FMarcaABM(TipoOperacion.Nuevo);
            fmarca.Show();
        }
    }
}
