using Servicios.Helpers;
using Servicios.LogicaNegocio.Venta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta.VentaLibre.DTO
{
    public class VentaLibreDTO
    {
        public long VentaLibreId { get; set; }

        // Empleado que registra
        public long IdEmpleado { get; set; }
        public string EmpleadoNombreCompleto { get; set; }

        // Vendedor
        public long IdVendedor { get; set; }
        public string VendedorNombreCompleto { get; set; }

        // Cliente
        public long? IdCliente { get; set; }
        public string ClienteNombreCompleto { get; set; }

        // Datos de venta
        public string NumeroVenta { get; set; }
        public DateTime FechaVenta { get; set; }

        public decimal Total { get; set; }

        public int Estado { get; set; }

        public string Detalle { get; set; }

        // Pagos
        public decimal MontoAdeudado { get; set; }
        public decimal MontoPagado { get; set; }

        // Detalle de pagos
        public List<FormaPago> TiposDePagoSeleccionado { get; set; }
    }
}
