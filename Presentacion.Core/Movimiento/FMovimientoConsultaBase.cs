using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.OpcionesPagos;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Movimiento;
using Servicios.LogicaNegocio.Movimiento.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TuProyecto.Presentacion;

namespace Presentacion.Core.Movimiento
{
    /// <summary>
    /// Base de las consultas de movimientos (libro de caja, cuentas corrientes, gastos).
    /// Resuelve grilla, colores, resumen al pie y "Ver detalle"; cada hija define sus filtros y qué consulta llama.
    /// Son solo lectura: el libro es inmutable.
    /// </summary>
    public abstract class FMovimientoConsultaBase : FBaseConsulta
    {
        protected readonly IMovimientoConsultaServicio _consultaServicio;

        private bool _tieneSaldo;

        protected FMovimientoConsultaBase(IMovimientoConsultaServicio consultaServicio)
        {
            _consultaServicio = consultaServicio;
        }

        // ---- A definir por cada consulta ---------------------------------------------------

        protected abstract ResultadoMovimientos Consultar(FiltroMovimientos filtro);

        /// <summary>Completa los filtros propios de la consulta (cajas, cuentas, origen...).</summary>
        protected abstract void AplicarFiltrosEspecificos(FiltroConsulta origen, FiltroMovimientos destino);

        protected abstract void ConfigurarFiltrosEspecificos();

        /// <summary>La columna "Caja" solo tiene sentido en el libro de caja.</summary>
        protected virtual bool MostrarColumnaCaja => false;

        protected virtual string TextoCheckHistorial => "Ver todo el historial";

        // ---- Filtros comunes ---------------------------------------------------------------

        protected override bool UsarCheck1 => true;

        protected override bool EsModoSoloLectura(FiltroConsulta filtro) => true;

        protected override string TextoLblBuscar => "Buscar por número:";

        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

            ActivarFiltroFechas("Filtrar por fecha");
            ActivarCheck(chkBool1, TextoCheckHistorial);

            ConfigurarFiltrosEspecificos();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = TextoTitular;
        }

        // ---- Opciones de combos reutilizables ----------------------------------------------

        protected static OpcionFiltro OpcionTodos(string texto = "Todos") =>
            new OpcionFiltro { Texto = texto, Valor = "" };

        protected static OpcionFiltro OpcionOperacion(Operacion op) =>
            new OpcionFiltro { Texto = OperacionDescripcion.De((int)op), Valor = "OP_" + (int)op };

        protected static List<OpcionFiltro> OpcionesMedioDePago(bool incluirCuentaCorriente)
        {
            var lista = new List<OpcionFiltro> { OpcionTodos() };

            foreach (TipoDePago tipo in Enum.GetValues(typeof(TipoDePago)))
            {
                if (tipo == TipoDePago.CtaCte && !incluirCuentaCorriente)
                    continue;

                lista.Add(new OpcionFiltro { Texto = tipo.ToString(), Valor = ((int)tipo).ToString() });
            }

            return lista;
        }

        protected static long? ComoLong(object? valor)
        {
            var texto = valor?.ToString();
            return long.TryParse(texto, out var n) ? n : null;
        }

        protected static int? ComoInt(object? valor)
        {
            var texto = valor?.ToString();
            return int.TryParse(texto, out var n) ? n : null;
        }

        /// <summary>Valores "OP_n" (operación) o "TM_n" (ingreso/egreso).</summary>
        protected static void AplicarTipoUOperacion(object? valor, FiltroMovimientos destino)
        {
            var texto = valor?.ToString();
            if (string.IsNullOrWhiteSpace(texto))
                return;

            if (texto.StartsWith("OP_") && int.TryParse(texto.AsSpan(3), out var op))
                destino.Operacion = op;
            else if (texto.StartsWith("TM_") && int.TryParse(texto.AsSpan(3), out var tm))
                destino.Tipo = tm;
        }

