using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Venta.TipoPago;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.TipoPago
{
    public partial class FTipoPagoConsulta : FBaseConsulta
    {
        private readonly ITipoPagoServicio _tipoPagoServicio;
        public long? tipoPagoSeleccionado = null;
        public bool soloSeleccion;

        public FTipoPagoConsulta() : this(new TipoPagoServicio())
        {
            InitializeComponent();
            soloSeleccion = false;
        }

        public FTipoPagoConsulta(ITipoPagoServicio tipoPagoServicio)
        {
            _tipoPagoServicio = tipoPagoServicio;
            soloSeleccion = false;
        }

        public FTipoPagoConsulta(bool soloSeleccion)
        {
            InitializeComponent();
            this.soloSeleccion = soloSeleccion;
            _tipoPagoServicio = new TipoPagoServicio();

            if (soloSeleccion)
                MessageBox.Show("Seleccione el tipo de pago con doble click");
        }

        #region 🔥 ACCIONES DINAMICAS (opcional migrar botones acá)

        protected override void ConfigurarAccionesPersonalizadas()
        {
            // ejemplo si mañana querés algo custom
            /*
            AgregarAccion(
                "Acción test",
                SystemIcons.Information.ToBitmap(),
                (id) => { MessageBox.Show("Acción personalizada"); },
                false
            );
            */
        }

        #endregion

        #region BOTONES BASE

        public override void EjecutarBtnNuevo()
        {
            var f = new FTipoPagoABM(TipoOperacion.Nuevo);
            f.ShowDialog();
            ActualizarGrillaBase();
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (!puedeEjecutarComando) return;

            var f = new FTipoPagoABM(TipoOperacion.Modificar, entidadID);
            f.ShowDialog();
            ActualizarSegunOperacion(f.RealizoAlgunaOperacion);
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (!puedeEjecutarComando) return;

            var f = new FTipoPagoABM(TipoOperacion.Eliminar, entidadID);
            f.ShowDialog();
            ActualizarSegunOperacion(f.RealizoAlgunaOperacion);
        }

        #endregion

        #region GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Contains("TipoPagoId"))
            {
                grilla.Columns["TipoPagoId"].Visible = false;
                grilla.Columns["TipoPagoId"].Name = "Id";
            }

            if (grilla.Columns.Contains("Nombre"))
            {
                grilla.Columns["Nombre"].Visible = true;
                grilla.Columns["Nombre"].HeaderText = "Nombre";
                grilla.Columns["Nombre"].Width = 120;
            }

            if (grilla.Columns.Contains("Codigo"))
            {
                grilla.Columns["Codigo"].Visible = true;
                grilla.Columns["Codigo"].HeaderText = "Código";
                grilla.Columns["Codigo"].Width = 120;
            }

            if (grilla.Columns.Contains("Detalle"))
            {
                grilla.Columns["Detalle"].Visible = true;
                grilla.Columns["Detalle"].HeaderText = "Detalle";
                grilla.Columns["Detalle"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        //public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        //{
        //    base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

        //    if (check.Checked)
        //    {
        //        grilla.DataSource = _tipoPagoServicio.ObtenerTipoPagosEliminados(cadenaBuscar);
        //        toolStrip.Enabled = false;
        //    }
        //    else
        //    {
        //        grilla.DataSource = _tipoPagoServicio.ObtenerTipoPagos(cadenaBuscar);
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
    }
}
