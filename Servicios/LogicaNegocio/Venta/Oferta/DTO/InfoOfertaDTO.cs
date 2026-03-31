using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta.Oferta.DTO
{
    public class InfoOfertaDTO
    {
        public string Titulo { get; set; }

        // Texto principal (mensaje fuerte)
        public string TextoPrincipal { get; set; }

        // Texto secundario (detalle o explicación)
        public string TextoSecundario { get; set; }

        public int Tipo;
    }
}
