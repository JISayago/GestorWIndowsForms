using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Servicios.Helpers.Gasto;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Venta.VentaLibre;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Venta
{
    public partial class FVentaLibreConsulta : FBaseConsulta
    {
        private readonly IVentaLibreServicio _ventaLibreServicio;
        private long? ventaLibreSeleccionada;
        public FVentaLibreConsulta()
        {
            InitializeComponent();
            _ventaLibreServicio = new VentaLibreServicio();
        }

        protected override void ConfigurarAccionesPersonalizadas()
        {
            AgregarAccion(
                "Anular",
                Constantes.Imagenes.ImgEliminar,
                AnularVentaLibre,
                true
            );

        }

        private void AnularVentaLibre(long? id)
        {
            if (!id.HasValue)
            {
                MessageBox.Show("Seleccione una venta.");
                return;
            }

            var resultado = _ventaLibreServicio.AnularVentaLibre(id.Value);

            if (resultado.Exitoso)
            {
                Recargar();
                MessageBox.Show(resultado.Mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Recargar();
                MessageBox.Show(resultado.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void VerDetalleVentaLibre(long? id)
        //{
        //    if (!id.HasValue)
        //    {
        //        MessageBox.Show("Seleccione una venta.");
        //        return;
        //    }

        //    var f = new FVentaLibreDetalle(id.Value);
        //    f.ShowDialog();
        //}

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (!grilla.Columns.Contains("VentaLibreId")) return;
            grilla.Columns["VentaLibreId"].Visible = false;
            grilla.Columns["VentaLibreId"].Name = "Id";

            grilla.Columns["NumeroVenta"].Visible = true;
            grilla.Columns["NumeroVenta"].HeaderText = "N° Venta";
            grilla.Columns["NumeroVenta"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grilla.Columns["FechaVenta"].Visible = true;
            grilla.Columns["FechaVenta"].HeaderText = "Fecha";
            grilla.Columns["FechaVenta"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grilla.Columns["ClienteNombreCompleto"].Visible = true;
            grilla.Columns["ClienteNombreCompleto"].HeaderText = "Cliente";
            grilla.Columns["ClienteNombreCompleto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grilla.Columns["VendedorNombreCompleto"].Visible = true;
            grilla.Columns["VendedorNombreCompleto"].HeaderText = "Vendedor";
            grilla.Columns["VendedorNombreCompleto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grilla.Columns["Total"].Visible = true;
            grilla.Columns["Total"].HeaderText = "Total";
            grilla.Columns["Total"].DefaultCellStyle.Format = "C2";

            grilla.Columns["MontoPagado"].Visible = true;
            grilla.Columns["MontoPagado"].HeaderText = "Pagado";
            grilla.Columns["MontoPagado"].DefaultCellStyle.Format = "C2";

            grilla.Columns["MontoAdeudado"].Visible = true;
            grilla.Columns["MontoAdeudado"].HeaderText = "Adeudado";
            grilla.Columns["MontoAdeudado"].DefaultCellStyle.Format = "C2";

            grilla.Columns["Estado"].Visible = true;
            grilla.Columns["Estado"].HeaderText = "Estado";
            grilla.Columns["Estado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grilla.Columns["Detalle"].Visible = true;
            grilla.Columns["Detalle"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            // 🔹 llamada unificada
            var resultado = _ventaLibreServicio.ObtenerVentasLibres(filtros);

            // 🔹 bind
            dgv.DataSource = resultado.Items;

            // 🔴 clave: reaplicar formato
            ResetearGrilla(dgv);

            // 🔹 paginación
            var paginacion = new DatosPaginacion
            {
                PaginaActual = resultado.Page,
                PageSize = resultado.PageSize,
                CantidadRegistros = resultado.TotalRegistros,
            };

            ActualizarPaginacionUI(paginacion);

            // 🔹 estado botones
            BarraLateralBotones.Enabled = !filtros.VerEliminados;
        }

        //public override void EjecutarBtnNuevo()
        //{
        //    var f = new FVentaLibre(_logeadoId);
        //    f.ShowDialog();

        //    if (f.RealizoAlgunaOperacion)
        //        Recargar();
        //}

        private void Recargar()
        {
            var filtros = new FiltroConsulta();
            ActualizarDatos(dgvGrilla, filtros);
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (!entidadID.HasValue)
            {
                MessageBox.Show("Seleccione una venta.");
                return;
            }

            ventaLibreSeleccionada = entidadID;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FVentaLibreConsulta_Load(object sender, EventArgs e)
        {
        }
        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

            // 🔵 combo estado
    //        var opciones = new List<OpcionFiltro>
    //{
    //    new OpcionFiltro { Texto = "Todos", Valor = 0 }
    //};

    //        foreach (EstadoGasto estado in Enum.GetValues(typeof(EstadoGasto)))
    //        {
    //            opciones.Add(new OpcionFiltro
    //            {
    //                Texto = estado.ToString(),
    //                Valor = (int)estado
    //            });
    //        }

            //ActivarFiltroCombo(opciones, "Texto", "Valor");

            // 👉 default
            cbxFiltroOpcional.SelectedValue = 0;

            // 🔵 fechas
            ActivarFiltroFechas("Filtrar por fecha");
        }
    }
}
