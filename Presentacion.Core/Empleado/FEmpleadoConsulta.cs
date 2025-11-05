using Presentacion.Core.Empleado.Rol;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Empleado;
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
        private readonly IEmpleadoServicio _empleadoServicio;
        public long? empleadoSeleccionado = null;
        public bool soloSeleccion;
        public bool vieneDeCargaVendedor = false;
        public FEmpleadoConsulta() : this(new EmpleadoServicio())
        {
            InitializeComponent();
            soloSeleccion = false;
        }
        public FEmpleadoConsulta(IEmpleadoServicio empleadoServicio)
        {
            _empleadoServicio = empleadoServicio;
            soloSeleccion = false;
        }
        public FEmpleadoConsulta(bool _vieneDeCargaVendedor) :this(new EmpleadoServicio())
        {
            InitializeComponent();
            this.vieneDeCargaVendedor = _vieneDeCargaVendedor;
            if (vieneDeCargaVendedor)
            {
                btnSeleccionarVendedor.Visible = true;
                btnSeleccionarVendedor.Enabled = true;
            }
            else
            {
                btnSeleccionarVendedor.Visible = false;
                btnSeleccionarVendedor.Enabled = false;
            }
            // this.soloSeleccion = soloSeleccion;
            //if (soloSeleccion) MessageBox.Show("Seleccione el empleado con doble click");
            //_empleadoServicio = new EmpleadoServicio();

        }
        /*  public override void EjecutarDobleClickFila()
          {
              if (soloSeleccion)
              {
                  Console.WriteLine("In ClienteCons");
                  empleadoSeleccionado = (long)entidadID;
                  Close();
              }
          }*/
        public override void EjecutarBtnNuevo()
        {
            var FormularioEmpleadoABM = new FEmpleadoABM(TipoOperacion.Nuevo);
            FormularioEmpleadoABM.ShowDialog();
        }

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);
            grilla.Columns["PersonaId"].Visible = false;
            grilla.Columns["PersonaId"].Name = "Id";

            grilla.Columns["Legajo"].Visible = true;
            grilla.Columns["Legajo"].Width = 80;

            grilla.Columns["Nombre"].Visible = true;
            grilla.Columns["Nombre"].Width = 100;

            grilla.Columns["Apellido"].Visible = true;
            grilla.Columns["Apellido"].Width = 100;

            grilla.Columns["Username"].Visible = true;
            grilla.Columns["Username"].HeaderText = "Usuario";
            grilla.Columns["Username"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            grilla.Columns["DNI"].Visible = true;
            grilla.Columns["DNI"].Width = 100;

            grilla.Columns["Email"].Visible = true;
            grilla.Columns["Email"].Width = 130;

            grilla.Columns["Telefono"].Visible = true;
            grilla.Columns["Telefono"].Width = 100;

            grilla.Columns["EstadoDescripcion"].Visible = true;
            grilla.Columns["EstadoDescripcion"].Width = 100;
            grilla.Columns["EstadoDescripcion"].HeaderText = "Estado";
        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        {
            base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

            if (check.Checked)
            {
                grilla.DataSource = _empleadoServicio.ObtenerEmpleadosEliminados(cadenaBuscar);
                toolStrip.Enabled = false;

            }
            else
            {
                grilla.DataSource = _empleadoServicio.ObtenerEmpleados(cadenaBuscar);
                toolStrip.Enabled = true;
            }
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (puedeEjecutarComando)
            {
                var FormularioABMEmpleado = new FEmpleadoABM(TipoOperacion.Modificar, entidadID);
                FormularioABMEmpleado.ShowDialog();
                ActualizarSegunOperacion(FormularioABMEmpleado.RealizoAlgunaOperacion);
            }
        }
        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (puedeEjecutarComando)
            {
                var FormularioABMEmpleado = new FEmpleadoABM(TipoOperacion.Eliminar, entidadID);
                FormularioABMEmpleado.ShowDialog();
                ActualizarSegunOperacion(FormularioABMEmpleado.RealizoAlgunaOperacion);
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
            base.EjecutarMostrarEliminados();
        }

        private void btnAsignacionRoles_Click(object sender, EventArgs e)
        {
            ControlCargaExistencaDatos();
            if (puedeEjecutarComando)
            {
                EjecutarAsignacionRoles();
            }
        }

        private void EjecutarAsignacionRoles()
        {
            var FormularioAsignacionRolesEmpleados = new FAsignacionRolesEmpleados(TipoAsignacionRol.Existente, entidadID);
            FormularioAsignacionRolesEmpleados.ShowDialog();
            //ActualizarSegunOperacion(FormularioAsignacionRolesEmpleados.RealizoAlgunaOperacion);
        }

        public void ControlCargaExistencaDatos()
        {
            if (dgvGrilla.RowCount > 0)
            {
                if (!entidadID.HasValue)
                {
                    MessageBox.Show("Por favor seleccione un registro.");
                    puedeEjecutarComando = false;
                    return;
                }
                else
                {
                    puedeEjecutarComando = true;
                }
            }
            else
            {
                MessageBox.Show("No hay Datos Cargados.");
            }
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            ControlCargaExistencaDatos();
            if (puedeEjecutarComando)
            {
                var formularioCrearUsuario = new FEmpleadoCrearUsuario(entidadID);
                formularioCrearUsuario.ShowDialog();
            }
        }

        private void btnSeleccionarVendedor_Click(object sender, EventArgs e)
        {
            ControlCargaExistencaDatos();
            if (!puedeEjecutarComando) return;

            empleadoSeleccionado = (long)entidadID;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

