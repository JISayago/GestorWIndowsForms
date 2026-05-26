using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Articulo.Categoria.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Articulo.Categoria
{
    public interface ICategoriaServicio
    {
        ResultadoPaginacion<CategoriaDTO> ObtenerCategorias(FiltroConsulta filtros);


        CategoriaDTO ObtenerPorId(long categoria);

        EstadoOperacion Insertar(CategoriaDTO categoriaDTO); /* agregar estado operacion a las funciones abm*/

        EstadoOperacion Modificar(CategoriaDTO categoriaDTO);

        EstadoOperacion Eliminar(long CategoriaId);
    }
}
