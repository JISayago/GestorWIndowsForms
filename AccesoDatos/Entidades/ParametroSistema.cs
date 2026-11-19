using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AccesoDatos.Entidades
{
    /// <summary>
    /// Fila de configuración del sistema guardada en la base (clave -> JSON, o bytes para el logo).
    /// En GestorContextDB agregar:  public DbSet&lt;ParametroSistema&gt; ParametrosSistema { get; set; }
    /// Luego:  Add-Migration AgregarParametrosSistema   y   Update-Database
    /// </summary>
    [Index(nameof(Clave), IsUnique = true)]
    public class ParametroSistema
    {
        [Key]
        public long ParametroSistemaId { get; set; }

        public string Clave { get; set; } = "";

        public string? Valor { get; set; }

        public byte[]? ValorBinario { get; set; }

        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}