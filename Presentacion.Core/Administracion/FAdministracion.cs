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
using Servicios.LogicaNegocio.Sistema.Administracion;
using Servicios.LogicaNegocio.Sistema.Administracion.DTO;
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
        // ==========================================
        // DEPENDENCIAS Y SERVICIOS DE CAPA DE NEGOCIO
        // ==========================================
        private readonly long _logeadoId;
        private readonly CajaServicio _cajaSerivicio;
        private readonly VentaServicio _ventaServicio;
        List<CajaDTO> todasLasCajas;
        private readonly AdministracionGraficosServicios _graficoServicio;
        private GraficosAdministracionDTO _graficosDTO;
        private int? _anioCargado;
        private int? _mesCargado;
        private bool _graficosInicializados;

        private bool _grafico1Construido;
        private bool _grafico2Construido;
        private bool _grafico3Construido;
        private bool _grafico4Construido;
        private bool _grafico5Construido;
        private bool _grafico6Construido;

        private TableLayoutPanel _tlpKpis;
        private System.Windows.Forms.Label _lblKpiTotalMes;
        private System.Windows.Forms.Label _lblKpiCantVentas;
        private System.Windows.Forms.Label _lblKpiTicketPromedio;
        private System.Windows.Forms.Label _lblKpiVariacion;

        private static readonly ScottPlot.Color ColorIngresos = ScottPlot.Color.FromHex("#2E7D32");
        private static readonly ScottPlot.Color ColorEgresos = ScottPlot.Color.FromHex("#C62828");
        private static readonly ScottPlot.Color ColorMonto = ScottPlot.Color.FromHex("#2E7D32");
        private static readonly ScottPlot.Color ColorCantidad = ScottPlot.Color.FromHex("#1565C0");
        private static readonly ScottPlot.Color ColorTendencia = ScottPlot.Color.FromHex("#291a3e");
        private static readonly ScottPlot.Color ColorComparativo = ScottPlot.Color.FromHex("#F57F17"); // ámbar para comparativo
        
        // Constantes de umbral de proximidad para tooltips (unificadas)
        private const double TooltipThreshold = 0.4;
        private const double TooltipThresholdBars = 1.2; // Para gráficos de barras agrupadas (gráfico 1)
        
        // Cache para datos que no dependen del filtro mes/año
        private List<CajaDTO> _cajas31DiasCache;

        // ==========================================
        // CONFIGURACIÓN DE TOOLTIPS PERSONALIZADOS
        // ==========================================
        private Font _toolTipFont = new Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, GraphicsUnit.Pixel);
        private string _currentToolTipText = string.Empty;
        private System.Windows.Forms.ToolTip _winFormsToolTip;

        // ==========================================
        // ENMASCARAMIENTO DE ESTADOS MATEMÁTICOS (SCOTTPLOT)
        // Vectores globales que almacenan los ejes X e Y actuales de cada gráfico para el motor de proximidad.
        // ==========================================
        private double[] _xs1, _ys1; // Gráfico 1: Ingresos vs Egresos por caja
        private double[] _egresos1;
        private double[] _xs2, _ys2; // Gráfico 2: Cajas Últimos 31 Días
        private double[] _xs3, _ys3; // Gráfico 3: Ventas Diarias del Mes
        private double[] _xs3prev, _ys3prev; // Gráfico 3: Mes anterior para comparativo
        private double[] _xs4, _ys4; // Gráfico 4: Cantidad de Ventas Diarias
        private double[] _xs4prev, _ys4prev; // Gráfico 4: Mes anterior para comparativo
        private double[] _xs5, _ys5; // Gráfico 5: Balance Anual Combinado
        private double[] _xs6, _ys6; // Gráfico 6: Volumen de Ventas Anual

        // Índices del último punto trackeado por el mouse (Evitan el parpadeo y la re-ejecución del render de Windows)
        private int _lastIndex1 = -1;
        private int _lastIndex2 = -1;
        private int _lastIndex3 = -1;
        private int _lastIndex4 = -1;
        private int _lastIndex5 = -1;
        private int _lastIndex6 = -1;

        /// <summary>
        /// Constructor del Formulario Administrativo
        /// </summary>
        public FAdministracion(long logeadoId)
        {
            InitializeComponent();
            DibujarBotones();
            InicializarPanelKpis();

            _logeadoId = logeadoId;

            // Inicialización de la lógica de negocio
            _cajaSerivicio = new CajaServicio();
            _ventaServicio = new VentaServicio();
            _graficoServicio = new AdministracionGraficosServicios();

            // Configuración inicial del ToolTip nativo con retardos en cero para respuesta inmediata
            _winFormsToolTip = new System.Windows.Forms.ToolTip
            {
                InitialDelay = 0,
                ReshowDelay = 0,
                AutomaticDelay = 0,
                UseAnimation = false,
                UseFading = false,
                OwnerDraw = true // Habilita el control total del dibujo visual (fuentes, bordes)
            };

            // Enlace de eventos para el redibujado estético del ToolTip
            _winFormsToolTip.Popup += WinFormsToolTip_Popup;
            _winFormsToolTip.Draw += WinFormsToolTip_Draw;

            // Configuración estricta de UI: DropDownList impide que el usuario tipee texto libre en los filtros cronológicos
            cbMesGrafico.DropDownStyle = ComboBoxStyle.DropDownList;
            cbAñoGraficos.DropDownStyle = ComboBoxStyle.DropDownList;

            tabControlGraficoArriba.SelectedIndexChanged += TabControlGraficos_SelectedIndexChanged;
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;

            // Ejecutamos la carga inicial dinámica analizando la base de datos de manera segura
            //InicializarFiltrosCronologicos();

        }

        /// <summary>
        /// Analiza de forma segura los registros históricos en la BD para cargar los años disponibles.
        /// Aplica patrones de resguardo para sistemas nuevos sin transacciones.
        /// </summary>
        private void InicializarFiltrosCronologicos()
        {
            // DESVINCULACIÓN TEMPORAL DE EVENTOS:
            // Evita loops infinitos de refresco visual e hilos bloqueados mientras se manipulan los DataSources por código.
            cbAñoGraficos.SelectedIndexChanged -= cbAñoGraficos_SelectedIndexChanged;
            cbMesGrafico.SelectedIndexChanged -= cbMesGrafico_SelectedIndexChanged;

            // Consulta a la base de datos de los años que registran actividad de cajas
            var aniosDisponibles = _cajaSerivicio.ObtenerAniosConCajas();

            // BLOQUE DE RESGUARDO (FALLBACK): Si el sistema está vacío (App nueva), pre-cargamos el año corriente en curso
            if (aniosDisponibles.Count == 0)
            {
                aniosDisponibles.Add(DateTime.Now.Year);
            }

            // Enlace de datos al combo de años
            cbAñoGraficos.DataSource = aniosDisponibles;

            // Seleccionamos cronológicamente el año más reciente de la lista y cargamos sus meses correspondientes
            int anioInicial = aniosDisponibles.First();
            ActualizarComboMeses(anioInicial);

            // RE-VINCULACIÓN DE EVENTOS: Una vez que la UI está armada de forma segura, volvemos a escuchar al usuario
            cbAñoGraficos.SelectedIndexChanged += cbAñoGraficos_SelectedIndexChanged;
            cbMesGrafico.SelectedIndexChanged += cbMesGrafico_SelectedIndexChanged;
        }
        private async Task CargarDashboardAsync(int año, int mes)
        {
            if (_graficosDTO != null &&
                _anioCargado == año &&
                _mesCargado == mes)
            {
                return;
            }

            _graficosDTO = await Task.Run(() =>
                _graficoServicio.ObtenerDatos(año, mes));

            _anioCargado = año;
            _mesCargado = mes;

            ActualizarKpis();
        }

        private void InicializarPanelKpis()
        {
            var colorTexto = System.Drawing.Color.FromArgb(31, 26, 43);
            var colorTitulo = System.Drawing.Color.FromArgb(100, 100, 100);

            tlpBaseNivel1.RowCount = 3;
            tlpBaseNivel1.RowStyles.Clear();
            tlpBaseNivel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            tlpBaseNivel1.RowStyles.Add(new RowStyle(SizeType.Percent, 42F));
            tlpBaseNivel1.RowStyles.Add(new RowStyle(SizeType.Percent, 58F));
            tlpBaseNivel1.SetRow(tlpArribaNivel2, 1);
            tlpBaseNivel1.SetRow(tlpBajoNivel2, 2);

            _tlpKpis = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                BackColor = System.Drawing.Color.FromArgb(245, 245, 245),
                Padding = new Padding(8, 4, 8, 4)
            };

            for (int i = 0; i < 4; i++)
                _tlpKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            _lblKpiTotalMes = CrearLabelKpi("Total del mes", colorTitulo, colorTexto);
            _lblKpiCantVentas = CrearLabelKpi("Ventas del mes", colorTitulo, colorTexto);
            _lblKpiTicketPromedio = CrearLabelKpi("Ticket promedio", colorTitulo, colorTexto);
            _lblKpiVariacion = CrearLabelKpi("vs mes anterior", colorTitulo, colorTexto);

            _tlpKpis.Controls.Add(_lblKpiTotalMes, 0, 0);
            _tlpKpis.Controls.Add(_lblKpiCantVentas, 1, 0);
            _tlpKpis.Controls.Add(_lblKpiTicketPromedio, 2, 0);
            _tlpKpis.Controls.Add(_lblKpiVariacion, 3, 0);

            tlpBaseNivel1.Controls.Add(_tlpKpis, 0, 0);
        }

        private static System.Windows.Forms.Label CrearLabelKpi(string titulo, System.Drawing.Color colorTitulo, System.Drawing.Color colorValor)
        {
            return new System.Windows.Forms.Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold),
                ForeColor = colorValor,
                Text = $"{titulo}\r\n—"
            };
        }

        private void ActualizarKpis()
        {
            if (_graficosDTO == null)
                return;

            var ventas = _graficosDTO.VentasMes ?? new List<Servicios.LogicaNegocio.Venta.DTO.VentaDTO>();
            var ventasAnterior = _graficosDTO.VentasMesAnterior ?? new List<Servicios.LogicaNegocio.Venta.DTO.VentaDTO>();

            decimal totalMes = ventas.Sum(v => v.Total);
            int cantVentas = ventas.Count;
            decimal ticketPromedio = cantVentas > 0 ? totalMes / cantVentas : 0;
            decimal totalAnterior = ventasAnterior.Sum(v => v.Total);

            _lblKpiTotalMes.Text = $"Total del mes\r\n{totalMes:C2}";
            _lblKpiCantVentas.Text = $"Ventas del mes\r\n{cantVentas:N0}";
            _lblKpiTicketPromedio.Text = $"Ticket promedio\r\n{ticketPromedio:C2}";

            if (totalAnterior > 0)
            {
                decimal variacion = (totalMes - totalAnterior) / totalAnterior * 100;
                _lblKpiVariacion.Text = $"vs mes anterior\r\n{variacion:+0.0;-0.0;0.0}%";
                _lblKpiVariacion.ForeColor = variacion >= 0
                    ? System.Drawing.Color.FromArgb(46, 125, 50)
                    : System.Drawing.Color.FromArgb(198, 40, 40);
            }
            else
            {
                _lblKpiVariacion.Text = "vs mes anterior\r\n—";
                _lblKpiVariacion.ForeColor = System.Drawing.Color.FromArgb(31, 26, 43);
            }
        }

        private void ResetGraficos()
        {
            _grafico1Construido = false;
            _grafico2Construido = false;
            _grafico3Construido = false;
            _grafico4Construido = false;
            _grafico5Construido = false;
            _grafico6Construido = false;
        }

        private static readonly string[] NombresMeses =
        {
            "Enero", "Febrero", "Marzo", "Abril",
            "Mayo", "Junio", "Julio", "Agosto",
            "Septiembre", "Octubre", "Noviembre", "Diciembre"
        };

        private static IEnumerable<DateTime> DiasDelMes(int año, int mes)
        {
            int totalDias = DateTime.DaysInMonth(año, mes);
            for (int dia = 1; dia <= totalDias; dia++)
                yield return new DateTime(año, mes, dia);
        }



        /// <summary>
        /// Actualiza dinámicamente el combo de meses en base al año seleccionado en el combo padre.
        /// </summary>
        private void ActualizarComboMeses(int anio)
        {
            // Consulta los meses que contienen transacciones para el año provisto
            var mesesNumeros = _cajaSerivicio.ObtenerMesesConCajas(anio);

            // FALLBACK: Si no hay transacciones en ese año, inyectamos el mes actual para evitar listas vacías de control
            if (mesesNumeros.Count == 0)
            {
                mesesNumeros.Add(DateTime.Now.Month);
            }

            // Transformación con LINQ a objetos de negocio complejos utilizando la Cultura del Sistema Operativo
            var listaMeses = mesesNumeros.Select(m => new MesFiltro
            {
                Numero = m, // Lo que procesa el código (.SelectedValue)
                Nombre = char.ToUpper(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m)[0]) +
                         CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m).Substring(1) // Lo que ve el usuario ("Enero")
            }).ToList();

            cbMesGrafico.DataSource = null; // Forzamos la limpieza del motor de enlaces nativos de WinForms
            cbMesGrafico.ValueMember = "Numero";
            cbMesGrafico.DisplayMember = "Nombre";
            cbMesGrafico.DataSource = listaMeses;

            // Posiciona automáticamente el foco visual en el último mes registrado disponible de ese lote
            cbMesGrafico.SelectedValue = mesesNumeros.Max();
        }

        /// <summary>
        /// Helper de resolución de strings para formatear títulos dinámicos con la primera letra en mayúscula.
        /// </summary>
        private string ObtenerNombreMesLocal(int numeroMes)
        {
            string nombre = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(numeroMes);
            return char.ToUpper(nombre[0]) + nombre.Substring(1);
        }

        /// <summary>
        /// Evento de carga principal del formulario. Inicializa y dibuja los 6 gráficos analíticos en pantalla.
        /// </summary>
        private async void FAdministracion_Load(object sender, EventArgs e)
        {
            _graficosInicializados = false;

            InicializarFiltrosCronologicos();

            if (cbAñoGraficos.SelectedItem is int año &&
                cbMesGrafico.SelectedValue is int mes)
            {
                await CargarDashboardAsync(año, mes);

                _graficosInicializados = true;
                CargarGraficoActivo();
            }

            // Ajuste de ejes del gráfico 6 y refresco inicial obligatorio
            //formsPlot6.Plot.Axes.AutoScale();
            //formsPlot6.Refresh();
            

            // Vinculación de eventos de mouse para procesar la proximidad matemática y mostrar Tooltips interactivos
            formsPlot1.MouseMove += FormsPlot1_MouseMove;
            formsPlot2.MouseMove += FormsPlot2_MouseMove;
            formsPlot3.MouseMove += FormsPlot3_MouseMove;
            formsPlot4.MouseMove += FormsPlot4_MouseMove;
            formsPlot5.MouseMove += FormsPlot5_MouseMove;
            formsPlot6.MouseMove += FormsPlot6_MouseMove;
        }

        // =================================================================================
        // CONTROLADORES DE INTERFACCIÓN Y FILTRADO CRONOLÓGICO
        // =================================================================================
        private async void cbAñoGraficos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_graficosInicializados)
                return;

            if (cbAñoGraficos.SelectedItem is int añoFiltrado)
            {
                cbMesGrafico.SelectedIndexChanged -= cbMesGrafico_SelectedIndexChanged;

                ActualizarComboMeses(añoFiltrado);

                cbMesGrafico.SelectedIndexChanged += cbMesGrafico_SelectedIndexChanged;

                if (cbMesGrafico.SelectedValue is int mesFiltrado)
                {
                    ResetGraficos();
                    await filtrarGraficos(añoFiltrado, mesFiltrado);
                }
            }
        }
        private async void cbMesGrafico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_graficosInicializados)
                return;

            if (cbAñoGraficos.SelectedItem is int año &&
                cbMesGrafico.SelectedValue is int mes)
            {
                ResetGraficos();
                await filtrarGraficos(año, mes);
            }
        }

        /// <summary>
        /// Refresca en bloque los gráficos dependientes de filtros temporales.
        /// </summary>
        private async Task filtrarGraficos(int año, int mes)
        {
            await CargarDashboardAsync(año, mes);

            CargarGraficoActivo();
        }

        // =================================================================================
        // ARQUITECTURA INTERNA DE LOS GRÁFICOS (SCOTTPLOT)
        // Todos los métodos respetan el patrón: 1. Leer BD -> 2. Formatear Vectores -> 3. Dibujar -> 4. Guardar variables de mouse
        // =================================================================================

        /// <summary>
        /// Gráfico 1: Barras agrupadas - Ingresos vs Egresos por caja en el mes/año seleccionado.
        /// </summary>
        private void grafico1()
        {
            var cajasEnUnMesXyAñoX = _graficosDTO.CajasMes;
            int cantidadCajas = cajasEnUnMesXyAñoX.Count;

            double[] ingresosPorCaja = cajasEnUnMesXyAñoX
                .Select(c => (double)c.TotalIngresos)
                .ToArray();

            double[] egresosPorCaja = cajasEnUnMesXyAñoX
                .Select(c => (double)c.TotalEgresos)
                .ToArray();

            string[] fechasDeCadaCaja = cajasEnUnMesXyAñoX
                .Select(c => $"A: {c.FechaInicio:dd/MM}\nC: {c.FechaFin?.ToString("dd/MM") ?? "Abierta"}")
                .ToArray();

            const double separacionGrupo = 3.0;
            const double offsetBarra = 0.4;

            var barras = new List<Bar>();
            var centrosGrupo = new double[cantidadCajas];

            for (int i = 0; i < cantidadCajas; i++)
            {
                double centro = (i + 1) * separacionGrupo;
                centrosGrupo[i] = centro;

                barras.Add(new Bar
                {
                    Position = centro - offsetBarra,
                    Value = ingresosPorCaja[i],
                    FillColor = ColorIngresos
                });

                barras.Add(new Bar
                {
                    Position = centro + offsetBarra,
                    Value = egresosPorCaja[i],
                    FillColor = ColorEgresos
                });
            }

            _xs1 = centrosGrupo;
            _ys1 = ingresosPorCaja;
            _egresos1 = egresosPorCaja;
            _lastIndex1 = -1;

            formsPlot1.Plot.Clear();

            string title = $"Ingresos vs Egresos — {ObtenerNombreMesLocal(_mesCargado ?? DateTime.Now.Month)}";

            formsPlot1.Plot.Title(title);
            formsPlot1.Plot.XLabel("Cajas del mes");
            formsPlot1.Plot.YLabel("Monto ($)");

            if (barras.Count > 0)
            {
                formsPlot1.Plot.Add.Bars(barras.ToArray());

                Tick[] ticks = centrosGrupo
                    .Select((centro, i) => new Tick(centro, fechasDeCadaCaja[i]))
                    .ToArray();

                formsPlot1.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);
                formsPlot1.Plot.Axes.Bottom.MajorTickStyle.Length = 0;
            }

            formsPlot1.Plot.Legend.IsVisible = true;
            formsPlot1.Plot.Legend.Alignment = Alignment.UpperRight;
            formsPlot1.Plot.Legend.ManualItems.Clear();
            formsPlot1.Plot.Legend.ManualItems.Add(new LegendItem { LabelText = "Ingresos", FillColor = ColorIngresos });
            formsPlot1.Plot.Legend.ManualItems.Add(new LegendItem { LabelText = "Egresos", FillColor = ColorEgresos });

            formsPlot1.Plot.Axes.AutoScale();
            formsPlot1.Plot.Axes.Margins(bottom: 0);

            // Formato moneda en eje Y usando NumericAutomatic con LabelFormatter
            var tickGen1 = new ScottPlot.TickGenerators.NumericAutomatic();
            tickGen1.LabelFormatter = (value) => value.ToString("C0");
            formsPlot1.Plot.Axes.Left.TickGenerator = tickGen1;
        }

        /// <summary>
        /// Gráfico 2: Scatter (Puntos y Líneas) - Muestra ingresos agrupados por día de los últimos 31 días.
        /// </summary>
        private void grafico2()
        {
            // Usar caché para evitar re-consultar los mismos datos
            var cajasUltimos31Dias = _cajas31DiasCache ?? _graficosDTO.Cajas31Dias;
            if (_cajas31DiasCache == null)
                _cajas31DiasCache = _graficosDTO.Cajas31Dias;

            var cajasPorDia = cajasUltimos31Dias
                .GroupBy(c => c.FechaInicio.Date)
                .OrderBy(g => g.Key)
                .ToList();

            double[] ingresosPorDia = cajasPorDia
                .Select(g => (double)g.Sum(c => c.TotalIngresos))
                .ToArray();

            string[] dias = cajasPorDia
                .Select(g => g.Key.ToString("dd/MM"))
                .ToArray();

            double[] numerosDias = Enumerable
                .Range(1, cajasPorDia.Count)
                .Select(i => (double)i)
                .ToArray();

            _xs2 = numerosDias;
            _ys2 = ingresosPorDia;
            _lastIndex2 = -1;

            formsPlot2.Plot.Clear();

            formsPlot2.Plot.Title("Cajas últimos 31 días");
            formsPlot2.Plot.XLabel("Fecha");
            formsPlot2.Plot.YLabel("Total Ingresos");

            var scatter = formsPlot2.Plot.Add.Scatter(numerosDias, ingresosPorDia);

            scatter.Color = ColorTendencia;
            scatter.LineWidth = 2;

            formsPlot2.Plot.Axes.Bottom.SetTicks(numerosDias, dias);
            formsPlot2.Plot.Axes.AutoScale();

            // Formato moneda en eje Y usando NumericAutomatic con LabelFormatter
            var tickGen2 = new ScottPlot.TickGenerators.NumericAutomatic();
            tickGen2.LabelFormatter = (value) => value.ToString("C0");
            formsPlot2.Plot.Axes.Left.TickGenerator = tickGen2;
        }

        /// <summary>
        /// Gráfico 3: Bars (Barras) - Muestra la sumatoria económica diaria total de ventas en el mes.
        /// </summary>
        private void grafico3()
        {
            int año = _anioCargado ?? DateTime.Now.Year;
            int mes = _mesCargado ?? DateTime.Now.Month;
            var ventas = _graficosDTO.VentasMes;
            var ventasAnterior = _graficosDTO.VentasMesAnterior;

            var ventasPorDia = ventas
                .GroupBy(v => v.FechaVenta.Date)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.Total));

            var ventasPorDiaAnterior = ventasAnterior
                .GroupBy(v => v.FechaVenta.Date)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.Total));

            var diasDelMes = DiasDelMes(año, mes).ToList();

            string[] dias = diasDelMes
                .Select(x => x.ToString("dd/MM"))
                .ToArray();

            double[] valores = diasDelMes
                .Select(fecha => ventasPorDia.TryGetValue(fecha, out var total) ? (double)total : 0)
                .ToArray();

            double[] valoresAnterior = diasDelMes
                .Select(fecha => ventasPorDiaAnterior.TryGetValue(fecha, out var total) ? (double)total : 0)
                .ToArray();

            double[] posiciones = Enumerable
                .Range(0, dias.Length)
                .Select(i => (double)i)
                .ToArray();

            _xs3 = posiciones;
            _ys3 = valores;
            _xs3prev = posiciones;
            _ys3prev = valoresAnterior;
            _lastIndex3 = -1;

            formsPlot3.Plot.Clear();

            formsPlot3.Plot.Title($"Ventas diarias en {ObtenerNombreMesLocal(mes)}");

            formsPlot3.Plot.XLabel("Días");
            formsPlot3.Plot.YLabel("Total Ventas");

            // Barras del mes actual
            var bars = new List<ScottPlot.Bar>();
            for (int i = 0; i < valores.Length; i++)
            {
                bars.Add(new ScottPlot.Bar
                {
                    Position = posiciones[i] - 0.15,
                    Value = valores[i],
                    FillColor = ColorMonto
                });
            }
            formsPlot3.Plot.Add.Bars(bars.ToArray());

            // Agregar barras del mes anterior para comparativo (más transparentes, offset -0.15)
            if (valoresAnterior.Any(v => v > 0))
            {
                var barsPrev = new List<ScottPlot.Bar>();
                for (int i = 0; i < valoresAnterior.Length; i++)
                {
                    if (valoresAnterior[i] > 0)
                    {
                        barsPrev.Add(new ScottPlot.Bar
                        {
                            Position = posiciones[i] + 0.15,
                            Value = valoresAnterior[i],
                            FillColor = ColorComparativo.WithAlpha(120)
                        });
                    }
                }
                formsPlot3.Plot.Add.Bars(barsPrev.ToArray());
            }

            formsPlot3.Plot.Axes.Bottom.SetTicks(posiciones, dias);
            if (dias.Length > 15)
                formsPlot3.Plot.Axes.Bottom.TickLabelStyle.Rotation = 45;

            formsPlot3.Plot.Axes.AutoScale();
            formsPlot3.Plot.Axes.Margins(bottom: 0);

            // Formato moneda en eje Y usando NumericAutomatic con LabelFormatter
            var tickGen3 = new ScottPlot.TickGenerators.NumericAutomatic();
            tickGen3.LabelFormatter = (value) => value.ToString("C0");
            formsPlot3.Plot.Axes.Left.TickGenerator = tickGen3;
        }

        /// <summary>
        /// Gráfico 4: Bars (Barras) - Cantidad transaccional de ventas brutas realizadas por día.
        /// </summary>
        private void grafico4()
        {
            int año = _anioCargado ?? DateTime.Now.Year;
            int mes = _mesCargado ?? DateTime.Now.Month;
            var ventas = _graficosDTO.VentasMes;
            var ventasAnterior = _graficosDTO.VentasMesAnterior;

            var ventasPorDia = ventas
                .GroupBy(v => v.FechaVenta.Date)
                .ToDictionary(g => g.Key, g => g.Count());

            var ventasPorDiaAnterior = ventasAnterior
                .GroupBy(v => v.FechaVenta.Date)
                .ToDictionary(g => g.Key, g => g.Count());

            var diasDelMes = DiasDelMes(año, mes).ToList();

            string[] dias = diasDelMes
                .Select(x => x.ToString("dd/MM"))
                .ToArray();

            double[] cantidades = diasDelMes
                .Select(fecha => ventasPorDia.TryGetValue(fecha, out var cantidad) ? (double)cantidad : 0)
                .ToArray();

            double[] cantidadesAnterior = diasDelMes
                .Select(fecha => ventasPorDiaAnterior.TryGetValue(fecha, out var cantidad) ? (double)cantidad : 0)
                .ToArray();

            double[] posiciones = Enumerable
                .Range(0, dias.Length)
                .Select(i => (double)i)
                .ToArray();

            _xs4 = posiciones;
            _ys4 = cantidades;
            _xs4prev = posiciones;
            _ys4prev = cantidadesAnterior;
            _lastIndex4 = -1;

            formsPlot4.Plot.Clear();

            formsPlot4.Plot.Title($"Ventas diarias en {ObtenerNombreMesLocal(mes)}");

            formsPlot4.Plot.XLabel("Días");
            formsPlot4.Plot.YLabel("Cantidad");

            // Barras del mes actual
            var bars4 = new List<ScottPlot.Bar>();
            for (int i = 0; i < cantidades.Length; i++)
            {
                bars4.Add(new ScottPlot.Bar
                {
                    Position = posiciones[i] - 0.15,
                    Value = cantidades[i],
                    FillColor = ColorCantidad
                });
            }
            formsPlot4.Plot.Add.Bars(bars4.ToArray());

            // Agregar barras del mes anterior para comparativo (offset +0.15)
            if (cantidadesAnterior.Any(v => v > 0))
            {
                var barsPrev = new List<ScottPlot.Bar>();
                for (int i = 0; i < cantidadesAnterior.Length; i++)
                {
                    if (cantidadesAnterior[i] > 0)
                    {
                        barsPrev.Add(new ScottPlot.Bar
                        {
                            Position = posiciones[i] + 0.15,
                            Value = cantidadesAnterior[i],
                            FillColor = ColorComparativo.WithAlpha(120)
                        });
                    }
                }
                formsPlot4.Plot.Add.Bars(barsPrev.ToArray());
            }

            formsPlot4.Plot.Axes.Bottom.SetTicks(posiciones, dias);
            if (dias.Length > 15)
                formsPlot4.Plot.Axes.Bottom.TickLabelStyle.Rotation = 45;

            formsPlot4.Plot.Axes.AutoScale();
            formsPlot4.Plot.Axes.Margins(bottom: 0);
        }

        /// <summary>
        /// Gráfico 5: Bars (Barras) - Acumulado mensual financiero anualizado.
        /// </summary>
        private void grafico5()
        {
            var cajas = _graficosDTO.CajasAnio;

            var ingresosPorMes = cajas
                .GroupBy(c => c.FechaInicio.Month)
                .ToDictionary(g => g.Key, g => g.Sum(c => c.TotalIngresos));

            double[] xs = Enumerable.Range(0, 12).Select(i => (double)i).ToArray();
            double[] ys = Enumerable.Range(1, 12)
                .Select(m => ingresosPorMes.TryGetValue(m, out var total) ? (double)total : 0)
                .ToArray();

            _xs5 = xs;
            _ys5 = ys;
            _lastIndex5 = -1;

            formsPlot5.Plot.Clear();

            formsPlot5.Plot.Title($"Ingresos en {_anioCargado}");

            formsPlot5.Plot.XLabel("Meses");
            formsPlot5.Plot.YLabel("Ingresos");

            var bars = formsPlot5.Plot.Add.Bars(xs, ys);

            bars.Color = ColorMonto;

            formsPlot5.Plot.Axes.Bottom.SetTicks(xs, NombresMeses);
            formsPlot5.Plot.Axes.Bottom.TickLabelStyle.Rotation = 45;
            formsPlot5.Plot.Axes.AutoScale();
            formsPlot5.Plot.Axes.Margins(bottom: 0);

            // Formato moneda en eje Y usando NumericAutomatic con LabelFormatter
            var tickGen5 = new ScottPlot.TickGenerators.NumericAutomatic();
            tickGen5.LabelFormatter = (value) => value.ToString("C0");
            formsPlot5.Plot.Axes.Left.TickGenerator = tickGen5;
        }

        /// <summary>
        /// Gráfico 6: Bars (Barras) - Volumen total de operaciones comerciales anualizado por mes.
        /// </summary>
        private void grafico6()
        {
            var ventas = _graficosDTO.VentasAnio;

            var ventasPorMes = ventas
                .GroupBy(v => v.FechaVenta.Month)
                .ToDictionary(g => g.Key, g => g.Count());

            double[] xs = Enumerable.Range(0, 12).Select(i => (double)i).ToArray();
            double[] ys = Enumerable.Range(1, 12)
                .Select(m => ventasPorMes.TryGetValue(m, out var cantidad) ? (double)cantidad : 0)
                .ToArray();

            _xs6 = xs;
            _ys6 = ys;
            _lastIndex6 = -1;

            formsPlot6.Plot.Clear();

            formsPlot6.Plot.Title($"Ventas en {_anioCargado}");

            formsPlot6.Plot.XLabel("Meses");
            formsPlot6.Plot.YLabel("Cantidad");

            var bars = formsPlot6.Plot.Add.Bars(xs, ys);

            bars.Color = ColorCantidad;

            formsPlot6.Plot.Axes.Bottom.SetTicks(xs, NombresMeses);
            formsPlot6.Plot.Axes.Bottom.TickLabelStyle.Rotation = 45;
            formsPlot6.Plot.Axes.AutoScale();
            formsPlot6.Plot.Axes.Margins(bottom: 0);
        }

        private async void btnFechaActualGraficos_Click(object sender, EventArgs e)
        {
            InicializarFiltrosCronologicos();

            if (cbAñoGraficos.SelectedItem is int año &&
                cbMesGrafico.SelectedValue is int mes)
            {
                _anioCargado = null;
                _mesCargado = null;
                ResetGraficos();
                await filtrarGraficos(año, mes);
            }
        }

        // =================================================================================
        // ENRUTAMIENTO DIRECTO DE MÓDULOS DEL MENÚ ADMINISTRATIVO
        // Métodos encapsulados para invocar los distintos formularios satélites del sistema de gestión.
        // =================================================================================
        private void btnVolver_Click(object sender, EventArgs e) => this.Close();
        private void sTOCKToolStripMenuItem_Click(object sender, EventArgs e) => new FProductoConsulta().Show();
        private void mARCASToolStripMenuItem_Click(object sender, EventArgs e) => new FMarcaConsulta(false).Show();
        private void cATEGORIASToolStripMenuItem_Click(object sender, EventArgs e) => new FCategoriaConsulta(false).Show();
        private void rUBROSToolStripMenuItem_Click(object sender, EventArgs e) => new FRubroConsulta(false).Show();
        private void lISTADOEMPLEADOSToolStripMenuItem_Click(object sender, EventArgs e) => new FEmpleadoConsulta(_logeadoId).Show();
        private void lISTADOCLIENTESToolStripMenuItem_Click(object sender, EventArgs e) => new FClienteConsulta().Show();
        private void lISTADOOFERTASToolStripMenuItem_Click(object sender, EventArgs e) => new FOfertaConsulta().Show();
        private void lOTESToolStripMenuItem_Click(object sender, EventArgs e) => new FLoteConsulta().Show();
        private void btnMovimientos_Click(object sender, EventArgs e) => new FMovimientoConsulta().Show();
        private void tIPOPAGOToolStripMenuItem_Click(object sender, EventArgs e) => new FTipoPagoConsulta().Show();
        private void rOLESToolStripMenuItem_Click(object sender, EventArgs e) => new FRolConsulta().Show();
        private void nUEVAOFERTAToolStripMenuItem_Click(object sender, EventArgs e) => new FOfertaABM().ShowDialog();
        private void btnGasto_Click(object sender, EventArgs e) => new Gasto.FGastoConsulta(_logeadoId).Show();
        private void historialToolStripMenuItem_Click(object sender, EventArgs e) => new FVentaConsulta().Show();
        private void historialVentasLibresToolStripMenuItem_Click(object sender, EventArgs e) => new FVentaLibreConsulta().Show();
        private void nuevaVentaLibreToolStripMenuItem_Click(object sender, EventArgs e) => new FVentaLibre(_logeadoId).Show();

        /// <summary>
        /// Acceso rápido al subsistema de impresión y visualización de archivos PDF almacenados localmente.
        /// </summary>
        private void btnComprobantes_Click(object sender, EventArgs e)
        {
            var escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var carpeta = Path.Combine(escritorio, "ComprobantesPdf");

            if (!Directory.Exists(carpeta))
            {
                MessageBox.Show("La carpeta de comprobantes todavía no existe.", "Comprobantes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dialog = new OpenFileDialog { InitialDirectory = carpeta, Filter = "Archivos PDF (*.pdf)|*.pdf", Title = "Seleccionar comprobante" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using var visor = new FVisorPDF(dialog.FileName);
                visor.ShowDialog();
            }
        }

        // =================================================================================
        // MOTOR MATEMÁTICO DE SEGUIMIENTO DE MOUSE Y RENDERIZADO PERSONALIZADO DE TOOLTIPS
        // =================================================================================

        /// <summary>
        /// Realiza la traducción de coordenadas físicas del monitor (píxeles de pantalla) a coordenadas cartesianas 
        /// matemáticas de ScottPlot para identificar el punto de datos más cercano bajo el puntero del mouse.
        /// </summary>
        /// <param name="formsPlot">El control UI sobre el que se desplaza el cursor.</param>
        /// <param name="xs">Colección de valores cartesianos del eje X actual del gráfico.</param>
        /// <param name="ys">Colección de valores cartesianos del eje Y actual del gráfico.</param>
        /// <param name="e">Argumentos del evento de movimiento de mouse nativo.</param>
        /// <param name="prefijo">Texto descriptivo inicial ("Ingreso", "Ganancia").</param>
        /// <param name="formato">Cadena de formato estándar .NET para monedas o enteros ("C2", "N0").</param>
        /// <param name="lastIndex">Referencia por dirección (ref) del puntero interno del gráfico para trackear cambios de estado.</param>
        private void EvaluarPosicionMouse(FormsPlot formsPlot, double[] xs, double[] ys, MouseEventArgs e, string prefijo, string formato, ref int lastIndex)
        {
            // Si el lote de datos estructurales está vacío, forzamos la ocultación inmediata del ToolTip y cancelamos la evaluación
            if (xs == null || ys == null || xs.Length == 0) { OcultarTooltip(formsPlot, ref lastIndex); return; }

            // 1. Instanciamos la posición del puntero en píxeles
            Pixel pixelMouse = new Pixel(e.X, e.Y);
            // 2. Traducimos esos píxeles a coordenadas lógicas cartesianas matemáticas
            Coordinates coordMouse = formsPlot.Plot.GetCoordinates(pixelMouse);

            int indexMasCercano = -1;
            double minimaDistanciaX = double.MaxValue;

            // 3. Algoritmo de Proximidad Lineal por diferencia absoluta mínima en el eje X
            for (int i = 0; i < xs.Length; i++)
            {
                double distancia = Math.Abs(xs[i] - coordMouse.X);
                if (distancia < minimaDistanciaX) { minimaDistanciaX = distancia; indexMasCercano = i; }
            }

            // 4. Umbral de Sensibilidad (0.4 unidades matemáticas): Evita que se dispare el tooltip si el mouse está lejos del punto real
            if (indexMasCercano != -1 && minimaDistanciaX < TooltipThreshold)
            {
                // Solo operamos si el usuario movió el cursor a un punto estadístico DIFERENTE al evaluado en el ciclo anterior
                if (lastIndex != indexMasCercano)
                {
                    lastIndex = indexMasCercano; // Guardamos el nuevo estado para evitar parpadeos
                    double valorY = ys[indexMasCercano];
                    _currentToolTipText = $"{prefijo}: {valorY.ToString(formato)}";

                    // Despliega el ToolTip flotante con un pequeño desfasaje estratégico de +15 píxeles para no tapar el nodo visual
                    _winFormsToolTip.Show(_currentToolTipText, formsPlot, e.X + 15, e.Y + 15, 3000);
                }
            }
            else { OcultarTooltip(formsPlot, ref lastIndex); }
        }

        /// <summary>
        /// Intercepta el evento de dimensionamiento del cuadro de diálogo flotante.
        /// Mide de antemano el tamaño de los strings para forzar márgenes de padding estéticos personalizados.
        /// </summary>
        private void WinFormsToolTip_Popup(object sender, PopupEventArgs e)
        {
            Size tamanoTexto = TextRenderer.MeasureText(_currentToolTipText, _toolTipFont);
            // Añade un padding controlado (+12 de ancho, +8 de alto) sobre el tamaño nativo delimitado por el texto
            e.ToolTipSize = new Size(tamanoTexto.Width + 12, tamanoTexto.Height + 8);
        }

        /// <summary>
        /// Sobrescribe por completo el motor de dibujo por defecto de Windows (OwnerDraw).
        /// Permite aplicar tipografías anti-aliasing de alta definición y paletas de color minimalistas profesionales.
        /// </summary>
        private void WinFormsToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            // Relleno de fondo minimalista blanco
            e.Graphics.FillRectangle(Brushes.White, e.Bounds);

            // Dibujado del marco perimetral con un tono gris neutro estilizado (RGB: 180, 180, 180)
            using (Pen lapizBorde = new Pen(System.Drawing.Color.FromArgb(180, 180, 180), 1))
            {
                e.Graphics.DrawRectangle(lapizBorde, 0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1);
            }

            // Forzado de alineación bidireccional completamente centrada tanto vertical como horizontalmente
            TextFormatFlags alineacion = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(e.Graphics, e.ToolTipText, _toolTipFont, e.Bounds, System.Drawing.Color.Black, alineacion);
        }

        /// <summary>
        /// Oculta de manera segura el cartel flotante activo y reinicia los punteros de control del mouse.
        /// </summary>
        private void OcultarTooltip(FormsPlot formsPlot, ref int lastIndex)
        {
            if (lastIndex != -1) { lastIndex = -1; _winFormsToolTip.Hide(formsPlot); }
        }

        // =================================================================================
        // REDIRECCIONAMIENTO DIRECTO DE EVENTOS DE MOUSE INDIVIDUALES POR COMPONENTE VISUAL
        // =================================================================================
        private void FormsPlot1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_xs1 == null || _ys1 == null || _egresos1 == null || _xs1.Length == 0)
            {
                OcultarTooltip(formsPlot1, ref _lastIndex1);
                return;
            }

            Pixel pixelMouse = new Pixel(e.X, e.Y);
            Coordinates coordMouse = formsPlot1.Plot.GetCoordinates(pixelMouse);

            int indexMasCercano = -1;
            double minimaDistanciaX = double.MaxValue;

            for (int i = 0; i < _xs1.Length; i++)
            {
                double distancia = Math.Abs(_xs1[i] - coordMouse.X);
                if (distancia < minimaDistanciaX)
                {
                    minimaDistanciaX = distancia;
                    indexMasCercano = i;
                }
            }

            if (indexMasCercano != -1 && minimaDistanciaX < TooltipThresholdBars)
            {
                if (_lastIndex1 != indexMasCercano)
                {
                    _lastIndex1 = indexMasCercano;
                    _currentToolTipText =
                        $"Ingreso: {_ys1[indexMasCercano]:C2}\nEgreso: {_egresos1[indexMasCercano]:C2}";
                    _winFormsToolTip.Show(_currentToolTipText, formsPlot1, e.X + 15, e.Y + 15, 3000);
                }
            }
            else
            {
                OcultarTooltip(formsPlot1, ref _lastIndex1);
            }
        }
        private void FormsPlot2_MouseMove(object sender, MouseEventArgs e) => EvaluarPosicionMouse(formsPlot2, _xs2, _ys2, e, "Total Día", "C2", ref _lastIndex2);
        private void FormsPlot3_MouseMove(object sender, MouseEventArgs e) => EvaluarPosicionMouse(formsPlot3, _xs3, _ys3, e, "Venta", "C2", ref _lastIndex3);
        private void FormsPlot4_MouseMove(object sender, MouseEventArgs e) => EvaluarPosicionMouse(formsPlot4, _xs4, _ys4, e, "Cant. Ventas", "N0", ref _lastIndex4);
        private void FormsPlot5_MouseMove(object sender, MouseEventArgs e) => EvaluarPosicionMouse(formsPlot5, _xs5, _ys5, e, "Total Mes", "C2", ref _lastIndex5);
        private void FormsPlot6_MouseMove(object sender, MouseEventArgs e) => EvaluarPosicionMouse(formsPlot6, _xs6, _ys6, e, "Cant. Ventas", "N0", ref _lastIndex6);

        private void DibujarBotones()
        {
            //BOTON VOLVER A INICIO//
            btnVolver.Image = Constantes.Imagenes.ImgVolver; // Asumiendo que tienes un recurso de imagen llamado "volver" en tu proyecto
            // Alineamos la imagen arriba al centro
            btnVolver.ImageAlign = ContentAlignment.TopCenter;
            btnVolver.TextImageRelation = TextImageRelation.ImageAboveText;
            // Le damos un padding superior para que el ícono no pegue contra el techo del botón
            btnVolver.Padding = new Padding(0, 10, 0, 0);

            //BOTON GASTO//
            btnGasto.Image = Constantes.Imagenes.ImgGasto; // Asumiendo que tienes un recurso de imagen llamado "gasto" en tu proyecto
            // Alineamos la imagen arriba al centro
            btnGasto.ImageAlign = ContentAlignment.TopCenter;
            btnGasto.TextImageRelation = TextImageRelation.ImageAboveText;
            // Le damos un padding superior para que el ícono no pegue contra el techo del botón
            btnGasto.Padding = new Padding(0, 10, 0, 0);

            //BOTON MOVIMIENTOS//
            btnMovimientos.Image = Constantes.Imagenes.ImgMovimiento; // Asumiendo que tienes un recurso de imagen llamado "movimientos" en tu proyecto
            // Alineamos la imagen arriba al centro
            btnMovimientos.ImageAlign = ContentAlignment.TopCenter;
            btnMovimientos.TextImageRelation = TextImageRelation.ImageAboveText;
            // Le damos un padding superior para que el ícono no pegue contra el techo del botón
            btnMovimientos.Padding = new Padding(0, 10, 0, 0);

            //BOTON COMPROBANTES//
            btnComprobantes.Image = Constantes.Imagenes.ImgComprobante; // Asumiendo que tienes un recurso de imagen llamado "comprobantes" en tu proyecto
            // Alineamos la imagen arriba al centro
            btnComprobantes.ImageAlign = ContentAlignment.TopCenter;
            btnComprobantes.TextImageRelation = TextImageRelation.ImageAboveText;
            // Le damos un padding superior para que el ícono no pegue contra el techo del botón
            btnComprobantes.Padding = new Padding(0, 10, 0, 0);
        }
        private void TabControlGraficos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGraficoActivo();
        }
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGraficoActivo();
        }
        private void CargarGraficoActivo()
        {
            switch (tabControlGraficoArriba.SelectedIndex)
            {
                case 0:
                    if (!_grafico1Construido)
                    {
                        grafico1();
                        _grafico1Construido = true;
                    formsPlot1.Refresh();
                    }

                    break;

                case 1:
                    if (!_grafico2Construido)
                    {
                        grafico2();
                        _grafico2Construido = true;
                    formsPlot2.Refresh();
                    }

                    break;
            }

            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    if (!_grafico3Construido)
                    {
                        grafico3();
                        _grafico3Construido = true;
                    formsPlot3.Refresh();
                    }


                    if (!_grafico4Construido)
                    {
                        grafico4();
                        _grafico4Construido = true;
                    formsPlot4.Refresh();
                    }

                    break;

                case 1:
                    if (!_grafico5Construido)
                    {
                        grafico5();
                        _grafico5Construido = true;
                    formsPlot5.Refresh();
                    }


                    if (!_grafico6Construido)
                    {
                        grafico6();
                        _grafico6Construido = true;
                    formsPlot6.Refresh();
                    }

                    break;
            }
        }
    }
}