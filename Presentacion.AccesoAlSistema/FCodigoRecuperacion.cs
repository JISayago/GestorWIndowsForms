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
        public FCodigoRecuperacion(long usuarioId)
        {
            InitializeComponent();
            _usuarioId = usuarioId;
        }
    }
}
