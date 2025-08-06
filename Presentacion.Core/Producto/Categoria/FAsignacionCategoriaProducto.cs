using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
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

        /*
         Cargar los datos de categorias en el datagrid , 
         si el producto ya tiene asignadas cargarlas en el datagrid y marcar las categorias que ya tiene asignadas.

         Permitir seleccionar varias categorias y asignarlas al producto, si el producto ya tiene asignadas las categorias, permitir desasignarlas.

         Boton de aceptar para guardar los cambios y cerrar el formulario.

            
         */

        private readonly IProductoServicio _productoServicio;
        private readonly ICategoriaServicio _categoriaServicio;
        private List<CategoriaDTO> _categoriasDisponibles;
        public List<long> CategoriasSeleccionadas { get; private set; } = new List<long>();

        public FAsignacionCategoriaProducto()
        {
            InitializeComponent();

            //EntidadID = entidadID;
            _productoServicio = new ProductoServicio();
            _categoriaServicio = new CategoriaServicio();

            //CargarDatos(entidadID);
            InicializacionGrillas();
        }

        /* public FAsignacionCategoriaProducto(long? entidadID = null) : this()
         {
             EntidadID = entidadID;
             _productoServicio = new ProductoServicio();
             _categoriaServicio = new CategoriaServicio();

             CargarDatos(entidadID);
             InicializacionGrillas();
         }
        */

        private void CargarDatos(long? entidadId)
        {
            if (entidadId.HasValue)
            {
                var producto = _productoServicio.ObtenerProductoPorId(entidadId.Value);
                MessageBox.Show(producto.CategoriaIds.ToString());
            }

        }

        private void ActualizarGrillas()
        {

            dvgCategoriasProducto.DataSource = null;
            dvgCategoriasProducto.DataSource = _categoriasDisponibles;

            /*
            if (EntidadID.HasValue)
            {
                var categoriasProducto = _productoServicio.ObtenerCategoriasPorProductoId(EntidadID.Value); // List<long> o List<CategoriaDTO>

                foreach (DataGridViewRow row in dvgCategoriasProducto.Rows)
                {
                    var categoria = row.DataBoundItem as CategoriaDTO;
                    if (categoria != null && categoriasProducto.Any(c => c == categoria.Id || c.Id == categoria.Id))
                    {
                        row.Cells["Seleccionado"].Value = true;
                    }
                }
            }
            */
        }

        private void InicializacionGrillas()
        {
            _categoriasDisponibles = _categoriaServicio.ObtenerCategoria(string.Empty).ToList();



            ActualizarGrillas();
            ResetearGrillas(dvgCategoriasProducto);
        }

        private void ResetearGrillas(DataGridView grillaCategorias)
        {
            grillaCategorias.Columns["Id"].Visible = false;
            grillaCategorias.Columns["Id"].Name = "Id";

            grillaCategorias.Columns["Nombre"].Visible = true;
            grillaCategorias.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            grillaCategorias.Columns["EstaEliminado"].Visible = false;

            var checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "Seleccionado";
            checkColumn.HeaderText = "Seleccionar";
            dvgCategoriasProducto.Columns.Add(checkColumn);

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            CategoriasSeleccionadas.Clear();

            foreach (DataGridViewRow row in dvgCategoriasProducto.Rows)
            {
                bool seleccionado = Convert.ToBoolean(row.Cells["Seleccionado"].Value);
                if (seleccionado)
                {
                    var dto = row.DataBoundItem as CategoriaDTO;
                    if (dto != null)
                    {
                        CategoriasSeleccionadas.Add(dto.Id); // o dto.CategoriaId según tu DTO
                    }
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
