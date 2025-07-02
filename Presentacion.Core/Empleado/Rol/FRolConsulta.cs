using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.Empleado.Rol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Empleado.Rol
{
    public partial class FRolConsulta : FBaseConsulta
    {
        private readonly IRolServicio _rolServicio;
        public long? rolSeleccionado = null;
        public bool soloSeleccion;
        public FRolConsulta() : this(new RolServicio())
        {
            InitializeComponent();
            soloSeleccion = false;
        }
        public FRolConsulta(IRolServicio rolServicio)
        {
            _rolServicio = rolServicio;
            soloSeleccion = false;
        }
        public override void EjecutarBtnNuevo()
        {
            var FormularioRolABM = new FRolABM(TipoOperacion.Nuevo);
            FormularioRolABM.ShowDialog();
        }
    }
}