        // ---- Datos -------------------------------------------------------------------------

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            var filtro = new FiltroMovimientos
            {
                TextoBuscar = filtros.TextoBuscar,
                Desde = filtros.FechaDesde,
                Hasta = filtros.FechaHasta,
                SinLimiteFecha = filtros.Bool1,
                Page = filtros.Page,
                PageSize = filtros.PageSize
            };

            AplicarFiltrosEspecificos(filtros, filtro);

            var resultado = Consultar(filtro);

            _tieneSaldo = resultado.TieneSaldo;

            dgv.DataSource = resultado.Items;

            ResetearGrilla(dgv);

            ResumenPie =
                $"Ingresos {resultado.TotalIngresos:C2}  |  Egresos {resultado.TotalEgresos:C2}  |  Neto {resultado.Neto:C2}";

            ActualizarPaginacionUI(new DatosPaginacion
            {
                PaginaActual = resultado.Page,
                PageSize = resultado.PageSize,
                CantidadRegistros = resultado.TotalRegistros
            });
        }

        // ---- Acciones ----------------------------------------------------------------------

        protected override void ConfigurarAccionesPersonalizadas()
        {
            AgregarAccion(
                "Ver Detalle",
                Constantes.Imagenes.ImgMovimiento,
                id =>
                {
                    if (!id.HasValue)
                        return;

                    new FMovimientoDetallado(id.Value).ShowDialog();
                },
                true);
        }

        public override void EjecutarDobleClickFila(long? id)
        {
            if (id.HasValue)
                new FMovimientoDetallado(id.Value).ShowDialog();
        }

        // ---- Grilla ------------------------------------------------------------------------

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla); // oculta todo; abajo se muestra lo que corresponde

            grilla.ReadOnly = true;

            if (grilla.Columns.Count == 0)
                return;

            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (grilla.Columns.Contains("MovimientoId"))
                grilla.Columns["MovimientoId"].Name = "Id"; // FBaseConsulta toma la selección de la columna "Id"

            int orden = 0;

            Mostrar(grilla, "NumeroMovimiento", "Número", 230, 170, ref orden);
            Mostrar(grilla, "FechaMovimiento", "Fecha", 140, 130, ref orden, "dd/MM/yyyy HH:mm");
            Mostrar(grilla, "TipoMovimientoDescripcion", "Movimiento", 90, 85, ref orden);
            Mostrar(grilla, "OperacionDescripcion", "Operación", 140, 120, ref orden);
            Mostrar(grilla, "TipoPago", "Medio de pago", 110, 100, ref orden);
            Mostrar(grilla, "Referencia", "Referencia", 200, 150, ref orden);

            if (MostrarColumnaCaja)
                Mostrar(grilla, "CajaId", "Caja", 60, 60, ref orden);

            Mostrar(grilla, "MontoConSigno", "Monto", 120, 110, ref orden, "+#,##0.00;-#,##0.00;0.00", derecha: true);

            if (_tieneSaldo)
                Mostrar(grilla, "Saldo", "Saldo", 120, 110, ref orden, "C2", derecha: true);

            grilla.CellFormatting -= Grilla_CellFormatting;
            grilla.CellFormatting += Grilla_CellFormatting;
        }

        private static void Mostrar(
            DataGridView grilla, string nombre, string titulo, float peso, int minimo, ref int orden,
            string? formato = null, bool derecha = false)
        {
            if (!grilla.Columns.Contains(nombre))
                return;

            var col = grilla.Columns[nombre];

            col.Visible = true;
            col.HeaderText = titulo;
            col.FillWeight = peso;
            col.MinimumWidth = minimo;
            col.DisplayIndex = orden++;

            if (formato != null)
                col.DefaultCellStyle.Format = formato;

            if (derecha)
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void Grilla_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (sender is not DataGridView grilla || e.RowIndex < 0 || e.Value == null)
                return;

            if (grilla.Columns[e.ColumnIndex].Name != "MontoConSigno")
                return;

            if (e.Value is decimal monto)
            {
                e.CellStyle.ForeColor = monto >= 0 ? Color.SeaGreen : Color.Firebrick;
                e.CellStyle.Font = new Font(grilla.Font, FontStyle.Bold);
            }
        }
    }
}
