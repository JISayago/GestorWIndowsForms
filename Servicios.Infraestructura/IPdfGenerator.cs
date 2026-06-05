using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Servicios.Infraestructura
{
    public interface IPdfGenerator
{
    string GenerarVenta(Venta venta);

    string GenerarVentaLibre(VentaLibre venta);

    string GenerarCancelacionVenta(Venta venta);

    string GenerarGasto(Gasto gasto);

    string GenerarGastoAnulado(Gasto gasto);
}
}
