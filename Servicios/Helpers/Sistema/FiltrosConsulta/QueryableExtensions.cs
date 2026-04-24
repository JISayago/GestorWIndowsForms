using Servicios.Helpers.Sistema.FiltrosConsulta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> AplicarPaginacion<T>(this IQueryable<T> query, FiltroConsulta filtros)
        {
            var skip = (filtros.Page - 1) * filtros.PageSize;

            return query
                .Skip(skip)
                .Take(filtros.PageSize);
        }
    }
}
