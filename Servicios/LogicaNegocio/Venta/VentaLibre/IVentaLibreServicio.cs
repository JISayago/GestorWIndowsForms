using Servicios.Helpers.Sistema;
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
        IEnumerable<VentaLibreDTO> ObtenerVentasLibres(string cadenabuscar);
        EstadoOperacion AnularVentaLibre(long ventaLibreId);
        List<VentaLibreDTO> ObtenerVentasLibresFiltrados(
    string textoBuscar = null,
    int? estado = null,
    DateTime? fechaDesde = null,
    DateTime? fechaHasta = null);
    }
}
