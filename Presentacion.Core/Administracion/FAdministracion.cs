using MigraDoc.DocumentObjectModel.Internals;
using Presentacion.Core.Articulo.Marca;
using Presentacion.Core.Categoria;
using Presentacion.Core.Cliente;
using Presentacion.Core.CuentaCorriente;
using Presentacion.Core.Empleado;
using Presentacion.Core.Empleado.Rol;
using Presentacion.Core.Herramientas;
using Presentacion.Core.Movimiento;
using Presentacion.Core.Oferta;
using Presentacion.Core.Producto;
using Presentacion.Core.Producto.Lote;
using Presentacion.Core.Producto.Rubro;
using Presentacion.Core.TipoPago;
using Presentacion.Core.Venta;
using ScottPlot;
using ScottPlot.WinForms;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Caja;
using Servicios.LogicaNegocio.Caja.DTO;
using Servicios.LogicaNegocio.Venta;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Presentacion.Core.Administracion
{
    public partial class FAdministracion : FBase.FBase
    {
        private readonly long _logeadoId;
        private readonly CajaServicio _cajaSerivicio;
        private readonly VentaServicio _ventaServicio;
        List<CajaDTO> todasLasCajas;
        // Definición de la fuente y tamaño personalizados
        private Font _toolTipFont = new Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, GraphicsUnit.Pixel);
        private string _currentToolTipText = string.Empty;

        // Componente nativo de WinForms para mostrar los valores flotantes
        private System.Windows.Forms.ToolTip _winFormsToolTip;

        // Arrays de respaldo para almacenar los datos reales de cada gráfico y leerlos en el MouseMove
        private double[] _xs1, _ys1;
        private double[] _xs2, _ys2;
        private double[] _xs3, _ys3;
        private double[] _xs4, _ys4;
        private double[] _xs5, _ys5;
        private double[] _xs6, _ys6;

        // Índices para controlar el estado y evitar parpadeos molestos al mover el mouse
        private int _lastIndex1 = -1;
        private int _lastIndex2 = -1;
        private int _lastIndex3 = -1;
        private int _lastIndex4 = -1;
        private int _lastIndex5 = -1;
        private int _lastIndex6 = -1;

        public FAdministracion(long logeadoId)
        {
            InitializeComponent();
            _logeadoId = logeadoId;

            _cajaSerivicio = new CajaServicio();
            _ventaServicio = new VentaServicio();

            // Inicializamos el ToolTip con un comportamiento rápido de respuesta
            // Modificá esta parte dentro de tu Constructor actual:
            _winFormsToolTip = new System.Windows.Forms.ToolTip
            {
                InitialDelay = 0,
                ReshowDelay = 0,
                AutomaticDelay = 0,
                UseAnimation = false,
                UseFading = false,
                OwnerDraw = true // <-- ACTIVAR ESTO
            };

            // Suscribir los eventos de dibujo personalizado
            _winFormsToolTip.Popup += WinFormsToolTip_Popup;
            _winFormsToolTip.Draw += WinFormsToolTip_Draw;

            //ComboBox de meses para el filtro del grafico de ganancias por mes
            var meses = DateTimeFormatInfo.CurrentInfo.MonthNames
                .Where(m => !string.IsNullOrEmpty(m))
                .ToArray();

            cbMesGrafico.DataSource = meses;

            cbMesGrafico.DropDownStyle = ComboBoxStyle.DropDown;
            cbMesGrafico.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbMesGrafico.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbMesGrafico.SelectedIndex = DateTime.Now.Month - 1;

            //ComboBox de años para el filtro del grafico de ganancias por mes
            int anioActual = DateTime.Now.Year;

            var anios = Enumerable.Range(anioActual - 9, 10)
                                  .OrderByDescending(a => a)
                                  .ToList();

            cbAñoGraficos.DataSource = anios;

            cbAñoGraficos.DropDownStyle = ComboBoxStyle.DropDown;
            cbAñoGraficos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbAñoGraficos.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sTOCKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fProducto = new FProductoConsulta();
            fProducto.Show();
        }

        private void mARCASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fmarca = new FMarcaConsulta(false);
            fmarca.Show();
        }

        private void cATEGORIASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fCategoria = new FCategoriaConsulta(false);
            fCategoria.Show();
        }

        private void rUBROSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fRubro = new FRubroConsulta(false);
            fRubro.Show();
        }

        private void lISTADOEMPLEADOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fEmpleado = new FEmpleadoConsulta(_logeadoId);
            fEmpleado.Show();
        }

        private void lISTADOCLIENTESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FCliente = new FClienteConsulta();
            FCliente.Show();
        }

        private void cUENTASCORRIENTESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FCuentaCorriente = new FCuentaCorrienteConsulta();
            FCuentaCorriente.Show();
        }

        private void lISTADOOFERTASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FOferta = new FOfertaConsulta();
            FOferta.Show();
        }

        private void aCTIVARDESACTIVARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FActDesac = new FOfertaConsulta(true, "a");
            FActDesac.Show();
        }

        private void lOTESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var flote = new FLoteConsulta();
            flote.Show();
        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            var FMovimiento = new FMovimientoConsulta();
            FMovimiento.Show();
        }

        private void tIPOPAGOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fTipoPago = new FTipoPagoConsulta();
            fTipoPago.Show();
        }

        private void rOLESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FRoles = new FRolConsulta();
            FRoles.Show();
        }

        private void nUEVAOFERTAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fGrupo = new FOfertaGrupoABM();
            fGrupo.ShowDialog();
        }

        private void btnGasto_Click(object sender, EventArgs e)
        {
            var FGasto = new Gasto.FGastoConsulta(_logeadoId);
            FGasto.Show();
        }

        private void btnComprobantes_Click(object sender, EventArgs e)
        {
            var escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var carpeta = Path.Combine(escritorio, "ComprobantesPdf");

            if (!Directory.Exists(carpeta))
            {
                MessageBox.Show(
                    "La carpeta de comprobantes todavía no existe.",
                    "Comprobantes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            using var dialog = new OpenFileDialog
            {
                InitialDirectory = carpeta,
                Filter = "Archivos PDF (*.pdf)|*.pdf",
                Title = "Seleccionar comprobante"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using var visor = new FVisorPDF(dialog.FileName);
                visor.ShowDialog();
            }
        }

        private void FAdministracion_Load(object sender, EventArgs e)
        {
            grafico1();
            grafico2();
            grafico3();
            grafico4();
            grafico5();
            grafico6();

            formsPlot6.Plot.Axes.AutoScale();
            formsPlot6.Refresh();

            // Enlazamos los movimientos de mouse de los controles a nuestra lógica segura
            formsPlot1.MouseMove += FormsPlot1_MouseMove;
            formsPlot2.MouseMove += FormsPlot2_MouseMove;
            formsPlot3.MouseMove += FormsPlot3_MouseMove;
            formsPlot4.MouseMove += FormsPlot4_MouseMove;
            formsPlot5.MouseMove += FormsPlot5_MouseMove;
            formsPlot6.MouseMove += FormsPlot6_MouseMove;
        }

        private void btnFiltrarGraficos_Click(object sender, EventArgs e)
        {
            int añoFiltrado = (int)cbAñoGraficos.SelectedItem;
            int mesFiltrado = cbMesGrafico.SelectedIndex + 1;

            filtrarGraficos(añoFiltrado, mesFiltrado);
        }

        private void filtrarGraficos(int año, int mes)
        {
            grafico1(mes, año);
            grafico3(mes, año);
            grafico4(mes, año);
            grafico5(año);
            grafico6(año);
        }

        private void grafico1(int? mes = null, int? año = null)
        {
            var cajasEnUnMesXyAñoX = _cajaSerivicio.ObtenerCajasPorMesYAño(DateTime.Now.Month, DateTime.Now.Year);

            if (mes.HasValue && año.HasValue)
            {
                int añoBusqueda = año.Value;
                int mesBusqueda = mes.Value;
                cajasEnUnMesXyAñoX = _cajaSerivicio.ObtenerCajasPorMesYAño(mesBusqueda, añoBusqueda);
            }

            double[] gananciasPorCaja = cajasEnUnMesXyAñoX
                .Select(c => (double)c.TotalIngresos)
                .ToArray();

            string[] fechasDeCadaCaja = cajasEnUnMesXyAñoX
            .Select(c => $"A: {c.FechaInicio:dd/MM}\nC: {c.FechaFin?.ToString("dd/MM") ?? "Abierta"}")
            .ToArray();

            double[] numerosCajas = Enumerable
                .Range(1, cajasEnUnMesXyAñoX.Count)
                .Select(i => (double)i)
                .ToArray();

            // Guardamos copia local en variables de clase para el Tooltip
            _xs1 = numerosCajas;
            _ys1 = gananciasPorCaja;
            _lastIndex1 = -1; // Reseteamos puntero

            formsPlot1.Plot.Clear();

            string title = mes.HasValue ?
                $"Cajas en {cbMesGrafico.Items[mes.Value - 1]}" :
                $"Cajas en {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month)}";

            formsPlot1.Plot.Title(title);
            formsPlot1.Plot.XLabel("Fecha de las Cajas");
            formsPlot1.Plot.YLabel("Total Ingresos");

            formsPlot1.Plot.Add.Scatter(numerosCajas, gananciasPorCaja);

            formsPlot1.Plot.Axes.Bottom.SetTicks(numerosCajas, fechasDeCadaCaja);
            formsPlot1.Plot.Axes.AutoScale();
            formsPlot1.Refresh();
        }

        private void grafico2()
        {
            var cajasUltimos31Dias = _cajaSerivicio.ObtenerCajasUltimosXDias(31);

            var cajasPorDia = cajasUltimos31Dias
            .GroupBy(c => c.FechaInicio.Date)
            .OrderBy(g => g.Key)
            .ToList();

            double[] ingresosPorDia = cajasPorDia
                .Select(g => (double)g.Sum(c => c.TotalIngresos))
                .ToArray();

            string[] dias = cajasPorDia.Select(g => g.Key.ToString("dd/MM")).ToArray();

            double[] numerosDias = Enumerable
                .Range(1, cajasPorDia.Count)
                .Select(i => (double)i)
                .ToArray();

            // Guardamos copia local en variables de clase
            _xs2 = numerosDias;
            _ys2 = ingresosPorDia;
            _lastIndex2 = -1;

            formsPlot2.Plot.Clear();

            formsPlot2.Plot.Title("Cajas ultimos 31 dias agrupadas por fecha");
            formsPlot2.Plot.XLabel("Fecha de las Cajas");
            formsPlot2.Plot.YLabel("Total Ingresos");

            formsPlot2.Plot.Add.Scatter(numerosDias, ingresosPorDia);

            formsPlot2.Plot.Axes.Bottom.SetTicks(numerosDias, dias);
            formsPlot2.Plot.Axes.AutoScale();
            formsPlot2.Refresh();
        }

        private void grafico3(int? mes = null, int? año = null)
        {
            var GananciasMesXAñoX = _ventaServicio.ObtenerVentasPorMesYAño(System.DateTime.Now.Month, System.DateTime.Now.Year);

            if (mes.HasValue && año.HasValue)
            {
                int añoBusqueda = año.Value;
                int mesBusqueda = mes.Value;
                GananciasMesXAñoX = _ventaServicio.ObtenerVentasPorMesYAño(mesBusqueda, añoBusqueda);
            }

            var ganaciasAgrupadasPorFecha = GananciasMesXAñoX
                .GroupBy(i => i.FechaVenta.Date)
                .Select(g => new
                {
                    Fecha = g.Key,
                    IngresoTotal = g.Sum(x => x.Total)
                })
                .OrderBy(x => x.Fecha)
                .ToList();

            string[] diaDeLasGanancias = ganaciasAgrupadasPorFecha
                .Select(x => x.Fecha.ToString("dd/MM"))
                .ToArray();

            double[] cantidadesGananciasPorDia = ganaciasAgrupadasPorFecha.Select(x => (double)x.IngresoTotal).ToArray();

            double[] posicionesDiasGanancias = Enumerable.Range(0, diaDeLasGanancias.Length)
                                .Select(i => (double)i)
                                .ToArray();

            // Guardamos copia local en variables de clase
            _xs3 = posicionesDiasGanancias;
            _ys3 = cantidadesGananciasPorDia;
            _lastIndex3 = -1;

            formsPlot3.Plot.Clear();

            string title = mes.HasValue ?
                $"Ganancias diarias en {cbMesGrafico.Items[mes.Value - 1]}" :
                $"Ganancias diarias en {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month)}";

            formsPlot3.Plot.Title(title);
            formsPlot3.Plot.XLabel("Dias");
            formsPlot3.Plot.YLabel("Total Ventas");

            formsPlot3.Plot.Add.Bars(cantidadesGananciasPorDia);

            formsPlot3.Plot.Axes.Bottom.SetTicks(posicionesDiasGanancias, diaDeLasGanancias);
            formsPlot3.Plot.Axes.AutoScale();
            formsPlot3.Refresh();
        }

        private void grafico4(int? mes = null, int? año = null)
        {
            var ventasMesXAñoX = _ventaServicio.ObtenerVentasPorMesYAño(System.DateTime.Now.Month, System.DateTime.Now.Year);

            if (año.HasValue && mes.HasValue)
            {
                int añoBusqueda = año.Value;
                int mesBusqueda = mes.Value;
                ventasMesXAñoX = _ventaServicio.ObtenerVentasPorMesYAño(mesBusqueda, añoBusqueda);
            }

            var ventarAgrupadasPorFecha = ventasMesXAñoX.Select(i => i.FechaVenta.Date)
                .GroupBy(fecha => fecha)
                .Select(g => new
                {
                    Fecha = g.Key,
                    CantidadVentas = g.Count()
                })
                .OrderBy(x => x.Fecha)
                .ToList();

            string[] diaDeLasVentas = ventarAgrupadasPorFecha
                .Select(x => x.Fecha.ToString("dd/MM"))
                .ToArray();

            double[] cantidadesVentasPorDia = ventarAgrupadasPorFecha.Select(x => (double)x.CantidadVentas).ToArray();

            double[] posicionesDiasVentas = Enumerable.Range(0, diaDeLasVentas.Length)
                                .Select(i => (double)i)
                                .ToArray();

            // Guardamos copia local en variables de clase
            _xs4 = posicionesDiasVentas;
            _ys4 = cantidadesVentasPorDia;
            _lastIndex4 = -1;

            formsPlot4.Plot.Clear();

            string title = mes.HasValue ?
                $"Ventas diarias en {cbMesGrafico.Items[mes.Value - 1]}" :
                $"Ventas diarias en {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month)}";

            formsPlot4.Plot.Title(title);
            formsPlot4.Plot.XLabel("Dias");
            formsPlot4.Plot.YLabel("Total Ventas");

            formsPlot4.Plot.Add.Bars(cantidadesVentasPorDia);

            formsPlot4.Plot.Axes.Bottom.SetTicks(posicionesDiasVentas, diaDeLasVentas);
            formsPlot4.Plot.Axes.AutoScale();
            formsPlot4.Refresh();
        }

        private void grafico5(int? año = null)
        {
            var cajasAñoX = _cajaSerivicio.ObtenerLasCajasDeXAño(DateTime.Now.Year);

            if (año.HasValue)
            {
                int añoBusqueda = año.Value;
                cajasAñoX = _cajaSerivicio.ObtenerLasCajasDeXAño(añoBusqueda);
            }

            var fechasYGanaciasAgrupadasPorMeses = cajasAñoX.GroupBy(c => new { c.FechaInicio.Year, c.FechaInicio.Month })
            .Select(g => new
            {
                Fecha = new DateTime(g.Key.Year, g.Key.Month, 1),
                Balance = g.Sum(c => c.TotalIngresos)
            }).ToList();

            string[] meses =
            {
                "Enero", "Febrero", "Marzo", "Abril",
                "Mayo", "Junio", "Julio", "Agosto",
                "Septiembre", "Octubre", "Noviembre", "Diciembre"
            };

            double[] xs = fechasYGanaciasAgrupadasPorMeses
                .Select(x => x.Fecha.Month - 1)
                .Select(m => (double)m)
                .ToArray();

            double[] ganaciasPorMesEjeY = fechasYGanaciasAgrupadasPorMeses
                .Select(x => (double)x.Balance)
                .ToArray();

            string[] mesesPresentesEjeX = fechasYGanaciasAgrupadasPorMeses
                .Select(x => meses[x.Fecha.Month - 1])
                .ToArray();

            // Guardamos copia local en variables de clase
            _xs5 = xs;
            _ys5 = ganaciasPorMesEjeY;
            _lastIndex5 = -1;

            formsPlot5.Plot.Clear();

            string title = año.HasValue ? $"Ganancias en {año.Value}" : $"Ganancias en {DateTime.Now.Year}";

            formsPlot5.Plot.Title(title);
            formsPlot5.Plot.XLabel("Meses");
            formsPlot5.Plot.YLabel("Total Ingresos");

            formsPlot5.Plot.Add.Bars(xs, ganaciasPorMesEjeY);

            formsPlot5.Plot.Axes.Bottom.SetTicks(xs, mesesPresentesEjeX);
            formsPlot5.Plot.Axes.Bottom.TickLabelStyle.Rotation = 45;
            formsPlot5.Plot.Axes.AutoScale();
            formsPlot5.Refresh();
        }

        private void grafico6(int? año = null)
        {
            var ventasAñoX = _ventaServicio.ObtenerVentasPorMesYAño(0, 2026);

            if (año.HasValue)
            {
                int añoBusqueda = año.Value;
                ventasAñoX = _ventaServicio.ObtenerVentasPorMesYAño(0, añoBusqueda);
            }

            var fechasYVentasAgrupadasPorMeses = ventasAñoX.GroupBy(c => new { c.FechaVenta.Year, c.FechaVenta.Month })
            .Select(g => new
            {
                Fecha = new DateTime(g.Key.Year, g.Key.Month, 1),
                QuantityVentas = g.Count() // Corregido tipado interno implícito
            }).ToList();

            string[] mesesVentas =
            {
                "Enero", "Febrero", "Marzo", "Abril",
                "Mayo", "Junio", "Julio", "Agosto",
                "Septiembre", "Octubre", "Noviembre", "Diciembre"
            };

            double[] xsVentas = fechasYVentasAgrupadasPorMeses
                .Select(x => x.Fecha.Month - 1)
                .Select(m => (double)m)
                .ToArray();

            double[] ventasPorMesEjeY = fechasYVentasAgrupadasPorMeses
                .Select(x => (double)x.QuantityVentas)
                .ToArray();

            string[] mesesVentasPresentesEjeX = fechasYVentasAgrupadasPorMeses
                .Select(x => mesesVentas[x.Fecha.Month - 1])
                .ToArray();

            // Guardamos copia local en variables de clase
            _xs6 = xsVentas;
            _ys6 = ventasPorMesEjeY;
            _lastIndex6 = -1;

            formsPlot6.Plot.Clear();

            string title = año.HasValue ? $"Ventas en {año.Value}" : $"Ventas in {DateTime.Now.Year}";

            formsPlot6.Plot.Title(title);
            formsPlot6.Plot.XLabel("Meses");
            formsPlot6.Plot.YLabel("Total Ingresos");

            formsPlot6.Plot.Add.Bars(xsVentas, ventasPorMesEjeY);

            formsPlot6.Plot.Axes.Bottom.SetTicks(xsVentas, mesesVentasPresentesEjeX);
            formsPlot6.Plot.Axes.AutoScale();
            formsPlot6.Refresh();
        }

        private void btnFechaActualGraficos_Click(object sender, EventArgs e)
        {
            grafico1(DateTime.Now.Month, DateTime.Now.Year);
            grafico3(DateTime.Now.Month, DateTime.Now.Year);
            grafico4(DateTime.Now.Month, DateTime.Now.Year);
            grafico5(DateTime.Now.Year);
            grafico6(DateTime.Now.Year);
            cbMesGrafico.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void historialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cv = new FVentaConsulta();
            cv.Show();
        }

        private void historialVentasLibresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cfvl = new FVentaLibreConsulta();
            cfvl.Show();
        }

        private void nuevaVentaLibreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fvl = new FVentaLibre(_logeadoId);
            fvl.Show();
        }

        // =======================================================
        // LÓGICA DE PROXIMIDAD MATEMÁTICA Y RENDERIZADO FLOTANTE
        // =======================================================

        private void EvaluarPosicionMouse(FormsPlot formsPlot, double[] xs, double[] ys, MouseEventArgs e, string prefijo, string formato, ref int lastIndex)
        {
            if (xs == null || ys == null || xs.Length == 0)
            {
                OcultarTooltip(formsPlot, ref lastIndex);
                return;
            }

            Pixel pixelMouse = new Pixel(e.X, e.Y);
            Coordinates coordMouse = formsPlot.Plot.GetCoordinates(pixelMouse);

            int indexMasCercano = -1;
            double minimaDistanciaX = double.MaxValue;

            for (int i = 0; i < xs.Length; i++)
            {
                double distancia = Math.Abs(xs[i] - coordMouse.X);
                if (distancia < minimaDistanciaX)
                {
                    minimaDistanciaX = distancia;
                    indexMasCercano = i;
                }
            }

            if (indexMasCercano != -1 && minimaDistanciaX < 0.4)
            {
                if (lastIndex != indexMasCercano)
                {
                    lastIndex = indexMasCercano;
                    double valorY = ys[indexMasCercano];

                    // 1. Guardamos el texto en la variable global para medirlo en el Popup
                    _currentToolTipText = $"{prefijo}: {valorY.ToString(formato)}";

                    // 2. Lanzamos el ToolTip
                    _winFormsToolTip.Show(_currentToolTipText, formsPlot, e.X + 15, e.Y + 15, 3000);
                }
            }
            else
            {
                OcultarTooltip(formsPlot, ref lastIndex);
            }
        }

        // =======================================================
        // NUEVOS MÉTODOS PARA CONTROLAR FUENTE, TAMAÑO Y DISEÑO
        // =======================================================

        private void WinFormsToolTip_Popup(object sender, PopupEventArgs e)
        {
            // Medimos cuánto va a medir el texto usando la fuente personalizada
            // Sumamos un pequeño margen (Padding) para que no quede pegado a los bordes
            Size tamanoTexto = TextRenderer.MeasureText(_currentToolTipText, _toolTipFont);
            e.ToolTipSize = new Size(tamanoTexto.Width + 12, tamanoTexto.Height + 8);
        }

        private void WinFormsToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            // 1. Dibujamos el fondo (Blanco limpio minimalista)
            e.Graphics.FillRectangle(Brushes.White, e.Bounds);

            // 2. Dibujamos un borde sutil gris oscuro
            using (Pen lapizBorde = new Pen(System.Drawing.Color.FromArgb(180, 180, 180), 1))
            {
                e.Graphics.DrawRectangle(lapizBorde, 0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1);
            }

            // 3. Dibujamos el texto usando nuestra FUENTE y TAMAÑO personalizados
            // TextFormatFlags centra el texto perfectamente en el rectángulo calculado
            TextFormatFlags alineacion = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(e.Graphics, e.ToolTipText, _toolTipFont, e.Bounds, System.Drawing.Color.Black, alineacion);
        }

        private void OcultarTooltip(FormsPlot formsPlot, ref int lastIndex)
        {
            if (lastIndex != -1)
            {
                lastIndex = -1;
                _winFormsToolTip.Hide(formsPlot);
            }
        }

        // Enlaces individuales de cada control hacia la función de evaluación matemática segura
        private void FormsPlot1_MouseMove(object sender, MouseEventArgs e) =>
            EvaluarPosicionMouse(formsPlot1, _xs1, _ys1, e, "Ingreso", "C2", ref _lastIndex1);

        private void FormsPlot2_MouseMove(object sender, MouseEventArgs e) =>
            EvaluarPosicionMouse(formsPlot2, _xs2, _ys2, e, "Total Día", "C2", ref _lastIndex2);

        private void FormsPlot3_MouseMove(object sender, MouseEventArgs e) =>
            EvaluarPosicionMouse(formsPlot3, _xs3, _ys3, e, "Ganancia", "C2", ref _lastIndex3);

        private void FormsPlot4_MouseMove(object sender, MouseEventArgs e) =>
            EvaluarPosicionMouse(formsPlot4, _xs4, _ys4, e, "Cant. Ventas", "N0", ref _lastIndex4);

        private void FormsPlot5_MouseMove(object sender, MouseEventArgs e) =>
            EvaluarPosicionMouse(formsPlot5, _xs5, _ys5, e, "Total Mes", "C2", ref _lastIndex5);

        private void FormsPlot6_MouseMove(object sender, MouseEventArgs e) =>
            EvaluarPosicionMouse(formsPlot6, _xs6, _ys6, e, "Cant. Ventas", "N0", ref _lastIndex6);

        private void cbMesGrafico_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Evaluamos de forma segura si ya hay un año seleccionado y lo extraemos
            if (cbAñoGraficos.SelectedItem is int añoFiltrado)
            {
                int mesFiltrado = cbMesGrafico.SelectedIndex + 1;

                // Solo ejecutamos el filtro si el mes es válido
                if (mesFiltrado > 0)
                {
                    filtrarGraficos(añoFiltrado, mesFiltrado);
                }
            }
            // Si es null (como en el arranque), el 'if' no se cumple y no hace nada,
            // evitando que la aplicación se rompa.
        }
        private void cbAñoGraficos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Validamos de forma segura que el objeto seleccionado sea un entero válido
            if (cbAñoGraficos.SelectedItem is int añoFiltrado)
            {
                int mesFiltrado = cbMesGrafico.SelectedIndex + 1;

                // Nos aseguramos de que el mes también tenga una selección válida
                if (mesFiltrado > 0)
                {
                    filtrarGraficos(añoFiltrado, mesFiltrado);
                }
            }
        }
    }
}