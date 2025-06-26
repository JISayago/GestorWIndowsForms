using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.FBase
{
    public partial class FBaseABM : Form
    {
        public FBaseABM()
        {
            InitializeComponent();
        }

        public  void btnEjecutar_Click(object sender, EventArgs e)
        {
            Ejectuar();
        }

        public virtual void Ejectuar()
        {
        }
    }
}
