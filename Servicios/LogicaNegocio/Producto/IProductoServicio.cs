using Servicios.Helpers;
using Servicios.LogicaNegocio.Producto.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Producto
{
    public interface IProductoServicio
    {
        IEnumerable<ProductoDTO> ObtenerProductos(string cadenabuscar);
        IEnumerable<ProductoDTO> ObtenerProductosEliminados(string cadenabuscar);
        ProductoDTO ObtenerProductoPorId(long productoId);
        EstadoOperacion Insertar(ProductoDTO productoDto);
        EstadoOperacion Modificar(ProductoDTO productoDto, long? productoId);
        EstadoOperacion Eliminar(long productoId);
    }
}
