using Presentacion.Core.Cliente;
using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Cliente.CtaCte;
using Servicios.Helpers.Producto;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.CuentaCorriente;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Presentacion.Core.CuentaCorriente
{
    public partial class FCuentaCorrienteConsulta : FBaseConsulta
    {
        private readonly ICuentaCorrienteServicio _cuentacorrienteServicio;

        public FCuentaCorrienteConsulta() : this(new CuentaCorrienteServicio())
        {
            InitializeComponent();
        }

        public FCuentaCorrienteConsulta(ICuentaCorrienteServicio cuentacorrienteServicio)
        {
            _cuentacorrienteServicio = cuentacorrienteServicio;
            InitializeComponent();
        }

        #region 🔷 GRILLA
        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Count == 0) return;

            // Ocultamos el ID numérico
            if (grilla.Columns.Contains("CuentaCorrienteId"))
            {
                grilla.Columns["CuentaCorrienteId"].Visible = false;
                grilla.Columns["CuentaCorrienteId"].Name = "Id";
            }

            if (grilla.Columns.Contains("NombreCuentaCorriente"))
            {
                grilla.Columns["NombreCuentaCorriente"].Visible = true;
                grilla.Columns["NombreCuentaCorriente"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grilla.Columns["NombreCuentaCorriente"].HeaderText = "Nombre CC";
            }

            if (grilla.Columns.Contains("FechaVencimiento"))
            {
                grilla.Columns["FechaVencimiento"].Visible = true;
                grilla.Columns["FechaVencimiento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grilla.Columns["FechaVencimiento"].HeaderText = "Fecha Vencimiento";
            }

            // 🔹 MODIFICACIÓN: Ocultamos el entero EstadoCtaCte para que no ensucie la grilla
            if (grilla.Columns.Contains("EstadoCtaCte"))
            {
                grilla.Columns["EstadoCtaCte"].Visible = false;
            }

            // 🔹 MODIFICACIÓN: Mostramos la propiedad calculada con la descripción en texto
            if (grilla.Columns.Contains("EstadoDescripcionCtaCte"))
            {
                grilla.Columns["EstadoDescripcionCtaCte"].Visible = true;
                grilla.Columns["EstadoDescripcionCtaCte"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grilla.Columns["EstadoDescripcionCtaCte"].HeaderText = "Estado CC";
            }

            // 🔹 TIP ADICIONAL: Podés ocultar listas internas si Windows Forms las auto-genera como columnas toscas
            if (grilla.Columns.Contains("DniAutorizados")) grilla.Columns["DniAutorizados"].Visible = false;
            if (grilla.Columns.Contains("MovimientoCuentaCorrienteIds")) grilla.Columns["MovimientoCuentaCorrienteIds"].Visible = false;
        }

        #endregion

        #region 🔥 ACTUALIZAR DATOS

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            filtros.Filtro1 ??= "NombreCuentaCorriente";

            var resultado = _cuentacorrienteServicio.ObtenerCuentaCorrientes(filtros);

            // Al asignar la lista de DTOs, la grilla auto-mapea la propiedad EstadoDescripcionCtaCte al instante
            dgv.DataSource = resultado.Items;

            ResetearGrilla(dgv);

            var paginacion = new DatosPaginacion
            {
                PaginaActual = resultado.Page,
                PageSize = resultado.PageSize,
                CantidadRegistros = resultado.TotalRegistros,
            };

            ActualizarPaginacionUI(paginacion);

            BarraLateralBotones.Enabled = !filtros.Bool1;
        }

        #endregion

        #region 🔷 BOTONES BASE

        public override void EjecutarBtnNuevo()
        {
            //var f = new FCuentaCorrienteABM(TipoOperacion.Nuevo);
            //f.ShowDialog();

            //if (f.RealizoAlgunaOperacion)
            //    Recargar();

            var f = new FClienteConsulta(true, 0);
            f.ShowDialog();

        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();

            if (!puedeEjecutarComando) return;

            var f = new FCuentaCorrienteABM(TipoOperacion.Modificar, entidadID);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();

            if (!puedeEjecutarComando) return;

            var f = new FCuentaCorrienteABM(TipoOperacion.Eliminar, entidadID);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        private void Recargar()
        {
            RefrescarGrilla();
        }

        #endregion

        #region 🔷 FILTROS

        protected override string TextoLblBuscar => "Buscar Cuenta Corriente:";
        protected override string TextoLblCbx1 => "Filtrar por Propiedad";
        protected override string TextoLblCbx2 => "Filtrar por Estado";
        protected override string TextoLblCbx3 => "Filtrar por Fecha";
        protected override string TextoTitular
          => "Listado de Cuentas Corrientes";

        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

            ActivarCheck(chkBool1, "Mostrar Eliminados");
            ActivarCheck(chkBool2, "Mostrar Cuentas Corrientes (Histórico)");

            var opcionesBusqueda = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todos", Valor = "" },
                new OpcionFiltro { Texto = "Nombre Cuenta Corriente", Valor = "NombreCuentaCorriente" }
            };

            ActivarCombo(cbx1, lblcbx1, opcionesBusqueda, "Texto", "Valor", "Buscar cuenta por:");
            ActivarFiltroFechas("Usar filtro de fechas");

            var opcionesEstado = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todos", Valor = "" },
                new OpcionFiltro { Texto = "Activa", Valor = ((int)EstadoCuentaCorriente.Activa).ToString() },
                new OpcionFiltro { Texto = "Suspendida", Valor = ((int)EstadoCuentaCorriente.Suspendida).ToString() },
                new OpcionFiltro { Texto = "Cerrada", Valor = ((int)EstadoCuentaCorriente.Cerrada).ToString() }
            };

            ActivarCombo(cbx2, lblcbx2, opcionesEstado, "Texto", "Valor", "Filtrar por:");

            var tipoFecha = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Fecha de vencimiento", Valor = "vto" }
            };

            ActivarCombo(cbx3, lblcbx3, tipoFecha, "Texto", "Valor", "Filtrar por:");

            cbx1.SelectedValue = "";
            cbx2.SelectedValue = "";
            cbx3.SelectedValue = "vto";
        }

        protected override void AccionCheck2()
        {
            if (chkBool2.Checked)
            {
                _actualizandoFiltros = true;
                chkBool1.Checked = false;
                _actualizandoFiltros = false;
                LimpiarFiltrosEspeciales();
            }

        }

        protected override void AccionCheck1()
        {
            if (chkBool1.Checked)
            {
                _actualizandoFiltros = true;
                chkBool2.Checked = false;
                _actualizandoFiltros = false;
                LimpiarFiltrosEspeciales();
            }
        }

        private void LimpiarFiltrosEspeciales()
        {
            _actualizandoFiltros = true;
            txtBuscar.Clear();

            if (cbx1.Enabled) cbx1.SelectedIndex = 0;
            if (cbx2.Enabled) cbx2.SelectedIndex = 0;
            if (cbx3.Enabled) cbx3.SelectedIndex = 0;

            chkUsarFecha.Checked = false;
            _actualizandoFiltros = false;
        }

        #endregion
    }
}