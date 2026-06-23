using AccesoDatos;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.Helpers.Venta.Oferta;
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
        ResultadoPaginacion<ProductoDTO> ObtenerProductos(FiltroConsulta filtros);
        ProductoDTO ObtenerProductoPorId(long productoId);
        ProductosEnOfertaDescuentosDTO ControlarProductoEstaEnOfertaPorId(long productoId);

        EstadoOperacion AgregarQuitarStock(MovilizacionStockDTO mStockDTO);
        EstadoOperacion Insertar(ProductoDTO productoDto);
        EstadoOperacion Modificar(ProductoDTO productoDto, long? productoId);
        EstadoOperacion Eliminar(long productoId);

        IEnumerable<ProductoOfertaDTO> ObtenerProductosPorMarcaRubroCategoriaParaOferta(FiltroBusquedaComboGrupo filtroGrupo);
        List<DetalleVentaLoteDTO> DescontarStockProductos(List<ItemVentaDTO> items, GestorContextDB context);
        void RestaurarStockProductos(List<ItemVentaDTO> items, GestorContextDB context, long ventdaId);
        void ModificarEstadoStockProductos(GestorContextDB context);
    }
}
