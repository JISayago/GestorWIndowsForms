using Presentacion.FBase;
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
        public FEmpleadoConsulta(bool soloSeleccion)
        {
            InitializeComponent();
            this.soloSeleccion = soloSeleccion;
            if (soloSeleccion) MessageBox.Show("Seleccione el empleado con doble click");
            _empleadoServicio = new EmpleadoServicio();
        }

        public override void EjecutarBtnNuevo()
        {
            var FormularioEmpleadoABM = new FEmpleadoABM(TipoOperacion.Nuevo);
            FormularioEmpleadoABM.ShowDialog();
        }

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);
            grilla.Columns["PersonaId"].Visible = true;
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

            grilla.Columns["Estado"].Visible = true;
            grilla.Columns["Estado"].Width = 100;
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
        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            MessageBox.Show($"{respuestaTest}");
            if (puedeEjecutarComando)
            {
                var FormularioABMEmpleado = new FEmpleadoABM(TipoOperacion.Eliminar, entidadID);
                FormularioABMEmpleado.ShowDialog();
                // ActualizarSegunOperacion(FormularioABMEmpleado.RealizoAlgunaOperacion);
            }
        }
        public override void EjecutarMostrarEliminados()
        {
            base.EjecutarMostrarEliminados();
        }


    }
}
