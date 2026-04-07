using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Gasto;
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

            if (grilla.Columns.Contains("GastoId"))
            {
                grilla.Columns["GastoId"].Visible = false;
                grilla.Columns["GastoId"].Name = "Id";
            }

            grilla.Columns["NumeroGasto"].Visible = true;
            grilla.Columns["NumeroGasto"].HeaderText = "N° Gasto";
            grilla.Columns["NumeroGasto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grilla.Columns["FechaGasto"].Visible = true;
            grilla.Columns["FechaGasto"].HeaderText = "Fecha";
            grilla.Columns["FechaGasto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grilla.Columns["NombreEmpleado"].Visible = true;
            grilla.Columns["NombreEmpleado"].HeaderText = "Empleado";
            grilla.Columns["NombreEmpleado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grilla.Columns["CategoriaGasto"].Visible = true;
            grilla.Columns["CategoriaGasto"].HeaderText = "Categoría";
            grilla.Columns["CategoriaGasto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grilla.Columns["MontoTotal"].Visible = true;
            grilla.Columns["MontoTotal"].HeaderText = "Monto Total";
            grilla.Columns["MontoTotal"].DefaultCellStyle.Format = "C2";

            grilla.Columns["MontoPagado"].Visible = true;
            grilla.Columns["MontoPagado"].HeaderText = "Monto Pagado";
            grilla.Columns["MontoPagado"].DefaultCellStyle.Format = "C2";

            grilla.Columns["EstadoGasto"].Visible = true;
            grilla.Columns["EstadoGasto"].HeaderText = "Estado";
            grilla.Columns["EstadoGasto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grilla.Columns["Detalle"].Visible = true;
            grilla.Columns["Detalle"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        #endregion

        #region 🔥 ACTUALIZAR DATOS (NUEVO)

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);
            int? estado = null;
                Debug.WriteLine($"Valor de Extra: {filtros.Extra}"); // Agrega esta línea para depurar el valor de Extra
            if (filtros.Extra != null && filtros.Extra != "0")

            estado = Convert.ToInt32(filtros.Extra);

            dgv.DataSource = _gastoServicio.ObtenerGastosFiltrados(
                filtros.TextoBuscar,
                estado,
                filtros.FechaDesde,
                filtros.FechaHasta
            );
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

        private void FGastoConsulta_Load(object sender, EventArgs e)
        {
            var opciones = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todos", Valor = "0" }
            };

            foreach (EstadoGasto estado in Enum.GetValues(typeof(EstadoGasto)))
            {
                opciones.Add(new OpcionFiltro
                {
                    Texto = estado.ToString(),
                    Valor = Convert.ToString((int)estado)
                });
            }

            ActivarFiltroCombo("Estado del gasto:", opciones, "Texto", "Valor");

            // 👉 seleccionar "Todos" por defecto
            cbxFiltroOpcional.SelectedValue = Convert.ToString(0);

            ActivarFiltroFechas("Filtrar por fecha");
        }
    }
}
