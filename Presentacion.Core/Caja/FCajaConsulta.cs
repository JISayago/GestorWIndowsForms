using AccesoDatos.Entidades;
using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Caja;
using Servicios.LogicaNegocio.Caja.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Presentacion.Core.Caja
{
    public partial class FCajaConsulta : FBaseConsulta
    {
        private readonly ICajaServicio _cajaServicio;

        public FCajaConsulta() : this(new CajaServicio())
        {
            InitializeComponent();
        }

        public FCajaConsulta(ICajaServicio cajaServicio)
        {
            _cajaServicio = cajaServicio;
        }

        #region INIT

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Text = "Consulta de Cajas";
        }

        #endregion

        #region FILTROS

        protected override bool UsarCheck1 => true; // eliminados
        protected override bool UsarCheck2 => true; // histórico
        protected override bool EsModoSoloLectura(FiltroConsulta filtro)
        {
            return true;
        }
        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

            // =========================================
            // BUSQUEDA
            // =========================================
            var estado = new List<OpcionFiltro>
        {
              new OpcionFiltro { Texto = "Todos", Valor = "" },
            new OpcionFiltro { Texto = "Abierta", Valor = "Abierta" },
            new OpcionFiltro { Texto = "Cerrada", Valor = "Cerrada" }
        };

            ActivarCombo(
                cbx1,
                lblcbx1,
                estado,
                "Texto",
                "Valor",
                "Buscar por Caja"
            );
            // =========================================
            // FECHAS
            // =========================================
            ActivarFiltroFechas("Filtrar por fecha");

            // =========================================
            // CHECKS
            // =========================================
            ActivarCheck(chkBool1, "Ver eliminados");
            ActivarCheck(chkBool2, "Ver todas (histórico)");

            cbx1.SelectedValue = "";
        }

        protected override string TextoLblBuscar => "Buscar Cajas:";
        protected override string TextoLblCbx1 => "Filtrar por Propiedad";
        protected override string TextoLblCbx2 => "Filtrar por Estado";
        protected override string TextoLblCbx3 => "Filtrar por";

        protected override void AccionCheck2()
        {
            if (chkBool2.Checked)
            {
                _actualizandoFiltros = true;
                chkBool1.Checked = false;
                _actualizandoFiltros = false;

                LimpiarFiltros();
            }

        }

        protected override void AccionCheck1()
        {
            if (chkBool1.Checked)
            {
                _actualizandoFiltros = true;
                chkBool2.Checked = false;
                _actualizandoFiltros = false;

                LimpiarFiltros();
            }

        }

        private void LimpiarFiltros()
        {
            _actualizandoFiltros = true;

            txtBuscar.Clear();

            if (cbx1.Enabled) cbx1.SelectedIndex = 0;
            if (cbx2.Enabled) cbx2.SelectedIndex = 0;

            chkUsarFecha.Checked = false;

            _actualizandoFiltros = false;
        }

        #endregion

        #region DATOS

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            var resultado = _cajaServicio.ObtenerCajas(filtros);

            dgv.DataSource = null;
            dgv.DataSource = resultado.Items;

            ResetearGrilla(dgv); // 🔥 siempre después del DataSource

            base.ActualizarDatos(dgv, filtros); // esto no jode

            ActualizarPaginacionUI(new DatosPaginacion
            {
                PaginaActual = resultado.Page,
                PageSize = resultado.PageSize,
                CantidadRegistros = resultado.TotalRegistros
            });
        }

        #endregion

        #region GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            grilla.ReadOnly = true;

            if (grilla.Columns.Count == 0)
                return;

            // 🔥 CLAVE: asegurar generación y layout
            grilla.AutoGenerateColumns = true;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // =========================================
            // ID (OCULTO)
            // =========================================
            if (grilla.Columns.Contains("CajaId"))
            {
                grilla.Columns["CajaId"].Visible = false;
                grilla.Columns["CajaId"].Name = "Id"; // importante para RowEnter
            }

            // =========================================
            // FECHA APERTURA
            // =========================================
            if (grilla.Columns.Contains("FechaInicio"))
            {
                var col = grilla.Columns["FechaInicio"];
                col.Visible = true;
                col.HeaderText = "Fecha Apertura";
                col.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                col.FillWeight = 140;
                col.MinimumWidth = 130;
            }

            // =========================================
            // FECHA CIERRE
            // =========================================
            if (grilla.Columns.Contains("FechaFin"))
            {
                var col = grilla.Columns["FechaFin"];
                col.Visible = true;
                col.HeaderText = "Fecha Cierre";
                col.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                col.FillWeight = 140;
                col.MinimumWidth = 130;
            }

            // =========================================
            // SALDO INICIAL
            // =========================================
            if (grilla.Columns.Contains("SaldoInicial"))
            {
                var col = grilla.Columns["SaldoInicial"];
                col.Visible = true;
                col.HeaderText = "Inicial";
                col.DefaultCellStyle.Format = "C2";
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.FillWeight = 120;
                col.MinimumWidth = 110;
            }

            // =========================================
            // SALDO ACTUAL
            // =========================================
            if (grilla.Columns.Contains("SaldoActual"))
            {
                var col = grilla.Columns["SaldoActual"];
                col.Visible = true;
                col.HeaderText = "Actual";
                col.DefaultCellStyle.Format = "C2";
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.FillWeight = 130;
                col.MinimumWidth = 120;
            }

            // =========================================
            // BALANCE FINAL
            // =========================================
            if (grilla.Columns.Contains("BalanceFinal"))
            {
                var col = grilla.Columns["BalanceFinal"];
                col.Visible = true;
                col.HeaderText = "Balance";
                col.DefaultCellStyle.Format = "C2";
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.FillWeight = 130;
                col.MinimumWidth = 120;
            }

            // =========================================
            // ESTADO (STRING)
            // =========================================
            if (grilla.Columns.Contains("detalleEstadoCaja"))
            {
                var col = grilla.Columns["detalleEstadoCaja"];
                col.Visible = true;
                col.HeaderText = "Estado";
                col.FillWeight = 100;
                col.MinimumWidth = 100;
            }

            // ocultar bool crudo
            if (grilla.Columns.Contains("EstaCerrada"))
            {
                grilla.Columns["EstaCerrada"].Visible = false;
            }

            // =========================================
            // EMPLEADO APERTURA
            // =========================================
            if (grilla.Columns.Contains("NombreEmpleadoApertura"))
            {
                var col = grilla.Columns["NombreEmpleadoApertura"];
                col.Visible = true;
                col.HeaderText = "Apertura";
                col.FillWeight = 150;
                col.MinimumWidth = 130;
            }

            // =========================================
            // EMPLEADO CIERRE
            // =========================================
            if (grilla.Columns.Contains("NombreEmpleadoCierre"))
            {
                var col = grilla.Columns["NombreEmpleadoCierre"];
                col.Visible = true;
                col.HeaderText = "Cierre";
                col.FillWeight = 150;
                col.MinimumWidth = 130;
            }
        }

        #endregion
    }
}
