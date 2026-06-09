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
    /// <summary>
    /// Estructura auxiliar interna para mapear de forma segura los objetos de los ComboBoxes.
    /// Permite desacoplar el índice visual del control (.SelectedIndex) de los valores reales de la consulta.
    /// </summary>


    /// <summary>
    /// Formulario de Administración Centralizada.
    /// Encargado de la navegación modular y el procesamiento de métricas financieras mediante gráficos estadísticos.
    /// </summary>
    public partial class FAdministracion : FBase.FBase
    {
        // ==========================================
        // DEPENDENCIAS Y SERVICIOS DE CAPA DE NEGOCIO
        // ==========================================
        private readonly long _logeadoId;
        private readonly CajaServicio _cajaSerivicio;
        private readonly VentaServicio _ventaServicio;
        List<CajaDTO> todasLasCajas;

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
        private double[] _xs1, _ys1; // Gráfico 1: Histórico Cajas del Mes
        private double[] _xs2, _ys2; // Gráfico 2: Cajas Últimos 31 Días
        private double[] _xs3, _ys3; // Gráfico 3: Ganancias Diarias del Mes
        private double[] _xs4, _ys4; // Gráfico 4: Cantidad de Ventas Diarias
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

            _logeadoId = logeadoId;

            // Inicialización de la lógica de negocio
            _cajaSerivicio = new CajaServicio();
            _ventaServicio = new VentaServicio();

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

            // Ejecutamos la carga inicial dinámica analizando la base de datos de manera segura
            InicializarFiltrosCronologicos();
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
        private void FAdministracion_Load(object sender, EventArgs e)
        {
            // Verificación tipada segura: Leemos los estados pre-calculados por los combos dinámicos en vez de usar DateTime.Now directo
            if (cbAñoGraficos.SelectedItem is int año && cbMesGrafico.SelectedValue is int mes)
            {
                grafico1(mes, año);
                grafico2(); // Histórico estático de los últimos 31 días agrupados
                grafico3(mes, año);
                grafico4(mes, año);
                grafico5(año);
                grafico6(año);
            }
            else
            {
                // Fallback de contingencia por si falla la resolución de tipos en los combos
                grafico1(); grafico2(); grafico3(); grafico4(); grafico5(); grafico6();
            }

            // Ajuste de ejes del gráfico 6 y refresco inicial obligatorio
            formsPlot6.Plot.Axes.AutoScale();
            formsPlot6.Refresh();

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

        private void btnFiltrarGraficos_Click(object sender, EventArgs e)
        {
            if (cbAñoGraficos.SelectedItem is int añoFiltrado && cbMesGrafico.SelectedValue is int mesFiltrado)
            {
                filtrarGraficos(añoFiltrado, mesFiltrado);
            }
        }

        private void cbAñoGraficos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAñoGraficos.SelectedItem is int añoFiltrado)
            {
                // Apagamos el trigger del mes intermitentemente para evitar llamadas huérfanas 
                // con combinaciones de mes/año inexistentes durante el cambio de DataSource
                cbMesGrafico.SelectedIndexChanged -= cbMesGrafico_SelectedIndexChanged;

                ActualizarComboMeses(añoFiltrado);

                cbMesGrafico.SelectedIndexChanged += cbMesGrafico_SelectedIndexChanged;

                if (cbMesGrafico.SelectedValue is int mesFiltrado)
                {
                    filtrarGraficos(añoFiltrado, mesFiltrado);
                }
            }
        }

        private void cbMesGrafico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAñoGraficos.SelectedItem is int añoFiltrado && cbMesGrafico.SelectedValue is int mesFiltrado)
            {
                filtrarGraficos(añoFiltrado, mesFiltrado);
            }
        }

        /// <summary>
        /// Refresca en bloque los gráficos dependientes de filtros temporales.
        /// </summary>
        private void filtrarGraficos(int año, int mes)
        {
            grafico1(mes, año);
            grafico3(mes, año);
            grafico4(mes, año);
            grafico5(año);
            grafico6(año);
        }

        // =================================================================================
        // ARQUITECTURA INTERNA DE LOS GRÁFICOS (SCOTTPLOT)
        // Todos los métodos respetan el patrón: 1. Leer BD -> 2. Formatear Vectores -> 3. Dibujar -> 4. Guardar variables de mouse
        // =================================================================================

        /// <summary>
        /// Gráfico 1: Scatter (Puntos y Líneas) - Muestra los ingresos brutos individuales de cada caja en el mes/año provisto.
        /// </summary>
        private void grafico1(int? mes = null, int? año = null)
        {
            // 1. Obtención de datos con parámetros por defecto en caso de nulidad
            var cajasEnUnMesXyAñoX = _cajaSerivicio.ObtenerCajasPorMesYAño(DateTime.Now.Month, DateTime.Now.Year);

            if (mes.HasValue && año.HasValue)
            {
                cajasEnUnMesXyAñoX = _cajaSerivicio.ObtenerCajasPorMesYAño(mes.Value, año.Value);
            }

            // 2. Proyección de colecciones hacia arrays de tipos primitivos (Requerido por ScottPlot)
            double[] gananciasPorCaja = cajasEnUnMesXyAñoX.Select(c => (double)c.TotalIngresos).ToArray();
            string[] fechasDeCadaCaja = cajasEnUnMesXyAñoX
                .Select(c => $"A: {c.FechaInicio:dd/MM}\nC: {c.FechaFin?.ToString("dd/MM") ?? "Abierta"}")
                .ToArray();

            // El eje X requiere un array incremental secuencial (1, 2, 3...) sobre el cual mapear los Ticks textuales
            double[] numerosCajas = Enumerable.Range(1, cajasEnUnMesXyAñoX.Count).Select(i => (double)i).ToArray();

            // 3. Resguardo de estado en variables globales de la clase para el motor de Tooltips
            _xs1 = numerosCajas;
            _ys1 = gananciasPorCaja;
            _lastIndex1 = -1;

            // 4. Renderizado en el componente UI
            formsPlot1.Plot.Clear(); // Limpieza del buffer del lienzo anterior

            string nombreMes = mes.HasValue ? ObtenerNombreMesLocal(mes.Value) : ObtenerNombreMesLocal(DateTime.Now.Month);
            string title = $"Cajas en {nombreMes}";

            formsPlot1.Plot.Title(title);
            formsPlot1.Plot.XLabel("Fecha de las Cajas");
            formsPlot1.Plot.YLabel("Total Ingresos");

            // Añade el tipo de gráfico lineal
            var scatter = formsPlot1.Plot.Add.Scatter(numerosCajas, gananciasPorCaja);
            // Sobrescribe los números del eje X con las etiquetas personalizadas de fechas ("A: 10/05...")
            formsPlot1.Plot.Axes.Bottom.SetTicks(numerosCajas, fechasDeCadaCaja);
            formsPlot1.Plot.Axes.AutoScale();
            formsPlot1.Refresh(); // Renderiza los cambios en pantalla

            scatter.Color = ScottPlot.Color.FromHex("#291a3e");
        }

        /// <summary>
        /// Gráfico 2: Scatter (Puntos y Líneas) - Muestra ingresos agrupados por día de los últimos 31 días.
        /// </summary>
        private void grafico2()
        {
            var cajasUltimos31Dias = _cajaSerivicio.ObtenerCajasUltimosXDias(31);
            // Agrupamiento estricto por truncamiento de fecha (.Date) para consolidar ingresos diarios
            var cajasPorDia = cajasUltimos31Dias.GroupBy(c => c.FechaInicio.Date).OrderBy(g => g.Key).ToList();

            double[] ingresosPorDia = cajasPorDia.Select(g => (double)g.Sum(c => c.TotalIngresos)).ToArray();
            string[] dias = cajasPorDia.Select(g => g.Key.ToString("dd/MM")).ToArray();
            double[] numerosDias = Enumerable.Range(1, cajasPorDia.Count).Select(i => (double)i).ToArray();

            _xs2 = numerosDias;
            _ys2 = ingresosPorDia;
            _lastIndex2 = -1;

            formsPlot2.Plot.Clear();
            formsPlot2.Plot.Title("Cajas ultimos 31 dias agrupadas por fecha");
            formsPlot2.Plot.XLabel("Fecha de las Cajas");
            formsPlot2.Plot.YLabel("Total Ingresos");

            var scatter = formsPlot2.Plot.Add.Scatter(numerosDias, ingresosPorDia);
            formsPlot2.Plot.Axes.Bottom.SetTicks(numerosDias, dias);
            formsPlot2.Plot.Axes.AutoScale();
            formsPlot2.Refresh();

            scatter.Color = ScottPlot.Color.FromHex("#291a3e");
        }

        /// <summary>
        /// Gráfico 3: Bars (Barras) - Muestra la sumatoria económica diaria total de ventas en el mes.
        /// </summary>
        private void grafico3(int? mes = null, int? año = null)
        {
            var GananciasMesXAñoX = _ventaServicio.ObtenerVentasPorMesYAño(System.DateTime.Now.Month, System.DateTime.Now.Year);

            if (mes.HasValue && año.HasValue)
            {
                GananciasMesXAñoX = _ventaServicio.ObtenerVentasPorMesYAño(mes.Value, año.Value);
            }

            var ganaciasAgrupadasPorFecha = GananciasMesXAñoX
                .GroupBy(i => i.FechaVenta.Date)
                .Select(g => new { Fecha = g.Key, IngresoTotal = g.Sum(x => x.Total) })
                .OrderBy(x => x.Fecha)
                .ToList();

            string[] diaDeLasGanancias = ganaciasAgrupadasPorFecha.Select(x => x.Fecha.ToString("dd/MM")).ToArray();
            double[] cantidadesGananciasPorDia = ganaciasAgrupadasPorFecha.Select(x => (double)x.IngresoTotal).ToArray();
            double[] posicionesDiasGanancias = Enumerable.Range(0, diaDeLasGanancias.Length).Select(i => (double)i).ToArray();

            _xs3 = posicionesDiasGanancias;
            _ys3 = cantidadesGananciasPorDia;
            _lastIndex3 = -1;

            formsPlot3.Plot.Clear();

            string nombreMes = mes.HasValue ? ObtenerNombreMesLocal(mes.Value) : ObtenerNombreMesLocal(DateTime.Now.Month);
            string title = $"Ganancias diarias en {nombreMes}";

            formsPlot3.Plot.Title(title);
            formsPlot3.Plot.XLabel("Dias");
            formsPlot3.Plot.YLabel("Total Ventas");

            // Instancia gráfico de barras nativo
            var bars = formsPlot3.Plot.Add.Bars(cantidadesGananciasPorDia);
            formsPlot3.Plot.Axes.Bottom.SetTicks(posicionesDiasGanancias, diaDeLasGanancias);
            formsPlot3.Plot.Axes.AutoScale();
            formsPlot3.Refresh();

            bars.Color = ScottPlot.Color.FromHex("#291a3e");
        }

        /// <summary>
        /// Gráfico 4: Bars (Barras) - Cantidad transaccional de ventas brutas realizadas por día.
        /// </summary>
        private void grafico4(int? mes = null, int? año = null)
        {
            var ventasMesXAñoX = _ventaServicio.ObtenerVentasPorMesYAño(System.DateTime.Now.Month, System.DateTime.Now.Year);

            if (año.HasValue && mes.HasValue)
            {
                ventasMesXAñoX = _ventaServicio.ObtenerVentasPorMesYAño(mes.Value, año.Value);
            }

            var ventarAgrupadasPorFecha = ventasMesXAñoX.Select(i => i.FechaVenta.Date)
                .GroupBy(fecha => fecha)
                .Select(g => new { Fecha = g.Key, CantidadVentas = g.Count() }) // Mapeo con .Count() en lugar de .Sum()
                .OrderBy(x => x.Fecha)
                .ToList();

            string[] diaDeLasVentas = ventarAgrupadasPorFecha.Select(x => x.Fecha.ToString("dd/MM")).ToArray();
            double[] cantidadesVentasPorDia = ventarAgrupadasPorFecha.Select(x => (double)x.CantidadVentas).ToArray();
            double[] posicionesDiasVentas = Enumerable.Range(0, diaDeLasVentas.Length).Select(i => (double)i).ToArray();

            _xs4 = posicionesDiasVentas;
            _ys4 = cantidadesVentasPorDia;
            _lastIndex4 = -1;

            formsPlot4.Plot.Clear();

            string nombreMes = mes.HasValue ? ObtenerNombreMesLocal(mes.Value) : ObtenerNombreMesLocal(DateTime.Now.Month);
            string title = $"Ventas diarias en {nombreMes}";

            formsPlot4.Plot.Title(title);
            formsPlot4.Plot.XLabel("Dias");
            formsPlot4.Plot.YLabel("Total Ventas");

            var bars = formsPlot4.Plot.Add.Bars(cantidadesVentasPorDia);
            formsPlot4.Plot.Axes.Bottom.SetTicks(posicionesDiasVentas, diaDeLasVentas);
            formsPlot4.Plot.Axes.AutoScale();
            formsPlot4.Refresh();

            bars.Color = ScottPlot.Color.FromHex("#291a3e");
        }

        /// <summary>
        /// Gráfico 5: Bars (Barras) - Acumulado mensual financiero anualizado.
        /// </summary>
        private void grafico5(int? año = null)
        {
            var cajasAñoX = _cajaSerivicio.ObtenerLasCajasDeXAño(DateTime.Now.Year);

            if (año.HasValue)
            {
                cajasAñoX = _cajaSerivicio.ObtenerLasCajasDeXAño(año.Value);
            }

            // Agrupación de claves compuestas (Año y Mes) para aislar balances de periodos interanuales
            var fechasYGanaciasAgrupadasPorMeses = cajasAñoX.GroupBy(c => new { c.FechaInicio.Year, c.FechaInicio.Month })
                .Select(g => new { Fecha = new DateTime(g.Key.Year, g.Key.Month, 1), Balance = g.Sum(c => c.TotalIngresos) })
                .ToList();

            string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            // Desplazamiento aritmético (Month - 1) para coordinar el índice base cero de los arrays con el calendario gregoriano (1-12)
            double[] xs = fechasYGanaciasAgrupadasPorMeses.Select(x => (double)(x.Fecha.Month - 1)).ToArray();
            double[] ganaciasPorMesEjeY = fechasYGanaciasAgrupadasPorMeses.Select(x => (double)x.Balance).ToArray();
            string[] mesesPresentesEjeX = fechasYGanaciasAgrupadasPorMeses.Select(x => meses[x.Fecha.Month - 1]).ToArray();

            _xs5 = xs;
            _ys5 = ganaciasPorMesEjeY;
            _lastIndex5 = -1;

            formsPlot5.Plot.Clear();
            string title = año.HasValue ? $"Ganancias en {año.Value}" : $"Ganancias en {DateTime.Now.Year}";

            formsPlot5.Plot.Title(title);
            formsPlot5.Plot.XLabel("Meses");
            formsPlot5.Plot.YLabel("Total Ingresos");

            var bars = formsPlot5.Plot.Add.Bars(xs, ganaciasPorMesEjeY);
            formsPlot5.Plot.Axes.Bottom.SetTicks(xs, mesesPresentesEjeX);
            // Rotación visual a 45 grados de las etiquetas de texto del eje inferior para evitar solapamientos tipográficos
            formsPlot5.Plot.Axes.Bottom.TickLabelStyle.Rotation = 45;
            formsPlot5.Plot.Axes.AutoScale();
            formsPlot5.Refresh();

            bars.Color = ScottPlot.Color.FromHex("#291a3e");
        }

        /// <summary>
        /// Gráfico 6: Bars (Barras) - Volumen total de operaciones comerciales anualizado por mes.
        /// </summary>
        private void grafico6(int? año = null)
        {
            var ventasAñoX = _ventaServicio.ObtenerVentasPorMesYAño(0, 2026);

            if (año.HasValue)
            {
                ventasAñoX = _ventaServicio.ObtenerVentasPorMesYAño(0, año.Value);
            }

            var fechasYVentasAgrupadasPorMeses = ventasAñoX.GroupBy(c => new { c.FechaVenta.Year, c.FechaVenta.Month })
                .Select(g => new { Fecha = new DateTime(g.Key.Year, g.Key.Month, 1), QuantityVentas = g.Count() })
                .ToList();

            string[] mesesVentas = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            double[] xsVentas = fechasYVentasAgrupadasPorMeses.Select(x => (double)(x.Fecha.Month - 1)).ToArray();
            double[] ventasPorMesEjeY = fechasYVentasAgrupadasPorMeses.Select(x => (double)x.QuantityVentas).ToArray();
            string[] mesesVentasPresentesEjeX = fechasYVentasAgrupadasPorMeses.Select(x => mesesVentas[x.Fecha.Month - 1]).ToArray();

            _xs6 = xsVentas;
            _ys6 = ventasPorMesEjeY;
            _lastIndex6 = -1;

            formsPlot6.Plot.Clear();
            string title = año.HasValue ? $"Ventas en {año.Value}" : $"Ventas in {DateTime.Now.Year}";

            formsPlot6.Plot.Title(title);
            formsPlot6.Plot.XLabel("Meses");
            formsPlot6.Plot.YLabel("Total Ingresos");

            var bars = formsPlot6.Plot.Add.Bars(xsVentas, ventasPorMesEjeY);
            formsPlot6.Plot.Axes.Bottom.SetTicks(xsVentas, mesesVentasPresentesEjeX);
            formsPlot6.Plot.Axes.AutoScale();
            formsPlot6.Refresh();

            bars.Color = ScottPlot.Color.FromHex("#291a3e");
        }

        private void btnFechaActualGraficos_Click(object sender, EventArgs e)
        {
            // Resetea y sincroniza visualmente el estado del dashboard a la fecha real de hoy
            InicializarFiltrosCronologicos();
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
        private void cUENTASCORRIENTESToolStripMenuItem_Click(object sender, EventArgs e) => new FCuentaCorrienteConsulta().Show();
        private void lISTADOOFERTASToolStripMenuItem_Click(object sender, EventArgs e) => new FOfertaConsulta().Show();
        private void aCTIVARDESACTIVARToolStripMenuItem_Click(object sender, EventArgs e) => new FOfertaConsulta(true, "a").Show();
        private void lOTESToolStripMenuItem_Click(object sender, EventArgs e) => new FLoteConsulta().Show();
        private void btnMovimientos_Click(object sender, EventArgs e) => new FMovimientoConsulta().Show();
        private void tIPOPAGOToolStripMenuItem_Click(object sender, EventArgs e) => new FTipoPagoConsulta().Show();
        private void rOLESToolStripMenuItem_Click(object sender, EventArgs e) => new FRolConsulta().Show();
        private void nUEVAOFERTAToolStripMenuItem_Click(object sender, EventArgs e) => new FOfertaGrupoABM().ShowDialog();
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
            if (indexMasCercano != -1 && minimaDistanciaX < 0.4)
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
        private void FormsPlot1_MouseMove(object sender, MouseEventArgs e) => EvaluarPosicionMouse(formsPlot1, _xs1, _ys1, e, "Ingreso", "C2", ref _lastIndex1);
        private void FormsPlot2_MouseMove(object sender, MouseEventArgs e) => EvaluarPosicionMouse(formsPlot2, _xs2, _ys2, e, "Total Día", "C2", ref _lastIndex2);
        private void FormsPlot3_MouseMove(object sender, MouseEventArgs e) => EvaluarPosicionMouse(formsPlot3, _xs3, _ys3, e, "Ganancia", "C2", ref _lastIndex3);
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
    }
}