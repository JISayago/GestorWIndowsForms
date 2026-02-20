using AccesoDatos;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Venta.DTO;
using Servicios.LogicaNegocio.Venta.Oferta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Producto
{
    public interface IProductoServicio
    {
        IEnumerable<ProductoDTO> ObtenerProductos(string cadenabuscar, string columa);
        IEnumerable<ProductoDTO> ObtenerProductosEliminados(string cadenabuscar, string columa);
        ProductoDTO ObtenerProductoPorId(long productoId);
        ProductosEnOfertaDescuentosDTO ControlarProductoEstaEnOfertaPorId(long productoId);

        EstadoOperacion AgregarQuitarStock(MovilizacionStockDTO mStockDTO);
        EstadoOperacion Insertar(ProductoDTO productoDto);
        EstadoOperacion Modificar(ProductoDTO productoDto, long? productoId);
        EstadoOperacion Eliminar(long productoId);

        IEnumerable<ProductoDTO> ObtenerProductosPorMarcaRubroCategoriaParaOferta(long? MarcaId = null, long? RubroId = null, long? CategoriaId = null);
        void DescontarStockProductos(List<ItemVentaDTO> items, GestorContextDB context);
        void RestaurarStockProductos(List<ItemVentaDTO> items, GestorContextDB context);
    }
}
