using Servicios.Helpers;
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
        IEnumerable<CategoriaDTO> ObtenerCategoria(string cadenaBuscar);

        IEnumerable<CategoriaDTO> ObtenerCategoriaEliminada(string cadenaBuscar);

        CategoriaDTO ObtenerPorId(long marca);

        EstadoOperacion Insertar(CategoriaDTO categoriaDTO); /* agregar estado operacion a las funciones abm*/

        EstadoOperacion Modificar(CategoriaDTO categoriaDTO);

        EstadoOperacion Eliminar(long CategoriaId);
    }
}
