using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Empleado
{
    public partial class FEmpleadoConsulta : FBaseConsulta
    {
        public FEmpleadoConsulta()
        {
            InitializeComponent();
        }

        public override void EjecutarBtnNuevo()
        {
            var FormularioEmpleadoABM = new FEmpleadoABM(TipoOperacion.Nuevo);
            FormularioEmpleadoABM.ShowDialog();
        }
    }
}
