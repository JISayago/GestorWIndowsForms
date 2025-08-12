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
        private readonly IProductoServicio _productoServicio;
        private readonly ICategoriaServicio _categoriaServicio;
        private List<CategoriaDTO> _categoriasDisponibles;
        protected long? EntidadID;
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

             CargarDatos(entidadID);
             InicializacionGrillas();
         }

        private void CargarDatos(long? entidadId)
        {
            if (entidadId.HasValue)
            {
                var producto = _productoServicio.ObtenerProductoPorId(entidadId.Value);
                MessageBox.Show(producto.CategoriaIds.ToString());
            }
        }

        private void InicializacionGrillas()
        {
            _categoriasDisponibles = _categoriaServicio.ObtenerCategoria(string.Empty).ToList();

            ActualizarGrillas(EntidadID);
            ResetearGrillas(dvgCategoriasProducto);
        }

        private void ActualizarGrillas(long? EntidadID)
        {

            dvgCategoriasProducto.DataSource = null;
            dvgCategoriasProducto.DataSource = _categoriasDisponibles;


            if (EntidadID.HasValue)
            {
                var categoriasProducto = _productoServicio.ObtenerProductoPorId(EntidadID.Value); // List<long> o List<CategoriaDTO>


                foreach (DataGridViewRow row in dvgCategoriasProducto.Rows)
                {
                    //buscar todas las id de categoria disponibles xq en el dgv tengo solo el id de categoriaProducto

                    var categoria = row.DataBoundItem as CategoriaDTO;

                    if (categoria != null && categoriasProducto.CategoriaIds.Contains(categoria.Id))
                    {
                        row.Cells["Seleccionado"].Value = true;
                    }
                }
            }        
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
        private void EnsureSeleccionadoColumn()
        {
            if (!dvgCategoriasProducto.Columns.Contains("Seleccionado"))
            {
                var checkColumn = new DataGridViewCheckBoxColumn()
                {
                    Name = "Seleccionado",
                    HeaderText = "Seleccionar",
                    TrueValue = true,
                    FalseValue = false
                };
            }
        }
    }
}
