using System.Collections.Generic;

namespace Presentacion.Core.Producto.Asignacion
{
    // Item mínimo que usa el form base. Cada form hijo mapea su DTO a esto.
    public class ItemSeleccion
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
    }

    // Página de resultados ya mapeada (se arma desde resultado.Items / Page / PageSize / TotalRegistros)
    public class PaginaItems
    {
        public List<ItemSeleccion> Items { get; set; } = new List<ItemSeleccion>();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRegistros { get; set; }
    }
}
