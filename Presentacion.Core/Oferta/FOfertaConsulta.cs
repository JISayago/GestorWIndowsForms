using Presentacion.Core.Empleado;
using Presentacion.Core.Empleado.Rol;
using Presentacion.Core.Producto;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Venta.Oferta;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Oferta
{
    public partial class FOfertaConsulta : FBaseConsulta
    {
        private readonly IOfertaServicio _ofertaServicio;
        public long? ofertaSeleccionada = null;
        private bool _vieneDeVenta = false;
        private bool estaActiva = true;
        private bool activarDesactivar = false;
        public FOfertaConsulta()
        {
            InitializeComponent();
            _ofertaServicio = new OfertaServicio();
        }
        public FOfertaConsulta(bool vieneDeVenta) : this()
        {
            _vieneDeVenta = vieneDeVenta;
            MessageBox.Show("Seleccione la oferta que desea aplicar a la venta.");
        }
        public FOfertaConsulta(bool ActivarDesactivar, string a) : this()
        {
            activarDesactivar = ActivarDesactivar;
            if (activarDesactivar)
            {
                btnSeleccionarParaVenta.Enabled = false;
                btnActivarDesactivarOferta.Visible = true;
                btnActivarDesactivarOferta.Enabled = true;
            }
        }

        public override void EjecutarBtnNuevo()
        {
            var FOferta = new FSeleccionTipoOferta();
            FOferta.Show();
        }

        /*  public override void ResetearGrilla(DataGridView grilla)
          {
              base.ResetearGrilla(grilla);

              // Oculto el ID interno
              // Id oculto
              grilla.Columns["OfertaDescuentoId"].Visible = false;
              grilla.Columns["OfertaDescuentoId"].Name = "Id";

              // Descripción: ocupa todo el espacio libre
              grilla.Columns["Descripcion"].Visible = true;
              grilla.Columns["Descripcion"].HeaderText = "Descripción";
              grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
              grilla.Columns["Descripcion"].FillWeight = 60; // ocupa 60% del espacio flexible


              // Código y Grupo: anchos grandes fijos
              grilla.Columns["Codigo"].Visible = true;
              grilla.Columns["Codigo"].HeaderText = "Código";
              grilla.Columns["Codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
              grilla.Columns["Codigo"].FillWeight = 30; // ocupa 20%

              grilla.Columns["GrupoNombre"].Visible = true;
              grilla.Columns["GrupoNombre"].HeaderText = "Grupo";
              grilla.Columns["GrupoNombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
              grilla.Columns["GrupoNombre"].FillWeight = 30; // ocupa 20%
              // Detalle
              grilla.Columns["Detalle"].Visible = true;
              grilla.Columns["Detalle"].HeaderText = "Detalle";

              // Resto de columnas
              grilla.Columns["PrecioOriginal"].Visible = true;
              grilla.Columns["PrecioOriginal"].HeaderText = "Precio Original";
              grilla.Columns["PrecioOriginal"].DefaultCellStyle.Format = "C2";

              grilla.Columns["PrecioFinal"].Visible = true;
              grilla.Columns["PrecioFinal"].HeaderText = "Precio Final";
              grilla.Columns["PrecioFinal"].DefaultCellStyle.Format = "C2";

              grilla.Columns["DescuentoTotalFinal"].Visible = true;
              grilla.Columns["DescuentoTotalFinal"].HeaderText = "Descuento $";
              grilla.Columns["DescuentoTotalFinal"].DefaultCellStyle.Format = "C2";

              grilla.Columns["PorcentajeDescuento"].Visible = true;
              grilla.Columns["PorcentajeDescuento"].HeaderText = "% Desc.";
              grilla.Columns["PorcentajeDescuento"].DefaultCellStyle.Format = "N2";

              grilla.Columns["FechaInicio"].Visible = true;
              grilla.Columns["FechaInicio"].HeaderText = "Inicio";
              grilla.Columns["FechaInicio"].DefaultCellStyle.Format = "dd/MM/yyyy";

              grilla.Columns["FechaFin"].Visible = true;
              grilla.Columns["FechaFin"].HeaderText = "Fin";
              grilla.Columns["FechaFin"].DefaultCellStyle.Format = "dd/MM/yyyy";

              grilla.Columns["CantidadProductosDentroOferta"].Visible = true;
              grilla.Columns["CantidadProductosDentroOferta"].HeaderText = "Cant. Productos";

              grilla.Columns["CantidadLimiteDeStock"].Visible = true;
              grilla.Columns["CantidadLimiteDeStock"].HeaderText = "Cant. Máx.";
              grilla.Columns["EstaActiva"].Visible = true;

              // Columnas no necesarias a la vista
              grilla.Columns["EsUnSoloProducto"].Visible = false;
              grilla.Columns["esOfertaPorGrupo"].Visible = false;
              grilla.Columns["TieneLimiteDeStock"].Visible = false;

              grilla.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

              ToolTip tip = new ToolTip
              {
                  BackColor = Color.Yellow,
                  ForeColor = Color.Black
              };

              grilla.CellMouseEnter += (s, e) =>
              {
                  if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                  {
                      var cell = grilla[e.ColumnIndex, e.RowIndex];
                      if (cell.Value != null)
                      {
                          string texto = cell.Value.ToString();
                          var size = TextRenderer.MeasureText(texto, grilla.Font);
                          if (size.Width > cell.Size.Width)
                              tip.Show(texto, grilla, grilla.PointToClient(Cursor.Position).X + 10, grilla.PointToClient(Cursor.Position).Y + 10, 3000);
                          else
                              tip.Hide(grilla);
                      }
                  }
              };

          }*/
        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            // Oculto el ID interno
            if (grilla.Columns.Contains("OfertaDescuentoId"))
            {
                grilla.Columns["OfertaDescuentoId"].Visible = false;
                grilla.Columns["OfertaDescuentoId"].Name = "Id";
            }

            // Descripción: ocupa todo el espacio libre
            if (grilla.Columns.Contains("Descripcion"))
            {
                grilla.Columns["Descripcion"].Visible = true;
                grilla.Columns["Descripcion"].HeaderText = "Descripción";
                grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grilla.Columns["Descripcion"].FillWeight = 60;
            }

            // Código y Grupo
            if (grilla.Columns.Contains("Codigo"))
            {
                grilla.Columns["Codigo"].Visible = true;
                grilla.Columns["Codigo"].HeaderText = "Código";
                grilla.Columns["Codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grilla.Columns["Codigo"].FillWeight = 30;
            }

            if (grilla.Columns.Contains("GrupoNombre"))
            {
                grilla.Columns["GrupoNombre"].Visible = true;
                grilla.Columns["GrupoNombre"].HeaderText = "Grupo";
                grilla.Columns["GrupoNombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grilla.Columns["GrupoNombre"].FillWeight = 30;
            }

            // Detalle
            if (grilla.Columns.Contains("Detalle"))
            {
                grilla.Columns["Detalle"].Visible = true;
                grilla.Columns["Detalle"].HeaderText = "Detalle";
            }

            // Resto de columnas
            if (grilla.Columns.Contains("PrecioOriginal"))
            {
                grilla.Columns["PrecioOriginal"].Visible = true;
                grilla.Columns["PrecioOriginal"].HeaderText = "Precio Original";
                grilla.Columns["PrecioOriginal"].DefaultCellStyle.Format = "C2";
            }

            if (grilla.Columns.Contains("PrecioFinal"))
            {
                grilla.Columns["PrecioFinal"].Visible = true;
                grilla.Columns["PrecioFinal"].HeaderText = "Precio Final";
                grilla.Columns["PrecioFinal"].DefaultCellStyle.Format = "C2";
            }

            if (grilla.Columns.Contains("DescuentoTotalFinal"))
            {
                grilla.Columns["DescuentoTotalFinal"].Visible = true;
                grilla.Columns["DescuentoTotalFinal"].HeaderText = "Descuento $";
                grilla.Columns["DescuentoTotalFinal"].DefaultCellStyle.Format = "C2";
            }

            if (grilla.Columns.Contains("PorcentajeDescuento"))
            {
                grilla.Columns["PorcentajeDescuento"].Visible = true;
                grilla.Columns["PorcentajeDescuento"].HeaderText = "% Desc.";
                grilla.Columns["PorcentajeDescuento"].DefaultCellStyle.Format = "N2";
            }

            if (grilla.Columns.Contains("FechaInicio"))
            {
                grilla.Columns["FechaInicio"].Visible = true;
                grilla.Columns["FechaInicio"].HeaderText = "Inicio";
                grilla.Columns["FechaInicio"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (grilla.Columns.Contains("FechaFin"))
            {
                grilla.Columns["FechaFin"].Visible = true;
                grilla.Columns["FechaFin"].HeaderText = "Fin";
                grilla.Columns["FechaFin"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (grilla.Columns.Contains("CantidadProductosDentroOferta"))
            {
                grilla.Columns["CantidadProductosDentroOferta"].Visible = true;
                grilla.Columns["CantidadProductosDentroOferta"].HeaderText = "Cant. Productos";
            }

            if (grilla.Columns.Contains("CantidadLimiteDeStock"))
            {
                grilla.Columns["CantidadLimiteDeStock"].Visible = true;
                grilla.Columns["CantidadLimiteDeStock"].HeaderText = "Cant. Máx.";
            }

            // Reemplazar la columna booleana por una columna de texto que muestre "Esta Activa" / "Inactiva"
            if (grilla.Columns.Contains("EstaActiva"))
            {
                var existing = grilla.Columns["EstaActiva"];
                if (!(existing is DataGridViewTextBoxColumn))
                {
                    int idx = existing.Index;
                    grilla.Columns.Remove("EstaActiva");

                    var colEstado = new DataGridViewTextBoxColumn
                    {
                        Name = "EstaActiva",
                        HeaderText = "Estado",
                        DataPropertyName = "EstaActiva",
                        ReadOnly = true,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                        Tag = "EstadoFormat" // marcador para saber que ya le pusimos formateo
                    };

                    grilla.Columns.Insert(Math.Min(idx, grilla.Columns.Count), colEstado);
                }
                else
                {
                    // si ya existe como TextBoxColumn, aseguramos encabezado/auto size
                    existing.HeaderText = "Estado";
                    existing.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    if (existing.Tag == null) existing.Tag = "EstadoFormat";
                }
            }

            // Columnas no necesarias a la vista
            if (grilla.Columns.Contains("EsUnSoloProducto")) grilla.Columns["EsUnSoloProducto"].Visible = false;
            if (grilla.Columns.Contains("esOfertaPorGrupo")) grilla.Columns["esOfertaPorGrupo"].Visible = false;
            if (grilla.Columns.Contains("TieneLimiteDeStock")) grilla.Columns["TieneLimiteDeStock"].Visible = false;

            grilla.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            // Tooltip: mostrar texto completo si no cabe
            ToolTip tip = new ToolTip { BackColor = Color.Yellow, ForeColor = Color.Black };


            // Attach CellFormatting sólo si no lo hemos hecho antes (usando Tag en la columna)
            if (grilla.Columns.Contains("EstaActiva"))
            {
                var col = grilla.Columns["EstaActiva"];
                if (col.Tag != null && col.Tag.ToString() == "EstadoFormatWithHandler")
                {
                    // ya agregado
                }
                else
                {
                    col.Tag = "EstadoFormatWithHandler";
                    grilla.CellFormatting += (s, e) =>
                    {
                        if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                        if (e.ColumnIndex != grilla.Columns["EstaActiva"].Index) return;

                        // Convertir bool -> texto
                        if (e.Value is bool b)
                        {
                            e.Value = b ? "Esta Activa" : "Inactiva";
                            e.FormattingApplied = true;
                        }
                        else if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
                        {
                            e.Value = "Inactiva";
                            e.FormattingApplied = true;
                        }
                    };
                }
            }
        }

        // Método separado para el tooltip (evita añadir múltiples lambdas anónimos)
        private void Grilla_CellMouseEnter_ShowTooltip(object sender, DataGridViewCellEventArgs e)
        {
            if (!(sender is DataGridView grilla) || e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var cell = grilla[e.ColumnIndex, e.RowIndex];
            if (cell?.Value == null) return;

            string texto = cell.Value.ToString();
            var size = TextRenderer.MeasureText(texto, grilla.Font);
            if (size.Width > cell.Size.Width)
            {
                var tip = new ToolTip { BackColor = Color.Yellow, ForeColor = Color.Black };
                var p = Cursor.Position;
                var clientPoint = grilla.PointToClient(p);
                tip.Show(texto, grilla, clientPoint.X + 10, clientPoint.Y + 10, 3000);
            }
            else
            {
                // no mostrar
            }
        }




        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        {
            base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

            if (_vieneDeVenta)
            {
                if (check.Checked)
                {
                    grilla.DataSource = _ofertaServicio.ObtenerOfertasInactivasCompuesta(cadenaBuscar);
                    toolStrip.Enabled = false;
                }
                else
                {
                    grilla.DataSource = _ofertaServicio.ObtenerOfertasActivasCompuestas(cadenaBuscar);
                    toolStrip.Enabled = true;
                    
                }
            }
            else
            {
                grilla.DataSource = _ofertaServicio.ObtenerOfertasActivasInactivas(cadenaBuscar);
                toolStrip.Enabled = false;
                
                /*if (check.Checked)
                {
                    grilla.DataSource = _ofertaServicio.ObtenerOfertasInactivas(cadenaBuscar);
                    toolStrip.Enabled = false;
                }
                else
                {
                    grilla.DataSource = _ofertaServicio.ObtenerOfertasActivas(cadenaBuscar);
                    toolStrip.Enabled = true;
                }*/
            }
        }

        public void ControlCargaExistencaDatos()
        {
            if (dgvGrilla.RowCount > 0)
            {
                if (!entidadID.HasValue)
                {
                    MessageBox.Show("Por favor seleccione un registro.");
                    puedeEjecutarComando = false;
                    return;
                }
                else
                {
                    puedeEjecutarComando = true;
                }
            }
            else
            {
                MessageBox.Show("No hay Datos Cargados.");
            }
        }
        private void btnSeleccionarParaVenta_Click(object sender, EventArgs e)
        {

            ofertaSeleccionada = (long)entidadID;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FOfertaConsulta_Load(object sender, EventArgs e)
        {
            this.cbxEstaEliminado.Text = "Mostrar ofertas inactivas";

        }

        private void btnActivarDesactivarOferta_Click(object sender, EventArgs e)
        {
            ofertaSeleccionada = (long)entidadID;
            if (!ofertaSeleccionada.HasValue)
            {
                MessageBox.Show("Por favor seleccione una oferta.");
                return;
            }

            var ofertaAD = _ofertaServicio.ActivarDesactivar(ofertaSeleccionada.Value);

        // Recargar los datos en la grilla ANTES de cerrar el formulario
        // Llamo a ActualizarDatos para que vuelva a setear DataSource según el estado del checkbox
        // Ajustá los parámetros si usás otro control/toolstrip
          _vieneDeVenta = false; // Asegurarse de que no está en modo venta para recargar correctamente
            ActualizarDatos(dgvGrilla, string.Empty, cbxEstaEliminado, BarraLateralBotones);

            // Volver a aplicar format (reemplaza columna bool por texto)

            // Si querés cerrar el diálogo solo si la operación fue OK:
            if (ofertaAD.OfertaDescuentoId != null)
            {
                MessageBox.Show($"La oferta {ofertaAD.Codigo} paso a estar Activa.");

            } else
            {
                MessageBox.Show("No se pudo cambiar el estado de la oferta.");
            }
        }
    }
}