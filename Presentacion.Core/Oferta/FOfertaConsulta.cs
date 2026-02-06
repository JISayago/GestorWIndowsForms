using Presentacion.FBase;
using Servicios.LogicaNegocio.Venta.Oferta;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.Oferta
{
    public partial class FOfertaConsulta : FBaseConsulta
    {
        private readonly IOfertaServicio _ofertaServicio;
        public long? ofertaSeleccionada = null;

        private bool _vieneDeVenta = false;
        private bool activarDesactivar = false;

        public FOfertaConsulta()
        {
            InitializeComponent();
            _ofertaServicio = new OfertaServicio();
        }

        public FOfertaConsulta(bool vieneDeVenta) : this()
        {
            _vieneDeVenta = vieneDeVenta;
            MessageBox.Show("Seleccione la oferta que desea aplicar a la venta.");
        }

        public FOfertaConsulta(bool ActivarDesactivar, string a) : this()
        {
            activarDesactivar = ActivarDesactivar;

            if (activarDesactivar)
            {
                btnSeleccionarParaVenta.Enabled = false;
                btnActivarDesactivarOferta.Visible = true;
                btnActivarDesactivarOferta.Enabled = true;
            }
        }

        #region 🔥 ACCIONES DINAMICAS (si querés migrar botones al lateral base)

        protected override void ConfigurarAccionesPersonalizadas()
        {
            if (activarDesactivar)
            {
                AgregarAccion(
                    "Activar / Desactivar",
                    SystemIcons.Shield.ToBitmap(),
                    (id) =>
                    {
                        if (!id.HasValue)
                        {
                            MessageBox.Show("Seleccione una oferta.");
                            return;
                        }

                        var ofertaAD = _ofertaServicio.ActivarDesactivar(id.Value);

                        if (ofertaAD.OfertaDescuentoId != null)
                            MessageBox.Show($"La oferta {ofertaAD.Codigo} cambió su estado.");
                        else
                            MessageBox.Show("No se pudo cambiar el estado.");

                        ActualizarGrillaBase();
                    },
                    true
                );
            }
        }

        #endregion

        #region BOTONES BASE

        public override void EjecutarBtnNuevo()
        {
            var f = new FSeleccionTipoOferta();
            f.Show();
        }

        #endregion

        #region GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Contains("OfertaDescuentoId"))
            {
                grilla.Columns["OfertaDescuentoId"].Visible = false;
                grilla.Columns["OfertaDescuentoId"].Name = "Id";
            }

            if (grilla.Columns.Contains("Descripcion"))
            {
                grilla.Columns["Descripcion"].Visible = true;
                grilla.Columns["Descripcion"].HeaderText = "Descripción";
                grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grilla.Columns["Descripcion"].FillWeight = 60;
            }

            if (grilla.Columns.Contains("Codigo"))
            {
                grilla.Columns["Codigo"].Visible = true;
                grilla.Columns["Codigo"].HeaderText = "Código";
                grilla.Columns["Codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grilla.Columns["Codigo"].FillWeight = 30;
            }

            if (grilla.Columns.Contains("GrupoNombre"))
            {
                grilla.Columns["GrupoNombre"].Visible = true;
                grilla.Columns["GrupoNombre"].HeaderText = "Grupo";
                grilla.Columns["GrupoNombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grilla.Columns["GrupoNombre"].FillWeight = 30;
            }

            if (grilla.Columns.Contains("Detalle"))
            {
                grilla.Columns["Detalle"].Visible = true;
                grilla.Columns["Detalle"].HeaderText = "Detalle";
            }

            if (grilla.Columns.Contains("PrecioOriginal"))
            {
                grilla.Columns["PrecioOriginal"].Visible = true;
                grilla.Columns["PrecioOriginal"].HeaderText = "Precio Original";
                grilla.Columns["PrecioOriginal"].DefaultCellStyle.Format = "C2";
            }

            if (grilla.Columns.Contains("PrecioFinal"))
            {
                grilla.Columns["PrecioFinal"].Visible = true;
                grilla.Columns["PrecioFinal"].HeaderText = "Precio Final";
                grilla.Columns["PrecioFinal"].DefaultCellStyle.Format = "C2";
            }

            if (grilla.Columns.Contains("DescuentoTotalFinal"))
            {
                grilla.Columns["DescuentoTotalFinal"].Visible = true;
                grilla.Columns["DescuentoTotalFinal"].HeaderText = "Descuento $";
                grilla.Columns["DescuentoTotalFinal"].DefaultCellStyle.Format = "C2";
            }

            if (grilla.Columns.Contains("PorcentajeDescuento"))
            {
                grilla.Columns["PorcentajeDescuento"].Visible = true;
                grilla.Columns["PorcentajeDescuento"].HeaderText = "% Desc.";
                grilla.Columns["PorcentajeDescuento"].DefaultCellStyle.Format = "N2";
            }

            if (grilla.Columns.Contains("FechaInicio"))
            {
                grilla.Columns["FechaInicio"].Visible = true;
                grilla.Columns["FechaInicio"].HeaderText = "Inicio";
                grilla.Columns["FechaInicio"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (grilla.Columns.Contains("FechaFin"))
            {
                grilla.Columns["FechaFin"].Visible = true;
                grilla.Columns["FechaFin"].HeaderText = "Fin";
                grilla.Columns["FechaFin"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (grilla.Columns.Contains("CantidadProductosDentroOferta"))
            {
                grilla.Columns["CantidadProductosDentroOferta"].Visible = true;
                grilla.Columns["CantidadProductosDentroOferta"].HeaderText = "Cant. Productos";
            }

            if (grilla.Columns.Contains("CantidadLimiteDeStock"))
            {
                grilla.Columns["CantidadLimiteDeStock"].Visible = true;
                grilla.Columns["CantidadLimiteDeStock"].HeaderText = "Cant. Máx.";
            }

            if (grilla.Columns.Contains("EstaActiva"))
            {
                grilla.Columns["EstaActiva"].HeaderText = "Estado";
                grilla.Columns["EstaActiva"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                grilla.CellFormatting += (s, e) =>
                {
                    if (e.RowIndex < 0) return;
                    if (grilla.Columns[e.ColumnIndex].Name != "EstaActiva") return;

                    if (e.Value is bool b)
                    {
                        e.Value = b ? "Activa" : "Inactiva";
                        e.FormattingApplied = true;
                    }
                };
            }

            if (grilla.Columns.Contains("EsUnSoloProducto")) grilla.Columns["EsUnSoloProducto"].Visible = false;
            if (grilla.Columns.Contains("esOfertaPorGrupo")) grilla.Columns["esOfertaPorGrupo"].Visible = false;
            if (grilla.Columns.Contains("TieneLimiteDeStock")) grilla.Columns["TieneLimiteDeStock"].Visible = false;

            grilla.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        //public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        //{
        //    base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

        //    if (_vieneDeVenta)
        //    {
        //        if (check.Checked)
        //        {
        //            grilla.DataSource = _ofertaServicio.ObtenerOfertasInactivasCompuesta(cadenaBuscar);
        //            toolStrip.Enabled = false;
        //        }
        //        else
        //        {
        //            grilla.DataSource = _ofertaServicio.ObtenerOfertasActivasCompuestas(cadenaBuscar);
        //            toolStrip.Enabled = true;
        //        }
        //    }
        //    else
        //    {
        //        grilla.DataSource = _ofertaServicio.ObtenerOfertasActivasInactivas(cadenaBuscar);
        //        toolStrip.Enabled = false;
        //    }
        //}

        #endregion

        #region SELECCIONAR PARA VENTA

        private void btnSeleccionarParaVenta_Click(object sender, EventArgs e)
        {
            if (!entidadID.HasValue) return;

            ofertaSeleccionada = entidadID;
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region ACTIVAR / DESACTIVAR

        private void btnActivarDesactivarOferta_Click(object sender, EventArgs e)
        {
            if (!entidadID.HasValue)
            {
                MessageBox.Show("Seleccione una oferta.");
                return;
            }

            var ofertaAD = _ofertaServicio.ActivarDesactivar(entidadID.Value);

            ActualizarGrillaBase();

            if (ofertaAD.OfertaDescuentoId != null)
                MessageBox.Show($"La oferta {ofertaAD.Codigo} cambió su estado.");
            else
                MessageBox.Show("No se pudo cambiar el estado.");
        }

        #endregion

        private void FOfertaConsulta_Load(object sender, EventArgs e)
        {
            cbxEstaEliminado.Text = "Mostrar ofertas inactivas";
        }

        private void ActualizarGrillaBase()
        {
            //ActualizarDatos(dgvGrilla, txtBuscar?.Text ?? "", cbxEstaEliminado, BarraLateralBotones);
        }
    }
}
