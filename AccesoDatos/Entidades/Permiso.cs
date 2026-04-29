using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Permiso
    {
        [Key]
        public int PermisoId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; } = string.Empty;

    }
}
