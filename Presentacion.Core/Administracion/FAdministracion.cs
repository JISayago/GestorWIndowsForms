using MigraDoc.DocumentObjectModel.Internals;
using Presentacion.Core.Articulo.Marca;
using Presentacion.Core.Categoria;
using Presentacion.Core.Cliente;
using Presentacion.Core.CuentaCorriente;
using Presentacion.Core.Empleado;
using Presentacion.Core.Empleado.Rol;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Administracion
{
    public partial class FAdministracion : Form
    {
        private readonly CajaServicio _cajaSerivicio;
        private readonly VentaServicio _ventaServicio;
        List<CajaDTO> todasLasCajas;

        public FAdministracion()
        {
            InitializeComponent();

            _cajaSerivicio = new CajaServicio();
            _ventaServicio = new VentaServicio();
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
            //CONSULTA GASTO

        }

        private void btnComprobantes_Click(object sender, EventArgs e)
        {
            // PODER ABRIR UNA CARPETA TAL VEZ CON COMPROBANTES GUARDADOS
        }

        private void formsPlot1_Load(object sender, EventArgs e)
        {

        }

        private void FAdministracion_Load(object sender, EventArgs e)
        {
            //Generar X cantidad de numeros random
            int cuantosRandom = 15;
            Random random = new Random();

            double[] numerosR = Enumerable
                .Range(0, cuantosRandom)
                .Select(i => random.NextDouble()) // 0.0 a 1.0
                .ToArray();

            //Generar X numeros consecutivos
            int cuantosConsecutivos = 14;

            double[] numerosConsecutivos = Enumerable
                .Range(0, cuantosConsecutivos)
                .Select(i => (double)i)
                .ToArray();

            //Generar X fechas consecutivas
            DateTime[] fechas = Generate.ConsecutiveDays(start: DateTime.Now.Date.AddDays(-14), count: 14);

            string[] diasDelMes = fechas.Select(d => d.ToString("MMM dd")).ToArray();

            //LO DE ARRIBA SE BORRA, SOLO ES PARA PROBAR SCOTT PLOT




            //Todas la cajas abiertas y cerradas
            var todasLasCajas = _cajaSerivicio.ObetenerTodasLasCajas();

            //Ventas
            var ventas = _ventaServicio.ObtenerTodasLasVentas();


            ////////////////////

            //Agarrar las cajas abiertas y cerradas en los ultimos 30 dias (seria mas facil las 30 ultimas cajas x las fecha pueden ser direntes entre caja (duracion))
            //Tomar para el eje Y las ganancias diarias (o balance final) de cada caja
            //El eje x serian las cajas en orden cronologico (1,2,3,4...) o por fecha (dia mes)

            // Si quiero por fecha, deberia agrupar las cajas por fecha y sumar los balances finales de las cajas abiertas y cerradas en ese dia

            //creo que caja no esta calculando el balance final 

            //Obtener las ultimas 30 cajas abiertas o cerradas ordenadas por fecha de inicio




                        ////Primer grafico////

            //Ganacias por cajas (ultimas 30 cajas) 
            //No me gusta mucho las fechas en el eje x, mejor numeros de caja?, por casos de que abran varias cajas al dia

            var ultimasCajas = todasLasCajas
                .TakeLast(30)
                .OrderBy(c => c.FechaInicio)
                .ToList();                  //funciona en service para traer las ultimas X cajas

            //Deberiamos mostrar el balance final? y no solo el ingreso?
            double[] gananciasPorCaja = ultimasCajas
                .Select(c => (double)c.TotalIngresos)
                .ToArray();

            string[] fechasDeCadaCaja = ultimasCajas
            .Select(c => $"A: {c.FechaInicio:dd/MM}\nC: {c.FechaFin?.ToString("dd/MM") ?? "Abierta"}")
            .ToArray();

            double[] numerosCajas = Enumerable
                .Range(1, ultimasCajas.Count)
                .Select(i => (double)i)
                .ToArray();

            formsPlot1.Plot.Title("Caja Ultimo Mes");
            formsPlot1.Plot.XLabel("Fecha de las Cajas");
            formsPlot1.Plot.YLabel("Total Ingresos");


            formsPlot1.Plot.Add.Scatter(numerosCajas, gananciasPorCaja); //Eje x e y

            formsPlot1.Plot.Axes.Bottom.SetTicks(numerosCajas, fechasDeCadaCaja); //Etiquetas eje x

            formsPlot1.Refresh();



                        ////Primer grafico v2////

            //Ganacias por cajas agrupadas por dias (ultimas 30 dias) 
            //Agrupar las cajas por dia y sumar los ingresos de cada dia
            //Creo que este deberia remplazar al primero, preg a moncho

            var cajasPorDia = ultimasCajas
            .GroupBy(c => c.FechaInicio.Date)
            .OrderBy(g => g.Key)
            .Take(30)
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

            formsPlot2.Plot.Title("Cajas agrupadas por dias");
            formsPlot2.Plot.XLabel("Fecha de las Cajas");
            formsPlot2.Plot.YLabel("Total Ingresos");

            formsPlot2.Plot.Add.Scatter(numerosDias, ingresosPorDia);

            formsPlot2.Plot.Axes.Bottom.SetTicks(numerosDias, dias); //Etiquetas eje x

            formsPlot2.Refresh();



                        ////Segundo grafico/////

            string[] meses =
            {
                "Enero", "Febrero", "Marzo", "Abril",
                "Mayo", "Junio", "Julio", "Agosto",
                "Septiembre", "Octubre", "Noviembre", "Diciembre"
            };

            // X = índices (0..11)
            double[] xs = Enumerable.Range(0, meses.Length)
                                    .Select(i => (double)i)
                                    .ToArray();

            var fechasGananciasPorMes = todasLasCajas.GroupBy(c => new { c.FechaInicio.Year, c.FechaInicio.Month })
            .Select(g => new
            {
                Fecha = new DateTime(g.Key.Year, g.Key.Month, 1),
                Balance = g.Sum(c => c.TotalIngresos)//deberia ser balance final?
            })
            .OrderByDescending(x => x.Fecha)
            .Take(12)
            .OrderBy(x => x.Fecha)
            .ToList();

            double[] ganaciasPorMes = fechasGananciasPorMes
                .Select(x => (double)x.Balance)
                .ToArray();


            formsPlot3.Plot.Clear();

            formsPlot3.Plot.Title("Ganacias por mes");
            formsPlot3.Plot.XLabel("Meses");
            formsPlot3.Plot.YLabel("Total Ingresos");

            formsPlot3.Plot.Add.Scatter(xs, ganaciasPorMes);

            //Meses como labels del eje X
            formsPlot3.Plot.Axes.Bottom.SetTicks(xs, meses);

            //Rotar etiquetas
            formsPlot3.Plot.Axes.Bottom.TickLabelStyle.Rotation = 45;
            
            formsPlot3.Refresh();



                        ////Tercer grafico////

            //Ventas por dia en el ultimo mes
            //Agarrar las ventas de los ultimos 31 dias, juntar por dia y contar la cantidad de ventas por dia

            var fechaHaceUnMes = DateTime.Now.Date.AddDays(-30);

            var ventasUltimoMes = ventas
                .Where(v => v.FechaVenta.Date >= fechaHaceUnMes)
                .ToList();

            var ventasPorDia = ventasUltimoMes.Select( i => i.FechaVenta.Date)
                .GroupBy(fecha => fecha)
                .Select(g => new
                {
                    Fecha = g.Key,
                    CantidadVentas = g.Count()
                })
                .OrderBy(x => x.Fecha)
                .ToList();

            string[] diaDeLasVentas = ventasPorDia
                .Select(x => x.Fecha.ToString("dd/MM"))
                .ToArray();

            double[] cantidadesVentasPorDia = ventasPorDia.Select(x => (double)x.CantidadVentas).ToArray();

            double[] posiciones = Enumerable.Range(0, diaDeLasVentas.Length) //para meses pero deberia mostar por dia las ventas
                                .Select(i => (double)i)
                                .ToArray();

            formsPlot4.Plot.Clear();

            formsPlot4.Plot.Title("Ventas por dia (Ultimo Mes)");
            formsPlot4.Plot.XLabel("Dias");
            formsPlot4.Plot.YLabel("Total Ventas");

            formsPlot4.Plot.Add.Bars(cantidadesVentasPorDia);

            formsPlot4.Plot.Axes.Bottom.SetTicks(posiciones, diaDeLasVentas);

            formsPlot4.Refresh();


            ////Cuarto grafico////

            //Ingresos, egresos
            //La idea que sea los dos graficos en un mismo formplot, barras lado a lado puede ser una 
            //o directamente con scatter



        }
    }
}
