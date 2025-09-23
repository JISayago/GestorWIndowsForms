using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.CuentaCorriente;

namespace Presentacion.Core.CuentaCorriente
{
    public partial class FCuentaCorrienteConsulta : FBaseConsulta
    {
        private readonly ICuentaCorrienteServicio _cuentacorrienteServicio;

        public FCuentaCorrienteConsulta() : this(new CuentaCorrienteServicio())
        {
            InitializeComponent();
        }
        public FCuentaCorrienteConsulta(ICuentaCorrienteServicio cuentacorrienteServicio)
        {
            _cuentacorrienteServicio = cuentacorrienteServicio;
        }

        public override void EjecutarBtnNuevo()
        {
            var FormularioCuentaCorrienteABM = new FCuentaCorrienteABM(TipoOperacion.Nuevo);
            FormularioCuentaCorrienteABM.ShowDialog();
        }

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);
            grilla.Columns["CuentaCorrienteId"].Visible = false;
            grilla.Columns["CuentaCorrienteId"].Name = "Id";

            grilla.Columns["NombreCuentaCorriente"].Visible = true;
            grilla.Columns["NombreCuentaCorriente"].Width = 100;
            grilla.Columns["NombreCuentaCorriente"].HeaderText = "Nombre CC";
            
            grilla.Columns["FechaVencimiento"].Visible = true;
            grilla.Columns["FechaVencimiento"].Width = 100;
            grilla.Columns["FechaVencimiento"].HeaderText = "Fecha Vencimiento";

            grilla.Columns["EstadoCuentaCorriente"].Visible = true;
            grilla.Columns["EstadoCuentaCorriente"].Width = 100;
            grilla.Columns["EstadoCuentaCorriente"].HeaderText = "Estado CC";
        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        {
            base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

            if (check.Checked)
            {
                grilla.DataSource = _cuentacorrienteServicio.ObtenerCuentaCorrientesEliminada(cadenaBuscar);
                toolStrip.Enabled = false;

            }
            else
            {
                grilla.DataSource = _cuentacorrienteServicio.ObtenerCuentaCorrientes(cadenaBuscar);
                toolStrip.Enabled = true;
            }
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (puedeEjecutarComando)
            {
                var FormularioABMCuentaCorriente = new FCuentaCorrienteABM(TipoOperacion.Modificar, entidadID);
                FormularioABMCuentaCorriente.ShowDialog();
                ActualizarSegunOperacion(FormularioABMCuentaCorriente.RealizoAlgunaOperacion);
            }
        }
        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (puedeEjecutarComando)
            {
                var FormularioABMCuentaCorriente = new FCuentaCorrienteABM(TipoOperacion.Eliminar, entidadID);
                FormularioABMCuentaCorriente.ShowDialog();
                ActualizarSegunOperacion(FormularioABMCuentaCorriente.RealizoAlgunaOperacion);
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
    }
}
