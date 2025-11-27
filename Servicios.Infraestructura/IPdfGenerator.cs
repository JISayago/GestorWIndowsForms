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
        string GenerarComprobante(Venta venta);
    }
}
