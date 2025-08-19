using AccesoDatos.Entidades;
using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Articulo.Marca;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Producto.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Producto
{
    public partial class FProductoABM : FBaseABM
    {
        private readonly IProductoServicio _ProductoServicio;
        private readonly IMarcaServicio _MarcaServicio;
        protected long? EntidadID;
        private List<long> _categoriasSeleccionadas = new List<long>();

        public override void FBaseABM_Load(object sender, EventArgs e)
        {
            base.FBaseABM_Load(sender, e);
            Inicializador(EntidadID);
        }

        public FProductoABM(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _ProductoServicio = new ProductoServicio();
            _MarcaServicio = new MarcaServicio();
            EntidadID = entidadId;

            if (tipoOperacion == TipoOperacion.Eliminar || tipoOperacion == TipoOperacion.Modificar)
            {
                CargarDatos(entidadId);
            }

            if (tipoOperacion == TipoOperacion.Eliminar)
            {
                DesactivarControles(this);
            }
            /*
            AgregarControlesObligatorios(txtProducto, "Producto");
            AgregarControlesObligatorios(txtStock, "Stock");
            AgregarControlesObligatorios(cmbMarca, "Marca");
            */
            var marcas = _MarcaServicio.ObtenerMarca("").ToList();

            cmbMarca.DisplayMember = "Nombre"; // lo que se muestra
            cmbMarca.ValueMember = "Id";
            cmbMarca.DataSource = marcas;

            cmbMarca.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbMarca.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbMarca.DropDownStyle = ComboBoxStyle.DropDown;


            EntidadID = entidadId;
        }

        public override void Inicializador(long? entidadId)
        {
            /*if (entidadId.HasValue) return;

            txtProducto.KeyPress += Validacion.NoSimbolos;
            txtProducto.KeyPress += Validacion.NoNumeros;
            */
        }

        public override void DesactivarControles(object obj)
        {
            base.DesactivarControles(obj);

            btnLimpiar.Enabled = false;
            btnLimpiar.Visible = false;
        }

        public override void CargarDatos(long? entidadId)
        {
            if (!entidadId.HasValue)
            {
                MessageBox.Show(@"Ocurrio un Error Grave", @"Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                this.Close();
            }

            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                btnLimpiar.Enabled = false;
            }

            var Producto = _ProductoServicio.ObtenerProductoPorId(entidadId.Value);

            if (Producto != null)
            {
                txtProducto.Text = Producto.Descripcion;
                txtEstado.Text = Producto.Estado.ToString();
                txtMedida.Text = Producto.Medida;
                txtUnidadMedida.Text = Producto.UnidadMedida;
                txtStock.Text = Producto.Stock.ToString();
                txtPrecioCosto.Text = Producto.PrecioCosto.ToString();
                txtPrecioVenta.Text = Producto.PrecioVenta.ToString();
            }
            else
            {
                MessageBox.Show(@"Ocurrio un Error Grave", @"Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            var ProductoNueva = new ProductoDTO
            {
                Descripcion = txtProducto.Text,
                Stock = int.Parse(txtStock.Text),
                PrecioCosto = int.Parse(txtPrecioCosto.Text),
                PrecioVenta = int.Parse(txtPrecioVenta.Text),
                Estado = int.Parse(txtEstado.Text),
                Medida = txtMedida.Text,
                UnidadMedida = txtUnidadMedida.Text,
                IdMarca = (long)cmbMarca.SelectedValue,
                CategoriaIds = _categoriasSeleccionadas.ToList(),
                EstaEliminado = false
            };

            var response = _ProductoServicio.Insertar(ProductoNueva);

            if (response.Exitoso)
            {
                MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                return true;
            }
            else
            {
                MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return false;
            }

        }

        public override bool EjecutarComandoEliminar()
        {
            if (!EntidadID.HasValue)
            {
                MessageBox.Show(@"´Por favor seleccione una Producto válida.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;

            }
            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                var response = _ProductoServicio.Eliminar((long)EntidadID);
                if (response.Exitoso)
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    return false;
                }

            }
            return false;

        }

        public override bool EjecutarComandoModificar()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            if (TipoOperacion == TipoOperacion.Modificar)
            {
                if (!EntidadID.HasValue)
                {
                    MessageBox.Show(@"´Por favor seleccione un Producto válida.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;

                }

                var ProductoModificar = new ProductoDTO //completar con los datos del producto a modificar
                {
                    Descripcion = txtProducto.Text,
                    Stock = int.Parse(txtStock.Text),
                    PrecioCosto = int.Parse(txtPrecioCosto.Text),
                    PrecioVenta = int.Parse(txtPrecioVenta.Text),
                    Estado = int.Parse(txtEstado.Text),
                    Medida = txtMedida.Text,
                    UnidadMedida = txtUnidadMedida.Text,
                    IdMarca = (long)cmbMarca.SelectedValue,
                    CategoriaIds = _categoriasSeleccionadas.ToList(),
                    EstaEliminado = false
                };

                var response = _ProductoServicio.Modificar(ProductoModificar, ProductoModificar.ProductoId);

                if (response.Exitoso)
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }

            }
            return true;

        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            //si existe el id recien entrar a fcategoriaProducto

            var fCategoriaProducto = new Categoria.FAsignacionCategoriaProducto(EntidadID);

            if (fCategoriaProducto.ShowDialog() == DialogResult.OK)
            {
                // Guardamos internamente las categorías elegidas por el usuario
                _categoriasSeleccionadas = fCategoriaProducto.CategoriasSeleccionadas;

                // Si querés mostrarlas en una textbox invisible o label:
                //txtCategoria.Text = string.Join(",", _categoriasSeleccionadas);
            }
        }
    }
}
