using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Empleado.Rol;
using System;
using System.Drawing;
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
            _rolServicio = new RolServicio();

            if (soloSeleccion)
                MessageBox.Show("Seleccione el rol con doble click");
        }

        #region 🔥 ACCIONES DINAMICAS (si querés migrar botones al lateral nuevo)

        protected override void ConfigurarAccionesPersonalizadas()
        {
            AgregarAccion(
                "Asignar Roles",
                SystemIcons.Shield.ToBitmap(),
                (id) =>
                {
                    var f = new FAsignacionRolesEmpleados(TipoAsignacionRol.Nuevo);
                    f.ShowDialog();
                },
                false // no requiere fila seleccionada
            );
        }

        #endregion

        #region BOTONES BASE

        public override void EjecutarBtnNuevo()
        {
            var f = new FRolABM(TipoOperacion.Nuevo);
            f.ShowDialog();
            ActualizarGrillaBase();
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (!puedeEjecutarComando) return;

            var f = new FRolABM(TipoOperacion.Modificar, entidadID);
            f.ShowDialog();
            ActualizarSegunOperacion(f.RealizoAlgunaOperacion);
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (!puedeEjecutarComando) return;

            var f = new FRolABM(TipoOperacion.Eliminar, entidadID);
            f.ShowDialog();
            ActualizarSegunOperacion(f.RealizoAlgunaOperacion);
        }

        #endregion

        #region GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Contains("RolId"))
            {
                grilla.Columns["RolId"].Visible = false;
                grilla.Columns["RolId"].Name = "Id";
            }

            if (grilla.Columns.Contains("Nombre"))
            {
                grilla.Columns["Nombre"].Visible = true;
                grilla.Columns["Nombre"].HeaderText = "Rol";
                grilla.Columns["Nombre"].Width = 120;
            }

            if (grilla.Columns.Contains("CodigoRol"))
            {
                grilla.Columns["CodigoRol"].Visible = true;
                grilla.Columns["CodigoRol"].Width = 120;
                grilla.Columns["CodigoRol"].HeaderText = "Código";
            }

            if (grilla.Columns.Contains("DetalleRol"))
            {
                grilla.Columns["DetalleRol"].Visible = true;
                grilla.Columns["DetalleRol"].HeaderText = "Detalle";
                grilla.Columns["DetalleRol"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        //public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        //{
        //    base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

        //    if (check.Checked)
        //    {
        //        grilla.DataSource = _rolServicio.ObtenerRolesEliminados(cadenaBuscar);
        //        toolStrip.Enabled = false;
        //    }
        //    else
        //    {
        //        grilla.DataSource = _rolServicio.ObtenerRoles(cadenaBuscar);
        //        toolStrip.Enabled = true;
        //    }
        //}

        #endregion

        #region REFRESH

        private void ActualizarSegunOperacion(bool realizoOperacion)
        {
            if (realizoOperacion)
                ActualizarGrillaBase();
        }

        private void ActualizarGrillaBase()
        {
            //ActualizarDatos(dgvGrilla, txtBuscar?.Text ?? "", cbxEstaEliminado, BarraLateralBotones);
        }

        #endregion

        #region BOTON VIEJO (si lo seguís usando en el form)

        private void btnAsignacionRoles_Click(object sender, EventArgs e)
        {
            var f = new FAsignacionRolesEmpleados(TipoAsignacionRol.Nuevo);
            f.ShowDialog();
        }

        #endregion
    }
}
