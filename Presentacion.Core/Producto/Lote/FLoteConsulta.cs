using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Producto.Lote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Producto.Lote
{
    public partial class FLoteConsulta : FBaseConsulta
    {
        private readonly ILoteServicio _LoteServicio;
        public long? LoteSeleccionada = null;
        private bool vieneDeCargaLote = true;

        public FLoteConsulta(bool vieneDeCargaLote = true) : this(new LoteServicio())
        {
            InitializeComponent();
            this.vieneDeCargaLote = vieneDeCargaLote;
        }

        public FLoteConsulta(ILoteServicio LoteServicio)
        {
            _LoteServicio = LoteServicio;
        }

        #region INIT

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            Text = "Consulta de Lotes";
        }

        #endregion

        #region 🔥 ACCIONES PERSONALIZADAS DINAMICAS

        protected override void ConfigurarAccionesPersonalizadas()
        {
            if (vieneDeCargaLote)
            {

                // Seleccion ID Lote
                AgregarAccion(
                    "Seleccionar Lote",
                    SystemIcons.Information.ToBitmap(),
                    SeleccionLote,
                    false
                );
            }
        }

        private void SeleccionLote(long? id)
        {
            if (!id.HasValue) return;

            LoteSeleccionada = entidadID;
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region 🧱 GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Contains("IdLote"))
            {
                grilla.Columns["IdLote"].Visible = true;
                grilla.Columns["IdLote"].HeaderText = "Lote";
                grilla.Columns["IdLote"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            if (filtros.VerEliminados)
            {
                //dgv.DataSource = _LoteServicio.ObtenerLoteEliminada(filtros.TextoBuscar);
                BarraLateralBotones.Enabled = false;
            }
            else
            {
                dgv.DataSource = _LoteServicio.ObtenerLote(filtros.TextoBuscar);
                BarraLateralBotones.Enabled = true;
            }
        }

        #endregion

        #region 🧰 BOTONES BASE

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (!puedeEjecutarComando) return;

            var f = new FGestionStockLotes(TipoOperacion.Eliminar, null, entidadID); // deberia pasar tambien el producto? o directamente del service
            f.ShowDialog();

            //ActualizarSegunOperacion(f.RealizoAlgunaOperacion);
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (!puedeEjecutarComando) return;

            var f = new FGestionStockLotes(TipoOperacion.Modificar, null, entidadID); // deberia pasar tambien el producto? o directamente del service
            f.ShowDialog();

            //ActualizarSegunOperacion(f.RealizoAlgunaOperacion);
        }

        public override void EjecutarBtnNuevo()
        {
            var f = new FGestionStockLotes(TipoOperacion.Nuevo); //Deberia abrir el form de alta lote con el producto del lote seleccionado?
            f.ShowDialog();

            //ActualizarSegunOperacion(f.RealizoAlgunaOperacion);
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
