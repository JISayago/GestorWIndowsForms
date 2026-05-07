using MigraDoc.DocumentObjectModel.Internals;
using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Servicios.Helpers.Producto;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.Helpers.Venta.Oferta;
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
            var f = new FOfertaGrupoABM();
            f.Show();
        }

        #endregion

        #region 🔵 FILTROS NUEVOS (combo + fechas)

        //protected override FiltroConsulta ObtenerFiltros()
        //{
        //    var f = base.ObtenerFiltros();

        //    if (cbx1?.SelectedItem is OpcionFiltro op)
        //        f.Extra = op.Valor;

        //    if (chkUsarFecha != null && chkUsarFecha.Checked)
        //    {
        //        f.FechaDesde = dtpDesde.Value.Date;
        //        f.FechaHasta = dtpHasta.Value.Date;
        //    }

        //    return f;
        //}


        private void FOfertaConsulta_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region 🔷 GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Count == 0)
                return;

            // 🔹 ID
            if (grilla.Columns.Contains("OfertaDescuentoId"))
            {
                grilla.Columns["OfertaDescuentoId"].Visible = false;
                grilla.Columns["OfertaDescuentoId"].Name = "Id";
            }

            // 🔹 Descripción
            if (grilla.Columns.Contains("Descripcion"))
            {
                grilla.Columns["Descripcion"].Visible = true;
                grilla.Columns["Descripcion"].HeaderText = "Descripción";
                grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            // 🔹 Código
            if (grilla.Columns.Contains("Codigo"))
            {
                grilla.Columns["Codigo"].Visible = true;
                grilla.Columns["Codigo"].HeaderText = "Código";
            }

            // 🔹 Grupo
            if (grilla.Columns.Contains("GrupoNombre"))
            {
                grilla.Columns["GrupoNombre"].Visible = true;
                grilla.Columns["GrupoNombre"].HeaderText = "Grupo";
            }

            // 🔹 Fechas
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

            // 🔹 Estado
            if (grilla.Columns.Contains("EstaActiva"))
            {
                grilla.Columns["EstaActiva"].Visible = true;
                grilla.Columns["EstaActiva"].HeaderText = "Estado";
                //grilla.CellFormatting += (s, e) => 
                //{ 
                //    if (e.RowIndex < 0) return;
                //    if (grilla.Columns[e.ColumnIndex].Name != "EstaActiva") return;
                //    if (e.Value is bool b)
                //    { e.Value = b ? "Activa" : "Inactiva"; 
                //        e.FormattingApplied = true; }
                //};
            }


            //    // 🔴 LIMPIAR EVENTO ANTES DE VOLVER A ASIGNAR
            //    grilla.CellFormatting -= FormatearEstadoOferta;
            //    grilla.CellFormatting += FormatearEstadoOferta;
        }
        //private void FormatearEstadoOferta(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    var grilla = (DataGridView)sender;

        //    if (e.RowIndex < 0) return;
        //    if (!grilla.Columns.Contains("EstaActiva")) return;
        //    if (grilla.Columns[e.ColumnIndex].Name != "EstaActiva") return;

        //    if (e.Value is bool b)
        //    {
        //        e.Value = b ? "Activa" : "Inactiva";
        //        e.FormattingApplied = true;
        //    }
        //}

        #endregion

        //#region 🔥 ACTUALIZAR DATOS CON FILTROS

        //public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        //{
        //    base.ActualizarDatos(dgv, filtros);

        //    filtros.Extra ??= "Descripcion";

        //    var resultado = _ofertaServicio.ObtenerOfertas(filtros, _vieneDeVenta);

        //    dgv.DataSource = resultado.Items;

        //    ResetearGrilla(dgv);

        //    var paginacion = new DatosPaginacion
        //    {
        //        PaginaActual = resultado.Page,
        //        PageSize = resultado.PageSize,
        //        CantidadRegistros = resultado.TotalRegistros,
        //    };

        //    ActualizarPaginacionUI(paginacion);

        //    BarraLateralBotones.Enabled = !filtros.VerEliminados;
        //}

        //#endregion

        //protected override void ConfigurarFiltrosUI()
        //{

        //    base.ConfigurarFiltrosUI();

        //    ActivarFiltroEliminados("Mostrar productos eliminados.");

        //    var opciones = new List<OpcionFiltro>
        //    {
        //        new OpcionFiltro { Texto = "Todos", Valor = "" },
        //        new OpcionFiltro { Texto = "Codigo", Valor = "Codigo" },
        //        new OpcionFiltro { Texto = "Descripción", Valor = "Descripcion" },
        //        new OpcionFiltro { Texto = "Detalle", Valor = "Detalle" },
        //        new OpcionFiltro { Texto = "Nombre de Grupo ", Valor = "GrupoNombre" },
        //        new OpcionFiltro { Texto = "Marca ", Valor = "NombreMarca" },
        //        new OpcionFiltro { Texto = "Rubro", Valor = "NombreRubro" },
        //        new OpcionFiltro { Texto = "Categoria", Valor = "NombreCategoria" },
        //    };

        //    ActivarFiltroCombo(opciones, "Texto", "Valor");

        //    ActivarFiltroFechas("Filtrar por fecha");

        //    var tiposFecha = new List<OpcionFiltro>
        //    {
        //        new OpcionFiltro { Texto = "Todas", Valor = "" },
        //        new OpcionFiltro { Texto = "Fecha Inicio", Valor = ((int)TipoFiltroFechaOferta.FechaInicio).ToString() },
        //        new OpcionFiltro { Texto = "Fecha Fin", Valor = ((int)TipoFiltroFechaOferta.FechaFin).ToString() },
        //        new OpcionFiltro { Texto = "Solo Activas", Valor = ((int)TipoFiltroOferta.Activas).ToString() },
        //        new OpcionFiltro { Texto = "Solo Inactivas", Valor = ((int)TipoFiltroOferta.Inactivas).ToString() },
        //        new OpcionFiltro { Texto = "Es un solo producto", Valor = ((int)TipoFiltroOferta.EsUnSoloProducto).ToString() },
        //        new OpcionFiltro { Texto = "Es combo", Valor = ((int)TipoFiltroOferta.EsCombo).ToString() },
        //        new OpcionFiltro { Texto = "Es grupo", Valor = ((int)TipoFiltroOferta.EsGrupo).ToString() },
        //    };

        //    ActivarComboOpcional(tiposFecha, "Texto", "Valor");

        //    cbx1.SelectedValue = "";
        //    cbxFiltroExtraEstado.SelectedValue = "";
        //}

        //protected override string ObtenerTextoLabelFiltroOpcional()
        //{
        //    return "Buscar oferta por:";
        //}

        //protected override string ObtenerTextoLabelFiltroExtra()
        //{
        //    return "Filtrar por:";
        //}

        //protected override string ObtenerTextoLabelBusqueda()
        //{
        //    return "Buscar oferta:";
        //}
    }
}
