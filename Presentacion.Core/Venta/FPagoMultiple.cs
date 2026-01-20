using Presentacion.Core.Venta.TipoPago;
using Servicios.Helpers;
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
    public partial class FPagoMultiple : Form
    {
        // Resultado final accesible por el caller
        public List<FormaPago> ResultPagos { get; private set; } = new List<FormaPago>();

        public int CantidadPagos { get; private set; } = 0;
        private decimal TotalVenta { get; set; } = 0m;

        // Para reutilizar el selector de forma de pago si lo necesitás (como en tu proyecto)
        private readonly bool _incluirCtaCte;
        private readonly long? _idCliente;
        private DatosVenta _dv;

        // Contenedores dinámicos de controles por índice (0..CantidadPagos-1)
        private readonly List<TextBox> _txtMontos = new List<TextBox>();
        private readonly List<Button> _btnTipoPago = new List<Button>();
        private readonly List<Label> _lblTipoPago = new List<Label>();
        private readonly List<TipoDePago?> _tipoSeleccionado = new List<TipoDePago?>();

        // Panel donde se agregan las filas
        private FlowLayoutPanel flowPanel;

        // Controles fijos
        private Label lblTotal;
        private Label lblRestante;
        private Button btnAceptar;
        private Button btnCancelar;
        private System.Windows.Forms.Panel panelBottom;

        public FPagoMultiple()
        {
            InitializerComponent();
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

        private void InitializerComponent()
        {
            // Configuración básica del form
            this.Text = "Pagos múltiples";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.AutoScaleMode = AutoScaleMode.Font;

            flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = false,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(8),
                AutoScroll = true
            };
            this.Controls.Add(flowPanel);

            // Panel inferior para totales y botones
            panelBottom = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Bottom,
                Height = 64,
                Padding = new Padding(8),
                BackColor = SystemColors.ControlLight
            };

            lblTotal = new Label
            {
                AutoSize = true,
                Font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold),
                Location = new Point(8, 12)
            };
            panelBottom.Controls.Add(lblTotal);

            lblRestante = new Label
            {
                AutoSize = true,
                Font = new Font(FontFamily.GenericSansSerif, 9),
                Location = new Point(200, 12)
            };
            panelBottom.Controls.Add(lblRestante);

            btnAceptar = new Button
            {
                Text = "Aceptar",
                Width = 100,
                Height = 30
            };
            btnAceptar.Click += BtnAceptar_Click;
            panelBottom.Controls.Add(btnAceptar);

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Width = 100,
                Height = 30
            };
            btnCancelar.Click += BtnCancelar_Click;
            panelBottom.Controls.Add(btnCancelar);

            this.Controls.Add(panelBottom);

            // Eventos de layout
            this.Load += FPagoMultiple_Load;
            this.Resize += FPagoMultiple_Resize;
            panelBottom.Resize += PanelBottom_Resize;
        }

        private void FPagoMultiple_Load(object sender, EventArgs e)
        {
            // Validaciones de cantidad:
            if (CantidadPagos > 7)
            {
                MessageBox.Show("No se permiten más de 7 formas de pago. Operación cancelada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }
            else if (CantidadPagos > 3)
            {
                MessageBox.Show("No es recomendable usar más de 3 formas de pago. ¿Desea continuar?", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Mostrar total
            lblTotal.Text = $"Total: {TotalVenta.ToString("C2")}";
            // Inicial restante
            lblRestante.Text = $"Restante: {TotalVenta.ToString("C2")}";

            // Construir filas dinámicas
            BuildRows();

            // Ajuste de tamaño para intentar mostrar todas las filas sin cortar.
            // Calculamos altura necesaria y limitamos a un máximo razonable (por ejemplo 600px).
            int filaAltura = 42;
            int neededHeight = Math.Min(600, 120 + CantidadPagos * filaAltura);
            int bottomPanelHeight = panelBottom.Height + 16;
            this.ClientSize = new Size(Math.Max(600, this.ClientSize.Width), neededHeight + bottomPanelHeight);

            // Ajusto la altura del flowPanel para ocupar el espacio superior
            flowPanel.Height = this.ClientSize.Height - bottomPanelHeight - 8;

            // Alinear botones
            PositionButtons();

            UpdateRestante();
        }

        private void PanelBottom_Resize(object sender, EventArgs e)
        {
            PositionButtons();
        }

        private void FPagoMultiple_Resize(object sender, EventArgs e)
        {
            // Ajusta el ancho de cada fila para que coincida con el cliente
            foreach (Control row in flowPanel.Controls)
            {
                row.Width = this.ClientSize.Width - 24;
            }

            // Reposicionar botones también
            PositionButtons();
        }

        private void PositionButtons()
        {
            // Coloco los botones alineados a la derecha dentro del panelBottom
            int paddingRight = 12;
            int gap = 8;
            int x = panelBottom.ClientSize.Width - paddingRight - btnCancelar.Width;
            btnCancelar.Location = new Point(x, (panelBottom.ClientSize.Height - btnCancelar.Height) / 2);
            x -= (btnAceptar.Width + gap);
            btnAceptar.Location = new Point(x, (panelBottom.ClientSize.Height - btnAceptar.Height) / 2);
        }

        private void BuildRows()
        {
            _txtMontos.Clear();
            _btnTipoPago.Clear();
            _lblTipoPago.Clear();
            _tipoSeleccionado.Clear();

            flowPanel.Controls.Clear();

            for (int i = 0; i < CantidadPagos; i++)
            {
                int idx = i; // <<--- captura local obligatoria

                // Panel fila
                var panelRow = new System.Windows.Forms.Panel
                {
                    Width = this.ClientSize.Width - 24,
                    Height = 36,
                    Margin = new Padding(0, 0, 0, 6)
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
                    Text = "0.00",
                    TextAlign = HorizontalAlignment.Right
                };
                // Usar idx en las lambdas
                txtMonto.TextChanged += (s, e) => TxtMonto_TextChanged(s, e, idx);
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
                btnTipo.Click += (s, e) => BtnTipo_Click(s, e, idx); // uso de idx
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

                // Inicializamos sel tipo como nulo
                _tipoSeleccionado.Add(null);

                flowPanel.Controls.Add(panelRow);
            }
        }
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
                // Reducir el valor del textbox editado para que la suma quede en TotalVenta
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

            // SI queda exactamente 1 cero y ese cero es el ÚLTIMO índice -> autocompletar
            if (ceros == 1 && ultimoIndiceCero == _txtMontos.Count - 1)
            {
                decimal sumaSinUltimo = valores.Sum() - valores[ultimoIndiceCero];
                decimal restante = TotalVenta - sumaSinUltimo;
                if (restante < 0) restante = 0m;

                // Asignar al último textbox
                _txtMontos[ultimoIndiceCero].Text = restante.ToString("N2");
            }

            // Actualizar restante y UI
            UpdateRestante();
        }
        private void TxtMonto_TextChanged(object sender, EventArgs e, int index)
        {
            // Intentar parsear; no lanzar excepción
            decimal monto = 0m;
            var txt = sender as TextBox;
            if (txt == null) return;

            var ok = decimal.TryParse(txt.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out monto);
            if (!ok)
            {
                // si no es válido, no hacemos nada por ahora; al perder foco se formateará
            }

            // Actualizamos restante
            UpdateRestante();
        }

        private void FormatTextBoxDecimal(TextBox txt)
        {
            if (txt == null) return;
            if (decimal.TryParse(txt.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var v))
            {
                txt.Text = v.ToString("N2");
            }
            else
            {
                txt.Text = "0.00";
            }
        }

        private void BtnTipo_Click(object sender, EventArgs e, int index)
        {
            // Prepara la lista actual de pagos para pasar al selector si lo necesitás
            var tmpPagos = new List<FormaPago>();
            for (int i = 0; i < CantidadPagos; i++)
            {
                decimal monto = 0m;
                decimal.TryParse(_txtMontos[i].Text, NumberStyles.Number, CultureInfo.CurrentCulture, out monto);
                var fp = new FormaPago { Numero = i + 1, Monto = monto, TipoDePago = null };
                tmpPagos.Add(fp);
            }
            // Abrir selector (reutiliza tu dialog existente). Ajustá el constructor si lo tenés distinto.
            using var fTipo = new FTipoPagoSeleccionEnVenta(_dv, tmpPagos, index, _idCliente);
            if (fTipo.ShowDialog() == DialogResult.OK)
            {
                var tipo = fTipo.tipoPagoSeleccionado;
                if (index < 0 || index >= _tipoSeleccionado.Count)
                {
                    MessageBox.Show("Índice inválido al asignar forma de pago.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _tipoSeleccionado[index] = tipo;

                // Pongo el texto en label
                _lblTipoPago[index].Text = tipo.ToString();
                // Mantengo el botón como "Cambiar"
                _btnTipoPago[index].Text = "Cambiar forma";
            }
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
            // Permitimos restante negativo en cálculo (lo mostramos como 0 para evitar signos raros)
            if (restante < 0) restante = 0m;

            lblRestante.Text = $"Restante: {restante.ToString("C2")}";
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            // Validaciones:
            // - Suma de montos no puede superar total
            // - Debe existir al menos un monto > 0
            // - Para cada monto > 0, debe existir forma de pago asignada (label != "No asignada")

            decimal suma = 0m;
            for (int i = 0; i < _txtMontos.Count; i++)
            {
                if (decimal.TryParse(_txtMontos[i].Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var v))
                    suma += v;
            }

            if (suma <= 0m)
            {
                MessageBox.Show("Ingrese al menos un monto mayor a cero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (suma > TotalVenta)
            {
                MessageBox.Show("La suma de los montos no puede superar el total de la venta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Construyo ResultPagos y valido formas asignadas
            ResultPagos.Clear();
            for (int i = 0; i < CantidadPagos; i++)
            {
                decimal monto = 0m;
                decimal.TryParse(_txtMontos[i].Text, NumberStyles.Number, CultureInfo.CurrentCulture, out monto);

                // Si monto > 0, debo tener forma de pago asignada
                if (monto > 0 && (_tipoSeleccionado.Count <= i || _tipoSeleccionado[i] == null))
                {
                    MessageBox.Show($"Debe seleccionar una forma de pago para el Pago {i + 1} (monto {monto:C2}).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var forma = new FormaPago
                {
                    Numero = i + 1,
                    Monto = monto,
                    TipoDePago = _tipoSeleccionado[i] ?? default
                };

                ResultPagos.Add(forma);
            }

            // Todo ok
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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
