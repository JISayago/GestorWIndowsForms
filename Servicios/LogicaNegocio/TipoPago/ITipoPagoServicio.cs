using Servicios.Helpers;
using Servicios.LogicaNegocio.Empleado.DTO;
using Servicios.LogicaNegocio.TipoPago.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.TipoPago
{
    public interface ITipoPagoServicio
    {
        IEnumerable<TipoPagoDTO> ObtenerTipoPagos(string cadenabuscar);
        IEnumerable<TipoPagoDTO> ObtenerTipoPagosEliminados(string cadenabuscar);
        EstadoOperacion Insertar(TipoPagoDTO tipoPagoDto);
        TipoPagoDTO ObtenerTipoPagoPorId(long tipoPagoId);
        EstadoOperacion Modificar(TipoPagoDTO tipoPagoDto, long? tipoPagoId);
        EstadoOperacion Eliminar(long tipoPagoId);
    }
}
