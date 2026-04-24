using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class NotaRapida
    {
        // Usaremos un ID fijo (por ejemplo, 1) para asegurar que siempre sea el mismo registro
        public int NotaId { get; set; } = 1;

        public string Cuerpo { get; set; }

        public DateTime FechaModificacion { get; set; }

        // Opcional: para saber quién dejó el último mensaje
        public string UsuarioNombre { get; set; }
    }
}
