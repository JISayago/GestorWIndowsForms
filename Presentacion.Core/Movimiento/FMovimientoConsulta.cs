using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.Producto;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Movimiento;
using System;
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

        #region 🔥 ACCIONES EXTRA (opcional futuro)

        protected override void ConfigurarAccionesPersonalizadas()
        {
            // abrir detalle desde botón dinámico (opcional)
            AgregarAccion(
                "Ver Detalle",
                SystemIcons.Information.ToBitmap(),
                (id) =>
                {
                    if (!id.HasValue) return;

                    var f = new FMovimientoDetallado(id.Value);
                    f.ShowDialog();
                },
                true
            );
        }

        #endregion

        #region 🧱 GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Count == 0)
                return;

            // 🔹 ID
            if (grilla.Columns.Contains("MovimientoId"))
            {
                grilla.Columns["MovimientoId"].Visible = false;
                grilla.Columns["MovimientoId"].Name = "Id";
            }

            // 🔹 Número
            if (grilla.Columns.Contains("NumeroMovimiento"))
            {
                grilla.Columns["NumeroMovimiento"].Visible = true;
                grilla.Columns["NumeroMovimiento"].HeaderText = "Número";
                grilla.Columns["NumeroMovimiento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            // 🔹 Fecha
            if (grilla.Columns.Contains("FechaMovimiento"))
            {
                grilla.Columns["FechaMovimiento"].Visible = true;
                grilla.Columns["FechaMovimiento"].HeaderText = "Fecha";
                grilla.Columns["FechaMovimiento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grilla.Columns["FechaMovimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }
        #endregion
        #region 🔥 ACTUALIZAR DATOS (NUEVO SISTEMA)

        #endregion


        #region 🔥 ACTUALIZAR DATOS (NUEVO SISTEMA)


        //public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        //{
        //    base.ActualizarDatos(dgv, filtros);

        //    var resultado = _movimientoServicio.ObtenerMovimientos(filtros);

        //    dgv.DataSource = resultado.Items;

        //    // 🔴 CLAVE: volver a aplicar formato
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


        #endregion

        #region BOTON ABRIR DETALLE (label viejo)

        private void lblAbrir_Click(object sender, EventArgs e)
        {
            if (!entidadID.HasValue) return;

            var f = new FMovimientoDetallado(entidadID.Value);
            f.ShowDialog();
        }

        #endregion
        //protected override void ConfigurarFiltrosUI()
        //{

        //    base.ConfigurarFiltrosUI();

        //    ActivarFiltroEliminados("Mostrar productos eliminados.");

        //    var opciones = new List<OpcionFiltro>
        //    {
        //        new OpcionFiltro { Texto = "Todos", Valor = "" },
        //        new OpcionFiltro { Texto = "Número Movimiento", Valor = "NumeroMovimiento" },
        //    };

        //    ActivarFiltroCombo(opciones, "Texto", "Valor");

        //    ActivarFiltroFechas("Filtrar por fecha");

        //    var tiposFecha = new List<OpcionFiltro>
        //    {
        //        new OpcionFiltro { Texto = "Todas", Valor = "" },
        //        new OpcionFiltro { Texto = "Fecha Movimiento", Valor = "FM"},
        //        new OpcionFiltro { Texto = "Ingresos", Valor = ((int)TipoMovimiento.Ingreso).ToString() },
        //        new OpcionFiltro { Texto = "Egresos", Valor = ((int)TipoMovimiento.Egreso).ToString() }
        //    };

        //    ActivarComboOpcional(tiposFecha, "Texto", "Valor");

        //    cbx1.SelectedValue = "";
        //    cbxFiltroExtraEstado.SelectedValue = "";
        //}

        //protected override string ObtenerTextoLabelFiltroOpcional()
        //{
        //    return "Buscar movimiento por:";
        //}

        //protected override string ObtenerTextoLabelFiltroExtra()
        //{
        //    return "Filtrar por:";
        //}

        //protected override string ObtenerTextoLabelBusqueda()
        //{
        //    return "Buscar Movimiento:";
        //}
    }
}
