using AccesoDatos.Entidades;
using Microsoft.IdentityModel.Tokens;
using Presentacion.Core.Producto;
using Presentacion.Core.Venta;
using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Articulo.Marca;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Producto.Rubro;
using Servicios.LogicaNegocio.Venta.DTO;
using Servicios.LogicaNegocio.Venta.Oferta;
using Servicios.LogicaNegocio.Venta.Oferta.DTO;
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
    public partial class FOfertaABM : FBase.FBase
    {
        private readonly IOfertaServicio _ofertaServicio;
        protected long? EntidadID;
        private bool _esCombinacionProductos = false;
        private bool _ofertaActiva = false;
        private bool _es2x1 = false;
        private bool _hastaCumplirLimiteDeStock = false;
        private decimal _limiteDeStock = 0.0m;
        private DateTime _fechaInicio;
        private DateTime _fechaFin;
        private string _descripcion;
        private decimal _precioFinal;
        private decimal _precioOriginal;
        private List<ProductosEnOfertaDescuentos> _productosDentroDeLaOferta;//oldver
        private BindingList<ProductoDTO> _productosOfertaDTO;
        private BindingList<ProductoDTO> _productosParaOferta;
        private readonly TipoOferta _tipoOferta;
        private string _textoDescriptivo;
        private decimal _cantidadProductos = 0.0m;
        private bool _precioMontoFijo = false;
        private string _codigoN = "*";


        public FOfertaABM(TipoOferta tipoOferta)
        {
            InitializeComponent();
            _ofertaServicio = new OfertaServicio();
            _tipoOferta = tipoOferta;

        }

        //private void cbxCantidadProductos_CheckedChanged(object sender, EventArgs e)
        //{
        //    _esCombinacionProductos = cbxCombinacionProductos.Checked;
        //    if (_esCombinacionProductos)
        //    {
        //        cbx2x1.Enabled = false;
        //    }
        //    else
        //    {
        //        cbx2x1.Enabled = true;

        //    }
        //}

        //private void cbxEstaActiva_CheckedChanged(object sender, EventArgs e)
        //{
        //    //_ofertaActiva = cbxEstaActiva.Checked;
        //    //ahorava a ser una preg
        //}

        //private void cbx2x1_CheckedChanged(object sender, EventArgs e)
        //{
        //    _es2x1 = cbx2x1.Checked;
        //    if (_es2x1)
        //    {
        //        cbxCombinacionProductos.Enabled = false;
        //    }
        //    else
        //    {
        //        cbxCombinacionProductos.Enabled = true;

        //    }
        //}

        private void dtpFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaFin.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La fecha de Inicio de la oferta no puede ser mayor a la fecha de fin.");
                dtpFechaInicio.Value = dtpFechaFin.Value;
            }
            else
            {
                _fechaInicio = dtpFechaInicio.Value;
            }
        }

        private void dtpFechaFin_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaFin.Value < dtpFechaInicio.Value)
            {
                MessageBox.Show("La fecha de Fin de la oferta no puede ser menor a la fecha de inicio.");
                dtpFechaFin.Value = dtpFechaInicio.Value;
            }
            else
            {
                _fechaFin = dtpFechaFin.Value;
            }
        }

        //private void btnCrear_Click(object sender, EventArgs e)
        //{
        //    CrearNuevaOferta();
        //}

        //private void CrearNuevaOferta()
        //{
        //    var ofertaNueva = new OfertaDTO
        //    {
        //        Descripcion = _descripcion,
        //        PrecioFinal = _precioFinal,
        //        PrecioOriginal = _precioOriginal,
        //        DescuentoTotalFinal = 0.0m,
        //        PorcentajeDescuento = 0.0m,
        //        FechaInicio = _fechaInicio,
        //        Codigo = string.Empty,
        //        Detalle = string.Empty,
        //        FechaFin = _fechaFin,
        //        CantidadProductosDentroOferta = 0.0m,
        //        EstaActiva = _ofertaActiva,
        //        EsUnSoloProducto = !_esCombinacionProductos,
        //        Productos = _productosOfertaDTO,
        //        esOfertaPorGrupo = false,
        //        IdMarca = 0,
        //        IdRubro = 0,
        //        IdCategoria = 0,
        //        GrupoNombre = string.Empty,
        //    };
        //}

        private void ActualizarGrillas()
        {
            dgvProductos.Refresh(); // refresca la visualización
        }



        public virtual void IniciarGrilla(DataGridView grilla)
        {
            for (int i = 0; i < grilla.ColumnCount; i++)
            {
                grilla.Columns[i].Visible = false;
            }
        }

        private void ResetearGrilla(DataGridView grilla)
        {
            //deberia crear un itemOferta
            for (int i = 0; i < grilla.ColumnCount; i++)
            {
                grilla.Columns[i].Visible = false;
            }
            grilla.Columns["ProductoId"].Visible = false;
            grilla.Columns["ProductoId"].DisplayIndex = 0;

            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].DisplayIndex = 1;
            grilla.Columns["Descripcion"].HeaderText = "Descripción";

            grilla.Columns["Stock"].Visible = true;
            grilla.Columns["Stock"].Width = 100;
            grilla.Columns["Stock"].DisplayIndex = 2;
            grilla.Columns["Stock"].HeaderText = "Cantidad disponible";

            grilla.Columns["Codigo"].Visible = true;
            grilla.Columns["Codigo"].Width = 100;
            grilla.Columns["Codigo"].DisplayIndex = 2;
            grilla.Columns["Codigo"].HeaderText = "Código";

            grilla.Columns["PrecioVenta"].Visible = true;
            grilla.Columns["PrecioVenta"].Width = 100;
            grilla.Columns["PrecioVenta"].DisplayIndex = 5;
            grilla.Columns["PrecioVenta"].HeaderText = "Precio Venta";

            grilla.Columns["PrecioCosto"].Visible = true;
            grilla.Columns["PrecioCosto"].Width = 100;
            grilla.Columns["PrecioCosto"].DisplayIndex = 5;
            grilla.Columns["PrecioCosto"].HeaderText = "Precio Costo";

            grilla.Columns["CantidadItemEnOferta"].Visible = true;
            grilla.Columns["CantidadItemEnOferta"].Width = 100;
            grilla.Columns["CantidadItemEnOferta"].DisplayIndex = 6;
            grilla.Columns["CantidadItemEnOferta"].HeaderText = "Cantidad en oferta";

        }
        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var fila = dgvProductos.Rows[e.RowIndex];
            if (fila == null || fila.IsNewRow) return;

            // Aquí tu código seguro para manejar el click
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null) return;
            if (dgvProductos.CurrentRow.IsNewRow) return;

            if (!dgvProductos.Columns.Contains("ProductoId")) return;

            var val = dgvProductos.CurrentRow.Cells["ProductoId"].Value;
            if (val == null || val == DBNull.Value) return;

            // Lógica segura con val
        }

        private void dgvProductos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0 || e.RowIndex >= dgvProductos.Rows.Count)
                return;

            var fila = dgvProductos.Rows[e.RowIndex];
            if (fila == null || fila.IsNewRow)
                return;

            if (!dgvProductos.Columns.Contains("ProductoId"))
                return;

            var celda = fila.Cells["ProductoId"];
            if (celda?.Value == null || celda.Value == DBNull.Value)
                return;

            long entidadID = Convert.ToInt64(celda.Value);
            // tu lógica...
        }

        private void btnCargarProducto_Click(object sender, EventArgs e)
        {
            var fProductos = new FProductoConsulta(true);

            if (fProductos.ShowDialog() != DialogResult.OK ||
                !fProductos.productoSeleccionado.HasValue)
                return;

            var idProducto = fProductos.productoSeleccionado.Value;
            var producto = new ProductoServicio().ObtenerProductoPorId(idProducto);
            if (producto == null) return;

            var fCantidad = new FCantidadItem();
            if (fCantidad.ShowDialog() != DialogResult.OK || fCantidad.cantidad <= 0)
                return;

            var cantidad = fCantidad.cantidad;

            var productoDto = new ProductoDTO
            {
                ProductoId = producto.ProductoId,
                IdMarca = producto.IdMarca,
                IdRubro = producto.IdRubro, // 👈 OJO acá tenías mal IdMarca
                Stock = producto.Stock,
                PrecioCosto = producto.PrecioCosto,
                PrecioVenta = producto.PrecioVenta,
                Descripcion = producto.Descripcion,
                EstaEliminado = producto.EstaEliminado,
                Estado = producto.Estado,
                Medida = producto.Medida,
                UnidadMedida = producto.UnidadMedida,
                Codigo = producto.Codigo,
                CodigoBarra = producto.CodigoBarra,
                IvaIncluidoPrecioFinal = producto.IvaIncluidoPrecioFinal,
                EsFraccionable = producto.EsFraccionable,
                MarcaNombre = string.Empty,
                RubroNombre = string.Empty,
                CategoriaIds = producto.CategoriaIds,
                CantidadItemEnOferta = cantidad
            };

            // 🔥 Validación real: productos distintos
            var productosDistintos = _productosParaOferta
                                        .Select(x => x.ProductoId)
                                        .Distinct()
                                        .Count();

            if (productosDistintos >= 2)
            {
                var respuesta = MessageBox.Show(
                    "La oferta ya tiene 2 productos distintos.\n¿Esta seguro que desea agregar un tercero?",
                    "Advertencia",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (respuesta == DialogResult.No)
                    return;
            }

            _productosParaOferta.Add(productoDto);

            RecalcularTotales();

            _descripcion += $"({productoDto.Descripcion} Código:{productoDto.Codigo} Cant:{cantidad}) + ";
            txtDescripcion.Text = _descripcion;
        }
        private void RecalcularTotales()
        {
            var totalCosto = _productosParaOferta
                                .Sum(x => x.PrecioCosto * x.CantidadItemEnOferta);

            var totalVenta = _productosParaOferta
                                .Sum(x => x.PrecioVenta * x.CantidadItemEnOferta);

            txtPrecioCostoAcumulado.Text = totalCosto?.ToString("N2");
            txtPrecioVentaReal.Text = totalVenta?.ToString("N2");
        }
        private void FOfertaABM_Load(object sender, EventArgs e)
        {
            LimpiarInicializarControles();
            ConfigurarCodigoManualOAutomatico();
        }

        private void ConfigurarCodigoManualOAutomatico()
        {
            var respuesta = MessageBox.Show(
                "¿Desea ingresar el código de la oferta manualmente?",
                "Tipo de Código",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                while (true)
                {
                    string codigoManual = MostrarInputCodigo();

                    if (string.IsNullOrWhiteSpace(codigoManual))
                        return;

                    if (_ofertaServicio.ExisteOfertaPorCodigo(codigoManual))
                    {
                        MessageBox.Show(
                            $"Ya existe una oferta con el código '{codigoManual}'. Ingrese otro.",
                            "Código duplicado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        continue; // vuelve al inicio del while
                    }

                    _codigoN = codigoManual;
                    lblCodigoManual.Text = _codigoN;
                    break; // sale del while
                }
            }
            else
            {
                _codigoN = "*";
            }
        }
        private string MostrarInputCodigo()
        {
            Form prompt = new Form()
            {
                Width = 350,
                Height = 170,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Ingrese Código Manual",
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label lbl = new Label()
            {
                Left = 20,
                Top = 20,
                Text = "Código:",
                AutoSize = true
            };

            TextBox txt = new TextBox()
            {
                Left = 20,
                Top = 45,
                Width = 280
            };

            Button btnOk = new Button()
            {
                Text = "Aceptar",
                Left = 140,
                Width = 75,
                Top = 80,
                DialogResult = DialogResult.OK
            };

            Button btnCancel = new Button()
            {
                Text = "Cancelar",
                Left = 225,
                Width = 75,
                Top = 80,
                DialogResult = DialogResult.Cancel
            };

            prompt.Controls.Add(lbl);
            prompt.Controls.Add(txt);
            prompt.Controls.Add(btnOk);
            prompt.Controls.Add(btnCancel);

            prompt.AcceptButton = btnOk;
            prompt.CancelButton = btnCancel;

            return prompt.ShowDialog() == DialogResult.OK ? txt.Text : null;
        }

        //private void cbxDescuentoPesos_CheckedChanged(object sender, EventArgs e)
        //{
        //    _precioMontoFijo = cbxDescuentoPesos.Checked;
        //    if (_precioMontoFijo)
        //    {
        //        txtPrecioDescuentoPesos.Text = string.Empty;
        //        txtPrecioTotalOfertaAplicada.Text = string.Empty;
        //    }
        //}


        private void btnCrear_Click_1(object sender, EventArgs e)
        {
            try
            {
                btnCrear.Enabled = false;

                if (_hastaCumplirLimiteDeStock)
                {
                    if (string.IsNullOrEmpty(txtLimiteStock.Text))
                    {
                       MessageBox.Show("Debe ingresar un límite de stock si seleccionó la opción de hasta cumplir límite de stock.");
                        return;
                    }
                    if (!decimal.TryParse(txtLimiteStock.Text, out decimal limiteStock) || limiteStock <= 0)
                    {
                        MessageBox.Show("El límite de stock debe ser un número positivo válido.");
                        return;
                    }
                    _limiteDeStock = limiteStock;
                }
                _precioFinal = decimal.TryParse(txtPrecioDescuentoPesos.Text, out decimal precioFinal) ? precioFinal : 0.0m;
                _precioOriginal = _productosParaOferta.Sum(x => x.PrecioVenta * (decimal)x.CantidadItemEnOferta);
                _cantidadProductos = _productosParaOferta.Count();

                var descuentoTotalFinal = _precioOriginal - _precioFinal;
                var precioOriginalFinal = _productosParaOferta.Sum(x => x.PrecioVenta * (decimal)x.CantidadItemEnOferta);

                _ofertaActiva = MessageBox.Show(
                "¿Desea activar la oferta ahora? (Si no la activa, podrá activarla manualmente más adelante, pero no se ejecutará automáticamente en la fecha de inicio).",
                "Activar oferta",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;

                var ofertaDto = new OfertaDTO
                {
                    Descripcion = txtDescripcion.Text?.Trim(),
                    PrecioFinal = _precioFinal,
                    PrecioOriginal = precioOriginalFinal,
                    DescuentoTotalFinal = descuentoTotalFinal,
                    PorcentajeDescuento = -1.0m,
                    FechaInicio = dtpFechaInicio.Value,
                    FechaFin = dtpFechaFin.Value,
                    CantidadProductosDentroOferta = _cantidadProductos, // si esto puede ser null, convertí igual
                    EstaActiva = _ofertaActiva, 
                    EsUnSoloProducto = _productosParaOferta.Count() > 1 ? false : true,
                    Detalle = txtDetalle.Text?.Trim(),
                    Codigo = _codigoN,
                    esOfertaPorGrupo = false,
                    TieneLimiteDeStock = _hastaCumplirLimiteDeStock,
                    CantidadLimiteDeStock = _limiteDeStock,
                    IdMarca = null,
                    IdRubro = null,
                    IdCategoria = null,
                    GrupoNombre = string.Empty,
                    Productos = _productosParaOferta.ToList()
                };
                var resultado = _ofertaServicio.Insertar(ofertaDto);

                if (resultado == null)
                {
                    MessageBox.Show("No se recibió respuesta del servicio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!resultado.Exitoso)
                {
                    MessageBox.Show(resultado.Mensaje ?? "Error al crear la oferta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show(resultado.Mensaje ?? "Oferta creada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarInicializarControles();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCrear.Enabled = true;
            }
        }


        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (!_precioMontoFijo)
            {
                MessageBox.Show("Debe ingresar un valor en el campo de descuento");

                return;
            }
            if ((_precioMontoFijo && txtPrecioDescuentoPesos.Text.IsNullOrEmpty()))
            {

                MessageBox.Show("Debe ingresar un valor en el campo de descuento");
                return;
            }
            if (_productosParaOferta.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto a la oferta");
                return;
            }
            //if (_precioMontoFijo)
            //{
            //    _precioFinal = decimal.TryParse((txtPrecioDescuentoPesos.Text), out decimal precioFinal) ? precioFinal : 0.0m;
            //    _precioOriginal = _productosParaOferta.Sum(x => x.PrecioVenta * (decimal)x.CantidadItemEnOferta);
            //    txtPrecioTotalOfertaAplicada.Text = _precioFinal.ToString("N2");
            //    txtPrecioTotalRealProductos.Text = _precioOriginal.ToString("N2");
            //    txtPrecioTotalPerdido.Text = (_precioOriginal - _precioFinal).ToString("N2");
            //}

        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarInicializarControles();
        }

        private void cbxLimiteCumplirStock_CheckedChanged(object sender, EventArgs e)
        {
            txtLimiteStock.Enabled = cbxLimiteCumplirStock.Checked;
            _hastaCumplirLimiteDeStock = cbxLimiteCumplirStock.Checked;
        }

        private void LimpiarInicializarControles()
        {
            _precioOriginal = 0.0m;
            _precioFinal = 0.0m;
            _precioMontoFijo = false;
            dgvProductos.AllowUserToAddRows = false;
            _productosParaOferta = new BindingList<ProductoDTO>();
            dgvProductos.DataSource = _productosParaOferta;  // bind directo
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now.AddDays(1);
            _fechaInicio = dtpFechaInicio.Value;
            _fechaFin = dtpFechaFin.Value;
            txtDescripcion.Text = string.Empty;
            txtPrecioDescuentoPesos.Text = string.Empty;
            //txtPrecioTotalOfertaAplicada.Text = string.Empty;
            //txtPrecioTotalPerdido.Text = string.Empty;
            //txtPrecioTotalRealProductos.Text = string.Empty;
            //cbxDescuentoPesos.Checked = false;
            //_codigoOferta = $"Of-PROD_{DateTime.Now.ToString("yyyyMMddHHmmss")}_";
            ResetearGrilla(dgvProductos);
        }
    }
}