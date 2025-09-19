using AccesoDatos.Entidades;
using Presentacion.Core.Articulo.Marca;
using Presentacion.Core.Categoria;
using Presentacion.Core.Producto.Rubro;
using Servicios.LogicaNegocio.Articulo.Categoria;
using Servicios.LogicaNegocio.Articulo.Marca;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Producto.Rubro;
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
        private bool _esMarca = false;
        private bool _esRubro = false;
        private bool _esCategoria = false;

        private readonly IMarcaServicio _marcaServicio;
        private readonly IRubroServicio _rubroServicio;
        private readonly ICategoriaServicio _categoriaServicio;
        private readonly IProductoServicio _productoServicio;

        public FOfertaGrupoABM()
        {
            InitializeComponent();
            _marcaServicio = new MarcaServicio();
            _rubroServicio = new RubroServicio();
            _categoriaServicio = new CategoriaServicio();
            _productoServicio = new ProductoServicio();
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
                    txtMarca.Text = marca.Nombre;
                }
            }

        }

        private void FOfertaGrupoABM_Load(object sender, EventArgs e)
        {

        }

        private void btnCargarGrupoRubro_Click(object sender, EventArgs e)
        {
            if (_esRubro)
            {
                var fRubro = new FRubroConsulta();
                if (fRubro.ShowDialog() == DialogResult.OK && fRubro.rubroSeleccionado.HasValue)
                {
                    var rubro = _rubroServicio.ObtenerPorId(fRubro.rubroSeleccionado.Value);
                    txtRubro.Text = rubro.Nombre;
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
                    txtCategoria.Text = categoria.Nombre;
                }
            }
        }

        private void btnCargarProductosAlcanzados_Click(object sender, EventArgs e)
        {
            if (!_esCategoria || !_esRubro || !_esMarca) { 
                MessageBox.Show(Text, "Debe seleccionar al menos un grupo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtMarca.Text)|| string.IsNullOrEmpty(txtRubro.Text)|| string.IsNullOrEmpty(txtCategoria.Text))
            {
                MessageBox.Show(Text, "Debe cargar al menos un grupo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            var productos = _productoServicio.ObtenerProductosPorMarcaRubroCategoriaParaOferta(_esMarca ? txtMarca.Text : null,
                                                                                     _esRubro ? txtRubro.Text : null,
                                                                                     _esCategoria ? txtCategoria.Text : null);

        }
    }
}
