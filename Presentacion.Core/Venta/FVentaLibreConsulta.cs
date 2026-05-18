using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.Helpers.VentaEnum;
using Servicios.LogicaNegocio.Venta.VentaLibre;
using System;
using System.Collections.Generic;
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

        #region 🔷 FILTROS
        protected override string TextoLblBuscar
      => "Buscar Venta:";

        protected override string TextoLblCbx1
            => "Filtrar por Propiedad";

        protected override string TextoLblCbx2
            => "Filtrar por Estado";

        protected override string TextoLblCbx3
            => "Filtrar por Fecha";
        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();
            ActivarCheck(chkBool1, "Mostrar ventas canceladas");
            ActivarCheck(chkBool2, "Mostrar todas las Ventas Libres (histórico)");
            var opcionesBusqueda = new List<OpcionFiltro>
            {
                new OpcionFiltro
                {
                    Texto = "Todos",
                    Valor = ""
                },
                new OpcionFiltro
                {
                    Texto = "Número Venta",
                    Valor = "NumeroVenta"
                },
                new OpcionFiltro
                {
                    Texto = "Cliente",
                    Valor = "ClienteNombreCompleto"
                }
            };

            ActivarCombo(
                cbx1,
                lblcbx1,
                opcionesBusqueda,
                "Texto",
                "Valor",
                "Buscar por"
            );

            ActivarFiltroFechas("Usar filtro por fecha");

            var filtrosEstado = new List<OpcionFiltro>
            {
                new OpcionFiltro
                {
                    Texto = "Todos",
                    Valor = ""
                },
             
                new OpcionFiltro
                {
                    Texto = "Confirmada",
                    Valor = ((int)EstadoVenta.Confirmada).ToString()
                },
                new OpcionFiltro
                {
                    Texto = "Venta Cancelada",
                    Valor = ((int)EstadoVenta.Cancelada).ToString()
                },
                new OpcionFiltro
                {
                    Texto = "Cancelación Venta",
                    Valor = ((int)EstadoVenta.CancelacionVenta).ToString()
                }
            };
            ActivarCombo(
                cbx2,
                lblcbx2,
                filtrosEstado,
                "Texto",
                "Valor",
                "Filtrar por"
            );
            var filtrosFecha = new List<OpcionFiltro>
            {
                new OpcionFiltro
                {
                    Texto = "Fecha Venta",
                    Valor = "FVL"
                },
            };

            ActivarCombo(
                cbx3,
                lblcbx3,
                filtrosFecha,
                "Texto",
                "Valor",
                "Filtrar por"
            );
            cbx1.SelectedValue = "";
            cbx2.SelectedValue = "";
            cbx3.SelectedValue = "FVL";
        }
        protected override void AccionCheck2()
        {
            if (chkBool2.Checked)
            {
                _actualizandoFiltros = true;

                chkBool1.Checked = false;

                _actualizandoFiltros = false;

                LimpiarFiltrosEspeciales();
            }

        }
        protected override void AccionCheck1()
        {
            if (chkBool1.Checked)
            {
                _actualizandoFiltros = true;

                chkBool2.Checked = false;

                _actualizandoFiltros = false;

                LimpiarFiltrosEspeciales();
            }

        }

        private void LimpiarFiltrosEspeciales()
        {
            _actualizandoFiltros = true;

            txtBuscar.Clear();

            if (cbx1.Enabled)
                cbx1.SelectedIndex = 0;

            if (cbx2.Enabled)
                cbx2.SelectedIndex = 0;

            if (cbx3.Enabled)
                cbx3.SelectedIndex = 0;

            chkUsarFecha.Checked = false;

            _actualizandoFiltros = false;
        }
        #endregion

        #region 🔷 ACCIONES PERSONALIZADAS

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

            var resultado =
                _ventaLibreServicio.AnularVentaLibre(id.Value);

            Recargar();

            if (resultado.Exitoso)
            {
                MessageBox.Show(
                    resultado.Mensaje,
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    resultado.Mensaje,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 🔷 GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Contains("VentaLibreId"))
            {
                grilla.Columns["VentaLibreId"].Visible = false;
                grilla.Columns["VentaLibreId"].Name = "Id";
            }

            if (grilla.Columns.Contains("NumeroVenta"))
            {
                grilla.Columns["NumeroVenta"].Visible = true;
                grilla.Columns["NumeroVenta"].HeaderText = "N° Venta";
                grilla.Columns["NumeroVenta"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
            }

            if (grilla.Columns.Contains("FechaVenta"))
            {
                grilla.Columns["FechaVenta"].Visible = true;
                grilla.Columns["FechaVenta"].HeaderText = "Fecha";
                grilla.Columns["FechaVenta"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
            }

            if (grilla.Columns.Contains("ClienteNombreCompleto"))
            {
                grilla.Columns["ClienteNombreCompleto"].Visible = true;
                grilla.Columns["ClienteNombreCompleto"].HeaderText =
                    "Cliente";

                grilla.Columns["ClienteNombreCompleto"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
            }

            if (grilla.Columns.Contains("VendedorNombreCompleto"))
            {
                grilla.Columns["VendedorNombreCompleto"].Visible = true;

                grilla.Columns["VendedorNombreCompleto"].HeaderText =
                    "Vendedor";

                grilla.Columns["VendedorNombreCompleto"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
            }

            if (grilla.Columns.Contains("Total"))
            {
                grilla.Columns["Total"].Visible = true;
                grilla.Columns["Total"].HeaderText = "Total";
                grilla.Columns["Total"].DefaultCellStyle.Format = "C2";
            }

            if (grilla.Columns.Contains("MontoPagado"))
            {
                grilla.Columns["MontoPagado"].Visible = true;
                grilla.Columns["MontoPagado"].HeaderText = "Pagado";
                grilla.Columns["MontoPagado"].DefaultCellStyle.Format = "C2";
            }

            if (grilla.Columns.Contains("MontoAdeudado"))
            {
                grilla.Columns["MontoAdeudado"].Visible = true;

                grilla.Columns["MontoAdeudado"].HeaderText = "Adeudado";

                grilla.Columns["MontoAdeudado"].DefaultCellStyle.Format =
                    "C2";
            }

            if (grilla.Columns.Contains("Estado"))
            {
                grilla.Columns["Estado"].Visible = true;
                grilla.Columns["Estado"].HeaderText = "Estado";

                grilla.Columns["Estado"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
            }

            if (grilla.Columns.Contains("Detalle"))
            {
                grilla.Columns["Detalle"].Visible = true;

                grilla.Columns["Detalle"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        #endregion

        #region 🔥 DATOS

        public override void ActualizarDatos(
            DataGridView dgv,
            FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            var resultado =
                _ventaLibreServicio.ObtenerVentasLibres(filtros);

            dgv.DataSource = resultado.Items;

            ResetearGrilla(dgv);

            var paginacion = new DatosPaginacion
            {
                PaginaActual = resultado.Page,
                PageSize = resultado.PageSize,
                CantidadRegistros = resultado.TotalRegistros
            };

            ActualizarPaginacionUI(paginacion);
        }

        #endregion

        #region 🔷 AUXILIARES

        private void Recargar()
        {
            RefrescarGrilla();
        }

        #endregion

        #region 🔷 EVENTOS

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

        #endregion
    }
}