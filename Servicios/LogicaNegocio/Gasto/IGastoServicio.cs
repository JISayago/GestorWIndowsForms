using AccesoDatos;
using Servicios.Helpers;
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
        List<GastoDTO> ObtenerGastos(int? estadoGasto);
        EstadoOperacion AnularGasto(long gastoId);
    }
}
