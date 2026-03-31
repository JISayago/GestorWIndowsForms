using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Articulo.Marca;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.Articulo.Marca
{
    public partial class FMarcaConsulta : FBaseConsulta
    {
        private readonly IMarcaServicio _marcaServicio;
        public long? marcaSeleccionada = null;
        private bool vieneDeCargaMarca = true;

        public FMarcaConsulta(bool vieneDeCargaMarca = true) : this(new MarcaServicio())
        {
            InitializeComponent();
            this.vieneDeCargaMarca = vieneDeCargaMarca;
        }

        public FMarcaConsulta(IMarcaServicio marcaServicio)
        {
            _marcaServicio = marcaServicio;
        }

        #region INIT

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            Text = "Consulta de Marcas";
        }

        #endregion

        #region 🔥 ACCIONES PERSONALIZADAS DINAMICAS

        protected override void ConfigurarAccionesPersonalizadas()
        {
            if (vieneDeCargaMarca)
            {

            // Seleccion ID marca
            AgregarAccion(
                "Seleccionar Marca",
                SystemIcons.Information.ToBitmap(),
                SeleccionMarca,
                false
            );
            }
        }

        private void SeleccionMarca(long? id)
        {
            if (!id.HasValue) return;

            marcaSeleccionada = entidadID;
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region 🧱 GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Contains("Nombre"))
            {
                grilla.Columns["Nombre"].Visible = true;
                grilla.Columns["Nombre"].HeaderText = "Marca";
                grilla.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            if (filtros.VerEliminados)
            {
                dgv.DataSource = _marcaServicio.ObtenerMarcaEliminada(filtros.TextoBuscar);
                BarraLateralBotones.Enabled = false;
            }
            else
            {
                dgv.DataSource = _marcaServicio.ObtenerMarca(filtros.TextoBuscar);
                BarraLateralBotones.Enabled = true;
            }
        }

        #endregion

        #region 🧰 BOTONES BASE

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (!puedeEjecutarComando) return;

            var f = new FMarcaABM(TipoOperacion.Eliminar, entidadID);
            f.ShowDialog();

            ActualizarSegunOperacion(f.RealizoAlgunaOperacion);
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (!puedeEjecutarComando) return;

            var f = new FMarcaABM(TipoOperacion.Modificar, entidadID);
            f.ShowDialog();

            ActualizarSegunOperacion(f.RealizoAlgunaOperacion);
        }

        public override void EjecutarBtnNuevo()
        {
            var f = new FMarcaABM(TipoOperacion.Nuevo);
            f.ShowDialog();

            ActualizarSegunOperacion(f.RealizoAlgunaOperacion);
        }

        private void ActualizarSegunOperacion(bool realizoOperacion)
        {
            //if (realizoOperacion)
            //    btnActualizar_Click_Base();
        }

        #endregion

        #region 🎯 SELECCIONAR


        #endregion
    }
}
