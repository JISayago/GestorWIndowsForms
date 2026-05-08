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

            if (grilla.Columns.Contains("EstadoCuentaCorriente"))
            {
                grilla.Columns["EstadoCuentaCorriente"].Visible = true;
                grilla.Columns["EstadoCuentaCorriente"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grilla.Columns["EstadoCuentaCorriente"].HeaderText = "Estado CC";
            }
        }

        #endregion

        #region 🔥 ACTUALIZAR DATOS

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            filtros.Filtro1 ??= "NombreCuentaCorriente";

            var resultado = _cuentacorrienteServicio.ObtenerCuentaCorrientes(filtros);

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
            var f = new FCuentaCorrienteABM(TipoOperacion.Nuevo);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
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

        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

            ActivarCheck(
                chkBool1,
                "Mostrar cuentas eliminadas"
            );

            var opcionesBusqueda = new List<OpcionFiltro>
            {
                new OpcionFiltro
                {
                    Texto = "Todos",
                    Valor = ""
                },

                new OpcionFiltro
                {
                    Texto = "Nombre Cuenta Corriente",
                    Valor = "NombreCuentaCorriente"
                }
            };

            ActivarCombo(
                cbx1,
                lblcbx1,
                opcionesBusqueda,
                "Texto",
                "Valor",
                "Buscar cuenta por:"
            );

            ActivarFiltroFechas("Usar filtro de fechas");

            var opcionesEstado = new List<OpcionFiltro>
            {
                new OpcionFiltro
                {
                    Texto = "Todos",
                    Valor = ""
                },

                new OpcionFiltro
                {
                    Texto = "Fecha vencimiento",
                    Valor = "vto"
                },

                new OpcionFiltro
                {
                    Texto = "Activa",
                    Valor = ((int)EstadoCuentaCorriente.Activa).ToString()
                },

                new OpcionFiltro
                {
                    Texto = "Suspendida",
                    Valor = ((int)EstadoCuentaCorriente.Suspendida).ToString()
                },

                new OpcionFiltro
                {
                    Texto = "Cerrada",
                    Valor = ((int)EstadoCuentaCorriente.Cerrada).ToString()
                }
            };

            ActivarCombo(
                cbx2,
                lblcbx2,
                opcionesEstado,
                "Texto",
                "Valor",
                "Filtrar por:"
            );

            cbx1.SelectedValue = "";
            cbx2.SelectedValue = "";
        }

        #endregion
    }
}