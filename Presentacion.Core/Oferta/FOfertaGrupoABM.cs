using AccesoDatos.Entidades;
using Microsoft.IdentityModel.Tokens;
using Presentacion.Core.Articulo.Marca;
using Presentacion.Core.Categoria;
using Presentacion.Core.Producto.Rubro;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Articulo.Categoria;
using Servicios.LogicaNegocio.Articulo.Marca;
using Servicios.LogicaNegocio.Empleado.Rol.DTO;
using Servicios.LogicaNegocio.Producto;
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

        private BindingList<ProductosEnOfertaDescuentosDTO> _productosOfertaDTO;
        private BindingList<ProductosEnOfertaDescuentosDTO> _productosOfertaDTOQuitarDeOferta;

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
            _productosOfertaDTO = new BindingList<ProductosEnOfertaDescuentosDTO>();
            _productosOfertaDTOQuitarDeOferta = new BindingList<ProductosEnOfertaDescuentosDTO>();
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now.AddDays(1);
            _fechaInicio = dtpFechaInicio.Value;
            _fechaFin = dtpFechaFin.Value;
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
            /*if (!_esCategoria || !_esRubro || !_esMarca) { 
                MessageBox.Show(Text, "Debe seleccionar al menos un grupo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtMarca.Text)|| string.IsNullOrEmpty(txtRubro.Text)|| string.IsNullOrEmpty(txtCategoria.Text))
            {
                MessageBox.Show(Text, "Debe cargar al menos un grupo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }*/

            MessageBox.Show($"marcaid: {_marcaId}");
            MessageBox.Show($"rubroid: {_rubroId}");
            MessageBox.Show($"categoriaid: {_categoriaId}");
            var productos = _productoServicio.ObtenerProductosPorMarcaRubroCategoriaParaOferta(_marcaId, _rubroId, _categoriaId);
            _productosOfertaDTO = new BindingList<ProductosEnOfertaDescuentosDTO>(productos.ToList());
            dgvProductos.DataSource = _productosOfertaDTO;

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
            //deberia crear un itemOferta
            for (int i = 0; i < grilaProductosOferta.ColumnCount; i++)
            {
                grilaProductosOferta.Columns[i].Visible = false;
            }
            grilaProductosOferta.Columns["ProductoOfertaId"].Visible = false;
            grilaProductosOferta.Columns["ProductoOfertaId"].DisplayIndex = 0;

            grilaProductosOferta.Columns["Descripcion"].Visible = true;
            grilaProductosOferta.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilaProductosOferta.Columns["Descripcion"].DisplayIndex = 1;

            grilaProductosOferta.Columns["Cantidad"].Visible = true;
            grilaProductosOferta.Columns["Cantidad"].Width = 100;
            grilaProductosOferta.Columns["Cantidad"].DisplayIndex = 2;
            grilaProductosOferta.Columns["Codigo"].Visible = true;
            grilaProductosOferta.Columns["Codigo"].Width = 100;
            grilaProductosOferta.Columns["Codigo"].DisplayIndex = 3;

            /*grilla.Columns["Cantidad"].Visible = true;
            grilla.Columns["Cantidad"].Width = 100;
            grilla.Columns["Cantidad"].DisplayIndex = 4;*/

            grilaProductosOferta.Columns["PrecioVenta"].Visible = true;
            grilaProductosOferta.Columns["PrecioVenta"].Width = 100;
            grilaProductosOferta.Columns["PrecioVenta"].DisplayIndex = 5;
            grilaProductosOferta.Columns["PrecioCosto"].Visible = true;
            grilaProductosOferta.Columns["PrecioCosto"].Width = 100;
            grilaProductosOferta.Columns["PrecioCosto"].DisplayIndex = 5;
            if (_productoId.HasValue)
            {
                for (int i = 0; i < grillaProductosQuitados.ColumnCount; i++)
                {
                    grillaProductosQuitados.Columns[i].Visible = false;
                }
                grillaProductosQuitados.Columns["ProductoOfertaId"].Visible = false;
                grillaProductosQuitados.Columns["ProductoOfertaId"].DisplayIndex = 0;

                grillaProductosQuitados.Columns["Descripcion"].Visible = true;
                grillaProductosQuitados.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grillaProductosQuitados.Columns["Descripcion"].DisplayIndex = 1;

                grillaProductosQuitados.Columns["Cantidad"].Visible = true;
                grillaProductosQuitados.Columns["Cantidad"].Width = 100;
                grillaProductosQuitados.Columns["Cantidad"].DisplayIndex = 2;
                grillaProductosQuitados.Columns["Codigo"].Visible = true;
                grillaProductosQuitados.Columns["Codigo"].Width = 100;
                grillaProductosQuitados.Columns["Codigo"].DisplayIndex = 3;

                /*grilla.Columns["Cantidad"].Visible = true;
                grilla.Columns["Cantidad"].Width = 100;
                grilla.Columns["Cantidad"].DisplayIndex = 4;*/

                grillaProductosQuitados.Columns["PrecioVenta"].Visible = true;
                grillaProductosQuitados.Columns["PrecioVenta"].Width = 100;
                grillaProductosQuitados.Columns["PrecioVenta"].DisplayIndex = 5;
                grillaProductosQuitados.Columns["PrecioCosto"].Visible = true;
                grillaProductosQuitados.Columns["PrecioCosto"].Width = 100;
                grillaProductosQuitados.Columns["PrecioCosto"].DisplayIndex = 5;
            }

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

            if (!dgvProductos.Columns.Contains("ProductoOfertaId")) return;

            var val = dgvProductos.CurrentRow.Cells["ProductoOfertaId"].Value;
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

            if (!dgvProductos.Columns.Contains("ProductoOfertaId"))
                return;

            var celda = fila.Cells["ProductoOfertaId"];
            if (celda?.Value == null || celda.Value == DBNull.Value)
                return;

            _productoId = (long)celda.Value;
        }
        private void ActualizarGrillas()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = _productosOfertaDTO;

            dgvProductosQuitados.DataSource = null;
            dgvProductosQuitados.DataSource = _productosOfertaDTOQuitarDeOferta;
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
            var productoEnLista = _productosOfertaDTO
                .FirstOrDefault(p => p.ProductoOfertaId == productoSeleccionado.ProductoOfertaId);


            if (productoEnLista == null)
            {
                MessageBox.Show("No se encontró el producto en la lista principal (probable problema de referencia).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mover entre listas
            _productosOfertaDTO.Remove(productoEnLista);
            _productosOfertaDTOQuitarDeOferta.Add(productoEnLista);

            // Forzar refresco si fuera necesario (normalmente no hace falta)
            var cm = (CurrencyManager)BindingContext[_productosOfertaDTO];
            cm.Refresh();
        }

        private ProductosEnOfertaDescuentosDTO ObtenerProducto(DataGridView grillaProductosOferta)
        {
            if (grillaProductosOferta.CurrentRow != null && grillaProductosOferta.CurrentRow.DataBoundItem is ProductosEnOfertaDescuentosDTO producto)
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
            var productoEnListaParaDevolver = _productosOfertaDTOQuitarDeOferta
                .FirstOrDefault(p => p.ProductoOfertaId == productoSeleccionado.ProductoOfertaId);


            if (productoEnListaParaDevolver == null)
            {
                MessageBox.Show("No se encontró el producto en la lista principal (probable problema de referencia).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mover entre listas
            _productosOfertaDTOQuitarDeOferta.Remove(productoEnListaParaDevolver);
            _productosOfertaDTO.Add(productoEnListaParaDevolver);

            // Forzar refresco si fuera necesario (normalmente no hace falta)
            var cm = (CurrencyManager)BindingContext[_productosOfertaDTOQuitarDeOferta];
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

                var ofertaDto = new OfertaDTO
                {
                    Descripcion = txtDescripcion.Text?.Trim(),
                    PrecioFinal = _precioFinal,
                    PrecioOriginal = 1000m,
                    DescuentoTotalFinal = 1, //_precioOriginal - _precioFinal,
                    PorcentajeDescuento = 0.0m,//Convert.ToDecimal(txtPrecioDescuentoPorcentaje.Text),
                    FechaInicio = dtpFechaInicio.Value,
                    FechaFin = dtpFechaFin.Value,
                    CantidadProductosDentroOferta = 1,//_cantidadProductos, // si esto puede ser null, convertí igual
                    EstaActiva = cbxEstaActiva.Checked,//cbxEstaActiva.Checked,
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
                    Productos = _productosOfertaDTO.ToList()
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
    }
}
