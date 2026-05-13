using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Producto.Rubro.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Producto.Rubro
{
    public interface IRubroServicio
    {
        ResultadoPaginacion<RubroDTO> ObtenerRubros(FiltroConsulta filtros);
        RubroDTO ObtenerPorId(long Rubro);

        EstadoOperacion Insertar(RubroDTO RubroDTO);

        EstadoOperacion Modificar(RubroDTO RubroDTO);

        EstadoOperacion Eliminar(long RubroId);

    }
}
