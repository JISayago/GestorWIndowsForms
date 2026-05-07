using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Cliente.CtaCte;
using Servicios.Helpers.Producto;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.CuentaCorriente;
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

        #region 🔥 ACTUALIZAR DATOS (NUEVO SISTEMA)

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            // 🔹 valor por defecto de búsqueda (ajustalo a tu modelo)
            filtros.Extra ??= "Nombre";

            // 🔹 llamada única al servicio (nuevo esquema)
            var resultado = _cuentacorrienteServicio.ObtenerCuentaCorrientes(filtros);

            // 🔹 bind
            dgv.DataSource = resultado.Items;

            // 🔴 CLAVE: reaplicar columnas
            ResetearGrilla(dgv);

            // 🔹 paginación
            var paginacion = new DatosPaginacion
            {
                PaginaActual = resultado.Page,
                PageSize = resultado.PageSize,
                CantidadRegistros = resultado.TotalRegistros,
            };

            ActualizarPaginacionUI(paginacion);

            // 🔹 estado botones
            BarraLateralBotones.Enabled = !filtros.VerEliminados;
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
         //   btnActualizar_Click_Base();
        }

        #endregion

        protected override void ConfigurarFiltrosUI()
        {

            base.ConfigurarFiltrosUI();

            ActivarFiltroEliminados("Mostrar productos eliminados.");

            var opciones = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todos", Valor = "" },
                new OpcionFiltro { Texto = "Nombre de Cuenta Corriente", Valor = "NombreCuentaCorriente" },
            };

            ActivarFiltroCombo(opciones, "Texto", "Valor");

            ActivarFiltroFechas("Filtrar por fecha de vencimiento");

            var tiposFecha = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todas", Valor = "" },
                new OpcionFiltro { Texto = "Fecha vencimiento", Valor = "vto" },
                //new OpcionFiltro { Texto = "Fecha creación", Valor = "creacion" },
                new OpcionFiltro { Texto = "Activa", Valor = ((int)EstadoCuentaCorriente.Activa).ToString() },
                new OpcionFiltro { Texto = "Suspendida", Valor = ((int)EstadoCuentaCorriente.Suspendida).ToString() },
                new OpcionFiltro { Texto = "Cerrada", Valor = ((int)EstadoCuentaCorriente.Cerrada).ToString() },
            };

            ActivarComboOpcional(tiposFecha, "Texto", "Valor");

            cbx1.SelectedValue = "";
            cbxFiltroExtraEstado.SelectedValue = "";
        }

        protected override string ObtenerTextoLabelFiltroOpcional()
        {
            return "Buscar cuente corriente por:";
        }

        protected override string ObtenerTextoLabelFiltroExtra()
        {
            return "Filtrar por:";
        }

        protected override string ObtenerTextoLabelBusqueda()
        {
            return "Buscar cuenta corriente:";
        }
    }
}
