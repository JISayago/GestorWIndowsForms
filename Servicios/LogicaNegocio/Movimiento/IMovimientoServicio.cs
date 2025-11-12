using Servicios.LogicaNegocio.Movimiento.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Movimiento
{
    public interface IMovimientoServicio
    {
        void CrearMovimientoVenta(Venta.DTO.VentaDTO ventaDto);
        MovimientoDTO ObtenerMovimientoPorId(long movimientoId);
        IEnumerable<MovimientoDTO> ObtenerMovimientos();
    }
}
