using AccesoDatos.Entidades;
using Azure;
using Servicios.Helpers.OpcionesPagos;
using Servicios.Helpers.VentaEnum;
using Servicios.LogicaNegocio.Venta.TipoPago.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta.DTO
{
    public  class VentaDTO
    {
        public long VentaId { get; set; }
        public long IdEmpleado { get; set; }
        public long IdVendedor { get; set; }

        public long? IdCliente { get; set; }
        public string NumeroVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
        public decimal TotalSinDescuento { get; set; }
        public decimal Descuento { get; set; }

        public string ClienteNombreCompleto { get; set; }
        public int Estado { get; set; }
        public string EstadoDescripcion
        {
            get
            {
                return Estado switch
                {
                    (int)EstadoVenta.Confirmada => "Confirmada",
                    (int)EstadoVenta.Cancelada => "Venta Cancelada",
                    (int)EstadoVenta.CancelacionVenta => "Cancelación Venta",
                    _ => "Desconocido"
                };
            }
        }
        public string Detalle { get; set; }
        public string ExtraDetallePago { get; set; } // Para información adicional como DNI o nombre del cliente en pagos.
        public List<ItemVentaDTO> Items { get; set; }
        public List<FormaPago> TiposDePagoSeleccionado { get; set; }
    }
}
