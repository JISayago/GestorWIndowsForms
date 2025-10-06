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
        private bool _esUnSoloProducto = false;
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
        private bool _precioPorcentaje = false;
        private string _codigoOferta = string.Empty;


        public FOfertaABM(TipoOferta tipoOferta)
        {
            InitializeComponent();
            _ofertaServicio = new OfertaServicio();
            _tipoOferta = tipoOferta;

            CargarControlesSegunTipoOferta(_tipoOferta);
        }

        private void CargarControlesSegunTipoOferta(TipoOferta tipoOferta)
        {
            if (_tipoOferta == TipoOferta.Grupo)
            {
                var FOfertaGrupo = new FOfertaGrupoABM(TipoOferta.Grupo);
                FOfertaGrupo.ShowDialog();
                this.Close();
            }
        }

        private void cbxCantidadProductos_CheckedChanged(object sender, EventArgs e)
        {
            _esCombinacionProductos = cbxCombinacionProductos.Checked;
            if (_esCombinacionProductos)
            {
                cbx2x1.Enabled = false;
            }
            else
            {
                cbx2x1.Enabled = true;

            }
        }

        private void cbxEstaActiva_CheckedChanged(object sender, EventArgs e)
        {
            _ofertaActiva = cbxEstaActiva.Checked;
        }

        private void cbx2x1_CheckedChanged(object sender, EventArgs e)
        {
            _es2x1 = cbx2x1.Checked;
            if (_es2x1)
            {
                cbxCombinacionProductos.Enabled = false;
            }
            else
            {
                cbxCombinacionProductos.Enabled = true;

            }
        }

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

        private void btnCrear_Click(object sender, EventArgs e)
        {
            CrearNuevaOferta();
        }

        private void CrearNuevaOferta()
        {
            var ofertaNueva = new OfertaDTO
            {
                Descripcion = _descripcion,
                PrecioFinal = _precioFinal,
                PrecioOriginal = _precioOriginal,
                DescuentoTotalFinal = 0.0m,
                PorcentajeDescuento = 0.0m,
                FechaInicio = _fechaInicio,
                Codigo = string.Empty,
                Detalle = string.Empty,
                FechaFin = _fechaFin,
                CantidadProductosDentroOferta = 0.0m,
                EstaActiva = _ofertaActiva,
                EsUnSoloProducto = !_esCombinacionProductos,
                Productos = _productosOfertaDTO,
                esOfertaPorGrupo = false,
                IdMarca = 0,
                IdRubro = 0,
                IdCategoria = 0,
                GrupoNombre = string.Empty,
            };
        }

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
            if(!_es2x1 && !_esCombinacionProductos)
            {
                 MessageBox.Show("Debe seleccionar una opción de oferta: 2x1 o combinación de productos.");
                return;
            }
            var fProductos = new FProductoConsulta(true);

            if (fProductos.ShowDialog() == DialogResult.OK && fProductos.productoSeleccionado.HasValue)
            {
                var idProducto = fProductos.productoSeleccionado.Value;
                var producto = new ProductoServicio().ObtenerProductoPorId(idProducto);
                if (producto == null) return;

                var fCantidad = new FCantidadItem();
                if (fCantidad.ShowDialog() == DialogResult.OK && fCantidad.cantidad > 0)
                {
                    var cantidad = fCantidad.cantidad;

                    var productoDto = new ProductoDTO
                    {
                        ProductoId = producto.ProductoId,
                        IdMarca = producto.IdMarca,
                        IdRubro = producto.IdMarca,
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
                    };

                    // VALIDACIÓN: si _es2x1 es true, no permitir agregar productos distintos
                    if (_es2x1)
                    {
                        if (_productosParaOferta.Count > 0)
                        {
                            var primerId = _productosParaOferta[0].ProductoId;
                            if (primerId != productoDto.ProductoId)
                            {
                                MessageBox.Show("La oferta 2x1 sólo puede contener unidades del mismo producto. No se puede agregar un producto distinto.",
                                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return; // salimos sin agregar
                            }
                        }
                    }

                    // --- Opción recomendada: si ya existe el mismo producto, actualizar cantidad en la lista ---
                    // Si usás una DTO que guarda Cantidad (por ejemplo ProductosEnOfertaDescuentosDTO),
                    // sería mejor buscar si ya existe una entrada para ese ProductoId y sumar la cantidad.
                    // Ejemplo comentado (descomentar/adaptar si tenés lista con Cantidad):
                    /*
                    var existente = _productosOfertaDTO.FirstOrDefault(p => p.ProductoOfertaId == productoDto.ProductoId);
                    if (existente != null)
                    {
                        existente.Cantidad += cantidad;
                        // actualizar descripción/labels como corresponda
                    }
                    else
                    {
                        var productoOfertaDto = new ProductosEnOfertaDescuentosDTO
                        {
                            ProductoOfertaId = producto.ProductoId,
                            Descripcion = producto.Descripcion,
                            Codigo = producto.Codigo,
                            CodigoBarra = producto.CodigoBarra,
                            Cantidad = cantidad,
                            PrecioCosto = producto.PrecioCosto,
                            PrecioVenta = producto.PrecioVenta,
                        };
                        _productosOfertaDTO.Add(productoOfertaDto);
                    }
                    */

                    // Si no usás cantidades por elemento y querés simplemente agregar la entrada:
                    _productosParaOferta.Add(productoDto);

                    _descripcion = $"({_descripcion} {productoDto.Descripcion} codigo:{productoDto.Codigo} cantidad:{cantidad}) + ";
                    txtDescripcion.Text = _descripcion;
                    if (_es2x1)
                    {
                        cbxDescuentoPesos.Checked = true; // por defecto
                        txtPrecioDescuentoPesos.Text = productoDto.PrecioVenta.ToString("N2");
                    _codigoOferta = _codigoOferta + $"{productoDto.Codigo}_{cantidad}_";
                        txtCodigoOferta.Text = _codigoOferta;
                    }
                    _cantidadProductos = _cantidadProductos + cantidad;
                    lblCantidadProductos.Text = $"Cantidad Productos: {_cantidadProductos}";
                    if (_esCombinacionProductos)
                    {
                        _codigoOferta = _codigoOferta + $"{productoDto.Codigo}_{cantidad}_";
                        txtCodigoOferta.Text = _codigoOferta;
                    }
                }
            }
        }


        private void FOfertaABM_Load(object sender, EventArgs e)
        {
             dgvProductos.AllowUserToAddRows = false;
             _productosParaOferta = new BindingList<ProductoDTO>();
             dgvProductos.DataSource = _productosParaOferta;  // bind directo
             dtpFechaInicio.Value = DateTime.Now;
             dtpFechaFin.Value = DateTime.Now.AddDays(1);
             _fechaInicio = dtpFechaInicio.Value;
             _fechaFin = dtpFechaFin.Value;
            _codigoOferta = $"Of-COMP_{DateTime.Now.ToString("yyyyMMddHHmmss")}_";
            txtCodigoOferta.Text = _codigoOferta;
            ResetearGrilla(dgvProductos);
        }

        private void cbxDescuentoPesos_CheckedChanged(object sender, EventArgs e)
        {
            _precioMontoFijo = cbxDescuentoPesos.Checked;
            if (_precioMontoFijo)
            {
                cbxDescuentoPorcentaje.Checked = false;
                txtPrecioDescuentoPorcentaje.Enabled = false;
                txtPrecioDescuentoPesos.Text = string.Empty;
                txtPrecioTotalOfertaAplicada.Text = string.Empty;
            }
            else
            {
                cbxDescuentoPorcentaje.Checked = true;
                txtPrecioDescuentoPorcentaje.Enabled = true;
            }
        }

        private void cbxDescuentoPorcentaje_CheckedChanged(object sender, EventArgs e)
        {
            _precioPorcentaje = cbxDescuentoPorcentaje.Checked;
            if (_precioPorcentaje)
            {
                cbxDescuentoPesos.Checked = false;
                txtPrecioDescuentoPesos.Enabled = false;
                txtPrecioDescuentoPesos.Text = string.Empty;
                txtPrecioTotalOfertaAplicada.Text = string.Empty;
            }
            else
            {
                cbxDescuentoPesos.Checked = true;
                txtPrecioDescuentoPesos.Enabled = true;
            }
        }

        private void btnCrear_Click_1(object sender, EventArgs e)
        {
            if (txtCodigoOferta.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Debe ingresar un código para la oferta");
                return;
            }


            if (!_ofertaActiva)
            {
                var result = MessageBox.Show(
                    "Está creando una oferta que no estará activa.\n\n" +
                    "¿Desea continuar sin activarla o prefiere activarla ahora?. (Considere que debe estar activa para ejecutarse en la fecha de inicio especificado).",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    return;
                }
            }
            try
            {
                btnCrear.Enabled = false;

                var ofertaDto = new OfertaDTO
                {
                    //OfertaDescuentoId = 0, // si corresponde dejar 0 para nuevo registro
                    Descripcion = txtDescripcion.Text?.Trim(),
                    PrecioFinal = _precioFinal,
                    PrecioOriginal = 0.0m,
                    DescuentoTotalFinal = _precioOriginal - _precioFinal,
                    PorcentajeDescuento = 0.0m,//Convert.ToDecimal(txtPrecioDescuentoPorcentaje.Text),
                    FechaInicio = dtpFechaInicio.Value,
                    FechaFin = dtpFechaFin.Value,
                    CantidadProductosDentroOferta = _cantidadProductos, // si esto puede ser null, convertí igual
                    EstaActiva = cbxEstaActiva.Checked,
                    EsUnSoloProducto = false,
                    Detalle = txtDetalle.Text?.Trim(),
                    Codigo = txtCodigoOferta.Text?.Trim(),
                    esOfertaPorGrupo = false,
                    TieneLimiteDeStock = cbxLimiteCumplirStock.Checked,
                    CantidadLimiteDeStock = 0.0m, //cbxLimiteCumplirStock.Checked ? Convert.ToDecimal(txtLimiteStock.Text) : null,
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
            if (!_precioMontoFijo && !_precioPorcentaje)
            {
                MessageBox.Show("Debe ingresar un valor en el campo de descuento");

                return;
            }
            if ((_precioMontoFijo && txtPrecioDescuentoPesos.Text.IsNullOrEmpty()) || (_precioPorcentaje && txtPrecioDescuentoPorcentaje.Text.IsNullOrEmpty()))
            {

                MessageBox.Show("Debe ingresar un valor en el campo de descuento");
                return;
            }
            if (_productosParaOferta.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto a la oferta");
                return;
            }
            if (_precioMontoFijo)
            {
                _precioFinal = decimal.TryParse((txtPrecioDescuentoPesos.Text), out decimal precioFinal) ? precioFinal : 0.0m;
               // _precioOriginal = _productosOfertaDTO.Sum(x => x.Producto.PrecioVenta * x.Cantidad);
                txtPrecioTotalOfertaAplicada.Text = _precioFinal.ToString("N2");
                txtPrecioTotalRealProductos.Text = _precioOriginal.ToString("N2");
                txtPrecioTotalPerdido.Text = (_precioOriginal - _precioFinal).ToString("N2");
            }

        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {

        }

        private void cbxLimiteCumplirStock_CheckedChanged(object sender, EventArgs e)
        {
            txtLimiteStock.Enabled = cbxLimiteCumplirStock.Checked;
        }
    }
}