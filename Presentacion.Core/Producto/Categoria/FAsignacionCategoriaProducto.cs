using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Articulo.Categoria;
using Servicios.LogicaNegocio.Articulo.Categoria.DTO;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.Empleado.Rol;
using Servicios.LogicaNegocio.Empleado.Rol.DTO;
using Servicios.LogicaNegocio.Producto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Producto.Categoria
{
    public partial class FAsignacionCategoriaProducto : FBase.FBase
    {
        private const int TAMANIO_PAGINA = 50;

        private readonly IProductoServicio _productoServicio;
        private readonly ICategoriaServicio _categoriaServicio;
        private List<CategoriaDTO> _categoriasDisponibles;

        // Ids de categorias tildadas, se mantiene aparte de la grilla para no perder
        // la seleccion cuando se filtra/repagina y las filas tildadas dejan de estar visibles.
        private readonly HashSet<long> _categoriasSeleccionadasIds = new HashSet<long>();

        private bool _cargandoGrilla;

        protected long? EntidadID;
        public List<string> descripcionCategorias = new List<string>();
        public List<long> CategoriasSeleccionadas { get; private set; } = new List<long>();

        public FAsignacionCategoriaProducto()
        {
            InitializeComponent();
        }

        public FAsignacionCategoriaProducto(long? entidadID) : this()
        {
            EntidadID = entidadID;
            _productoServicio = new ProductoServicio();
            _categoriaServicio = new CategoriaServicio();

            InicializacionGrillas();
        }

        private void InicializacionGrillas()
        {
            ResetearGrillas(dvgCategoriasProducto);

            CargarCategoriasSeleccionadasDelProducto();

            BuscarCategorias(string.Empty);
        }

        private void CargarCategoriasSeleccionadasDelProducto()
        {
            if (!EntidadID.HasValue)
                return;

            var producto = _productoServicio.ObtenerProductoPorId(EntidadID.Value);

            if (producto?.CategoriaIds == null)
                return;

            foreach (var categoriaId in producto.CategoriaIds)
                _categoriasSeleccionadasIds.Add(categoriaId);
        }

        // Trae solo las categorias que matchean el texto buscado (paginadas), en vez de
        // traer todas las categorias existentes de una.
        private void BuscarCategorias(string textoBuscar)
        {
            var filtros = new FiltroConsulta
            {
                TextoBuscar = textoBuscar?.Trim() ?? string.Empty,
                Bool1 = false, // no eliminados
                Page = 1,
                PageSize = TAMANIO_PAGINA
            };

            var categorias = _categoriaServicio
                .ObtenerCategorias(filtros)
                .Items
                .ToList();

            // Si hay categorias tildadas (p.ej. las que ya tiene el producto) que no
            // entraron en esta página/búsqueda, las agregamos igual para que sigan
            // viéndose marcadas en vez de desaparecer de la grilla.
            var idsFaltantes = _categoriasSeleccionadasIds
                .Except(categorias.Select(c => c.Id))
                .ToList();

            foreach (var id in idsFaltantes)
            {
                var categoria = _categoriaServicio.ObtenerPorId(id);
                if (categoria != null)
                    categorias.Insert(0, categoria);
            }

            _categoriasDisponibles = categorias;

            ActualizarGrilla();
        }

        private void ActualizarGrilla()
        {
            dvgCategoriasProducto.DataSource = null;
            dvgCategoriasProducto.DataSource = _categoriasDisponibles;
        }

        private void ResetearGrillas(DataGridView grillaCategorias)
        {
            grillaCategorias.AutoGenerateColumns = true;
            grillaCategorias.DataSource = null;
            grillaCategorias.Columns.Clear();

            grillaCategorias.DataBindingComplete += DvgCategoriasProducto_DataBindingComplete;
            grillaCategorias.CurrentCellDirtyStateChanged += DvgCategoriasProducto_CurrentCellDirtyStateChanged;
            grillaCategorias.CellValueChanged += DvgCategoriasProducto_CellValueChanged;
        }

        private void DvgCategoriasProducto_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var grillaCategorias = dvgCategoriasProducto;

            if (grillaCategorias.Columns.Contains("Id"))
            {
                grillaCategorias.Columns["Id"].Visible = false;
            }

            if (grillaCategorias.Columns.Contains("Nombre"))
            {
                grillaCategorias.Columns["Nombre"].Visible = true;
                grillaCategorias.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (grillaCategorias.Columns.Contains("EstaEliminado"))
            {
                grillaCategorias.Columns["EstaEliminado"].Visible = false;
            }

            if (!grillaCategorias.Columns.Contains("Seleccionado"))
            {
                var checkColumn = new DataGridViewCheckBoxColumn();
                checkColumn.Name = "Seleccionado";
                checkColumn.HeaderText = "Seleccionar";
                grillaCategorias.Columns.Add(checkColumn);
            }

            // Se marca acá, dentro del propio evento de binding, para garantizar que
            // ya existan tanto las filas como la columna "Seleccionado" en este momento.
            MarcarCategoriasSeleccionadas();
        }

        private void MarcarCategoriasSeleccionadas()
        {
            _cargandoGrilla = true;

            foreach (DataGridViewRow row in dvgCategoriasProducto.Rows)
            {
                var categoria = row.DataBoundItem as CategoriaDTO;

                if (categoria != null)
                    row.Cells["Seleccionado"].Value = _categoriasSeleccionadasIds.Contains(categoria.Id);
            }

            _cargandoGrilla = false;
        }

        // Confirma inmediatamente el click sobre el checkbox para que dispare CellValueChanged
        private void DvgCategoriasProducto_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dvgCategoriasProducto.IsCurrentCellDirty)
                dvgCategoriasProducto.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        // Mantiene el HashSet de seleccionados sincronizado con lo que el usuario tilda/destilda
        private void DvgCategoriasProducto_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_cargandoGrilla || e.RowIndex < 0)
                return;

            if (dvgCategoriasProducto.Columns[e.ColumnIndex].Name != "Seleccionado")
                return;

            var row = dvgCategoriasProducto.Rows[e.RowIndex];
            var categoria = row.DataBoundItem as CategoriaDTO;

            if (categoria == null)
                return;

            bool seleccionado = Convert.ToBoolean(row.Cells["Seleccionado"].Value);

            if (seleccionado)
                _categoriasSeleccionadasIds.Add(categoria.Id);
            else
                _categoriasSeleccionadasIds.Remove(categoria.Id);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // Reinicia el timer en cada tecla: la búsqueda se dispara recién
            // cuando el usuario deja de escribir un instante (evita golpear la
            // base de datos en cada letra).
            timerBusqueda.Stop();
            timerBusqueda.Start();
        }

        private void timerBusqueda_Tick(object sender, EventArgs e)
        {
            timerBusqueda.Stop();
            BuscarCategorias(txtBuscar.Text);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Fuerza el commit por si quedó un check editado sin confirmar
            if (dvgCategoriasProducto.IsCurrentCellDirty)
                dvgCategoriasProducto.CommitEdit(DataGridViewDataErrorContexts.Commit);

            CategoriasSeleccionadas = _categoriasSeleccionadasIds.ToList();

            descripcionCategorias.Clear();

            // Los nombres de las categorias que no están cargadas actualmente en la grilla
            // (porque quedaron fuera del filtro/página) se resuelven aparte para no perderlos.
            var idsPendientes = _categoriasSeleccionadasIds.ToList();

            foreach (var categoria in _categoriasDisponibles.Where(c => _categoriasSeleccionadasIds.Contains(c.Id)))
            {
                descripcionCategorias.Add(categoria.Nombre);
                idsPendientes.Remove(categoria.Id);
            }

            foreach (var id in idsPendientes)
            {
                var categoria = _categoriaServicio.ObtenerPorId(id);
                if (categoria != null)
                    descripcionCategorias.Add(categoria.Nombre);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FAsignacionCategoriaProducto_Load(object sender, EventArgs e)
        {
        }
    }
}
