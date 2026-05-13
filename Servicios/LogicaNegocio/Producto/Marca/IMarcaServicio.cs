using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Articulo.Marca.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Articulo.Marca
{
    public interface IMarcaServicio
    {
        ResultadoPaginacion<MarcaDTO> ObtenerMarcas(FiltroConsulta filtros);
        MarcaDTO ObtenerPorId(long marca);

        EstadoOperacion Insertar(MarcaDTO marcaDTO);

        EstadoOperacion Modificar(MarcaDTO marcaDTO);

        EstadoOperacion Eliminar(long marcaId);
    }
}
