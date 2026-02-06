using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Producto.Rubro;
using System;
using System.Windows.Forms;

namespace Presentacion.Core.Producto.Rubro
{
    public partial class FRubroConsulta : FBaseConsulta
    {
        private readonly IRubroServicio _rubroServicio;
        public long? rubroSeleccionado = null;

        public FRubroConsulta() : this(new RubroServicio())
        {
            InitializeComponent();
        }

        public FRubroConsulta(IRubroServicio rubroServicio)
        {
            _rubroServicio = rubroServicio;
        }

        private void FRubroConsulta_Load(object sender, EventArgs e)
        {
            //ConfigurarFormularioConsulta(
            //    "Consulta de Rubros",
            //    true,   // nuevo
            //    true,   // modificar
            //    true,   // eliminar
            //    false,  // restaurar
            //    false,  // imprimir
            //    false   // exportar
            //);
        }

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Contains("RubroId"))
            {
                grilla.Columns["RubroId"].Visible = false;
                grilla.Columns["RubroId"].Name = "Id";
            }

            grilla.Columns["Nombre"].Visible = true;
            grilla.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Nombre"].HeaderText = "Rubro";
        }

        //public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        //{
        //    base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

        //    if (check.Checked)
        //    {
        //        grilla.DataSource = _rubroServicio.ObtenerRubroEliminado(cadenaBuscar);
        //        toolStrip.Enabled = false;
        //    }
        //    else
        //    {
        //        grilla.DataSource = _rubroServicio.ObtenerRubro(cadenaBuscar);
        //        toolStrip.Enabled = true;
        //    }
        //}

        public override void EjecutarBtnNuevo()
        {
            var form = new FRubroABM(TipoOperacion.Nuevo);
            form.ShowDialog();
            ActualizarSegunOperacion(form.RealizoAlgunaOperacion);
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();

            if (!puedeEjecutarComando)
                return;

            var form = new FRubroABM(TipoOperacion.Modificar, entidadID);
            form.ShowDialog();
            ActualizarSegunOperacion(form.RealizoAlgunaOperacion);
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();

            if (!puedeEjecutarComando)
                return;

            var form = new FRubroABM(TipoOperacion.Eliminar, entidadID);
            form.ShowDialog();
            ActualizarSegunOperacion(form.RealizoAlgunaOperacion);
        }

        private void ActualizarSegunOperacion(bool realizoOperacion)
        {
            if (!realizoOperacion)
                return;

            //ActualizarDatos(dgvGrilla, txtBuscar.Text, cbxEstaEliminado, BarraLateralBotones);
        }

        private void btnRubroSeleccion_Click(object sender, EventArgs e)
        {
            if (entidadID == null)
                return;

            rubroSeleccionado = (long)entidadID;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
