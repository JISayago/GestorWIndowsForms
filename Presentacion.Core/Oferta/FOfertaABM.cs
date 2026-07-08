using AccesoDatos.Entidades;
using Microsoft.IdentityModel.Tokens;
using Presentacion.Core.Articulo.Marca;
using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.Core.Producto;
using Presentacion.Core.Producto.Rubro;
using Presentacion.Core.Venta;
using Servicios.Helpers.Venta.Oferta;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Producto.DTO;
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
        private readonly IProductoServicio _productoServicio;
        private readonly FiltroBusquedaComboGrupo _filtroGrupos;
        private string _descripcionMarca;
        private string _descripcionRubro;
        private decimal cantidadTotalEnOferta = 0.0m;
        private decimal cantidadTotalFueraOferta = 0.0m;
        private long? _productoId;
        private string? _prefijoCodigoOferta;
        private string _codigoOferta;
        private List<string> _descripcionCategorias = new();
        private BindingList<ProductoOfertaDTO> _productosParaOfertaDTO;
        private BindingList<ProductoOfertaDTO> _productosParaQuitarDeOfertaDTO;
        private TipoOferta _tipoOferta = TipoOferta.Grupo;
        private DateTime FInicio = DateTime.Now;
        private DateTime FFin = DateTime.Now.AddDays(1);
        private bool _usarLimiteStock;
        private int? _limiteStock;
        private ContextMenuStrip _menuLimiteProducto;
        private bool _actualizandoDescuento;

        public FOfertaABM()
        {
            InitializeComponent();

            _filtroGrupos = new FiltroBusquedaComboGrupo();
            _productoServicio = new ProductoServicio();
        }
        private void FOfertaABM_Load(object sender, EventArgs e)
        {
            dgvProductos.AllowUserToAddRows = false;
            dgvProductosQuitados.AllowUserToAddRows = false;

            _productosParaOfertaDTO = new BindingList<ProductoOfertaDTO>();
            _productosParaQuitarDeOfertaDTO = new BindingList<ProductoOfertaDTO>();

            ActualizarGrillas();
            ResetearGrillas(dgvProductos, dgvProductosQuitados);
            dtpFechaInicio.Value = FInicio;
            dtpFechaFin.Value = FFin;
            txtLimiteStock.Enabled = false;

            _menuLimiteProducto = new ContextMenuStrip();

            _menuLimiteProducto.Items.Add(
                "Asignar límite particular",
                null,
                AsignarLimiteParticular_Click);

            dgvProductos.ContextMenuStrip = _menuLimiteProducto;
        }
        private void btnCargarGrupoMarca_Click(object sender, EventArgs e)
        {
            if (!ConfirmarCambioDesdeProductosAGrupos())
                return;

            _tipoOferta = TipoOferta.Grupo;

            var fMarca = new FMarcaConsulta();

            if (fMarca.ShowDialog() == DialogResult.OK)
            {
                _filtroGrupos.IdMarca = fMarca.marcaSeleccionada;
                txtMarca.Text = fMarca.descripcionMarca;

                _descripcionMarca = fMarca.descripcionMarca;

                ActualizarDetalleGruposFiltro();
            }
        }
        private void btnCargarGrupoRubro_Click(object sender, EventArgs e)
        {
            if (!ConfirmarCambioDesdeProductosAGrupos())
                return;

            _tipoOferta = TipoOferta.Grupo;

            var fRubro = new FRubroConsulta();

            if (fRubro.ShowDialog() == DialogResult.OK)
            {
                _filtroGrupos.IdRubro = fRubro.rubroSeleccionado;
                txtRubro.Text = fRubro.descripcionRubro;
                _descripcionRubro = fRubro.descripcionRubro;
                ActualizarDetalleGruposFiltro();
            }
        }
        private void btnCargarGrupoCategoria_Click(object sender, EventArgs e)
        {
            if (!ConfirmarCambioDesdeProductosAGrupos())
                return;

            _tipoOferta = TipoOferta.Grupo;
            var fCategoriaProducto = new Core.Producto.Categoria.FAsignacionCategoriaProducto(null);

            if (fCategoriaProducto.ShowDialog() == DialogResult.OK)
            {
                _filtroGrupos.IdCategorias =
                    fCategoriaProducto.CategoriasSeleccionadas.ToList();

                txtCategorias.Text = string.Join(",", fCategoriaProducto.descripcionCategorias);
                _descripcionCategorias = fCategoriaProducto.descripcionCategorias;
                ActualizarDetalleGruposFiltro();
            }
        }
        private void btnCargarProductosAlcanzados_Click(object sender, EventArgs e)
        {
            if (_filtroGrupos.IdMarca == null && _filtroGrupos.IdRubro == null && (_filtroGrupos.IdCategorias == null || !_filtroGrupos.IdCategorias.Any()))
            {
                MessageBox.Show("Debe seleccionar al menos un filtro para cargar productos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var productosOfertasDto = _productoServicio.ObtenerProductosPorMarcaRubroCategoriaParaOferta(_filtroGrupos);

            _productosParaOfertaDTO.Clear();

            foreach (var producto in productosOfertasDto)
            {
                _productosParaOfertaDTO.Add(producto);
            }

            _tipoOferta = TipoOferta.Grupo;

            RefrescarOferta();



        }
        private string Abreviar(string texto, int longitud = 3)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;

            return new string(texto
                .Trim()
                .ToUpper()
                .Where(char.IsLetterOrDigit)
                .Take(longitud)
                .ToArray());
        }
        private string ObtenerPrefijo()
        {
            return _tipoOferta switch
            {
                TipoOferta.Grupo => "GRP",
                TipoOferta.Producto => "PRD",
                TipoOferta.Combo => "COM",
                TipoOferta.DosPorUno => "2X1",
                _ => "OFE"
            };
        }
        private SugerenciaOfertaDTO GenerarSugerenciaOferta()
        {
            var codigo = new List<string>();
            var descripcion = new List<string>();

            codigo.Add(ObtenerPrefijo());

            switch (_tipoOferta)
            {
                case TipoOferta.Grupo:

                    descripcion.Add("Oferta por grupo.");

                    if (!string.IsNullOrWhiteSpace(_descripcionMarca))
                    {
                        codigo.Add(Abreviar(_descripcionMarca));
                        descripcion.Add($"Marca: {_descripcionMarca}");
                    }

                    if (!string.IsNullOrWhiteSpace(_descripcionRubro))
                    {
                        codigo.Add(Abreviar(_descripcionRubro));
                        descripcion.Add($"Rubro: {_descripcionRubro}");
                    }

                    if (_descripcionCategorias.Any())
                    {
                        codigo.Add($"CAT{_descripcionCategorias.Count}");

                        descripcion.Add(
                            $"Categorías: {string.Join(", ", _descripcionCategorias)}");
                    }

                    break;

                case TipoOferta.Producto:

                    var producto = _productosParaOfertaDTO.First();

                    codigo.Add(Abreviar(producto.Descripcion));

                    descripcion.Add(
                        $"Descuento sobre el producto {producto.Descripcion}.");

                    break;

                case TipoOferta.Combo:

                    descripcion.Add("Combo compuesto por:");

                    foreach (var p in _productosParaOfertaDTO)
                    {
                        codigo.Add(Abreviar(p.Descripcion));
                        descripcion.Add($"• {p.Descripcion}");
                    }

                    break;

                case TipoOferta.DosPorUno:

                    var prod2x1 = _productosParaOfertaDTO.First();

                    codigo.Add(Abreviar(prod2x1.Descripcion));

                    descripcion.Add(
                        $"Promoción 2x1 sobre {prod2x1.Descripcion}.");

                    break;
            }

            codigo.Add(DateTime.Now.ToString("ddMMyy-HHmm"));

            descripcion.Add($"Productos alcanzados: {_productosParaOfertaDTO.Count}");
            descripcion.Add($"Creación: {DateTime.Now.ToString("dd/MM/yyyy | HH:mm:ss")}");

            return new SugerenciaOfertaDTO
            {
                Codigo = string.Join("-", codigo),
                Descripcion = string.Join(Environment.NewLine, descripcion)
            };
        }
        private bool TieneCargaManualProductos()
        {
            return _productosParaOfertaDTO.Any();
        }
        private bool ConfirmarCambioDesdeProductosAGrupos()
        {
            if (!TieneCargaManualProductos())
                return true;

            var respuesta = MessageBox.Show(
                "La oferta posee productos cargados manualmente.\n\n" +
                "Si continúa, se eliminarán los productos actualmente agregados y se reemplazarán por la carga por grupo.\n\n" +
                "¿Desea continuar?",
                "Confirmar acción",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (respuesta != DialogResult.Yes)
                return false;

            LimpiarDatosGrilla();
            LimpiarCodigoYDescripcion();

            return true;
        }
        private void LimpiarCargaPorGrupo()
        {
            // Limpiar filtros
            _filtroGrupos.IdMarca = null;
            _filtroGrupos.IdRubro = null;
            _filtroGrupos.IdCategorias.Clear();

            // Limpiar descripciones
            _descripcionMarca = string.Empty;
            _descripcionRubro = string.Empty;
            _descripcionCategorias.Clear();

            // Limpiar controles
            txtMarca.Clear();
            txtRubro.Clear();
            txtCategorias.Clear();

            ActualizarDetalleGruposFiltro();
        }
        private void LimpiarDatosGrilla()
        {
            // Limpiar productos
            _productosParaOfertaDTO.Clear();
            _productosParaQuitarDeOfertaDTO.Clear();

            ActualizarGrillas();

            cantidadTotalEnOferta = 0;
            cantidadTotalFueraOferta = 0;

            lblNumeroProductoAfectados.Text = "0";
            lblNumeroProductoQuitados.Text = "0";
        }
        private void LimpiarCodigoYDescripcion()
        {
            txtCodigo.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }
        private void btnCargarProducto_Click(object sender, EventArgs e)
        {
            if (_filtroGrupos.IdRubro.HasValue
                || _filtroGrupos.IdMarca.HasValue
                || _filtroGrupos.IdCategorias.Any())
            {
                var respuesta = MessageBox.Show(
                    "La oferta posee productos cargados por grupo.\n\n" +
                    "Si continúa, se eliminarán los filtros de Marca, Rubro y Categorías, " +
                    "junto con los productos actualmente alcanzados.\n\n" +
                    "¿Desea continuar con la carga manual del producto?",
                    "Confirmar acción",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (respuesta == DialogResult.No)
                    return;

                LimpiarCargaPorGrupo();
                LimpiarDatosGrilla();
                LimpiarCodigoYDescripcion();

                _tipoOferta = TipoOferta.Producto;
            }

            var fProductos = new FProductoConsulta(true);

            if (fProductos.ShowDialog() != DialogResult.OK ||
                !fProductos.productoSeleccionado.HasValue)
                return;

            var producto = _productoServicio.ObtenerProductoPorId(
                fProductos.productoSeleccionado.Value);

            if (producto == null)
                return;

            var fCantidad = new FCantidadItem();

            if (fCantidad.ShowDialog() != DialogResult.OK ||
                fCantidad.cantidad <= 0)
                return;

            var cantidad = fCantidad.cantidad;

            var productoOfertaDto = new ProductoOfertaDTO
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
                MarcaNombre = producto.MarcaNombre,
                RubroNombre = producto.RubroNombre,
                CategoriaIds = producto.CategoriaIds,
                CantidadItemEnOferta = cantidad
            };
            if(_productosParaOfertaDTO.Count() < 2)
            {
                MessageBox.Show("Recuerde que se debe cargar al menos 2 unidades del mismo producto o un producto extra para poder crear la oferta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (_productosParaOfertaDTO.Count() == 1)
                {
                   if (_productosParaOfertaDTO.First().CantidadItemEnOferta < 2)
                   {
                       MessageBox.Show("Recuerde que se debe cargar al menos 2 unidades del mismo producto o un producto extra para poder crear la oferta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        _tipoOferta = TipoOferta.DosPorUno;
                    }
                }
                else
                {
                    _tipoOferta = TipoOferta.Combo;
                }
            }

                var existente = _productosParaOfertaDTO
                    .FirstOrDefault(x => x.ProductoId == productoOfertaDto.ProductoId);

            if (existente != null)
            {
                var respuesta = MessageBox.Show(
                    $"El producto '{productoOfertaDto.Descripcion}' ya se encuentra en la oferta.\n\n" +
                    "¿Desea sumar la cantidad ingresada a la existente?",
                    "Producto existente",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.No)
                    return;

                existente.CantidadItemEnOferta += cantidad;
            }
            else
            {
                var productosDistintos = _productosParaOfertaDTO
                    .Select(x => x.ProductoId)
                    .Distinct()
                    .Count();

                if (productosDistintos >= 2)
                {
                    var respuesta = MessageBox.Show(
                        "La oferta ya posee 2 productos distintos.\n\n" +
                        "¿Está seguro que desea agregar un tercero?",
                        "Advertencia",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (respuesta == DialogResult.No)
                        return;
                }
            }

            _productosParaOfertaDTO.Add(productoOfertaDto);
            ActualizarTipoOfertaPorProductos();

            cantidadTotalEnOferta = _productosParaOfertaDTO.Count;
            cantidadTotalFueraOferta = _productosParaQuitarDeOfertaDTO.Count;

            lblNumeroProductoAfectados.Text = cantidadTotalEnOferta.ToString();
            lblNumeroProductoQuitados.Text = cantidadTotalFueraOferta.ToString();

            RefrescarOferta();
        }
        private void ActualizarTipoOfertaPorProductos()
        {
            // Si la oferta se arma por grupo nunca se cambia el tipo.
            if (_filtroGrupos.IdMarca.HasValue
                || _filtroGrupos.IdRubro.HasValue
                || _filtroGrupos.IdCategorias.Any())
            {
                _tipoOferta = TipoOferta.Grupo;
                return;
            }

            if (!_productosParaOfertaDTO.Any())
            {
                _tipoOferta = TipoOferta.Producto;
                return;
            }

            var productosDistintos = _productosParaOfertaDTO
                .Select(x => x.ProductoId)
                .Distinct()
                .Count();

            var cantidadTotal = _productosParaOfertaDTO
                .Sum(x => x.CantidadItemEnOferta ?? 1);
           
        }
        private void ActualizarDetalleGruposFiltro()
        {
            var detalles = new List<string>();

            if (!string.IsNullOrWhiteSpace(_descripcionMarca))
            {
                detalles.Add($"Marca: {_descripcionMarca}");
            }

            if (!string.IsNullOrWhiteSpace(_descripcionRubro))
            {
                detalles.Add($"Rubro: {_descripcionRubro}");
            }

            if (_descripcionCategorias.Any())
            {
                detalles.Add($"Categorías: {string.Join(", ", _descripcionCategorias)}");
            }

            lblDetalleGruposFiltro.Text = detalles.Any()
                ? $"Grupos de Filtro: {string.Join(" | ", detalles)}"
                : "Sin filtros seleccionados";
        }
        public virtual void IniciarGrilla(DataGridView grilla)
        {
            for (int i = 0; i < grilla.ColumnCount; i++)
            {
                grilla.Columns[i].Visible = false;
            }
        }
        private void ResetearGrillas(DataGridView grillaProductosOferta, DataGridView grillaProductosQuitados)
        {
            foreach (DataGridViewColumn col in grillaProductosOferta.Columns)
            {
                col.Visible = false;
            }

            foreach (DataGridViewColumn col in grillaProductosQuitados.Columns)
            {
                col.Visible = false;
            }
            //dentro de la oferta
            grillaProductosOferta.Columns["ProductoId"].Visible = false;
            grillaProductosOferta.Columns["ProductoId"].DisplayIndex = 0;

            grillaProductosOferta.Columns["Descripcion"].Visible = true;
            grillaProductosOferta.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grillaProductosOferta.Columns["Descripcion"].DisplayIndex = 1;
            grillaProductosOferta.Columns["Descripcion"].HeaderText = "Descripción";

            grillaProductosOferta.Columns["Stock"].Visible = true;
            grillaProductosOferta.Columns["Stock"].Width = 100;
            grillaProductosOferta.Columns["Stock"].DisplayIndex = 2;
            grillaProductosOferta.Columns["Stock"].HeaderText = "Cantidad disponible";

            grillaProductosOferta.Columns["Codigo"].Visible = true;
            grillaProductosOferta.Columns["Codigo"].Width = 100;
            grillaProductosOferta.Columns["Codigo"].DisplayIndex = 2;
            grillaProductosOferta.Columns["Codigo"].HeaderText = "Código";

            grillaProductosOferta.Columns["PrecioVenta"].Visible = true;
            grillaProductosOferta.Columns["PrecioVenta"].Width = 100;
            grillaProductosOferta.Columns["PrecioVenta"].DisplayIndex = 5;
            grillaProductosOferta.Columns["PrecioVenta"].HeaderText = "Precio Venta";

            grillaProductosOferta.Columns["PrecioCosto"].Visible = true;
            grillaProductosOferta.Columns["PrecioCosto"].Width = 100;
            grillaProductosOferta.Columns["PrecioCosto"].DisplayIndex = 5;
            grillaProductosOferta.Columns["PrecioCosto"].HeaderText = "Precio Costo";

            grillaProductosOferta.Columns["CantidadItemEnOferta"].Visible = true;
            grillaProductosOferta.Columns["CantidadItemEnOferta"].Width = 80;
            grillaProductosOferta.Columns["CantidadItemEnOferta"].DisplayIndex = 6;
            grillaProductosOferta.Columns["CantidadItemEnOferta"].HeaderText = "Cantidad en oferta";

            grillaProductosOferta.Columns["LimiteEnOferta"].Visible = true;
            grillaProductosOferta.Columns["LimiteEnOferta"].Width = 100;
            grillaProductosOferta.Columns["LimiteEnOferta"].DisplayIndex = 7;
            grillaProductosOferta.Columns["LimiteEnOferta"].HeaderText = "Cantidad Limite";

            grillaProductosOferta.Columns["ConLimiteEnOferta"].Visible = false;

            grillaProductosOferta.Columns["DescripcionLimiteEnOferta"].Visible = true;
            grillaProductosOferta.Columns["DescripcionLimiteEnOferta"].Width = 150;
            grillaProductosOferta.Columns["DescripcionLimiteEnOferta"].DisplayIndex = 8;
            grillaProductosOferta.Columns["DescripcionLimiteEnOferta"].HeaderText = "Límite en Oferta";

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
            ResetearGrillas(dgvProductos, dgvProductosQuitados);
        }
        private ProductoOfertaDTO ObtenerProducto(DataGridView grillaProductosOferta)
        {
            if (grillaProductosOferta.CurrentRow != null && grillaProductosOferta.CurrentRow.DataBoundItem is ProductoOfertaDTO producto)
                return producto;

            return null;
        }
        private void btnQuitarProducto_Click_1(object sender, EventArgs e)
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

            RefrescarOferta();

            cantidadTotalEnOferta = _productosParaOfertaDTO.Count();
            cantidadTotalFueraOferta = _productosParaQuitarDeOfertaDTO.Count();
            lblNumeroProductoAfectados.Text = cantidadTotalEnOferta.ToString();
            lblNumeroProductoQuitados.Text = cantidadTotalFueraOferta.ToString();
            // Forzar refresco si fuera necesario (normalmente no hace falta)
            var cm = (CurrencyManager)BindingContext[_productosParaOfertaDTO];
            var cm2 = (CurrencyManager)BindingContext[_productosParaQuitarDeOfertaDTO];
            cm.Refresh();
            cm2.Refresh();
            ActualizarIdentidadOferta();
        }
        private void btnDevolverAOferta_Click_1(object sender, EventArgs e)
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

            RefrescarOferta();

            cantidadTotalEnOferta = _productosParaOfertaDTO.Count();
            cantidadTotalFueraOferta = _productosParaQuitarDeOfertaDTO.Count();

            lblNumeroProductoAfectados.Text = cantidadTotalEnOferta.ToString();
            lblNumeroProductoQuitados.Text = cantidadTotalFueraOferta.ToString();
            // Forzar refresco si fuera necesario (normalmente no hace falta)
            var cm = (CurrencyManager)BindingContext[_productosParaOfertaDTO];
            var cm2 = (CurrencyManager)BindingContext[_productosParaQuitarDeOfertaDTO];
            cm.Refresh();
            cm2.Refresh();
            ActualizarIdentidadOferta();
        }
        private void ActualizarIdentidadOferta()
        {
            // Oferta por grupo
            if (_tipoOferta == TipoOferta.Grupo)
            {
                if (!_productosParaOfertaDTO.Any())
                {
                    LimpiarCodigoYDescripcion();
                    return;
                }

                var sugerenciaGrupo = GenerarSugerenciaOferta();

                txtCodigo.Text = sugerenciaGrupo.Codigo;
                txtDescripcion.Text = sugerenciaGrupo.Descripcion;

                return;
            }

            // Oferta manual
            if (!_productosParaOfertaDTO.Any())
            {
                LimpiarCodigoYDescripcion();
                return;
            }

            ActualizarTipoOfertaPorProductos();

            var sugerencia = GenerarSugerenciaOferta();

            txtCodigo.Text = sugerencia.Codigo;
            txtDescripcion.Text = sugerencia.Descripcion;
        }
        private decimal CalcularTotalReal()
        {
            return _productosParaOfertaDTO.Sum(x =>
                x.PrecioCosto * ObtenerCantidadParaCalculo(x));
        }
        private decimal CalcularTotalVenta()
        {
            return _productosParaOfertaDTO.Sum(x =>
                x.PrecioVenta * ObtenerCantidadParaCalculo(x));
        }
        private decimal CalcularTotalOferta()
        {
            var totalVenta = CalcularTotalVenta();

            decimal porcentaje;

            if (decimal.TryParse(txtPorcentajeDescuento.Text, out porcentaje)
                && porcentaje > 0)
            {
                return totalVenta - (totalVenta * porcentaje / 100m);
            }

            decimal precioFinal;

            if (decimal.TryParse(txtPrecioFinal.Text, out precioFinal)
                && precioFinal > 0)
            {
                return precioFinal;
            }

            return totalVenta;
        }
        private decimal CalcularResultadoPromocion()
        {
            return CalcularTotalOferta() - CalcularTotalReal();
        }
        private decimal CalcularDescuentoOtorgado()
        {
            return CalcularTotalVenta() - CalcularTotalOferta();
        }
        private void ActualizarResumenEconomico()
        {
            var totalReal = CalcularTotalReal();
            var totalVenta = CalcularTotalVenta();

            lblTotalAcumuladoReal.Text =
                $"Monto acumulado de precio Costo: {totalReal:C2}";

            lblTotalAcumuladoVenta.Text =
                $"Monto acumulado de precio Venta: {totalVenta:C2}";

            bool tienePorcentaje =
                decimal.TryParse(txtPorcentajeDescuento.Text, out var porcentaje)
                && porcentaje > 0;

            bool tienePrecioFinal =
                decimal.TryParse(txtPrecioFinal.Text, out var precioFinal)
                && precioFinal > 0;

            // OFERTA POR GRUPO
            if (_tipoOferta == TipoOferta.Grupo)
            {
                if (tienePorcentaje)
                {
                    lblTotalFinal.Text =
                        $"Todos los productos tendrán {porcentaje}% de descuento";
                }
                else if (tienePrecioFinal)
                {
                    lblTotalFinal.Text =
                        $"Descuento fijo configurado: {precioFinal:C2}";
                }
                else
                {
                    lblTotalFinal.Text =
                        "Sin descuento configurado";
                }

                lblTotalPerdidoConREspectoAlVentayREal.Text =
                    "Descuento aplicado sobre todos los productos alcanzados";

                return;
            }

            // OFERTA MANUAL (Producto, Combo, 2x1)
            lblTotalFinal.Text =
                $"Total final de oferta: {CalcularTotalOfertaManual():C2}";

            if (tienePorcentaje || tienePrecioFinal)
            {
                lblTotalPerdidoConREspectoAlVentayREal.Text =
                    $"Monto perdido con respecto al precio Venta y Costo: {CalcularResultadoPromocion():C2}";
            }
            else
            {
                lblTotalPerdidoConREspectoAlVentayREal.Text =
                    "Sin descuento configurado";
            }
        }
        private void RefrescarOferta()
        {
            ActualizarGrillas();

            cantidadTotalEnOferta = _productosParaOfertaDTO.Count;
            cantidadTotalFueraOferta = _productosParaQuitarDeOfertaDTO.Count;

            lblNumeroProductoAfectados.Text = cantidadTotalEnOferta.ToString();
            lblNumeroProductoQuitados.Text = cantidadTotalFueraOferta.ToString();

            ActualizarIdentidadOferta();
            ActualizarResumenEconomico();
        }
        private void txtPorcentajeDescuento_TextChanged(object sender, EventArgs e)
        {
            if (_actualizandoDescuento)
                return;

            if (!string.IsNullOrWhiteSpace(txtPorcentajeDescuento.Text))
            {
                if (!decimal.TryParse(txtPorcentajeDescuento.Text, out var porcentaje))
                {
                    MessageBox.Show(
                        "El porcentaje de descuento debe ser un valor numérico.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    _actualizandoDescuento = true;
                    txtPorcentajeDescuento.Clear();
                    _actualizandoDescuento = false;
                    return;
                }

                if (porcentaje < 1 || porcentaje > 100)
                {
                    MessageBox.Show(
                        "El porcentaje de descuento debe estar entre 1 y 100.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    _actualizandoDescuento = true;
                    txtPorcentajeDescuento.Clear();
                    _actualizandoDescuento = false;
                    return;
                }

                _actualizandoDescuento = true;
                txtPrecioFinal.Clear();
                _actualizandoDescuento = false;
            }

            ActualizarResumenEconomico();
        }
        private void txtPrecioFinal_TextChanged(object sender, EventArgs e)
        {
            if (_actualizandoDescuento)
                return;

            if (!string.IsNullOrWhiteSpace(txtPrecioFinal.Text))
            {
                _actualizandoDescuento = true;
                txtPorcentajeDescuento.Clear();
                _actualizandoDescuento = false;
            }

            ActualizarResumenEconomico();
        }
        private decimal ObtenerCantidadParaCalculo(ProductoOfertaDTO producto)
        {
            if (_tipoOferta == TipoOferta.Grupo)
            {
                return 1;
            }

            return (decimal)producto.CantidadItemEnOferta <= 0
                ? 1
                : (decimal)producto.CantidadItemEnOferta;
        }
        private decimal ObtenerCantidadPagada(ProductoOfertaDTO producto)
        {
            if (_tipoOferta == TipoOferta.DosPorUno)
            {
                return Math.Ceiling((decimal)producto.CantidadItemEnOferta / 2m);
            }

            return ObtenerCantidadParaCalculo(producto);
        }
        private void dtpFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            FInicio = dtpFechaInicio.Value.Date;

            // Mínimo 1 día de duración
            if (dtpFechaFin.Value.Date <= FInicio)
            {
                dtpFechaFin.Value = FInicio.AddDays(1);
                FFin = dtpFechaFin.Value.Date;
            }
        }
        private void dtpFechaFin_ValueChanged(object sender, EventArgs e)
        {
            FFin = dtpFechaFin.Value.Date;

            // No puede ser igual o menor que la fecha de inicio
            if (FFin <= dtpFechaInicio.Value.Date)
            {
                MessageBox.Show(
                    "La fecha de fin debe ser al menos 1 día posterior a la fecha de inicio.",
                    "Fecha inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                dtpFechaFin.Value = dtpFechaInicio.Value.Date.AddDays(1);
                FFin = dtpFechaFin.Value.Date;
            }
        }
        private void cbxLimiteCumplirStock_CheckedChanged(object sender, EventArgs e)
        {
            _usarLimiteStock = cbxLimiteCumplirStock.Checked;

            txtLimiteStock.Enabled = _usarLimiteStock;

            if (!_usarLimiteStock)
            {
                txtLimiteStock.Text = string.Empty;
                _limiteStock = null;
            }
        }
        private void txtLimiteStock_TextChanged(object sender, EventArgs e)
        {
            if (!_usarLimiteStock)
            {
                _limiteStock = null;
                return;
            }

            if (int.TryParse(txtLimiteStock.Text, out var limite) && limite > 0)
            {
                _limiteStock = limite;
            }
            else
            {
                _limiteStock = null;
            }
        }
        private void txtLimiteStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void btnControlStockDisponible_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtLimiteStock.Text, out var limite) || limite <= 0)
            {
                MessageBox.Show("Ingrese un límite válido.");
                return;
            }

            var productosAjustados = new List<string>();

            foreach (var producto in _productosParaOfertaDTO)
            {
                var limiteFinal = limite;

                if (producto.Stock < limite)
                {
                    limiteFinal = producto.Stock;
                    productosAjustados.Add(producto.Descripcion);
                }

                producto.LimiteEnOferta = limiteFinal;
                producto.ConLimiteEnOferta = true;
            }

            var cm = (CurrencyManager)BindingContext[_productosParaOfertaDTO];
            cm.Refresh();

            dgvProductos.Refresh();

            if (productosAjustados.Any())
            {
                MessageBox.Show(
                    "Algunos productos no tenían stock suficiente y se asignó automáticamente el máximo disponible.\n\n" +
                    string.Join("\n", productosAjustados),
                    "Límites ajustados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        private void AsignarLimiteParticular_Click(object sender, EventArgs e)
        {
            var producto = ObtenerProducto(dgvProductos);

            if (producto == null)
                return;

            var fCantidad = new FCantidadItem();

            if (fCantidad.ShowDialog() != DialogResult.OK)
                return;

            var limite = fCantidad.cantidad;

            if (limite > producto.Stock)
            {
                MessageBox.Show(
                    $"El producto posee solamente {producto.Stock} unidades disponibles.",
                    "Stock insuficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                limite = producto.Stock;
            }

            producto.LimiteEnOferta = limite;
            producto.ConLimiteEnOferta = true;
            dgvProductos.Refresh();
        }
        private void dgvProductos_CellMouseDown_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvProductos.ClearSelection();
                dgvProductos.Rows[e.RowIndex].Selected = true;
                dgvProductos.CurrentCell =
     dgvProductos.Rows[e.RowIndex].Cells["Descripcion"];
            }
        }
        private decimal CalcularTotalOfertaManual()
        {
            if (_tipoOferta == TipoOferta.DosPorUno)
            {
                return _productosParaOfertaDTO.Sum(x =>
                    x.PrecioVenta * ObtenerCantidadPagada(x));
            }

            var totalVenta = CalcularTotalVenta();

            if (decimal.TryParse(txtPorcentajeDescuento.Text, out var porcentaje)
                && porcentaje > 0)
            {
                return totalVenta - (totalVenta * porcentaje / 100m);
            }

            if (decimal.TryParse(txtPrecioFinal.Text, out var precioFinal)
                && precioFinal > 0)
            {
                return precioFinal;
            }

            return totalVenta;
        }
        private bool ValidarDescuento()
        {
            bool tienePorcentaje =
                decimal.TryParse(txtPorcentajeDescuento.Text, out var porcentaje)
                && porcentaje > 0;

            bool tienePrecioFinal =
                decimal.TryParse(txtPrecioFinal.Text, out var precioFinal)
                && precioFinal > 0;

            if (tienePorcentaje && tienePrecioFinal)
            {
                MessageBox.Show(
                    "La oferta no puede tener un porcentaje de descuento y un precio final fijo al mismo tiempo.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }
        private void btnCancelarYSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnCrearOferta_Click(object sender, EventArgs e)
        {
            CrearOferta();
        }
        private void ValidarTxtTipoYVacios()
        {
            if (!_productosParaOfertaDTO.Any())
            {
                MessageBox.Show(
                    "Debe agregar al menos un producto a la oferta.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
            if (string.IsNullOrWhiteSpace(txtPorcentajeDescuento.Text) &&
     string.IsNullOrWhiteSpace(txtPrecioFinal.Text))
            {
                MessageBox.Show(
                    "Debe ingresar un porcentaje de descuento o un precio final.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!string.IsNullOrWhiteSpace(txtPorcentajeDescuento.Text) &&
                !decimal.TryParse(txtPorcentajeDescuento.Text, out _))
            {
                MessageBox.Show(
                    "El porcentaje de descuento debe ser un valor numérico.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPorcentajeDescuento.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtPrecioFinal.Text) &&
                !decimal.TryParse(txtPrecioFinal.Text, out _))
            {
                MessageBox.Show(
                    "El precio final debe ser un valor numérico.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPrecioFinal.Focus();
                return;
            }
        }
        private void ValidarLimitacionDeStockEnProducto()
        {
            if (!_productosParaOfertaDTO.Any())
                return;

            var productosConLimiteAutomatico = new List<string>();

            // Hay un límite global configurado
            if (_usarLimiteStock && _limiteStock.HasValue)
            {
                foreach (var producto in _productosParaOfertaDTO)
                {
                    var limiteFinal = Math.Min(producto.Stock, _limiteStock.Value);

                    if (producto.Stock < _limiteStock.Value)
                    {
                        productosConLimiteAutomatico.Add(
                            $"{producto.Descripcion} (Stock disponible: {producto.Stock})");
                    }

                    producto.LimiteEnOferta = limiteFinal;
                    producto.ConLimiteEnOferta = true;
                }
            }
            else
            {
                // No hay límite global: asegurar que cada producto tenga uno.
                foreach (var producto in _productosParaOfertaDTO)
                {
                    if (!producto.ConLimiteEnOferta ||
                        !producto.LimiteEnOferta.HasValue ||
                        producto.LimiteEnOferta <= 0)
                    {
                        producto.LimiteEnOferta = producto.Stock;
                        producto.ConLimiteEnOferta = true;

                        productosConLimiteAutomatico.Add(
                            $"{producto.Descripcion} (Stock disponible: {producto.Stock})");
                    }
                    else if (producto.LimiteEnOferta > producto.Stock)
                    {
                        producto.LimiteEnOferta = producto.Stock;

                        productosConLimiteAutomatico.Add(
                            $"{producto.Descripcion} (Stock disponible: {producto.Stock})");
                    }
                }
            }

            dgvProductos.Refresh();

            var cm = (CurrencyManager)BindingContext[_productosParaOfertaDTO];
            cm.Refresh();

            if (productosConLimiteAutomatico.Any())
            {
                MessageBox.Show(
                    "Algunos productos no tenían un límite configurado o el límite superaba el stock disponible.\n\n" +
                    "Se utilizará automáticamente el stock máximo disponible para dichos productos:\n\n" +
                    string.Join(Environment.NewLine, productosConLimiteAutomatico) +
                    "\n\nSi desea limitar una cantidad menor para alguno de ellos, asígnela manualmente desde la opción 'Asignar límite particular'.",
                    "Límites de stock ajustados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        private void CrearOferta()
        {

            ValidarTxtTipoYVacios();

            ActualizarTipoOfertaPorProductos();

            if (!ValidarOfertaAntesDeGuardar())
                return;

            if (!ValidarDescuento())
                return;

            decimal? porcentajeDescuento = null;
            decimal? precioFinal = null;

            if (decimal.TryParse(txtPorcentajeDescuento.Text, out var porcentaje))
                porcentajeDescuento = porcentaje;

            if (decimal.TryParse(txtPrecioFinal.Text, out var precio))
                precioFinal = precio;

            ValidarLimitacionDeStockEnProducto();
            var ofertaDto = new OfertaDTO
            {
                Descripcion = txtDescripcion.Text.Trim(),
                Codigo = txtCodigo.Text.Trim(),
                FechaInicio = dtpFechaInicio.Value,
                FechaFin = dtpFechaFin.Value,
                EstaActiva = true,
                TipoOferta = (int)_tipoOferta,
                PorcentajeDescuento = porcentajeDescuento,
                PrecioFinal = precioFinal,

                Productos = _productosParaOfertaDTO
                    .Select(x => new ProductosEnOfertaDescuentosDTO
                    {
                        ProductoId = x.ProductoId,
                        CantidadRequerida = x.CantidadItemEnOferta ?? 1,
                        PrecioCostoBase = x.PrecioCosto,
                        PrecioVentaBase = x.PrecioVenta,
                        PrecioOfertaBase = CalcularPrecioOfertaProducto(
                            x,
                            porcentajeDescuento,
                            precioFinal),
                        LimiteVentaProducto = x.ConLimiteEnOferta
                            ? x.LimiteEnOferta
                            : null
                    })
                    .ToList()
            };
            if (_productosParaOfertaDTO.Count() == 1 && _productosParaOfertaDTO.First().CantidadItemEnOferta < 2)
            {
                MessageBox.Show("Recuerde que se debe cargar al menos 2 unidades del mismo producto o un producto extra para poder crear la oferta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var servicio = new OfertaServicio();

            var resultado = servicio.Insertar(ofertaDto);

            if (resultado.Exitoso)
            {
                MessageBox.Show(
                    resultado.Mensaje,
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Close();
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
        private bool ValidarOfertaAntesDeGuardar()
        {
            // Las ofertas por grupo o producto simple siempre son válidas.
            if (_tipoOferta == TipoOferta.Producto ||
                _tipoOferta == TipoOferta.Grupo)
                return true;

            var productosDistintos = _productosParaOfertaDTO
                .Select(x => x.ProductoId)
                .Distinct()
                .Count();

            var cantidadTotal = _productosParaOfertaDTO
                .Sum(x => x.CantidadItemEnOferta ?? 1);

            // 2x1
            if (_tipoOferta == TipoOferta.DosPorUno)
            {
                if (productosDistintos != 1 || cantidadTotal < 2)
                {
                    MessageBox.Show(
                        "Un 2x1 debe contener un único producto con una cantidad mínima de 2 unidades.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return false;
                }

                return true;
            }

            // Combo
            if (_tipoOferta == TipoOferta.Combo)
            {
                if (productosDistintos < 2)
                {
                    MessageBox.Show(
                        "Un combo debe estar compuesto por al menos dos productos distintos.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return false;
                }

                return true;
            }

            return true;
        }
        private decimal? CalcularPrecioOfertaProducto(ProductoOfertaDTO producto,decimal? porcentaje,decimal? precioFinal)
{
    if (_tipoOferta == TipoOferta.Grupo)
        return null;

    if (_tipoOferta == TipoOferta.DosPorUno)
    {
        return producto.PrecioVenta / 2m;
    }

    if (porcentaje.HasValue)
    {
        return producto.PrecioVenta -
               (producto.PrecioVenta * porcentaje.Value / 100m);
    }

    if (precioFinal.HasValue)
    {
        return precioFinal.Value;
    }

    return producto.PrecioVenta;
}
        private void btnLimpiarCampos_Click(object sender, EventArgs e)
        {
            var respuesta = MessageBox.Show(
                "Se perderán todos los datos cargados.\n\n¿Desea continuar?",
                "Limpiar formulario",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            LimpiarFormulario();
        }
        private void LimpiarFormulario()
        {
            _tipoOferta = TipoOferta.Grupo;

            _productoId = null;
            _prefijoCodigoOferta = null;
            _codigoOferta = string.Empty;

            _descripcionMarca = string.Empty;
            _descripcionRubro = string.Empty;
            _descripcionCategorias.Clear();

            cantidadTotalEnOferta = 0;
            cantidadTotalFueraOferta = 0;

            _actualizandoDescuento = true;
            txtPorcentajeDescuento.Clear();
            txtPrecioFinal.Clear();
            _actualizandoDescuento = false;

            txtCategorias.Text = _descripcionCategorias.ToString();
            txtMarca.Text = _descripcionMarca;
            txtRubro.Text = _descripcionRubro;

            LimpiarCargaPorGrupo();
            LimpiarDatosGrilla();
            LimpiarCodigoYDescripcion();

            ResetearFechas();
            ResetearStock();
            ResetearResumen();

            RefrescarOferta();
        }
        private void ResetearFechas()
        {
            FInicio = DateTime.Now;
            FFin = DateTime.Now.AddDays(1);

            dtpFechaInicio.Value = FInicio;
            dtpFechaFin.Value = FFin;
        }

        private void ResetearStock()
        {
            _usarLimiteStock = false;
            _limiteStock = null;

            cbxLimiteCumplirStock.Checked = false;
            txtLimiteStock.Clear();
            txtLimiteStock.Enabled = false;
        }

        private void ResetearResumen()
        {
            lblNumeroProductoAfectados.Text = "0";
            lblNumeroProductoQuitados.Text = "0";

            lblTotalAcumuladoReal.Text = "Monto acumulado de precio Costo: $0,00";
            lblTotalAcumuladoVenta.Text = "Monto acumulado de precio Venta: $0,00";
            lblTotalFinal.Text = "Sin descuento configurado";
            lblTotalPerdidoConREspectoAlVentayREal.Text = "Sin descuento configurado";
        }
    }
}
