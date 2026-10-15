using Presentacion.Core.Producto.Asignacion;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Articulo.Categoria;
using Servicios.LogicaNegocio.Producto;
using System.Collections.Generic;
using System.Linq;

namespace Presentacion.Core.Producto.Categoria
{
    // Sin partial, sin Designer y sin .resx: toda la UI vive en FAsignacionBase.
    public class FAsignacionCategoria : FAsignacionBase
    {
        private readonly ICategoriaServicio _categoriaServicio = new CategoriaServicio();
        private readonly List<long> _idsIniciales;

        public FAsignacionCategoria(IEnumerable<long> idsIniciales)
            : base(null, permiteMultiples: true)
        {
            _idsIniciales = idsIniciales?.ToList() ?? new List<long>();
        }

        protected override string TextoTitulo => "Asignar Categorías";
        protected override string TextoPlaceholder => "Buscar categoría...";

        protected override PaginaItems ObtenerPagina(string textoBuscar, int pagina, int pageSize)
        {
            var filtros = new FiltroConsulta
            {
                TextoBuscar = textoBuscar,
                Bool1 = false, // no eliminados
                Page = pagina,
                PageSize = pageSize
            };

            var r = _categoriaServicio.ObtenerCategorias(filtros);

            return new PaginaItems
            {
                Items = r.Items.Select(c => new ItemSeleccion { Id = c.Id, Nombre = c.Nombre }).ToList(),
                Page = r.Page,
                PageSize = r.PageSize,
                TotalRegistros = r.TotalRegistros
            };
        }

        protected override ItemSeleccion ObtenerPorId(long id)
        {
            var c = _categoriaServicio.ObtenerPorId(id);
            return c == null ? null : new ItemSeleccion { Id = c.Id, Nombre = c.Nombre };
        }
        protected override IEnumerable<long> ObtenerIdsAsignados() => _idsIniciales;
        //protected override IEnumerable<long> ObtenerIdsAsignados()
        //{
        //    if (!EntidadID.HasValue)
        //        return Enumerable.Empty<long>();

        //    var producto = _productoServicio.ObtenerProductoPorId(EntidadID.Value);
        //    return producto?.CategoriaIds ?? Enumerable.Empty<long>();
        //}
    }
}
