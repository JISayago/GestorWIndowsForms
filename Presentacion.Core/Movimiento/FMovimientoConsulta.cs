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

        protected override bool UsarCheck1 => true;
        protected override bool UsarCheck2 => true;

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

                         var opcionesTipoMovimiento = new List<OpcionFiltro>
             {
                 new OpcionFiltro { Texto = "Todos", Valor = "" },
             
                 // 🔹 TipoMovimiento (nivel general)
                 new OpcionFiltro { Texto = "Ingresos", Valor = "TM_" + ((int)TipoMovimiento.Ingreso) },
                 new OpcionFiltro { Texto = "Egresos", Valor = "TM_" + ((int)TipoMovimiento.Egreso) },
             
                 // 🔹 TipoMovimientoDetalle
                 new OpcionFiltro { Texto = "Cancelación", Valor = "TMD_" + ((int)TipoMovimientoDetalle.Cancelacion) },
                 new OpcionFiltro { Texto = "Cuenta Corriente", Valor = "TMD_" + ((int)TipoMovimientoDetalle.CuentaCorriente) },
                 new OpcionFiltro { Texto = "Stock", Valor = "TMD_" + ((int)TipoMovimientoDetalle.Stock) },
                 new OpcionFiltro { Texto = "Venta", Valor = "TMD_" + ((int)TipoMovimientoDetalle.Venta) },
                 new OpcionFiltro { Texto = "Compra", Valor = "TMD_" + ((int)TipoMovimientoDetalle.Compra) },
                 new OpcionFiltro { Texto = "Servicios", Valor = "TMD_" + ((int)TipoMovimientoDetalle.Servicios) },
                 new OpcionFiltro { Texto = "Venta Libre", Valor = "TMD_" + ((int)TipoMovimientoDetalle.VentaLibre) },
             
                 // 📅 Fecha
             };
             
            ActivarCombo(
                cbx2,
                lblcbx2,
                opcionesTipoMovimiento,
                "Texto",
                "Valor",
                "Tipo Movimiento"
            );
            var opcionFecha = new List<OpcionFiltro>
            {

             new OpcionFiltro { Texto = "Todos", Valor = "" },
             new OpcionFiltro { Texto = "Fecha Movimiento", Valor = "FECHA" },
            };
            ActivarCombo(
                cbx3,
                lblcbx3,
                opcionFecha,
                "Texto",
                "Valor",
                "Fecha"
            );
            ActivarFiltroFechas("Filtrar por fecha");
            ActivarCheck(chkBool1, "Ver eliminados");
            ActivarCheck(chkBool2, "Ver todos (históricos)");
            cbx1.SelectedValue = "";
            cbx2.SelectedValue = "";
            cbx3.SelectedValue = "";
        }
        
        protected override string TextoLblBuscar
     => "Buscar Movimientos:";

        protected override string TextoLblCbx1
            => "Filtrar por Propiedad";

        protected override string TextoLblCbx2
            => "Filtrar por Movimiento";

        protected override string TextoLblCbx3
            => "Filtrar por Fecha";

        protected override void AccionCheck2()
        {
            if (chkBool2.Checked)
            {
                _actualizandoFiltros = true;
                chkBool1.Checked = false;
                _actualizandoFiltros = false;

                LimpiarFiltrosParaTodos();
            }
            paginaActual = 1;
        }

        protected override void AccionCheck1()
        {
            if (chkBool1.Checked)
            {
                _actualizandoFiltros = true;
                chkBool2.Checked = false;
                _actualizandoFiltros = false;

                LimpiarFiltrosParaTodos();
                paginaActual = 1;
            }
        }
        private void LimpiarFiltrosParaTodos()
        {
            _actualizandoFiltros = true;

            txtBuscar.Clear();

            if (cbx1.Enabled)
                cbx1.SelectedIndex = 0;

            if (cbx2.Enabled)
                cbx2.SelectedIndex = 0;

            if (cbx3.Enabled)
                cbx3.SelectedIndex = 0;

            chkUsarFecha.Checked = false;

            _actualizandoFiltros = false;
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

            grilla.ReadOnly = true;

            if (grilla.Columns.Count == 0)
                return;

            // IMPORTANTE: usar Fill real
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // =========================================
            // ID (OCULTO)
            // =========================================
            if (grilla.Columns.Contains("MovimientoId"))
            {
                grilla.Columns["MovimientoId"].Visible = false;
                grilla.Columns["MovimientoId"].Name = "Id";
            }

            // =========================================
            // NUMERO (GRANDE)
            // =========================================
            if (grilla.Columns.Contains("NumeroMovimiento"))
            {
                var col = grilla.Columns["NumeroMovimiento"];

                col.Visible = true;
                col.HeaderText = "Número";

                col.FillWeight = 300;   // 🔥 grande
                col.MinimumWidth = 180;
            }

            // =========================================
            // FECHA (MEDIO)
            // =========================================
            if (grilla.Columns.Contains("FechaMovimiento"))
            {
                var col = grilla.Columns["FechaMovimiento"];

                col.Visible = true;
                col.HeaderText = "Fecha";

                col.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                col.FillWeight = 150;
                col.MinimumWidth = 130;
            }

            // =========================================
            // MOVIMIENTO (CHICO)
            // =========================================
            if (grilla.Columns.Contains("TipoMovimientoDescripcion"))
            {
                var col = grilla.Columns["TipoMovimientoDescripcion"];

                col.Visible = true;
                col.HeaderText = "Movimiento";

                col.FillWeight = 100;
                col.MinimumWidth = 90;
            }

            if (grilla.Columns.Contains("TipoMovimiento"))
            {
                grilla.Columns["TipoMovimiento"].Visible = false;
            }

            // =========================================
            // TIPO DETALLE (MEDIO)
            // =========================================
            if (grilla.Columns.Contains("TipoMovimientoDetalleDescripcion"))
            {
                var col = grilla.Columns["TipoMovimientoDetalleDescripcion"];

                col.Visible = true;
                col.HeaderText = "Tipo";

                col.FillWeight = 100;
                col.MinimumWidth = 120;
            }

            if (grilla.Columns.Contains("TipoMovimientoDetalle"))
            {
                grilla.Columns["TipoMovimientoDetalle"].Visible = false;
            }

            // =========================================
            // MONTO (GRANDE)
            // =========================================
            if (grilla.Columns.Contains("Monto"))
            {
                var col = grilla.Columns["Monto"];

                col.Visible = true;
                col.HeaderText = "Monto";

                col.DefaultCellStyle.Format = "C2";

                col.FillWeight = 150;   // 🔥 grande
                col.MinimumWidth = 150;
            }

            // =========================================
            // OCULTOS
            // =========================================
            if (grilla.Columns.Contains("EntidadId"))
                grilla.Columns["EntidadId"].Visible = false;

            if (grilla.Columns.Contains("TipoEntidad"))
                grilla.Columns["TipoEntidad"].Visible = false;

            if (grilla.Columns.Contains("EstaEliminado"))
                grilla.Columns["EstaEliminado"].Visible = false;
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