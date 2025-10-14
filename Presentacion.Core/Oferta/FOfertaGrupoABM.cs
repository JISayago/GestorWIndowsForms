using AccesoDatos.Entidades;
using Microsoft.IdentityModel.Tokens;
using Presentacion.Core.Articulo.Marca;
using Presentacion.Core.Categoria;
using Presentacion.Core.Producto;
using Presentacion.Core.Producto.Rubro;
using Servicios.Helpers;
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
        private readonly IOfertaServicio _ofertaServicio;
        private readonly TipoOferta _tipoOferta;
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
        private bool _ofertaActiva = false;
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

        private BindingList<ProductoDTO> _productosParaOfertaDTO;
        private BindingList<ProductoDTO> _productosParaQuitarDeOfertaDTO;

        public FOfertaGrupoABM(TipoOferta tipoOferta)
        {
            InitializeComponent();
            _marcaServicio = new MarcaServicio();
            _rubroServicio = new RubroServicio();
            _categoriaServicio = new CategoriaServicio();
            _productoServicio = new ProductoServicio();
            _ofertaServicio = new OfertaServicio();
            _tipoOferta = tipoOferta;
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
                var fMarca = new FMarcaConsulta();
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
            _fechaInicio = dtpFechaInicio.Value;
            _fechaFin = dtpFechaFin.Value;
            lblNumeroProductoAfectados.Text = cantidadTotalEnOferta.ToString();
            lblNumeroProductoQuitados.Text = cantidadTotalFueraOferta.ToString();
            _codigoOferta = $"Of-GRUPO_{DateTime.Now.ToString("yyyyMMddHHmmss")}_";
            txtCodigoOferta.Text = _codigoOferta;
            ActualizarGrillas();
            ResetearGrillas(dgvProductos, dgvProductosQuitados);

        }

        private void btnCargarGrupoRubro_Click(object sender, EventArgs e)
        {
            if (_esRubro)
            {
                var fRubro = new FRubroConsulta();
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
                var fCategoria = new FCategoriaConsulta();
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
                        txtCodigoOferta.Text = _codigoOferta;
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
            txtCodigoOferta.Text = _codigoOferta;
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
                var hora = DateTime.Now;
                var desc = txtDescripcion.Text?.Trim();
                var porcDesc = string.IsNullOrEmpty(txtPrecioDescuentoPorcentaje.Text) ? "0" : txtPrecioDescuentoPorcentaje.Text;
                var ofertaDto = new OfertaDTO
                {
                    Descripcion = $"{desc}{hora.ToString()}",
                    PrecioFinal = _precioFinal,
                    PrecioOriginal = -1m,
                    DescuentoTotalFinal = 1, //_precioOriginal - _precioFinal,
                    PorcentajeDescuento = Convert.ToDecimal(porcDesc),//Convert.ToDecimal(txtPrecioDescuentoPorcentaje.Text),
                    FechaInicio = dtpFechaInicio.Value,
                    FechaFin = dtpFechaFin.Value,
                    CantidadProductosDentroOferta = Convert.ToDecimal(cantidadTotalEnOferta),//_cantidadProductoWEs, // si esto puede ser null, convertí igual
                    EstaActiva = _ofertaActiva,//cbxEstaActiva.Checked,
                    EsUnSoloProducto = false,
                    Detalle = txtDetalle.Text?.Trim(),
                    Codigo = txtCodigoOferta.Text?.Trim(),
                    esOfertaPorGrupo = true,
                    TieneLimiteDeStock = cbxLimiteCumplirStock.Checked,
                    CantidadLimiteDeStock = 0.0m, //cbxLimiteCumplirStock.Checked ? Convert.ToDecimal(txtLimiteStock.Text) : null,
                    IdMarca = _marcaId,
                    IdRubro = _rubroId,
                    IdCategoria = _categoriaId,
                    GrupoNombre = $"{_marcaN}-{_rubroN}-{_categoriaN}",
                    Productos = _productosParaOfertaDTO.ToList()
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

        private void cbxEstaActiva_CheckedChanged(object sender, EventArgs e)
        {
            _ofertaActiva = cbxEstaActiva.Checked;
        }

        private void cbxEsUnProducto_CheckedChanged(object sender, EventArgs e)
        {
            _esUnSoloProducto = cbxEsUnProducto.Checked;
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
            if (_esUnSoloProducto)
            {
                _codigoOferta = $"Of-PROD_{DateTime.Now.ToString("yyyyMMddHHmmss")}_";
                txtCodigoOferta.Text = _codigoOferta;
            }
            else
            {
                _codigoOferta = $"Of-GRUPO_{DateTime.Now.ToString("yyyyMMddHHmmss")}_";
                txtCodigoOferta.Text = _codigoOferta;
            }
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
            cbxEsUnProducto.Checked = false;
            _productosParaOfertaDTO = new BindingList<ProductoDTO>();
            _productosParaQuitarDeOfertaDTO = new BindingList<ProductoDTO>();
            cantidadTotalEnOferta = 0.0m;
            cantidadTotalFueraOferta = 0.0m;
            lblCantidadProductos.Text = "0";
            lblCantidadProductosQuitados.Text = "0";
            txtCodigoOferta.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtDetalle.Text = string.Empty;
            txtPrecioDescuentoPesos.Text = string.Empty;
            txtPrecioDescuentoPorcentaje.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtRubro.Text = string.Empty;
            txtCategoria.Text = string.Empty;
            cbxDescuentoPesos.Checked = false;
            cbxDescuentoPorcentaje.Checked = false;

            ActualizarGrillas();
        }

        private void cbxDescuentoPesos_CheckedChanged(object sender, EventArgs e)
        {
            txtPrecioDescuentoPorcentaje.Text = string.Empty;
            cbxDescuentoPorcentaje.Checked = false;
            txtPrecioDescuentoPorcentaje.Enabled = !cbxDescuentoPesos.Checked;
        }

        private void cbxDescuentoPorcentaje_CheckedChanged(object sender, EventArgs e)
        {
            txtPrecioDescuentoPesos.Text = string.Empty;
            cbxDescuentoPesos.Checked = false;
            txtPrecioDescuentoPesos.Enabled = !cbxDescuentoPorcentaje.Checked;
        }
    }
}
