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
            if (grilla.Columns.Contains("CategoriaGasto"))
            {
                grilla.Columns["CategoriaGasto"].Visible = true;
                grilla.Columns["CategoriaGasto"].HeaderText = "Categoría";
                grilla.Columns["CategoriaGasto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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
            if (grilla.Columns.Contains("EstadoGasto"))
            {
                grilla.Columns["EstadoGasto"].Visible = true;
                grilla.Columns["EstadoGasto"].HeaderText = "Estado";
                grilla.Columns["EstadoGasto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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
                CantidadRegistros = resultado.TotalRegistros,
            };

            ActualizarPaginacionUI(paginacion);
        }

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
        protected override void ConfigurarFiltrosUI()
        {

            base.ConfigurarFiltrosUI();


            var opciones = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todos", Valor = "" },
                new OpcionFiltro { Texto = "Numero de Gasto", Valor = "NumeroGasto" },
                new OpcionFiltro { Texto = "Empleado", Valor = "NombreEmpleado" },

            };

            ActivarFiltroCombo(opciones, "Texto", "Valor");

            ActivarFiltroFechas("Filtrar por fecha");

            var tiposFecha = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todas", Valor = "" },
                new OpcionFiltro { Texto = "Fecha del Gasto Realizado", Valor = ((int)TipoFiltroFechaGasto.FechaGasto).ToString() },
                new OpcionFiltro { Texto = "Fecha del Registro del Gasto", Valor = ((int)TipoFiltroFechaGasto.FechaRegistro).ToString() },
                new OpcionFiltro { Texto = "Pagado", Valor = ((int)EstadoGasto.Pagado).ToString() },
                new OpcionFiltro { Texto = "Pendiente", Valor = ((int)EstadoGasto.Pendiente).ToString() },
                new OpcionFiltro { Texto = "Anulado", Valor = ((int)EstadoGasto.Pagado).ToString() }
            };

            ActivarComboOpcional(tiposFecha, "Texto", "Valor");

            cbxFiltroOpcional.SelectedValue = "";
            cbxFiltroExtraEstado.SelectedValue = "";
        }

        protected override string ObtenerTextoLabelFiltroOpcional()
        {
            return "Buscar gasto por:";
        }

        protected override string ObtenerTextoLabelFiltroExtra()
        {
            return "Filtrar gasto por:";
        }

        protected override string ObtenerTextoLabelBusqueda()
        {
            return "Buscar gasto:";
        }
        private void FGastoConsulta_Load(object sender, EventArgs e)
        {
        }
    }
}
