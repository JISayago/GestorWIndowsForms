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
using Presentacion.FBase.Helpers;
using ScottPlot;
using ScottPlot.WinForms;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Caja;
using Servicios.LogicaNegocio.Caja.DTO;
using Servicios.LogicaNegocio.Sistema.Administracion;
using Servicios.LogicaNegocio.Sistema.Administracion.DTO;
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
        List<CajaDTO> todasLasCajas;
        private readonly AdministracionGraficosServicios _graficoServicio;
        private GraficosAdministracionDTO _graficosDTO;
        private int? _anioCargado;
        private int? _mesCargado;
        private bool _graficosInicializados;

        private bool _grafico1Construido;
        private bool _grafico2Construido;
        private bool _grafico3Construido;
        private bool _grafico5Construido;
        private bool _grafico7Construido;
        private bool _grafico8Construido;
        private bool _grafico9Construido;
        private bool _grafico10Construido;
        private bool _ventasDiariasModoMonto = true;
        private bool _ventasMensualesModoMonto = true;

        private TableLayoutPanel _tlpKpis;
        private System.Windows.Forms.Label _lblKpiTotalMes;
        private System.Windows.Forms.Label _lblKpiCantVentas;
        private System.Windows.Forms.Label _lblKpiTicketPromedio;
        private System.Windows.Forms.Label _lblKpiVariacion;
        private System.Windows.Forms.Label _lblKpiMargen;

        private TabPage _tabPagePagos;
        private FormsPlot formsPlot7;
        private TableLayoutPanel _pnlLeyendaPagos;
        private FlowLayoutPanel _pnlCardsPagos;
        private System.Windows.Forms.Label _lblTituloPagos;
        private System.Windows.Forms.Label _lblTotalPagos;
        private FormsPlot formsPlot8;
        private FormsPlot formsPlot9;
        private FormsPlot formsPlot10;
        private ScottPlot.Panels.ColorBar _colorBarHeatmap;
        private RadioButton _rbVentasMonto;
        private RadioButton _rbVentasCantidad;
        private RadioButton _rbMensualMonto;
        private RadioButton _rbMensualCantidad;

        private static readonly ScottPlot.Color ColorIngresos = ScottPlot.Color.FromHex("#2E7D32");
        private static readonly ScottPlot.Color ColorEgresos = ScottPlot.Color.FromHex("#C62828");
        private static readonly ScottPlot.Color ColorMonto = ScottPlot.Color.FromHex("#2E7D32");
        private static readonly ScottPlot.Color ColorCantidad = ScottPlot.Color.FromHex("#1565C0");
        private static readonly ScottPlot.Color ColorTendencia = ScottPlot.Color.FromHex("#291a3e");
        private static readonly ScottPlot.Color ColorComparativo = ScottPlot.Color.FromHex("#F57F17");
        private static readonly ScottPlot.Color ColorPromedio = ScottPlot.Color.FromHex("#6A1B9A");
        private static readonly ScottPlot.Color ColorMaximo = ScottPlot.Color.FromHex("#E65100");

        private static readonly ScottPlot.Color[] ColoresPie =
        {
            ScottPlot.Color.FromHex("#2E7D32"),
            ScottPlot.Color.FromHex("#1565C0"),
            ScottPlot.Color.FromHex("#F57F17"),
            ScottPlot.Color.FromHex("#6A1B9A"),
            ScottPlot.Color.FromHex("#C62828"),
            ScottPlot.Color.FromHex("#00838F"),
            ScottPlot.Color.FromHex("#5D4037"),
            ScottPlot.Color.FromHex("#455A64"),
        };

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
        private double[] _xs1, _ys1; // Gráfico 1: Ingresos y egresos por caja
        private double[] _egresos1;
        private double[] _xs2, _ys2; // Gráfico 2: Ingresos por caja
        private double[] _xs3, _ys3; // Gráfico diario unificado
        private double[] _xs3prev, _ys3prev;
        private double[] _xs5, _ys5;
        private double[] _ys5prev;

        // Constantes de umbral de proximidad para tooltips
        private const double TooltipThreshold = 0.4;
        private const double TooltipThresholdBars = 1.2;

        // Mismo gris del tema (#EAEAEA) para que el marco del plot no contraste con el panel.
        private static readonly System.Drawing.Color ColorFondoWinForms = TemaSistema.Fondo;
        private static readonly ScottPlot.Color ColorFondoPlot = ScottPlot.Color.FromHex("#EAEAEA");

        private int _lastIndex1 = -1;
        private int _lastIndex2 = -1;
        private int _lastIndex3 = -1;
        private int _lastIndex5 = -1;

        /// <summary>
        /// Constructor del Formulario Administrativo
        /// </summary>
        public FAdministracion(long logeadoId)
        {
            InitializeComponent();
            DibujarBotones();
            InicializarPanelKpis();
            InicializarTabFormasPago();
            InicializarTabsAnaliticaExtra();
            ConfigurarLayoutPorDia();
            ConfigurarLayoutPorMes();
            ConfigurarAnchoPestanas();
            UnificarFondosGraficos();

            _logeadoId = logeadoId;

            // Inicialización de la lógica de negocio
            _cajaSerivicio = new CajaServicio();
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
                ColumnCount = 5,
                RowCount = 1,
                BackColor = TemaSistema.FondoControl,
                Padding = new Padding(8, 4, 8, 4)
            };

            for (int i = 0; i < 5; i++)
                _tlpKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));

            _lblKpiTotalMes = CrearLabelKpi("Total del mes", colorTitulo, colorTexto);
            _lblKpiCantVentas = CrearLabelKpi("Ventas del mes", colorTitulo, colorTexto);
            _lblKpiTicketPromedio = CrearLabelKpi("Ticket promedio", colorTitulo, colorTexto);
            _lblKpiVariacion = CrearLabelKpi("vs mes anterior", colorTitulo, colorTexto);
            _lblKpiMargen = CrearLabelKpi("Margen del mes", colorTitulo, colorTexto);

            _tlpKpis.Controls.Add(_lblKpiTotalMes, 0, 0);
            _tlpKpis.Controls.Add(_lblKpiCantVentas, 1, 0);
            _tlpKpis.Controls.Add(_lblKpiTicketPromedio, 2, 0);
            _tlpKpis.Controls.Add(_lblKpiVariacion, 3, 0);
            _tlpKpis.Controls.Add(_lblKpiMargen, 4, 0);

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

            var ventas = _graficosDTO.VentasMes ?? new List<VentaResumenGraficoDTO>();
            var ventasAnteriorPorDia = _graficosDTO.VentasMesAnteriorPorDia ?? new List<VentaDiaAgregadoDTO>();

            decimal totalMes = ventas.Sum(v => v.Total);
            int cantVentas = ventas.Count;
            decimal ticketPromedio = cantVentas > 0 ? totalMes / cantVentas : 0;
            decimal totalAnterior = ventasAnteriorPorDia.Sum(v => v.Total);

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

            decimal ingresosCaja = (_graficosDTO.CajasMes ?? new List<CajaDTO>()).Sum(c => c.TotalIngresos);
            decimal egresosCaja = (_graficosDTO.CajasMes ?? new List<CajaDTO>()).Sum(c => c.TotalEgresos);
            decimal gastosMes = _graficosDTO.TotalGastosMes;
            decimal margen = ingresosCaja - egresosCaja - gastosMes;

            _lblKpiMargen.Text = $"Margen del mes\r\n{margen:C2}";
            _lblKpiMargen.ForeColor = margen >= 0
                ? System.Drawing.Color.FromArgb(46, 125, 50)
                : System.Drawing.Color.FromArgb(198, 40, 40);
        }

        private void ResetGraficos()
        {
            _grafico1Construido = false;
            _grafico2Construido = false;
            _grafico3Construido = false;
            _grafico5Construido = false;
            _grafico7Construido = false;
            _grafico8Construido = false;
            _grafico9Construido = false;
            _grafico10Construido = false;
        }

        private void InicializarTabFormasPago()
        {
            formsPlot7 = new FormsPlot
            {
                Dock = DockStyle.Fill,
                Name = "formsPlot7",
                BackColor = ColorFondoWinForms
            };

            _lblTituloPagos = new System.Windows.Forms.Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(31, 26, 43),
                Text = "Formas de pago"
            };

            _lblTotalPagos = new System.Windows.Forms.Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(46, 125, 50),
                Text = "Total: —"
            };

            _pnlCardsPagos = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = false,
                BackColor = ColorFondoWinForms,
                Padding = new Padding(0, 4, 0, 0)
            };

            _pnlLeyendaPagos = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = ColorFondoWinForms,
                Padding = new Padding(12, 16, 12, 8)
            };
            _pnlLeyendaPagos.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            _pnlLeyendaPagos.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            _pnlLeyendaPagos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _pnlLeyendaPagos.Controls.Add(_lblTituloPagos, 0, 0);
            _pnlLeyendaPagos.Controls.Add(_lblTotalPagos, 0, 1);
            _pnlLeyendaPagos.Controls.Add(_pnlCardsPagos, 0, 2);

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = ColorFondoWinForms,
                Padding = new Padding(0)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            layout.Controls.Add(formsPlot7, 0, 0);
            layout.Controls.Add(_pnlLeyendaPagos, 1, 0);

            _tabPagePagos = new TabPage("Formas de pago")
            {
                Padding = new Padding(3),
                BackColor = ColorFondoWinForms,
                UseVisualStyleBackColor = false
            };
            _tabPagePagos.Controls.Add(layout);
            tabControlGraficoArriba.Controls.Add(_tabPagePagos);
        }

        /// <summary>
        /// Unifica "Por Día" en un solo gráfico full-width con toggle Monto/Cantidad.
        /// </summary>
        private void ConfigurarLayoutPorDia()
        {
            tlpBajoNivel3.SuspendLayout();
            tlpBajoNivel3.Controls.Clear();
            tlpBajoNivel3.ColumnStyles.Clear();
            tlpBajoNivel3.RowStyles.Clear();
            tlpBajoNivel3.ColumnCount = 1;
            tlpBajoNivel3.RowCount = 2;
            tlpBajoNivel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBajoNivel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tlpBajoNivel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var toolbar = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = ColorFondoWinForms,
                Padding = new Padding(10, 6, 10, 4)
            };

            var lblModo = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Text = "Mostrar:",
                Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(31, 26, 43),
                Margin = new Padding(0, 4, 8, 0)
            };

            _rbVentasMonto = new RadioButton
            {
                AutoSize = true,
                Text = "Monto ($)",
                Checked = true,
                Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold),
                Margin = new Padding(0, 2, 16, 0)
            };
            _rbVentasCantidad = new RadioButton
            {
                AutoSize = true,
                Text = "Cantidad",
                Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold),
                Margin = new Padding(0, 2, 16, 0)
            };

            var lblAyuda = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Text = "Barras = mes actual · Línea = mes anterior",
                Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic),
                ForeColor = System.Drawing.Color.FromArgb(100, 100, 100),
                Margin = new Padding(12, 5, 0, 0)
            };

            _rbVentasMonto.CheckedChanged += RbVentasDiarias_CheckedChanged;
            _rbVentasCantidad.CheckedChanged += RbVentasDiarias_CheckedChanged;

            toolbar.Controls.Add(lblModo);
            toolbar.Controls.Add(_rbVentasMonto);
            toolbar.Controls.Add(_rbVentasCantidad);
            toolbar.Controls.Add(lblAyuda);

            formsPlot3.Dock = DockStyle.Fill;
            formsPlot3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            formsPlot4.Visible = false;

            tlpBajoNivel3.Controls.Add(toolbar, 0, 0);
            tlpBajoNivel3.Controls.Add(formsPlot3, 0, 1);
            tlpBajoNivel3.ResumeLayout();
        }

        private void RbVentasDiarias_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb && !rb.Checked)
                return;

            _ventasDiariasModoMonto = _rbVentasMonto.Checked;
            if (!_graficosInicializados || _graficosDTO == null)
                return;

            grafico3();
            formsPlot3.Refresh();
            _grafico3Construido = true;
        }

        /// <summary>
        /// Unifica "Ventas por mes" en un solo gráfico full-width con toggle Monto/Cantidad.
        /// </summary>
        private void ConfigurarLayoutPorMes()
        {
            tlpBajoNivel3Pagina2.SuspendLayout();
            tlpBajoNivel3Pagina2.Controls.Clear();
            tlpBajoNivel3Pagina2.ColumnStyles.Clear();
            tlpBajoNivel3Pagina2.RowStyles.Clear();
            tlpBajoNivel3Pagina2.ColumnCount = 1;
            tlpBajoNivel3Pagina2.RowCount = 2;
            tlpBajoNivel3Pagina2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBajoNivel3Pagina2.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tlpBajoNivel3Pagina2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBajoNivel3Pagina2.Dock = DockStyle.Fill;
            tlpBajoNivel3Pagina2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            var toolbar = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = ColorFondoWinForms,
                Padding = new Padding(10, 6, 10, 4)
            };

            var lblModo = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Text = "Mostrar:",
                Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(31, 26, 43),
                Margin = new Padding(0, 4, 8, 0)
            };

            _rbMensualMonto = new RadioButton
            {
                AutoSize = true,
                Text = "Monto ($)",
                Checked = true,
                Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold),
                Margin = new Padding(0, 2, 16, 0)
            };
            _rbMensualCantidad = new RadioButton
            {
                AutoSize = true,
                Text = "Cantidad",
                Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold),
                Margin = new Padding(0, 2, 16, 0)
            };

            var lblAyuda = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Text = "Barras = año actual · Línea = año anterior",
                Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic),
                ForeColor = System.Drawing.Color.FromArgb(100, 100, 100),
                Margin = new Padding(12, 5, 0, 0)
            };

            _rbMensualMonto.CheckedChanged += RbVentasMensuales_CheckedChanged;
            _rbMensualCantidad.CheckedChanged += RbVentasMensuales_CheckedChanged;

            toolbar.Controls.Add(lblModo);
            toolbar.Controls.Add(_rbMensualMonto);
            toolbar.Controls.Add(_rbMensualCantidad);
            toolbar.Controls.Add(lblAyuda);

            formsPlot5.Dock = DockStyle.Fill;
            formsPlot5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            formsPlot6.Visible = false;

            tlpBajoNivel3Pagina2.Controls.Add(toolbar, 0, 0);
            tlpBajoNivel3Pagina2.Controls.Add(formsPlot5, 0, 1);
            tlpBajoNivel3Pagina2.ResumeLayout();
        }

        private void RbVentasMensuales_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb && !rb.Checked)
                return;

            _ventasMensualesModoMonto = _rbMensualMonto.Checked;
            if (!_graficosInicializados || _graficosDTO == null)
                return;

            grafico5();
            formsPlot5.Refresh();
            _grafico5Construido = true;
        }

        private void ActualizarLeyendaPagos(IReadOnlyList<PagoTipoResumenDTO> pagos, decimal totalGeneral, string titulo)
        {
            _lblTituloPagos.Text = titulo;
            _lblTotalPagos.Text = pagos.Count == 0 ? "Sin pagos en el período" : $"Total: {totalGeneral:C2}";

            _pnlCardsPagos.SuspendLayout();
            _pnlCardsPagos.Controls.Clear();

            for (int i = 0; i < pagos.Count; i++)
            {
                var pago = pagos[i];
                double pct = totalGeneral > 0 ? (double)(pago.Total / totalGeneral * 100) : 0;
                var color = ColoresPie[i % ColoresPie.Length];

                var card = new Panel
                {
                    Width = 230,
                    Height = 58,
                    Margin = new Padding(0, 0, 10, 10),
                    BackColor = System.Drawing.Color.White,
                    Padding = new Padding(8)
                };

                var swatch = new Panel
                {
                    Width = 14,
                    Height = 14,
                    Left = 8,
                    Top = 12,
                    BackColor = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B)
                };

                var lblNombre = new System.Windows.Forms.Label
                {
                    AutoSize = false,
                    Left = 28,
                    Top = 6,
                    Width = 190,
                    Height = 20,
                    Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.FromArgb(31, 26, 43),
                    Text = pago.Nombre ?? "Sin nombre",
                    AutoEllipsis = true
                };

                var lblDetalle = new System.Windows.Forms.Label
                {
                    AutoSize = false,
                    Left = 28,
                    Top = 28,
                    Width = 190,
                    Height = 20,
                    Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular),
                    ForeColor = System.Drawing.Color.FromArgb(80, 80, 80),
                    Text = $"{pago.Total:C2}  ({pct:0.#}%)",
                    AutoEllipsis = true
                };

                card.Controls.Add(swatch);
                card.Controls.Add(lblNombre);
                card.Controls.Add(lblDetalle);
                _pnlCardsPagos.Controls.Add(card);
            }

            _pnlCardsPagos.ResumeLayout();
        }

        /// <summary>
        /// Alinea paneles, pestañas y ScottPlot al mismo fondo del tema (sin marco gris distinto).
        /// </summary>
        private void UnificarFondosGraficos()
        {
            System.Drawing.Color fondo = TemaSistema.Fondo;

            pnlInfoInicial.BackColor = fondo;
            tlpBaseNivel1.BackColor = fondo;
            tlpArribaNivel2.BackColor = fondo;
            tlpBajoNivel2.BackColor = fondo;
            tlpBajoNivel3.BackColor = fondo;
            tlpBajoNivel3Pagina2.BackColor = fondo;

            tabControlGraficoArriba.HeaderBackColor = fondo;
            tabControlGraficoArriba.BackColor = fondo;
            tabControl1.HeaderBackColor = fondo;
            tabControl1.BackColor = fondo;

            foreach (TabPage page in tabControlGraficoArriba.TabPages)
            {
                page.UseVisualStyleBackColor = false;
                page.BackColor = fondo;
            }

            foreach (TabPage page in tabControl1.TabPages)
            {
                page.UseVisualStyleBackColor = false;
                page.BackColor = fondo;
            }

            AplicarFondoPlot(formsPlot1);
            AplicarFondoPlot(formsPlot2);
            AplicarFondoPlot(formsPlot3);
            AplicarFondoPlot(formsPlot5);
            if (formsPlot4 != null)
            {
                formsPlot4.BackColor = fondo;
            }
            if (formsPlot6 != null)
            {
                formsPlot6.BackColor = fondo;
            }
            if (formsPlot7 != null)
                AplicarFondoPlot(formsPlot7);
            if (formsPlot8 != null)
                AplicarFondoPlot(formsPlot8);
            if (formsPlot9 != null)
                AplicarFondoPlot(formsPlot9);
            if (formsPlot10 != null)
                AplicarFondoPlot(formsPlot10);
        }

        private static void AplicarFondoPlot(FormsPlot formsPlot)
        {
            formsPlot.BackColor = ColorFondoWinForms;
            formsPlot.Plot.FigureBackground.Color = ColorFondoPlot;
            formsPlot.Plot.DataBackground.Color = ColorFondoPlot;
            formsPlot.Plot.Benchmark.IsVisible = false;
            ConfigurarInteraccionPlotParaTooltips(formsPlot);
        }

        /// <summary>
        /// Click izquierdo: libre para el cartel de valor (sin pan).
        /// Click derecho + arrastre: pan (pick-and-move), en lugar del zoom derecho por defecto.
        /// </summary>
        private static void ConfigurarInteraccionPlotParaTooltips(FormsPlot formsPlot)
        {
            if (formsPlot == null)
                return;

            var input = formsPlot.UserInputProcessor;
            input.IsEnabled = true;
            input.Reset();

            // Sin pan con click izquierdo: ese gesto robaba el click del cartel.
            input.LeftClickDragPan(false, false, false);

            // Saca el zoom por arrastre derecho y el menú contextual.
            input.RightClickDragZoom(false, false, false);
            input.RemoveAll<ScottPlot.Interactivity.UserActionResponses.SingleClickContextMenu>();

            // Pan con click derecho (la funcionalidad que antes tenía el izquierdo).
            input.UserActionResponses.Add(
                new ScottPlot.Interactivity.UserActionResponses.MouseDragPan(
                    ScottPlot.Interactivity.StandardMouseButtons.Right));
        }

        /// <summary>
        /// Línea de promedio (opcionalmente solo días con actividad / hasta hoy) + marcador del máximo.
        /// </summary>
        private void AgregarPromedioYMaximo(ScottPlot.Plot plot, double[] xs, double[] ys, bool formatoMoneda, bool promedioDiario = false)
        {
            if (xs == null || ys == null || ys.Length == 0)
                return;

            int limite = ys.Length;
            if (promedioDiario && _anioCargado == DateTime.Now.Year && _mesCargado == DateTime.Now.Month)
                limite = Math.Min(limite, DateTime.Now.Day);

            var activos = ys.Take(limite).Where(y => y > 0).ToArray();
            if (activos.Length > 0)
            {
                double promedio = activos.Average();
                var lineaPromedio = plot.Add.HorizontalLine(promedio);
                lineaPromedio.Color = ColorPromedio;
                lineaPromedio.LineWidth = 2;
                lineaPromedio.LinePattern = LinePattern.Dashed;
                lineaPromedio.LegendText = formatoMoneda
                    ? $"Promedio ({FormatoMontoEje(promedio)})"
                    : $"Promedio ({promedio:N1})";
            }

            int idxMax = 0;
            for (int i = 1; i < ys.Length; i++)
            {
                if (ys[i] > ys[idxMax])
                    idxMax = i;
            }

            if (ys[idxMax] <= 0)
                return;

            var marcador = plot.Add.Marker(xs[idxMax], ys[idxMax]);
            marcador.Color = ColorMaximo;
            marcador.Size = 14;
            marcador.Shape = MarkerShape.FilledCircle;
            marcador.LegendText = formatoMoneda
                ? $"Máximo ({FormatoMontoEje(ys[idxMax])})"
                : $"Máximo ({ys[idxMax]:N0})";

            plot.Legend.IsVisible = true;
            plot.Legend.Alignment = Alignment.UpperLeft;
        }

        private void InicializarTabsAnaliticaExtra()
        {
            formsPlot8 = new FormsPlot { Dock = DockStyle.Fill, Name = "formsPlot8", BackColor = ColorFondoWinForms };
            formsPlot9 = new FormsPlot { Dock = DockStyle.Fill, Name = "formsPlot9", BackColor = ColorFondoWinForms };
            formsPlot10 = new FormsPlot { Dock = DockStyle.Fill, Name = "formsPlot10", BackColor = ColorFondoWinForms };

            var tlpProductosGastos = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = ColorFondoWinForms
            };
            tlpProductosGastos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tlpProductosGastos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tlpProductosGastos.Controls.Add(formsPlot8, 0, 0);
            tlpProductosGastos.Controls.Add(formsPlot9, 1, 0);

            var tabProductos = new TabPage("Productos y margen")
            {
                Padding = new Padding(3),
                BackColor = ColorFondoWinForms,
                UseVisualStyleBackColor = false
            };
            tabProductos.Controls.Add(tlpProductosGastos);

            var tabHorarios = new TabPage("Actividad horaria")
            {
                Padding = new Padding(3),
                BackColor = ColorFondoWinForms,
                UseVisualStyleBackColor = false
            };
            tabHorarios.Controls.Add(formsPlot10);

            tabControl1.Controls.Add(tabProductos);
            tabControl1.Controls.Add(tabHorarios);
        }

        /// <summary>
        /// Evita que nombres largos se corten con SizeMode.Fixed del FlatTabControl.
        /// </summary>
        private void ConfigurarAnchoPestanas()
        {
            tabControlGraficoArriba.ItemSize = new Size(170, 28);
            tabControl1.ItemSize = new Size(165, 28);
        }

        private static readonly string[] NombresMesesCortos =
        {
            "Ene", "Feb", "Mar", "Abr", "May", "Jun",
            "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"
        };

        private static readonly ScottPlot.Color ColorTextoEje = ScottPlot.Color.FromHex("#1F1A2B");

        /// <summary>
        /// Montos compactos para ejes (evita $ 3.865.821 apretado).
        /// </summary>
        private static string FormatoMontoEje(double value)
        {
            double abs = Math.Abs(value);
            if (abs >= 1_000_000d)
                return $"${value / 1_000_000d:0.#}M";
            if (abs >= 1_000d)
                return $"${value / 1_000d:0.#}K";
            return $"${value:0}";
        }

        private static string FormatoCantidadEje(double value) => value.ToString("N0");

        private static void EstilizarEjes(
            ScottPlot.Plot plot,
            bool rotarEtiquetasX = false,
            bool montoEnY = false,
            bool cantidadEnY = false,
            bool montoEnX = false)
        {
            plot.Axes.Title.Label.FontSize = 16;
            plot.Axes.Title.Label.Bold = true;
            plot.Axes.Title.Label.ForeColor = ColorTextoEje;

            plot.Axes.Bottom.Label.FontSize = 13;
            plot.Axes.Bottom.Label.Bold = true;
            plot.Axes.Bottom.Label.ForeColor = ColorTextoEje;

            plot.Axes.Left.Label.FontSize = 13;
            plot.Axes.Left.Label.Bold = true;
            plot.Axes.Left.Label.ForeColor = ColorTextoEje;

            plot.Axes.Bottom.TickLabelStyle.FontSize = 12;
            plot.Axes.Bottom.TickLabelStyle.Bold = true;
            plot.Axes.Bottom.TickLabelStyle.ForeColor = ColorTextoEje;

            plot.Axes.Left.TickLabelStyle.FontSize = 12;
            plot.Axes.Left.TickLabelStyle.Bold = true;
            plot.Axes.Left.TickLabelStyle.ForeColor = ColorTextoEje;

            if (rotarEtiquetasX)
            {
                plot.Axes.Bottom.TickLabelStyle.Rotation = 45;
                plot.Axes.Bottom.TickLabelStyle.Alignment = Alignment.MiddleLeft;
                plot.Axes.Bottom.MinimumSize = 55;
            }
            else
            {
                plot.Axes.Bottom.TickLabelStyle.Rotation = 0;
                plot.Axes.Bottom.TickLabelStyle.Alignment = Alignment.UpperCenter;
                plot.Axes.Bottom.MinimumSize = 0;
            }

            if (montoEnY)
            {
                var tickGen = new ScottPlot.TickGenerators.NumericAutomatic
                {
                    LabelFormatter = FormatoMontoEje,
                    MaxTickCount = 7
                };
                plot.Axes.Left.TickGenerator = tickGen;
                plot.Axes.Left.MinimumSize = 58;
            }
            else if (cantidadEnY)
            {
                var tickGen = new ScottPlot.TickGenerators.NumericAutomatic
                {
                    LabelFormatter = FormatoCantidadEje,
                    MaxTickCount = 7
                };
                plot.Axes.Left.TickGenerator = tickGen;
                plot.Axes.Left.MinimumSize = 42;
            }

            if (montoEnX)
            {
                var tickGen = new ScottPlot.TickGenerators.NumericAutomatic
                {
                    LabelFormatter = FormatoMontoEje,
                    MaxTickCount = 6
                };
                plot.Axes.Bottom.TickGenerator = tickGen;
                plot.Axes.Bottom.MinimumSize = 40;
            }
        }

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
        /// Evento de carga principal del formulario. Inicializa filtros y gráficos del dashboard.
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

            // MouseDown: el click se evalúa antes; el pan izquierdo ya está desactivado.
            formsPlot1.MouseDown += FormsPlot1_MouseClick;
            formsPlot2.MouseDown += FormsPlot2_MouseClick;
            formsPlot3.MouseDown += FormsPlot3_MouseClick;
            formsPlot5.MouseDown += FormsPlot5_MouseClick;

            formsPlot1.MouseMove += FormsPlot1_MouseMove;
            formsPlot2.MouseMove += FormsPlot2_MouseMove;
            formsPlot3.MouseMove += FormsPlot3_MouseMove;
            formsPlot5.MouseMove += FormsPlot5_MouseMove;

            formsPlot1.MouseLeave += (_, __) => OcultarTooltip(formsPlot1, ref _lastIndex1);
            formsPlot2.MouseLeave += (_, __) => OcultarTooltip(formsPlot2, ref _lastIndex2);
            formsPlot3.MouseLeave += (_, __) => OcultarTooltip(formsPlot3, ref _lastIndex3);
            formsPlot5.MouseLeave += (_, __) => OcultarTooltip(formsPlot5, ref _lastIndex5);

            ConfigurarInteraccionPlotParaTooltips(formsPlot1);
            ConfigurarInteraccionPlotParaTooltips(formsPlot2);
            ConfigurarInteraccionPlotParaTooltips(formsPlot3);
            ConfigurarInteraccionPlotParaTooltips(formsPlot5);
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
        /// Gráfico 1: Barras agrupadas - Ingresos y egresos por caja en el mes/año seleccionado.
        /// </summary>
        private void grafico1()
        {
            var cajasEnUnMesXyAñoX = _graficosDTO.CajasMes ?? new List<CajaDTO>();
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
            AplicarFondoPlot(formsPlot1);

            string title = $"Ingresos y egresos por caja — {ObtenerNombreMesLocal(_mesCargado ?? DateTime.Now.Month)}";

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
            EstilizarEjes(formsPlot1.Plot, rotarEtiquetasX: false, montoEnY: true);
            formsPlot1.Plot.Axes.Bottom.TickLabelStyle.Rotation = 0;
            formsPlot1.Plot.Axes.Bottom.TickLabelStyle.Alignment = Alignment.UpperCenter;
            formsPlot1.Plot.Axes.Bottom.MinimumSize = 48;
        }

        /// <summary>
        /// Gráfico 2: Scatter - Ingresos de cada caja del mes/año filtrado.
        /// </summary>
        private void grafico2()
        {
            var cajasMes = _graficosDTO.CajasMes ?? new List<CajaDTO>();

            double[] ingresosPorCaja = cajasMes
                .Select(c => (double)c.TotalIngresos)
                .ToArray();

            string[] etiquetas = cajasMes
                .Select(c => $"A: {c.FechaInicio:dd/MM}\nC: {c.FechaFin?.ToString("dd/MM") ?? "Abierta"}")
                .ToArray();

            double[] numerosCajas = Enumerable
                .Range(1, cajasMes.Count)
                .Select(i => (double)i)
                .ToArray();

            _xs2 = numerosCajas;
            _ys2 = ingresosPorCaja;
            _lastIndex2 = -1;

            formsPlot2.Plot.Clear();
            AplicarFondoPlot(formsPlot2);

            string mesNombre = ObtenerNombreMesLocal(_mesCargado ?? DateTime.Now.Month);
            formsPlot2.Plot.Title($"Ingresos por caja — {mesNombre}");
            formsPlot2.Plot.XLabel("Cajas del mes");
            formsPlot2.Plot.YLabel("Ingresos ($)");

            if (numerosCajas.Length > 0)
            {
                var scatter = formsPlot2.Plot.Add.Scatter(numerosCajas, ingresosPorCaja);
                scatter.Color = ColorTendencia;
                scatter.LineWidth = 2;
                scatter.MarkerSize = 8;

                formsPlot2.Plot.Axes.Bottom.SetTicks(numerosCajas, etiquetas);
                formsPlot2.Plot.Axes.AutoScale();
                AgregarPromedioYMaximo(formsPlot2.Plot, numerosCajas, ingresosPorCaja, formatoMoneda: true);
            }

            EstilizarEjes(formsPlot2.Plot, rotarEtiquetasX: false, montoEnY: true);
            formsPlot2.Plot.Axes.Bottom.TickLabelStyle.Rotation = 0;
            formsPlot2.Plot.Axes.Bottom.TickLabelStyle.Alignment = Alignment.UpperCenter;
            formsPlot2.Plot.Axes.Bottom.MinimumSize = 48;
        }

        /// <summary>
        /// Ventas diarias full-width: barras (mes actual) + línea (mes anterior). Toggle Monto/Cantidad.
        /// </summary>
        private void grafico3()
        {
            int año = _anioCargado ?? DateTime.Now.Year;
            int mes = _mesCargado ?? DateTime.Now.Month;
            var ventas = _graficosDTO.VentasMes ?? new List<VentaResumenGraficoDTO>();
            var ventasAnteriorPorDia = _graficosDTO.VentasMesAnteriorPorDia ?? new List<VentaDiaAgregadoDTO>();
            bool modoMonto = _ventasDiariasModoMonto;

            var actualPorDia = modoMonto
                ? ventas.GroupBy(v => v.FechaVenta.Date).ToDictionary(g => g.Key, g => (double)g.Sum(x => x.Total))
                : ventas.GroupBy(v => v.FechaVenta.Date).ToDictionary(g => g.Key, g => (double)g.Count());

            var anteriorPorDia = modoMonto
                ? ventasAnteriorPorDia.ToDictionary(x => x.Dia, x => (double)x.Total)
                : ventasAnteriorPorDia.ToDictionary(x => x.Dia, x => (double)x.Cantidad);

            var diasDelMes = DiasDelMes(año, mes).ToList();
            string[] dias = diasDelMes.Select(x => x.Day.ToString("00")).ToArray();

            double[] valores = diasDelMes
                .Select(fecha => actualPorDia.TryGetValue(fecha, out var v) ? v : 0)
                .ToArray();

            double[] valoresAnterior = diasDelMes
                .Select(fecha => anteriorPorDia.TryGetValue(fecha.Day, out var v) ? v : 0)
                .ToArray();

            double[] posiciones = Enumerable.Range(0, dias.Length).Select(i => (double)i).ToArray();

            _xs3 = posiciones;
            _ys3 = valores;
            _xs3prev = posiciones;
            _ys3prev = valoresAnterior;
            _lastIndex3 = -1;

            formsPlot3.Plot.Clear();
            AplicarFondoPlot(formsPlot3);
            formsPlot3.Plot.Legend.ManualItems.Clear();

            string mesNombre = ObtenerNombreMesLocal(mes);
            formsPlot3.Plot.Title(modoMonto
                ? $"Ventas diarias (monto) — {mesNombre}"
                : $"Ventas diarias (cantidad) — {mesNombre}");
            formsPlot3.Plot.XLabel($"Día de {mesNombre}");
            formsPlot3.Plot.YLabel(modoMonto ? "Total ventas ($)" : "Cantidad de ventas");

            var colorActual = modoMonto ? ColorMonto : ColorCantidad;
            var bars = new List<Bar>();
            for (int i = 0; i < valores.Length; i++)
            {
                bars.Add(new Bar
                {
                    Position = posiciones[i],
                    Value = valores[i],
                    FillColor = colorActual
                });
            }
            formsPlot3.Plot.Add.Bars(bars.ToArray());

            if (valoresAnterior.Any(v => v > 0))
            {
                var linea = formsPlot3.Plot.Add.Scatter(posiciones, valoresAnterior);
                linea.Color = ColorComparativo;
                linea.LineWidth = 3;
                linea.MarkerSize = 6;
            }

            formsPlot3.Plot.Legend.IsVisible = true;
            formsPlot3.Plot.Legend.Alignment = Alignment.UpperLeft;
            formsPlot3.Plot.Legend.ManualItems.Add(new LegendItem
            {
                LabelText = "Mes actual",
                FillColor = colorActual
            });
            if (valoresAnterior.Any(v => v > 0))
            {
                formsPlot3.Plot.Legend.ManualItems.Add(new LegendItem
                {
                    LabelText = "Mes anterior",
                    LineColor = ColorComparativo,
                    LineWidth = 3
                });
            }

            formsPlot3.Plot.Axes.AutoScale();
            formsPlot3.Plot.Axes.Margins(bottom: 0);

            AgregarPromedioYMaximo(formsPlot3.Plot, posiciones, valores, formatoMoneda: modoMonto, promedioDiario: true);
            EstilizarEjes(formsPlot3.Plot, rotarEtiquetasX: false, montoEnY: modoMonto, cantidadEnY: !modoMonto);
            formsPlot3.Plot.Axes.Bottom.SetTicks(posiciones, dias);
            formsPlot3.Plot.Axes.Bottom.TickLabelStyle.Rotation = 0;
            formsPlot3.Plot.Axes.Bottom.TickLabelStyle.Alignment = Alignment.UpperCenter;
        }

        /// <summary>
        /// Ventas mensuales full-width: barras (año actual) + línea (año anterior). Toggle Monto/Cantidad.
        /// </summary>
        private void grafico5()
        {
            int anio = _anioCargado ?? DateTime.Now.Year;
            var ventasAnio = _graficosDTO.VentasAnioPorMes ?? new List<VentaMesAgregadoDTO>();
            var ventasAnioAnterior = _graficosDTO.VentasAnioAnteriorPorMes ?? new List<VentaMesAgregadoDTO>();
            bool modoMonto = _ventasMensualesModoMonto;

            var actualPorMes = modoMonto
                ? ventasAnio.ToDictionary(x => x.Mes, x => (double)x.Total)
                : ventasAnio.ToDictionary(x => x.Mes, x => (double)x.Cantidad);

            var anteriorPorMes = modoMonto
                ? ventasAnioAnterior.ToDictionary(x => x.Mes, x => (double)x.Total)
                : ventasAnioAnterior.ToDictionary(x => x.Mes, x => (double)x.Cantidad);

            double[] xs = Enumerable.Range(0, 12).Select(i => (double)i).ToArray();
            double[] ys = Enumerable.Range(1, 12)
                .Select(m => actualPorMes.TryGetValue(m, out var v) ? v : 0)
                .ToArray();
            double[] ysPrev = Enumerable.Range(1, 12)
                .Select(m => anteriorPorMes.TryGetValue(m, out var v) ? v : 0)
                .ToArray();

            _xs5 = xs;
            _ys5 = ys;
            _ys5prev = ysPrev;
            _lastIndex5 = -1;

            formsPlot5.Plot.Clear();
            AplicarFondoPlot(formsPlot5);
            formsPlot5.Plot.Legend.ManualItems.Clear();

            formsPlot5.Plot.Title(modoMonto
                ? $"Ventas mensuales (monto) — {anio}"
                : $"Ventas mensuales (cantidad) — {anio}");
            formsPlot5.Plot.XLabel("Mes");
            formsPlot5.Plot.YLabel(modoMonto ? "Total ventas ($)" : "Cantidad de ventas");

            var colorActual = modoMonto ? ColorMonto : ColorCantidad;
            var bars = new List<Bar>();
            for (int i = 0; i < 12; i++)
            {
                bars.Add(new Bar
                {
                    Position = xs[i],
                    Value = ys[i],
                    FillColor = colorActual
                });
            }
            formsPlot5.Plot.Add.Bars(bars.ToArray());

            if (ysPrev.Any(v => v > 0))
            {
                var linea = formsPlot5.Plot.Add.Scatter(xs, ysPrev);
                linea.Color = ColorComparativo;
                linea.LineWidth = 3;
                linea.MarkerSize = 6;
            }

            formsPlot5.Plot.Legend.IsVisible = true;
            formsPlot5.Plot.Legend.Alignment = Alignment.UpperLeft;
            formsPlot5.Plot.Legend.ManualItems.Add(new LegendItem
            {
                LabelText = $"{anio}",
                FillColor = colorActual
            });
            if (ysPrev.Any(v => v > 0))
            {
                formsPlot5.Plot.Legend.ManualItems.Add(new LegendItem
                {
                    LabelText = $"{anio - 1}",
                    LineColor = ColorComparativo,
                    LineWidth = 3
                });
            }

            formsPlot5.Plot.Axes.AutoScale();
            formsPlot5.Plot.Axes.Margins(bottom: 0);

            AgregarPromedioYMaximo(formsPlot5.Plot, xs, ys, formatoMoneda: modoMonto);
            EstilizarEjes(formsPlot5.Plot, rotarEtiquetasX: false, montoEnY: modoMonto, cantidadEnY: !modoMonto);
            formsPlot5.Plot.Axes.Bottom.SetTicks(xs, NombresMesesCortos);
            formsPlot5.Plot.Axes.Bottom.TickLabelStyle.Rotation = 0;
            formsPlot5.Plot.Axes.Bottom.TickLabelStyle.Alignment = Alignment.UpperCenter;
        }

        /// <summary>
        /// Gráfico 7: Donut de formas de pago + leyenda lateral (evita el círculo chico en un panel ancho).
        /// </summary>
        private void grafico7()
        {
            var pagos = _graficosDTO.PagosMes ?? new List<PagoTipoResumenDTO>();
            string mesNombre = ObtenerNombreMesLocal(_mesCargado ?? DateTime.Now.Month);
            string titulo = $"Formas de pago — {mesNombre} {_anioCargado}";
            decimal totalGeneral = pagos.Sum(p => p.Total);

            ActualizarLeyendaPagos(pagos, totalGeneral, titulo);

            formsPlot7.Plot.Clear();
            AplicarFondoPlot(formsPlot7);
            formsPlot7.Plot.Legend.IsVisible = false;
            formsPlot7.Plot.Axes.Frameless();
            formsPlot7.Plot.HideGrid();

            if (pagos.Count == 0)
            {
                formsPlot7.Plot.Axes.AutoScale();
                return;
            }

            var slices = new List<PieSlice>();
            for (int i = 0; i < pagos.Count; i++)
            {
                double pct = totalGeneral > 0 ? (double)(pagos[i].Total / totalGeneral * 100) : 0;
                var color = ColoresPie[i % ColoresPie.Length];

                slices.Add(new PieSlice
                {
                    Value = (double)pagos[i].Total,
                    FillColor = color,
                    Label = pct >= 4 ? $"{pct:0.#}%" : string.Empty,
                    LabelFontSize = 14,
                    LabelFontColor = Colors.White,
                    LegendText = string.Empty
                });
            }

            var pie = formsPlot7.Plot.Add.Pie(slices);
            pie.DonutFraction = 0.55;
            pie.SliceLabelDistance = 0.72;
            pie.ExplodeFraction = 0;
            pie.Padding = 0.02;
            pie.LineWidth = 3;
            pie.LineColor = ColorFondoPlot;
            pie.ManageAxisLimits = true;

            var textoCentro = formsPlot7.Plot.Add.Text($"Total\n{totalGeneral:C0}", 0, 0);
            textoCentro.LabelFontSize = 13;
            textoCentro.LabelFontName = "Segoe UI";
            textoCentro.LabelFontColor = ScottPlot.Color.FromHex("#1F1A2B");
            textoCentro.LabelBold = true;
            textoCentro.LabelAlignment = Alignment.MiddleCenter;
        }

        /// <summary>
        /// Gráfico 8: Top 10 productos del mes (barras horizontales por monto).
        /// </summary>
        private void grafico8()
        {
            var top = _graficosDTO.TopProductosMes ?? new List<ProductoTopDTO>();
            string mesNombre = ObtenerNombreMesLocal(_mesCargado ?? DateTime.Now.Month);

            formsPlot8.Plot.Clear();
            AplicarFondoPlot(formsPlot8);
            formsPlot8.Plot.Title($"Top 10 productos — {mesNombre}");
            formsPlot8.Plot.XLabel("Monto vendido");

            if (top.Count == 0)
            {
                formsPlot8.Plot.Axes.AutoScale();
                return;
            }

            var barras = new List<Bar>();
            var ticks = new List<Tick>();

            for (int i = 0; i < top.Count; i++)
            {
                double pos = top.Count - i;
                string nombre = top[i].Nombre ?? "Sin nombre";
                if (nombre.Length > 28)
                    nombre = nombre.Substring(0, 27) + "…";

                barras.Add(new Bar
                {
                    Position = pos,
                    Value = (double)top[i].Total,
                    FillColor = ColorCantidad,
                    Label = FormatoMontoEje((double)top[i].Total)
                });
                ticks.Add(new Tick(pos, nombre));
            }

            var barPlot = formsPlot8.Plot.Add.Bars(barras.ToArray());
            barPlot.Horizontal = true;
            barPlot.ValueLabelStyle.FontSize = 10;

            formsPlot8.Plot.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks.ToArray());
            formsPlot8.Plot.Axes.Left.MajorTickStyle.Length = 0;
            formsPlot8.Plot.Axes.AutoScale();
            formsPlot8.Plot.Axes.Margins(left: 0.38, right: 0.18);

            EstilizarEjes(formsPlot8.Plot, montoEnX: true);
            formsPlot8.Plot.Axes.Left.TickLabelStyle.FontSize = 11;
            formsPlot8.Plot.Axes.Left.MinimumSize = 120;
        }

        /// <summary>
        /// Gráfico 9: Ingresos de caja vs egresos de caja vs gastos del mes.
        /// </summary>
        private void grafico9()
        {
            var cajas = _graficosDTO.CajasMes ?? new List<CajaDTO>();
            decimal ingresos = cajas.Sum(c => c.TotalIngresos);
            decimal egresos = cajas.Sum(c => c.TotalEgresos);
            decimal gastos = _graficosDTO.TotalGastosMes;
            decimal margen = ingresos - egresos - gastos;
            string mesNombre = ObtenerNombreMesLocal(_mesCargado ?? DateTime.Now.Month);

            formsPlot9.Plot.Clear();
            AplicarFondoPlot(formsPlot9);
            formsPlot9.Plot.Title($"Flujo del mes (caja + gastos) — {mesNombre}");
            formsPlot9.Plot.YLabel("Monto ($)");

            ScottPlot.Bar[] barras =
            {
                new() { Position = 1, Value = (double)ingresos, FillColor = ColorIngresos, Label = FormatoMontoEje((double)ingresos) },
                new() { Position = 2, Value = (double)egresos, FillColor = ColorEgresos, Label = FormatoMontoEje((double)egresos) },
                new() { Position = 3, Value = (double)gastos, FillColor = ColorComparativo, Label = FormatoMontoEje((double)gastos) }
            };

            var barPlot = formsPlot9.Plot.Add.Bars(barras);
            barPlot.ValueLabelStyle.FontSize = 11;
            barPlot.ValueLabelStyle.Bold = true;

            Tick[] ticks =
            {
                new(1, "Ingresos"),
                new(2, "Egresos"),
                new(3, "Gastos")
            };
            formsPlot9.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);
            formsPlot9.Plot.Axes.Bottom.MajorTickStyle.Length = 0;

            formsPlot9.Plot.Legend.IsVisible = true;
            formsPlot9.Plot.Legend.Alignment = Alignment.UpperRight;
            formsPlot9.Plot.Legend.ManualItems.Clear();
            formsPlot9.Plot.Legend.ManualItems.Add(new LegendItem
            {
                LabelText = $"Margen: {FormatoMontoEje((double)margen)}",
                FillColor = margen >= 0 ? ColorIngresos : ColorEgresos
            });

            formsPlot9.Plot.Axes.AutoScale();
            formsPlot9.Plot.Axes.Margins(bottom: 0);
            EstilizarEjes(formsPlot9.Plot, montoEnY: true);
        }

        /// <summary>
        /// Gráfico 10: Heatmap día de la semana × hora (cantidad de ventas confirmadas).
        /// </summary>
        private void grafico10()
        {
            var ventas = _graficosDTO.VentasMes ?? new List<VentaResumenGraficoDTO>();
            string mesNombre = ObtenerNombreMesLocal(_mesCargado ?? DateTime.Now.Month);

            // Filas: Lun(0)..Dom(6)  |  Columnas: horas 0..23
            double[,] data = new double[7, 24];
            foreach (var venta in ventas)
            {
                int dia = ((int)venta.FechaVenta.DayOfWeek + 6) % 7;
                int hora = venta.FechaVenta.Hour;
                data[dia, hora] += 1;
            }

            // Plot.Clear() no elimina ColorBars (paneles); hay que sacarlos a mano
            if (_colorBarHeatmap != null)
            {
                formsPlot10.Plot.Remove(_colorBarHeatmap);
                _colorBarHeatmap = null;
            }

            // Por si quedaron colorbars huérfanos de filtros anteriores
            foreach (var panel in formsPlot10.Plot.Axes.GetPanels().OfType<ScottPlot.Panels.ColorBar>().ToList())
                formsPlot10.Plot.Remove(panel);

            formsPlot10.Plot.Clear();
            AplicarFondoPlot(formsPlot10);
            formsPlot10.Plot.Title($"Actividad por día y hora — {mesNombre} {_anioCargado}");
            formsPlot10.Plot.XLabel("Hora del día");
            formsPlot10.Plot.YLabel("Día");

            var hm = formsPlot10.Plot.Add.Heatmap(data);
            hm.Colormap = new ScottPlot.Colormaps.Viridis();
            _colorBarHeatmap = formsPlot10.Plot.Add.ColorBar(hm);

            string[] dias = { "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb", "Dom" };
            var ticksY = dias.Select((d, i) => new Tick(i + 0.5, d)).ToArray();
            var ticksX = Enumerable.Range(0, 24)
                .Where(h => h % 2 == 0)
                .Select(h => new Tick(h + 0.5, h.ToString("00")))
                .ToArray();

            formsPlot10.Plot.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticksY);
            formsPlot10.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticksX);
            formsPlot10.Plot.Axes.AutoScale();
            EstilizarEjes(formsPlot10.Plot);
            formsPlot10.Plot.Axes.Left.TickLabelStyle.FontSize = 13;
            formsPlot10.Plot.Axes.Bottom.TickLabelStyle.FontSize = 12;
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
        // MOTOR DE TOOLTIPS: click para mostrar, salir de la columna para ocultar
        // =================================================================================

        private static int BuscarIndiceCercano(FormsPlot formsPlot, double[] xs, MouseEventArgs e, double umbral)
        {
            if (formsPlot == null || xs == null || xs.Length == 0)
                return -1;

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

            return indexMasCercano != -1 && minimaDistanciaX < umbral
                ? indexMasCercano
                : -1;
        }

        private void MostrarTooltip(FormsPlot formsPlot, string texto, MouseEventArgs e, ref int lastIndex, int index)
        {
            lastIndex = index;
            _currentToolTipText = texto;
            // Arriba del plot para que el mouse no entre al cartel y dispare MouseLeave.
            int x = Math.Max(8, Math.Min(e.X, formsPlot.Width - 160));
            _winFormsToolTip.Show(_currentToolTipText, formsPlot, x, 8);
        }

        /// <summary>
        /// Si hay un tooltip activo por click, lo oculta cuando el mouse deja la columna fijada.
        /// </summary>
        private void EvaluarSalidaTooltip(FormsPlot formsPlot, double[] xs, MouseEventArgs e, ref int lastIndex, double umbral)
        {
            if (lastIndex < 0)
                return;

            if (xs == null || lastIndex >= xs.Length)
            {
                OcultarTooltip(formsPlot, ref lastIndex);
                return;
            }

            Pixel pixelMouse = new Pixel(e.X, e.Y);
            Coordinates coordMouse = formsPlot.Plot.GetCoordinates(pixelMouse);
            if (Math.Abs(xs[lastIndex] - coordMouse.X) >= umbral)
                OcultarTooltip(formsPlot, ref lastIndex);
        }

        private void EvaluarClickTooltip(
            FormsPlot formsPlot,
            double[] xs,
            double[] ys,
            MouseEventArgs e,
            string prefijo,
            string formato,
            ref int lastIndex,
            double umbral)
        {
            if (e.Button != MouseButtons.Left)
                return;

            int index = BuscarIndiceCercano(formsPlot, xs, e, umbral);
            if (index < 0 || ys == null || index >= ys.Length)
            {
                OcultarTooltip(formsPlot, ref lastIndex);
                return;
            }

            MostrarTooltip(
                formsPlot,
                $"{prefijo}: {ys[index].ToString(formato)}",
                e,
                ref lastIndex,
                index);
        }

        private void WinFormsToolTip_Popup(object sender, PopupEventArgs e)
        {
            Size tamanoTexto = TextRenderer.MeasureText(_currentToolTipText, _toolTipFont);
            e.ToolTipSize = new Size(tamanoTexto.Width + 12, tamanoTexto.Height + 8);
        }

        private void WinFormsToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.White, e.Bounds);

            using (Pen lapizBorde = new Pen(System.Drawing.Color.FromArgb(180, 180, 180), 1))
            {
                e.Graphics.DrawRectangle(lapizBorde, 0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1);
            }

            TextFormatFlags alineacion = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(e.Graphics, e.ToolTipText, _toolTipFont, e.Bounds, System.Drawing.Color.Black, alineacion);
        }

        private void OcultarTooltip(FormsPlot formsPlot, ref int lastIndex)
        {
            if (lastIndex == -1)
                return;

            lastIndex = -1;
            _winFormsToolTip.Hide(formsPlot);
        }

        // =================================================================================
        // EVENTOS DE MOUSE POR GRÁFICO
        // =================================================================================
        private void FormsPlot1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (_xs1 == null || _ys1 == null || _egresos1 == null || _xs1.Length == 0)
            {
                OcultarTooltip(formsPlot1, ref _lastIndex1);
                return;
            }

            int index = BuscarIndiceCercano(formsPlot1, _xs1, e, TooltipThresholdBars);
            if (index < 0)
            {
                OcultarTooltip(formsPlot1, ref _lastIndex1);
                return;
            }

            MostrarTooltip(
                formsPlot1,
                $"Ingreso: {_ys1[index]:C2}\nEgreso: {_egresos1[index]:C2}",
                e,
                ref _lastIndex1,
                index);
        }

        private void FormsPlot1_MouseMove(object sender, MouseEventArgs e)
            => EvaluarSalidaTooltip(formsPlot1, _xs1, e, ref _lastIndex1, TooltipThresholdBars);

        private void FormsPlot2_MouseClick(object sender, MouseEventArgs e)
            => EvaluarClickTooltip(formsPlot2, _xs2, _ys2, e, "Ingresos", "C2", ref _lastIndex2, TooltipThreshold);

        private void FormsPlot2_MouseMove(object sender, MouseEventArgs e)
            => EvaluarSalidaTooltip(formsPlot2, _xs2, e, ref _lastIndex2, TooltipThreshold);

        private void FormsPlot3_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (_xs3 == null || _ys3 == null || _xs3.Length == 0)
            {
                OcultarTooltip(formsPlot3, ref _lastIndex3);
                return;
            }

            int index = BuscarIndiceCercano(formsPlot3, _xs3, e, TooltipThresholdBars);
            if (index < 0)
            {
                OcultarTooltip(formsPlot3, ref _lastIndex3);
                return;
            }

            string formato = _ventasDiariasModoMonto ? "C2" : "N0";
            double prev = _ys3prev != null && index < _ys3prev.Length ? _ys3prev[index] : 0;
            int dia = index + 1;
            MostrarTooltip(
                formsPlot3,
                $"Día {dia:00}\nActual: {_ys3[index].ToString(formato)}\nAnterior: {prev.ToString(formato)}",
                e,
                ref _lastIndex3,
                index);
        }

        private void FormsPlot3_MouseMove(object sender, MouseEventArgs e)
            => EvaluarSalidaTooltip(formsPlot3, _xs3, e, ref _lastIndex3, TooltipThresholdBars);

        private void FormsPlot5_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (_xs5 == null || _ys5 == null || _xs5.Length == 0)
            {
                OcultarTooltip(formsPlot5, ref _lastIndex5);
                return;
            }

            int index = BuscarIndiceCercano(formsPlot5, _xs5, e, TooltipThresholdBars);
            if (index < 0)
            {
                OcultarTooltip(formsPlot5, ref _lastIndex5);
                return;
            }

            int anio = _anioCargado ?? DateTime.Now.Year;
            string formato = _ventasMensualesModoMonto ? "C2" : "N0";
            double prev = _ys5prev != null && index < _ys5prev.Length ? _ys5prev[index] : 0;
            string mes = index < NombresMesesCortos.Length
                ? NombresMesesCortos[index]
                : (index + 1).ToString("00");

            MostrarTooltip(
                formsPlot5,
                $"{mes}\n{anio}: {_ys5[index].ToString(formato)}\n{anio - 1}: {prev.ToString(formato)}",
                e,
                ref _lastIndex5,
                index);
        }

        private void FormsPlot5_MouseMove(object sender, MouseEventArgs e)
            => EvaluarSalidaTooltip(formsPlot5, _xs5, e, ref _lastIndex5, TooltipThresholdBars);

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

                case 2:
                    if (!_grafico7Construido)
                    {
                        grafico7();
                        _grafico7Construido = true;
                        formsPlot7.Refresh();
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
                    break;

                case 1:
                    if (!_grafico5Construido)
                    {
                        grafico5();
                        _grafico5Construido = true;
                    formsPlot5.Refresh();
                    }
                    break;

                case 2:
                    if (!_grafico8Construido)
                    {
                        grafico8();
                        _grafico8Construido = true;
                        formsPlot8.Refresh();
                    }
                    if (!_grafico9Construido)
                    {
                        grafico9();
                        _grafico9Construido = true;
                        formsPlot9.Refresh();
                    }
                    break;

                case 3:
                    if (!_grafico10Construido)
                    {
                        grafico10();
                        _grafico10Construido = true;
                        formsPlot10.Refresh();
                    }
                    break;
            }
        }
    }
}