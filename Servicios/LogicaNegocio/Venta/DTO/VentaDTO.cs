using AccesoDatos.Entidades;
using Azure;
using Servicios.Helpers;
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
        public string NumeroVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
        public decimal TotalSinDescuento { get; set; }
        public decimal Descuento { get; set; }
        public int Estado { get; set; }
        public string Detalle { get; set; }
        public List<ItemVentaDTO> Items { get; set; }
        public List<FormaPago> TiposDePagoSeleccionado { get; set; }
    }
}
