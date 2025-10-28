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
        private bool _vieneDeVenta = false;
        private bool estaActiva = true;
        public FOfertaConsulta()
        {
            InitializeComponent();
            _ofertaServicio = new OfertaServicio();
        }
        public FOfertaConsulta(bool vieneDeOferta) : this()
        {
            _vieneDeVenta = vieneDeOferta;
            MessageBox.Show("Seleccione la oferta que desea aplicar a la venta.");
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
            // Id oculto
            grilla.Columns["OfertaDescuentoId"].Visible = false;
            grilla.Columns["OfertaDescuentoId"].Name = "Id";

            // Descripción: ocupa todo el espacio libre
            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].HeaderText = "Descripción";
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].FillWeight = 60; // ocupa 60% del espacio flexible


            // Código y Grupo: anchos grandes fijos
            grilla.Columns["Codigo"].Visible = true;
            grilla.Columns["Codigo"].HeaderText = "Código";
            grilla.Columns["Codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Codigo"].FillWeight = 30; // ocupa 20%

            grilla.Columns["GrupoNombre"].Visible = true;
            grilla.Columns["GrupoNombre"].HeaderText = "Grupo";
            grilla.Columns["GrupoNombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["GrupoNombre"].FillWeight = 30; // ocupa 20%
            // Detalle
            grilla.Columns["Detalle"].Visible = true;
            grilla.Columns["Detalle"].HeaderText = "Detalle";

            // Resto de columnas
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

            grilla.Columns["CantidadLimiteDeStock"].Visible = true;
            grilla.Columns["CantidadLimiteDeStock"].HeaderText = "Cant. Máx.";

            // Columnas no necesarias a la vista
            grilla.Columns["EstaActiva"].Visible = false;
            grilla.Columns["EsUnSoloProducto"].Visible = false;
            grilla.Columns["esOfertaPorGrupo"].Visible = false;
            grilla.Columns["TieneLimiteDeStock"].Visible = false;

            grilla.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            ToolTip tip = new ToolTip
            {
                BackColor = Color.Yellow,
                ForeColor = Color.Black
            };

            grilla.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    var cell = grilla[e.ColumnIndex, e.RowIndex];
                    if (cell.Value != null)
                    {
                        string texto = cell.Value.ToString();
                        var size = TextRenderer.MeasureText(texto, grilla.Font);
                        if (size.Width > cell.Size.Width)
                            tip.Show(texto, grilla, grilla.PointToClient(Cursor.Position).X + 10, grilla.PointToClient(Cursor.Position).Y + 10, 3000);
                        else
                            tip.Hide(grilla);
                    }
                }
            };

        }



        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        {
            base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

            if (_vieneDeVenta)
            {
                if (check.Checked)
                {
                    grilla.DataSource = _ofertaServicio.ObtenerOfertasInactivasCompuesta(cadenaBuscar);
                    toolStrip.Enabled = false;
                }
                else
                {
                    grilla.DataSource = _ofertaServicio.ObtenerOfertasActivasCompuestas(cadenaBuscar);
                    toolStrip.Enabled = true;
                }
            }
            else
            {
            if (check.Checked)
            {
                grilla.DataSource = _ofertaServicio.ObtenerOfertasInactivas(cadenaBuscar);
                toolStrip.Enabled = false;
            }
            else
            {
                grilla.DataSource = _ofertaServicio.ObtenerOfertasActivas(cadenaBuscar);
                toolStrip.Enabled = true;
            }
            }
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

        private void FOfertaConsulta_Load(object sender, EventArgs e)
        {
            this.cbxEstaEliminado.Text = "Mostrar ofertas inactivas";

        }

    }
}