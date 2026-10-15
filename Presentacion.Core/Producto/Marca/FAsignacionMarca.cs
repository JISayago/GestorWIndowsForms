using Presentacion.Core.Producto.Asignacion;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Articulo.Marca;
using Servicios.LogicaNegocio.Producto;
using System.Collections.Generic;
using System.Linq;

namespace Presentacion.Core.Producto.Marca
{
    // Sin partial, sin Designer y sin .resx: toda la UI vive en FAsignacionBase.
    public class FAsignacionMarca : FAsignacionBase
    {
        private readonly IProductoServicio _productoServicio = new ProductoServicio();
        private readonly IMarcaServicio _marcaServicio = new MarcaServicio();
        private readonly long? _idInicial;

        // Por defecto selección simple; se puede pasar true si algún día se permiten varias
        public FAsignacionMarca(long? idInicial, bool permiteMultiples = false)
            : base(null, permiteMultiples)
        {
            _idInicial = idInicial;
        }

        protected override string TextoTitulo => "Asignar Marca";
        protected override string TextoPlaceholder => "Buscar marca...";

        protected override PaginaItems ObtenerPagina(string textoBuscar, int pagina, int pageSize)
        {
            var filtros = new FiltroConsulta
            {
                TextoBuscar = textoBuscar,
                Bool1 = false,
                Page = pagina,
                PageSize = pageSize
            };

            var r = _marcaServicio.ObtenerMarcas(filtros);

            return new PaginaItems
            {
                Items = r.Items.Select(m => new ItemSeleccion { Id = m.Id, Nombre = m.Nombre }).ToList(),
                Page = r.Page,
                PageSize = r.PageSize,
                TotalRegistros = r.TotalRegistros
            };
        }

        protected override ItemSeleccion ObtenerPorId(long id)
        {
            var m = _marcaServicio.ObtenerPorId(id);
            return m == null ? null : new ItemSeleccion { Id = m.Id, Nombre = m.Nombre };
        }

        protected override IEnumerable<long> ObtenerIdsAsignados()
            => _idInicial.HasValue && _idInicial.Value > 0
                ? new[] { _idInicial.Value }
                : Enumerable.Empty<long>();
    }

}
