using AccesoDatos;
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
        public void crearLote(LoteDTO lote);

        public List<LoteDTO> obtenerLotesDeUnProducto(long productoId);

        public AccesoDatos.Entidades.Lote obtenerLoteFEFO(long productoId, GestorContextDB context);

        public void actualizarStockLote(decimal cantidadADescontar, long productoId);

        public void loteEstaActivo(long loteId, bool estaActivo);
    }
}
