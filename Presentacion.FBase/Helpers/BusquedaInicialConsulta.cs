namespace Presentacion.FBase.Helpers
{
    /// <summary>
    /// Filtros con los que una consulta (FBaseConsulta) debe abrirse la primera vez, por ejemplo
    /// al llegar desde un aviso de la pantalla principal. Todo es opcional: lo que quede en null
    /// no se toca y la consulta usa su valor por defecto.
    /// </summary>
    public class BusquedaInicialConsulta
    {
        /// <summary>Texto a escribir en el cuadro de búsqueda.</summary>
        public string TextoBuscar { get; set; }

        /// <summary>Valor del combo 1 ("Buscar por"), por ejemplo "NumeroLote" o "Codigo".</summary>
        public object Filtro1 { get; set; }

        /// <summary>Valor del combo 2 (estado / tipo), por ejemplo "Vencido".</summary>
        public object Filtro2 { get; set; }

        /// <summary>Valor del combo 3.</summary>
        public object Filtro3 { get; set; }

        /// <summary>Estado inicial del check 1 (null = no tocar).</summary>
        public bool? Bool1 { get; set; }

        /// <summary>Estado inicial del check 2 (null = no tocar).</summary>
        public bool? Bool2 { get; set; }
    }
}
