using Servicios.Helpers;
using Servicios.LogicaNegocio.Producto.DTO;
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
        public List<OfertaDTO> ObtenerOfertasActivas(string cadenaBuscar);
        public List<OfertaDTO> ObtenerOfertasActivasCompuestas(string cadenaBuscar);
        public List<OfertaDTO> ObtenerOfertasInactivas(string cadenaBuscar);
        public List<OfertaDTO> ObtenerOfertasInactivasCompuesta(string cadenaBuscar);
        public List<InformacionExistenciaOfertaDescuentoProducto> ObtenerProductosEnOferta(List<ProductoDTO> productosDentroOferta);
        public OfertaDTO ObtenerOfertaPorId(long idOFerta);
        public OfertaDTO ObtenerOfertaActivaPorId(long idOFerta);

        
        public List<OfertaDTO> ObtenerOfertasActivasInactivas(string cadenaBuscar);

        public OfertaDTO? ActivarDesactivar(long ofertaId);

        public bool ExisteOfertaPorCodigo(string codigo);
    }
}
