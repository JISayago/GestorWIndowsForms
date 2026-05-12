using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Movimiento;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TuProyecto.Presentacion;

namespace Presentacion.Core.Movimiento
{
    public partial class FMovimientoConsulta : FBaseConsulta
    {
        private readonly IMovimientoServicio _movimientoServicio;

        public FMovimientoConsulta() : this(new MovimientoServicio())
        {
            InitializeComponent();
        }

        public FMovimientoConsulta(IMovimientoServicio movimientoServicio)
        {
            _movimientoServicio = movimientoServicio;
        }

        #region INIT

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            Text = "Consulta de Movimientos";
        }

        #endregion

        #region FILTROS

        protected override bool UsarCheck1 => false;

        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

            var opcionesBusqueda = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todos", Valor = "" },
                new OpcionFiltro { Texto = "Número Movimiento", Valor = "NumeroMovimiento" },
            };

            ActivarCombo(
                cbx1,
                lblcbx1,
                opcionesBusqueda,
                "Texto",
                "Valor",
                "Buscar por"
            );

            var opcionesTipo = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todos", Valor = "" },
                new OpcionFiltro { Texto = "Ingresos", Valor = ((int)TipoMovimiento.Ingreso).ToString() },
                new OpcionFiltro { Texto = "Egresos", Valor = ((int)TipoMovimiento.Egreso).ToString() }
            };

            ActivarCombo(
                cbx2,
                lblcbx2,
                opcionesTipo,
                "Texto",
                "Valor",
                "Tipo Movimiento"
            );

            ActivarFiltroFechas("Filtrar por fecha");
        }

        protected override void ActualizarTextosLabels()
        {
            base.ActualizarTextosLabels();

        }

        #endregion

        #region ACCIONES EXTRA

        protected override void ConfigurarAccionesPersonalizadas()
        {
            AgregarAccion(
                "Ver Detalle",
                SystemIcons.Information.ToBitmap(),
                (id) =>
                {
                    if (!id.HasValue)
                        return;

                    var f = new FMovimientoDetallado(id.Value);

                    f.ShowDialog();
                },
                true
            );
        }

        #endregion

        #region DATOS

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            var resultado = _movimientoServicio.ObtenerMovimientos(filtros);

            dgv.DataSource = resultado.Items;

            ResetearGrilla(dgv);

            var paginacion = new DatosPaginacion
            {
                PaginaActual = resultado.Page,
                PageSize = resultado.PageSize,
                CantidadRegistros = resultado.TotalRegistros
            };

            ActualizarPaginacionUI(paginacion);
        }

        #endregion

        #region GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Count == 0)
                return;

            if (grilla.Columns.Contains("MovimientoId"))
            {
                grilla.Columns["MovimientoId"].Visible = false;
                grilla.Columns["MovimientoId"].Name = "Id";
            }

            if (grilla.Columns.Contains("NumeroMovimiento"))
            {
                grilla.Columns["NumeroMovimiento"].Visible = true;
                grilla.Columns["NumeroMovimiento"].HeaderText = "Número";
                grilla.Columns["NumeroMovimiento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (grilla.Columns.Contains("FechaMovimiento"))
            {
                grilla.Columns["FechaMovimiento"].Visible = true;
                grilla.Columns["FechaMovimiento"].HeaderText = "Fecha";
                grilla.Columns["FechaMovimiento"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                grilla.Columns["FechaMovimiento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (grilla.Columns.Contains("TipoMovimiento"))
            {
                grilla.Columns["TipoMovimiento"].Visible = true;
                grilla.Columns["TipoMovimiento"].HeaderText = "Tipo";
                grilla.Columns["TipoMovimiento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (grilla.Columns.Contains("Detalle"))
            {
                grilla.Columns["Detalle"].Visible = true;
                grilla.Columns["Detalle"].HeaderText = "Detalle";
                grilla.Columns["Detalle"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (grilla.Columns.Contains("Monto"))
            {
                grilla.Columns["Monto"].Visible = true;
                grilla.Columns["Monto"].HeaderText = "Monto";
                grilla.Columns["Monto"].DefaultCellStyle.Format = "C2";
                grilla.Columns["Monto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        #endregion

        #region EVENTOS

        private void lblAbrir_Click(object sender, EventArgs e)
        {
            if (!entidadID.HasValue)
                return;

            var f = new FMovimientoDetallado(entidadID.Value);

            f.ShowDialog();
        }

        #endregion
    }
}