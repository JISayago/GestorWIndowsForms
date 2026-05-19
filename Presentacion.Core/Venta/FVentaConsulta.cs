using AccesoDatos.Entidades;
using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.Helpers.VentaEnum;
using Servicios.LogicaNegocio.Venta;
using Servicios.LogicaNegocio.Venta.VentaLibre;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Venta
{
    public partial class FVentaConsulta : FBaseConsulta
    {
        private readonly IVentaServicio _ventaServicio;
        private long _usuarioLogeadoID;
        private long? ventaLibreSeleccionada;
        public FVentaConsulta()
        {
            InitializeComponent();
            _ventaServicio = new VentaServicio();
        }
        public FVentaConsulta(long usuarioLogeadoID) : this()
        {
            _usuarioLogeadoID = usuarioLogeadoID;
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
            ActivarCheck(chkBool2, "Mostrar todas las Ventas (histórico)");
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
                    Valor = "FV"
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
            cbx3.SelectedValue = "FV";
        }
        protected override void AccionCheck2()
        {
            if (chkBool2.Checked)
            {
                _actualizandoFiltros = true;

                chkBool1.Checked = false;

                _actualizandoFiltros = false;

                LimpiarFiltrosEspeciales();
                paginaActual = 1;
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
                paginaActual = 1;
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
                _ventaServicio.ObtenerVentasPorIds(new List<long> { id.Value });
            if (resultado[0].Estado == (int)EstadoVenta.CancelacionVenta || resultado[0].Estado == (int)EstadoVenta.Cancelada)
            {
                MessageBox.Show("Por favor selecione una venta que no haya sido cancelada.");
                return;

            }
            var fVenta = new FVenta(_usuarioLogeadoID, id.Value);
            fVenta.Show();
            Close();

            //Recargar();

            //if (resultado.Exitoso)
            //{
            //    MessageBox.Show(
            //        resultado.Mensaje,
            //        "Éxito",
            //        MessageBoxButtons.OK,
            //        MessageBoxIcon.Information);
            //}
            //else
            //{
            //    MessageBox.Show(
            //        resultado.Mensaje,
            //        "Error",
            //        MessageBoxButtons.OK,
            //        MessageBoxIcon.Error);
            //}
        }

        #endregion

        #region 🔷 GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Contains("VentaId"))
            {
                grilla.Columns["VentaId"].Visible = false;
                grilla.Columns["VentaId"].Name = "Id";
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
                grilla.Columns["FechaVenta"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                grilla.Columns["FechaVenta"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
            }

            if (grilla.Columns.Contains("Total"))
            {
                grilla.Columns["Total"].Visible = true;
                grilla.Columns["Total"].HeaderText = "Total";
                grilla.Columns["Total"].DefaultCellStyle.Format = "C2";
            }

            if (grilla.Columns.Contains("TotalSinDescuento"))
            {
                grilla.Columns["TotalSinDescuento"].Visible = true;
                grilla.Columns["TotalSinDescuento"].HeaderText = "Total s/Desc";
                grilla.Columns["TotalSinDescuento"].DefaultCellStyle.Format = "C2";
            }

            if (grilla.Columns.Contains("Descuento"))
            {
                grilla.Columns["Descuento"].Visible = true;
                grilla.Columns["Descuento"].HeaderText = "Descuento";
                grilla.Columns["Descuento"].DefaultCellStyle.Format = "C2";
            }

            // 🔥 IMPORTANTE: mostrar descripción, no el int
            if (grilla.Columns.Contains("EstadoDescripcion"))
            {
                grilla.Columns["EstadoDescripcion"].Visible = true;
                grilla.Columns["EstadoDescripcion"].HeaderText = "Estado";
                grilla.Columns["EstadoDescripcion"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
            }

            // ocultás el campo crudo
            if (grilla.Columns.Contains("Estado"))
            {
                grilla.Columns["Estado"].Visible = false;
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
                _ventaServicio.ObtenerVentas(filtros);

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

    }
}
