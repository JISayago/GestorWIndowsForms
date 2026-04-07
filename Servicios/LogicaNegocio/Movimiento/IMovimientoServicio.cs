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
        void CrearMovimientoVenta(long ventaId, decimal monto,int estado, TipoMovimientoDetalle detalleTipo, TipoEntidadMovimiento tipoEntidad, GestorContextDB context);
        void CrearMovimientoCtaCte(decimal total, long cajaId, long ctacteId, TipoMovimientoDetalle detalleTipo, GestorContextDB context);
        void CrearMovimientoGasto(long gastoId, decimal monto, TipoMovimientoDetalle detalleTipo, GestorContextDB context);
        MovimientoDTO ObtenerMovimientoPorId(long movimientoId);
        IEnumerable<MovimientoDTO> ObtenerMovimiento(string cadenabuscar);
        IEnumerable<MovimientoDTO> ObtenerMovimientoEliminado(string cadenabuscar);
    }
}
