using Presentacion.FBase;
using Presentacion.FBase.Helpers;
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
        private bool vieneDeCargaRubro = true;

        public FRubroConsulta(bool vieneDeCargaRubro = true) : this(new RubroServicio())
        {
            InitializeComponent();
            this.vieneDeCargaRubro = vieneDeCargaRubro;
        }

        public FRubroConsulta(IRubroServicio rubroServicio)
        {
            _rubroServicio = rubroServicio;
        }

        private void FRubroConsulta_Load(object sender, EventArgs e)
        {
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

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            if (filtros.VerEliminados)
            {
                dgv.DataSource = _rubroServicio.ObtenerRubroEliminado(filtros.TextoBuscar);
                BarraLateralBotones.Enabled = false;
            }
            else
            {
                dgv.DataSource = _rubroServicio.ObtenerRubro(filtros.TextoBuscar);
                BarraLateralBotones.Enabled = true;
            }
        }

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
        #region 🔵 ACCIONES DINÁMICAS EXTRA

        protected override void ConfigurarAccionesPersonalizadas()
        {
            // BOTON Seleccionar
            if (vieneDeCargaRubro)
            {
                AgregarAccion(
                    "Seleccionar Rubro",
                    Constantes.Imagenes.ImgPerfilUsuario,
                    SeleccionRubro,
                    true
                );
            }

        }


        private void SeleccionRubro(long? id)
        {
            if (entidadID == null)
                return;

            rubroSeleccionado = (long)entidadID;
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion
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
