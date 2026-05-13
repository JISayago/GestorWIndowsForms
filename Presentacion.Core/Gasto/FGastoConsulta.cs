using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Gasto;
using Servicios.Helpers.Producto;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Gasto;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.Gasto
{
    public partial class FGastoConsulta : FBaseConsulta
    {
        private readonly IGastoServicio _gastoServicio;
        private readonly long _logeadoId;

        public long? gastoSeleccionado = null;
        private bool vieneDeSeleccionGasto = false;

        public FGastoConsulta(long logeadoId)
            : this(new GastoServicio(), logeadoId)
        {
            InitializeComponent();
        }

        public FGastoConsulta(IGastoServicio gastoServicio, long logeadoId)
        {
            _gastoServicio = gastoServicio;
            _logeadoId = logeadoId;
            InitializeComponent();
        }

        public FGastoConsulta(bool _vieneDeSeleccionGasto, long logeadoId)
            : this(new GastoServicio(), logeadoId)
        {
            vieneDeSeleccionGasto = _vieneDeSeleccionGasto;
            InitializeComponent();
        }

        #region 🔷 ACCIONES DINAMICAS

        protected override void ConfigurarAccionesPersonalizadas()
        {
            AgregarAccion(
                "Anular",
                Constantes.Imagenes.ImgEliminar,
                AnularGasto,
                true
            );
            AgregarAccion(
               "Pagar",
               Constantes.Imagenes.ImgEliminar,
               MarcarComoPagada,
               true
           );
        }

        private void MarcarComoPagada(long? id)
        {
            if (!id.HasValue)
            {
                MessageBox.Show("Seleccione un gasto.");
                return;
            }

            var resultado = _gastoServicio.ConfirmarPago(id.Value);

            if (resultado.Exitoso)
            {
                Recargar();
                MessageBox.Show(resultado.Mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Recargar();
                MessageBox.Show(resultado.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AnularGasto(long? id)
        {
            if (!id.HasValue)
            {
                MessageBox.Show("Seleccione un gasto.");
                return;
            }

            var resultado = _gastoServicio.AnularGasto(id.Value);

            if (resultado.Exitoso)
            {
                Recargar();
                MessageBox.Show(resultado.Mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Recargar();
                MessageBox.Show(resultado.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 🔷 GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Count == 0)
                return;

            // 🔹 ID
            if (grilla.Columns.Contains("GastoId"))
            {
                grilla.Columns["GastoId"].Visible = false;
                grilla.Columns["GastoId"].Name = "Id";
            }

            // 🔹 Número
            if (grilla.Columns.Contains("NumeroGasto"))
            {
                grilla.Columns["NumeroGasto"].Visible = true;
                grilla.Columns["NumeroGasto"].HeaderText = "N° Gasto";
                grilla.Columns["NumeroGasto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            // 🔹 Fecha
            if (grilla.Columns.Contains("FechaGasto"))
            {
                grilla.Columns["FechaGasto"].Visible = true;
                grilla.Columns["FechaGasto"].HeaderText = "Fecha";
                grilla.Columns["FechaGasto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                grilla.Columns["FechaGasto"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            // 🔹 Empleado
            if (grilla.Columns.Contains("NombreEmpleado"))
            {
                grilla.Columns["NombreEmpleado"].Visible = true;
                grilla.Columns["NombreEmpleado"].HeaderText = "Empleado";
                grilla.Columns["NombreEmpleado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            // 🔹 Categoría
            // 🔹 Categoría (DESCRIPCIÓN)
            if (grilla.Columns.Contains("CategoriaGastoDescripcion"))
            {
                grilla.Columns["CategoriaGastoDescripcion"].Visible = true;
                grilla.Columns["CategoriaGastoDescripcion"].HeaderText = "Categoría";
                grilla.Columns["CategoriaGastoDescripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            // 🔸 Ocultar el int
            if (grilla.Columns.Contains("CategoriaGasto"))
            {
                grilla.Columns["CategoriaGasto"].Visible = false;
            }

            // 🔹 Montos
            if (grilla.Columns.Contains("MontoTotal"))
            {
                grilla.Columns["MontoTotal"].Visible = true;
                grilla.Columns["MontoTotal"].HeaderText = "Monto Total";
                grilla.Columns["MontoTotal"].DefaultCellStyle.Format = "C2";
            }

            if (grilla.Columns.Contains("MontoPagado"))
            {
                grilla.Columns["MontoPagado"].Visible = true;
                grilla.Columns["MontoPagado"].HeaderText = "Monto Pagado";
                grilla.Columns["MontoPagado"].DefaultCellStyle.Format = "C2";
            }

            // 🔹 Estado
           

            if (grilla.Columns.Contains("EstadoGastoDescripcion"))
            {
                grilla.Columns["EstadoGastoDescripcion"].Visible = true;
                grilla.Columns["EstadoGastoDescripcion"].HeaderText = "Estado";
                grilla.Columns["EstadoGastoDescripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            // 🔸 Ocultar el int
            if (grilla.Columns.Contains("EstadoGasto"))
            {
                grilla.Columns["EstadoGasto"].Visible = false;
            }
            // 🔹 Detalle
            if (grilla.Columns.Contains("Detalle"))
            {
                grilla.Columns["Detalle"].Visible = true;
                grilla.Columns["Detalle"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        #endregion

        #region 🔥 ACTUALIZAR DATOS (NUEVO)

        //public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        //{
        //    base.ActualizarDatos(dgv, filtros);

        //    var resultado = _gastoServicio.ObtenerGastos(filtros);

        //    dgv.DataSource = resultado.Items;

        //    ResetearGrilla(dgv);

        //    var paginacion = new DatosPaginacion
        //    {
        //        PaginaActual = resultado.Page,
        //        PageSize = resultado.PageSize,
        //        CantidadRegistros = resultado.TotalRegistros,
        //    };

        //    ActualizarPaginacionUI(paginacion);
        //}

        #endregion

        #region 🔷 BOTONES BASE

        public override void EjecutarBtnNuevo()
        {
            var f = new FGastoABM(_logeadoId);
            f.ShowDialog();

           if (f.RealizoAlgunaOperacion)
           Recargar();
        }

        private void Recargar()
        {
            var filtros = new FiltroConsulta();
           ActualizarDatos(dgvGrilla, filtros);
        }

        #endregion

        #region 🔷 SELECCIONAR GASTO

        private void btnSeleccionarGasto_Click(object sender, System.EventArgs e)
        {
            if (!entidadID.HasValue)
            {
                MessageBox.Show("Seleccione un gasto.");
                return;
            }

            gastoSeleccionado = entidadID;
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion
        #region 🔷 FILTROS
        protected override string TextoLblBuscar
    => "Buscar Gasto:";

        protected override string TextoLblCbx1
            => "Filtrar por Propiedad";

        protected override string TextoLblCbx2
            => "Filtrar por Estado";

        protected override string TextoLblCbx3
            => "Filtrar por Fecha";
        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();
            ActivarCheck(chkBool1, "Mostrar Anulados");
            ActivarCheck(chkBool2, "Mostrar todos los Gastos (histórico)");
            ActivarFiltroFechas("Filtrar por fecha");
            // 🔎 Buscar por
            var filtrosBusqueda = new List<OpcionFiltro>
    {
                new OpcionFiltro { Texto = "Todos", Valor = "" },
        new OpcionFiltro { Texto = "Número Gasto", Valor = "NumeroGasto" },
        new OpcionFiltro { Texto = "Empleado", Valor = "NombreEmpleado" },
        new OpcionFiltro { Texto = "Categoría", Valor = "CategoriaGasto" }
    };

            ActivarCombo(
                cbx1,
                lblcbx1,
                filtrosBusqueda,
                "Texto",
                "Valor",
                "Buscar por"
            );

            cbx1.SelectedIndex = 0;

            // 📅 Fechas

            var filtrosFecha = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todos", Valor = "" },
                 new OpcionFiltro
        {
            Texto = "Fecha del gasto",
            Valor = ((int)TipoFiltroFechaGasto.FechaGasto).ToString()
        },

        new OpcionFiltro
        {

            Texto = "Fecha registro",
            Valor = ((int)TipoFiltroFechaGasto.FechaRegistro).ToString()
        },
            };
            var filtrosEstado = new List<OpcionFiltro>
    {
       new OpcionFiltro { Texto = "Todos", Valor = "" },

        new OpcionFiltro
        {
            Texto = "Pagado",
            Valor = ((int)EstadoGasto.Pagado).ToString()
        },

        new OpcionFiltro
        {
            Texto = "Pendiente",
            Valor = ((int)EstadoGasto.Pendiente).ToString()
        },

        new OpcionFiltro
        {
            Texto = "Anulado",
            Valor = ((int)EstadoGasto.Anulado).ToString()
        }
    };

            ActivarCombo(
                cbx2,
                lblcbx2,
                filtrosEstado,
                "Texto",
                "Valor",
                "Estado"
            );
            ActivarCombo(
                cbx3,
                lblcbx3,
                filtrosFecha,
                "Texto",
                "Valor",
                "Fecha"
            );
            cbx1.SelectedValue = "";
            cbx2.SelectedValue = "";
            cbx3.SelectedValue = "";

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

            paginaActual = 1;

            var filtros = ObtenerFiltros();

            ActualizarDatos(dgvGrilla, filtros);
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

            paginaActual = 1;

            var filtros = ObtenerFiltros();

            ActualizarDatos(dgvGrilla, filtros);
        }

        private void LimpiarFiltrosEspeciales()
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
        #endregion

        #region 🔥 FILTROS

        protected override FiltroConsulta ObtenerFiltros()
        {
            var filtros = base.ObtenerFiltros();

            filtros.Filtro1 = cbx1.SelectedValue?.ToString();
            filtros.Filtro2 = cbx2.SelectedValue?.ToString();

            return filtros;
        }

        #endregion

        #region 🔥 ACTUALIZAR DATOS

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            var resultado = _gastoServicio.ObtenerGastos(filtros);

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
    }
}
