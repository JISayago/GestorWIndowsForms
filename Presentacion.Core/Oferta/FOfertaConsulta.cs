using MigraDoc.DocumentObjectModel.Internals;
using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Servicios.LogicaNegocio.Venta.Oferta;
using System;
using System.Collections.Generic;
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
        }

        #region 🔥 ACCIONES DINAMICAS

        protected override void ConfigurarAccionesPersonalizadas()
        {
            if (activarDesactivar)
            {
                AgregarAccion(
                   "Activar/Desactivar Oferta",
                   Constantes.Imagenes.ImgPerfilUsuario,
                   ActivarDesactivar,
                   true
               );
            }

            if (_vieneDeVenta)
            {
                AgregarAccion(
                   "Seleccionar para venta",
                   Constantes.Imagenes.ImgPerfilUsuario,
                   SeleccionarParaVenta,
                   true
               );
            }
        }

        private void ActivarDesactivar(long? id)
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
        }

        private void SeleccionarParaVenta(long? id)
        {
            if (!id.HasValue) return;

            ofertaSeleccionada = id;
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region BOTONES BASE

        public override void EjecutarBtnNuevo()
        {
            var f = new FSeleccionTipoOferta();
            f.Show();
        }

        #endregion

        #region 🔵 FILTROS NUEVOS (combo + fechas)

        protected override FiltroConsulta ObtenerFiltros()
        {
            var f = base.ObtenerFiltros();

            if (cbxFiltroOpcional?.SelectedItem is OpcionFiltro op)
                f.Extra = op.Valor;

            if (chkUsarFecha != null && chkUsarFecha.Checked)
            {
                f.FechaDesde = dtpDesde.Value.Date;
                f.FechaHasta = dtpHasta.Value.Date;
            }

            return f;
        }


        private void FOfertaConsulta_Load(object sender, EventArgs e)
        {
            cbxEstaEliminado.Text = "Mostrar ofertas inactivas";

            // 🔵 combo columna búsqueda
            var opciones = new List<OpcionFiltro>
            {
                new OpcionFiltro{ Texto="Descripción", Valor="Descripcion"},
                new OpcionFiltro{ Texto="Código", Valor="Codigo"},
                new OpcionFiltro{ Texto="Grupo", Valor="GrupoNombre"}
            };

            ActivarFiltroCombo("Buscar en:", opciones, "Texto", "Valor");

            // 🔵 activar rango fechas
            ActivarFiltroFechas("Filtrar por fecha");
        }

        #endregion

        #region 🔷 GRILLA

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
            }

            if (grilla.Columns.Contains("Codigo"))
            {
                grilla.Columns["Codigo"].Visible = true;
                grilla.Columns["Codigo"].HeaderText = "Código";
            }

            if (grilla.Columns.Contains("GrupoNombre"))
            {
                grilla.Columns["GrupoNombre"].Visible = true;
                grilla.Columns["GrupoNombre"].HeaderText = "Grupo";
            }

            if (grilla.Columns.Contains("FechaInicio"))
            {
                grilla.Columns["FechaInicio"].Visible = true;
                grilla.Columns["FechaInicio"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (grilla.Columns.Contains("FechaFin"))
            {
                grilla.Columns["FechaFin"].Visible = true;
                grilla.Columns["FechaFin"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (grilla.Columns.Contains("EstaActiva"))
            {
                grilla.Columns["EstaActiva"].HeaderText = "Estado";

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
        }

        #endregion

        #region 🔥 ACTUALIZAR DATOS CON FILTROS

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            string columna = filtros.Extra?.ToString() ?? "Descripcion";

            if (_vieneDeVenta)
            {
                if (filtros.VerEliminados)
                {
                    dgv.DataSource = _ofertaServicio.ObtenerOfertasInactivasCompuesta(
                        filtros.TextoBuscar,
                        columna,
                        filtros.FechaDesde,
                        filtros.FechaHasta);
                }
                else
                {
                    dgv.DataSource = _ofertaServicio.ObtenerOfertasActivasCompuestas(
                        filtros.TextoBuscar,
                        columna,
                        filtros.FechaDesde,
                        filtros.FechaHasta);
                }
            }
            else
            {
                dgv.DataSource = _ofertaServicio.ObtenerOfertasActivasInactivas(
                    filtros.TextoBuscar,
                    columna,
                    filtros.FechaDesde,
                    filtros.FechaHasta);
            }
        }

        #endregion
    }
}
