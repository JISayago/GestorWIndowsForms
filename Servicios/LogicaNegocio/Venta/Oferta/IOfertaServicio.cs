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
       // public EstadoOperacion Modificar(long ofertaId, OfertaDTO dto);
    }
}
