using Servicios.Helpers;
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
        IEnumerable<MarcaDTO> ObtenerMarca(string cadenaBuscar);

        IEnumerable<MarcaDTO> ObtenerMarcaEliminada(string cadenaBuscar);

        MarcaDTO ObtenerPorId(long marca);

        EstadoOperacion Insertar(MarcaDTO marcaDTO);

        EstadoOperacion Modificar(MarcaDTO marcaDTO);

        EstadoOperacion Eliminar(long marcaId);
    }
}
