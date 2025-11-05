using Presentacion.Core.Empleado.Rol;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Empleado.Rol;
using Servicios.LogicaNegocio.Venta.TipoPago;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (soloSeleccion) MessageBox.Show("Seleccione el tipo de pago con doble click");
            _tipoPagoServicio = new TipoPagoServicio();
        }
        public override void EjecutarBtnNuevo()
        {
            var FormularioTipoPagoABM = new FTipoPagoABM(TipoOperacion.Nuevo);
            FormularioTipoPagoABM.ShowDialog();
        }
        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);
            grilla.Columns["TipoPagoId"].Visible = false;
            grilla.Columns["TipoPagoId"].Name = "Id";

            grilla.Columns["Nombre"].Visible = true;
            grilla.Columns["Nombre"].Width = 100;

            grilla.Columns["Codigo"].Visible = true;
            grilla.Columns["Codigo"].Width = 100;
            grilla.Columns["Codigo"].HeaderText = "Código";

            grilla.Columns["Detalle"].Visible = true;
            grilla.Columns["Detalle"].HeaderText = "Detalle";
            grilla.Columns["Detalle"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        {
            base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

            if (check.Checked)
            {
                grilla.DataSource = _tipoPagoServicio.ObtenerTipoPagosEliminados(cadenaBuscar);
                toolStrip.Enabled = false;

            }
            else
            {
                grilla.DataSource = _tipoPagoServicio.ObtenerTipoPagos(cadenaBuscar);
                toolStrip.Enabled = true;

            }
        }
        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (puedeEjecutarComando)
            {
                var FormularioTipoPagoABM = new FTipoPagoABM(TipoOperacion.Modificar, entidadID);
                FormularioTipoPagoABM.ShowDialog();
                ActualizarSegunOperacion(FormularioTipoPagoABM.RealizoAlgunaOperacion);
            }
        }
        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (puedeEjecutarComando)
            {
                var FormularioTipoPagoABM = new FTipoPagoABM(TipoOperacion.Eliminar, entidadID);
                FormularioTipoPagoABM.ShowDialog();
                ActualizarSegunOperacion(FormularioTipoPagoABM.RealizoAlgunaOperacion);
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
