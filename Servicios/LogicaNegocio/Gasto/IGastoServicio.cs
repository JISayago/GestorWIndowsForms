using AccesoDatos;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Gasto.DTO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Gasto
{
    public interface IGastoServicio
    {
        EstadoOperacion NuevoGasto(GastoDTO gastoDto);
        GastoDTO ObtenerGastoPorId(long gastoId);
        //List<GastoDTO> ObtenerGastos(int? estadoGasto);
        ResultadoPaginacion<GastoDTO> ObtenerGastos(FiltroConsulta filtros);
        EstadoOperacion AnularGasto(long gastoId);
        EstadoOperacion ConfirmarPago(long gastoId);
    }
}
