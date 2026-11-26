using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.Sistema
{
    public class EstadoOperacion
    {
        public bool Exitoso { get; set; }   
        public string Mensaje { get; set; }
        public long? EntidadId { get; set; }  

        public string DatoExtra { get; set; } = string.Empty;

        /// <summary>
        /// true cuando la operación no se hizo por una regla del negocio que el usuario puede resolver
        /// (ej. la caja no tiene efectivo suficiente): la pantalla lo muestra como aviso y no como error.
        /// </summary>
        public bool EsAdvertencia { get; set; }
    }
}
