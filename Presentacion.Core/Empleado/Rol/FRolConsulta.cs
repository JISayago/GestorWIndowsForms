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
        public FRolConsulta(bool soloSeleccion)
        {
            InitializeComponent();
            this.soloSeleccion = soloSeleccion;
            if (soloSeleccion) MessageBox.Show("Seleccione el rol con doble click");
            _rolServicio = new RolServicio();
        }
        public override void EjecutarBtnNuevo()
        {
            var FormularioRolABM = new FRolABM(TipoOperacion.Nuevo);
            FormularioRolABM.ShowDialog();
        }
        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);
            grilla.Columns["RolId"].Visible = true;
            grilla.Columns["RolId"].Name = "Id";

            grilla.Columns["Nombre"].Visible = true;
            grilla.Columns["Nombre"].Width = 100;
            
            grilla.Columns["CodigoRol"].Visible = true;
            grilla.Columns["CodigoRol"].Width = 100;
            grilla.Columns["DetalleRol"].HeaderText = "Código";

            grilla.Columns["DetalleRol"].Visible = true;
            grilla.Columns["DetalleRol"].HeaderText = "Detalle";
            grilla.Columns["DetalleRol"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        {
            base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

            if (check.Checked)
            {
                grilla.DataSource = _rolServicio.ObtenerRolesEliminados(cadenaBuscar);
                toolStrip.Enabled = false;

            }
            else
            {
                grilla.DataSource = _rolServicio.ObtenerRoles(cadenaBuscar);
                toolStrip.Enabled = true;
               
            }
        }
        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (puedeEjecutarComando)
            {
                var FormularioRolABM = new FRolABM(TipoOperacion.Modificar, entidadID);
                FormularioRolABM.ShowDialog();
                ActualizarSegunOperacion(FormularioRolABM.RealizoAlgunaOperacion);
            }
        }
        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (puedeEjecutarComando)
            {
                var FormularioRolABM = new FRolABM(TipoOperacion.Eliminar, entidadID);
                FormularioRolABM.ShowDialog();
                ActualizarSegunOperacion(FormularioRolABM.RealizoAlgunaOperacion);
            }
        }
        private void ActualizarSegunOperacion(bool realizoOperacion)
        {
            if (realizoOperacion)
            {
                ActualizarDatos(dgvGrilla, string.Empty, cbxEstaEliminado, BarraLateralBotones);
            }
        }
        public override void EjecutarMostrarEliminados()
        {
        }

    }
}
