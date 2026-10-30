using AccesoDatos.Entidades;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Servicios.Helpers.Gasto;
using Servicios.Helpers.OpcionesPagos;
using Servicios.LogicaNegocio.Gasto;
using Servicios.LogicaNegocio.Gasto.DTO;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Gasto
{
    public partial class FGastoABM : FBaseABM
    {
        private readonly IGastoServicio _gastoServicio;
        private readonly long _logeadoId;

        public bool RealizoAlgunaOperacion = false;

        // Si tiene valor, el formulario se usa para pagar un gasto pendiente existente (Confirmar pago).
        private long? _gastoAConfirmar;

        public FGastoABM(long logeadoId)
        {
            InitializeComponent();
            _gastoServicio = new GastoServicio();
            _logeadoId = logeadoId;


            AgregarControlesObligatorios(txtDetalle, "Detalle del Gasto");
            AgregarControlesObligatorios(txtMontoPago, "Monto del Gasto", Validaciones.DecimalMayorACero, Validaciones.DecimalConMaxDecimales(2));
            AgregarControlesObligatorios(cmbCategoriaGasto, "Categoría del Gasto", Validaciones.ComboSinPlaceholder);
            AgregarControlesObligatorios(cmbEstado, "Estado del Gasto", Validaciones.ComboSinPlaceholder);
        }

        /// <summary>Confirmar pago de un gasto pendiente: pide fecha, origen y tipo de pago.</summary>
        public FGastoABM(long logeadoId, long gastoAConfirmar) : this(logeadoId)
        {
            _gastoAConfirmar = gastoAConfirmar;
        }

        private void FGastoABM_Load(object sender, EventArgs e)
        {
            // 🔹 Categorías
            var listaCategoria = Enum.GetValues(typeof(CategoriaGasto))
                .Cast<CategoriaGasto>()
                .Select(x => new
                {
                    Texto = x.ToString().Replace("_", " "),
                    Valor = (int)x
                })
                .ToList();

            cmbCategoriaGasto.DataSource = listaCategoria;
            cmbCategoriaGasto.DisplayMember = "Texto";
            cmbCategoriaGasto.ValueMember = "Valor";

            // 🔹 Estados (solo los válidos)
            var listaEstado = Enum.GetValues(typeof(EstadoGasto))
                .Cast<EstadoGasto>()
                .Where(x => x == EstadoGasto.Pagado || x == EstadoGasto.Pendiente)
                .Select(x => new
                {
                    Texto = x.ToString(),
                    Valor = (int)x
                })
                .ToList();

            cmbEstado.DataSource = listaEstado;
            cmbEstado.DisplayMember = "Texto";
            cmbEstado.ValueMember = "Valor";

            // 🔹 Origen del pago
            cmbOrigen.DataSource = new[]
            {
                new { Texto = "Caja", Valor = (int)OrigenPagoGasto.Caja },
                new { Texto = "Otro", Valor = (int)OrigenPagoGasto.Otro }
            }.ToList();
            cmbOrigen.DisplayMember = "Texto";
            cmbOrigen.ValueMember = "Valor";

            // 🔹 Tipo de pago (la cuenta corriente es de clientes: no se ofrece para gastos)
            cmbTipoPago.DataSource = Enum.GetValues(typeof(TipoDePago))
                .Cast<TipoDePago>()
                .Where(x => x != TipoDePago.CtaCte)
                .Select(x => new
                {
                    Texto = x.ToString(),
                    Valor = (int)x
                })
                .ToList();
            cmbTipoPago.DisplayMember = "Texto";
            cmbTipoPago.ValueMember = "Valor";

            // 🔥 Default
            cmbEstado.SelectedValue = (int)EstadoGasto.Pagado;
            cmbOrigen.SelectedValue = (int)OrigenPagoGasto.Caja;
            cmbTipoPago.SelectedValue = (int)TipoDePago.Efectivo;
            dtpDiaGasto.Enabled = true;
            ActualizarControlesDePago();

            if (_gastoAConfirmar.HasValue)
                PrepararConfirmacionDePago(_gastoAConfirmar.Value);
        }

        private void PrepararConfirmacionDePago(long gastoId)
        {
            var gastoExistente = _gastoServicio.ObtenerGastoPorId(gastoId);

            if (gastoExistente == null)
            {
                Informar("El gasto no existe.", "Atención");
                Close();
                return;
            }

            if (gastoExistente.EstadoGasto != (int)EstadoGasto.Pendiente)
            {
                Informar("Solo se pueden pagar gastos pendientes.", "Atención");
                Close();
                return;
            }

            Text = "Confirmar pago de gasto";
            btnRegistrarGasto.Text = "Confirmar pago";

            txtDetalle.Text = gastoExistente.Detalle;
            txtMontoPago.Text = gastoExistente.MontoTotal.ToString();
            cmbCategoriaGasto.SelectedValue = gastoExistente.CategoriaGasto;

            // Solo se completan los datos del pago; el resto del gasto no se modifica acá.
            txtDetalle.Enabled = false;
            txtMontoPago.Enabled = false;
            cmbCategoriaGasto.Enabled = false;
            cmbEstado.SelectedValue = (int)EstadoGasto.Pagado;
            cmbEstado.Enabled = false;

            dtpDiaGasto.Value = DateTime.Now;
            dtpDiaGasto.Enabled = true;
        }

        private void cmbOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarControlesDePago();
        }

        private static int? ValorCombo(ComboBox combo)
        {
            var item = combo.SelectedItem;
            if (item == null) return null;

            var prop = item.GetType().GetProperty("Valor");
            return prop == null ? null : (int?)prop.GetValue(item);
        }

        /// <summary>Muestra los datos del pago solo si el gasto está Pagado; con origen Caja el tipo de pago es siempre Efectivo.</summary>
        private void ActualizarControlesDePago()
        {
            // Durante el binding (al asignar ValueMember) SelectedValue todavía devuelve el ítem completo,
            // por eso se lee el valor desde SelectedItem.
            var estadoSel = ValorCombo(cmbEstado);
            if (estadoSel == null) return;

            bool esPagado = estadoSel == (int)EstadoGasto.Pagado;

            lblOrigen.Visible = esPagado;
            cmbOrigen.Visible = esPagado;
            lblTipoPago.Visible = esPagado;
            cmbTipoPago.Visible = esPagado;
            lblExtraPago.Visible = esPagado;
            txtExtraPago.Visible = esPagado;

            if (ValorCombo(cmbOrigen) == (int)OrigenPagoGasto.Caja)
            {
                cmbTipoPago.SelectedValue = (int)TipoDePago.Efectivo;
                cmbTipoPago.Enabled = false;
            }
            else
            {
                cmbTipoPago.Enabled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnRegistrarGasto_Click(object sender, EventArgs e)
        {
            await RegistrarGasto();
        }

        private async Task RegistrarGasto()
        {
            if (cmbCategoriaGasto.SelectedValue == null)
            {
                Informar("Seleccione una categoría.","Error");
                //MessageBox.Show("Seleccione una categoría.");
                return;
            }

            if (cmbEstado.SelectedValue == null)
            {
                Informar("Seleccione un estado.","Error");
                //MessageBox.Show("Seleccione un estado.");
                return;
            }

            if (!decimal.TryParse(txtMontoPago.Text, out var monto))
            {
                Informar("Monto inválido.","Error");
                //MessageBox.Show("Monto inválido.");
                return;
            }

            var estado = (int)cmbEstado.SelectedValue;

            DateTime? fechaGasto = null;

            if (estado == (int)EstadoGasto.Pagado)
            {
                fechaGasto = dtpDiaGasto.Value;
            }

            var gasto = new GastoDTO
            {
                IdEmpleado = _logeadoId,
                CategoriaGasto = (int)cmbCategoriaGasto.SelectedValue,
                Detalle = txtDetalle.Text,
                MontoTotal = monto,
                FechaGasto = fechaGasto,
                EstadoGasto = estado
            };

            if (estado == (int)EstadoGasto.Pagado)
            {
                if (cmbOrigen.SelectedValue == null || cmbTipoPago.SelectedValue == null)
                {
                    Informar("Seleccione el origen y el tipo de pago.", "Error");
                    return;
                }

                gasto.OrigenPago = (int)cmbOrigen.SelectedValue;
                gasto.TipoPago = (int)cmbTipoPago.SelectedValue;
                gasto.ExtraDescripcionPago = txtExtraPago.Text;
            }

            using var frmProcesando = new FProcesando();

            try
            {
                btnRegistrarGasto.Enabled = false;

                frmProcesando.Show();
                frmProcesando.ActualizarEstado(_gastoAConfirmar.HasValue ? "Confirmando pago..." : "Registrando gasto...");

                var resultado = await Task.Run(() =>
                    _gastoAConfirmar.HasValue
                        ? _gastoServicio.ConfirmarPago(_gastoAConfirmar.Value, gasto)
                        : _gastoServicio.NuevoGasto(gasto));

                frmProcesando.Close();

                if (resultado.Exitoso)
                {
                    RealizoAlgunaOperacion = true;
                    Exito($"{resultado.Mensaje}");
                    //MessageBox.Show(
                    //    resultado.Mensaje,
                    //    "Éxito",
                    //    MessageBoxButtons.OK,
                    //    MessageBoxIcon.Information);

                    Close();
                }
                else
                {
                    MostrarError(resultado.Mensaje);
                    //MessageBox.Show(
                    //    resultado.Mensaje,
                    //    "Error",
                    //    MessageBoxButtons.OK,
                    //    MessageBoxIcon.Error);
                }
            }
            finally
            {
                btnRegistrarGasto.Enabled = true;
            }
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstado.SelectedValue == null) return;

            var estadoObj = cmbEstado.SelectedItem;

            if (estadoObj == null)
                return;

            var estado = (int)estadoObj.GetType().GetProperty("Valor").GetValue(estadoObj);

            bool esPagado = estado == (int)EstadoGasto.Pagado;

            dtpDiaGasto.Enabled = esPagado;
            ActualizarControlesDePago();

            if (!esPagado)
            {
                // opcional: limpiar visualmente
                dtpDiaGasto.Value = DateTime.Now;
            }
        }

    }
}