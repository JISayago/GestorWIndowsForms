
using Presentacion.Core.Venta.HelpersVenta;
using Presentacion.Core.Venta.TipoPago;
using Servicios.Helpers.OpcionesPagos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Venta
{
    public partial class FPagoMultiple : FBase.FBase
    {
        private const int MaxPagos = 7;
        private const int PagosRecomendados = 3;
        private const int AlturaFila = 36;
        private const int MargenFila = 6;
        private const int AlturaMaxForm = 600;
        private const int AnchoMinForm = 600;

        // Resultado final accesible por el caller
        public List<FormaPago> ResultPagos { get; private set; } = new List<FormaPago>();

        public int CantidadPagos { get; private set; } = 0;
        private decimal TotalVenta { get; set; } = 0m;

        // Para reutilizar el selector de forma de pago
        private readonly bool _incluirCtaCte;
        private readonly long? _idCliente;
        private DatosVenta _dv;

        // Controles dinámicos por índice (0..CantidadPagos-1)
        private readonly List<TextBox> _txtMontos = new List<TextBox>();
        private readonly List<Button> _btnTipoPago = new List<Button>();
        private readonly List<Label> _lblTipoPago = new List<Label>();
        private readonly List<TipoDePago?> _tipoSeleccionado = new List<TipoDePago?>();
        private readonly List<string> _datosExtra = new List<string>();

        // Los controles fijos (flowPanel, panelBottom, lblTotal, lblRestante,
        // btnAceptar, btnCancelar) se declaran en FPagoMultiple.Designer.cs

        public FPagoMultiple()
        {
            InitializeComponent();
        }

        // Constructor recomendado: pasá cantidad y total
        public FPagoMultiple(int cantidadPagos, decimal totalVenta, DatosVenta dv, long? idCliente = null) : this()
        {
            CantidadPagos = Math.Max(1, cantidadPagos);
            TotalVenta = totalVenta;
            _dv = dv;
            _incluirCtaCte = dv.IncluirCtaCte;
            _idCliente = idCliente;
        }

        // ------------------------------------------------------------------
        // Teclas (FBase intercepta Escape/Enter y delega en estos métodos)
        // ------------------------------------------------------------------
        protected override void EjecutarEnter()
        {
            // En un monto: pasar al siguiente control para que dispare el Leave (formateo y autocompletado)
            if (ActiveControl is TextBox txt && _txtMontos.Contains(txt))
            {
                SelectNextControl(txt, true, true, true, false);
                return;
            }

            // En un botón: pulsarlo
            if (ActiveControl is Button btn)
            {
                btn.PerformClick();
                return;
            }

            BtnAceptar_Click(btnAceptar, EventArgs.Empty);
        }

        // ------------------------------------------------------------------
        // Load / layout
        // ------------------------------------------------------------------
        private void FPagoMultiple_Load(object sender, EventArgs e)
        {
            if (CantidadPagos > MaxPagos)
            {
                Advertir($"No se permiten más de {MaxPagos} formas de pago. Operación cancelada.");
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            if (CantidadPagos > PagosRecomendados &&
                !Confirmar($"No es recomendable usar más de {PagosRecomendados} formas de pago. ¿Desea continuar?"))
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            lblTotal.Text = $"Total: {TotalVenta.ToString("C2")}";

            BuildRows();
            AjustarTamanio();
            UpdateRestante();
        }

        private void AjustarTamanio()
        {
            // Altura necesaria para mostrar todas las filas sin scroll (con tope razonable)
            int altoFilas = CantidadPagos * (AlturaFila + MargenFila);
            int alto = Math.Min(AlturaMaxForm, flowPanel.Padding.Vertical + altoFilas + panelBottom.Height);

            ClientSize = new Size(Math.Max(AnchoMinForm, ClientSize.Width), alto);
            AjustarAnchoFilas();
            CenterToParent();
        }

        private void flowPanel_SizeChanged(object sender, EventArgs e)
        {
            AjustarAnchoFilas();
        }

        private void AjustarAnchoFilas()
        {
            int ancho = flowPanel.ClientSize.Width - flowPanel.Padding.Horizontal;
            if (ancho <= 0) return;

            foreach (Control row in flowPanel.Controls)
                row.Width = ancho;
        }

        private void BuildRows()
        {
            _txtMontos.Clear();
            _btnTipoPago.Clear();
            _lblTipoPago.Clear();
            _tipoSeleccionado.Clear();
            _datosExtra.Clear();

            flowPanel.SuspendLayout();
            flowPanel.Controls.Clear();

            for (int i = 0; i < CantidadPagos; i++)
            {
                int idx = i; // captura local obligatoria para las lambdas

                // Panel fila (el tema de FBase lo pinta en OnLoad)
                var panelRow = new System.Windows.Forms.Panel
                {
                    Height = AlturaFila,
                    Margin = new Padding(0, 0, 0, MargenFila)
                };

                // Etiqueta "Pago X"
                var lblPago = new Label
                {
                    Text = $"Pago {idx + 1}:",
                    Location = new Point(4, 8),
                    AutoSize = true
                };
                panelRow.Controls.Add(lblPago);

                // TextBox monto
                var txtMonto = new TextBox
                {
                    Name = $"txtMonto_{idx}",
                    Width = 120,
                    Location = new Point(80, 4),
                    Text = 0m.ToString("N2"),
                    TextAlign = HorizontalAlignment.Right
                };
                txtMonto.TextChanged += (s, e) => UpdateRestante();
                txtMonto.Leave += (s, e) => TxtMonto_Leave_AutoFillLast(s, e, idx);
                panelRow.Controls.Add(txtMonto);
                _txtMontos.Add(txtMonto);

                // Botón seleccionar forma de pago
                var btnTipo = new Button
                {
                    Name = $"btnTipo_{idx}",
                    Text = "Seleccionar forma",
                    Width = 140,
                    Height = 24,
                    Location = new Point(210, 6)
                };
                btnTipo.Click += (s, e) => BtnTipo_Click(s, e, idx);
                panelRow.Controls.Add(btnTipo);
                _btnTipoPago.Add(btnTipo);

                // Label que muestra la forma seleccionada
                var lblTipo = new Label
                {
                    Name = $"lblTipo_{idx}",
                    Text = "No asignada",
                    AutoSize = true,
                    Location = new Point(360, 9)
                };
                panelRow.Controls.Add(lblTipo);
                _lblTipoPago.Add(lblTipo);

                // Forma de pago inicial: sin asignar
                _tipoSeleccionado.Add(null);
                _datosExtra.Add(null);

                flowPanel.Controls.Add(panelRow);
            }

            flowPanel.ResumeLayout(true);
        }

        // ------------------------------------------------------------------
        // Montos
        // ------------------------------------------------------------------
        private void TxtMonto_Leave_AutoFillLast(object sender, EventArgs e, int index)
        {
            var txt = sender as TextBox;
            if (txt == null) return;

            // Formatear el textbox que editamos
            FormatTextBoxDecimal(txt);

            // Parsear todos los montos actuales
            decimal[] valores = new decimal[_txtMontos.Count];
            int ceros = 0;
            int ultimoIndiceCero = -1;
            for (int i = 0; i < _txtMontos.Count; i++)
            {
                if (!decimal.TryParse(_txtMontos[i].Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var v))
                    v = 0m;
                valores[i] = v;
                if (v == 0m)
                {
                    ceros++;
                    ultimoIndiceCero = i;
                }
            }

            // Recortamos el valor editado si la suma supera el total
            decimal sumaParcial = valores.Sum();
            if (sumaParcial > TotalVenta)
            {
                decimal excedente = sumaParcial - TotalVenta;
                decimal nuevoValor = valores[index] - excedente;
                if (nuevoValor < 0) nuevoValor = 0m;
                valores[index] = nuevoValor;
                _txtMontos[index].Text = nuevoValor.ToString("N2");

                // Recalcular ceros/último índice (posible que nuevoValor sea 0)
                ceros = 0;
                ultimoIndiceCero = -1;
                for (int i = 0; i < valores.Length; i++)
                    if (valores[i] == 0m) { ceros++; ultimoIndiceCero = i; }
            }

            // Si queda exactamente 1 cero y ese cero es el ÚLTIMO índice -> autocompletar
            if (ceros == 1 && ultimoIndiceCero == _txtMontos.Count - 1)
            {
                decimal sumaSinUltimo = valores.Sum() - valores[ultimoIndiceCero];
                decimal restante = TotalVenta - sumaSinUltimo;
                if (restante < 0) restante = 0m;

                _txtMontos[ultimoIndiceCero].Text = restante.ToString("N2");
            }

            UpdateRestante();
        }

        private void FormatTextBoxDecimal(TextBox txt)
        {
            if (txt == null) return;

            if (decimal.TryParse(txt.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var v))
                txt.Text = v.ToString("N2");
            else
                txt.Text = 0m.ToString("N2");
        }

        private void UpdateRestante()
        {
            decimal suma = 0m;
            for (int i = 0; i < _txtMontos.Count; i++)
            {
                if (decimal.TryParse(_txtMontos[i].Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var v))
                    suma += v;
            }

            var restante = TotalVenta - suma;
            // Se muestra 0 en vez de negativo para evitar signos raros
            if (restante < 0) restante = 0m;

            lblRestante.Text = $"Restante: {restante.ToString("C2")}";
        }

        // ------------------------------------------------------------------
        // Forma de pago
        // ------------------------------------------------------------------
        private void BtnTipo_Click(object sender, EventArgs e, int index)
        {
            // Lista actual de pagos para pasar al selector
            var tmpPagos = new List<FormaPago>();
            for (int i = 0; i < CantidadPagos; i++)
            {
                decimal monto = 0m;
                decimal.TryParse(_txtMontos[i].Text, NumberStyles.Number, CultureInfo.CurrentCulture, out monto);
                tmpPagos.Add(new FormaPago { Numero = i + 1, Monto = monto, TipoDePago = null, DatosExtra = "Sin especificar" });
            }

            using var fTipo = new FTipoPagoSeleccionEnVenta(_dv, tmpPagos, index, _idCliente);
            if (fTipo.ShowDialog(this) != DialogResult.OK) return;

            if (index < 0 || index >= _tipoSeleccionado.Count)
            {
                MostrarError("Índice inválido al asignar la forma de pago.");
                return;
            }

            var tipo = fTipo.tipoPagoSeleccionado;
            _tipoSeleccionado[index] = tipo;
            _datosExtra[index] = fTipo.datosExtra ?? "Sin especificar";

            _lblTipoPago[index].Text = $"{tipo.ToString()} ({_datosExtra[index]})";
            _btnTipoPago[index].Text = "Cambiar forma";
        }

        // ------------------------------------------------------------------
        // Aceptar / Cancelar
        // ------------------------------------------------------------------
        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            // Validaciones:
            // - Suma de montos no puede superar total
            // - Debe existir al menos un monto > 0
            // - Para cada monto > 0, debe existir forma de pago asignada

            decimal suma = 0m;
            for (int i = 0; i < _txtMontos.Count; i++)
            {
                if (decimal.TryParse(_txtMontos[i].Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var v))
                    suma += v;
            }

            if (suma <= 0m)
            {
                Advertir("Ingrese al menos un monto mayor a cero.");
                return;
            }

            if (suma > TotalVenta)
            {
                Advertir("La suma de los montos no puede superar el total de la venta.");
                return;
            }

            // Construyo ResultPagos y valido formas asignadas
            ResultPagos.Clear();
            for (int i = 0; i < CantidadPagos; i++)
            {
                decimal monto = 0m;
                decimal.TryParse(_txtMontos[i].Text, NumberStyles.Number, CultureInfo.CurrentCulture, out monto);

                if (monto > 0 && (_tipoSeleccionado.Count <= i || _tipoSeleccionado[i] == null))
                {
                    ResultPagos.Clear();
                    Advertir($"Debe seleccionar una forma de pago para el Pago {i + 1} (monto {monto:C2}).");
                    return;
                }

                ResultPagos.Add(new FormaPago
                {
                    Numero = i + 1,
                    Monto = monto,
                    TipoDePago = _tipoSeleccionado[i] ?? default,
                    DatosExtra = _datosExtra[i] ?? "Sin especificar"
                });
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // Si por alguna razón tenés que mapear desde label, lo dejo por compatibilidad:
        private TipoDePago ParseTipoFromLabel(string label)
        {
            if (string.IsNullOrWhiteSpace(label) || label == "No asignada")
                return default;

            try
            {
                if (Enum.TryParse(typeof(TipoDePago), label, out var val))
                    return (TipoDePago)val;
            }
            catch { }

            return default;
        }
    }
}