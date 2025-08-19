using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Producto.Rubro;


namespace Presentacion.Core.Producto.Rubro
{
    public partial class FRubroConsulta : FBaseConsulta
    {
        private readonly IRubroServicio _rubroServicio;

        public FRubroConsulta() : this(new RubroServicio())
        {
            InitializeComponent();
        }

        public FRubroConsulta(IRubroServicio rubroServicio)
        {
            _rubroServicio = rubroServicio;
        }

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);


            grilla.Columns["Nombre"].Visible = true;
            grilla.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Nombre"].HeaderText = "Rubro";

        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        {

            base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

            if (check.Checked)
            {
                grilla.DataSource = _rubroServicio.ObtenerRubroEliminado(cadenaBuscar);
                toolStrip.Enabled = false;
            }
            else
            {
                grilla.DataSource = _rubroServicio.ObtenerRubro(cadenaBuscar);
                toolStrip.Enabled = true;
            }
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (puedeEjecutarComando)
            {
                var FormularioABMRubro = new FRubroABM(TipoOperacion.Eliminar, entidadID);
                FormularioABMRubro.ShowDialog();
                ActualizarSegunOperacion(FormularioABMRubro.RealizoAlgunaOperacion);
            }
        }

        private void ActualizarSegunOperacion(bool realizoOperacion)
        {
            if (realizoOperacion)
            {
                ActualizarDatos(dgvGrilla, string.Empty, cbxEstaEliminado, BarraLateralBotones);
            }
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (puedeEjecutarComando)
            {
                var FormularioABMRubro = new FRubroABM(TipoOperacion.Modificar, entidadID);
                FormularioABMRubro.ShowDialog();
                ActualizarSegunOperacion(FormularioABMRubro.RealizoAlgunaOperacion);
            }
        }

        public override void EjecutarBtnNuevo()
        {
            var FormularioABMRubro = new FRubroABM(TipoOperacion.Nuevo);
            FormularioABMRubro.ShowDialog();
            ActualizarSegunOperacion(FormularioABMRubro.RealizoAlgunaOperacion);
        }
    }
}
