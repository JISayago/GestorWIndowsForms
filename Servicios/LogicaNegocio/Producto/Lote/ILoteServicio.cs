using AccesoDatos;
using Servicios.Helpers.Sistema;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Venta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Producto.Lote
{
    public interface ILoteServicio
    {
        EstadoOperacion CrearLote(LoteDTO lote);
        IEnumerable<LoteDTO> ObtenerLote(string cadenaBuscar);
        EstadoOperacion ModficiarLote(LoteDTO loteDto, long loteId);
        EstadoOperacion EliminarLote(long loteId);
        LoteDTO ObtenerLotePorId(long loteId);
        IEnumerable<LoteDTO> ObtenerLotesEliminados(string cadenabuscar, string columna);
        public List<LoteDTO> ObtenerLotesDeUnProducto(long productoId);
        List<DetalleVentaLoteDTO> DescontarStockLoteFifoLifo(decimal cantidadADescontar, long productoId, bool tieneFechaVencimiento);
        void RestaurarStockLoteFifoLifo(decimal cantidadARestaurar, List<long> loteId, bool tieneFechaVencimiento);
        string GenerarNumeroLote();

    }
}
