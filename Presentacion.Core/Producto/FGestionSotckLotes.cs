using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Producto.Lote;
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
    public partial class FGestionSotckLotes : Form
    {
        private readonly ILoteServicio _loteSevicio;
        private readonly IProductoServicio _productoServicio;

        //borrar si no hacen falta
        private decimal stockIncial = 0;
        private decimal stockActual = 0;

        public FGestionSotckLotes(long productoID, string nombreProducto)
        {
            InitializeComponent();

            _loteSevicio = new LoteServicio();
            _productoServicio = new ProductoServicio();

            lblNombreProducto.Text = nombreProducto;
        }
    }
}
