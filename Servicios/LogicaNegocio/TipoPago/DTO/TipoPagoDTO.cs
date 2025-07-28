using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.TipoPago.DTO
{
    public class TipoPagoDTO
    {
        public long TipoPagoId { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        public string Codigo { get; set; }
        public bool EstaEliminado { get; set; }
    }
}
