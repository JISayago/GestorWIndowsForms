using AccesoDatos.Entidades;
using Microsoft.IdentityModel.Tokens;
using Presentacion.Core.Articulo.Marca;
using Presentacion.Core.Categoria;
using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.Core.Producto;
using Presentacion.Core.Producto.Rubro;
using Presentacion.Core.Venta;
using Servicios.Helpers.Sistema;
using Servicios.LogicaNegocio.Articulo.Categoria;
using Servicios.LogicaNegocio.Articulo.Marca;
using Servicios.LogicaNegocio.Empleado.Rol.DTO;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Producto.Rubro;
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
    public partial class FOfertaGrupoABM : Form
    {
        private readonly OfertaServicio _ofertaServicio;
        private readonly TipoOferta _tipoOferta;
        private bool _ofertaActiva = false;
        private DateTime _fechaInicio;
        private DateTime _fechaFin;
        private bool _esMarca = false;
        private bool _esRubro = false;
        private bool _esCategoria = false;
        private long? _marcaId;
        private long? _rubroId;
        private long? _categoriaId;
        private string _marcaN;
        private string _categoriaN;
        private string _rubroN;
        private decimal _precioFinal;
        private decimal _precioOriginal;
        private decimal _cantidadProductos = 0.0m;
        private decimal _cantidadProductosQuitados = 0.0m;
        private bool _precioMontoFijo = false;
        private bool _precioPorcentaje = false;
        private readonly IMarcaServicio _marcaServicio;
        private readonly IRubroServicio _rubroServicio;
        private readonly ICategoriaServicio _categoriaServicio;
        private readonly IProductoServicio _productoServicio;
        private long? _productoId;
        private string _descripcion = "Oferta Compuesta: filtrado";
        private decimal cantidadTotalEnOferta = 0.0m;
        private decimal cantidadTotalFueraOferta = 0.0m;
        private bool _esUnSoloProducto = false;
        private bool _hastaCumplirStock = false;
        private string _codigoOferta = string.Empty;
        private bool forzarInactivo = false;
        private string _codigoN = "*";

        private BindingList<ProductoDTO> _productosParaOfertaDTO;
        private BindingList<ProductoDTO> _productosParaQuitarDeOfertaDTO;

        public FOfertaGrupoABM()
        {
            InitializeComponent();
            _marcaServicio = new MarcaServicio();
            _rubroServicio = new RubroServicio();
            _categoriaServicio = new CategoriaServicio();
            _productoServicio = new ProductoServicio();
            _ofertaServicio = new OfertaServicio();
        }

        private void cbxMarca_CheckedChanged(object sender, EventArgs e)
        {
            _esMarca = cbxMarca.Checked;
            if (_esMarca)
            {
                btnCargarGrupoMarca.Enabled = true;
            }
            else
            {
                btnCargarGrupoMarca.Enabled = false;
            }
        }

        private void cbxRubro_CheckedChanged(object sender, EventArgs e)
        {
            _esRubro = cbxRubro.Checked;
            if (_esRubro)
            {
                btnCargarGrupoRubro.Enabled = true;
            }
            else
            {
                btnCargarGrupoRubro.Enabled = false;
            }


        }

        private void cbxCategoria_CheckedChanged(object sender, EventArgs e)
        {
            _esCategoria = cbxCategoria.Checked;
            if (_esCategoria)
            {
                btnCargarGrupoCategoria.Enabled = true;
            }
            else
            {
                btnCargarGrupoCategoria.Enabled = false;
            }
        }

        private void btnCargarGrupoMarca_Click(object sender, EventArgs e)
        {
            if (_esMarca)
            {
                var fMarca = new FMarcaConsulta(true);
                if (fMarca.ShowDialog() == DialogResult.OK && fMarca.marcaSeleccionada.HasValue)
                {
                    var marca = _marcaServicio.ObtenerPorId(fMarca.marcaSeleccionada.Value);
                    _marcaId = fMarca.marcaSeleccionada.Value;
                    txtMarca.Text = marca.Nombre;
                    _marcaN = marca.Nombre;
                }
            }

        }

        private void FOfertaGrupoABM_Load(object sender, EventArgs e)
        {
            dgvProductos.AllowUserToAddRows = false;
            dgvProductosQuitados.AllowUserToAddRows = false;

            _productosParaOfertaDTO = new BindingList<ProductoDTO>();
            _productosParaQuitarDeOfertaDTO = new BindingList<ProductoDTO>();

            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now.AddDays(1);

            _codigoN = CodigoOfertaHelper.ObtenerCodigo(_ofertaServicio, this);

            if (_codigoN == null)
            {
                Close();
                return;
            }

            lblCodigoManual.Text = _codigoN == "*" ? "AUTOMÁTICO" : _codigoN;

            ActualizarGrillas();
        }

        private void btnCargarGrupoRubro_Click(object sender, EventArgs e)
        {
            if (_esRubro)
            {
                var fRubro = new FRubroConsulta(true);
                if (fRubro.ShowDialog() == DialogResult.OK && fRubro.rubroSeleccionado.HasValue)
                {
                    var rubro = _rubroServicio.ObtenerPorId(fRubro.rubroSeleccionado.Value);
                    _rubroId = fRubro.rubroSeleccionado.Value;
                    txtRubro.Text = rubro.Nombre;
                    _rubroN = rubro.Nombre;
                }
            }
        }

        private void btnCargarGrupoCategoria_Click(object sender, EventArgs e)
        {

            if (_esCategoria)
            {
                var fCategoria = new FCategoriaConsulta(true);
                if (fCategoria.ShowDialog() == DialogResult.OK && fCategoria.categoriaSeleccionada.HasValue)
                {
                    var categoria = _categoriaServicio.ObtenerPorId(fCategoria.categoriaSeleccionada.Value);
                    _categoriaId = fCategoria.categoriaSeleccionada.Value;
                    txtCategoria.Text = categoria.Nombre;
                    _categoriaN = categoria.Nombre;
                }
            }
        }

        private void btnCargarProductosAlcanzados_Click(object sender, EventArgs e)
        {
            // Si estamos en modo "un solo producto" abrimos el formulario de consulta y cargamos ese producto
            if (_esUnSoloProducto)
            {
                using var fProductos = new FProductoConsulta(true);
                if (fProductos.ShowDialog() == DialogResult.OK && fProductos.productoSeleccionado.HasValue)
                {
                    try
                    {
                        var idProductoSeleccionado = fProductos.productoSeleccionado.Value;

                        // Obtener el producto desde el servicio (ajusta el nombre del método si es distinto)
                        var productoDto = _productoServicio.ObtenerProductoPorId(idProductoSeleccionado);


                        if (productoDto == null)
                        {
                            MessageBox.Show("No se encontró el producto seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Evitar duplicados (ajusta la propiedad Id si en tu DTO se llama distinto: ProductoId, IdProducto, etc.)
                        var existe = _productosParaOfertaDTO.Any(p =>
                        {
                            var prop = p.GetType().GetProperty("ProductoId");
                            if (prop != null) return Convert.ToInt64(prop.GetValue(p)) == Convert.ToInt64(productoDto.GetType().GetProperty("ProductoId").GetValue(productoDto));
                            // Si no hay propiedad Id, intenta con ProductoId (opcional)
                            prop = p.GetType().GetProperty("ProductoId");
                            return prop != null && Convert.ToInt64(prop.GetValue(p)) == Convert.ToInt64(productoDto.GetType().GetProperty("ProductoId").GetValue(productoDto));
                        });

                        if (existe)
                        {
                            MessageBox.Show("El producto ya está en la lista para la oferta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            _productosParaOfertaDTO.Add(productoDto);
                            // Si quieres forzar refrescar el grid:
                            dgvProductos.Refresh();
                        }

                        // Actualizamos contadores, etiquetas y descripción
                        _codigoOferta = _codigoOferta + $"{productoDto.Codigo}_{productoDto.Descripcion}";
                        cantidadTotalEnOferta = _productosParaOfertaDTO.Count();
                        cantidadTotalFueraOferta = _productosParaQuitarDeOfertaDTO?.Count() ?? 0;
                        lblNumeroProductoAfectados.Text = cantidadTotalEnOferta.ToString();
                        lblNumeroProductoQuitados.Text = cantidadTotalFueraOferta.ToString();
                        _descripcion = $"({_descripcion} M-{_marcaN} C-{_categoriaN}) + cant{cantidadTotalEnOferta}";
                        txtDescripcion.Text = _descripcion;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error cargando el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // terminamos — en modo "un solo producto" no ejecutamos la carga masiva
                return;
            }
            var productosOfertasDto = _productoServicio.ObtenerProductosPorMarcaRubroCategoriaParaOferta(_marcaId, _rubroId, _categoriaId);

            _productosParaOfertaDTO = new BindingList<ProductoDTO>(productosOfertasDto.ToList());
            dgvProductos.DataSource = _productosParaOfertaDTO;
            cantidadTotalEnOferta = _productosParaOfertaDTO.Count();
            cantidadTotalFueraOferta = _productosParaQuitarDeOfertaDTO.Count();
            lblNumeroProductoAfectados.Text = cantidadTotalEnOferta.ToString();
            lblNumeroProductoQuitados.Text = cantidadTotalFueraOferta.ToString();
            _codigoOferta = _codigoOferta + $"({_descripcion} M-{_marcaN}C-{_categoriaN})_{cantidadTotalEnOferta}_";
            _descripcion = $"({_descripcion} M-{_marcaN}C-{_categoriaN}) + cant{cantidadTotalEnOferta}";
            txtDescripcion.Text = _descripcion;


        }

        public virtual void IniciarGrilla(DataGridView grilla)
        {
            for (int i = 0; i < grilla.ColumnCount; i++)
            {
                grilla.Columns[i].Visible = false;
            }
        }

        private void ResetearGrillas(DataGridView grilaProductosOferta, DataGridView grillaProductosQuitados)
        {
            for (int i = 0; i < grilaProductosOferta.ColumnCount; i++)
            {
                grilaProductosOferta.Columns[i].Visible = false;
                grillaProductosQuitados.Columns[i].Visible = false;
            }
            //dentro de la oferta
            grilaProductosOferta.Columns["ProductoId"].Visible = false;
            grilaProductosOferta.Columns["ProductoId"].DisplayIndex = 0;

            grilaProductosOferta.Columns["Descripcion"].Visible = true;
            grilaProductosOferta.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilaProductosOferta.Columns["Descripcion"].DisplayIndex = 1;
            grilaProductosOferta.Columns["Descripcion"].HeaderText = "Descripción";

            grilaProductosOferta.Columns["Stock"].Visible = true;
            grilaProductosOferta.Columns["Stock"].Width = 100;
            grilaProductosOferta.Columns["Stock"].DisplayIndex = 2;
            grilaProductosOferta.Columns["Stock"].HeaderText = "Cantidad disponible";

            grilaProductosOferta.Columns["Codigo"].Visible = true;
            grilaProductosOferta.Columns["Codigo"].Width = 100;
            grilaProductosOferta.Columns["Codigo"].DisplayIndex = 2;
            grilaProductosOferta.Columns["Codigo"].HeaderText = "Código";

            grilaProductosOferta.Columns["PrecioVenta"].Visible = true;
            grilaProductosOferta.Columns["PrecioVenta"].Width = 100;
            grilaProductosOferta.Columns["PrecioVenta"].DisplayIndex = 5;
            grilaProductosOferta.Columns["PrecioVenta"].HeaderText = "Precio Venta";

            grilaProductosOferta.Columns["PrecioCosto"].Visible = true;
            grilaProductosOferta.Columns["PrecioCosto"].Width = 100;
            grilaProductosOferta.Columns["PrecioCosto"].DisplayIndex = 5;
            grilaProductosOferta.Columns["PrecioCosto"].HeaderText = "Precio Costo";

            //quitados de la oferta


            grillaProductosQuitados.Columns["ProductoId"].Visible = false;
            grillaProductosQuitados.Columns["ProductoId"].DisplayIndex = 0;

            grillaProductosQuitados.Columns["Descripcion"].Visible = true;
            grillaProductosQuitados.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grillaProductosQuitados.Columns["Descripcion"].DisplayIndex = 1;
            grillaProductosQuitados.Columns["Descripcion"].HeaderText = "Descripción";

            grillaProductosQuitados.Columns["Stock"].Visible = true;
            grillaProductosQuitados.Columns["Stock"].Width = 100;
            grillaProductosQuitados.Columns["Stock"].DisplayIndex = 2;
            grillaProductosQuitados.Columns["Stock"].HeaderText = "Cantidad disponible";

            grillaProductosQuitados.Columns["Codigo"].Visible = true;
            grillaProductosQuitados.Columns["Codigo"].Width = 100;
            grillaProductosQuitados.Columns["Codigo"].DisplayIndex = 2;
            grillaProductosQuitados.Columns["Codigo"].HeaderText = "Código";

            grillaProductosQuitados.Columns["PrecioVenta"].Visible = true;
            grillaProductosQuitados.Columns["PrecioVenta"].Width = 100;
            grillaProductosQuitados.Columns["PrecioVenta"].DisplayIndex = 5;
            grillaProductosQuitados.Columns["PrecioVenta"].HeaderText = "Precio Venta"; ;

            grillaProductosQuitados.Columns["PrecioCosto"].Visible = true;
            grillaProductosQuitados.Columns["PrecioCosto"].Width = 100;
            grillaProductosQuitados.Columns["PrecioCosto"].DisplayIndex = 5;
            grillaProductosQuitados.Columns["PrecioCosto"].HeaderText = "Precio Costo";

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

            _productoId = (long)celda.Value;
        }
        private void ActualizarGrillas()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = _productosParaOfertaDTO;

            dgvProductosQuitados.DataSource = null;
            dgvProductosQuitados.DataSource = _productosParaQuitarDeOfertaDTO;
        }
        private void btnQuitarProducto_Click(object sender, EventArgs e)
        {

            var productoSeleccionado = ObtenerProducto(dgvProductos);
            if (productoSeleccionado == null)
            {
                MessageBox.Show("Por favor seleccione un producto disponible para quitar de la oferta.", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var respuesta = MessageBox.Show($"¿Esta seguro que desea quitar el producto {productoSeleccionado.Descripcion.ToUpper()} del alcance de la oferta?",
           "Confirmar acción",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes) return;

            // Buscar el elemento real en la lista por ID (evita problemas de referencia)
            var productoEnLista = _productosParaOfertaDTO
                .FirstOrDefault(p => p.ProductoId == productoSeleccionado.ProductoId);


            if (productoEnLista == null)
            {
                MessageBox.Show("No se encontró el producto en la lista principal (probable problema de referencia).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mover entre listas
            _productosParaOfertaDTO.Remove(productoEnLista);
            _productosParaQuitarDeOfertaDTO.Add(productoEnLista);

            cantidadTotalEnOferta = _productosParaOfertaDTO.Count();
            cantidadTotalFueraOferta = _productosParaQuitarDeOfertaDTO.Count();
            lblNumeroProductoAfectados.Text = cantidadTotalEnOferta.ToString();
            lblNumeroProductoQuitados.Text = cantidadTotalFueraOferta.ToString();

            // Forzar refresco si fuera necesario (normalmente no hace falta)
            var cm = (CurrencyManager)BindingContext[_productosParaOfertaDTO];
            cm.Refresh();
        }

        private ProductoDTO ObtenerProducto(DataGridView grillaProductosOferta)
        {
            if (grillaProductosOferta.CurrentRow != null && grillaProductosOferta.CurrentRow.DataBoundItem is ProductoDTO producto)
                return producto;

            return null;
        }

        private void btnDevolverAOferta_Click(object sender, EventArgs e)
        {
            var productoSeleccionado = ObtenerProducto(dgvProductosQuitados);
            if (productoSeleccionado == null)
            {
                MessageBox.Show("Por favor seleccione un producto disponible para devolver a la oferta.", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var respuesta = MessageBox.Show($"¿Esta seguro que desea quitar el producto {productoSeleccionado.Descripcion.ToUpper()} del alcance de la oferta?",
           "Confirmar acción",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes) return;

            // Buscar el elemento real en la lista por ID (evita problemas de referencia)
            var productoEnListaParaDevolver = _productosParaQuitarDeOfertaDTO
                .FirstOrDefault(p => p.ProductoId == productoSeleccionado.ProductoId);


            if (productoEnListaParaDevolver == null)
            {
                MessageBox.Show("No se encontró el producto en la lista principal (probable problema de referencia).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mover entre listas
            _productosParaQuitarDeOfertaDTO.Remove(productoEnListaParaDevolver);
            _productosParaOfertaDTO.Add(productoEnListaParaDevolver);

            cantidadTotalEnOferta = _productosParaOfertaDTO.Count();
            cantidadTotalFueraOferta = _productosParaQuitarDeOfertaDTO.Count();

            lblNumeroProductoAfectados.Text = cantidadTotalEnOferta.ToString();
            lblNumeroProductoQuitados.Text = cantidadTotalFueraOferta.ToString();
            // Forzar refresco si fuera necesario (normalmente no hace falta)
            var cm = (CurrencyManager)BindingContext[_productosParaQuitarDeOfertaDTO];
            cm.Refresh();
        }


        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                btnCrear.Enabled = false;

                if (!ValidarFormulario())
                    return;

                var ofertaDto = ConstruirOfertaDto();

                var resultado = _ofertaServicio.Insertar(ofertaDto);

                if (resultado == null || !resultado.Exitoso)
                {
                    MessageBox.Show(resultado?.Mensaje ?? "Error al crear la oferta.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show(resultado.Mensaje ?? "Oferta creada correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCrear.Enabled = true;
            }
        }

        private bool ValidarFormulario()
        {
            if (cbxComboProductos.Checked &&
                (_productosParaOfertaDTO == null || !_productosParaOfertaDTO.Any()))
            {
                MessageBox.Show("Debe agregar al menos un producto.");
                return false;
            }

            if (!cbxDescuentoPorcentaje.Checked && !cbxDescuentoPesos.Checked)
            {
                MessageBox.Show("Debe seleccionar tipo de descuento.");
                return false;
            }

            if (cbxDescuentoPorcentaje.Checked && cbxDescuentoPesos.Checked)
            {
                MessageBox.Show("Seleccione solo un tipo de descuento.");
                return false;
            }

            if (!decimal.TryParse(txtMontoPorcentaje.Text, out decimal valor) || valor <= 0)
            {
                MessageBox.Show("Ingrese un valor válido.");
                return false;
            }

            if (cbxLimiteCumplirStock.Checked)
            {
                if (!decimal.TryParse(txtLimiteStock.Text, out decimal limite) || limite <= 0)
                {
                    MessageBox.Show("El límite de stock debe ser mayor a 0.");
                    return false;
                }
            }

            return true;
        }

        private (decimal precioOriginal, decimal precioFinal, decimal descuento, decimal porcentaje)
            CalcularDescuento()
        {
            decimal precioOriginal;
            if (cbxComboProductos.Checked)
                precioOriginal = _productosParaOfertaDTO
                    .Sum(p => p.PrecioVenta * (decimal)p.CantidadItemEnOferta);
            else
                precioOriginal = _productosParaOfertaDTO
                    .Sum(p => p.PrecioVenta);

            decimal valorIngresado = Convert.ToDecimal(txtMontoPorcentaje.Text);

            decimal precioFinal = precioOriginal;
            decimal descuento = 0;
            decimal porcentaje = 0;

            if (cbxDescuentoPorcentaje.Checked)
            {
                porcentaje = valorIngresado;
                descuento = precioOriginal * (porcentaje / 100m);
                precioFinal = precioOriginal - descuento;
            }
            else
            {
                precioFinal = valorIngresado;
                descuento = precioOriginal - precioFinal;
                porcentaje = (descuento / precioOriginal) * 100m;
            }

            return (precioOriginal, precioFinal, descuento, porcentaje);
        }

        private OfertaDTO ConstruirOfertaDto()
        {
            var calculo = CalcularDescuento();
            var hora = DateTime.Now;
            _ofertaActiva  = MessageBox.Show(
                "¿Desea activar la oferta ahora? (Si no la activa, podrá activarla manualmente más adelante, pero no se ejecutará automáticamente en la fecha de inicio).",
                "Activar oferta",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;

            var ofertaDto = new OfertaDTO
            {
                Descripcion = $"{txtDescripcion.Text?.Trim()} {hora}",
                PrecioOriginal = calculo.precioOriginal,
                PrecioFinal = calculo.precioFinal,
                Productos = _productosParaOfertaDTO?.ToList(),
                DescuentoTotalFinal = calculo.descuento,
                PorcentajeDescuento = calculo.porcentaje,
                FechaInicio = dtpFechaInicio.Value,
                FechaFin = dtpFechaFin.Value,
                Detalle = txtDetalle.Text?.Trim(),
                Codigo = _codigoN,
                EstaActiva = _ofertaActiva,
                EsUnSoloProducto = (_productosParaOfertaDTO?.Count ?? 0) == 1,
                CantidadProductosDentroOferta =
                    _productosParaOfertaDTO?.Sum(p => p.CantidadItemEnOferta) ?? 0,
                TieneLimiteDeStock = cbxLimiteCumplirStock.Checked,
                CantidadLimiteDeStock = cbxLimiteCumplirStock.Checked
                    ? Convert.ToDecimal(txtLimiteStock.Text)
                    : -1
            };

            if (cbxComboProductos.Checked)
            {
                ofertaDto.esOfertaPorGrupo = false;
                ofertaDto.IdMarca = null;
                ofertaDto.IdRubro = null;
                ofertaDto.IdCategoria = null;
                ofertaDto.GrupoNombre = string.Empty;
                
            }
            else
            {
                ofertaDto.esOfertaPorGrupo = true;
                ofertaDto.IdMarca = _marcaId;
                ofertaDto.IdRubro = _rubroId;
                ofertaDto.IdCategoria = _categoriaId;
                ofertaDto.GrupoNombre = $"{_marcaN}-{_rubroN}-{_categoriaN}";
            }

            return ofertaDto;
        }

        private void cbxEsUnProducto_CheckedChanged(object sender, EventArgs e)
        {
            _esUnSoloProducto = _productosParaOfertaDTO.Count() > 1 ? false : true;
            btnCargarProductosAlcanzados.Text = _esUnSoloProducto ? "Cargar Producto" : "Cargar Productos Alcanzados";
            cbxCategoria.Checked = false;
            cbxCategoria.Enabled = !_esUnSoloProducto;
            cbxMarca.Checked = false;
            cbxMarca.Enabled = !_esUnSoloProducto;
            cbxRubro.Checked = false;
            cbxRubro.Enabled = !_esUnSoloProducto;
            btnQuitarProducto.Enabled = !_esUnSoloProducto;
            btnDevolverAOferta.Enabled = !_esUnSoloProducto;
            dgvProductosQuitados.Enabled = !_esUnSoloProducto;
        }

        private void cbxLimiteCumplirStock_CheckedChanged(object sender, EventArgs e)
        {
            _hastaCumplirStock = cbxLimiteCumplirStock.Checked;
            txtLimiteStock.Enabled = _hastaCumplirStock;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cbxCategoria.Checked = false;
            cbxMarca.Checked = false;
            cbxRubro.Checked = false;
            _productosParaOfertaDTO = new BindingList<ProductoDTO>();
            _productosParaQuitarDeOfertaDTO = new BindingList<ProductoDTO>();
            cantidadTotalEnOferta = 0.0m;
            cantidadTotalFueraOferta = 0.0m;
            lblCantidadProductos.Text = "0";
            lblCantidadProductosQuitados.Text = "0";
            txtDescripcion.Text = string.Empty;
            txtDetalle.Text = string.Empty;
            txtMontoPorcentaje.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtRubro.Text = string.Empty;
            txtCategoria.Text = string.Empty;
            cbxDescuentoPesos.Checked = false;
            cbxDescuentoPorcentaje.Checked = false;

            ActualizarGrillas();
        }

        private void cbxDescuentoPesos_CheckedChanged(object sender, EventArgs e)
        {
            cbxDescuentoPorcentaje.Checked = false;
        }

        private void cbxDescuentoPorcentaje_CheckedChanged(object sender, EventArgs e)
        {
            cbxDescuentoPesos.Checked = false;
        }

        private void cbxComboProductos_CheckedChanged(object sender, EventArgs e)
        {
            cbxCategoria.Enabled = !cbxComboProductos.Checked;
            cbxMarca.Enabled = !cbxComboProductos.Checked;
            cbxRubro.Enabled = !cbxComboProductos.Checked;
            cbxDescuentoPorcentaje.Enabled = !cbxComboProductos.Checked;
            cbxDescuentoPesos.Checked = cbxComboProductos.Checked;
            cbxDescuentoPesos.Enabled = !cbxComboProductos.Checked;
            btnCargarProductosAlcanzados.Enabled = !cbxComboProductos.Checked;
            btnCargarProducto.Enabled = cbxComboProductos.Checked;
            dgvProductosQuitados.Enabled = !cbxComboProductos.Checked;
            btnDevolverAOferta.Enabled = !cbxComboProductos.Checked;
            lblTotalPrecioCosto.Enabled = cbxComboProductos.Checked;
            lblTotalPrecioCosto.Visible = cbxComboProductos.Checked;
            txtPrecioCostoAcumulado.Visible = cbxComboProductos.Checked;
            lblTotalPrecioReal.Enabled = cbxComboProductos.Checked;
            lblTotalPrecioReal.Visible = cbxComboProductos.Checked;
            txtPrecioVentaReal.Visible = cbxComboProductos.Checked;

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
                IdRubro = producto.IdRubro,
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
            var productosDistintos = _productosParaOfertaDTO
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

            _productosParaOfertaDTO.Add(productoDto);

            RecalcularTotales();

            _descripcion += $"({productoDto.Descripcion} Código:{productoDto.Codigo} Cant:{cantidad}) + ";
            txtDescripcion.Text = _descripcion;
        }
        private void RecalcularTotales()
        {
            var totalCosto = _productosParaOfertaDTO
                                .Sum(x => x.PrecioCosto * x.CantidadItemEnOferta);

            var totalVenta = _productosParaOfertaDTO
                                .Sum(x => x.PrecioVenta * x.CantidadItemEnOferta);

            txtPrecioCostoAcumulado.Text = totalCosto?.ToString("N2");
            txtPrecioVentaReal.Text = totalVenta?.ToString("N2");
        }
    }
}
