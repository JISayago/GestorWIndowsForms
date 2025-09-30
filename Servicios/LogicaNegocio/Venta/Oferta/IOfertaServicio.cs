using Servicios.Helpers;
using Servicios.LogicaNegocio.Venta.Oferta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta.Oferta
{
    public interface IOfertaServicio
    {
        public EstadoOperacion Insertar(OfertaDTO dto);
        public List<OfertaDTO> ObtenerOfertas(string cadenaBuscar);
        public OfertaDTO ObtenerOfertaPorId(long idOFerta);
    }
}
