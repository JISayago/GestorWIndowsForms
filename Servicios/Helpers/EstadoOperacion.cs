using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers
{
    public class EstadoOperacion
    {
        public bool Exitoso { get; set; }   
        public string Mensaje { get; set; }
        public long? EntidadId { get; set; }  
    }
}
