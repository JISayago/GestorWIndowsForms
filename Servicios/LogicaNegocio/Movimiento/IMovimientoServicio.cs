using AccesoDatos;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Movimiento.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Servicios.LogicaNegocio.Movimiento
{
    public interface IMovimientoServicio
    {
        void CrearMovimientoVenta(Venta.DTO.VentaDTO ventaDto, long cajaId, TipoMovimientoDetalle detalleTipo, GestorContextDB context);
        void CrearMovimientoCtaCte(decimal total, long cajaId, long ctacteId, TipoMovimientoDetalle detalleTipo, GestorContextDB context);

        MovimientoDTO ObtenerMovimientoPorId(long movimientoId);
        IEnumerable<MovimientoDTO> ObtenerMovimiento(string cadenabuscar);
        IEnumerable<MovimientoDTO> ObtenerMovimientoEliminado(string cadenabuscar);
    }
}
