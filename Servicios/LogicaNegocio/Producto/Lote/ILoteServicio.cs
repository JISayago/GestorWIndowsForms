using AccesoDatos;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Producto.DTO;
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
        void ModficiarLote(long loteId);
        void EliminarLote(long loteId);
        /*public List<LoteDTO> ObtenerLotesDeUnProducto(long productoId);*/
        void DescontarStockLoteFifoLifo(decimal cantidadADescontar, long productoId, bool tieneFechaVencimiento);
        void RestaurarStockLoteFifoLifo(decimal cantidadARestaurar, List<long> loteId, bool tieneFechaVencimiento);

    }
}
