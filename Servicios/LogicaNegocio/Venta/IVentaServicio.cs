using AccesoDatos;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Empleado.DTO;
using Servicios.LogicaNegocio.Venta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta
{
    public interface IVentaServicio
    {
        EstadoOperacion NuevaVenta (VentaDTO ventaDto);
        List<long> ObtenerVentasParaCancelacion(DateTime fecha, string filtroNumero = null);
        public List<VentaDTO> ObtenerVentasPorIds(List<long> ventaIds);
        EstadoOperacion CancelacionVentaPorId(long ventaId);
        VentaDTO ObtenerVentaDetalle(long Ventaid);
    }
}
