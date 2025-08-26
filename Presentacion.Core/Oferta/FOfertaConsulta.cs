using Presentacion.Core.Producto;
using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Venta.Oferta;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Oferta
{
    public partial class FOfertaConsulta : FBaseConsulta
    {
        private readonly IOfertaServicio _ofertaServicio;


        public long? OfertaSeleccionado = null;
        private bool vieneDeCargaProducto = false;
        public FOfertaConsulta() : this(new OfertaServicio())
        {
            InitializeComponent();
        }

        public FOfertaConsulta(IOfertaServicio ofertaServicio)
        {
            _ofertaServicio = ofertaServicio;
        }

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            grilla.Columns["OfertaId"].Visible = false;
            grilla.Columns["OfertaId"].Name = "Id";

            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].HeaderText = "Oferta";

        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        {

            base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

            if (check.Checked)
            {
                grilla.DataSource = _ofertaServicio.ObtenerTodas(cadenaBuscar);
                toolStrip.Enabled = false;
            }
            else
            {
                grilla.DataSource = _ofertaServicio.ObtenerTodas(cadenaBuscar);
                toolStrip.Enabled = true;
            }
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (puedeEjecutarComando)
            {
                var FormularioABMOFerta = new FOfertaABM(TipoOperacion.Eliminar, entidadID);
                FormularioABMOFerta.ShowDialog();
                ActualizarSegunOperacion(FormularioABMOFerta.RealizoAlgunaOperacion);
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
                var FormularioABMOFerta = new FOfertaABM(TipoOperacion.Modificar, entidadID);
                FormularioABMOFerta.ShowDialog();
                ActualizarSegunOperacion(FormularioABMOFerta.RealizoAlgunaOperacion);
            }
        }

        public override void EjecutarBtnNuevo()
        {
            var FormularioABMOFerta = new FOfertaABM(TipoOperacion.Nuevo);
            FormularioABMOFerta.ShowDialog();
            ActualizarSegunOperacion(FormularioABMOFerta.RealizoAlgunaOperacion);
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


    }
}
