using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Venta.DTO;
using Servicios.LogicaNegocio.Venta.VentaLibre.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta.VentaLibre
{
    public interface IVentaLibreServicio
    {
        EstadoOperacion NuevaVentaLibre(VentaLibreDTO ventaLibreDto);
        EstadoOperacion AnularVentaLibre(long ventaLibreId);
        ResultadoPaginacion<VentaLibreDTO> ObtenerVentasLibres(FiltroConsulta filtros);
    }
}
