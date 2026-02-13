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
using Presentacion.Core.Producto.Rubro;
using Presentacion.Core.TipoPago;
using ScottPlot;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Administracion
{
    public partial class FAdministracion : Form
    {
        private readonly long _logeadoId;
        private readonly CajaServicio _cajaSerivicio;
        private readonly VentaServicio _ventaServicio;
        List<CajaDTO> todasLasCajas;

        public FAdministracion(long logeadoId)
        {
            InitializeComponent();
            _logeadoId = logeadoId;

            _cajaSerivicio = new CajaServicio();
            _ventaServicio = new VentaServicio();

            //ComboBox de meses para el filtro del grafico de ganancias por mes
            var meses = DateTimeFormatInfo.CurrentInfo.MonthNames
                .Where(m => !string.IsNullOrEmpty(m))
                .ToArray();

            cbMesGrafico.DataSource = meses;

            cbMesGrafico.DropDownStyle = ComboBoxStyle.DropDown;
            cbMesGrafico.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbMesGrafico.AutoCompleteSource = AutoCompleteSource.ListItems;

            //ComboBox de años para el filtro del grafico de ganancias por mes
            //Buscar el año mas viejo en ventas o en caja, asi limitamos el rango de años a mostrar en el combo

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
            var fmarca = new FMarcaConsulta();

            fmarca.Show();
        }

        private void cATEGORIASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fCategoria = new FCategoriaConsulta();

            fCategoria.Show();
        }

        private void rUBROSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fRubro = new FRubroConsulta();
            fRubro.Show();
        }

        private void lISTADOEMPLEADOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fEmpleado = new FEmpleadoConsulta();
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
            //ver porque creo q falta una pantalla intermedia entre creacion y listado

            var FOferta = new FOfertaConsulta();
            FOferta.Show();

        }

        private void aCTIVARDESACTIVARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FActDesac = new FOfertaConsulta(true, "a");
            FActDesac.Show();
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
            var FOferta = new FSeleccionTipoOferta();
            FOferta.Show();
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

        private void formsPlot1_Load(object sender, EventArgs e)
        {

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
            ////grafico X////

            //Ingresos, egresos
            //La idea que sea los dos graficos en un mismo formplot, barras lado a lado puede ser una 
            //o directamente con scatter
            //capaz es al pedo mostrar ingresos si ya lo estmaos mostrando como en 2 graficos pero en diferentes contextos

            ////////////////////////////////Deberiamos mostrar el balance final y no el total de ingresos
        }

        private void btnFiltrarGraficos_Click(object sender, EventArgs e)
        {
            int añoFiltrado = (int)cbAñoGraficos.SelectedItem;
            int mesFiltrado = cbMesGrafico.SelectedIndex + 1;

            filtrarGraficos(añoFiltrado, mesFiltrado);
        }

        private void filtrarGraficos(int año, int mes)
        {
            grafico3(año);
            grafico4(mes, año);
            grafico5(año);
            grafico6(mes, año);
        }

        private void grafico1()
        {

            ////Primer grafico////
            ///Ganacias por cajas de un MES y un AÑO especificos

            var ultimas31Cajas = _cajaSerivicio.ObtenerUltimasXCajas(31); //CAMBIAR LA FUNCION

            //LO SIGUIENTE SERIA PODER MODIFICAR EL MES Y AÑO DE LAS VENTAS

            double[] gananciasPorCaja = ultimas31Cajas
                .Select(c => (double)c.TotalIngresos)
                .ToArray();

            string[] fechasDeCadaCaja = ultimas31Cajas
            .Select(c => $"A: {c.FechaInicio:dd/MM}\nC: {c.FechaFin?.ToString("dd/MM") ?? "Abierta"}")
            .ToArray();

            double[] numerosCajas = Enumerable
                .Range(1, ultimas31Cajas.Count)
                .Select(i => (double)i)
                .ToArray();

            formsPlot1.Plot.Title("Caja Ultimo Mes");
            formsPlot1.Plot.XLabel("Fecha de las Cajas");
            formsPlot1.Plot.YLabel("Total Ingresos");


            formsPlot1.Plot.Add.Scatter(numerosCajas, gananciasPorCaja); //Eje x e y

            formsPlot1.Plot.Axes.Bottom.SetTicks(numerosCajas, fechasDeCadaCaja); //Etiquetas eje x

            formsPlot1.Plot.Axes.AutoScale();
            formsPlot1.Refresh();
        }

        private void grafico2()
        {
            ////Segundo Grafico////
            ///Ganacias por cajas agrupadas por X cantidad de dias (ultimos 31 dias actual)  // Este grafico no me cierra mucho teniendo el grafico 1

            //Agrupar las cajas por dia de apertura y sumar los ingresos de cada dia

            // LO SIGUIENTE SERIA PODER MODIFICAR LA CANTIDAD DE DIAS A MOSTRAR O PODER CAMBIAR EL MES 

            var cajasUltimos31Dias = _cajaSerivicio.ObtenerCajasUltimosXDias(31);

            var cajasPorDia = cajasUltimos31Dias
            .GroupBy(c => c.FechaInicio.Date)
            .OrderBy(g => g.Key)
            .ToList();

            double[] ingresosPorDia = cajasPorDia //Eje Y el total de ingresos (deberia ser balance final, pero no entraria la caja sin cerrar creo)
                .Select(g => (double)g.Sum(c => c.TotalIngresos))
                .ToArray();

            string[] dias = cajasPorDia.Select(g => g.Key.ToString("dd/MM")).ToArray();

            double[] numerosDias = Enumerable
                .Range(1, cajasPorDia.Count)
                .Select(i => (double)i)
                .ToArray();


            formsPlot2.Plot.Clear();

            formsPlot2.Plot.Title("Cajas ultimos 31 dias agrupadas por fecha");
            formsPlot2.Plot.XLabel("Fecha de las Cajas");
            formsPlot2.Plot.YLabel("Total Ingresos");

            formsPlot2.Plot.Add.Scatter(numerosDias, ingresosPorDia);

            formsPlot2.Plot.Axes.Bottom.SetTicks(numerosDias, dias); //Etiquetas eje x

            formsPlot2.Plot.Axes.AutoScale();
            formsPlot2.Refresh();
        }

        private void grafico3(int? año = null)
        {
            ////Tercer grafico/////
            ///Ganancias por meses en un año especifico

            var cajasAñoX = _cajaSerivicio.ObtenerLasCajasDeXAño(DateTime.Now.Year); //Filtro de año

            if (año.HasValue)
            {
                int añoBusqueda = año.Value;

                cajasAñoX = _cajaSerivicio.ObtenerLasCajasDeXAño(añoBusqueda); //Filtro de año

            }

            var fechasYGanaciasAgrupadasPorMeses = cajasAñoX.GroupBy(c => new { c.FechaInicio.Year, c.FechaInicio.Month })
            .Select(g => new
            {
                Fecha = new DateTime(g.Key.Year, g.Key.Month, 1),
                Balance = g.Sum(c => c.TotalIngresos)//deberia ser balance final?
            }).ToList();

            string[] meses =
            {
                "Enero", "Febrero", "Marzo", "Abril",
                "Mayo", "Junio", "Julio", "Agosto",
                "Septiembre", "Octubre", "Noviembre", "Diciembre"
            };

            // X = índices (0..11)
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


            formsPlot3.Plot.Clear();

            formsPlot3.Plot.Title("Ganacias por mes");
            formsPlot3.Plot.XLabel("Meses");
            formsPlot3.Plot.YLabel("Total Ingresos");

            formsPlot3.Plot.Add.Bars(xs, ganaciasPorMesEjeY);

            //Meses como labels del eje X
            formsPlot3.Plot.Axes.Bottom.SetTicks(xs, mesesPresentesEjeX);

            //Rotar etiquetas
            formsPlot3.Plot.Axes.Bottom.TickLabelStyle.Rotation = 45;

            formsPlot3.Plot.Axes.AutoScale();
            formsPlot3.Refresh();
        }

        private void grafico4(int? mes = null, int? año = null)
        {
            ////Cuarto grafico////
            ///Ganacias por dias en un mes y año especificos

            var GananciasMesXAñoX = _ventaServicio.ObtenerVentasPorMesYAño(System.DateTime.Now.Month, System.DateTime.Now.Year); //traemos toda las ventas de un mes y año especificos 

            if(mes.HasValue && año.HasValue)
            {
                int añoBusqueda = año.Value;
                int mesBusqueda = mes.Value;

                GananciasMesXAñoX = _ventaServicio.ObtenerVentasPorMesYAño(mesBusqueda, añoBusqueda); //traemos toda las ventas de un mes y año especificos 
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

            string[] diaDeLasGanancias = ganaciasAgrupadasPorFecha //seleccionamos las fechas de las ventas
                .Select(x => x.Fecha.ToString("dd/MM"))
                .ToArray();

            double[] cantidadesGananciasPorDia = ganaciasAgrupadasPorFecha.Select(x => (double)x.IngresoTotal).ToArray(); //seleccionamos la cantidad de ventas por dia (valor de las bars)

            double[] posicionesDiasGanancias = Enumerable.Range(0, diaDeLasGanancias.Length)
                                .Select(i => (double)i)
                                .ToArray();

            formsPlot4.Plot.Clear();

            formsPlot4.Plot.Title("Ganancias por Dia en el ultimo Mes");
            formsPlot4.Plot.XLabel("Dias");
            formsPlot4.Plot.YLabel("Total Ventas");

            formsPlot4.Plot.Add.Bars(cantidadesGananciasPorDia);

            formsPlot4.Plot.Axes.Bottom.SetTicks(posicionesDiasGanancias, diaDeLasGanancias); //agregamos al grafico 

            formsPlot4.Plot.Axes.AutoScale();
            formsPlot4.Refresh();
        }

        private void grafico5(int? año = null)
        {
            ////Quinto grafico/////
            ///Ventas por meses en un año especifico

            var ventasAñoX = _ventaServicio.ObtenerVentasPorMesYAño(0, 2026); //Filtro de año, pasamos 0 asi traemos el año completo

            if(año.HasValue)
            {
                int añoBusqueda = año.Value;

                ventasAñoX = _ventaServicio.ObtenerVentasPorMesYAño(0, añoBusqueda); //Filtro de año, pasamos 0 asi traemos el año completo

            }

            var fechasYVentasAgrupadasPorMeses = ventasAñoX.GroupBy(c => new { c.FechaVenta.Year, c.FechaVenta.Month })
            .Select(g => new
            {
                Fecha = new DateTime(g.Key.Year, g.Key.Month, 1),
                CantidadVentas = g.Count()
            }).ToList();

            string[] mesesVentas =
            {
                "Enero", "Febrero", "Marzo", "Abril",
                "Mayo", "Junio", "Julio", "Agosto",
                "Septiembre", "Octubre", "Noviembre", "Diciembre"
            };

            // X = índices (0..11)
            double[] xsVentas = fechasYVentasAgrupadasPorMeses
                .Select(x => x.Fecha.Month - 1)
                .Select(m => (double)m)
                .ToArray();

            double[] ventasPorMesEjeY = fechasYVentasAgrupadasPorMeses
                .Select(x => (double)x.CantidadVentas)
                .ToArray();

            string[] mesesVentasPresentesEjeX = fechasYVentasAgrupadasPorMeses
                .Select(x => mesesVentas[x.Fecha.Month - 1])
                .ToArray();


            formsPlot5.Plot.Clear();

            formsPlot5.Plot.Title("Ventas por mes");
            formsPlot5.Plot.XLabel("Meses");
            formsPlot5.Plot.YLabel("Total Ingresos");

            formsPlot5.Plot.Add.Bars(xsVentas, ventasPorMesEjeY);

            //Meses como labels del eje X
            formsPlot5.Plot.Axes.Bottom.SetTicks(xsVentas, mesesVentasPresentesEjeX);

            formsPlot5.Plot.Axes.AutoScale();
            formsPlot5.Refresh();
        }

        private void grafico6(int? mes = null ,int? año = null)
        {
            ////Sexto grafico////
            ///Ventas por dias en un mes y año especificos

            var ventasMesXAñoX = _ventaServicio.ObtenerVentasPorMesYAño(System.DateTime.Now.Month, System.DateTime.Now.Year); //traemos toda las ventas de un mes y año especificos

            if (año.HasValue && mes.HasValue)
            {
                int añoBusqueda = año.Value;
                int mesBusqueda = mes.Value;

                ventasMesXAñoX = _ventaServicio.ObtenerVentasPorMesYAño(mesBusqueda, añoBusqueda);
            }

            var ventarAgrupadasPorFecha = ventasMesXAñoX.Select(i => i.FechaVenta.Date) //agrupamos por fecha y contamos la cantidad de ventas de cada dia
                .GroupBy(fecha => fecha)
                .Select(g => new
                {
                    Fecha = g.Key,
                    CantidadVentas = g.Count()
                })
                .OrderBy(x => x.Fecha)
                .ToList();

            string[] diaDeLasVentas = ventarAgrupadasPorFecha //seleccionamos las fechas de las ventas
                .Select(x => x.Fecha.ToString("dd/MM"))
                .ToArray();

            double[] cantidadesVentasPorDia = ventarAgrupadasPorFecha.Select(x => (double)x.CantidadVentas).ToArray(); //seleccionamos la cantidad de ventas por dia (valor de las bars)

            double[] posicionesDiasVentas = Enumerable.Range(0, diaDeLasVentas.Length)
                                .Select(i => (double)i)
                                .ToArray();

            formsPlot6.Plot.Clear();

            formsPlot6.Plot.Title("Ventas por Dia en el ultimo Mes");
            formsPlot6.Plot.XLabel("Dias");
            formsPlot6.Plot.YLabel("Total Ventas");

            formsPlot6.Plot.Add.Bars(cantidadesVentasPorDia);

            formsPlot6.Plot.Axes.Bottom.SetTicks(posicionesDiasVentas, diaDeLasVentas); //agregamos al grafico 

            formsPlot6.Plot.Axes.AutoScale();
            formsPlot6.Refresh();
        }
    }
}
