using Presentacion.Core.Empleado;
using Presentacion.Core.Empleado.Rol;
using Presentacion.Core.Producto;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Empleado;
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
        public long? ofertaSeleccionada = null;
        private bool vieneDeVenta = false;
        public FOfertaConsulta()
        {
            InitializeComponent();
            _ofertaServicio = new OfertaServicio();
        }
        public FOfertaConsulta(bool cargaOfertaCompuesta)
        {
            InitializeComponent();
            _ofertaServicio = new OfertaServicio();
            vieneDeVenta = cargaOfertaCompuesta;
        }

        public override void EjecutarBtnNuevo()
        {
            var FOferta = new FSeleccionTipoOferta();
            FOferta.Show();
        }

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            // Oculto el ID interno
            grilla.Columns["OfertaDescuentoId"].Visible = false;
            grilla.Columns["OfertaDescuentoId"].Name = "Id";

            // Muestra principales
            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].HeaderText = "Descripción";
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            grilla.Columns["EstaActiva"].Visible = true;
            grilla.Columns["EstaActiva"].HeaderText = "Activa";

            grilla.Columns["Detalle"].Visible = true;
            grilla.Columns["Detalle"].HeaderText = "Detalle";

            grilla.Columns["Codigo"].Visible = true;
            grilla.Columns["Codigo"].HeaderText = "Código";


            grilla.Columns["PrecioOriginal"].Visible = true;
            grilla.Columns["PrecioOriginal"].HeaderText = "Precio Original";
            grilla.Columns["PrecioOriginal"].DefaultCellStyle.Format = "C2";

            grilla.Columns["PrecioFinal"].Visible = true;
            grilla.Columns["PrecioFinal"].HeaderText = "Precio Final";
            grilla.Columns["PrecioFinal"].DefaultCellStyle.Format = "C2";

            grilla.Columns["DescuentoTotalFinal"].Visible = true;
            grilla.Columns["DescuentoTotalFinal"].HeaderText = "Descuento $";
            grilla.Columns["DescuentoTotalFinal"].DefaultCellStyle.Format = "C2";

            grilla.Columns["PorcentajeDescuento"].Visible = true;
            grilla.Columns["PorcentajeDescuento"].HeaderText = "% Desc.";
            grilla.Columns["PorcentajeDescuento"].DefaultCellStyle.Format = "N2";

            grilla.Columns["FechaInicio"].Visible = true;
            grilla.Columns["FechaInicio"].HeaderText = "Inicio";
            grilla.Columns["FechaInicio"].DefaultCellStyle.Format = "dd/MM/yyyy";

            grilla.Columns["FechaFin"].Visible = true;
            grilla.Columns["FechaFin"].HeaderText = "Fin";
            grilla.Columns["FechaFin"].DefaultCellStyle.Format = "dd/MM/yyyy";

            grilla.Columns["CantidadProductosDentroOferta"].Visible = true;
            grilla.Columns["CantidadProductosDentroOferta"].HeaderText = "Cant. Productos";


            grilla.Columns["EsUnSoloProducto"].Visible = true;
            grilla.Columns["EsUnSoloProducto"].HeaderText = "1 Producto";

            grilla.Columns["esOfertaPorGrupo"].Visible = true;
            grilla.Columns["esOfertaPorGrupo"].HeaderText = "Por Grupo";

            grilla.Columns["GrupoNombre"].Visible = true;
            grilla.Columns["GrupoNombre"].HeaderText = "Grupo";


            grilla.Columns["TieneLimiteDeStock"].Visible = true;
            grilla.Columns["TieneLimiteDeStock"].HeaderText = "Stock Limitado";

            grilla.Columns["CantidadLimiteDeStock"].Visible = true;
            grilla.Columns["CantidadLimiteDeStock"].HeaderText = "Cant. Máx.";

        }


        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        {
            base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

            grilla.DataSource = _ofertaServicio.ObtenerOfertas(cadenaBuscar);
            toolStrip.Enabled = true;
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
        private void btnSeleccionarParaVenta_Click(object sender, EventArgs e)
        {

            ofertaSeleccionada = (long)entidadID;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}